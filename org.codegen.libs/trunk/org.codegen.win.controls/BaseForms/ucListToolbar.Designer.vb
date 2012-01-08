<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ucListToolbar
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ucListToolbar))
        Me.tlStripList = New System.Windows.Forms.ToolStrip()
        Me.cmdAdd = New System.Windows.Forms.ToolStripButton()
        Me.cmdEdit = New System.Windows.Forms.ToolStripButton()
        Me.cmdDelete = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.cmdPrint = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.cmdExcel = New System.Windows.Forms.ToolStripButton()
        Me.cmdChooseGridFields = New System.Windows.Forms.ToolStripButton()
        Me.cmdSelectAndClose = New System.Windows.Forms.ToolStripButton()
        Me.cmdCancel = New System.Windows.Forms.ToolStripButton()
        Me.tlStripList.SuspendLayout()
        Me.SuspendLayout()
        '
        'tlStripList
        '
        Me.tlStripList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlStripList.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmdSelectAndClose, Me.cmdCancel, Me.cmdAdd, Me.cmdEdit, Me.cmdDelete, Me.ToolStripSeparator1, Me.cmdPrint, Me.ToolStripSeparator2, Me.cmdExcel, Me.cmdChooseGridFields})
        Me.tlStripList.Location = New System.Drawing.Point(0, 0)
        Me.tlStripList.Name = "tlStripList"
        Me.tlStripList.Size = New System.Drawing.Size(666, 23)
        Me.tlStripList.TabIndex = 0
        Me.tlStripList.Text = "tlStripList"
        '
        'cmdAdd
        '
        Me.cmdAdd.Image = Global.org.codegen.win.controls.My.Resources.Resources.AddTableHS
        Me.cmdAdd.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdAdd.Name = "cmdAdd"
        Me.cmdAdd.Size = New System.Drawing.Size(51, 20)
        Me.cmdAdd.Text = "New"
        '
        'cmdEdit
        '
        Me.cmdEdit.Image = Global.org.codegen.win.controls.My.Resources.Resources.EditTableHS
        Me.cmdEdit.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdEdit.Name = "cmdEdit"
        Me.cmdEdit.Size = New System.Drawing.Size(47, 20)
        Me.cmdEdit.Text = "Edit"
        '
        'cmdDelete
        '
        Me.cmdDelete.Image = Global.org.codegen.win.controls.My.Resources.Resources.DeleteHS
        Me.cmdDelete.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdDelete.Name = "cmdDelete"
        Me.cmdDelete.Size = New System.Drawing.Size(60, 20)
        Me.cmdDelete.Text = "Delete"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 23)
        '
        'cmdPrint
        '
        Me.cmdPrint.Image = Global.org.codegen.win.controls.My.Resources.Resources.PrintHS
        Me.cmdPrint.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.Size = New System.Drawing.Size(52, 20)
        Me.cmdPrint.Text = "Print"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 23)
        '
        'cmdExcel
        '
        Me.cmdExcel.Image = Global.org.codegen.win.controls.My.Resources.Resources.toExcel
        Me.cmdExcel.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdExcel.Name = "cmdExcel"
        Me.cmdExcel.Size = New System.Drawing.Size(70, 20)
        Me.cmdExcel.Text = "To Excel"
        '
        'cmdChooseGridFields
        '
        Me.cmdChooseGridFields.Image = Global.org.codegen.win.controls.My.Resources.Resources.synch
        Me.cmdChooseGridFields.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdChooseGridFields.Name = "cmdChooseGridFields"
        Me.cmdChooseGridFields.Size = New System.Drawing.Size(118, 20)
        Me.cmdChooseGridFields.Text = "Choose Columns"
        '
        'cmdSelectAndClose
        '
        Me.cmdSelectAndClose.Image = CType(resources.GetObject("cmdSelectAndClose.Image"), System.Drawing.Image)
        Me.cmdSelectAndClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdSelectAndClose.Name = "cmdSelectAndClose"
        Me.cmdSelectAndClose.Size = New System.Drawing.Size(115, 20)
        Me.cmdSelectAndClose.Text = "Select And Close"
        '
        'cmdCancel
        '
        Me.cmdCancel.Image = Global.org.codegen.win.controls.My.Resources.Resources.synch
        Me.cmdCancel.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.Size = New System.Drawing.Size(63, 20)
        Me.cmdCancel.Text = "Cancel"
        '
        'ucListToolbar
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.tlStripList)
        Me.Name = "ucListToolbar"
        Me.Size = New System.Drawing.Size(666, 23)
        Me.tlStripList.ResumeLayout(False)
        Me.tlStripList.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents cmdAdd As System.Windows.Forms.ToolStripButton
    Friend WithEvents cmdEdit As System.Windows.Forms.ToolStripButton
    Friend WithEvents cmdDelete As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents cmdPrint As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents cmdExcel As System.Windows.Forms.ToolStripButton
    Friend WithEvents cmdChooseGridFields As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlStripList As System.Windows.Forms.ToolStrip
    Friend WithEvents cmdSelectAndClose As System.Windows.Forms.ToolStripButton
    Friend WithEvents cmdCancel As System.Windows.Forms.ToolStripButton

End Class
