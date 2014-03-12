Imports System.ComponentModel

Public Class CGDateTextBox
    Inherits CGTextBox

    Protected Const MINIMUM_DATE As Date = #12:00:00 AM#

    <Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
    Protected Overridable Property errMsgCode As String = "invalid_date"

    <Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
    Public Overrides Property FormatPattern As String = "dd/MM/yyyy"

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

    Protected Overridable Function dateKeyPress(ByVal KeyAscii As Integer) As Boolean

        Dim basedt As Date
        Dim _intervalType As String = "d"

        Const ASCII_GREEK_LOWER_M As Integer = 956
        Const ASCII_GREEK_UPPER_M As Integer = 924
        Const ASCII_GREEK_UPPER_T As Integer = 964
        Const ASCII_GREEK_UPPER_Y As Integer = 213
        Const ASCII_GREEK_LOWER_Y As Integer = 245

        If IsDate(Me.Text) Then
            basedt = CDate(Me.Text)
        Else
            basedt = Date.Today
        End If

        If ChrW(KeyAscii) = "+" Then
            Me.Text = FormatDateTime(DateAdd(_intervalType, 1, basedt), DateFormat.ShortDate)
            Return True

        ElseIf ChrW(KeyAscii) = "-" Then
            Me.Text = FormatDateTime(DateAdd(_intervalType, -1, basedt), DateFormat.ShortDate)
            Return True

        ElseIf KeyAscii = ASCII_GREEK_UPPER_T Or _
               KeyAscii = ASCII_GREEK_UPPER_T Or _
               KeyAscii = 116 Or _
               KeyAscii = 84 Then

            Me.Text = FormatDateTime(Date.Today, DateFormat.ShortDate)
            Return True
        ElseIf KeyAscii = 77 Or _
               KeyAscii = 109 Or _
               KeyAscii = ASCII_GREEK_UPPER_M Or _
               KeyAscii = ASCII_GREEK_LOWER_M Then

            Me.Text = FormatDateTime(CDate("1/" & Month(basedt) & "/" & Year(basedt)), DateFormat.ShortDate)
            Return True

        ElseIf KeyAscii = 89 Or _
               KeyAscii = 121 Or _
               KeyAscii = ASCII_GREEK_UPPER_Y Or _
               KeyAscii = ASCII_GREEK_LOWER_Y Then

            Me.Text = FormatDateTime(CDate("1/1/" & Year(basedt)), DateFormat.ShortDate)
            Return True

        ElseIf KeyAscii >= 45 And KeyAscii <= 57 Then
            Return False 'to allow input of mumbers and the "/",".", "-" signs

        ElseIf KeyAscii = 8 Then
            Return False

        Else
            Return True
        End If

    End Function

    Public Overrides Property Text() As String

        Get
            Return MyBase.Text
        End Get

        Set(ByVal Value As String)
            If IsDate(Value) AndAlso CDate(Value) > MINIMUM_DATE Then
                MyBase.Text = Format(CDate(Value), Me.FormatPattern)
            Else
                MyBase.Text = String.Empty
            End If
        End Set

    End Property

    <Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
    Public Overrides Property value() As Object
        Get
            Return Me.DateValue
        End Get
        Set(ByVal value As Object)
            If IsDate(value) Then
                Me.DateValue = CDate(value)
            Else
                Me.DateValue = Nothing
            End If
        End Set
    End Property

    <Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
    Public Property DateValue() As Date?

        Get
            If IsDate(MyBase.Text) Then
                Return CDate(MyBase.Text)
            End If
        End Get

        Set(ByVal Value? As Date)
            If Value IsNot Nothing Then
                MyBase.Text = Format(Value, Me.FormatPattern)
            Else
                MyBase.Text = String.Empty
            End If
        End Set

    End Property

    Private Sub _KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress

        e.Handled = Me.dateKeyPress(Asc(e.KeyChar))

    End Sub


    Private Sub _Validating(ByVal sender As Object, _
                            ByVal e As System.ComponentModel.CancelEventArgs) Handles Me.Validating

        If String.IsNullOrEmpty(Me.Text) = False AndAlso IsDate(Me.Text) = False Then

            Me.addError(String.Format(WinControlsLocalizer.getString(Me.errMsgCode), Me.Label))
            e.Cancel = True
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

        If String.IsNullOrEmpty(Me.MinValue) OrElse String.IsNullOrEmpty(Me.Text) Then
            Return True
        Else
            Dim compareToResult As Integer = CDate(Me.Text).CompareTo(CDate(Me.MinValue))
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

        If String.IsNullOrEmpty(Me.MaxValue) OrElse Not IsDate(Me.MaxValue) OrElse String.IsNullOrEmpty(Me.Text) Then
            Return True
        Else
            Dim compareToResult As Integer = CDate(Me.Text).CompareTo(CDate(Me.MinValue))
            Return compareToResult = COMPARE_TO_LESS_THAN OrElse _
                       compareToResult = COMPARE_TO_EQUAL
        End If

    End Function

End Class
