Imports System.Configuration.ConfigurationManager

Namespace TranslationServices

    ''' <summary>
    ''' Global System translator.  This is a singleton class
    ''' </summary>
    Public Class Translator

        Private Shared _Translator As Translator

        Private _stringsProvider As TranslatedStringsProvider

        Private Sub New()
            'prevent public instantiations
        End Sub

        ''' <summary>
        ''' Gets/Sets the instance of a TranslatedStringsProvider to retieve translated strings
        ''' </summary>
        Public Property StringsProvider() As TranslatedStringsProvider
            Get
                Return _stringsProvider
            End Get
            Set(ByVal value As TranslatedStringsProvider)
                _stringsProvider = Value
            End Set
        End Property

        ''' <summary>
        ''' Current instance of system translator
        ''' </summary>
        Public Shared Property current As Translator
            Get
                If _Translator Is Nothing Then
                    _Translator = New Translator
                    _Translator.StringsProvider = getFromConfig()
                End If
                Return _Translator
            End Get

            Set(ByVal value As Translator)
                _Translator = value
            End Set
        End Property

        ''' <summary>
        ''' Retrurns true if the current systen language is English
        ''' </summary>
        ''' <returns>True/False</returns>
        ''' <remarks>The current system language is the Curent Thread's </remarks>
        Public Function isEnglish() As Boolean
            Return TranslatedStringsProvider.CurrentLanguageCode = TranslatedStringsProvider.LANG_ENGLISH
        End Function

        ''' <summary>
        ''' Retrurns true if the current systen language is Greek
        ''' </summary>
        ''' <returns>True/False</returns>
        ''' <remarks>The current system language is the Curent Thread's </remarks>
        Public Function isGreek() As Boolean
            Return isEnglish() = False
        End Function

        Public Shared Function getString(ByVal key As String) As String
            Return current.StringsProvider.getString(key)
        End Function

        Public Shared Function getFromConfig() As TranslatedStringsProvider

            If AppSettings.Item("Translator") IsNot Nothing Then
                Try

                    Dim oTranslator As Object = Nothing
                    Dim ttype As Type = Type.GetType(AppSettings.Item("Translator"))
                    oTranslator = Activator.CreateInstance(ttype)
                    Return CType(oTranslator, TranslatedStringsProvider)

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