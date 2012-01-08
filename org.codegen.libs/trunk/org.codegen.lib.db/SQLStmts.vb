Option Strict On

Imports System.IO
Imports System.IO.File
Imports System.Xml
Imports System.Configuration
Imports System.Reflection
Imports System.Threading

Public Class SQLStmts

    Private domDs As DataSet
    Private dv As DataView
    Private _sqlStream As Stream
    Private _stmtsFile As String

    Private Const CONFIG_SQL_DIALECT As String = "sqlDialect"
    Private Const CONFIG_SQL_STMTS_FILE As String = "sqlStatementFile"

    Public Shared Function getStmtsFromResource(ByVal resourceFile As String) As SQLStmts

        Dim cSQLStmts As New SQLStmts
        cSQLStmts.statementsFileFromResource = resourceFile
        cSQLStmts.loadStatements()
        Return cSQLStmts

    End Function

    Public Shared Function getStmtsFromFile(ByVal filePath As String) As SQLStmts

        Dim cSQLStmts As New SQLStmts
        cSQLStmts.statementsFileFromPath = filePath
        cSQLStmts.loadStatements()
        Return cSQLStmts

    End Function

    Private Sub New()

    End Sub

    Private Sub loadStatements()

        If _sqlStream Is Nothing = False Then
            _sqlStream.Position = 0 'rewind the stream!!
        End If

        domDs = New DataSet
        domDs.ReadXml(_sqlStream)

        If domDs.Tables(0).Columns(1).ColumnName = "mssql" Then
            'old schema, must change to new!
            domDs.Tables(0).Columns(1).ColumnName = DBUtils.enumSqlDialect.MSSQL.ToString
            domDs.Tables(0).Columns(2).ColumnName = DBUtils.enumSqlDialect.ORACLE.ToString
            domDs.Tables(0).Columns(3).ColumnName = DBUtils.enumSqlDialect.JET.ToString
        End If
        'If domDs.Tables(0).Columns(2).ColumnName = DBUtilsBase.enumSqlDialect.ORACLE.ToString Then
        '    domDs.Tables(0).Columns.Remove(domDs.Tables(0).Columns(2))
        'End If
        Dim keys(0) As DataColumn
        keys(0) = domDs.Tables(0).Columns(0)
        dv = domDs.Tables(0).DefaultView
        dv.Sort = domDs.Tables(0).Columns(0).ColumnName

        _sqlStream.Close()

    End Sub

    Public Function getStatement(ByVal key As String, ByVal dialect As DBUtils.enumSqlDialect, ByVal ParamArray params() As String) As String

        Dim i As Integer
        Dim stm As String

        stm = String.Empty

        i = Me.dv.Find(key)
        If i > -1 Then
            stm = NullChecker.strNull(Me.dv.Item(i).Item(dialect.ToString))
            If stm Is Nothing OrElse stm = String.Empty Then
                stm = NullChecker.strNull(Me.dv.Item(i).Item(DBUtils.enumSqlDialect.COMMON))
            End If

            If stm = String.Empty Then
                Throw New ApplicationException("Statement Key:" & key & " not found")
            End If

            If params Is Nothing OrElse params.Length = 0 Then
                'just return the stmmt
            Else
                stm = String.Format(stm, params)
            End If

            If stm.IndexOf(" ") = -1 Then 'single word, assume table name
                stm = "select * from " & stm
            End If
        End If
        Return stm

    End Function

    Public Sub setStatement(ByVal skey As String, _
                               ByVal stnt As String, _
                               Optional ByVal isqlDialect As DBUtils.enumSqlDialect = DBUtils.enumSqlDialect.COMMON)

        Dim i As Integer
        Dim stm As String

        stm = String.Empty

        i = Me.dv.Find(skey)
        If i > -1 Then
            Me.dv.Item(i).Item(isqlDialect.ToString) = stnt
        Else
            Dim dr As DataRow = Me.domDs.Tables(0).NewRow
            dr("key") = skey
            dr(isqlDialect.ToString) = stnt
            Me.domDs.Tables(0).Rows.Add(dr)
        End If

        Me.domDs.AcceptChanges()


    End Sub

    Public Function getXML() As String

        Me.domDs.DataSetName = "SQLStatements"
        Me.domDs.AcceptChanges()
        _sqlStream.Close()
        Return Me.domDs.GetXml()

    End Function

    Public Sub writeXML()

        Me.domDs.DataSetName = "SQLStatements"
        Me.domDs.AcceptChanges()
        _sqlStream.Close()
        Me.domDs.WriteXml(statementsFileFromPath, XmlWriteMode.IgnoreSchema)

    End Sub

    Public Sub writeXML(ByVal sPath As String)

        Me.domDs.DataSetName = "SQLStatements"
        Me.domDs.AcceptChanges()
        _sqlStream.Close()
        Me.domDs.WriteXml(sPath, XmlWriteMode.IgnoreSchema)

    End Sub

    Public Property sqlStream() As Stream
        Get
            Return _sqlStream
        End Get
        Set(ByVal Value As Stream)
            _sqlStream = Value
        End Set
    End Property

    Public Property statementsFileFromResource() As String
        Get
            Return _stmtsFile
        End Get
        Set(ByVal Value As String)
            _stmtsFile = Value
            _sqlStream = getResourceStream(_stmtsFile)
            If _sqlStream Is Nothing = True Then
                Throw New ApplicationException("Could not load SQL Statements from " & _stmtsFile & _
                                               ". Make sure that the file is marked as Embedded Resource")
            End If
        End Set

    End Property

    Public Property statementsFileFromPath() As String
        Get
            Return _stmtsFile
        End Get
        Set(ByVal Value As String)
            _stmtsFile = Value
            _sqlStream = File.Open(_stmtsFile, FileMode.Open)
        End Set

    End Property

    Private Shared Function getResourceStream(ByVal resname As String) As Stream

        Dim loadAsembly As [Assembly]
        Dim i As Integer

        For i = 0 To Thread.GetDomain.GetAssemblies.Length - 1

            If resname.ToUpper.StartsWith(Thread.GetDomain.GetAssemblies(i).GetName.Name.ToUpper & ".") Then
                loadAsembly = Thread.GetDomain.GetAssemblies(i)
                Exit For
            End If
        Next
        If loadAsembly Is Nothing Then
            Throw New ApplicationException("Could not load resource file " & resname)
        End If
        'resname = resname.Replace(loadAsembly.GetName.Name, "")
        Return loadAsembly.GetManifestResourceStream(resname)

    End Function

End Class
