Namespace Forms.Edit
'EditFormTemplate.txt
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Class frmProjectDetails
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

    Friend WithEvents UcProject As UcProjectDetails

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.UcProject = New UcProjectDetails()
        Me.SuspendLayout()
        '
        'UcProject
        '
        Me.UcProject.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcProject.Location = New System.Drawing.Point(0, 0)
        Me.UcProject.Name = "UcProject"
        Me.UcProject.Size = New System.Drawing.Size(573, 262)
        Me.UcProject.TabIndex = 0
        '
        'frmProject
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(573, 115)
        Me.Controls.Add(Me.UcProject)
        Me.Name = "frmProjectDetails"
        Me.Text = "frmProjectDetails"
		
        Me.Controls.SetChildIndex(Me.UcProject, 0)
        Me.ResumeLayout(False)

    End Sub

#End Region

#Region "Standard Code"
	
	Public Overrides Sub DeleteData()

		Dim mapper As ProjectDBMapper = New ProjectDBMapper()
		mapper.deleteByKey(Me.IdValue)
		
		'after delete, close the form
        'Note: do not set the dialog result here.  It is handled by the frmBaseEdit, ie do not call Me.DialogResult = Windows.Forms.DialogResult.None
            
    End Sub

	Public Overrides Sub LoadData()
		
		Me.UcProject.ModelObject = New ProjectDBMapper().findByKey(  Me.IdValue)
		Me.UcProject.loadData()
		Me.setRecordLoadedStatus(Me.IdValue)

	End Sub


	Public Overrides Function SaveData() As enumSaveDataResult

		if Me.ValidateChildren() then
			Me.UcProject.loadToObject
			dim db as New ProjectDBMapper()
			db.save(  Me.UcProject.ModelObject )
			return enumSaveDataResult.SAVE_SUCESS_AND_CLOSE
		else
			return enumSaveDataResult.SAVE_FAIL
		end if

	End Function
	
	Public Overrides Function dataChanged() As Boolean

		Return Me.UcProject.hasChanges

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
		Call Me.UcProject.resetLastLoadedValues()

    End Sub
	
#End Region

End Class

End Namespace

