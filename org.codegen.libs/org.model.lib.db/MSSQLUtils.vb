Imports System.Data.SqlClient

Public Class MSSQLUtils
    Inherits DBUtils

    Public Overrides Function fillTypedDataSet(ByVal ds As DataSet, _
                                               ByVal tablename As String, _
                                               ByVal sql As String) As DataSet

        Dim adapter As SqlDataAdapter

        Try

            adapter = DirectCast(Me.getAdapter(sql), SqlDataAdapter)
            adapter.Fill(ds, tablename)

        Catch ex As Exception
            Throw New ApplicationException(ex.Message & vbCrLf & sql)
        Finally

        End Try

        Return ds

    End Function


    Public Overrides Function getAdapter(ByVal sql As String) As IDbDataAdapter

        Return New SqlDataAdapter(sql, DirectCast(Me.Connection, SqlClient.SqlConnection))

    End Function

    Public Overrides Function getDataTable(ByVal sql As String, Optional ByVal stablename As String = "table1") As DataTable

        Dim ds As New DataTable(stablename)
        Dim adapter As SqlDataAdapter

        Using conn As SqlConnection = DirectCast(Me.Connection, SqlClient.SqlConnection)

            Try

                adapter = New SqlDataAdapter(sql, conn)
                If Me.inTrans Then
                    adapter.SelectCommand.Transaction = DirectCast(Me.Transaction, SqlClient.SqlTransaction)
                End If

                adapter.Fill(ds)

            Catch ex As Exception
                Throw New ApplicationException(ex.Message & vbCrLf & sql)

            Finally


            End Try
        End Using

        Return ds

    End Function

    Protected Friend Overrides Sub setSpecialChars()

        MyBase.p_dbNow = "getDate()"
        MyBase.p_date_pattern = "'{0}'"
        MyBase.p_like_char = "%"

        MyBase.paramPrefix = "@"

    End Sub

    Protected Overrides ReadOnly Property ConnectionInternal() As IDbConnection
        Get
            Return New SqlConnection(p_connstring)
        End Get

    End Property

    Public Overrides Function getParameter() As IDataParameter

        Return New SqlParameter

    End Function

    Public Overrides Function getIdentitySQL() As String
        '"SELECT seq_util.currval from dual"

        Return "SELECT @@IDENTITY"

    End Function

    
End Class
