Namespace Forms.Edit

    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Public Class frmEmployeeDetails2
        Inherits org.codegen.win.controls.frmBaseModelObjectEdit

#Region "Designer"

        Sub New()
            Me.IdValue = 1
            Me.ModelObjectType = GetType(Employee)
            ' This call is required by the designer.
            InitializeComponent()

            ' Add any initialization after the InitializeComponent() call.

        End Sub
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

        Friend WithEvents UcEmployee As ucEmployeeDetails

        'Required by the Windows Form Designer
        Private components As System.ComponentModel.IContainer

        'NOTE: The following procedure is required by the Windows Form Designer
        'It can be modified using the Windows Form Designer.  
        'Do not modify it using the code editor.
        <System.Diagnostics.DebuggerStepThrough()> _
        Private Sub InitializeComponent()
            Me.UcEmployee = New ucEmployeeDetails()
            Me.SuspendLayout()
            '
            'UcEmployee
            '
            Me.UcEmployee.Dock = System.Windows.Forms.DockStyle.Fill
            Me.UcEmployee.Location = New System.Drawing.Point(0, 0)
            Me.UcEmployee.Name = "UcEmployee"
            Me.UcEmployee.Size = New System.Drawing.Size(573, 262)
            Me.UcEmployee.TabIndex = 0
            '
            'frmEmployee
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(573, 395)
            Me.Controls.Add(Me.UcEmployee)
            Me.Name = "frmEmployeeDetails"
            Me.Text = "frmEmployeeDetails"

            Me.Controls.SetChildIndex(Me.UcEmployee, 0)
            Me.ResumeLayout(False)

        End Sub

#End Region

#Region "Standard Code"

        Public Overrides Function dataChanged() As Boolean

            Return Me.UcEmployee.hasChanges

        End Function

        ''' <summary>
        ''' On the load, we need to call "resetLastLoadedValues"
        ''' at the **end** of the method.  This call keeps track of the 
        ''' loaded data on the page, for the 'datachanged' functionality
        ''' </summary>
        Private Sub frm_Load(ByVal sender As Object, _
                                ByVal e As System.EventArgs) Handles Me.Load

            '**** any code should go before "resetLastLoadedValues"


            '***** IMPORTANT: keep this call at the end of the procedure
            Call Me.UcEmployee.resetLastLoadedValues()

        End Sub

#End Region

    End Class

End Namespace

