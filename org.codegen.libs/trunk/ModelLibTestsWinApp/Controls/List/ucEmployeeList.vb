Imports System.ComponentModel

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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle11 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle12 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle13 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.EmployeeId = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.EmployeeName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.EmployeeRankId = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.Salary = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Address = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Telephone = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Mobile = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.IdNumber = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SSINumber = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.HireDate = New org.codegen.win.controls.DataGridViewCalendarColumn()
        Me.NumDependents = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.grdEmployee = New org.codegen.win.controls.Grid.CGSQLGrid()
        CType(Me.grdEmployee, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'EmployeeId
        '
        Me.EmployeeId.DataPropertyName = "EmployeeId"
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopRight
        Me.EmployeeId.DefaultCellStyle = DataGridViewCellStyle1
        Me.EmployeeId.HeaderText = "EmployeeId"
        Me.EmployeeId.Name = "EmployeeId"
        Me.EmployeeId.ReadOnly = True
        Me.EmployeeId.Visible = False
        Me.EmployeeId.Width = 50
        '
        'EmployeeName
        '
        Me.EmployeeName.DataPropertyName = "EmployeeName"
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft
        Me.EmployeeName.DefaultCellStyle = DataGridViewCellStyle2
        Me.EmployeeName.HeaderText = "EmployeeName"
        Me.EmployeeName.Name = "EmployeeName"
        Me.EmployeeName.ReadOnly = True
        '
        'EmployeeRankId
        '
        Me.EmployeeRankId.DataPropertyName = "EmployeeRankId"
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft
        Me.EmployeeRankId.DefaultCellStyle = DataGridViewCellStyle3
        Me.EmployeeRankId.HeaderText = "EmployeeRankId"
        Me.EmployeeRankId.Name = "EmployeeRankId"
        Me.EmployeeRankId.ReadOnly = True
        '
        'Salary
        '
        Me.Salary.DataPropertyName = "Salary"
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopRight
        Me.Salary.DefaultCellStyle = DataGridViewCellStyle4
        Me.Salary.HeaderText = "Salary"
        Me.Salary.Name = "Salary"
        Me.Salary.ReadOnly = True
        Me.Salary.Width = 50
        '
        'Address
        '
        Me.Address.DataPropertyName = "Address"
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft
        Me.Address.DefaultCellStyle = DataGridViewCellStyle5
        Me.Address.HeaderText = "Address"
        Me.Address.Name = "Address"
        Me.Address.ReadOnly = True
        '
        'Telephone
        '
        Me.Telephone.DataPropertyName = "Telephone"
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft
        Me.Telephone.DefaultCellStyle = DataGridViewCellStyle6
        Me.Telephone.HeaderText = "Telephone"
        Me.Telephone.Name = "Telephone"
        Me.Telephone.ReadOnly = True
        '
        'Mobile
        '
        Me.Mobile.DataPropertyName = "Mobile"
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft
        Me.Mobile.DefaultCellStyle = DataGridViewCellStyle7
        Me.Mobile.HeaderText = "Mobile"
        Me.Mobile.Name = "Mobile"
        Me.Mobile.ReadOnly = True
        '
        'IdNumber
        '
        Me.IdNumber.DataPropertyName = "IdNumber"
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft
        Me.IdNumber.DefaultCellStyle = DataGridViewCellStyle8
        Me.IdNumber.HeaderText = "IdNumber"
        Me.IdNumber.Name = "IdNumber"
        Me.IdNumber.ReadOnly = True
        '
        'SSINumber
        '
        Me.SSINumber.DataPropertyName = "SSINumber"
        DataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft
        Me.SSINumber.DefaultCellStyle = DataGridViewCellStyle9
        Me.SSINumber.HeaderText = "SSINumber"
        Me.SSINumber.Name = "SSINumber"
        Me.SSINumber.ReadOnly = True
        '
        'HireDate
        '
        Me.HireDate.DataPropertyName = "HireDate"
        DataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft
        Me.HireDate.DefaultCellStyle = DataGridViewCellStyle10
        Me.HireDate.HeaderText = "HireDate"
        Me.HireDate.Name = "HireDate"
        Me.HireDate.ReadOnly = True
        Me.HireDate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'NumDependents
        '
        Me.NumDependents.DataPropertyName = "NumDependents"
        DataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopRight
        Me.NumDependents.DefaultCellStyle = DataGridViewCellStyle11
        Me.NumDependents.HeaderText = "NumDependents"
        Me.NumDependents.Name = "NumDependents"
        Me.NumDependents.ReadOnly = True
        Me.NumDependents.Width = 50
        '
        'grdEmployee
        '
        Me.grdEmployee.AllowUserToAddRows = False
        Me.grdEmployee.AllowUserToDeleteRows = False
        Me.grdEmployee.AllowUserToOrderColumns = True
        Me.grdEmployee.AllowUserToResizeRows = False
        DataGridViewCellStyle12.BackColor = System.Drawing.Color.AntiqueWhite
        Me.grdEmployee.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle12
        Me.grdEmployee.BackgroundColor = System.Drawing.SystemColors.Window
        Me.grdEmployee.BindingSource = Nothing
        Me.grdEmployee.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal
        Me.grdEmployee.ColumnIndexToHide = 0
        Me.grdEmployee.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.EmployeeId, Me.EmployeeName, Me.EmployeeRankId, Me.Salary, Me.Address, Me.Telephone, Me.Mobile, Me.IdNumber, Me.SSINumber, Me.HireDate, Me.NumDependents})
        Me.grdEmployee.DBMapper = Nothing
        Me.grdEmployee.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdEmployee.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.grdEmployee.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdEmployee.gpEditForm = "Forms.Edit.frmEmployeeDetails"
        Me.grdEmployee.gpKeyColumnName = "EmployeeId"
        Me.grdEmployee.gpSelectFields = "*"
        Me.grdEmployee.gpSelectFrom = "Employee"
        Me.grdEmployee.gpSortColumn = Nothing
        Me.grdEmployee.gpSortDirection = System.ComponentModel.ListSortDirection.Ascending
        Me.grdEmployee.gpWhereclause = " "
        Me.grdEmployee.GridColor = System.Drawing.SystemColors.Control
        Me.grdEmployee.GridColumnProvider = Nothing
        Me.grdEmployee.isLocalizable = False
        Me.grdEmployee.lastLoadedSQL = ""
        Me.grdEmployee.Location = New System.Drawing.Point(0, 0)
        Me.grdEmployee.Name = "grdEmployee"
        Me.grdEmployee.ReadOnly = True
        DataGridViewCellStyle13.BackColor = System.Drawing.SystemColors.Desktop
        DataGridViewCellStyle13.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle13.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle13.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle13.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        Me.grdEmployee.RowHeadersDefaultCellStyle = DataGridViewCellStyle13
        Me.grdEmployee.RowHeadersVisible = False
        Me.grdEmployee.RowTemplate.Height = 20
        Me.grdEmployee.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdEmployee.SelectSQLStatement = Nothing
        Me.grdEmployee.Size = New System.Drawing.Size(795, 375)
        Me.grdEmployee.TabIndex = 0
        '
        'ucEmployeeList
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.grdEmployee)
        Me.Name = "ucEmployeeList"
        Me.Size = New System.Drawing.Size(795, 375)
        CType(Me.grdEmployee, System.ComponentModel.ISupportInitialize).EndInit()
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
	me.EmployeeRankId.DataSource = new BusObjects.Mappers.EmployeeRankDBMapper().findAll()
	me.EmployeeRankId.DisplayMember = "Rank"
	me.EmployeeRankId.ValueMember = "RankId"
	me.EmployeeRankId.FlatStyle = FlatStyle.Flat
	
	
	end sub

	
#End Region

End Class

