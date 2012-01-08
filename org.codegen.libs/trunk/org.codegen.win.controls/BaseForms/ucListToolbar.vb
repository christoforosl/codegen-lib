Public Class ucListToolbar

    Public Sub disableAdd()
        Me.cmdAdd.Enabled = False
    End Sub

    Public Sub disableDelete()
        Me.cmdDelete.Enabled = False
    End Sub

    Public Sub disableEdit()
        Me.cmdEdit.Enabled = False
    End Sub

    Public Sub hideAdd()
        Me.cmdAdd.Visible = False
    End Sub

    Public Sub hideDelete()
        Me.cmdDelete.Visible = False
    End Sub

    Public Sub hideEdit()
        Me.cmdEdit.Visible = False
    End Sub

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

    Private Sub ucListToolar_Load(ByVal sender As Object, ByVal e As EventArgs)

        Me.cmdAdd.Text = WinControlsLocalizer.getString("cmdAdd")
        Me.cmdEdit.Text = WinControlsLocalizer.getString("cmdEdit")
        Me.cmdExcel.Text = WinControlsLocalizer.getString("cmdExcel")
        Me.cmdPrint.Text = WinControlsLocalizer.getString("cmdHideShowFilter")
        Me.cmdChooseGridFields.Text = WinControlsLocalizer.getString("cmdChooseGridFields")
        Me.cmdDelete.Text = WinControlsLocalizer.getString("cmdDelete")


    End Sub

End Class
