Imports System.ComponentModel

Public Class CGTimeBox
    Inherits CGDateTextBox

    <Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
    Public Overrides Property FormatPattern As String = "HH:mm"

    <Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
    Protected Overrides Property errMsgCode As String = "invalid_time"

    Protected Overrides Function dateKeyPress(ByVal KeyAscii As Integer) As Boolean

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

End Class
