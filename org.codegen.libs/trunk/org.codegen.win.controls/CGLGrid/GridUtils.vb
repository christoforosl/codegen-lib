Imports System.Reflection.Assembly
Imports org.codegen.win.controls
Imports System.ComponentModel
Imports System.Reflection
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.CompilerServices

Namespace Grid
    Public Class GridUtils

        Public Shared Function toolbarItem(ByVal _img As Image, ByVal _name As String) As ToolStripButton
            Return toolbarItem(_img, _name, ToolStripItemDisplayStyle.ImageAndText, _name)
        End Function

        Public Shared Function toolbarItem(ByVal _img As Image, ByVal _name As String, ByVal _displStyle As ToolStripItemDisplayStyle) As ToolStripButton
            Return toolbarItem(_img, _name, _displStyle, _name)
        End Function

        Public Shared Function toolbarItem(ByVal _img As Image, ByVal _name As String, ByVal _displStyle As ToolStripItemDisplayStyle, ByVal _text As String) As ToolStripButton
            Dim button As New ToolStripButton
            button.Image = _img
            button.ImageTransparentColor = Color.Magenta
            button.Name = _name
            Dim size As New Size(&H41, &H1B)
            button.Size = size
            button.Text = _text
            button.DisplayStyle = _displStyle
            Return button
        End Function

    End Class
End Namespace
