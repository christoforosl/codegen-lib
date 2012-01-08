Imports System.ComponentModel

Public Class CGDecimalTextBox
    Inherits CGIntTextBox

    <Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
    Public Overrides Property value() As Object
        Get
            If IsNumeric(Me.Text) Then
                Return CDec(Me.Text)
            Else
                Return 0
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

    Private Sub _KeyPress(ByVal sender As Object, _
                                         ByVal e As System.Windows.Forms.KeyPressEventArgs) _
                                     Handles MyBase.KeyPress


        e.Handled = Me.decimalKeyPress(Asc(e.KeyChar))

    End Sub

    Private Sub _Validating(ByVal sender As Object, _
                                        ByVal e As System.ComponentModel.CancelEventArgs) _
                                    Handles Me.Validating

        Dim decvalue As Decimal
        If Not String.IsNullOrEmpty(Me.Text) Then
            If Not Decimal.TryParse(Me.Text, decvalue) Then
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
            Dim compareToResult As Integer = CDec(Me.Text).CompareTo(CDec(Me.MinValue))
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
