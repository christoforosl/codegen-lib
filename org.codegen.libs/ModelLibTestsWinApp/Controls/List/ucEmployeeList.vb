Imports System.ComponentModel
Imports ModelLibTestsWinApp.Forms.Edit

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Public Class ucEmployeeList
    Inherits System.Windows.Forms.UserControl

#Region "Designer"
    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()

        Me.EmployeeId = New DataGridViewTextBoxColumn
        Me.EmployeeName = New DataGridViewTextBoxColumn
        Me.EmployeeRankId = New DataGridViewComboBoxColumn
        Me.Salary = New DataGridViewTextBoxColumn
        Me.Address = New DataGridViewTextBoxColumn
        Me.Telephone = New DataGridViewTextBoxColumn
        Me.Mobile = New DataGridViewTextBoxColumn
        Me.IdNumber = New DataGridViewTextBoxColumn
        Me.SSINumber = New DataGridViewTextBoxColumn
        Me.HireDate = New DataGridViewCalendarColumn
        Me.NumDependents = New DataGridViewTextBoxColumn

        Me.grdEmployee = New org.codegen.win.controls.Grid.CGSQLGrid()
        Me.SuspendLayout()
        '
        'grdEmployee
        '
        Me.grdEmployee.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdEmployee.gpEditForm = frmEmployeeDetails.GetType
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
        EmployeeId.HeaderCell.Value = "EmployeeId"
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
        EmployeeName.HeaderCell.Value = "EmployeeName"
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
        EmployeeRankId.HeaderCell.Value = "EmployeeRankId"
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
        Salary.HeaderCell.Value = "Salary"
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
        Address.HeaderCell.Value = "Address"
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
        Telephone.HeaderCell.Value = "Telephone"
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
        Mobile.HeaderCell.Value = "Mobile"
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
        IdNumber.HeaderCell.Value = "IdNumber"
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
        SSINumber.HeaderCell.Value = "SSINumber"
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
        HireDate.HeaderCell.Value = "HireDate"
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
        NumDependents.HeaderCell.Value = "NumDependents"
        NumDependents.DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight
        NumDependents.HeaderCell.Style.Alignment = DataGridViewContentAlignment.TopRight
        NumDependents.Width = 50
        NumDependents.Visible = True
        '**** End Setup of column: NumDependents




        '
        'ucListEmployee
        '
        Me.Dock = DockStyle.Fill
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(573, 262)
        Me.Controls.Add(Me.grdEmployee)
        Me.grdEmployee.Columns.Add(EmployeeId)
        Me.grdEmployee.Columns.Add(EmployeeName)
        Me.grdEmployee.Columns.Add(EmployeeRankId)
        Me.grdEmployee.Columns.Add(Salary)
        Me.grdEmployee.Columns.Add(Address)
        Me.grdEmployee.Columns.Add(Telephone)
        Me.grdEmployee.Columns.Add(Mobile)
        Me.grdEmployee.Columns.Add(IdNumber)
        Me.grdEmployee.Columns.Add(SSINumber)
        Me.grdEmployee.Columns.Add(HireDate)
        Me.grdEmployee.Columns.Add(NumDependents)

        Me.Name = "ucEmployeeList"
        Me.Text = "ucEmployeeList"
        Me.ResumeLayout(False)

    End Sub

    Public WithEvents grdEmployee As org.codegen.win.controls.Grid.CGSQLGrid
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


#End Region

#Region "Standard Code"

    ''' <summary>
    ''' On load of control, we set the event handler for delete
    ''' </summary>
    Private Sub ucListEmployee_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Me.setColumnDataSources()


    End Sub


    'use this method to assign datasources to columns that are combo boxes!
    Protected Sub setColumnDataSources()
        If Me.DesignMode Then Exit Sub
        Me.EmployeeRankId.HeaderCell = New DataGridViewAutoFilterComboColumnHeaderCell()
        Me.EmployeeRankId.DataSource = New BusObjects.Mappers.EmployeeRankDBMapper().findAll()
        Me.EmployeeRankId.DisplayMember = "Rank"
        Me.EmployeeRankId.ValueMember = "RankId"
        Me.EmployeeRankId.FlatStyle = FlatStyle.Flat


    End Sub


#End Region

End Class

