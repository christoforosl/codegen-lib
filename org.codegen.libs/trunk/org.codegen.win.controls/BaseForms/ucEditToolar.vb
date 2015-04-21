Public Class ucEditToolar

    Private Sub ucEditToolar_Load(ByVal sender As Object, ByVal e As EventArgs) _
                Handles Me.Load

        If Me.DesignMode Then Exit Sub

        Me.cmdCancel.Text = WinControlsLocalizer.getString("cmdCancel")
        Me.cmdSave.Text = WinControlsLocalizer.getString("cmdSave")
        Me.cmdSaveAs.Text = WinControlsLocalizer.getString("cmdSaveAs")
        Me.cmdNext.Text = WinControlsLocalizer.getString("cmdNext")
        Me.cmdPrevious.Text = WinControlsLocalizer.getString("cmdPrevious")
        Me.cmdPrint.Text = WinControlsLocalizer.getString("cmdPrint")
        Me.cmdDelete.Text = WinControlsLocalizer.getString("cmdDelete")

    End Sub

    Public Property ShowSaveAs As Boolean
        Get
            Return Me.cmdSaveAs.Visible
        End Get
        Set(ByVal value As Boolean)
            Me.cmdSaveAs.Visible = value

        End Set
    End Property

    Public Property ShowAdd As Boolean
        Get
            Return Me.cmdAdd.Visible
        End Get
        Set(ByVal value As Boolean)
            Me.cmdAdd.Visible = value
            Me.sepAddButton.Visible = value
        End Set
    End Property

    Public Property ShowPrint As Boolean
        Get
            Return Me.cmdPrint.Visible
        End Get
        Set(ByVal value As Boolean)
            Me.cmdPrint.Visible = value
            Me.sepDeleteAndPrintBtns.Visible = Me.cmdPrint.Visible AndAlso Me.cmdDelete.Visible
        End Set
    End Property

    Public Property ShowDelete As Boolean
        Get
            Return Me.cmdDelete.Visible
        End Get

        Set(ByVal value As Boolean)
            Me.cmdDelete.Visible = value
            Me.sepDeleteAndPrintBtns.Visible = Me.cmdPrint.Visible AndAlso Me.cmdDelete.Visible
        End Set

    End Property

    Public Property ShowNavigationButtons As Boolean
        Get
            Return Me.cmdPrevious.Visible
        End Get

        Set(ByVal value As Boolean)
            Me.cmdPrevious.Visible = value
            Me.cmdNext.Visible = value
            Me.sepNavigation.Visible = Me.cmdPrevious.Visible AndAlso Me.cmdNext.Visible
        End Set

    End Property

    Public Sub addToolStripItem(ByVal stripItem As ToolStripItem)

        Me.tlStripEdit.Items.Add(stripItem)
        Me.sepCustomItems.Visible = True
    End Sub
    
End Class
