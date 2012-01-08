Imports System.Globalization
Imports System.Security.Permissions
Imports System.Threading
Imports System.Collections.Generic
Imports System.Configuration.ConfigurationManager

Namespace Translation
    ''' <summary>
    ''' Abstract class to provide an interface for Language translation of key-value pair
    ''' </summary>
    ''' <remarks></remarks>
    Public MustInherit Class ILanguageStrings

        Public Shared ReadOnly CULT_GREEK_GREECE As String = "EL-GR"
        Public Shared ReadOnly CULT_GREEK_CY As String = "EL-CY"
        Public Shared ReadOnly CULT_ENGLISH_UK As String = "EN-GB"

        Public Shared ReadOnly LANG_GREEK As String = "EL"
        Public Shared ReadOnly LANG_ENGLISH As String = "EN"

        Protected Const LABEL_KEY As String = "_label"
        Protected cachedStrings As Dictionary(Of String, String)

        Public MustOverride Sub deleteString(ByVal key As String)
        Public MustOverride Sub insertOrUpdate(ByVal key As String, ByVal valueEN As String, ByVal valueEL As String)
        Public MustOverride Function getStringDB(ByVal key As String, Optional ByVal inLang As String = "") As String

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
            Dim lGetStringDB As String = Me.getStringDB(key, inlang)

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

        Public Shared Function getFromConfig() As ILanguageStrings


            If AppSettings.Item("Translator") IsNot Nothing Then
                Try

                    Dim oTranslator As Object = Nothing
                    Dim ttype As Type = Type.GetType(AppSettings.Item("Translator"))
                    oTranslator = Activator.CreateInstance(ttype)
                    Return CType(oTranslator, ILanguageStrings)

                Catch ex As Exception
                    'just ignore error and return null
                End Try
                Return Nothing

            Else
                Return Nothing
            End If

        End Function

    End Class
End Namespace
