Imports System.ComponentModel

Public Class CGTextBox
    Inherits TextBox
    Implements ICGBaseControl

    Protected _isMandatory As Boolean

#Region " Component Designer generated code "

    Public Sub New(ByVal Container As System.ComponentModel.IContainer)
        MyClass.New()

        'Required for Windows.Forms Class Composition Designer support
        Container.Add(Me)
    End Sub

    Public Sub New()

        MyBase.New()
        Me.SetStyle(ControlStyles.SupportsTransparentBackColor, True)
        Me.CharacterCasing = FormsApplicationContext.current.ApplicationDefaultCasing

        'This call is required by the Component Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        Me.Font = FormsApplicationContext.current.ApplicationDefaultFont


    End Sub

    'Component overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Component Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Component Designer
    'It can be modified using the Component Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        components = New System.ComponentModel.Container()
    End Sub

#End Region

#Region "Properties"

    Private _AssociatedLabel As Label

    Public Const COMPARE_TO_LESS_THAN As Integer = -1
    Public Const COMPARE_TO_GREATER_THAN As Integer = 1
    Public Const COMPARE_TO_EQUAL As Integer = 0

    Public Property MaxValue As String = Nothing Implements ICGBaseControl.MaxValue
    Public Property MinValue As String = Nothing Implements ICGBaseControl.MinValue

    Public Property ErrProvider As System.Windows.Forms.ErrorProvider Implements ICGBaseControl.ErrProvider
    Public Overridable Property FormatPattern() As String

    ''' <summary>
    ''' The name of the field that corresponds to the name of the ModelObject property
    ''' </summary>
    ''' <remarks></remarks>
    Private _DataPropertyName As String

    ''' <summary>
    ''' Gets/Sets the DataPropertyName
    ''' </summary>
    Public Property DataPropertyName As String Implements ICGBaseControl.DataPropertyName
        Get
            If String.IsNullOrEmpty(_DataPropertyName) Then
                Return Me.Name
            End If
            Return _DataPropertyName
        End Get
        Set(value As String)
            _DataPropertyName = value
        End Set
    End Property

    ''' <summary>
    ''' We need to remove the accents so we override default.net 
    ''' behavior 
    ''' </summary>
    ''' <remarks></remarks>
    Private _CharacterCasing As CharacterCasing

    Public Property AssociatedLabel As Label Implements ICGBaseControl.AssociatedLabel
        Get
            Return _AssociatedLabel
        End Get
        Set(ByVal value As Label)
            _AssociatedLabel = value
            If _AssociatedLabel IsNot Nothing Then
                _AssociatedLabel.Font = FormsApplicationContext.current.ApplicationDefaultFont

            End If

        End Set
    End Property

    Public Property isMandatory() As Boolean Implements ICGBaseControl.isMandatory
        Get
            Return _isMandatory
        End Get
        Set(ByVal value As Boolean)
            _isMandatory = value
            CGTextBox.setbackColor(Me)
        End Set

    End Property

    <Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
    Public Overridable Property value() As Object Implements ICGBaseControl.Value
        Get
            Return Me.Text
        End Get
        Set(ByVal sValue As Object)

            Me.Text = CStr(sValue)

        End Set
    End Property

    <Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
    Public Property strValue() As String
        Get
            Return Me.Text
        End Get
        Set(ByVal sValue As String)
            Me.Text = sValue
        End Set
    End Property

    Public Shadows Property CharacterCasing As CharacterCasing
        Get
            Return _CharacterCasing
        End Get
        Set(ByVal value As CharacterCasing)
            _CharacterCasing = value
        End Set
    End Property

    Public Overrides Property Text As String
        Get
            If Me.CharacterCasing = CharacterCasing.Upper Then
                Return UpperCaseAndRemoveDiacritics(MyBase.Text)
            Else
                Return MyBase.Text
            End If

        End Get
        Set(ByVal value As String)
            If Me.CharacterCasing = CharacterCasing.Upper AndAlso value IsNot Nothing Then
                MyBase.Text = UpperCaseAndRemoveDiacritics(value)
            Else
                MyBase.Text = value
            End If

        End Set
    End Property

    Public Overloads Property [ReadOnly]() As Boolean _
                    Implements ICGBaseControl.ReadOnly
        Get
            Return MyBase.ReadOnly
        End Get
        Set(ByVal value As Boolean)
            MyBase.ReadOnly = value
            CGTextBox.setbackColor(Me)
        End Set
    End Property

