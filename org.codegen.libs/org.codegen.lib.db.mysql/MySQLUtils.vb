Imports MySql.Data.MySqlClient

Public Class MySQLDbUtils
    Inherits DBUtils

    Public Overrides Function fillTypedDataSet(ByVal ds As DataSet,
                                               ByVal tablename As String,
                                               ByVal sql As String) As DataSet

        Dim adapter As MySqlDataAdapter

        Try

            adapter = DirectCast(Me.getAdapter(sql), MySqlDataAdapter)
            adapter.Fill(ds, tablename)

        Catch ex As Exception
            Throw New ApplicationException(ex.Message & vbCrLf & sql)
        Finally
            Me.closeConnection()
        End Try

        Return ds

    End Function


    Public Overrides Function getAdapter(ByVal sql As String) As IDbDataAdapter

        Dim adapter As IDbDataAdapter
        Try
            adapter = New MySqlDataAdapter(sql, DirectCast(Me.Connection, MySqlConnection))
            Call logStatement(sql)
        Catch ex As Exception
            Throw

        Finally
            Me.closeConnection()

        End Try

        Return adapter

    End Function

    Public Overrides Function getDataTable(ByVal sql As String, Optional ByVal stablename As String = "table1") As DataTable

        Dim ds As New DataTable(stablename)
        Dim adapter As MySqlDataAdapter

        Try

            adapter = New MySqlDataAdapter(sql, DirectCast(Me.Connection, MySqlConnection))
            If Me.inTrans Then
                adapter.SelectCommand.Transaction = DirectCast(Me.Transaction, MySqlTransaction)
            End If

            adapter.Fill(ds)

        Catch ex As Exception
            Throw New ApplicationException(ex.Message & vbCrLf & sql)

        Finally

            Me.closeConnection()
        End Try

        Return ds

    End Function

    Protected Overrides Sub setSpecialChars()

        MyBase.p_dbNow = "CURDATE()"
        MyBase.p_date_pattern = "'{0}'"
        MyBase.p_like_char = "%"
        MyBase.p_string_quote_prefix = ""
        MyBase.p_paramPrefix = "@"

    End Sub

    Public Overrides Property Connection() As IDbConnection
        Get

            If p_Conn Is Nothing Then
                p_Conn = New MySqlConnection(p_connstring)
            End If

            If p_Conn.State <> ConnectionState.Open Then
                p_Conn.Open()
                Call logStatement("Open connection")
            Else
                Call logStatement("On open connection: already open")
            End If

            Return p_Conn


        End Get

        Set(ByVal Value As IDbConnection)
            p_Conn = Value
        End Set

    End Property

    Public Overrides Function getParameter() As IDataParameter

        Return New MySqlParameter

    End Function

    Public Overrides Function getIdentitySQL() As String
        '"SELECT seq_util.currval from dual"

        Return "SELECT @@IDENTITY"

    End Function

    Public Overrides Function quoteObjectName(ByVal objName As String) As String
        Return "`" & objName.Trim & "`"
    End Function

    Public Overrides Function GetDbObjectsDataTable() As DataTable
        Throw New NotImplementedException()
    End Function
End Class
