<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmGeneratedFiles
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.lstGenFiles = New System.Windows.Forms.ListBox
        Me.SuspendLayout()
        '
        'lstGenFiles
        '
        Me.lstGenFiles.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lstGenFiles.FormattingEnabled = True
        Me.lstGenFiles.Location = New System.Drawing.Point(12, 16)
        Me.lstGenFiles.Name = "lstGenFiles"
        Me.lstGenFiles.Size = New System.Drawing.Size(497, 407)
        Me.lstGenFiles.TabIndex = 0
        '
        'frmGeneratedFiles
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(521, 447)
        Me.Controls.Add(Me.lstGenFiles)
        Me.Name = "frmGeneratedFiles"
        Me.Text = "Generated Files"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lstGenFiles As System.Windows.Forms.ListBox
End Class
