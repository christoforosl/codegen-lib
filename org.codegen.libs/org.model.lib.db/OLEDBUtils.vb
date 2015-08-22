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


    Public Overrides Function getAdapter() As IDbDataAdapter

        Return New OleDbDataAdapter()

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

    Public Overloads Overrides Function getCommand() As IDbCommand
        Return New OleDbCommand
    End Function
End Class