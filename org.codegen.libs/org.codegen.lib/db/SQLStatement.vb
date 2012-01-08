Imports System.Collections

Public Class SQLStatement

    Private sql As String = Nothing
    Private conn As DBUtils = Nothing
    Private params As ArrayList = Nothing

    Public Sub New(ByVal c As DBUtils)

        Me.params = New ArrayList()
        Me.conn = c

    End Sub

    Public Sub New(ByVal c As DBUtils, ByVal sql As String, ByVal params As ArrayList)
        Me.New()
        Me.conn = c
        Me.setSql(sql)
        Me.params = params
    End Sub

    Public Sub New()
    End Sub

    Public Overridable Sub setSql(ByVal sql As String)
        Me.sql = sql
    End Sub

    Public Overridable Function getSql() As String
        Return sql
    End Function

    Public Overridable Sub setConn(ByVal c As DBUtils)
        Me.conn = c
    End Sub

    Public Overridable Function getConn() As DBUtils
        Return conn
    End Function

    Public Overridable Sub setParams(ByVal params As ArrayList)
        Me.params = params
    End Sub

    Public Overridable Function getParams() As ArrayList
        Return params
    End Function

    Public Overridable Function executeQuery() As IDataReader
        Return conn.getDataReaderWithParams(Me.sql, Me.params.ToArray())
    End Function

    Public Overridable Sub executeNonQuery()
        Call conn.executeSQLWithParams(Me.sql, Me.params.ToArray())
    End Sub

    Public Overridable Sub addParameter(ByVal val As Object)
        Me.params.Add(val)

    End Sub

End Class
