Namespace Forms.Edit

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Class frmEmployeeRankDetails
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

    Friend WithEvents UcEmployeeRank As UcEmployeeRankDetails

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.UcEmployeeRank = New UcEmployeeRankDetails()
        Me.SuspendLayout()
        '
        'UcEmployeeRank
        '
        Me.UcEmployeeRank.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UcEmployeeRank.Location = New System.Drawing.Point(0, 0)
        Me.UcEmployeeRank.Name = "UcEmployeeRank"
        Me.UcEmployeeRank.Size = New System.Drawing.Size(573, 262)
        Me.UcEmployeeRank.TabIndex = 0
        '
        'frmEmployeeRank
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(573, 80)
        Me.Controls.Add(Me.UcEmployeeRank)
        Me.Name = "frmEmployeeRank"
        Me.Text = "frmEmployeeRank"
		
        Me.Controls.SetChildIndex(Me.UcEmployeeRank, 0)
        Me.ResumeLayout(False)

    End Sub

#End Region

#Region "Standard Code"
	
	Public Overrides Sub DeleteData()

		Dim mapper As EmployeeRankDBMapper = New EmployeeRankDBMapper()
		mapper.deleteByKey(Me.IdValue)
		
		'after delete, close the form
        'Note: do not set the dialog result here.  It is handled by the frmBaseEdit, ie do not call Me.DialogResult = Windows.Forms.DialogResult.None
            
    End Sub

	Public Overrides Sub LoadData()
		
		Me.UcEmployeeRank.ModelObject = New EmployeeRankDBMapper().findByKey(  Me.IdValue)
		Me.UcEmployeeRank.loadData()
		Me.setRecordLoadedStatus(Me.IdValue)

	End Sub


	Public Overrides Function SaveData() As Boolean

		if Me.ValidateChildren() then
			Me.UcEmployeeRank.loadToObject
			dim db as New EmployeeRankDBMapper()
			db.save(  Me.UcEmployeeRank.ModelObject )
			return true
		else
			return false
		end if

	End Function
	
	Public Overrides Function dataChanged() As Boolean

		Return Me.UcEmployeeRank.hasChanges

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
		Call Me.UcEmployeeRank.resetLastLoadedValues()

    End Sub
	
#End Region

End Class

End Namespace

