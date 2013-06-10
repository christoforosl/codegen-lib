Namespace Forms.Edit

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Class frmEmployeeDetails
    Inherits org.codegen.win.controls.frmBaseEdit

#Region "Designer"
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

    Friend WithEvents UcEmployee As UcEmployeeDetails

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
            Me.UcEmployee = New ModelLibTestsWinApp.ucEmployeeDetails()
            Me.SuspendLayout()
            '
            'UcEditToolar
            '
            Me.UcEditToolar.ShowAdd = True
            Me.UcEditToolar.ShowDelete = True
            Me.UcEditToolar.ShowNavigationButtons = True
            Me.UcEditToolar.Size = New System.Drawing.Size(668, 30)
            '
            'UcEmployee
            '
            Me.UcEmployee.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
            Me.UcEmployee.Dock = System.Windows.Forms.DockStyle.Fill
            Me.UcEmployee.isInitialized = False
            Me.UcEmployee.Location = New System.Drawing.Point(0, 30)
            Me.UcEmployee.ModelObject = Nothing
            Me.UcEmployee.Name = "UcEmployee"
            Me.UcEmployee.Size = New System.Drawing.Size(668, 343)
            Me.UcEmployee.TabIndex = 0
            '
            'frmEmployeeDetails
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(668, 395)
            Me.Controls.Add(Me.UcEmployee)
            Me.Name = "frmEmployeeDetails"
            Me.Text = "frmEmployeeDetails"
            Me.Controls.SetChildIndex(Me.UcEditToolar, 0)
            Me.Controls.SetChildIndex(Me.UcEmployee, 0)
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub

#End Region

#Region "Standard Code"
	
	Public Overrides Sub DeleteData()

		Dim mapper As EmployeeDBMapper = New EmployeeDBMapper()
		mapper.deleteByKey(Me.IdValue)
		
		'after delete, close the form
        'Note: do not set the dialog result here.  It is handled by the frmBaseEdit, ie do not call Me.DialogResult = Windows.Forms.DialogResult.None
            
    End Sub

	Public Overrides Sub LoadData()
		
		Me.UcEmployee.ModelObject = New EmployeeDBMapper().findByKey(  Me.IdValue)
		Me.UcEmployee.loadData()
		Me.setRecordLoadedStatus(Me.IdValue)

	End Sub


	Public Overrides Function SaveData() As enumSaveDataResult

		if Me.ValidateChildren() then
			Me.UcEmployee.loadToObject
			dim db as New EmployeeDBMapper()
			db.save(  Me.UcEmployee.ModelObject )
			return enumSaveDataResult.SAVE_SUCESS_AND_CLOSE
		else
			return enumSaveDataResult.SAVE_FAIL
		end if

	End Function
	
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

