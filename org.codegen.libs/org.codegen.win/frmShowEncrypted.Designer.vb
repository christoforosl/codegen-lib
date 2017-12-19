<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmShowEncrypted
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
        Me.txtEncryptedString = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'txtEncryptedString
        '
        Me.txtEncryptedString.Location = New System.Drawing.Point(14, 13)
        Me.txtEncryptedString.Multiline = True
        Me.txtEncryptedString.Name = "txtEncryptedString"
        Me.txtEncryptedString.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtEncryptedString.Size = New System.Drawing.Size(929, 255)
        Me.txtEncryptedString.TabIndex = 0
        Me.txtEncryptedString.WordWrap = False
        '
        'frmShowEncrypted
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(958, 281)
        Me.Controls.Add(Me.txtEncryptedString)
        Me.Font = New System.Drawing.Font("Courier New", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "frmShowEncrypted"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Encrypted Connection String"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents txtEncryptedString As TextBox
End Class
