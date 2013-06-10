Imports System.Globalization
Imports System.Security.Permissions
Imports System.Threading
Imports System.Collections.Generic
Imports System.Configuration.ConfigurationManager

Namespace TranslationServices

    ''' <summary>
    ''' Abstract class to provide an interface for Language translation of key-value pair
    ''' </summary>
    ''' <remarks></remarks>
    Public MustInherit Class TranslatedStringsProvider

        Protected cachedStrings As Dictionary(Of String, String)

        ''' <summary>
        ''' Deletes a string to the store where translated strings are stored.
        ''' </summary>
        Public MustOverride Sub deleteString(ByVal key As String)

        ''' <summary>
        ''' Inserts or Updated a string to the store where translated strings are stored.
        ''' </summary>
        ''' <param name="key">Key of string to save/update</param>
        ''' <param name="valueEL">Value in Greek</param>
        ''' <param name="valueEN">Value in English</param>
        ''' <remarks></remarks>
        Public MustOverride Sub insertOrUpdate(ByVal key As String, ByVal valueEL As String, ByVal valueEN As String)


        ''' <summary>
        ''' Rertrieves a string from the store where translated strings are stored.
        ''' </summary>
        ''' <param name="key">Key of string to retrieve</param>
        ''' <param name="inLang">Language code - This is the TwoLetterISOLanguageName</param>
        ''' <returns>Translated string</returns>
        ''' <remarks></remarks>
        Public MustOverride Function retrieveStringFromStore(ByVal key As String, Optional ByVal inLang As String = "") As String

        ''' <summary>
        ''' Returns the TwoLetterISOLanguageName of the current thread
        ''' </summary>
        Public Shared ReadOnly Property CurrentLanguageCode() As String
            Get
                Return Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName.ToUpper
            End Get

        End Property

        Public Function getString(ByVal key As String, _
                                 Optional ByVal inlang As String = "", _
                                 Optional ByVal vdefault As String = "") As String

            Dim collKey As String
            Dim ret As String

            If inlang = String.Empty Then inlang = CurrentLanguageCode
            If vdefault = String.Empty Then vdefault = key
            If key = String.Empty Then Return String.Empty

            collKey = key & inlang
            collKey = collKey.ToUpper

            If cachedStrings.ContainsKey(collKey) Then
                ret = CStr(cachedStrings.Item(collKey))
            Else
                ret = GetStringFromProvider(key, inlang, collKey)
            End If
            If ret = String.Empty Then
                ret = vdefault
            End If

            Return ret

        End Function
        Private Sub AddToCachedStrings(ByVal collKey As String, ByVal lGetStringDB As String)

            cachedStrings.Add(collKey, lGetStringDB)

        End Sub
        Private Function GetStringFromProvider(ByVal key As String, ByVal inlang As String, ByVal collKey As String) As String

            Dim ret As String
            Dim lGetStringDB As String = Me.retrieveStringFromStore(key, inlang)

            Me.AddToCachedStrings(collKey, lGetStringDB)
            ret = CStr(cachedStrings.Item(collKey))
            Return ret

        End Function

        Public Function reset() As Boolean

            cachedStrings = Nothing
            cachedStrings = New Dictionary(Of String, String)()
            reset = True

        End Function

        Public Sub New()

            Me.cachedStrings = New Dictionary(Of String, String)()

        End Sub
    End Class

End Namespace
