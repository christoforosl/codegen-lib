Imports System.ComponentModel
'EditControlTemplate.txt
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

       
	Me.EmployeeNamelbl = New System.Windows.Forms.Label
	Me.EmployeeName = New CGTextBox
	Me.EmployeeRankIdlbl = New System.Windows.Forms.Label
	Me.EmployeeRankId = New CGComboBox
	Me.Salarylbl = New System.Windows.Forms.Label
	Me.Salary = New CGDecimalTextBox
	Me.Addresslbl = New System.Windows.Forms.Label
	Me.Address = New CGTextBox
	Me.Telephonelbl = New System.Windows.Forms.Label
	Me.Telephone = New CGTextBox
	Me.Mobilelbl = New System.Windows.Forms.Label
	Me.Mobile = New CGTextBox
	Me.IdNumberlbl = New System.Windows.Forms.Label
	Me.IdNumber = New CGTextBox
	Me.SSINumberlbl = New System.Windows.Forms.Label
	Me.SSINumber = New CGTextBox
	Me.HireDatelbl = New System.Windows.Forms.Label
	Me.HireDate = New CGDateTextBox
	Me.NumDependentslbl = New System.Windows.Forms.Label
	Me.NumDependents = New CGIntTextBox
	Me.EmployeeTypeCodelbl = New System.Windows.Forms.Label
	Me.EmployeeTypeCode = New CGTextBox
	Me.SampleGuidFieldlbl = New System.Windows.Forms.Label
	Me.SampleGuidField = New CGTextBox


        Me.SuspendLayout()
			'EmployeeNamelbl.
	Me.EmployeeNamelbl.AutoSize = False
	Me.EmployeeNamelbl.Location = New System.Drawing.Point(5, 15)
	Me.EmployeeNamelbl.Name = "EmployeeNamelbl"
	Me.EmployeeNamelbl.Size = New System.Drawing.Size(120, 20)
	Me.EmployeeNamelbl.TabIndex = 0
	Me.EmployeeNamelbl.Text = "EmployeeName"
	Me.EmployeeNamelbl.TextAlign = System.Drawing.ContentAlignment.MiddleRight

	'EmployeeName
	Me.EmployeeName.AutoSize = True
	Me.EmployeeName.Location = New System.Drawing.Point(135, 15)
	Me.EmployeeName.Name ="EmployeeName"
	Me.EmployeeName.Size = New System.Drawing.Size(200, 20)
	Me.EmployeeName.MaxLength = 255
	Me.EmployeeName.TabIndex = 0
	Me.EmployeeName.visible = True
	Me.EmployeeName.isMandatory = True
	Me.EmployeeName.AssociatedLabel = me.EmployeeNamelbl

	'EmployeeRankIdlbl.
	Me.EmployeeRankIdlbl.AutoSize = False
	Me.EmployeeRankIdlbl.Location = New System.Drawing.Point(5, 45)
	Me.EmployeeRankIdlbl.Name = "EmployeeRankIdlbl"
	Me.EmployeeRankIdlbl.Size = New System.Drawing.Size(120, 20)
	Me.EmployeeRankIdlbl.TabIndex = 1
	Me.EmployeeRankIdlbl.Text = "EmployeeRankId"
	Me.EmployeeRankIdlbl.TextAlign = System.Drawing.ContentAlignment.MiddleRight

	'EmployeeRankId
	Me.EmployeeRankId.AutoSize = True
	Me.EmployeeRankId.Location = New System.Drawing.Point(135, 45)
	Me.EmployeeRankId.Name ="EmployeeRankId"
	Me.EmployeeRankId.Size = New System.Drawing.Size(200, 20)
	Me.EmployeeRankId.MaxLength = 255
	Me.EmployeeRankId.TabIndex = 1
	Me.EmployeeRankId.visible = True
	Me.EmployeeRankId.isMandatory = True
	Me.EmployeeRankId.AssociatedLabel = me.EmployeeRankIdlbl

	'Salarylbl.
	Me.Salarylbl.AutoSize = False
	Me.Salarylbl.Location = New System.Drawing.Point(5, 75)
	Me.Salarylbl.Name = "Salarylbl"
	Me.Salarylbl.Size = New System.Drawing.Size(120, 20)
	Me.Salarylbl.TabIndex = 2
	Me.Salarylbl.Text = "Salary"
	Me.Salarylbl.TextAlign = System.Drawing.ContentAlignment.MiddleRight

	'Salary
	Me.Salary.AutoSize = True
	Me.Salary.Location = New System.Drawing.Point(135, 75)
	Me.Salary.Name ="Salary"
	Me.Salary.Size = New System.Drawing.Size(200, 20)
	Me.Salary.MaxLength = 10
	Me.Salary.FormatPattern = "00000000.00"
	Me.Salary.TabIndex = 2
	Me.Salary.visible = True
	Me.Salary.AssociatedLabel = me.Salarylbl

	'Addresslbl.
	Me.Addresslbl.AutoSize = False
	Me.Addresslbl.Location = New System.Drawing.Point(5, 105)
	Me.Addresslbl.Name = "Addresslbl"
	Me.Addresslbl.Size = New System.Drawing.Size(120, 20)
	Me.Addresslbl.TabIndex = 3
	Me.Addresslbl.Text = "Address"
	Me.Addresslbl.TextAlign = System.Drawing.ContentAlignment.MiddleRight

	'Address
	Me.Address.AutoSize = True
	Me.Address.Location = New System.Drawing.Point(135, 105)
	Me.Address.Name ="Address"
	Me.Address.Size = New System.Drawing.Size(200, 20)
	Me.Address.MaxLength = 255
	Me.Address.TabIndex = 3
	Me.Address.visible = True
	Me.Address.AssociatedLabel = me.Addresslbl

	'Telephonelbl.
	Me.Telephonelbl.AutoSize = False
	Me.Telephonelbl.Location = New System.Drawing.Point(5, 135)
	Me.Telephonelbl.Name = "Telephonelbl"
	Me.Telephonelbl.Size = New System.Drawing.Size(120, 20)
	Me.Telephonelbl.TabIndex = 4
	Me.Telephonelbl.Text = "Telephone"
	Me.Telephonelbl.TextAlign = System.Drawing.ContentAlignment.MiddleRight

	'Telephone
	Me.Telephone.AutoSize = True
	Me.Telephone.Location = New System.Drawing.Point(135, 135)
	Me.Telephone.Name ="Telephone"
	Me.Telephone.Size = New System.Drawing.Size(200, 20)
	Me.Telephone.MaxLength = 255
	Me.Telephone.TabIndex = 4
	Me.Telephone.visible = True
	Me.Telephone.AssociatedLabel = me.Telephonelbl

	'Mobilelbl.
	Me.Mobilelbl.AutoSize = False
	Me.Mobilelbl.Location = New System.Drawing.Point(5, 165)
	Me.Mobilelbl.Name = "Mobilelbl"
	Me.Mobilelbl.Size = New System.Drawing.Size(120, 20)
	Me.Mobilelbl.TabIndex = 5
	Me.Mobilelbl.Text = "Mobile"
	Me.Mobilelbl.TextAlign = System.Drawing.ContentAlignment.MiddleRight

	'Mobile
	Me.Mobile.AutoSize = True
	Me.Mobile.Location = New System.Drawing.Point(135, 165)
	Me.Mobile.Name ="Mobile"
	Me.Mobile.Size = New System.Drawing.Size(200, 20)
	Me.Mobile.MaxLength = 255
	Me.Mobile.TabIndex = 5
	Me.Mobile.visible = True
	Me.Mobile.AssociatedLabel = me.Mobilelbl

	'IdNumberlbl.
	Me.IdNumberlbl.AutoSize = False
	Me.IdNumberlbl.Location = New System.Drawing.Point(5, 195)
	Me.IdNumberlbl.Name = "IdNumberlbl"
	Me.IdNumberlbl.Size = New System.Drawing.Size(120, 20)
	Me.IdNumberlbl.TabIndex = 6
	Me.IdNumberlbl.Text = "IdNumber"
	Me.IdNumberlbl.TextAlign = System.Drawing.ContentAlignment.MiddleRight

	'IdNumber
	Me.IdNumber.AutoSize = True
	Me.IdNumber.Location = New System.Drawing.Point(135, 195)
	Me.IdNumber.Name ="IdNumber"
	Me.IdNumber.Size = New System.Drawing.Size(200, 20)
	Me.IdNumber.MaxLength = 255
	Me.IdNumber.TabIndex = 6
	Me.IdNumber.visible = True
	Me.IdNumber.AssociatedLabel = me.IdNumberlbl

	'SSINumberlbl.
	Me.SSINumberlbl.AutoSize = False
	Me.SSINumberlbl.Location = New System.Drawing.Point(5, 225)
	Me.SSINumberlbl.Name = "SSINumberlbl"
	Me.SSINumberlbl.Size = New System.Drawing.Size(120, 20)
	Me.SSINumberlbl.TabIndex = 7
	Me.SSINumberlbl.Text = "SSINumber"
	Me.SSINumberlbl.TextAlign = System.Drawing.ContentAlignment.MiddleRight

	'SSINumber
	Me.SSINumber.AutoSize = True
	Me.SSINumber.Location = New System.Drawing.Point(135, 225)
	Me.SSINumber.Name ="SSINumber"
	Me.SSINumber.Size = New System.Drawing.Size(200, 20)
	Me.SSINumber.MaxLength = 255
	Me.SSINumber.TabIndex = 7
	Me.SSINumber.visible = True
	Me.SSINumber.AssociatedLabel = me.SSINumberlbl

	'HireDatelbl.
	Me.HireDatelbl.AutoSize = False
	Me.HireDatelbl.Location = New System.Drawing.Point(5, 255)
	Me.HireDatelbl.Name = "HireDatelbl"
	Me.HireDatelbl.Size = New System.Drawing.Size(120, 20)
	Me.HireDatelbl.TabIndex = 8
	Me.HireDatelbl.Text = "HireDate"
	Me.HireDatelbl.TextAlign = System.Drawing.ContentAlignment.MiddleRight

	'HireDate
	Me.HireDate.AutoSize = True
	Me.HireDate.Location = New System.Drawing.Point(135, 255)
	Me.HireDate.Name ="HireDate"
	Me.HireDate.Size = New System.Drawing.Size(200, 20)
	Me.HireDate.MaxLength = 255
	Me.HireDate.TabIndex = 8
	Me.HireDate.visible = True
	Me.HireDate.AssociatedLabel = me.HireDatelbl

	'NumDependentslbl.
	Me.NumDependentslbl.AutoSize = False
	Me.NumDependentslbl.Location = New System.Drawing.Point(5, 285)
	Me.NumDependentslbl.Name = "NumDependentslbl"
	Me.NumDependentslbl.Size = New System.Drawing.Size(120, 20)
	Me.NumDependentslbl.TabIndex = 9
	Me.NumDependentslbl.Text = "NumDependents"
	Me.NumDependentslbl.TextAlign = System.Drawing.ContentAlignment.MiddleRight

	'NumDependents
	Me.NumDependents.AutoSize = True
	Me.NumDependents.Location = New System.Drawing.Point(135, 285)
	Me.NumDependents.Name ="NumDependents"
	Me.NumDependents.Size = New System.Drawing.Size(200, 20)
	Me.NumDependents.MaxLength = 255
	Me.NumDependents.TabIndex = 9
	Me.NumDependents.visible = True
	Me.NumDependents.AssociatedLabel = me.NumDependentslbl

	'EmployeeTypeCodelbl.
	Me.EmployeeTypeCodelbl.AutoSize = False
	Me.EmployeeTypeCodelbl.Location = New System.Drawing.Point(5, 315)
	Me.EmployeeTypeCodelbl.Name = "EmployeeTypeCodelbl"
	Me.EmployeeTypeCodelbl.Size = New System.Drawing.Size(120, 20)
	Me.EmployeeTypeCodelbl.TabIndex = 10
	Me.EmployeeTypeCodelbl.Text = "EmployeeTypeCode"
	Me.EmployeeTypeCodelbl.TextAlign = System.Drawing.ContentAlignment.MiddleRight

	'EmployeeTypeCode
	Me.EmployeeTypeCode.AutoSize = True
	Me.EmployeeTypeCode.Location = New System.Drawing.Point(135, 315)
	Me.EmployeeTypeCode.Name ="EmployeeTypeCode"
	Me.EmployeeTypeCode.Size = New System.Drawing.Size(200, 20)
	Me.EmployeeTypeCode.MaxLength = 255
	Me.EmployeeTypeCode.TabIndex = 10
	Me.EmployeeTypeCode.visible = True
	Me.EmployeeTypeCode.AssociatedLabel = me.EmployeeTypeCodelbl

	'SampleGuidFieldlbl.
	Me.SampleGuidFieldlbl.AutoSize = False
	Me.SampleGuidFieldlbl.Location = New System.Drawing.Point(5, 345)
	Me.SampleGuidFieldlbl.Name = "SampleGuidFieldlbl"
	Me.SampleGuidFieldlbl.Size = New System.Drawing.Size(120, 20)
	Me.SampleGuidFieldlbl.TabIndex = 11
	Me.SampleGuidFieldlbl.Text = "SampleGuidField"
	Me.SampleGuidFieldlbl.TextAlign = System.Drawing.ContentAlignment.MiddleRight

	'SampleGuidField
	Me.SampleGuidField.AutoSize = True
	Me.SampleGuidField.Location = New System.Drawing.Point(135, 345)
	Me.SampleGuidField.Name ="SampleGuidField"
	Me.SampleGuidField.Size = New System.Drawing.Size(200, 20)
	Me.SampleGuidField.MaxLength = 255
	Me.SampleGuidField.TabIndex = 11
	Me.SampleGuidField.visible = True
	Me.SampleGuidField.AssociatedLabel = me.SampleGuidFieldlbl


		
        'ucEmployee
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
       	me.Controls.add(me.EmployeeNamelbl)
	me.Controls.add(me.EmployeeName)
	me.Controls.add(me.EmployeeRankIdlbl)
	me.Controls.add(me.EmployeeRankId)
	me.Controls.add(me.Salarylbl)
	me.Controls.add(me.Salary)
	me.Controls.add(me.Addresslbl)
	me.Controls.add(me.Address)
	me.Controls.add(me.Telephonelbl)
	me.Controls.add(me.Telephone)
	me.Controls.add(me.Mobilelbl)
	me.Controls.add(me.Mobile)
	me.Controls.add(me.IdNumberlbl)
	me.Controls.add(me.IdNumber)
	me.Controls.add(me.SSINumberlbl)
	me.Controls.add(me.SSINumber)
	me.Controls.add(me.HireDatelbl)
	me.Controls.add(me.HireDate)
	me.Controls.add(me.NumDependentslbl)
	me.Controls.add(me.NumDependents)
	me.Controls.add(me.EmployeeTypeCodelbl)
	me.Controls.add(me.EmployeeTypeCode)
	me.Controls.add(me.SampleGuidFieldlbl)
	me.Controls.add(me.SampleGuidField)

		
		Me.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
        Me.Dock = DockStyle.Fill
        Me.Name = "ucEmployeeDetails"
        Me.Size = New System.Drawing.Size(548, 150)
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
	Friend WithEvents EmployeeTypeCodelbl As System.Windows.Forms.Label
	Friend WithEvents EmployeeTypeCode As CGTextBox
	Friend WithEvents SampleGuidFieldlbl As System.Windows.Forms.Label
	Friend WithEvents SampleGuidField As CGTextBox


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
	Me.EmployeeTypeCode.value = mo.EmployeeTypeCode
	Me.SampleGuidField.value = mo.SampleGuidField


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
	mo.setEmployeeTypeCode(Me.EmployeeTypeCode.text)
	mo.setSampleGuidField(Me.SampleGuidField.text)


    End Sub

    Public Sub _InitializeControl() Handles Me.InitializeControl

        If me.isInitialized = False Then
			'setup datasources
				Me.EmployeeRankId.DataSource = new VbBusObjects.DBMappers.EmployeeRankDBMapper().findAll()
	Me.EmployeeRankId.DisplayMember = "Rank"
	Me.EmployeeRankId.ValueMember = "RankId"

			me.isInitialized = true
		End if

	End Sub

#End Region


End Class


