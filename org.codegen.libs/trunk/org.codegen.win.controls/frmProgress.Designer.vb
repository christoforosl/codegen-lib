<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmProgress
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
        Me.ProgressBar = New System.Windows.Forms.ProgressBar()
        Me.bwProgress = New System.ComponentModel.BackgroundWorker()
        Me.lblMessage = New System.Windows.Forms.Label()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'ProgressBar
        '
        Me.ProgressBar.Location = New System.Drawing.Point(12, 56)
        Me.ProgressBar.Name = "ProgressBar"
        Me.ProgressBar.Size = New System.Drawing.Size(402, 23)
        Me.ProgressBar.TabIndex = 0
        '
        'bwProgress
        '
        Me.bwProgress.WorkerReportsProgress = True
        Me.bwProgress.WorkerSupportsCancellation = True
        '
        'lblMessage
        '
        Me.lblMessage.BackColor = System.Drawing.Color.Yellow
        Me.lblMessage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblMessage.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(161, Byte))
        Me.lblMessage.Location = New System.Drawing.Point(11, 9)
        Me.lblMessage.Name = "lblMessage"
        Me.lblMessage.Size = New System.Drawing.Size(402, 36)
        Me.lblMessage.TabIndex = 1
        Me.lblMessage.Text = "Label1"
        Me.lblMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(169, 105)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(88, 23)
        Me.btnCancel.TabIndex = 2
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'frmProgress
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(426, 151)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.lblMessage)
        Me.Controls.Add(Me.ProgressBar)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmProgress"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmProgress"
        Me.TopMost = True
        Me.ResumeLayout(False)

    End Sub
    Public WithEvents ProgressBar As System.Windows.Forms.ProgressBar
    Public WithEvents lblMessage As System.Windows.Forms.Label
    Public WithEvents btnCancel As System.Windows.Forms.Button
    Private WithEvents bwProgress As System.ComponentModel.BackgroundWorker
End Class
