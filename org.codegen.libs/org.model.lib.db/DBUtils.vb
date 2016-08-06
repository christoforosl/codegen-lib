Option Strict On

Imports System.IO
Imports System.Runtime.CompilerServices
Imports System.Data.Linq

''' <summary>
''' Database Utility class to fascilitate sql statements execution
''' </summary>
''' <remarks></remarks>
Public MustInherit Class DBUtils
    Implements IDisposable

#Region "db provider"

    Private Shared _dbrovider As IDBUtilsProvider

    <ThreadStatic()> _
    Private Shared _current As DBUtils

    Public Shared Property dbProvider As IDBUtilsProvider
        Get
            If _dbrovider Is Nothing Then
                _dbrovider = New DBUtilsProviderFromConfig
            End If
            Return _dbrovider
        End Get
        Set(ByVal value As IDBUtilsProvider)

            _dbrovider = value
            _current = Nothing

        End Set
    End Property

    ''' <summary>
    ''' Constructs a DBUtilsBase class from the connection string stored in the configuration file.
    ''' <seealso cref="DBConfig"></seealso>
    ''' </summary>
    Public Shared Function Current() As DBUtils

        If _current Is Nothing Then
            _current = dbProvider.getDBUtils
        End If
        Return _current

    End Function


    ''' <summary>
    ''' Constructs a DBUtils class from the connection string passed.
    ''' </summary>
    ''' <param name="connString"></param>
    ''' <param name="sqlConnType"></param>
    ''' <param name="iDialect"></param>
    ''' <param name="logFile"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function getFromConnString(ByVal connString As String, _
                                             ByVal sqlConnType As enumConnType, _
                                             ByVal iDialect As enumSqlDialect, _
                                             Optional ByVal logFile As String = "") As DBUtils

        Dim ret As DBUtils

        Select Case sqlConnType

            Case enumConnType.CONN_MSSQL
                ret = New MSSQLUtils()
                ret.sqldialect = enumSqlDialect.MSSQL

            Case enumConnType.CONN_OLEDB
                ret = New OLEDBUtils
                ret.sqldialect = iDialect

            Case Else
                Throw New ArgumentException("Cannot Identify SQL Connection Type.  Enter 1 for SQL or 3 for OLE DB")
        End Select

        ret.ConnString = connString

        Return ret

    End Function

#End Region

#Region "enums"

    ''' <summary>
    ''' Connection types
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum enumConnType
        CONN_MSSQL = 1
        CONN_ORACLE = 2
        CONN_OLEDB = 3
    End Enum

    ''' <summary>
    ''' eSqlDialect enumeration, indicating the sql dialect that our database uses.
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum enumSqlDialect
        MSSQL = 1
        JET = 2
        ORACLE = 3
        MYSQL = 4
        COMMON = 5
    End Enum

#End Region

#Region "field Declarations"

    Protected Shared stmtCache As ConditionalWeakTable(Of String, String) = New ConditionalWeakTable(Of String, String)

    Protected p_connstring As String


    Protected p_date_pattern As String
    Protected p_dbNow As String
    Protected p_like_char As String
    Protected p_sqldialect As enumSqlDialect
    Protected p_params As IDataParameterCollection

    Public Property doLogging As Boolean = False

#End Region

#Region "Class constructors"

    Protected Sub New()

    End Sub

#End Region

#Region "properties"

    ''' <summary>
    ''' Parameter prefix
    ''' </summary>
    Protected Friend Property paramPrefix() As String

    Public Function dbContext() As DataContext

        Return New DataContext(Me.Connection)

    End Function

    ''' <summary>
    ''' SQL Dialect
    ''' <seealso cref="org.model.lib.db.DBUtils.enumSqlDialect"></seealso>
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property sqldialect() As enumSqlDialect
        Get
            Return p_sqldialect
        End Get

        Set(ByVal Value As enumSqlDialect)
            p_sqldialect = Value
            If p_sqldialect = enumSqlDialect.JET Then
            ElseIf p_sqldialect = enumSqlDialect.MSSQL Then
                Me.ConnType = enumConnType.CONN_MSSQL
            ElseIf p_sqldialect = enumSqlDialect.ORACLE Then
                Me.ConnType = enumConnType.CONN_ORACLE
            End If
            Me.setSpecialChars()

        End Set
    End Property


    Private Sub logMessage(ByVal msg As String, ParamArray args() As String)

        If doLogging Then
            If args.Length > 0 Then
                Trace.TraceInformation(String.Format(msg, args))
            Else
                Trace.TraceInformation(msg)
            End If

        End If

    End Sub

#End Region

#Region "Transactions"

    Public Property Transaction() As IDbTransaction

    ''' <summary>
    ''' the database connection attached to the transaction object
    ''' </summary>
    Private _connTransation As IDbConnection

    ''' <summary>
    ''' Commits the current transaction and closes the connection to the database.
    ''' before committing, the class checks if a transaction is active.
    ''' If not, statement is ignored.
    ''' </summary>
    ''' <remarks>Tested</remarks>
    Public Sub commitTrans()

        If Not Me.Transaction Is Nothing Then
            logMessage("Commit Transaction")
            Me.Transaction.Commit()
            Me.Transaction.Dispose()
            Me.Transaction = Nothing 'explicitely set it to nothing
        End If

        If (_connTransation IsNot Nothing AndAlso _connTransation.State = ConnectionState.Open) Then
            _connTransation.Close()
        End If

    End Sub

    ''' <summary>
    ''' Rollbacks the current transaction and closes the connection to the database.
    ''' Before Rollback, the class checks if a transaction is active.
    ''' If not, statement is ignored.
    ''' </summary>
    ''' <remarks>Tested</remarks>
    Public Sub rollbackTrans()

        If Not Me.Transaction Is Nothing Then

            logMessage("Rolling Back Transaction")

            Me.Transaction.Rollback()
            Me.Transaction.Dispose()
            Me.Transaction = Nothing 'explicitely set it to nothing

        End If
        If (_connTransation IsNot Nothing AndAlso _connTransation.State = ConnectionState.Open) Then
            _connTransation.Close()
        End If


    End Sub

    ''' <summary>
    ''' Returns true if a transaction is active (ie started by a beginTrans call).
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>Tested</remarks>
    Public ReadOnly Property inTrans() As Boolean
        Get
            Dim bl As Boolean = (Me.Transaction Is Nothing = False)
            If (bl) Then logMessage("In Transaction")
            Return bl
        End Get
    End Property

    ''' <summary>
    ''' Starts a transaction and 
    ''' Sets the flag true that transaction is active.
    ''' </summary>
    ''' <remarks>Tested</remarks>
    Public Sub beginTrans()

        If Not Me.Transaction Is Nothing Then
            Throw New ApplicationException("Nested Transactions not supported")
        End If

        If (doLogging) Then
            Dim t As System.Diagnostics.StackTrace = New System.Diagnostics.StackTrace()
            logMessage("BeginTransaction Transaction:" & t.ToString())
        End If

        _connTransation = Me.Connection
        _connTransation.Open()
        Me.Transaction = _connTransation.BeginTransaction

    End Sub
#End Region

#Region "Connection"

    ''' <summary>
    ''' Returns a connection to the database.  This does not open the connection
    ''' </summary>
    ''' <remarks>tested</remarks>
    Public ReadOnly Property Connection() As IDbConnection
        Get
            If (inTrans) Then
                Return Me.Transaction.Connection
            Else
                Return ConnectionInternal
            End If
        End Get

    End Property
    ''' <summary>
    ''' Connection String of database connection
    ''' </summary>
    ''' <remarks>tested</remarks>
    Public Property ConnString() As String
        Get
            Return p_connstring
        End Get

        Set(ByVal Value As String)
            p_connstring = Value
        End Set

    End Property


    ''' <summary>
    ''' Sets/Returns a concrete ADO.NET connection object
    ''' </summary>
    ''' <remarks></remarks>
    Protected MustOverride ReadOnly Property ConnectionInternal() As IDbConnection

    Protected Friend MustOverride Sub setSpecialChars()

    ''' <summary>
    ''' Gets/Sets the connection type
    ''' </summary>
    ''' <remarks></remarks>
    Public Property ConnType() As enumConnType

#End Region

#Region "Utility Functions"

    Public MustOverride Function getAdapter() As IDbDataAdapter
    Public MustOverride Function getCommand() As IDbCommand

    ''' <summary>
    ''' Fills a typed dataset from the sql provided
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <param name="tablename"></param>
    ''' <param name="sql"></param>
    ''' <returns>Dataset</returns>
    ''' <remarks>tested</remarks>
    Public Function fillTypedDataSet(ByVal ds As DataSet, ByVal tablename As String, ByVal sql As String) As DataSet
        Try

            Dim adapter As IDbDataAdapter = Me.getAdapter
            adapter.SelectCommand = Me.getCommand(sql)
            adapter.Fill(ds)
            ds.Tables(0).TableName = tablename

        Catch ex As Exception
            Throw New ApplicationException(ex.Message & vbCrLf & sql)

        End Try

        Return ds
    End Function

    Public Function getDataSet(ByVal sql As String) As DataSet

        Dim ds As New DataSet
        Dim adapter As IDbDataAdapter

        Try

            adapter = Me.getAdapter()
            adapter.SelectCommand = Me.getCommand(sql)
            If Me.inTrans Then
                logMessage("getDataSet In ongloing transaction, assigning p_trans variable")
                adapter.SelectCommand.Transaction = Me.Transaction
            End If

            adapter.Fill(ds)

        Catch ex As Exception
            Throw New ApplicationException(ex.Message & vbCrLf & sql)

        Finally


        End Try

        Return ds

    End Function

    Private Const QMARK As String = "?"

    ''' <summary>
    ''' Support for positional placeholders in sql server.
    ''' By default, the sql server driver supprorts only **named** parameters.  This function will replace 
    ''' and question marks (?) with sql server parameters (@1, @2, etc)
    ''' </summary>
    ''' <param name="sql">SQL to execute.  Symbols ? and {i} are supported</param>
    ''' <param name="params">Paraneters of sql</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overridable Function replaceParameterPlaceHolders(ByVal sql As String, ByVal ParamArray params() As Object) As String

        If Me.sqldialect <> DBUtils.enumSqlDialect.MSSQL Then
            Return sql
        End If

        Dim ret As String = String.Empty

        stmtCache.TryGetValue(sql, ret)
        If ret IsNot Nothing Then
            Return ret
        End If

        If sql.IndexOf(QMARK) >= 0 Then

            Dim c As String() = sql.Split(CChar(QMARK))
            For i As Integer = 0 To c.Length - 1

                ret &= c(i)

                If i < c.Length - 1 Then
                    ret &= Me.paramPrefix & i
                End If
            Next


        Else
            ret = sql
            For i As Integer = 0 To params.Length - 1
                ret = ret.Replace("{" & i & "}", Me.paramPrefix & i)
            Next

        End If

        stmtCache.Add(sql, ret)
        Return ret

    End Function

    Public Function getDataSetWithParams(ByVal sql As String, ByVal ParamArray params() As IDataParameter) As DataSet
        Dim ds As New DataSet

        Try

            Dim adapter As IDbDataAdapter = Me.getAdapter()
            adapter.SelectCommand = Me.getCommand(sql)

            If Me.inTrans Then
                adapter.SelectCommand.Transaction = Me.Transaction
            End If

            For i As Integer = 0 To params.Length - 1
                adapter.SelectCommand.Parameters.Add(params(i))
            Next
            adapter.Fill(ds)
            logMessage("getDataSetWithParams:" & sql)

        Catch ex As Exception
            Throw New ApplicationException(ex.Message & vbCrLf & sql)

        Finally


        End Try

        Return ds
    End Function

    Public Function getDataSetWithParams(ByVal sql As String, ByVal ParamArray params() As Object) As DataSet

        Dim ds As New DataSet
        Dim adapter As IDbDataAdapter

        Try

            sql = replaceParameterPlaceHolders(sql, params.Length - 1)

            adapter = Me.getAdapter()
            adapter.SelectCommand = Me.getCommand(sql)
            If Me.inTrans Then
                adapter.SelectCommand.Transaction = Me.Transaction

            End If

            For i As Integer = 0 To params.Length - 1
                Dim iParam As System.Data.IDataParameter
                iParam = Me.getParameter(Me.paramPrefix & i)
                iParam.Value = params(i)
                adapter.SelectCommand.Parameters.Add(iParam)
            Next
            adapter.Fill(ds)
            logMessage("getDataSetWithParams:" & sql)

        Catch ex As Exception
            Throw New ApplicationException(ex.Message & vbCrLf & sql)

        Finally


        End Try

        Return ds
    End Function



    ''' <summary>
    ''' Closes a DataReader and the connection to the database.  Typically used when opening a datareader and 
    ''' processing results.
    ''' <code source="C:\vs2008Projects\org.codegen.lib.vb\org.codegen.lib.db\examples.vb" region="DataReader and NullChecker Examples" lang="vbnet" title="DataReader Example" />
    ''' </summary>
    ''' <param name="dr">DataReader object to close.</param>
    Public Sub closeDataReader(ByVal dr As IDataReader)

        If dr Is Nothing Then Exit Sub

        If dr.IsClosed = False Then
            dr.Close()
        End If


        dr = Nothing

    End Sub

    ''' <summary>
    ''' Executes the sql passed and returns a DataTable filled with the results of the sql.
    ''' <code source="C:\vs2008Projects\org.codegen.lib.vb\org.codegen.lib.db\examples.vb" region="DataTableExample" lang="vbnet" title="getDataTable Example" />
    ''' </summary>
    ''' <param name="sql">SQL Statement to be executed</param>
    ''' <param name="stablename">Table name of returned datatable, default "table1"</param>
    ''' <returns>a DataTable filled with the results of the sql</returns>
    Public Overridable Function getDataTable(ByVal sql As String, Optional ByVal stablename As String = "table1") As DataTable

        Dim ds As DataSet = Me.getDataSet(sql)
        If ds.Tables.Count > 0 Then
            ds.Tables(0).TableName = stablename
            Return ds.Tables(0)
        Else
            Return Nothing
        End If

    End Function

    ''' <summary>
    ''' Behaves just like DBUtilsBase.getDataTable
    ''' but it allows for a parametrized SQL statement
    ''' </summary>
    ''' <param name="sql">SQL Statement to be executed</param>
    ''' <param name="params">ParamArray of parameter values</param>
    ''' <returns>a DataTable filled with the results of the sql</returns>
    Public Function getDataTableWithParams(ByVal sql As String, ByVal ParamArray params() As Object) As DataTable

        Dim ds As DataSet = Me.getDataSetWithParams(sql, params)
        If ds.Tables.Count > 0 Then
            Return ds.Tables(0)
        Else
            Return Nothing
        End If

    End Function

    ''' <summary>
    ''' Behaves just like <see cref="DBUtils.getSValue">DBUtilsBase.getSValue</see> but it returns a Double value
    ''' </summary>
    ''' <param name="sql">SQL statement to execute</param>
    ''' <returns>String value returned by first row, first column.  If no rows are returned, returns 0. 
    ''' If value of first column is DBNULL, returns 0</returns>
    ''' <remarks></remarks>
    ''' <seealso cref="DBUtils.getSValue"></seealso>
    Function getDblValue(ByVal sql As String) As Double

        Dim rs As IDataReader
        Dim ret As Double

        Try
            rs = Me.getDataReader(sql)
            If rs.Read Then
                If IsDBNull(rs.GetValue(0)) OrElse Not IsNumeric(rs.GetValue(0)) Then

                Else
                    ret = CDbl(rs.GetValue(0))
                End If

            End If

        Finally
            Call Me.closeDataReader(rs)
        End Try

        Return ret

    End Function

    ''' <summary>
    ''' Behaves just like <see cref="DBUtils.getSValue">DBUtilsBase.getSValue</see> but it returns an Integer value
    ''' </summary>
    ''' <param name="sql">SQL statement to execute</param>
    ''' <returns>String value returned by first row, first column.  If no rows are returned, returns 0. 
    ''' If value of first column is DBNULL, returns 0</returns>
    ''' <remarks></remarks>
    ''' <seealso cref="DBUtils.getSValue"></seealso>
    Function getIntValue(ByVal sql As String) As Int32

        Return CInt(getLngValue(sql))

    End Function

    ''' <summary>
    ''' Behaves just like <see cref="DBUtils.getSValue">DBUtilsBase.getSValue</see> but it returns a Long value
    ''' </summary>
    ''' <param name="sql">SQL statement to execute</param>
    ''' <returns>String value returned by first row, first column.  If no rows are returned, returns 0. 
    ''' If value of first column is DBNULL, returns 0</returns>
    ''' <remarks></remarks>
    ''' <seealso cref="DBUtils.getSValue"></seealso>
    Function getLngValue(ByVal sql As String, ByVal ParamArray params() As Object) As Int32
        Dim rs As IDataReader
        Try
            rs = Me.getDataReaderWithParams(sql, params)
            If rs.Read Then
                If IsDBNull(rs.GetValue(0)) Then
                    getLngValue = 0
                Else
                    If (Not IsNumeric(rs.GetValue(0))) Then
                        Throw New ApplicationException("Non Numeric value returned in call to getLngValue ")
                    End If

                    getLngValue = CInt(rs.GetValue(0))

                End If

            End If

        Finally
            Call Me.closeDataReader(rs)
        End Try

    End Function

    Public Function getObjectValue(ByVal sql As String, ByVal ParamArray params() As Object) As Object
        Dim rs As IDataReader
        Try
            rs = Me.getDataReaderWithParams(sql, params)
            If rs.Read Then
                If Not IsDBNull(rs.GetValue(0)) Then
                    Return rs.GetValue(0)
                End If

            End If

        Finally
            Call Me.closeDataReader(rs)
        End Try


        Return Nothing
    End Function

    ''' <summary>
    ''' Behaves just like <see cref="DBUtils.getSValue">DBUtilsBase.getSValue</see> but it allows for a parametarized SQL, and it returns a Long value
    ''' <code source="C:\vs2008Projects\org.codegen.lib.vb\org.codegen.lib.db\examples.vb" region="getSValueWithParams" lang="vbnet" title="getSValueWithParams Example" />
    ''' </summary>
    ''' <param name="sql">SQL statement to execute</param>
    ''' <returns>String value returned by first row, first column.  If no rows are returned, returns 0. 
    ''' If value of first column is DBNULL, returns 0</returns>
    ''' <remarks></remarks>
    ''' <seealso cref="DBUtils.getSValueWithParams"></seealso>
    Function getLngValueWithParams(ByVal sql As String, ByVal ParamArray params() As Object) As Int32

        Dim rs As IDataReader

        Try
            getLngValueWithParams = 0

            rs = Me.getDataReaderWithParams(sql, params)
            If rs.Read Then
                If IsDBNull(rs.GetValue(0)) OrElse Not IsNumeric(rs.GetValue(0)) Then
                    getLngValueWithParams = 0
                Else
                    getLngValueWithParams = CInt(rs.GetValue(0))
                End If
            End If

        Finally
            Call Me.closeDataReader(rs)
        End Try

    End Function

    Public ReadOnly Property dbnow() As String
        Get
            Return p_dbNow
        End Get

    End Property

    ''' <summary>
    ''' Behaves just like <see cref="DBUtils.getSValue">DBUtilsBase.getSValue</see> but it allows for a parametarized SQL
    ''' <code source="C:\vs2008Projects\org.codegen.lib.vb\org.codegen.lib.db\examples.vb" region="getSValueWithParams" lang="vbnet" title="getSValueWithParams Example" />
    ''' </summary>
    ''' <param name="sql">SQL statement to execute</param>
    ''' <returns>String value returned by first row, first column.  If no rows are returned, returns String.Empty. 
    ''' If value of first column is DBNULL, returns String.Empty</returns>
    ''' <remarks></remarks>
    ''' <seealso cref="DBUtils.getSValue"></seealso>
    Function getSValueWithParams(ByVal sql As String, _
                                 ByVal ParamArray params() As Object) As String

        Dim rs As IDataReader

        Try
            rs = Me.getDataReaderWithParams(sql, params)
            If rs.Read() Then
                'rs.MoveFirst
                Return NullChecker.strNull(rs(0))
            End If
            Return String.Empty
        Finally
            Call Me.closeDataReader(rs)
        End Try

    End Function

    ''' <summary>
    ''' This function opens a datareader, reads the first row and returns the value of the first field 
    ''' as a <b>date</b>.
    ''' <code source="C:\vs2008Projects\org.codegen.lib.vb\org.codegen.lib.db\examples.vb" region="DtValueExample" lang="vbnet" title="DtValue Example" />
    ''' </summary>
    ''' <param name="sql">SQL statement to execute</param>
    ''' <returns>String value returned by first row, first column.  If no rows are returned, returns String.Empty. 
    ''' If value of first column is DBNULL, returns String.Empty</returns>
    ''' <remarks></remarks>
    Function getDtValue(ByVal sql As String) As Date

        Dim rs As IDataReader

        Try
            rs = Me.getDataReader(sql)
            If rs.Read() Then
                'rs.MoveFirst
                getDtValue = NullChecker.dateNull(rs(0))
            End If

        Finally
            Call Me.closeDataReader(rs)
        End Try

    End Function

    ''' <summary>
    ''' This function opens a datareader, reads the first row and returns the value of the first field 
    ''' as a string.
    ''' <code source="C:\vs2008Projects\org.codegen.lib.vb\org.codegen.lib.db\examples.vb" region="SValueExample" lang="vbnet" title="SValue Example" />
    ''' </summary>
    ''' <param name="sql">SQL statement to execute</param>
    ''' <returns>String value returned by first row, first column.  If no rows are returned, returns String.Empty. 
    ''' If value of first column is DBNULL, returns String.Empty</returns>
    ''' <remarks></remarks>
    Function getSValue(ByVal sql As String) As String

        Dim rs As IDataReader
        Dim ret As String = String.Empty
        Try
            rs = Me.getDataReader(sql)
            If rs.Read() Then
                'rs.MoveFirst
                ret = NullChecker.strNull(rs(0))
            End If

        Finally
            Call Me.closeDataReader(rs)
        End Try
        Return ret

    End Function

#End Region

#Region "Parameters"

    Public Function getCommand(Optional ByVal sql As String = "") As IDbCommand

        Dim icomm As IDbCommand = Me.Connection.CreateCommand()
        If sql <> String.Empty Then
            icomm.CommandText = sql
        End If
        icomm.Transaction = Me.Transaction
        icomm.CommandType = CommandType.Text
        Return icomm

    End Function

    Public MustOverride Function getParameter() As IDataParameter


    Public Function getParameter(ByVal _paramName As String, ByVal val As Byte()) As IDataParameter
        Dim tmp As IDataParameter = Me.getParameter
        tmp.ParameterName = _paramName
        tmp.DbType = DbType.Binary

        If val Is Nothing Then
            tmp.Value = DBNull.Value
        Else
            tmp.Value = val
        End If

        Return tmp

    End Function

    Public Function getParameter(ByVal _paramName As String, ByVal val As DateTime?) As IDataParameter
        Dim tmp As IDataParameter = Me.getParameter
        tmp.ParameterName = _paramName
        tmp.DbType = DbType.DateTime

        If val.HasValue = False Then
            tmp.Value = DBNull.Value
        Else
            Dim org As DateTime = val.Value
            Dim truncatedDateTime As DateTime = New DateTime(org.Year, org.Month, org.Day, org.Hour, org.Minute, org.Second)
            'Debug.WriteLine("Saving datetime:" & truncatedDateTime)
            tmp.Value = truncatedDateTime
        End If

        Return tmp

    End Function

    Public Function getParameter(ByVal _paramName As String, ByVal val As Int16?) As IDataParameter
        Dim tmp As IDataParameter = Me.getParameter
        tmp.ParameterName = _paramName
        tmp.DbType = DbType.Int16

        If val.HasValue = False Then
            tmp.Value = DBNull.Value
        Else
            tmp.Value = val
        End If

        Return tmp
    End Function

    Public Function getParameter(ByVal _paramName As String, ByVal val As Int64?) As IDataParameter
        Dim tmp As IDataParameter = Me.getParameter
        tmp.ParameterName = _paramName
        tmp.DbType = DbType.Int64

        If val.HasValue = False Then
            tmp.Value = DBNull.Value
        Else
            tmp.Value = val
        End If

        Return tmp
    End Function

    Public Function getParameter(ByVal _paramName As String, ByVal val As Boolean?) As IDataParameter
        Dim tmp As IDataParameter = Me.getParameter
        tmp.ParameterName = _paramName
        tmp.DbType = DbType.Int32

        If val.HasValue = False Then
            tmp.Value = 0
        Else
            tmp.Value = val
        End If

        Return tmp
    End Function

    Public Function getParameter(ByVal _paramName As String, ByVal val As Single?) As IDataParameter
        Dim tmp As IDataParameter = Me.getParameter
        tmp.ParameterName = _paramName
        tmp.DbType = DbType.Single

        If val.HasValue = False Then
            tmp.Value = DBNull.Value
        Else
            tmp.Value = val
        End If

        Return tmp
    End Function

    Public Function getParameter(ByVal _paramName As String, ByVal val As Double?) As IDataParameter
        Dim tmp As IDataParameter = Me.getParameter
        tmp.ParameterName = _paramName
        tmp.DbType = DbType.Double

        If val.HasValue = False Then
            tmp.Value = DBNull.Value
        Else
            tmp.Value = val
        End If

        Return tmp
    End Function

    Public Function getParameter(ByVal _paramName As String, ByVal val As Decimal?) As IDataParameter
        Dim tmp As IDataParameter = Me.getParameter
        tmp.ParameterName = _paramName
        tmp.DbType = DbType.Decimal

        If val.HasValue = False Then
            tmp.Value = DBNull.Value
        Else
            tmp.Value = val
        End If

        Return tmp
    End Function

    Public Function getParameter(ByVal _paramName As String, ByVal val As Integer?) As IDataParameter
        Dim tmp As IDataParameter = Me.getParameter
        tmp.ParameterName = _paramName
        tmp.DbType = DbType.Int32

        If val.HasValue = False Then
            tmp.Value = DBNull.Value
        Else
            tmp.Value = val
        End If

        Return tmp
    End Function

    Public Function getParameter(ByVal _paramName As String, ByVal val As String) As IDataParameter
        Dim tmp As IDataParameter = Me.getParameter
        tmp.ParameterName = _paramName
        tmp.DbType = DbType.String

        If String.IsNullOrEmpty(val) Then
            tmp.Value = DBNull.Value
        Else
            tmp.Value = val
        End If

        Return tmp
    End Function


    ''' <summary>
    ''' Returns a GUID parameter
    ''' </summary>
    ''' <param name="_paramName"></param>
    ''' <param name="val"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getParameter(ByVal _paramName As String, ByVal val As Guid?) As IDataParameter

        Dim tmp As IDataParameter = Me.getParameter
        tmp.ParameterName = _paramName
        tmp.DbType = DbType.Guid

        If val Is Nothing Then
            tmp.Value = DBNull.Value
        Else
            tmp.Value = val
        End If

        Return tmp
    End Function


    Public Function getParameter(ByVal _paramName As String) As IDataParameter
        Dim tmp As IDataParameter = Me.getParameter
        tmp.ParameterName = _paramName
        Return tmp
    End Function

    Public Function getParameterInOut(ByVal _paramName As String) As IDataParameter

        Dim tmp As IDataParameter = Me.getParameter(_paramName)
        tmp.Direction = ParameterDirection.InputOutput
        Return tmp

    End Function

    Public Function getOutputParameterValue(ByVal _paramName As String) As Object

        If p_params Is Nothing Then Return Nothing

        Dim tmp As IDataParameter = CType(p_params.Item(_paramName), IDataParameter)
        If tmp Is Nothing Then Return Nothing

        Return tmp.Value

    End Function

