Imports System.ComponentModel

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Class ucEmployeeProjectList
    Inherits System.Windows.Forms.UserControl

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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()

			me.EmployeeProjectId = new DataGridViewTextBoxColumn
	me.EPEmployeeId = new DataGridViewTextBoxColumn
	me.EPProjectId = new DataGridViewTextBoxColumn
	me.AssignDate = new DataGridViewCalendarColumn
	me.EndDate = new DataGridViewCalendarColumn
	me.Rate = new DataGridViewTextBoxColumn

        Me.grdEmployeeProject = New org.codegen.win.controls.Grid.CGSQLGrid()
        Me.SuspendLayout()
        '
        'grdEmployeeProject
        '
        Me.grdEmployeeProject.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdEmployeeProject.gpEditForm = "Forms.Edit.frmEmployeeProjectDetails"
        Me.grdEmployeeProject.gpSelectFields = "*"
        Me.grdEmployeeProject.gpSelectFrom = "EmployeeProject"
        
        Me.grdEmployeeProject.GridColumnProvider = Nothing
        Me.grdEmployeeProject.gpKeyColumnName = "EmployeeProjectId"
        Me.grdEmployeeProject.Location = New System.Drawing.Point(0, 0)
        Me.grdEmployeeProject.Name = "grdEmployeeProject"
        Me.grdEmployeeProject.Size = New System.Drawing.Size(573, 262)
        Me.grdEmployeeProject.TabIndex = 0

			' column: EmployeeProjectId
	EmployeeProjectId.CellTemplate = New DataGridViewTextBoxCell
	EmployeeProjectId.Name = "EmployeeProjectId"
	EmployeeProjectId.DataPropertyName = "EmployeeProjectId"
	EmployeeProjectId.ReadOnly = True
	EmployeeProjectId.HeaderText = "EmployeeProjectId"
	EmployeeProjectId.HeaderCell.value = "EmployeeProjectId"
	EmployeeProjectId.DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight
	EmployeeProjectId.HeaderCell.Style.Alignment = DataGridViewContentAlignment.TopRight
	EmployeeProjectId.Width = 50
	EmployeeProjectId.Visible = False
	'**** End Setup of column: EmployeeProjectId


	' column: EPEmployeeId
	EPEmployeeId.CellTemplate = New DataGridViewTextBoxCell
	EPEmployeeId.Name = "EPEmployeeId"
	EPEmployeeId.DataPropertyName = "EPEmployeeId"
	EPEmployeeId.ReadOnly = True
	EPEmployeeId.HeaderText = "EPEmployeeId"
	EPEmployeeId.HeaderCell.value = "EPEmployeeId"
	EPEmployeeId.DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight
	EPEmployeeId.HeaderCell.Style.Alignment = DataGridViewContentAlignment.TopRight
	EPEmployeeId.Width = 50
	EPEmployeeId.Visible = True
	'**** End Setup of column: EPEmployeeId


	' column: EPProjectId
	EPProjectId.CellTemplate = New DataGridViewTextBoxCell
	EPProjectId.Name = "EPProjectId"
	EPProjectId.DataPropertyName = "EPProjectId"
	EPProjectId.ReadOnly = True
	EPProjectId.HeaderText = "EPProjectId"
	EPProjectId.HeaderCell.value = "EPProjectId"
	EPProjectId.DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight
	EPProjectId.HeaderCell.Style.Alignment = DataGridViewContentAlignment.TopRight
	EPProjectId.Width = 50
	EPProjectId.Visible = True
	'**** End Setup of column: EPProjectId


	' column: AssignDate
	AssignDate.CellTemplate = New CalendarCell
	AssignDate.Name = "AssignDate"
	AssignDate.DataPropertyName = "AssignDate"
	AssignDate.ReadOnly = True
	AssignDate.HeaderText = "AssignDate"
	AssignDate.HeaderCell.value = "AssignDate"
	AssignDate.DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft
	AssignDate.HeaderCell.Style.Alignment = DataGridViewContentAlignment.TopLeft
	AssignDate.Width = 100
	AssignDate.Visible = True
	AssignDate.CellTemplate.Style.Format = "d"
	'**** End Setup of column: AssignDate


	' column: EndDate
	EndDate.CellTemplate = New CalendarCell
	EndDate.Name = "EndDate"
	EndDate.DataPropertyName = "EndDate"
	EndDate.ReadOnly = True
	EndDate.HeaderText = "EndDate"
	EndDate.HeaderCell.value = "EndDate"
	EndDate.DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft
	EndDate.HeaderCell.Style.Alignment = DataGridViewContentAlignment.TopLeft
	EndDate.Width = 100
	EndDate.Visible = True
	EndDate.CellTemplate.Style.Format = "d"
	'**** End Setup of column: EndDate


	' column: Rate
	Rate.CellTemplate = New DataGridViewTextBoxCell
	Rate.Name = "Rate"
	Rate.DataPropertyName = "Rate"
	Rate.ReadOnly = True
	Rate.HeaderText = "Rate"
	Rate.HeaderCell.value = "Rate"
	Rate.DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight
	Rate.HeaderCell.Style.Alignment = DataGridViewContentAlignment.TopRight
	Rate.Width = 50
	Rate.Visible = True
	Rate.CellTemplate.Style.Format = "C"
	'**** End Setup of column: Rate




        '
        'ucListEmployeeProject
        '
		Me.Dock=DockStyle.Fill
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(573, 262)
        Me.Controls.Add(Me.grdEmployeeProject)
			me.grdEmployeeProject.Columns.Add(EmployeeProjectId)
	me.grdEmployeeProject.Columns.Add(EPEmployeeId)
	me.grdEmployeeProject.Columns.Add(EPProjectId)
	me.grdEmployeeProject.Columns.Add(AssignDate)
	me.grdEmployeeProject.Columns.Add(EndDate)
	me.grdEmployeeProject.Columns.Add(Rate)

        Me.Name = "ucEmployeeProjectList"
        Me.Text = "ucEmployeeProjectList"
        Me.ResumeLayout(False)

    End Sub

    public WithEvents grdEmployeeProject As org.codegen.win.controls.Grid.CGSQLGrid
		Private WithEvents EmployeeProjectId As DataGridViewTextBoxColumn
	Private WithEvents EPEmployeeId As DataGridViewTextBoxColumn
	Private WithEvents EPProjectId As DataGridViewTextBoxColumn
	Private WithEvents AssignDate As DataGridViewCalendarColumn
	Private WithEvents EndDate As DataGridViewCalendarColumn
	Private WithEvents Rate As DataGridViewTextBoxColumn


#End Region

#Region "Standard Code"

    ''' <summary>
    ''' On load of control, we set the event handler for delete
    ''' </summary>
	Private Sub ucListEmployeeProject_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        
		me.setColumnDataSources()
		

    End Sub

	
    'use this method to assign datasources to columns that are combo boxes!
    protected sub setColumnDataSources
		if me.DesignMode then exit sub
			
	
	end sub

	
#End Region

End Class

