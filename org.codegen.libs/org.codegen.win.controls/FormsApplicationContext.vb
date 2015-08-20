Imports System.Configuration.ConfigurationManager
Imports org.codegen.common.TranslationServices
Imports System.Collections.Generic

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
    ''' Affects CGComboBox controls behavior when checking for valid values.  
    ''' If true, 0 values are assumed to be nothing selected in combox boxes
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ZerosInComboBoxesAreNull As Boolean = False


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

    Public Property editRecordIsNewProvider As IBaseEditRecordIsNewProvider = New BaseEditRecordIsNewProvider()

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




''' <summary>
''' Validation of using the fluent api. Example:
''' <para>
''' if (new UIValidate().
'''				addTranslatedValidationCondition(conditionA, "select_at_least_one_employee").
'''				addValidationCondition(conditionB, "some message {0}", this.CheckdateLabel.Text).
'''				validateAndShowErrors() == false) {
'''					return;
'''			}
''' </para>
''' </summary>
''' <remarks></remarks>
Public Class UIValidate

    Private Class UIValidateCondition

        Property isValid As Boolean = True
        Property errorMessage As String

        Public Overrides Function toString() As String
            Return errorMessage
        End Function

    End Class

    Private failedConditions As List(Of UIValidateCondition) = New List(Of UIValidateCondition)

    ''' <summary>
    ''' Adds a validation condition using the message as error message
    ''' </summary>
    ''' <param name="condition">If false, then the validation failed</param>
    ''' <param name="errorMessage">The error message</param>
    ''' <param name="vals">values to apply to string.format in errorMessage parameter</param>
    Public Function addValidateCondition(condition As Boolean, errorMessage As String, ParamArray vals() As String) As UIValidate

        If (Not condition) Then
            Dim uiv As New UIValidateCondition()
            uiv.isValid = False
            uiv.errorMessage = String.Format(errorMessage, vals)
            Me.failedConditions.Add(uiv)
        End If

        Return Me

    End Function

    ''' <summary>
    ''' Adds a validation condition with the specified language key and parameters for a translated message
    ''' </summary>
    ''' <param name="condition">boolean condition, if true then validation passes</param>
    ''' <param name="languageKey">a key with which a call to Translator.getString() will be called</param>
    ''' <param name="vals">Parameter values, if any to pass to translated string</param>
    Public Function addTranslatedValidationCondition(condition As Boolean, languageKey As String, ParamArray vals() As String) As UIValidate

        If (Not condition) Then
            Dim uiv As New UIValidateCondition()
            uiv.isValid = False
            uiv.errorMessage = String.Format(Translator.getString(languageKey, vals))
            Me.failedConditions.Add(uiv)
        End If
        Return Me

    End Function

    ''' <summary>
    ''' Validation occurs as the client adds condtions.  This call should be 
    ''' kept last in the chain, so that it shows the message box with all the errors.
    ''' Function returns true if all conditions are valid, otherwise it returns false
    ''' </summary>
    Public Function validateAndShowErrors() As Boolean

        If Me.failedConditions.Count > 0 Then
            winUtils.MsgboxStop(String.Join(vbCrLf, failedConditions))
            Return False
        End If

        Return True

    End Function

End Class
