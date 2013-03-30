Imports System.ComponentModel

Public Class CGDecimalTextBox
    Inherits CGTextBox

    <Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
    Public Overrides Property value() As Object
        Get
            If IsNumeric(Me.Text) Then
                Return CDec(Me.Text)
            Else
                Return String.Empty
            End If
        End Get

        Set(ByVal sValue As Object)
            If TypeOf sValue Is Decimal Then
                Me.decimalValue = CDec(sValue)
            Else

                If IsNumeric(sValue) Then
                    Me.Text = CStr(sValue)
                Else
                    Me.Text = String.Empty
                End If
            End If
        End Set
    End Property

    <System.ComponentModel.Browsable(False), _
   System.ComponentModel.DesignerSerializationVisibilityAttribute(System.ComponentModel.DesignerSerializationVisibility.Hidden)> _
    Public Property decimalValue() As Decimal?
        Get
            If IsNumeric(Me.Text) Then
                Return CDec(Me.Text)
            Else
                Return Nothing
            End If
        End Get

        Set(ByVal sValue As Decimal?)

            If sValue.HasValue AndAlso IsNumeric(sValue) Then
                Me.Text = CStr(sValue)
            Else
                Me.Text = String.Empty
            End If

        End Set

    End Property

    Private Sub _KeyPress(ByVal sender As Object, _
                                         ByVal e As System.Windows.Forms.KeyEventArgs) _
                                     Handles MyBase.KeyDown

        If e.KeyCode = Keys.Delete OrElse e.KeyCode = Keys.Back Then
        Else
            e.Handled = Me.decimalKeyPress(e.KeyCode)
        End If


    End Sub

    Private Sub _Validating(ByVal sender As Object, _
                                        ByVal e As System.ComponentModel.CancelEventArgs) _
                                    Handles Me.Validating

        Dim decvalue As Decimal
        If Not String.IsNullOrEmpty(Me.Text) Then
            If Not Decimal.TryParse(Me.Text, decvalue) Then
                e.Cancel = True
                Me.addError(String.Format(WinControlsLocalizer.getString("invalid_decimal"), Me.Label))
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
            Dim compareToResult As Integer = CDec(Me.Text).CompareTo(CDec(Me.MinValue))
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
            Dim compareToResult As Integer = CDec(Me.Text).CompareTo(CDec(Me.MaxValue))
            Return compareToResult = COMPARE_TO_LESS_THAN OrElse _
                       compareToResult = COMPARE_TO_EQUAL
        End If

    End Function

    Private Function decimalKeyPress(ByVal keyCode As Integer) As Boolean

        Dim currentCulture As System.Globalization.CultureInfo = _
                    System.Threading.Thread.CurrentThread.CurrentUICulture

        If Chr(keyCode) = currentCulture.NumberFormat.NumberDecimalSeparator _
             OrElse Chr(keyCode) = currentCulture.NumberFormat.NumberGroupSeparator _
             OrElse Char.IsDigit(Chr(keyCode)) Then

            Return False

        Else
            Return True
        End If

    End Function

End Class
