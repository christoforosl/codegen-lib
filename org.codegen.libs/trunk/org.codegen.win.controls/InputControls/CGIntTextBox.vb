Public Class CGIntTextBox
    Inherits CGTextBox

    Protected Shadows sFormatPattern As String = "0"
    Private _interval As Single = 0.5
    Private _canGoNegative As Boolean = False

#Region " Component Designer generated code "

    Public Sub New(ByVal Container As System.ComponentModel.IContainer)
        MyClass.New()

        'Required for Windows.Forms Class Composition Designer support
        Container.Add(Me)
    End Sub

    Public Sub New()
        MyBase.New()

        'This call is required by the Component Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

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

    End Sub

#End Region

    <System.ComponentModel.Browsable(False), _
    System.ComponentModel.DesignerSerializationVisibilityAttribute(System.ComponentModel.DesignerSerializationVisibility.Hidden)> _
    Public Overrides Property value() As Object
        Get
            If IsNumeric(Me.Text) Then
                Return CInt(Me.Text)
            Else
                Return Nothing
            End If
        End Get

        Set(ByVal sValue As Object)

            If IsNumeric(sValue) Then
                Me.Text = CStr(sValue)
            Else
                Me.Text = String.Empty
            End If

        End Set

    End Property

    Protected Function numberKeyPress(ByVal KeyAscii As Integer) As Boolean

        If Chr(KeyAscii) = "+" Then
            Me.Text = CStr(Val(Me.Text) + _interval)
            KeyAscii = 0
            Return True

        ElseIf Chr(KeyAscii) = "-" Then
            If Val(Me.Text) - _interval > 0 Then
                Me.Text = CStr(Val(Me.Text) - _interval)
            Else
                Me.Text = CStr(0)
            End If
            Return True

        Else
            Select Case KeyAscii
                Case 8, 13 'backspace, enter, ALLOW!!
                    Return False
                Case 48 To 57 'range of numbers, ALLOW!!
                    Return False
                Case Else
                    Return True
            End Select

        End If

        Return False

    End Function

    Public Overrides Property Text() As String

        Get
            Return MyBase.Text
        End Get

        Set(ByVal Value As String)
            If IsNumeric(Value) Then
                MyBase.Text = Format(Val(Value), sFormatPattern)
            Else
                MyBase.Text = Value

            End If
        End Set

    End Property

    Private Sub CGLNumberTextBox_KeyPress(ByVal sender As Object, _
                                          ByVal e As System.Windows.Forms.KeyPressEventArgs) _
                                      Handles MyBase.KeyPress

        e.Handled = Me.numberKeyPress(Asc(e.KeyChar))

    End Sub

    Private Sub CGIntTextBox_Validating(ByVal sender As Object, _
                                        ByVal e As System.ComponentModel.CancelEventArgs) _
                                    Handles Me.Validating

        If Not String.IsNullOrEmpty(Me.Text) Then
            Dim intvalue As Integer
            If Not Integer.TryParse(Me.Text, intvalue) Then
                Me.addError(String.Format(WinControlsLocalizer.getString("invalid_integer"), Me.Label))
                e.Cancel = True
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
    Public Overrides Function checkMinValue() As Boolean

        If Me.MinValue Is Nothing OrElse String.IsNullOrEmpty(Me.Text) Then
            Return True
        Else
            Dim compareToResult As Integer = CInt(Me.Text).CompareTo(CInt(Me.MinValue))
            Return compareToResult = COMPARE_TO_GREATER_THAN OrElse _
                       compareToResult = COMPARE_TO_EQUAL
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
    Public Overrides Function checkMaxValue() As Boolean

        If Me.MaxValue Is Nothing OrElse String.IsNullOrEmpty(Me.Text) Then
            Return True
        Else
            Dim compareToResult As Integer = CInt(Me.Text).CompareTo(CInt(Me.MaxValue))
            Return compareToResult = COMPARE_TO_LESS_THAN OrElse _
                       compareToResult = COMPARE_TO_EQUAL
        End If

    End Function

End Class
