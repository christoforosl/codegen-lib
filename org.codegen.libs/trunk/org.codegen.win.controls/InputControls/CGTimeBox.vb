Imports System.ComponentModel

Public Class CGTimeBox
    Inherits CGDateTextBox

    Private Const TIME_COMPONENT_HOURS As String = "h"
    Private Const TIME_COMPONENT_MINUTES As String = "m"
    Private Const TIME_COMPONENT_AM_OR_PM As String = "ap"
    Private Const STR_SEMI_COLON As String = ":"
    Private Const STR_SPACE As String = " "
    Private Const STR_DOT As String = "."
    Private Const STR_DASH As String = "-"
    Private Const STR_0 As String = "0"
    Private Const STR_9 As String = "9"

    <Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
    Public Overrides Property FormatPattern As String = "HH:mm"

    <Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
    Protected Overrides Property errMsgCode As String = "invalid_time"

    Sub New()
        Me.MaxLength = 5
    End Sub
    Protected Overrides Function dateKeyPress(ByVal KeyAscii As Integer) As Boolean
        'false: allow key
        'true: do not allow key

        If KeyAscii = 58 Then
            'this is ":"
            Return False

        ElseIf KeyAscii >= 48 And KeyAscii <= 57 Then
            Return False

        ElseIf KeyAscii = 8 Then
            Return False

        Else
            'MsgBox("rejected ascii code:" & KeyAscii)
            Return (True)
        End If

    End Function

    Protected Overrides Sub OnLostFocus(ByVal e As System.EventArgs)

        If parseTime(Me.Text) IsNot Nothing Then
            Me.Text = Format(parseTime(Me.Text), FormatPattern)
        End If

        MyBase.OnLostFocus(e)

    End Sub
    Protected Overrides Sub OnValidating(ByVal e As System.ComponentModel.CancelEventArgs)

        If String.IsNullOrEmpty(Me.Text.Trim) = False Then
            If parseTime(Me.Text) IsNot Nothing Then
                Me.Text = Format(parseTime(Me.Text), FormatPattern)
            Else
                Me.addError("Invalid time value entered in text box:" & Me.Label)
                e.Cancel = True
                Return
            End If
        End If

        MyBase.OnValidating(e)

    End Sub

    
    Public Shared Function parseTime(ByVal tm As String) As DateTime?

        Dim hs As String = String.Empty
        Dim ms As String = String.Empty
        Dim amOrPm As String = String.Empty
        Dim timeComponent As String = TIME_COMPONENT_HOURS
        Dim c As String = String.Empty

        If String.IsNullOrEmpty(tm) Then Return Nothing



        If (tm.Length = 4 AndAlso tm.IndexOf(STR_SEMI_COLON) = -1 AndAlso _
                    tm.IndexOf(STR_SPACE) = -1 AndAlso _
                    tm.IndexOf(STR_DOT) = -1 AndAlso _
                    tm.IndexOf(STR_DASH) = -1) Then

            hs = tm.Substring(0, 2)
            ms = tm.Substring(2)

        Else
            'alert('here')
            For i As Integer = 0 To tm.Length - 1
                c = tm.Substring(i, 1)
                c = c.ToUpper

                If (c >= STR_0 AndAlso c <= STR_9) Then
                    If (timeComponent = TIME_COMPONENT_HOURS) Then
                        hs = hs & c
                    ElseIf (timeComponent = TIME_COMPONENT_MINUTES) Then
                        ms = ms & c
                    End If

                ElseIf (c = "A" OrElse c = "P" OrElse c = "M") Then

                    If (timeComponent = TIME_COMPONENT_AM_OR_PM) Then
                        amOrPm = amOrPm & c

                    Else

                        If (c = "A" OrElse c = "P") Then
                            timeComponent = TIME_COMPONENT_AM_OR_PM
                            amOrPm = c & "M"
                            Exit For

                        Else
                            Return Nothing
                        End If
                    End If

                ElseIf (c = STR_SEMI_COLON OrElse c = STR_DOT OrElse c = STR_SPACE OrElse c = STR_DASH) Then
                    If (timeComponent = TIME_COMPONENT_HOURS) Then
                        timeComponent = TIME_COMPONENT_MINUTES
                    ElseIf (timeComponent = TIME_COMPONENT_MINUTES) Then
                        timeComponent = TIME_COMPONENT_AM_OR_PM
                    Else
                        Return Nothing
                    End If
                End If
            Next
        End If

        If (amOrPm = String.Empty) Then amOrPm = "AM"

        If (hs.Length < 1 OrElse hs.Length > 2 OrElse ms.Length > 2 OrElse amOrPm.Length <> 2) Then
            Return Nothing
        End If

        If (ms.Length = 0) Then
            ms = STR_0
        End If

        amOrPm = amOrPm.ToUpper()

        If (ms.Length = 2) Then
            If (CInt(ms) > 59 OrElse CInt(ms) < 0) Then
                Return Nothing
            End If
        End If


        If (CInt(hs) > 24 OrElse CInt(hs) < 0) Then
            Return Nothing
        Else
            If (CInt(hs) > 12) AndAlso amOrPm = "PM" Then
                Return Nothing
            End If


            If (CInt(hs) > 12) Then

            Else
                If (amOrPm = "PM") Then
                    hs = CStr(CInt(hs) + 12)
                End If
            End If
        End If


        Dim st As Date = TimeSerial(CInt(hs), CInt(ms), 0)

        Return st

    End Function

End Class
