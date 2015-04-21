<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTimeControlTest
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
        Me.components = New System.ComponentModel.Container()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.CgTimeBox2 = New org.codegen.win.controls.CGTimeBox()
        Me.CgTimeBox1 = New org.codegen.win.controls.CGTimeBox()
        Me.CgDateTextBox1 = New org.codegen.win.controls.CGDateTextBox(Me.components)
        Me.Label3 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(71, 24)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(81, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Time Control"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(11, 51)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(141, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "MandatoryTime Control"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'CgTimeBox2
        '
        Me.CgTimeBox2.AssociatedLabel = Me.Label2
        Me.CgTimeBox2.BackColor = System.Drawing.Color.LightYellow
        Me.CgTimeBox2.DateValue = Nothing
        Me.CgTimeBox2.ErrProvider = Nothing
        Me.CgTimeBox2.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))

        Me.CgTimeBox2.isMandatory = True
        Me.CgTimeBox2.Location = New System.Drawing.Point(158, 48)
        Me.CgTimeBox2.MaxValue = Nothing
        Me.CgTimeBox2.MinValue = Nothing
        Me.CgTimeBox2.Name = "CgTimeBox2"
        Me.CgTimeBox2.Size = New System.Drawing.Size(100, 21)
        Me.CgTimeBox2.TabIndex = 2
        '
        'CgTimeBox1
        '
        Me.CgTimeBox1.AssociatedLabel = Me.Label1
        Me.CgTimeBox1.BackColor = System.Drawing.Color.Transparent
        Me.CgTimeBox1.DateValue = Nothing
        Me.CgTimeBox1.ErrProvider = Nothing
        Me.CgTimeBox1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))

        Me.CgTimeBox1.isMandatory = False
        Me.CgTimeBox1.Location = New System.Drawing.Point(158, 21)
        Me.CgTimeBox1.MaxValue = Nothing
        Me.CgTimeBox1.MinValue = Nothing
        Me.CgTimeBox1.Name = "CgTimeBox1"
        Me.CgTimeBox1.Size = New System.Drawing.Size(100, 21)
        Me.CgTimeBox1.TabIndex = 0
        '
        'CgDateTextBox1
        '
        Me.CgDateTextBox1.AssociatedLabel = Me.Label3
        Me.CgDateTextBox1.DateValue = Nothing
        Me.CgDateTextBox1.ErrProvider = Nothing
        Me.CgDateTextBox1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))

        Me.CgDateTextBox1.isMandatory = False
        Me.CgDateTextBox1.Location = New System.Drawing.Point(158, 76)
        Me.CgDateTextBox1.MaxValue = Nothing
        Me.CgDateTextBox1.MinValue = Nothing
        Me.CgDateTextBox1.Name = "CgDateTextBox1"
        Me.CgDateTextBox1.Size = New System.Drawing.Size(166, 21)
        Me.CgDateTextBox1.TabIndex = 4
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(71, 79)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(80, 13)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Date Control"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'frmTimeControlTest
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(375, 262)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.CgDateTextBox1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.CgTimeBox2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.CgTimeBox1)
        Me.Name = "frmTimeControlTest"
        Me.Text = "frmTimeControlTest"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents CgTimeBox1 As org.codegen.win.controls.CGTimeBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents CgTimeBox2 As org.codegen.win.controls.CGTimeBox
    Friend WithEvents CgDateTextBox1 As org.codegen.win.controls.CGDateTextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
End Class
