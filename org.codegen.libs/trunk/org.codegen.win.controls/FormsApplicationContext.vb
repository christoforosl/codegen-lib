Imports System.Configuration.ConfigurationManager
Imports org.codegen.common.TranslationServices

''' <summary>
''' Singleton class
''' </summary>
''' <remarks></remarks>
Public Class FormsApplicationContext

#Region "Singleton"

    Private Shared context As FormsApplicationContext

    Private Sub New()

    End Sub

    Public Shared Function current() As FormsApplicationContext
        If context Is Nothing Then
            context = New FormsApplicationContext
        End If
        Return context

    End Function

#End Region


    ''' <summary>
    ''' If this property is true, then hitting "ENTER" key will validate and save a form
    ''' </summary>
    ''' <remarks></remarks>
    Public Property SaveOnEnterKey As Boolean = True

    Private _ApplicationDefaultCasing As CharacterCasing = _
       CharacterCasing.Normal

    Private _ApplicationdefaultFont As Font = _
                    New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, _
                                System.Drawing.GraphicsUnit.Point, CType(0, Byte))


    Private _Translator As TranslatedStringsProvider

    Public Property MarkMandatoryFieldsWithAsterisk As Boolean = True

    Public Property requiredLabelsColor As Color = Color.DarkRed

    Public Property Translator() As TranslatedStringsProvider

        Get
            If _Translator Is Nothing AndAlso AppSettings.Item("Translator") IsNot Nothing Then
                _Translator = TranslationServices.Translator.getFromConfig
            End If
            Return _Translator
        End Get

        Set(ByVal value As TranslatedStringsProvider)
            _Translator = value
        End Set

    End Property


    ''' <summary>
    ''' Gets/Sets the font used by all input controls
    ''' </summary>
    ''' <remarks>
    ''' All custom input controls are automatically assigned this font.
    ''' To change it, call
    ''' <code>ApplicationContext.current.ApplicationDefaultFont = yourFont</code>
    ''' </remarks>
    Public Property ApplicationDefaultFont() As Font
        Get
            Return _ApplicationdefaultFont
        End Get
        Set(ByVal value As Font)
            _ApplicationdefaultFont = value
        End Set
    End Property

    ''' <summary>
    ''' Gets/Sets the character casing used by all text controls in the system
    ''' </summary>
    ''' <remarks>
    ''' All CGTextBox input controls are automatically assigned this casing by default.
    ''' To change it, call
    ''' <code>winUtils.ApplicationDefaultCasing = xxx </code>
    ''' </remarks>
    Public Property ApplicationDefaultCasing() As CharacterCasing
        Get
            Return _ApplicationDefaultCasing
        End Get
        Set(ByVal value As CharacterCasing)
            _ApplicationDefaultCasing = value
        End Set
    End Property

    Private _windowTitle As String = Nothing

    ''' <summary>
    ''' Gets/Sets text that appears on Message Boxes
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>
    ''' At first retrieval of this property, and if the value of the property is null, 
    ''' it is retrieved from the Assembly Attribute "title"
    ''' So insert this in the assemblyinfo.vb file:
    ''' &lt;Assembly: AssemblyTitle("Your Title Here")&gt;
    ''' Alternatively, you can set this to a manual value in application startup:
    ''' <code>winUtils.windowTitle = "Your Title"</code>
    ''' </remarks>
    Public Property ApplicationTitle As String
        Get
            If _windowTitle Is Nothing Then
                _windowTitle = CommonUtils.GetEntryAssemblyTitle() & " [v " & CommonUtils.getEntryAssemblyVersion() & "]"
            End If
            Return _windowTitle
        End Get
        Set(ByVal value As String)
            _windowTitle = value
        End Set
    End Property


    Public Property MessageBoxHandler As IMessageBoxHandler = New DefaultMessageBoxHandler

End Class
