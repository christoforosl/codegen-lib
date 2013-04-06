Imports System.ComponentModel

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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.EmployeeInfoId = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.EIEmployeeId = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Salary = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Address = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.grdEmployeeInfo = New org.codegen.win.controls.Grid.CGSQLGrid()
        CType(Me.grdEmployeeInfo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'EmployeeInfoId
        '
        Me.EmployeeInfoId.DataPropertyName = "EmployeeInfoId"
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopRight
        Me.EmployeeInfoId.DefaultCellStyle = DataGridViewCellStyle1
        Me.EmployeeInfoId.HeaderText = "EmployeeInfoId"
        Me.EmployeeInfoId.Name = "EmployeeInfoId"
        Me.EmployeeInfoId.ReadOnly = True
        Me.EmployeeInfoId.Visible = False
        Me.EmployeeInfoId.Width = 50
        '
        'EIEmployeeId
        '
        Me.EIEmployeeId.DataPropertyName = "EIEmployeeId"
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopRight
        Me.EIEmployeeId.DefaultCellStyle = DataGridViewCellStyle2
        Me.EIEmployeeId.HeaderText = "EIEmployeeId"
        Me.EIEmployeeId.Name = "EIEmployeeId"
        Me.EIEmployeeId.ReadOnly = True
        Me.EIEmployeeId.Width = 50
        '
        'Salary
        '
        Me.Salary.DataPropertyName = "Salary"
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopRight
        Me.Salary.DefaultCellStyle = DataGridViewCellStyle3
        Me.Salary.HeaderText = "Salary"
        Me.Salary.Name = "Salary"
        Me.Salary.ReadOnly = True
        Me.Salary.Width = 50
        '
        'Address
        '
        Me.Address.DataPropertyName = "Address"
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft
        Me.Address.DefaultCellStyle = DataGridViewCellStyle4
        Me.Address.HeaderText = "Address"
        Me.Address.Name = "Address"
        Me.Address.ReadOnly = True
        '
        'grdEmployeeInfo
        '
        Me.grdEmployeeInfo.AllowUserToAddRows = False
        Me.grdEmployeeInfo.AllowUserToDeleteRows = False
        Me.grdEmployeeInfo.AllowUserToOrderColumns = True
        Me.grdEmployeeInfo.AllowUserToResizeRows = False
        DataGridViewCellStyle5.BackColor = System.Drawing.Color.AntiqueWhite
        Me.grdEmployeeInfo.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle5
        Me.grdEmployeeInfo.BackgroundColor = System.Drawing.SystemColors.Window
        Me.grdEmployeeInfo.BindingSource = Nothing
        Me.grdEmployeeInfo.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal
        Me.grdEmployeeInfo.ColumnIndexToHide = 0
        Me.grdEmployeeInfo.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.EmployeeInfoId, Me.EIEmployeeId, Me.Salary, Me.Address})
        Me.grdEmployeeInfo.DBMapper = Nothing
        Me.grdEmployeeInfo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdEmployeeInfo.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.grdEmployeeInfo.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdEmployeeInfo.gpEditForm = "Forms.Edit.frmEmployeeInfoDetails"
        Me.grdEmployeeInfo.gpKeyColumnName = "EmployeeInfoId"
        Me.grdEmployeeInfo.gpSelectFields = "*"
        Me.grdEmployeeInfo.gpSelectFrom = "EmployeeInfo"
        Me.grdEmployeeInfo.gpSortColumn = Nothing
        Me.grdEmployeeInfo.gpSortDirection = System.ComponentModel.ListSortDirection.Ascending
        Me.grdEmployeeInfo.gpWhereclause = " "
        Me.grdEmployeeInfo.GridColor = System.Drawing.SystemColors.Control
        Me.grdEmployeeInfo.GridColumnProvider = Nothing
        Me.grdEmployeeInfo.isLocalizable = False
        Me.grdEmployeeInfo.lastLoadedSQL = ""
        Me.grdEmployeeInfo.Location = New System.Drawing.Point(0, 0)
        Me.grdEmployeeInfo.Name = "grdEmployeeInfo"
        Me.grdEmployeeInfo.ReadOnly = True
        DataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Desktop
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        Me.grdEmployeeInfo.RowHeadersDefaultCellStyle = DataGridViewCellStyle6
        Me.grdEmployeeInfo.RowHeadersVisible = False
        Me.grdEmployeeInfo.RowTemplate.Height = 20
        Me.grdEmployeeInfo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdEmployeeInfo.SelectSQLStatement = Nothing
        Me.grdEmployeeInfo.Size = New System.Drawing.Size(795, 375)
        Me.grdEmployeeInfo.TabIndex = 0
        '
        'ucEmployeeInfoList
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.grdEmployeeInfo)
        Me.Name = "ucEmployeeInfoList"
        Me.Size = New System.Drawing.Size(795, 375)
        CType(Me.grdEmployeeInfo, System.ComponentModel.ISupportInitialize).EndInit()
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

