Imports System.ComponentModel
'ListControlTemplate.txt
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Class ucEmployeeInfoList
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

			me.EmployeeInfoId = new DataGridViewTextBoxColumn
	me.EIEmployeeId = new DataGridViewTextBoxColumn
	me.Salary = new DataGridViewTextBoxColumn
	me.Address = new DataGridViewTextBoxColumn

        Me.grdEmployeeInfo = New org.codegen.win.controls.Grid.CGSQLGrid()
        Me.SuspendLayout()
        '
        'grdEmployeeInfo
        '
        Me.grdEmployeeInfo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdEmployeeInfo.gpEditForm = "Forms.Edit.frmEmployeeInfoDetails"
        Me.grdEmployeeInfo.gpSelectFields = "*"
        Me.grdEmployeeInfo.gpSelectFrom = "EmployeeInfo"
        
        Me.grdEmployeeInfo.GridColumnProvider = Nothing
        Me.grdEmployeeInfo.gpKeyColumnName = "EmployeeInfoId"
        Me.grdEmployeeInfo.Location = New System.Drawing.Point(0, 0)
        Me.grdEmployeeInfo.Name = "grdEmployeeInfo"
        Me.grdEmployeeInfo.Size = New System.Drawing.Size(573, 262)
        Me.grdEmployeeInfo.TabIndex = 0

			' column: EmployeeInfoId
	EmployeeInfoId.CellTemplate = New DataGridViewTextBoxCell
	EmployeeInfoId.Name = "EmployeeInfoId"
	EmployeeInfoId.DataPropertyName = "EmployeeInfoId"
	EmployeeInfoId.ReadOnly = True
	EmployeeInfoId.HeaderText = "EmployeeInfoId"
	EmployeeInfoId.HeaderCell.value = "EmployeeInfoId"
	EmployeeInfoId.DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight
	EmployeeInfoId.HeaderCell.Style.Alignment = DataGridViewContentAlignment.TopRight
	EmployeeInfoId.Width = 50
	EmployeeInfoId.Visible = False
	'**** End Setup of column: EmployeeInfoId


	' column: EIEmployeeId
	EIEmployeeId.CellTemplate = New DataGridViewTextBoxCell
	EIEmployeeId.Name = "EIEmployeeId"
	EIEmployeeId.DataPropertyName = "EIEmployeeId"
	EIEmployeeId.ReadOnly = True
	EIEmployeeId.HeaderText = "EIEmployeeId"
	EIEmployeeId.HeaderCell.value = "EIEmployeeId"
	EIEmployeeId.DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight
	EIEmployeeId.HeaderCell.Style.Alignment = DataGridViewContentAlignment.TopRight
	EIEmployeeId.Width = 50
	EIEmployeeId.Visible = True
	'**** End Setup of column: EIEmployeeId


	' column: Salary
	Salary.CellTemplate = New DataGridViewTextBoxCell
	Salary.Name = "Salary"
	Salary.DataPropertyName = "Salary"
	Salary.ReadOnly = True
	Salary.HeaderText = "Salary"
	Salary.HeaderCell.value = "Salary"
	Salary.DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight
	Salary.HeaderCell.Style.Alignment = DataGridViewContentAlignment.TopRight
	Salary.Width = 50
	Salary.Visible = True
	Salary.CellTemplate.Style.Format = "C"
	'**** End Setup of column: Salary


	' column: Address
	Address.CellTemplate = New DataGridViewTextBoxCell
	Address.Name = "Address"
	Address.DataPropertyName = "Address"
	Address.ReadOnly = True
	Address.HeaderText = "Address"
	Address.HeaderCell.value = "Address"
	Address.DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft
	Address.HeaderCell.Style.Alignment = DataGridViewContentAlignment.TopLeft
	Address.Width = 100
	Address.Visible = True
	'**** End Setup of column: Address




        '
        'ucListEmployeeInfo
        '
		Me.Dock=DockStyle.Fill
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(573, 262)
        Me.Controls.Add(Me.grdEmployeeInfo)
			me.grdEmployeeInfo.Columns.Add(EmployeeInfoId)
	me.grdEmployeeInfo.Columns.Add(EIEmployeeId)
	me.grdEmployeeInfo.Columns.Add(Salary)
	me.grdEmployeeInfo.Columns.Add(Address)

        Me.Name = "ucEmployeeInfoList"
        Me.Text = "ucEmployeeInfoList"
        Me.ResumeLayout(False)

    End Sub

    public WithEvents grdEmployeeInfo As org.codegen.win.controls.Grid.CGSQLGrid
		Private WithEvents EmployeeInfoId As DataGridViewTextBoxColumn
	Private WithEvents EIEmployeeId As DataGridViewTextBoxColumn
	Private WithEvents Salary As DataGridViewTextBoxColumn
	Private WithEvents Address As DataGridViewTextBoxColumn


#End Region

#Region "Standard Code"

    ''' <summary>
    ''' On load of control, we set the event handler for delete
    ''' </summary>
	Private Sub ucListEmployeeInfo_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        
		me.setColumnDataSources()
		

    End Sub

	
    'use this method to assign datasources to columns that are combo boxes!
    protected sub setColumnDataSources
		if me.DesignMode then exit sub
			
	
	end sub

	
#End Region

End Class

