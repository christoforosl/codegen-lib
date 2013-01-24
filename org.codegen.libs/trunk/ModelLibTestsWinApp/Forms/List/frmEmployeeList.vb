Namespace Forms.List
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Class frmEmployeeList
    Inherits frmBaseGrid

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
     
	 Friend WithEvents ucEmployeeList As ucEmployeeList

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
            
			Me.ucEmployeeList = New ucEmployeeList()
            Me.pnlGrid.SuspendLayout()
            Me.SuspendLayout()
            '
            'pnlGrid
            '
            Me.pnlGrid.Controls.Add(Me.ucEmployeeList)
            Me.pnlGrid.Size = New System.Drawing.Size(760, 395)
            '
            'ucEmployeeList
            '
            Me.ucEmployeeList.Dock = System.Windows.Forms.DockStyle.Fill
            Me.ucEmployeeList.Location = New System.Drawing.Point(0, 0)
            Me.ucEmployeeList.Name = "ucEmployeeList"
            Me.ucEmployeeList.Size = New System.Drawing.Size(760, 395)
            Me.ucEmployeeList.TabIndex = 0
            '
            'frmEmployeeList
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(760, 442)
            Me.ShowConfigButton = True
            Me.ShowExcelButton = True
            Me.ShowPrintButton = True
            Me.ShowSearch = True
            Me.Name = "frmEmployeeList"
            Me.Text = "frmEmployeeList"
            Me.Controls.SetChildIndex(Me.pnlGrid, 0)
            Me.pnlGrid.ResumeLayout(False)
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub

#End Region

#Region "Standard Code"
	
	Private Sub frmEmployeeList_Load(ByVal sender As Object, ByVal e As System.EventArgs) _
				Handles Me.Load

		
        Me.grdData.loadGrid()

    End Sub

	''' <summary>
	''' This function is common to all forms that inherit from class frmBaseGrid
	''' It priovides a common name to the underlying grid control that shows the records
	''' </summary>
	Public Overrides Function grdData() As org.codegen.win.controls.Grid.CGBaseGrid
        return me.ucEmployeeList.grdEmployee
    End Function

        Protected Sub DeleteRecordConfirmed(ByVal pkval As Integer) Handles Me.gridDeleteRecordConfirmed

            Dim m As New EmployeeDBMapper
            Dim mo As Employee = m.findByKey(pkval)
            Call m.delete(mo)

        End Sub

#End Region

End Class

End Namespace 

