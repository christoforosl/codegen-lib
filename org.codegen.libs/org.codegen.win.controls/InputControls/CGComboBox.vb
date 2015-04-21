Imports System
Imports System.ComponentModel
Imports System.Data
Imports System.Diagnostics
Imports System.Windows.Forms
Imports System.Runtime.InteropServices

Public Class CGComboBox
    Inherits ComboBox
    Implements ICGBaseControl

#Region "read only combo code"

#Region "-- Declarations --"
    Private _ReadOnly As Boolean
    Private _DroppedDown As Boolean
    Private _SelectedIndex As Integer = -1
    Private _DropDownStyle As ComboBoxStyle = ComboBoxStyle.DropDown


    Private Declare Auto Function GetWindow Lib "user32.dll" (ByVal hwnd As IntPtr, ByVal wCmd As Int32) As IntPtr
    Private Declare Auto Function SendMessage Lib "user32.dll" (ByVal hwnd As IntPtr, ByVal wMsg As Int32, ByVal wParam As Boolean, ByVal lParam As Int32) As Int32

    <DllImport("user32.dll", CharSet:=CharSet.Auto, ExactSpelling:=True)>
    Private Shared Function GetDlgItem(ByVal hWnd As HandleRef, ByVal nIDDlgItem As Integer) As IntPtr
    End Function

    Private Const EM_SETREADONLY As Int32 = &HCF
    Private Const EM_EMPTYUNDOBUFFER As Int32 = &HCD
    Private Const CB_SHOWDROPDOWN As Int32 = &H14F
    Private Const GW_CHILD As Int32 = 5
#End Region     ' -- Declarations --

#Region "-- Properties --"
    <Browsable(True), DefaultValue(False)> _
    Public Property [ReadOnly]() As Boolean Implements ICGBaseControl.ReadOnly
        Get
            Return _ReadOnly
        End Get
        Set(ByVal Value As Boolean)
            ' In design mode we don't want setting the read only property 
            ' to alter the dropdown style.
            If Not DesignMode Then
                ' If the DropDownStyle is anything other than DropDown then setting
                ' ReadOnly to true will have no affect. Therefore we'll force the style
                ' to DropDown as it goes to ReadOnly and restore it when it's turned off.
                If Value Then
                    ' If the value is changing then we want to save the dropdown style.
                    ' In case the value gets set to true more than once we don't want 
                    ' to loose the saved drop down style.
                    If _ReadOnly <> Value Then
                        _DropDownStyle = MyBase.DropDownStyle
                        MyBase.DropDownStyle = ComboBoxStyle.Simple
                    End If
                Else    ' restore the saved drop down style
                    MyBase.DropDownStyle = _DropDownStyle
                End If
            End If
            _ReadOnly = Value
            ' If readonly then don't let the user tab to the field
            MyBase.TabStop = Not Value
            ' Setting TabStop to false causes the text in the box to be selected if it matches
            ' an entry in the list. Setting selection length to zero removes the selection.
            MyBase.SelectionLength = 0

            'if ComboBoxStyle.Simple is used, SendMessage(GetWindow(Me.Handle, GW_CHILD), EM_SETREADONLY, Value, 0) will not work
            'But the code below will sent a message to the handle of the textbox of the 
            'combo box by using the GetDlgItem
            Dim hr As New HandleRef(Me, Me.Handle)
            SendMessage(GetDlgItem(hr, 1001), EM_SETREADONLY, Value, 0)

            '' Send the textbox portion of the combo the readonly message.
            '' It will change the color and behavior.
            'SendMessage(GetWindow(Me.Handle, GW_CHILD), EM_SETREADONLY, Value, 0)

            ' If text was typed or pasted into the textbox, the context menu will
            ' have the undo activated. When the text box is in the readonly state
            ' the undo will still be active from the right click context menu
            ' allowing the user to restore the previous value. This sendmessage
            ' will clear the undo buffer which will clear the undo.
            SendMessage(GetWindow(Me.Handle, GW_CHILD), EM_EMPTYUNDOBUFFER, Value, 0)
            ' the dropdown may have been dropped before the readonly is set
            _DroppedDown = False
            Me.Refresh()
        End Set
    End Property
    '
    ' Saving and returning a local copy of the selected index keeps a
    ' changed value from being returned when the control is in the
    ' readonly state. The OnSelectedIndexChanged event captures the
    ' index value that is returned here. Must be shadows or else the
    ' value passed won't cause the OnSelectedIndexChanged method to fire
    ' and the text value to be displayed, won't.
    '
    Public Shadows Property SelectedIndex() As Integer
        Get
            Return _SelectedIndex
        End Get
        Set(ByVal Value As Integer)
            _SelectedIndex = Value
            MyBase.SelectedIndex = Value
            If Value = -1 Then ' Set it twice to work around databound bug KB327244
                _SelectedIndex = Value
                MyBase.SelectedIndex = Value
            End If
        End Set
    End Property
