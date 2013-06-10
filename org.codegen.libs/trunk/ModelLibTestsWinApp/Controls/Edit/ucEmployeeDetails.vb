Imports System.ComponentModel

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Class ucEmployeeDetails
    Inherits UcBaseEditControl
    Implements IUcEditControl

#Region "Designer"
    'UserControl overrides dispose to clean up the component list.
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

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        
        ' Add any initialization after the InitializeComponent() call.
        
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.EmployeeNamelbl = New System.Windows.Forms.Label()
        Me.EmployeeName = New org.codegen.win.controls.CGTextBox(Me.components)
        Me.EmployeeRankIdlbl = New System.Windows.Forms.Label()
        Me.EmployeeRankId = New org.codegen.win.controls.CGComboBox(Me.components)
        Me.Salarylbl = New System.Windows.Forms.Label()
        Me.Salary = New org.codegen.win.controls.CGDecimalTextBox()
        Me.Addresslbl = New System.Windows.Forms.Label()
        Me.Address = New org.codegen.win.controls.CGTextBox(Me.components)
        Me.Telephonelbl = New System.Windows.Forms.Label()
        Me.Telephone = New org.codegen.win.controls.CGTextBox(Me.components)
        Me.Mobilelbl = New System.Windows.Forms.Label()
        Me.Mobile = New org.codegen.win.controls.CGTextBox(Me.components)
        Me.IdNumberlbl = New System.Windows.Forms.Label()
        Me.IdNumber = New org.codegen.win.controls.CGTextBox(Me.components)
        Me.SSINumberlbl = New System.Windows.Forms.Label()
        Me.SSINumber = New org.codegen.win.controls.CGTextBox(Me.components)
        Me.HireDatelbl = New System.Windows.Forms.Label()
        Me.HireDate = New org.codegen.win.controls.CGDateTextBox(Me.components)
        Me.NumDependentslbl = New System.Windows.Forms.Label()
        Me.NumDependents = New org.codegen.win.controls.CGIntTextBox(Me.components)
        CType(Me.ErrProvider, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'EmployeeNamelbl
        '
        Me.EmployeeNamelbl.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.EmployeeNamelbl.Location = New System.Drawing.Point(5, 15)
        Me.EmployeeNamelbl.Name = "EmployeeNamelbl"
        Me.EmployeeNamelbl.Size = New System.Drawing.Size(120, 20)
        Me.EmployeeNamelbl.TabIndex = 0
        Me.EmployeeNamelbl.Text = "EmployeeName"
        Me.EmployeeNamelbl.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'EmployeeName
        '
        Me.EmployeeName.AssociatedLabel = Me.EmployeeNamelbl
        Me.EmployeeName.BackColor = System.Drawing.Color.LightYellow
        Me.EmployeeName.DataPropertyName = "EmployeeName"
        Me.EmployeeName.ErrProvider = Nothing
        Me.EmployeeName.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.EmployeeName.FormatPattern = Nothing
        Me.EmployeeName.isMandatory = True
        Me.EmployeeName.Location = New System.Drawing.Point(135, 15)
        Me.EmployeeName.MaxLength = 255
        Me.EmployeeName.MaxValue = Nothing
        Me.EmployeeName.MinValue = Nothing
        Me.EmployeeName.Name = "EmployeeName"
        Me.EmployeeName.Size = New System.Drawing.Size(200, 21)
        Me.EmployeeName.TabIndex = 0
        '
        'EmployeeRankIdlbl
        '
        Me.EmployeeRankIdlbl.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.EmployeeRankIdlbl.Location = New System.Drawing.Point(5, 45)
        Me.EmployeeRankIdlbl.Name = "EmployeeRankIdlbl"
        Me.EmployeeRankIdlbl.Size = New System.Drawing.Size(120, 20)
        Me.EmployeeRankIdlbl.TabIndex = 1
        Me.EmployeeRankIdlbl.Text = "EmployeeRankId"
        Me.EmployeeRankIdlbl.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'EmployeeRankId
        '
        Me.EmployeeRankId.AssociatedLabel = Me.EmployeeRankIdlbl
        Me.EmployeeRankId.BackColor = System.Drawing.Color.LightYellow
        Me.EmployeeRankId.DataPropertyName = "EmployeeRankId"
        Me.EmployeeRankId.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.EmployeeRankId.ErrProvider = Nothing
        Me.EmployeeRankId.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.EmployeeRankId.isMandatory = True
        Me.EmployeeRankId.Location = New System.Drawing.Point(135, 45)
        Me.EmployeeRankId.MaxLength = 255
        Me.EmployeeRankId.MaxValue = Nothing
        Me.EmployeeRankId.MinValue = Nothing
        Me.EmployeeRankId.Name = "EmployeeRankId"
        Me.EmployeeRankId.Size = New System.Drawing.Size(200, 21)
        Me.EmployeeRankId.TabIndex = 1
        '
        'Salarylbl
        '
        Me.Salarylbl.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Salarylbl.Location = New System.Drawing.Point(5, 75)
        Me.Salarylbl.Name = "Salarylbl"
        Me.Salarylbl.Size = New System.Drawing.Size(120, 20)
        Me.Salarylbl.TabIndex = 2
        Me.Salarylbl.Text = "Salary"
        Me.Salarylbl.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Salary
        '
        Me.Salary.AssociatedLabel = Me.Salarylbl
        Me.Salary.DataPropertyName = "Salary"
        Me.Salary.ErrProvider = Nothing
        Me.Salary.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Salary.FormatPattern = "00000000.00"
        Me.Salary.isMandatory = False
        Me.Salary.Location = New System.Drawing.Point(135, 75)
        Me.Salary.MaxLength = 10
        Me.Salary.MaxValue = Nothing
        Me.Salary.MinValue = Nothing
        Me.Salary.Name = "Salary"
        Me.Salary.Size = New System.Drawing.Size(200, 21)
        Me.Salary.TabIndex = 2
        '
        'Addresslbl
        '
        Me.Addresslbl.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Addresslbl.Location = New System.Drawing.Point(5, 105)
        Me.Addresslbl.Name = "Addresslbl"
        Me.Addresslbl.Size = New System.Drawing.Size(120, 20)
        Me.Addresslbl.TabIndex = 3
        Me.Addresslbl.Text = "Address"
        Me.Addresslbl.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Address
        '
        Me.Address.AssociatedLabel = Me.Addresslbl
        Me.Address.DataPropertyName = "Address"
        Me.Address.ErrProvider = Nothing
        Me.Address.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Address.FormatPattern = Nothing
        Me.Address.isMandatory = False
        Me.Address.Location = New System.Drawing.Point(135, 105)
        Me.Address.MaxLength = 255
        Me.Address.MaxValue = Nothing
        Me.Address.MinValue = Nothing
        Me.Address.Name = "Address"
        Me.Address.Size = New System.Drawing.Size(200, 21)
        Me.Address.TabIndex = 3
        '
        'Telephonelbl
        '
        Me.Telephonelbl.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Telephonelbl.Location = New System.Drawing.Point(5, 135)
        Me.Telephonelbl.Name = "Telephonelbl"
        Me.Telephonelbl.Size = New System.Drawing.Size(120, 20)
        Me.Telephonelbl.TabIndex = 4
        Me.Telephonelbl.Text = "Telephone"
        Me.Telephonelbl.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Telephone
        '
        Me.Telephone.AssociatedLabel = Me.Telephonelbl
        Me.Telephone.DataPropertyName = "Telephone"
        Me.Telephone.ErrProvider = Nothing
        Me.Telephone.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Telephone.FormatPattern = Nothing
        Me.Telephone.isMandatory = False
        Me.Telephone.Location = New System.Drawing.Point(135, 135)
        Me.Telephone.MaxLength = 255
        Me.Telephone.MaxValue = Nothing
        Me.Telephone.MinValue = Nothing
        Me.Telephone.Name = "Telephone"
        Me.Telephone.Size = New System.Drawing.Size(200, 21)
        Me.Telephone.TabIndex = 4
        '
        'Mobilelbl
        '
        Me.Mobilelbl.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Mobilelbl.Location = New System.Drawing.Point(5, 165)
        Me.Mobilelbl.Name = "Mobilelbl"
        Me.Mobilelbl.Size = New System.Drawing.Size(120, 20)
        Me.Mobilelbl.TabIndex = 5
        Me.Mobilelbl.Text = "Mobile"
        Me.Mobilelbl.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Mobile
        '
        Me.Mobile.AssociatedLabel = Me.Mobilelbl
        Me.Mobile.DataPropertyName = "Mobile"
        Me.Mobile.ErrProvider = Nothing
        Me.Mobile.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Mobile.FormatPattern = Nothing
        Me.Mobile.isMandatory = False
        Me.Mobile.Location = New System.Drawing.Point(135, 165)
        Me.Mobile.MaxLength = 255
        Me.Mobile.MaxValue = Nothing
        Me.Mobile.MinValue = Nothing
        Me.Mobile.Name = "Mobile"
        Me.Mobile.Size = New System.Drawing.Size(200, 21)
        Me.Mobile.TabIndex = 5
        '
        'IdNumberlbl
        '
        Me.IdNumberlbl.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.IdNumberlbl.Location = New System.Drawing.Point(5, 195)
        Me.IdNumberlbl.Name = "IdNumberlbl"
        Me.IdNumberlbl.Size = New System.Drawing.Size(120, 20)
        Me.IdNumberlbl.TabIndex = 6
        Me.IdNumberlbl.Text = "IdNumber"
        Me.IdNumberlbl.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'IdNumber
        '
        Me.IdNumber.AssociatedLabel = Me.IdNumberlbl
        Me.IdNumber.DataPropertyName = "IdNumber"
        Me.IdNumber.ErrProvider = Nothing
        Me.IdNumber.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.IdNumber.FormatPattern = Nothing
        Me.IdNumber.isMandatory = False
        Me.IdNumber.Location = New System.Drawing.Point(135, 195)
        Me.IdNumber.MaxLength = 255
        Me.IdNumber.MaxValue = Nothing
        Me.IdNumber.MinValue = Nothing
        Me.IdNumber.Name = "IdNumber"
        Me.IdNumber.Size = New System.Drawing.Size(200, 21)
        Me.IdNumber.TabIndex = 6
        '
        'SSINumberlbl
        '
        Me.SSINumberlbl.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SSINumberlbl.Location = New System.Drawing.Point(5, 225)
        Me.SSINumberlbl.Name = "SSINumberlbl"
        Me.SSINumberlbl.Size = New System.Drawing.Size(120, 20)
        Me.SSINumberlbl.TabIndex = 7
        Me.SSINumberlbl.Text = "SSINumber"
        Me.SSINumberlbl.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'SSINumber
        '
        Me.SSINumber.AssociatedLabel = Me.SSINumberlbl
        Me.SSINumber.DataPropertyName = "SSINumber"
        Me.SSINumber.ErrProvider = Nothing
        Me.SSINumber.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SSINumber.FormatPattern = Nothing
        Me.SSINumber.isMandatory = False
        Me.SSINumber.Location = New System.Drawing.Point(135, 225)
        Me.SSINumber.MaxLength = 255
        Me.SSINumber.MaxValue = Nothing
        Me.SSINumber.MinValue = Nothing
        Me.SSINumber.Name = "SSINumber"
        Me.SSINumber.Size = New System.Drawing.Size(200, 21)
        Me.SSINumber.TabIndex = 7
        '
        'HireDatelbl
        '
        Me.HireDatelbl.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.HireDatelbl.Location = New System.Drawing.Point(5, 255)
        Me.HireDatelbl.Name = "HireDatelbl"
        Me.HireDatelbl.Size = New System.Drawing.Size(120, 20)
        Me.HireDatelbl.TabIndex = 8
        Me.HireDatelbl.Text = "HireDate"
        Me.HireDatelbl.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'HireDate
        '
        Me.HireDate.AssociatedLabel = Me.HireDatelbl
        Me.HireDate.DataPropertyName = "HireDate"
        Me.HireDate.ErrProvider = Nothing
        Me.HireDate.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.HireDate.isMandatory = False
        Me.HireDate.Location = New System.Drawing.Point(135, 255)
        Me.HireDate.MaxLength = 255
        Me.HireDate.MaxValue = Nothing
        Me.HireDate.MinValue = Nothing
        Me.HireDate.Name = "HireDate"
        Me.HireDate.Size = New System.Drawing.Size(200, 21)
        Me.HireDate.TabIndex = 8
        '
        'NumDependentslbl
        '
        Me.NumDependentslbl.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NumDependentslbl.Location = New System.Drawing.Point(5, 285)
        Me.NumDependentslbl.Name = "NumDependentslbl"
        Me.NumDependentslbl.Size = New System.Drawing.Size(120, 20)
        Me.NumDependentslbl.TabIndex = 9
        Me.NumDependentslbl.Text = "NumDependents"
        Me.NumDependentslbl.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'NumDependents
        '
        Me.NumDependents.AssociatedLabel = Me.NumDependentslbl
        Me.NumDependents.DataPropertyName = "NumDependents"
        Me.NumDependents.ErrProvider = Nothing
        Me.NumDependents.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NumDependents.FormatPattern = Nothing
        Me.NumDependents.isMandatory = False
        Me.NumDependents.Location = New System.Drawing.Point(135, 285)
        Me.NumDependents.MaxLength = 255
        Me.NumDependents.MaxValue = Nothing
        Me.NumDependents.MinValue = Nothing
        Me.NumDependents.Name = "NumDependents"
        Me.NumDependents.Size = New System.Drawing.Size(200, 21)
        Me.NumDependents.TabIndex = 9
        '
        'ucEmployeeDetails
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
        Me.Controls.Add(Me.EmployeeNamelbl)
        Me.Controls.Add(Me.EmployeeName)
        Me.Controls.Add(Me.EmployeeRankIdlbl)
        Me.Controls.Add(Me.EmployeeRankId)
        Me.Controls.Add(Me.Salarylbl)
        Me.Controls.Add(Me.Salary)
        Me.Controls.Add(Me.Addresslbl)
        Me.Controls.Add(Me.Address)
        Me.Controls.Add(Me.Telephonelbl)
        Me.Controls.Add(Me.Telephone)
        Me.Controls.Add(Me.Mobilelbl)
        Me.Controls.Add(Me.Mobile)
        Me.Controls.Add(Me.IdNumberlbl)
        Me.Controls.Add(Me.IdNumber)
        Me.Controls.Add(Me.SSINumberlbl)
        Me.Controls.Add(Me.SSINumber)
        Me.Controls.Add(Me.HireDatelbl)
        Me.Controls.Add(Me.HireDate)
        Me.Controls.Add(Me.NumDependentslbl)
        Me.Controls.Add(Me.NumDependents)
        Me.Name = "ucEmployeeDetails"
        Me.Size = New System.Drawing.Size(844, 291)
        CType(Me.ErrProvider, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

	
	
	Friend WithEvents EmployeeNamelbl As System.Windows.Forms.Label
	Friend WithEvents EmployeeName As CGTextBox
	Friend WithEvents EmployeeRankIdlbl As System.Windows.Forms.Label
	Friend WithEvents EmployeeRankId As CGComboBox
	Friend WithEvents Salarylbl As System.Windows.Forms.Label
	Friend WithEvents Salary As CGDecimalTextBox
	Friend WithEvents Addresslbl As System.Windows.Forms.Label
	Friend WithEvents Address As CGTextBox
	Friend WithEvents Telephonelbl As System.Windows.Forms.Label
	Friend WithEvents Telephone As CGTextBox
	Friend WithEvents Mobilelbl As System.Windows.Forms.Label
	Friend WithEvents Mobile As CGTextBox
	Friend WithEvents IdNumberlbl As System.Windows.Forms.Label
	Friend WithEvents IdNumber As CGTextBox
	Friend WithEvents SSINumberlbl As System.Windows.Forms.Label
	Friend WithEvents SSINumber As CGTextBox
	Friend WithEvents HireDatelbl As System.Windows.Forms.Label
	Friend WithEvents HireDate As CGDateTextBox
	Friend WithEvents NumDependentslbl As System.Windows.Forms.Label
	Friend WithEvents NumDependents As CGIntTextBox


#End Region

#Region "Methods"
    
	 ''' <summary>
    ''' Fills the controls on the screen from data in the object
    ''' </summary>
    ''' <remarks></remarks>
	Public Sub _loadData() Handles Me.evLoadObjectData

        Dim mo As Employee = DirectCast(Me.ModelObject(), Employee)
        	Me.EmployeeName.value = mo.EmployeeName
	Me.EmployeeRankId.value = mo.EmployeeRankId
	Me.Salary.value = mo.Salary
	Me.Address.value = mo.Address
	Me.Telephone.value = mo.Telephone
	Me.Mobile.value = mo.Mobile
	Me.IdNumber.value = mo.IdNumber
	Me.SSINumber.value = mo.SSINumber
	Me.HireDate.value = mo.HireDate
	Me.NumDependents.value = mo.NumDependents


		me.resetLastLoadedValues()
		Me.ErrProvider.Clear()

    End Sub


    ''' <summary>
	''' Loads the object from the database and then sets the proeperties 
	''' of the object from values on the controls
	''' </summary>
	''' <remarks></remarks>
	Public Sub _loadToObject() Handles Me.evLoadToObject

        Dim mo As Employee = DirectCast(me.ModelObject, Employee)
        	mo.setEmployeeName(Me.EmployeeName.text)
	mo.EmployeeRankId= Me.EmployeeRankId.IntValue
	mo.setSalary(Me.Salary.text)
	mo.setAddress(Me.Address.text)
	mo.setTelephone(Me.Telephone.text)
	mo.setMobile(Me.Mobile.text)
	mo.setIdNumber(Me.IdNumber.text)
	mo.setSSINumber(Me.SSINumber.text)
	mo.setHireDate(Me.HireDate.text)
	mo.setNumDependents(Me.NumDependents.text)


    End Sub

    Public Sub _InitializeControl() Handles Me.InitializeControl

        If me.isInitialized = False Then
			'setup datasources
				Me.EmployeeRankId.DataSource = new BusObjects.Mappers.EmployeeRankDBMapper().findAll()
	Me.EmployeeRankId.DisplayMember = "Rank"
	Me.EmployeeRankId.ValueMember = "RankId"

			me.isInitialized = true
		End if

	End Sub

#End Region


  
End Class


