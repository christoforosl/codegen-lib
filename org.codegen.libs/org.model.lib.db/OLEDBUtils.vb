Option Strict On

Imports System.Data.OleDb

Public Class OLEDBUtils
    Inherits DBUtils


    Public Overrides Function getAdapter() As IDbDataAdapter

        Return New OleDbDataAdapter()

    End Function


    Protected Friend Overrides Sub setSpecialChars()

        If Me.sqldialect = enumSqlDialect.ORACLE Then
            MyBase.p_dbNow = "sysdate"
            MyBase.p_datePattern = "'{0}'"
            MyBase.p_likeChar = "%"
            MyBase.p_leftQuoteChar = """"
            MyBase.p_rightQuoteChar = """"
            MyBase.paramPrefix = ":"
        Else
            MyBase.p_dbNow = "getDate()"
            MyBase.p_datePattern = "'{0}'"
            MyBase.p_likeChar = "%"
            MyBase.p_leftQuoteChar = "["
            MyBase.p_rightQuoteChar = "]"
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

    Public Overloads Overrides Function getCommand() As IDbCommand
        Return New OleDbCommand
    End Function
End Class