#End Region     ' -- Properties --

#Region "-- Overrides --"
    '
    ' Intercepting message 273 when readonly and the listbox is dropped
    ' keeps the user from selecting an item in the list and having it update
    ' the text value of the combo as well as firing the associated changed events.
    ' Since we intercept a windows message we will have to manually bring up
    ' the listbox.
    '
    ' msg 305 (0x131)   =  an item was clicked from the dropdown list
    ' msg 273 (0x111)   = (WM_COMMAND) follows dropdown list click and all other actions?
    ' msg 8465 (0x2111) = (WM_REFLECT + WM_COMMAND) subsequent command after the 273
    '
    Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
        ' Cannot use me.DroppedDown, it causes a System.StackOverflowException.
        ' Asking for it's value must produce windows messages for the combobox
        ' and thus create a recursive loop.
        If _ReadOnly AndAlso _DroppedDown Then
            If m.Msg = 273 Then
                _DroppedDown = False
                ' bring up the dropdown
                SendMessage(Me.Handle, CB_SHOWDROPDOWN, CBool(System.Convert.ToInt32(False)), 0)
                Exit Sub
            End If
        End If
        MyBase.WndProc(m)
    End Sub
    '
    ' This event will not fire when msg 273 is intercepted in WndProc. When in the
    ' readonly state clicking on an item in the listbox appears to have no effect. It
    ' does in fact change the value of MyBase.SelectedIndex. Saving the last good index
    ' value locally allows the overriden Index property to supply the proper index value.
    '
    Protected Overrides Sub OnSelectedIndexChanged(ByVal e As System.EventArgs)
        _SelectedIndex = MyBase.SelectedIndex
        MyBase.OnSelectedIndexChanged(e)
    End Sub
    '
    ' We must manually track dropped state. Asking the control if it's
    ' dropped from within WndProc will cause a System.StackOverflowException.
    '
    Protected Overrides Sub OnDropDown(ByVal e As System.EventArgs)
        _DroppedDown = True
        MyBase.OnDropDown(e)
    End Sub
    '
    ' The up and down arrow keys cause the combobox to change selection to the next
    ' or previous in the list. The page up and page down keys change the selection
    ' by one page at a time as defined by the size of the dropdown list. Setting
    ' e.Handled to true if any of these keys is pressed stops the selection change
    ' when readonly. The alt down arrow combination is allowed since it drops the listbox.
    '
    Protected Overrides Sub OnKeyDown(ByVal e As System.Windows.Forms.KeyEventArgs)
        If _ReadOnly Then
            If e.KeyCode = Keys.Up OrElse e.KeyCode = Keys.PageUp OrElse _
              e.KeyCode = Keys.PageDown OrElse _
             (e.KeyCode = Keys.Down And ((Control.ModifierKeys And Keys.Alt) <> Keys.Alt)) Then
                e.Handled = True
            End If
        End If
        MyBase.OnKeyDown(e)
    End Sub
    '
    ' The combobox default behavior when pressing F4 is to drop the listbox.
    ' If F4 is immediately pressed a second time the OnSelectionChangeCommitted
    ' event fires regardless of whether a change has been made or not. When
    ' readonly we don't want a change event to fire. This code will stop it.
    '
    Protected Overrides Sub OnSelectionChangeCommitted(ByVal e As System.EventArgs)
        If _ReadOnly Then
        Else
            MyBase.OnSelectionChangeCommitted(e)
        End If
    End Sub
#End Region     ' -- Overrides --

#End Region

#Region "Fields"

    Private components As IContainer

    Private _isMandatory As Boolean
    Private _loading As Boolean


#End Region

