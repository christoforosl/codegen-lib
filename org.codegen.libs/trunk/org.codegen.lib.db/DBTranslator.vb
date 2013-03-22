Imports System.Globalization
Imports System.Security.Permissions
Imports System.Threading
Imports System.Collections.Generic
Imports org.codegen.common.Translation

Imports System.Configuration.ConfigurationManager

''' <summary>
''' Class to fascilitate translation services from a database table
''' The table structure must be
''' <code>
''' CREATE TABLE [dbo].sysLanguageStrings(
'''	[langKey] [varchar](70) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
'''	[langValueEN] [nvarchar(500)] COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
'''	[langValueEL] [nvarchar(500)] COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
''' CONSTRAINT [PK_sysLanguageStrings] PRIMARY KEY ([langKey])
''') 
'''
''' </code>
''' </summary>
''' <remarks></remarks>
Public Class DBTranslator
    Inherits ILanguageStrings

    Private Const STR_INSERT As String = "INSERT INTO sysLanguageStrings (langKey,langValueEL,langValueEN) VALUES ({0},{1},{2})"
    Private Const STR_UPDATE As String = "UPDATE sysLanguageStrings SET langValueEL={0},langValueEN={1} WHERE langKey={2}"
    Private Const STR_SELECT As String = "SELECT langKey from sysLanguageStrings where langKey={0}"
    Private Const STR_DELETE As String = "DELETE FROM sysLanguageStrings WHERE Key={0} "

    Private _insertDefaultTranslatorKey As Boolean

    Public Overrides Function getStringDB(ByVal key As String, Optional ByVal inLang As String = "") As String

        Dim skey As String
        Dim slang As String
        Dim tmp As String
        Dim dbConn As DBUtils = DBUtils.Current

        skey = key.Trim
        slang = inLang.Trim
        If slang = String.Empty Then slang = ILanguageStrings.LANG_ENGLISH

        If _insertDefaultTranslatorKey Then
            Me.insertInitialLangValue(skey)
        End If

        Dim sql As String = "select langValue" & slang & " from sysLanguageStrings where " & dbConn.dbUpper("langKey") & "={0}"
        tmp = dbConn.getSValueWithParams(sql, UCase(skey))

        If tmp = String.Empty Then
            'is null, take english as the default
            If slang <> ILanguageStrings.LANG_ENGLISH Then
                tmp = dbConn.getSValueWithParams(sql, UCase(skey), slang)
            End If
            getStringDB = tmp
        Else
            getStringDB = tmp
        End If

    End Function

    Public Overrides Sub deleteString(ByVal key As String)

        Call DBUtils.Current.executeSQLWithParams(STR_DELETE, key)

    End Sub

    ''' <summary>
    ''' Inserts the initial value in the database
    ''' </summary>
    ''' <param name="skey"></param>
    ''' <remarks></remarks>
    Protected Sub insertInitialLangValue(ByVal skey As String)

        Me.insertOrUpdate(skey, skey, skey)

    End Sub
    Protected Function hasLanguageKey(ByVal skey As String) As Boolean

        Return DBUtils.Current.getSValueWithParams(STR_SELECT, skey) <> String.Empty

    End Function

    Public Overrides Sub insertOrUpdate(ByVal skey As String, _
                                      ByVal valueEN As String, _
                                      ByVal valueEL As String)

        Dim dbConn As DBUtils = DBUtils.Current
        If Not hasLanguageKey(skey) Then
            dbConn.executeSQLWithParams(STR_INSERT, skey.ToUpper, valueEL, valueEN)
        Else
            'string already exists. do update
            dbConn.executeSQLWithParams(STR_UPDATE, valueEL, valueEN, skey.ToUpper)
        End If


        If cachedStrings.ContainsKey(skey.ToUpper) Then
            cachedStrings.Remove(skey.ToUpper)
        End If

    End Sub

    Public Sub New()

        If System.Configuration.ConfigurationManager.AppSettings.HasKeys Then
            Dim sInsertDefLangKey As String = AppSettings.Item("insertDefaultTranslatorKey")
            If sInsertDefLangKey IsNot Nothing Then
                Me._insertDefaultTranslatorKey = (sInsertDefLangKey.ToLower = "true" OrElse sInsertDefLangKey = "1")
            End If
        End If

    End Sub
End Class
