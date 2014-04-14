Imports System.Configuration.ConfigurationManager
Imports System.Threading
Imports System.Globalization

Namespace TranslationServices

    ''' <summary>
    ''' Global System translator.  This is a singleton class
    ''' </summary>
    Public Class Translator

        Public Shared ReadOnly CULT_GREEK_GREECE As String = "EL-GR"
        Public Shared ReadOnly CULT_GREEK_CY As String = "EL-CY"
        Public Shared ReadOnly CULT_ENGLISH_UK As String = "EN-GB"

        Public Shared ReadOnly LANG_GREEK As String = "EL"
        Public Shared ReadOnly LANG_ENGLISH As String = "EN"

        Private Shared _Translator As Translator

        Private _stringsProvider As TranslatedStringsProvider

        Private Sub New()
            'prevent public instantiations
        End Sub

        ''' <summary>
        ''' Returns a culture with Greek language and the pound as currency symbol, if 
        ''' current date is before 31/12/07, else it returns the Euro
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks>
        ''' </remarks>
        Private Shared Function CYGreekCulture() As CultureInfo

            Const STR_POUND As String = "£"
            Const STR_EURO As String = "€"

            Dim ret As CultureInfo = New CultureInfo("el", False)
            If Date.Today > DateSerial(2007, 31, 1) Then
                ret.NumberFormat.CurrencySymbol = STR_EURO
            Else
                ret.NumberFormat.CurrencySymbol = STR_POUND
            End If

            ret.NumberFormat.CurrencyPositivePattern = 2
            ret.NumberFormat.CurrencyNegativePattern = 12
            ret.NumberFormat.CurrencyDecimalDigits = 2
            ret.NumberFormat.CurrencyDecimalSeparator = "."
            ret.NumberFormat.CurrencyGroupSeparator = ","

            ret.NumberFormat.NumberGroupSeparator = ","
            ret.NumberFormat.NumberDecimalSeparator = "."

            ret.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy"

            Return ret

        End Function

        ''' <summary>
        ''' Sets the system culture
        ''' </summary>
        ''' <param name="slang"></param>
        ''' <remarks></remarks>
        Public Sub setSystemCulture(ByVal slang As String)

            Dim ci As CultureInfo
            If slang = TranslationServices.Translator.LANG_ENGLISH OrElse _
                       slang = TranslationServices.Translator.CULT_ENGLISH_UK Then
                ci = New CultureInfo(TranslationServices.Translator.CULT_ENGLISH_UK)
            Else
                ci = CYGreekCulture()
            End If

            Thread.CurrentThread.CurrentUICulture = ci
            Thread.CurrentThread.CurrentCulture = ci

        End Sub

        Public Function getSystemCulture() As CultureInfo

            Return Thread.CurrentThread.CurrentUICulture

        End Function

        'Public Sub setSystemCulture(ByVal slang As String, ByVal currencySymbol As String)

        '    Dim ci As CultureInfo
        '    If slang = TranslationServices.Translator.LANG_ENGLISH OrElse _
        '                slang = TranslationServices.Translator.CULT_ENGLISH_UK Then

        '        ci = New CultureInfo(TranslationServices.Translator.CULT_ENGLISH_UK)
        '    Else
        '        ci = CYGreekCulture()
        '    End If
        '    ci.NumberFormat.CurrencySymbol = currencySymbol
        '    Thread.CurrentThread.CurrentUICulture = ci
        '    Thread.CurrentThread.CurrentCulture = ci

        'End Sub

        ''' <summary>
        ''' Gets/Sets the instance of a TranslatedStringsProvider to retieve translated strings
        ''' </summary>
        Public Property StringsProvider() As TranslatedStringsProvider
            Get
                Return _stringsProvider
            End Get
            Set(ByVal value As TranslatedStringsProvider)
                _stringsProvider = value
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
            Return TranslatedStringsProvider.CurrentLanguageCode = Translator.LANG_ENGLISH
        End Function

        ''' <summary>
        ''' Retrurns true if the current systen language is Greek
        ''' </summary>
        ''' <returns>True/False</returns>
        ''' <remarks>The current system language is the Curent Thread's </remarks>
        Public Function isGreek() As Boolean
            Return isEnglish() = False
        End Function

        Public Shared Function getString(ByVal key As String, ParamArray args() As String) As String
            Return String.Format(current.StringsProvider.getString(key), args)
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