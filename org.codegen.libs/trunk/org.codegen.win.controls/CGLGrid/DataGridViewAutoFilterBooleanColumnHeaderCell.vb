Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Diagnostics
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports System.Windows.Forms.VisualStyles
Imports System.Collections
Imports System.Reflection


''' <summary>
''' Used to show a YES/No drop-down filter list in a DataGridViewColumnHeaderCell.
''' </summary>
Public Class DataGridViewAutoFilterBooleanColumnHeaderCell
    Inherits DataGridViewAutoFilterColumnHeaderCell

    Public Overrides Function GetFormattedValue2(ByVal value As Object) As String
        Dim formattedValue As String = "NO"

        If IsNumeric(value) AndAlso CInt(value) = 1 Then
            formattedValue = "YES"

        ElseIf TypeOf (value) Is System.String AndAlso CStr(value) = "Y" Then
            formattedValue = "YES"

        End If

        Return formattedValue

    End Function

End Class
