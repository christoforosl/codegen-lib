Imports System.IO
Imports system.Reflection
Imports system.Threading
Imports System.Linq

Friend Class ORADBUpdater
    Inherits DBUpdater

    Protected Friend Overrides Function getSQLCommandSeparator() As String
        Return vbCrLf & "/" & vbCrLf
    End Function

    Protected Friend Overrides Function getSQLCommandFileName(ByVal iVersion As Integer) As String
        Return "dbUpdate_" & CStr(iVersion) & "_ORA.SQL"
    End Function

    Protected Friend Overrides Function getVersionAndLockTable() As Int32

        Dim rs As IDataReader
        Dim dbversion As Int32

        rs = _dbconn.getDataReader("SELECT version from DatabaseVersion ORDER BY VERSION DESC FOR UPDATE NOWAIT") 'lock table exclusively
        If rs.Read Then
            dbversion = CInt(rs(0))
        Else
            Throw New ApplicationException("No Database file version info found.")
        End If

        _dbconn.closeDataReader(rs)
        Return dbversion

    End Function

End Class

Friend Class MSSQLDBUpdater
    Inherits DBUpdater

    Protected Friend Overrides Function getSQLCommandSeparator() As String
        Return vbCrLf & "go" & vbCrLf
    End Function

    Protected Friend Overrides Function getSQLCommandFileName(ByVal iVersion As Integer) As String

        Return "dbUpdate_" & CStr(iVersion) & "_MS.SQL"

    End Function

    Protected Friend Overrides Function getVersionAndLockTable() As Int32

        Dim rs As IDataReader
        Dim dbversion As Int32

        ' the following creates the DatabaseVersion table if it does not exist
        _dbconn.executeSQL("if not exists(select * from sysobjects where id=object_id('DatabaseVersion'))CREATE TABLE [dbo].[DatabaseVersion]([version] [int] NOT NULL,[VersionDate] [datetime] NOT NULL DEFAULT (getdate()))")

        rs = _dbconn.getDataReader("SELECT isnull(max(version), 0) from DatabaseVersion WITH (TABLOCKX)") 'lock table exclusively
        If rs.Read Then
            dbversion = CInt(rs(0))
        Else
            Throw New ApplicationException("No Database file version info found.")
        End If

        _dbconn.closeDataReader(rs)
        Return dbversion

    End Function

End Class

Friend Class MSAccessUpdater
    Inherits DBUpdater

    Protected Friend Overrides Function getSQLCommandSeparator() As String
        Return vbCrLf & "/" & vbCrLf
    End Function

    Protected Friend Overrides Function getSQLCommandFileName(ByVal iVersion As Integer) As String
        Return "dbUpdate_" & CStr(iVersion) & "_AC.SQL"
    End Function

    Protected Friend Overrides Function getVersionAndLockTable() As Int32

        Dim rs As IDataReader
        Dim dbversion As Int32

        rs = _dbconn.getDataReader("SELECT max(version) from DatabaseVersion") 'lock table exclusively
        If rs.Read Then
            dbversion = CInt(rs(0))
        Else
            Throw New ApplicationException("No Database file version info found.")
        End If

        _dbconn.closeDataReader(rs)
        Return dbversion

    End Function

End Class

