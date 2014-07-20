<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmBaseEdit
    Inherits frmBase

    'Form overrides dispose to clean up the component list.
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
        Me.UcEditToolar = New org.codegen.win.controls.ucEditToolar()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.lblEditStatus = New System.Windows.Forms.ToolStripStatusLabel()
        Me.panelControlArea = New System.Windows.Forms.Panel()
        Me.StatusStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'UcEditToolar
        '
        Me.UcEditToolar.Dock = System.Windows.Forms.DockStyle.Top
        Me.UcEditToolar.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UcEditToolar.Location = New System.Drawing.Point(0, 0)
        Me.UcEditToolar.Name = "UcEditToolar"
        Me.UcEditToolar.ShowAdd = True
        Me.UcEditToolar.ShowDelete = True
        Me.UcEditToolar.ShowNavigationButtons = True
        Me.UcEditToolar.ShowPrint = True
        Me.UcEditToolar.ShowSaveAs = True
        Me.UcEditToolar.Size = New System.Drawing.Size(804, 30)
        Me.UcEditToolar.TabIndex = 0
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.lblEditStatus})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 480)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Padding = New System.Windows.Forms.Padding(1, 0, 16, 0)
        Me.StatusStrip1.Size = New System.Drawing.Size(804, 22)
        Me.StatusStrip1.TabIndex = 1
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'lblEditStatus
        '
        Me.lblEditStatus.BackColor = System.Drawing.Color.Transparent
        Me.lblEditStatus.Name = "lblEditStatus"
        Me.lblEditStatus.Size = New System.Drawing.Size(23, 17)
        Me.lblEditStatus.Text = "OK"
        Me.lblEditStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'panelControlArea
        '
        Me.panelControlArea.Dock = System.Windows.Forms.DockStyle.Fill
        Me.panelControlArea.Location = New System.Drawing.Point(0, 30)
        Me.panelControlArea.Name = "panelControlArea"
        Me.panelControlArea.Size = New System.Drawing.Size(804, 450)
        Me.panelControlArea.TabIndex = 2
        '
        'frmBaseEdit
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(804, 502)
        Me.Controls.Add(Me.panelControlArea)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.UcEditToolar)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "frmBaseEdit"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmEditBase"
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Protected WithEvents UcEditToolar As org.codegen.win.controls.ucEditToolar
    Protected WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Protected WithEvents lblEditStatus As System.Windows.Forms.ToolStripStatusLabel
    Protected WithEvents panelControlArea As System.Windows.Forms.Panel

End Class