#End Region

#Region "SQL"

    ''' <summary>
    ''' <code source="C:\vs2008Projects\org.codegen.lib.vb\org.codegen.common.tests\DBUtilsTest.vb" region="Parameters Example, with output parameter" lang="vbnet" title="Parameters Example, with output parameter" />
    ''' </summary>
    ''' <param name="sql">Paratarized sql statement</param>
    ''' <param name="params">Parameters collection</param>
    ''' <remarks></remarks>
    ''' 
    Public Function executeSQLWithParams(ByVal sql As String, ByVal params As List(Of IDataParameter)) As Integer

        Dim dbconn As IDbConnection = Me.Connection
        Try

            If (dbconn.State <> ConnectionState.Open) Then dbconn.Open()

            Using icomm As IDbCommand = dbconn.CreateCommand()

                icomm.CommandText = replaceParameterPlaceHolders(sql, params)
                If Me.inTrans Then
                    icomm.Transaction = Me.Transaction
                End If
                icomm.CommandType = CommandType.Text

                For i As Integer = 0 To params.Count - 1
                    Dim iParam As System.Data.IDataParameter = params(i)
                    icomm.Parameters.Add(iParam)
                Next

                Return icomm.ExecuteNonQuery()

            End Using

            logMessage("getDataSetWithParams:" & sql)

        Finally
            If Not Me.inTrans Then
                dbconn.Close()
            End If
        End Try

    End Function

    ''' <summary>
    ''' Executes an sql statement of type UPDATE, INSERT or DELETE, allows for paramerized sql
    ''' Each parameter in the params Object array is converted into System.Data.IDataParameter
    ''' </summary>
    ''' <param name="sql">sql statement</param>
    ''' <param name="params">object array of parameter values</param>
    ''' <remarks></remarks>
    Public Function executeSQLWithParams(ByVal sql As String, ByVal ParamArray params() As Object) As Integer

        Dim dbconn As IDbConnection = Me.Connection
        Try




            If (dbconn.State <> ConnectionState.Open) Then dbconn.Open()

            Using icomm As IDbCommand = dbconn.CreateCommand()

                icomm.CommandText = replaceParameterPlaceHolders(sql, params)
                If Me.inTrans Then
                    icomm.Transaction = Me.Transaction
                End If
                icomm.CommandType = CommandType.Text
                Me.setParamValues(icomm, params)
                Return icomm.ExecuteNonQuery()

            End Using
        Finally
            If Not Me.inTrans Then
                dbconn.Close()
            End If
        End Try

    End Function

    Private Sub setParamValues(ByVal icomm As IDbCommand, ByVal ParamArray params() As Object)

        If params Is Nothing Then Exit Sub

        For i As Integer = 0 To params.Length - 1
            Dim iParam As System.Data.IDataParameter
            If TypeOf (params(i)) Is Date AndAlso CDate(params(i)) = NullChecker.NULL_DATE Then
                iParam = Me.getParameter(String.Concat(Me.paramPrefix, i))
            Else
                iParam = Me.getParameter(String.Concat(Me.paramPrefix, i))
                iParam.Value = params(i)
            End If

            icomm.Parameters.Add(iParam)
        Next

    End Sub

    Public Sub executeSQLWithParamsIdentity(ByVal sql As String, ByVal ParamArray params() As Object)

        Const STR_LEFT As String = "{"
        Const STR_RIGHT As String = "}"

        For i As Integer = 0 To params.Length - 1
            sql = sql.Replace(String.Concat(STR_LEFT, i, STR_RIGHT), String.Concat(Me.paramPrefix, i))
        Next

        Dim icomm As IDbCommand = Me.Connection.CreateCommand()
        icomm.CommandText = sql
        icomm.Transaction = Me.Transaction
        icomm.CommandType = CommandType.Text

        Me.setParamValues(icomm, params)

        Try
            icomm.ExecuteNonQuery()
        Finally
            icomm.Dispose()
        End Try
        logMessage("getDataSetWithParams:" & sql)

    End Sub

    Public Function getLastIdentity() As Integer
        Dim ireader As IDataReader
        Dim icomm As IDbCommand = Me.getCommand

        Try
            icomm.CommandText = Me.getIdentitySQL
            icomm.CommandType = CommandType.Text
            ireader = icomm.ExecuteReader()
            If ireader.Read() Then
                Return CInt(ireader.GetValue(0))
            End If

        Finally
            Call Me.closeDataReader(ireader)
        End Try

    End Function


    Public MustOverride Function getIdentitySQL() As String

    ''' <summary>
    ''' Executes a stored procedure with parameters. How to use:
    ''' </summary>
    ''' <param name="spSql">Pass only the name of the stored procedure</param>
    ''' <param name="spParams">Pass a list of parameters, with the same name as the parameters 
    ''' of the stored procedure</param>
    ''' <remarks></remarks>
    Public Sub executeSP(ByVal spSql As String, ByVal spParams As System.Collections.Generic.List(Of IDataParameter))

        Dim icomm As IDbCommand


        Using conn As IDbConnection = Me.Connection
            Try
                icomm = conn.CreateCommand()
                icomm.Transaction = Me.Transaction
                icomm.CommandText = spSql
                icomm.CommandType = CommandType.StoredProcedure

                For Each param As IDataParameter In spParams
                    icomm.Parameters.Add(param)
                Next
                icomm.ExecuteNonQuery()

                Me.p_params = Nothing
                Me.p_params = icomm.Parameters
                logMessage("Execute:" & spSql)

            Catch e As Exception
                Throw New ApplicationException(e.Message & vbCrLf & _
                                               "Error SQL:" & getStmtAndParamsAsString(icomm))

            Finally

                icomm.Dispose()

            End Try
        End Using


    End Sub

    ''' <summary>
    ''' Executes an sql statement of type UPDATE, INSERT or DELETE, no parameters
    ''' </summary>
    Public Function executeSQL(ByVal sql As String) As Integer
        Return Me.executeSQLWithParams(sql)
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sql"></param>
    Public Function getDataReader(ByVal sql As String, ByVal spParams As List(Of IDataParameter)) As IDataReader

        Dim rsado As IDataReader

        Dim conn As IDbConnection = Me.Connection


        Using icomm As IDbCommand = conn.CreateCommand()
            Try
                If (conn.State <> ConnectionState.Open) Then conn.Open()
                icomm.CommandText = sql
                icomm.CommandType = CommandType.Text
                For Each param As IDataParameter In spParams
                    icomm.Parameters.Add(param)
                Next
                If Me.inTrans Then
                    icomm.Transaction = Me.Transaction
                    rsado = icomm.ExecuteReader(CommandBehavior.Default)
                Else
                    rsado = icomm.ExecuteReader(CommandBehavior.CloseConnection)
                End If

                logMessage("getDataReader:" & sql)

            Catch e As Exception
                Throw New ApplicationException(e.Message & vbCrLf & _
                                               "Error SQL:" & getStmtAndParamsAsString(icomm))


            End Try
        End Using


        Return rsado

    End Function
    ''' <summary>
    ''' <seealso cref="DBUtils.getDataReader"></seealso>
    ''' <code source="C:\vs2008Projects\org.codegen.lib.vb\org.codegen.lib.db\examples.vb" region="DataReader with parameters and NullChecker Examples" lang="vbnet" title="DataReader With Parameters Example" />
    ''' </summary>
    ''' <param name="sql"></param>
    ''' <param name="params"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getDataReaderWithParams(ByVal sql As String, ByVal ParamArray params() As Object) As IDataReader

        Dim rs As IDataReader

        Dim conn As IDbConnection = Me.Connection

        If conn.State <> ConnectionState.Open Then
            conn.Open()
        End If

        Using icomm As IDbCommand = conn.CreateCommand()
            If params IsNot Nothing Then
                sql = replaceParameterPlaceHolders(sql, params)
            End If

            icomm.CommandText = sql
            icomm.CommandType = CommandType.Text

            setParamValues(icomm, params)
            Try
                If Me.inTrans Then
                    icomm.Transaction = Me.Transaction
                    rs = icomm.ExecuteReader(CommandBehavior.Default)
                Else
                    rs = icomm.ExecuteReader(CommandBehavior.CloseConnection)
                End If

            Catch e As Exception
                Throw New ApplicationException(e.Message & vbCrLf & _
                                               "Error SQL:" & getStmtAndParamsAsString(icomm))
            Finally
                'NOTE: do not close database connection! see icomm.ExecuteReader(CommandBehavior) above!
                'If Not Me.inTrans Then
                '    conn.Close()
                'End If
            End Try

        End Using

        logMessage("getDataReaderWithParams:" & sql)
        Return rs

    End Function


    ''' <summary>
    ''' Opens a datareader for SQL results processing
    ''' <code source="C:\vs2008Projects\org.codegen.lib.vb\org.codegen.lib.db\examples.vb" region="DataReader and NullChecker Examples" lang="vbnet" title="DataReader Example" />
    ''' </summary>
    ''' <param name="sql"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getDataReader(ByVal sql As String) As IDataReader

        Return getDataReaderWithParams(sql, Nothing)

    End Function

    ''' <summary>
    ''' Used to built a string 
    ''' </summary>
    ''' <param name="pstmt"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function getStmtAndParamsAsString(ByVal pstmt As System.Data.IDbCommand) As String

        Dim err As String = ""

        If pstmt IsNot Nothing Then
            err = "SQL: " & pstmt.CommandText & vbCrLf
            err &= "Parameters: " & vbCrLf
            For Each iparam As IDataParameter In pstmt.Parameters
                err &= "Name  :" & iparam.ParameterName & vbCrLf
                Try
                    err &= "DBType:" & iparam.DbType.ToString & vbCrLf

                Catch ex As Exception
                    err &= "DBType:UNKNOWN" & vbCrLf
                End Try

                If IsDBNull(iparam.Value) Then
                    err &= "Value : NULL" & vbCrLf
                Else
                    Try
                        err &= "Value :" & CStr(iparam.Value) & vbCrLf
                    Catch ex As Exception
                        err &= "Value : Could not convert to String" & vbCrLf
                    End Try

                End If


                err &= "------------" & vbCrLf
            Next

        End If
        Return err
    End Function


#End Region

#Region "Schema"

    Public Overridable Function GetDbObjectsDataTable() As DataTable

        Dim dt As DataTable
        Dim sql As String = String.Empty

        If Me.sqldialect = DBUtils.enumSqlDialect.MSSQL Then
            sql = "select '0' as id,' -- none -- ' as objName from sysobjects union SELECT [name] as id, [name] AS OBJNAME FROM sysobjects O WHERE (xtype = 'V') OR (xtype = 'U') ORDER BY 2"

        ElseIf Me.sqldialect = DBUtils.enumSqlDialect.ORACLE Then
            sql = "select '0' as id,' -- none -- ' as ""objName"" from dual union SELECT table_name as id, table_name AS ""objName"" FROM user_tables ORDER BY 2"

        Else
            Throw New NotSupportedException()

        End If
        dt = Me.getDataTable(sql)
        Return dt

    End Function

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                logMessage("DBUtils Disposing...")
                Me.rollbackTrans()
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region



End Class