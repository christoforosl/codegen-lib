Option Strict On

Imports System.Reflection
Imports System.Configuration
Imports System.Xml
Imports System.Web
Imports System.IO
Imports System.Data.OleDb
Imports Microsoft.VisualBasic

Public Class OLEDBUtils
    Inherits DBUtils

    Public Overrides Function fillTypedDataSet(ByVal ds As DataSet, _
                                                  ByVal tablename As String, _
                                                  ByVal sql As String) As DataSet

        Try

            Using adapter As OleDbDataAdapter = DirectCast(Me.getAdapter(sql), OleDbDataAdapter)
                adapter.Fill(ds, tablename)
            End Using

        Catch ex As Exception
            Throw New ApplicationException(ex.Message & vbCrLf & sql)

        End Try

        Return ds

    End Function


    Public Overrides Function getAdapter(ByVal sql As String) As IDbDataAdapter

        Dim adapter As IDbDataAdapter
        Try
            adapter = New OleDbDataAdapter(sql, DirectCast(Me.Connection, OleDbConnection))
            'Trace.TraceInformation(sql)
        Catch ex As Exception
            Throw

        Finally


        End Try

        Return adapter

    End Function


    Protected Friend Overrides Sub setSpecialChars()
        If Me.sqldialect = enumSqlDialect.ORACLE Then
            MyBase.p_dbNow = "sysdate"
            MyBase.p_date_pattern = "'{0}'"
            MyBase.p_like_char = "%"

            MyBase.paramPrefix = ":"
        Else
            MyBase.p_dbNow = "getDate()"
            MyBase.p_date_pattern = "'{0}'"
            MyBase.p_like_char = "%"

            MyBase.paramPrefix = "@"
        End If
        

    End Sub

    
    Protected Overrides ReadOnly Property ConnectionInternal() As IDbConnection
        Get

            Return New OleDbConnection(p_connstring)

        End Get

    End Property

    Public Overrides Function getParameter() As IDataParameter

        Return New OleDbParameter

    End Function

    Public Overrides Function getIdentitySQL() As String
        '"SELECT seq_util.currval from dual"

        Return "SELECT @@IDENTITY"

    End Function

End Class