<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ucEditToolar
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    Sub New()

        Me.Font = FormsApplicationContext.current.ApplicationDefaultFont
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub
    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.tlStripEdit = New System.Windows.Forms.ToolStrip()
        Me.cmdSave = New System.Windows.Forms.ToolStripButton()
        Me.cmdSaveAs = New System.Windows.Forms.ToolStripButton()
        Me.cmdCancel = New System.Windows.Forms.ToolStripButton()
        Me.sepDeleteAndPrintBtns = New System.Windows.Forms.ToolStripSeparator()
        Me.cmdDelete = New System.Windows.Forms.ToolStripButton()
        Me.cmdPrint = New System.Windows.Forms.ToolStripButton()
        Me.sepNavigation = New System.Windows.Forms.ToolStripSeparator()
        Me.cmdPrevious = New System.Windows.Forms.ToolStripButton()
        Me.cmdNext = New System.Windows.Forms.ToolStripButton()
        Me.sepAddButton = New System.Windows.Forms.ToolStripSeparator()
        Me.cmdAdd = New System.Windows.Forms.ToolStripButton()
        Me.sepCustomItems = New System.Windows.Forms.ToolStripSeparator()
        Me.tlStripEdit.SuspendLayout()
        Me.SuspendLayout()
        '
        'tlStripEdit
        '
        Me.tlStripEdit.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlStripEdit.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmdSave, Me.cmdSaveAs, Me.cmdCancel, Me.sepDeleteAndPrintBtns, Me.cmdDelete, Me.cmdPrint, Me.sepNavigation, Me.cmdPrevious, Me.cmdNext, Me.sepAddButton, Me.cmdAdd, Me.sepCustomItems})
        Me.tlStripEdit.Location = New System.Drawing.Point(0, 0)
        Me.tlStripEdit.Name = "tlStripEdit"
        Me.tlStripEdit.Size = New System.Drawing.Size(601, 27)
        Me.tlStripEdit.TabIndex = 1
        '
        'cmdSave
        '
        Me.cmdSave.Image = Global.org.codegen.win.controls.My.Resources.Resources.saveitem
        Me.cmdSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.Size = New System.Drawing.Size(51, 24)
        Me.cmdSave.Text = "&Save"
        '
        'cmdSaveAs
        '
        Me.cmdSaveAs.Image = Global.org.codegen.win.controls.My.Resources.Resources.SaveAllHS
        Me.cmdSaveAs.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdSaveAs.Name = "cmdSaveAs"
        Me.cmdSaveAs.Size = New System.Drawing.Size(67, 24)
        Me.cmdSaveAs.Text = "Save As"
        '
        'cmdCancel
        '
        Me.cmdCancel.Image = Global.org.codegen.win.controls.My.Resources.Resources.synch
        Me.cmdCancel.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.Size = New System.Drawing.Size(63, 24)
        Me.cmdCancel.Text = "Cancel"
        '
        'sepDeleteAndPrintBtns
        '
        Me.sepDeleteAndPrintBtns.Name = "sepDeleteAndPrintBtns"
        Me.sepDeleteAndPrintBtns.Size = New System.Drawing.Size(6, 27)
        '
        'cmdDelete
        '
        Me.cmdDelete.Image = Global.org.codegen.win.controls.My.Resources.Resources.DeleteHS
        Me.cmdDelete.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdDelete.Name = "cmdDelete"
        Me.cmdDelete.Size = New System.Drawing.Size(60, 24)
        Me.cmdDelete.Text = "Delete"
        '
        'cmdPrint
        '
        Me.cmdPrint.Image = Global.org.codegen.win.controls.My.Resources.Resources.PrintHS
        Me.cmdPrint.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.Size = New System.Drawing.Size(52, 24)
        Me.cmdPrint.Text = "Print"
        '
        'sepNavigation
        '
        Me.sepNavigation.Name = "sepNavigation"
        Me.sepNavigation.Size = New System.Drawing.Size(6, 27)
        '
        'cmdPrevious
        '
        Me.cmdPrevious.Image = Global.org.codegen.win.controls.My.Resources.Resources.GoRtlHS
        Me.cmdPrevious.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdPrevious.Name = "cmdPrevious"
        Me.cmdPrevious.Size = New System.Drawing.Size(72, 24)
        Me.cmdPrevious.Text = "Previous"
        '
        'cmdNext
        '
        Me.cmdNext.Image = Global.org.codegen.win.controls.My.Resources.Resources.GoLtrHS
        Me.cmdNext.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdNext.Name = "cmdNext"
        Me.cmdNext.Size = New System.Drawing.Size(51, 24)
        Me.cmdNext.Text = "Next"
        '
        'sepAddButton
        '
        Me.sepAddButton.Name = "sepAddButton"
        Me.sepAddButton.Size = New System.Drawing.Size(6, 27)
        '
        'cmdAdd
        '
        Me.cmdAdd.Image = Global.org.codegen.win.controls.My.Resources.Resources.AddTableHS
        Me.cmdAdd.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdAdd.Name = "cmdAdd"
        Me.cmdAdd.Size = New System.Drawing.Size(76, 24)
        Me.cmdAdd.Text = "Add New"
        '
        'sepCustomItems
        '
        Me.sepCustomItems.Name = "sepCustomItems"
        Me.sepCustomItems.Size = New System.Drawing.Size(6, 27)
        Me.sepCustomItems.Visible = False
        '
        'ucEditToolar
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.tlStripEdit)
        Me.Name = "ucEditToolar"
        Me.Size = New System.Drawing.Size(601, 27)
        Me.tlStripEdit.ResumeLayout(False)
        Me.tlStripEdit.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents cmdSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents cmdSaveAs As System.Windows.Forms.ToolStripButton
    Friend WithEvents cmdCancel As System.Windows.Forms.ToolStripButton
    Friend WithEvents sepDeleteAndPrintBtns As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents cmdDelete As System.Windows.Forms.ToolStripButton
    Friend WithEvents cmdPrint As System.Windows.Forms.ToolStripButton
    Friend WithEvents cmdNext As System.Windows.Forms.ToolStripButton
    Friend WithEvents cmdPrevious As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlStripEdit As System.Windows.Forms.ToolStrip
    Friend WithEvents sepNavigation As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents sepAddButton As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents cmdAdd As System.Windows.Forms.ToolStripButton
    Friend WithEvents sepCustomItems As System.Windows.Forms.ToolStripSeparator



    Private Sub ucEditToolar_ControlAdded(ByVal sender As Object, _
                                          ByVal e As System.Windows.Forms.ControlEventArgs) Handles Me.ControlAdded
        e.Control.Font = FormsApplicationContext.current.ApplicationDefaultFont
    End Sub
End Class
