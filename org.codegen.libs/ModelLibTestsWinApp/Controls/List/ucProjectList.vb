Imports System.ComponentModel
'ListControlTemplate.txt
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Class ucProjectList
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

			me.ProjectId = new DataGridViewTextBoxColumn
	me.ProjectName = new DataGridViewTextBoxColumn
	me.IsActive = new DataGridViewCheckBoxColumn

        Me.grdProject = New org.codegen.win.controls.Grid.CGSQLGrid()
        Me.SuspendLayout()
        '
        'grdProject
        '
        Me.grdProject.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdProject.gpEditForm = "Forms.Edit.frmProjectDetails"
        Me.grdProject.gpSelectFields = "*"
        Me.grdProject.gpSelectFrom = "Project"
        
        Me.grdProject.GridColumnProvider = Nothing
        Me.grdProject.gpKeyColumnName = "ProjectId"
        Me.grdProject.Location = New System.Drawing.Point(0, 0)
        Me.grdProject.Name = "grdProject"
        Me.grdProject.Size = New System.Drawing.Size(573, 262)
        Me.grdProject.TabIndex = 0

			' column: ProjectId
	ProjectId.CellTemplate = New DataGridViewTextBoxCell
	ProjectId.Name = "ProjectId"
	ProjectId.DataPropertyName = "ProjectId"
	ProjectId.ReadOnly = True
	ProjectId.HeaderText = "ProjectId"
	ProjectId.HeaderCell.value = "ProjectId"
	ProjectId.DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight
	ProjectId.HeaderCell.Style.Alignment = DataGridViewContentAlignment.TopRight
	ProjectId.Width = 50
	ProjectId.Visible = False
	'**** End Setup of column: ProjectId


	' column: ProjectName
	ProjectName.CellTemplate = New DataGridViewTextBoxCell
	ProjectName.Name = "ProjectName"
	ProjectName.DataPropertyName = "ProjectName"
	ProjectName.ReadOnly = True
	ProjectName.HeaderText = "ProjectName"
	ProjectName.HeaderCell.value = "ProjectName"
	ProjectName.DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft
	ProjectName.HeaderCell.Style.Alignment = DataGridViewContentAlignment.TopLeft
	ProjectName.Width = 100
	ProjectName.Visible = True
	'**** End Setup of column: ProjectName


	' column: IsActive
	IsActive.CellTemplate = New DataGridViewCheckBoxCell
	IsActive.HeaderCell = New DataGridViewAutoFilterBooleanColumnHeaderCell
	IsActive.Name = "IsActive"
	IsActive.DataPropertyName = "isActive"
	IsActive.ReadOnly = True
	IsActive.HeaderText = "IsActive"
	IsActive.HeaderCell.value = "IsActive"
	IsActive.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
	IsActive.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
	IsActive.Width = 100
	IsActive.Visible = True
	'**** End Setup of column: IsActive




        '
        'ucListProject
        '
		Me.Dock=DockStyle.Fill
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(573, 262)
        Me.Controls.Add(Me.grdProject)
			me.grdProject.Columns.Add(ProjectId)
	me.grdProject.Columns.Add(ProjectName)
	me.grdProject.Columns.Add(IsActive)

        Me.Name = "ucProjectList"
        Me.Text = "ucProjectList"
        Me.ResumeLayout(False)

    End Sub

    public WithEvents grdProject As org.codegen.win.controls.Grid.CGSQLGrid
		Private WithEvents ProjectId As DataGridViewTextBoxColumn
	Private WithEvents ProjectName As DataGridViewTextBoxColumn
	Private WithEvents IsActive As DataGridViewCheckBoxColumn


#End Region

#Region "Standard Code"

    ''' <summary>
    ''' On load of control, we set the event handler for delete
    ''' </summary>
	Private Sub ucListProject_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        
		me.setColumnDataSources()
		

    End Sub

	
    'use this method to assign datasources to columns that are combo boxes!
    protected sub setColumnDataSources
		if me.DesignMode then exit sub
			
	
	end sub

	
#End Region

End Class

