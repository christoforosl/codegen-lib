Option Strict On

Imports System.Globalization
Imports System.Security.Permissions
Imports System.Threading
Imports System.Collections.Generic

Namespace Translation


    ''' <summary>
    ''' Global Language Translation class
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Lang

        Public Enum enumLangProvider
            LANG_PROVIDER_DB
            LANG_PROVIDER_XML
        End Enum

        '        Private Shared _languages As System.Collections.Generic.Dictionary(Of String, ILanguageStrings)
        Private Shared _provider As Translation.ILanguageStrings
        Public Shared langProviderType As enumLangProvider = enumLangProvider.LANG_PROVIDER_DB


        Shared Sub New()

            '_languages = New Dictionary(Of String, ILanguageStrings)

        End Sub
        Public Shared Function getStringFormatted(ByVal key As String, _
                                                  ByVal ParamArray params() As String) As String

            Dim str As String = getString(key)
            If params Is Nothing OrElse params.Length = 0 Then
                'just return the stmmt
            Else
                str = String.Format(str, params)
            End If
            Return str

        End Function

        Public Shared Function getString(ByVal key As String, Optional ByVal lang As String = "", Optional ByVal vdefault As String = "") As String

            If lang = "" Then
                lang = CurrentLanguageCode()
            End If

            Return Provider.getString(key, lang, vdefault)
            
        End Function

        Public Shared Sub reset()

            '_languages = New Dictionary(Of String, ILanguageStrings)
            Provider.reset()

        End Sub

        

        Public Shared Property Provider() As Translation.ILanguageStrings

            Get
                If _provider Is Nothing Then
                    If langProviderType = enumLangProvider.LANG_PROVIDER_DB Then
                        _provider = New DBLanguageStrings
                    Else
                        _provider = New XMLanguageStrings
                    End If
                End If

                Return _provider

            End Get

            Set(ByVal value As Translation.ILanguageStrings)
                _provider = value
            End Set

        End Property

        'Private Shared Function getNewLanguageStringsProvider(ByVal sLang As String) As ILanguageStrings

        '    If langProviderType = enumLangProvider.LANG_PROVIDER_DB Then
        '        Return New DBLanguageStrings()
        '    Else
        '        Return New XMLanguageStrings()
        '    End If

        'End Function

    End Class

End Namespace
