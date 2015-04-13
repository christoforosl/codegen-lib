Imports System.ComponentModel
'ListControlTemplate.txt
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Class ucEmployeeList
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

			me.EmployeeId = new DataGridViewTextBoxColumn
	me.EmployeeName = new DataGridViewTextBoxColumn
	me.EmployeeRankId = new DataGridViewComboBoxColumn
	me.Salary = new DataGridViewTextBoxColumn
	me.Address = new DataGridViewTextBoxColumn
	me.Telephone = new DataGridViewTextBoxColumn
	me.Mobile = new DataGridViewTextBoxColumn
	me.IdNumber = new DataGridViewTextBoxColumn
	me.SSINumber = new DataGridViewTextBoxColumn
	me.HireDate = new DataGridViewCalendarColumn
	me.NumDependents = new DataGridViewTextBoxColumn
	me.CreateDate = new DataGridViewCalendarColumn
	me.UpdateDate = new DataGridViewCalendarColumn
	me.UpdateUser = new DataGridViewTextBoxColumn
	me.CreateUser = new DataGridViewTextBoxColumn
	me.EmployeeTypeCode = new DataGridViewTextBoxColumn

        Me.grdEmployee = New org.codegen.win.controls.Grid.CGSQLGrid()
        Me.SuspendLayout()
        '
        'grdEmployee
        '
        Me.grdEmployee.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdEmployee.gpEditForm = "Forms.Edit.frmEmployeeDetails"
        Me.grdEmployee.gpSelectFields = "*"
        Me.grdEmployee.gpSelectFrom = "Employee"
        
        Me.grdEmployee.GridColumnProvider = Nothing
        Me.grdEmployee.gpKeyColumnName = "EmployeeId"
        Me.grdEmployee.Location = New System.Drawing.Point(0, 0)
        Me.grdEmployee.Name = "grdEmployee"
        Me.grdEmployee.Size = New System.Drawing.Size(573, 262)
        Me.grdEmployee.TabIndex = 0

			' column: EmployeeId
	EmployeeId.CellTemplate = New DataGridViewTextBoxCell
	EmployeeId.Name = "EmployeeId"
	EmployeeId.DataPropertyName = "EmployeeId"
	EmployeeId.ReadOnly = True
	EmployeeId.HeaderText = "EmployeeId"
	EmployeeId.HeaderCell.value = "EmployeeId"
	EmployeeId.DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight
	EmployeeId.HeaderCell.Style.Alignment = DataGridViewContentAlignment.TopRight
	EmployeeId.Width = 50
	EmployeeId.Visible = False
	'**** End Setup of column: EmployeeId


	' column: EmployeeName
	EmployeeName.CellTemplate = New DataGridViewTextBoxCell
	EmployeeName.Name = "EmployeeName"
	EmployeeName.DataPropertyName = "EmployeeName"
	EmployeeName.ReadOnly = True
	EmployeeName.HeaderText = "EmployeeName"
	EmployeeName.HeaderCell.value = "EmployeeName"
	EmployeeName.DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft
	EmployeeName.HeaderCell.Style.Alignment = DataGridViewContentAlignment.TopLeft
	EmployeeName.Width = 100
	EmployeeName.Visible = True
	'**** End Setup of column: EmployeeName


	' column: EmployeeRankId
	EmployeeRankId.CellTemplate = New DataGridViewComboBoxCell
	EmployeeRankId.HeaderCell = New DataGridViewAutoFilterComboColumnHeaderCell
	EmployeeRankId.Name = "EmployeeRankId"
	EmployeeRankId.DataPropertyName = "EmployeeRankId"
	EmployeeRankId.ReadOnly = True
	EmployeeRankId.HeaderText = "EmployeeRankId"
	EmployeeRankId.HeaderCell.value = "EmployeeRankId"
	EmployeeRankId.DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft
	EmployeeRankId.HeaderCell.Style.Alignment = DataGridViewContentAlignment.TopLeft
	EmployeeRankId.Width = 100
	EmployeeRankId.Visible = True
	'**** End Setup of column: EmployeeRankId


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


	' column: Telephone
	Telephone.CellTemplate = New DataGridViewTextBoxCell
	Telephone.Name = "Telephone"
	Telephone.DataPropertyName = "Telephone"
	Telephone.ReadOnly = True
	Telephone.HeaderText = "Telephone"
	Telephone.HeaderCell.value = "Telephone"
	Telephone.DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft
	Telephone.HeaderCell.Style.Alignment = DataGridViewContentAlignment.TopLeft
	Telephone.Width = 100
	Telephone.Visible = True
	'**** End Setup of column: Telephone


	' column: Mobile
	Mobile.CellTemplate = New DataGridViewTextBoxCell
	Mobile.Name = "Mobile"
	Mobile.DataPropertyName = "Mobile"
	Mobile.ReadOnly = True
	Mobile.HeaderText = "Mobile"
	Mobile.HeaderCell.value = "Mobile"
	Mobile.DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft
	Mobile.HeaderCell.Style.Alignment = DataGridViewContentAlignment.TopLeft
	Mobile.Width = 100
	Mobile.Visible = True
	'**** End Setup of column: Mobile


	' column: IdNumber
	IdNumber.CellTemplate = New DataGridViewTextBoxCell
	IdNumber.Name = "IdNumber"
	IdNumber.DataPropertyName = "IdNumber"
	IdNumber.ReadOnly = True
	IdNumber.HeaderText = "IdNumber"
	IdNumber.HeaderCell.value = "IdNumber"
	IdNumber.DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft
	IdNumber.HeaderCell.Style.Alignment = DataGridViewContentAlignment.TopLeft
	IdNumber.Width = 100
	IdNumber.Visible = True
	'**** End Setup of column: IdNumber


	' column: SSINumber
	SSINumber.CellTemplate = New DataGridViewTextBoxCell
	SSINumber.Name = "SSINumber"
	SSINumber.DataPropertyName = "SSINumber"
	SSINumber.ReadOnly = True
	SSINumber.HeaderText = "SSINumber"
	SSINumber.HeaderCell.value = "SSINumber"
	SSINumber.DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft
	SSINumber.HeaderCell.Style.Alignment = DataGridViewContentAlignment.TopLeft
	SSINumber.Width = 100
	SSINumber.Visible = True
	'**** End Setup of column: SSINumber


	' column: HireDate
	HireDate.CellTemplate = New CalendarCell
	HireDate.Name = "HireDate"
	HireDate.DataPropertyName = "HireDate"
	HireDate.ReadOnly = True
	HireDate.HeaderText = "HireDate"
	HireDate.HeaderCell.value = "HireDate"
	HireDate.DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft
	HireDate.HeaderCell.Style.Alignment = DataGridViewContentAlignment.TopLeft
	HireDate.Width = 100
	HireDate.Visible = True
	HireDate.CellTemplate.Style.Format = "d"
	'**** End Setup of column: HireDate


	' column: NumDependents
	NumDependents.CellTemplate = New DataGridViewTextBoxCell
	NumDependents.Name = "NumDependents"
	NumDependents.DataPropertyName = "NumDependents"
	NumDependents.ReadOnly = True
	NumDependents.HeaderText = "NumDependents"
	NumDependents.HeaderCell.value = "NumDependents"
	NumDependents.DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight
	NumDependents.HeaderCell.Style.Alignment = DataGridViewContentAlignment.TopRight
	NumDependents.Width = 50
	NumDependents.Visible = True
	'**** End Setup of column: NumDependents


	' column: CreateDate
	CreateDate.CellTemplate = New CalendarCell
	CreateDate.Name = "CreateDate"
	CreateDate.DataPropertyName = "createDate"
	CreateDate.ReadOnly = True
	CreateDate.HeaderText = "CreateDate"
	CreateDate.HeaderCell.value = "CreateDate"
	CreateDate.DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft
	CreateDate.HeaderCell.Style.Alignment = DataGridViewContentAlignment.TopLeft
	CreateDate.Width = 100
	CreateDate.Visible = True
	CreateDate.CellTemplate.Style.Format = "d"
	'**** End Setup of column: CreateDate


	' column: UpdateDate
	UpdateDate.CellTemplate = New CalendarCell
	UpdateDate.Name = "UpdateDate"
	UpdateDate.DataPropertyName = "updateDate"
	UpdateDate.ReadOnly = True
	UpdateDate.HeaderText = "UpdateDate"
	UpdateDate.HeaderCell.value = "UpdateDate"
	UpdateDate.DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft
	UpdateDate.HeaderCell.Style.Alignment = DataGridViewContentAlignment.TopLeft
	UpdateDate.Width = 100
	UpdateDate.Visible = True
	UpdateDate.CellTemplate.Style.Format = "d"
	'**** End Setup of column: UpdateDate


	' column: UpdateUser
	UpdateUser.CellTemplate = New DataGridViewTextBoxCell
	UpdateUser.Name = "UpdateUser"
	UpdateUser.DataPropertyName = "updateUser"
	UpdateUser.ReadOnly = True
	UpdateUser.HeaderText = "UpdateUser"
	UpdateUser.HeaderCell.value = "UpdateUser"
	UpdateUser.DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft
	UpdateUser.HeaderCell.Style.Alignment = DataGridViewContentAlignment.TopLeft
	UpdateUser.Width = 100
	UpdateUser.Visible = True
	'**** End Setup of column: UpdateUser


	' column: CreateUser
	CreateUser.CellTemplate = New DataGridViewTextBoxCell
	CreateUser.Name = "CreateUser"
	CreateUser.DataPropertyName = "createUser"
	CreateUser.ReadOnly = True
	CreateUser.HeaderText = "CreateUser"
	CreateUser.HeaderCell.value = "CreateUser"
	CreateUser.DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft
	CreateUser.HeaderCell.Style.Alignment = DataGridViewContentAlignment.TopLeft
	CreateUser.Width = 100
	CreateUser.Visible = True
	'**** End Setup of column: CreateUser


	' column: EmployeeTypeCode
	EmployeeTypeCode.CellTemplate = New DataGridViewTextBoxCell
	EmployeeTypeCode.Name = "EmployeeTypeCode"
	EmployeeTypeCode.DataPropertyName = "EmployeeTypeCode"
	EmployeeTypeCode.ReadOnly = True
	EmployeeTypeCode.HeaderText = "EmployeeTypeCode"
	EmployeeTypeCode.HeaderCell.value = "EmployeeTypeCode"
	EmployeeTypeCode.DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft
	EmployeeTypeCode.HeaderCell.Style.Alignment = DataGridViewContentAlignment.TopLeft
	EmployeeTypeCode.Width = 100
	EmployeeTypeCode.Visible = True
	'**** End Setup of column: EmployeeTypeCode




        '
        'ucListEmployee
        '
		Me.Dock=DockStyle.Fill
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(573, 262)
        Me.Controls.Add(Me.grdEmployee)
			me.grdEmployee.Columns.Add(EmployeeId)
	me.grdEmployee.Columns.Add(EmployeeName)
	me.grdEmployee.Columns.Add(EmployeeRankId)
	me.grdEmployee.Columns.Add(Salary)
	me.grdEmployee.Columns.Add(Address)
	me.grdEmployee.Columns.Add(Telephone)
	me.grdEmployee.Columns.Add(Mobile)
	me.grdEmployee.Columns.Add(IdNumber)
	me.grdEmployee.Columns.Add(SSINumber)
	me.grdEmployee.Columns.Add(HireDate)
	me.grdEmployee.Columns.Add(NumDependents)
	me.grdEmployee.Columns.Add(CreateDate)
	me.grdEmployee.Columns.Add(UpdateDate)
	me.grdEmployee.Columns.Add(UpdateUser)
	me.grdEmployee.Columns.Add(CreateUser)
	me.grdEmployee.Columns.Add(EmployeeTypeCode)

        Me.Name = "ucEmployeeList"
        Me.Text = "ucEmployeeList"
        Me.ResumeLayout(False)

    End Sub

    public WithEvents grdEmployee As org.codegen.win.controls.Grid.CGSQLGrid
		Private WithEvents EmployeeId As DataGridViewTextBoxColumn
	Private WithEvents EmployeeName As DataGridViewTextBoxColumn
	Private WithEvents EmployeeRankId As DataGridViewComboBoxColumn
	Private WithEvents Salary As DataGridViewTextBoxColumn
	Private WithEvents Address As DataGridViewTextBoxColumn
	Private WithEvents Telephone As DataGridViewTextBoxColumn
	Private WithEvents Mobile As DataGridViewTextBoxColumn
	Private WithEvents IdNumber As DataGridViewTextBoxColumn
	Private WithEvents SSINumber As DataGridViewTextBoxColumn
	Private WithEvents HireDate As DataGridViewCalendarColumn
	Private WithEvents NumDependents As DataGridViewTextBoxColumn
	Private WithEvents CreateDate As DataGridViewCalendarColumn
	Private WithEvents UpdateDate As DataGridViewCalendarColumn
	Private WithEvents UpdateUser As DataGridViewTextBoxColumn
	Private WithEvents CreateUser As DataGridViewTextBoxColumn
	Private WithEvents EmployeeTypeCode As DataGridViewTextBoxColumn


#End Region

#Region "Standard Code"

    ''' <summary>
    ''' On load of control, we set the event handler for delete
    ''' </summary>
	Private Sub ucListEmployee_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        
		me.setColumnDataSources()
		

    End Sub

	
    'use this method to assign datasources to columns that are combo boxes!
    protected sub setColumnDataSources
		if me.DesignMode then exit sub
			Me.EmployeeRankId.HeaderCell =  New DataGridViewAutoFilterComboColumnHeaderCell()
	me.EmployeeRankId.DataSource = new VbBusObjects.DBMappers.EmployeeRankDBMapper().findAll()
	me.EmployeeRankId.DisplayMember = "Rank"
	me.EmployeeRankId.ValueMember = "RankId"
	me.EmployeeRankId.FlatStyle = FlatStyle.Flat
	
	
	end sub

	
#End Region

End Class