#End Region

    Public Shared Sub setbackColor(ByVal c As ICGBaseControl)

        If c Is Nothing Then Exit Sub
        Dim lC As Control = CType(c, Control)

        If c.ReadOnly Then
            lC.BackColor = System.Drawing.SystemColors.ButtonFace 'Color.FromArgb(204, 204, 204)
        Else
            If c.isMandatory Then
                lC.BackColor = System.Drawing.Color.LightYellow
            Else
                lC.BackColor = System.Drawing.Color.Transparent
            End If
        End If

    End Sub

    Private Sub CGTextBox_ParentChanged(sender As Object, e As System.EventArgs) Handles Me.ParentChanged
        If _AssociatedLabel IsNot Nothing AndAlso _
                Me.isMandatory AndAlso _
                FormsApplicationContext.current.MarkMandatoryFieldsWithAsterisk Then

            AddHandler _AssociatedLabel.TextChanged, AddressOf CGTextBox.addAsteriskToLabel
            Call CGTextBox.addAsteriskToLabel(_AssociatedLabel, New EventArgs)
            _AssociatedLabel.ForeColor = FormsApplicationContext.current.requiredLabelsColor()
        End If
    End Sub

    Private Sub CGTextBox_Validating(ByVal sender As Object, _
                                     ByVal e As System.ComponentModel.CancelEventArgs) _
                                 Handles Me.Validating

        If Me.DesignMode Then Exit Sub
        If Me.ErrProvider Is Nothing Then Exit Sub

        If Me.isMandatory AndAlso String.IsNullOrEmpty(Me.Text) Then
            Me.addError(getReqFieldMessage(Me.Label))
            e.Cancel = True
        Else
            If Me.checkMinValue = False Then
                Me.addError("Minimum allowed value in text box is " & CStr(Me.MinValue))
                e.Cancel = True
                Return
            End If
            If Me.checkMaxValue = False Then
                Me.addError("Maximum allowed value in text box is " & CStr(Me.MaxValue))
                e.Cancel = True
                Return
            End If

        End If

    End Sub

    ''' <summary>
    ''' Compares the text property of the control to the min value property.
    ''' Return true if valid data are entered in the control 
    ''' </summary>
    ''' <returns>True if value is greater than or equal to the MinValue property</returns>
    ''' <remarks>If MinValue is not set, or if the control's text is empty string, the function 
    ''' returns true
    ''' </remarks>
    Public Overridable Function checkMinValue() As Boolean

        If Me.MinValue Is Nothing OrElse String.IsNullOrEmpty(Me.Text) Then
            Return True
        Else
            Return CStr(Me.Text).CompareTo(Me.MinValue) = COMPARE_TO_GREATER_THAN OrElse _
                        CStr(Me.Text).CompareTo(Me.MinValue) = COMPARE_TO_EQUAL
        End If

    End Function

    ''' <summary>
    ''' Compares the text property of the control to the min value property.
    ''' Return true if valid data are entered in the control 
    ''' </summary>
    ''' <returns>True if value is less than or equal to the MaxValue property</returns>
    ''' <remarks>If MaxValue is not set, or if the control's text is empty string, the function 
    ''' returns true
    ''' </remarks>
    Public Overridable Function checkMaxValue() As Boolean

        If Me.MaxValue Is Nothing OrElse String.IsNullOrEmpty(Me.Text) Then
            Return True
        Else
            Return CStr(Me.Text).CompareTo(Me.MaxValue) = COMPARE_TO_LESS_THAN OrElse _
                        CStr(Me.Text).CompareTo(Me.MaxValue) = COMPARE_TO_EQUAL
        End If

    End Function

    Public Shared Function getReqFieldMessage(ByVal fldlabel As String) As String

        Dim reqFieldErrMsg As String = WinControlsLocalizer.getString("please_enter_or_select_field")
        Return String.Format(reqFieldErrMsg, fldlabel)

    End Function

    Public Sub addError(ByVal errMesg As String) Implements ICGBaseControl.addError
        If Me.ErrProvider IsNot Nothing Then
            Me.ErrProvider.SetError(Me, errMesg)
        End If
    End Sub

    Public Function Label() As String Implements ICGBaseControl.Label
        If Me.AssociatedLabel IsNot Nothing Then
            Return Me.AssociatedLabel.Text
        Else
            Return String.Empty
        End If
    End Function


    Public Shared Function UpperCaseAndRemoveDiacritics(ByVal inText As [String]) As [String]

        If inText Is Nothing Then Return Nothing
        If String.IsNullOrEmpty(inText.Trim) Then Return String.Empty

        Dim normalizedString As [String] = inText.Normalize(System.Text.NormalizationForm.FormD)
        Dim stringBuilder As New System.Text.StringBuilder()

        For i As Integer = 0 To normalizedString.Length - 1
            Dim c As [Char] = normalizedString(i)
            If Globalization.CharUnicodeInfo.GetUnicodeCategory(c) = _
                Globalization.UnicodeCategory.NonSpacingMark OrElse Globalization.CharUnicodeInfo.GetUnicodeCategory(c) = _
                Globalization.UnicodeCategory.ModifierSymbol Then
                'nothing to do here. ModifierSymbol = tonos...
            Else
                stringBuilder.Append(c)
            End If
        Next

        Return stringBuilder.ToString().Trim.ToUpper

    End Function

    Public Sub makeReadOnly() Implements IReadOnlyEnabled.setReadOnly
        Me.ReadOnly = True
    End Sub

    Public Shared Sub addAsteriskToLabel(sender As Object, e As EventArgs)

        Dim c As Label = CType(sender, Label)
        Const STR_Constant As String = " *"

        If Not c.Text.EndsWith(STR_Constant) Then
            c.Text = c.Text & STR_Constant
        End If



    End Sub


End Class