#Region "iCGBaseControl Properties"

    Private _AssociatedLabel As Windows.Forms.Label

    Public Property MaxValue As String Implements ICGBaseControl.MaxValue
    Public Property MinValue As String Implements ICGBaseControl.MinValue
    Public Property ErrProvider As System.Windows.Forms.ErrorProvider Implements ICGBaseControl.ErrProvider

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
#End Region

    Public Sub New()
        Me.SetStyle(ControlStyles.SupportsTransparentBackColor, True)

        Me._loading = False
        Me.InitializeComponent()
        Me.Font = FormsApplicationContext.current.ApplicationDefaultFont
        Me.DropDownStyle = ComboBoxStyle.DropDownList

    End Sub

    Public Sub New(ByVal Container As IContainer)

        Me.New()
        Container.Add(Me)

    End Sub

    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If (disposing AndAlso (Not Me.components Is Nothing)) Then
            Me.components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub


    <DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New Container
    End Sub

    Protected Overrides Sub OnSelectedItemChanged(ByVal e As EventArgs)
        If Not Me._loading Then
            MyBase.OnSelectedItemChanged(e)
        End If
    End Sub

    ''' <summary>
    ''' Returns the selected value of the control as a Nullable(Of Integer)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), _
    Description("Returns the Int value of selected Item.")> _
    Public ReadOnly Property intValue() As Integer?
        Get
            'change, 20/7: only return a number if value is greater than 0
            If IsNumeric(Me.Value) AndAlso CInt(Me.Value) > 0 Then
                Return CInt(Me.Value)
            Else
                Return Nothing
            End If
        End Get

    End Property


    ''' <summary>
    ''' Returns the selected value of the combo box as a String
    ''' </summary>
    <Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), _
     Description("Returns the String value of selected Item.")> _
    Public ReadOnly Property StrValue() As String
        Get
            'change, 20/7: only return a value if value is not empty string
            If Me.Value Is Nothing OrElse String.IsNullOrEmpty(CStr(Me.Value)) Then
                Return String.Empty
            Else
                Return CStr(Me.Value)
            End If
        End Get

    End Property


    <Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), _
        Description("Returns the Value of selected Item. Note: empty Strings and 0 values are ignored and nothing is returned instead.")> _
    Public Property Value() As Object Implements ICGBaseControl.Value

        Get
            If IsDBNull(Me.SelectedValue) = False _
                    AndAlso Me.SelectedValue IsNot Nothing AndAlso _
                    String.IsNullOrEmpty(CStr(Me.SelectedValue)) = False Then

                'note: we removed AndAlso _
                '    CStr(Me.SelectedValue) <> "0"
                'if "0" is selected we want it to be returned. 
                'In case of a boolean value 1/0 we need the 0 to be a legitimate value.
                'If you need to add a "Please Select", give it an string empty value and not 0
                '
                If CStr(Me.SelectedValue) = "0" AndAlso FormsApplicationContext.current.ZerosInComboBoxesAreNull Then
                    Return Nothing
                Else
                    Return Me.SelectedValue
                End If
            Else
                Return Nothing
            End If
        End Get

        Set(ByVal value As Object)
            Me.FormattingEnabled = True
            If ((Not value Is Nothing) AndAlso (CStr(value) <> String.Empty)) Then
                Me.SelectedValue = value
            Else
                Me.SelectedItem = Nothing
                Me.SelectedIndex = -1
            End If

        End Set

    End Property


    Public Property isMandatory() As Boolean Implements ICGBaseControl.isMandatory
        Get
            Return _isMandatory
        End Get
        Set(ByVal value As Boolean)
            If value Then
                Me.BackColor = System.Drawing.Color.LightYellow
            Else
                Me.BackColor = System.Drawing.Color.Transparent
            End If
            _isMandatory = value
        End Set

    End Property

    Private Sub CGComboBox_ParentChanged(sender As Object, e As System.EventArgs) Handles Me.ParentChanged
        If _AssociatedLabel IsNot Nothing AndAlso _
               Me.isMandatory AndAlso _
               FormsApplicationContext.current.MarkMandatoryFieldsWithAsterisk Then

            AddHandler _AssociatedLabel.TextChanged, AddressOf CGTextBox.addAsteriskToLabel
            Call CGTextBox.addAsteriskToLabel(_AssociatedLabel, New EventArgs)
            _AssociatedLabel.ForeColor = FormsApplicationContext.current.requiredLabelsColor()
        End If
    End Sub

    Private Sub _Validating(ByVal sender As Object, _
              ByVal e As System.ComponentModel.CancelEventArgs) _
             Handles Me.Validating

        If Me.Value Is Nothing AndAlso Me.isMandatory Then
            Me.addError(CGTextBox.getReqFieldMessage(Me.Label))
            e.Cancel = True
        End If

    End Sub

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



    Public Sub makeReadOnly() Implements IReadOnlyEnabled.setReadOnly
        Me.ReadOnly = True
    End Sub



End Class