''' <summary>
''' Class to fascilitate easy upgrade of a database.
''' </summary>
''' <remarks>
''' Requirements:
''' <ol>
''' <li>Create a table in your database called <b>sysDatabaseVersion</b>
''' with 2 columns: 
''' <ul><li>DatabaseVersion, integer</li>
''' <li>VersionDate, datetime, default: current date (getDate() in MSSQL, 
''' sysdate in Oracle.</li>
''' </ul>
''' </li>
''' <li>
''' Define a constant in your application that defines the database version
''' that your code expects.
''' </li>
''' <li>
''' In your assembly, create a directory where you will place the script files that contain 
''' the sql statements to execute to bring the database to the required version.  The sql statements 
''' must be separated by the "new line" + go + "new Line", ie  the word "go" or "GO" in a line by itself.
''' For oracle, the command separator is "/" on a line by it self.
''' <b>Important: all your sql script files should be marked as <b>embedded resource</b>.</b>
''' Naming conventions: 
''' <ul><li>For MS SQL files: "dbUpdate_&lt;version&gt;_MS.SQL".<br/>For example: dbUpdate_400_MS.SQL</li>
''' <li>For Oracle files: dbUpdate_&lt;version&gt;_ORA.SQL.<br/>For example: dbUpdate_400_ORA.SQL</li>
''' </ul>
''' </li>
''' </ol>
''' 
''' At your system startup, create a new DBUtils object, <see cref="org.model.lib.db.DBUtils">DBUtils object</see>, 
''' and call public shared sub DBUpdater.dbUpdateVersion. See also <seealso cref="DBUpdater.dbUpdateVersion">dbUpdateVersion</seealso>
''' dbUpdateVersion works by comparing the application database version with the database version stored in
''' the sysDatabaseVersion table.  If the application database version is greater than the latest version number 
''' in sysDatabaseVersion, then a loop is executed while  [sysDatabaseVersion] &lt; [application database version]
''' opening sql script files and executing the commands for each version.
''' After each version sql file is done, a new row is inserted in table sysDatabaseVersion, updating the version,
''' until the versions come to the same lebel.
''' </remarks>
Public MustInherit Class DBUpdater

    Protected _dbconn As DBUtils
    Protected _codeDatabaseVersion As Integer
    Protected _assemblyName As Assembly

    Public Property encoding() As System.Text.Encoding = System.Text.Encoding.GetEncoding("Windows-1253")

    Protected Friend MustOverride Function getVersionAndLockTable() As Int32

    'the character or string that the sql commands are separated with
    Protected Friend MustOverride Function getSQLCommandSeparator() As String
    Protected Friend MustOverride Function getSQLCommandFileName(ByVal iVersion As Integer) As String

    Public Event VersionUpgradeCompleted(ByVal iversion As Integer)

    ''' <summary>
    ''' Command to execute for backing up the database.  Only applies for sql server.
    ''' </summary>
    Public Property backupSQLStatement As String

    Private Shared Function getResourceStream(ByVal resname As String, ByVal assembly As Assembly) As Stream

        Dim resourceName As String = assembly.GetManifestResourceNames().FirstOrDefault(Function(xc)
                                                                                            Return xc.EndsWith("." & resname)
                                                                                        End Function)


        Return assembly.GetManifestResourceStream(resourceName)

    End Function

    Private Function getResourceFileText(ByVal resname As String, ByVal assembly As Assembly) As String

        Dim templ As String = String.Empty
        Dim d As Stream = getResourceStream(resname, assembly)
        Using ds As StreamReader = New StreamReader(d, Me.encoding)
            Dim tline As String
            Do
                tline = ds.ReadLine()
                templ &= tline & vbCrLf

            Loop Until tline Is Nothing

            Return templ

        End Using

    End Function

    Private Sub upgradeDatabase()

        Dim myerrprefix As String
        Dim scriptFile As String
        Dim sqlFile As String
        Dim execSQL As String

        Dim i As Integer
        Dim arrSQL() As String
        Dim oLock As Object = New Object
        Dim dbversion As Int32
        Dim dbName As String

        System.Threading.Monitor.Enter(oLock)

        Try
            dbversion = Me.getVersionAndLockTable

            If dbversion = _codeDatabaseVersion Then
                'good!

            ElseIf dbversion > _codeDatabaseVersion Then
                'Throw New ApplicationException("Bad File Version: " & dbversion & ".  Expected version less than " & _codeDatabaseVersion)
                'ErrorLogging.addError("Newer Database Version: " & dbversion & ".  Expected version less than " & _codeDatabaseVersion, "", "", ErrorLogging.enumErrType.ERR_INFO)
            Else

                If String.IsNullOrEmpty(Me.backupSQLStatement) = False Then
                    Dim sqlBackup As String = String.Format(Me.backupSQLStatement, CStr(dbversion), CStr(_codeDatabaseVersion))
                    _dbconn.executeSQL(sqlBackup)
                End If

                myerrprefix = "Error upgrading to version " & _codeDatabaseVersion & ": "

                'we have the _codeDatabaseVersion constant
                'and we compare it with dbversion. the version stored
                'in the database.  If dbversion is less than _codeDatabaseVersion
                Do While dbversion < _codeDatabaseVersion

                    scriptFile = Me.getSQLCommandFileName(CShort(dbversion))
                    sqlFile = getResourceFileText(scriptFile, Me._assemblyName)
                    arrSQL = Split(sqlFile, Me.getSQLCommandSeparator, , CompareMethod.Text)

                    _dbconn.beginTrans()
                    dbName = _dbconn.Connection.Database
                    For i = 0 To UBound(arrSQL)
                        'Call cPrg.prgProgress(CLng(i))
                        execSQL = arrSQL(i)
                        If Trim(Replace(execSQL, vbCrLf, "")) <> "" Then
                            _dbconn.executeSQL(execSQL)
                        End If
                    Next

                    RaiseEvent VersionUpgradeCompleted(dbversion)
                    dbversion = dbversion + 1

                    Call _dbconn.executeSQL("INSERT INTO DatabaseVersion (version) VALUES ('" & dbversion & "') ")

                    _dbconn.commitTrans()
                Loop
                'Call changeEname(1)

                execSQL = ""

            End If
            _dbconn.commitTrans()


        Catch ex As Exception
            Dim errMsg As String = scriptFile & vbCrLf & "Error Updating database """ & dbName & """ to version " & dbversion & vbCrLf & ex.Message & vbCrLf & ex.StackTrace
            'ErrorLogging.addError(errMsg, "", "", ErrorLogging.enumErrType.ERR_INFO)
            Throw New ApplicationException(errMsg)

        Finally
            _dbconn.rollbackTrans()
            System.Threading.Monitor.Exit(oLock)
        End Try

    End Sub

#Region "Public class interface"

    ''' <summary>
    ''' Creates an updater class instance and brings the database to the target version
    ''' </summary>
    ''' <param name="_dbconn">Database connection to your database</param>
    ''' <param name="_dbversion">The target version</param>
    ''' <param name="_backupSQLStatement">SQL to execute before the upgrade to backup database</param>
    ''' <param name="_assembly">the assembly that contains the embedded resource sql files</param>
    Public Shared Sub dbUpdateVersion(ByVal _dbconn As DBUtils, _
                                           ByVal _dbversion As Integer, _
                                           ByVal _assembly As Assembly, _
                                           Optional ByVal encoding As System.Text.Encoding = Nothing, _
                                           Optional ByVal _backupSQLStatement As String = "")

        Dim dbu As DBUpdater = Nothing

        If _dbconn.ConnType = DBUtils.enumConnType.CONN_OLEDB Then
            dbu = New MSAccessUpdater

        ElseIf _dbconn.ConnType = DBUtils.enumConnType.CONN_MSSQL Then
            dbu = New MSSQLDBUpdater
            dbu.backupSQLStatement = _backupSQLStatement
        ElseIf _dbconn.ConnType = DBUtils.enumConnType.CONN_ORACLE Then
            dbu = New ORADBUpdater
        End If

        dbu._dbconn = _dbconn
        dbu._codeDatabaseVersion = _dbversion
        dbu._assemblyName = _assembly
        dbu.encoding = CType(IIf(encoding Is Nothing, System.Text.Encoding.UTF8, encoding), System.Text.Encoding)



        dbu.upgradeDatabase()

    End Sub

#End Region



End Class

