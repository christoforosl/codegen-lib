<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmProgress
    Inherits System.Windows.Forms.Form


    Private Const CP_NOCLOSE_BUTTON As Integer = &H200
    Protected Overrides ReadOnly Property CreateParams() As CreateParams
        Get
            Dim myCp As CreateParams = MyBase.CreateParams
            myCp.ClassStyle = myCp.ClassStyle Or CP_NOCLOSE_BUTTON
            Return myCp

        End Get
    End Property

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
        Me.lblMessage = New System.Windows.Forms.Label()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.backroundWorkerProgress = New System.ComponentModel.BackgroundWorker()
        Me.lblPercentage = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'ProgressBar
        '
        Me.ProgressBar.Location = New System.Drawing.Point(13, 56)
        Me.ProgressBar.Name = "ProgressBar"
        Me.ProgressBar.Size = New System.Drawing.Size(402, 23)
        Me.ProgressBar.TabIndex = 0
        '
        'lblMessage
        '
        Me.lblMessage.BackColor = System.Drawing.Color.Yellow
        Me.lblMessage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblMessage.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(161, Byte))
        Me.lblMessage.Location = New System.Drawing.Point(12, 9)
        Me.lblMessage.Name = "lblMessage"
        Me.lblMessage.Size = New System.Drawing.Size(402, 36)
        Me.lblMessage.TabIndex = 1
        Me.lblMessage.Text = "Label1"
        Me.lblMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(152, 110)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(124, 29)
        Me.btnCancel.TabIndex = 2
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'backroundWorkerProgress
        '
        Me.backroundWorkerProgress.WorkerReportsProgress = True
        Me.backroundWorkerProgress.WorkerSupportsCancellation = True
        '
        'lblPercentage
        '
        Me.lblPercentage.BackColor = System.Drawing.Color.Transparent
        Me.lblPercentage.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblPercentage.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(161, Byte))
        Me.lblPercentage.Location = New System.Drawing.Point(166, 79)
        Me.lblPercentage.Name = "lblPercentage"
        Me.lblPercentage.Size = New System.Drawing.Size(100, 23)
        Me.lblPercentage.TabIndex = 3
        Me.lblPercentage.Text = "100%"
        Me.lblPercentage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'frmProgress
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(426, 151)
        Me.Controls.Add(Me.lblPercentage)
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
        Me.Text = "Progress Bar"
        Me.TopMost = True
        Me.ResumeLayout(False)

    End Sub
    Public WithEvents ProgressBar As System.Windows.Forms.ProgressBar
    Public WithEvents lblMessage As System.Windows.Forms.Label
    Public WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents backroundWorkerProgress As System.ComponentModel.BackgroundWorker
    Friend WithEvents lblPercentage As System.Windows.Forms.Label
End Class
