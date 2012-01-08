Imports System.ComponentModel

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Class ucEmployeeInfoDetails
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

       
	Me.EIEmployeeIdlbl = New System.Windows.Forms.Label
	Me.EIEmployeeId = New CGIntTextBox
	Me.Salarylbl = New System.Windows.Forms.Label
	Me.Salary = New CGDecimalTextBox
	Me.Addresslbl = New System.Windows.Forms.Label
	Me.Address = New CGTextBox


        Me.SuspendLayout()
			'EIEmployeeIdlbl.
	Me.EIEmployeeIdlbl.AutoSize = False
	Me.EIEmployeeIdlbl.Location = New System.Drawing.Point(5, 15)
	Me.EIEmployeeIdlbl.Name = "EIEmployeeIdlbl"
	Me.EIEmployeeIdlbl.Size = New System.Drawing.Size(120, 20)
	Me.EIEmployeeIdlbl.TabIndex = 0
	Me.EIEmployeeIdlbl.Text = "EIEmployeeId"
	Me.EIEmployeeIdlbl.TextAlign = System.Drawing.ContentAlignment.MiddleRight

	'EIEmployeeId
	Me.EIEmployeeId.AutoSize = True
	Me.EIEmployeeId.Location = New System.Drawing.Point(135, 15)
	Me.EIEmployeeId.Name ="EIEmployeeId"
	Me.EIEmployeeId.Size = New System.Drawing.Size(200, 20)
	Me.EIEmployeeId.MaxLength = 255
	Me.EIEmployeeId.TabIndex = 0
	Me.EIEmployeeId.visible = True
	Me.EIEmployeeId.isMandatory = True
	Me.EIEmployeeId.AssociatedLabel = me.EIEmployeeIdlbl

	'Salarylbl.
	Me.Salarylbl.AutoSize = False
	Me.Salarylbl.Location = New System.Drawing.Point(5, 45)
	Me.Salarylbl.Name = "Salarylbl"
	Me.Salarylbl.Size = New System.Drawing.Size(120, 20)
	Me.Salarylbl.TabIndex = 1
	Me.Salarylbl.Text = "Salary"
	Me.Salarylbl.TextAlign = System.Drawing.ContentAlignment.MiddleRight

	'Salary
	Me.Salary.AutoSize = True
	Me.Salary.Location = New System.Drawing.Point(135, 45)
	Me.Salary.Name ="Salary"
	Me.Salary.Size = New System.Drawing.Size(200, 20)
	Me.Salary.MaxLength = 2
	Me.Salary.TabIndex = 1
	Me.Salary.visible = True
	Me.Salary.AssociatedLabel = me.Salarylbl

	'Addresslbl.
	Me.Addresslbl.AutoSize = False
	Me.Addresslbl.Location = New System.Drawing.Point(5, 75)
	Me.Addresslbl.Name = "Addresslbl"
	Me.Addresslbl.Size = New System.Drawing.Size(120, 20)
	Me.Addresslbl.TabIndex = 2
	Me.Addresslbl.Text = "Address"
	Me.Addresslbl.TextAlign = System.Drawing.ContentAlignment.MiddleRight

	'Address
	Me.Address.AutoSize = True
	Me.Address.Location = New System.Drawing.Point(135, 75)
	Me.Address.Name ="Address"
	Me.Address.Size = New System.Drawing.Size(200, 20)
	Me.Address.MaxLength = 255
	Me.Address.TabIndex = 2
	Me.Address.visible = True
	Me.Address.AssociatedLabel = me.Addresslbl


		
        'ucEmployeeInfo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
       	me.Controls.add(me.EIEmployeeIdlbl)
	me.Controls.add(me.EIEmployeeId)
	me.Controls.add(me.Salarylbl)
	me.Controls.add(me.Salary)
	me.Controls.add(me.Addresslbl)
	me.Controls.add(me.Address)

		
		Me.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
        Me.Dock = DockStyle.Fill
        Me.Name = "ucEmployeeInfo"
        Me.Size = New System.Drawing.Size(548, 150)
		Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

	
	
	Friend WithEvents EIEmployeeIdlbl As System.Windows.Forms.Label
	Friend WithEvents EIEmployeeId As CGIntTextBox
	Friend WithEvents Salarylbl As System.Windows.Forms.Label
	Friend WithEvents Salary As CGDecimalTextBox
	Friend WithEvents Addresslbl As System.Windows.Forms.Label
	Friend WithEvents Address As CGTextBox


#End Region

#Region "Methods"
    
	 ''' <summary>
    ''' Fills the controls on the screen from data in the object
    ''' </summary>
    ''' <remarks></remarks>
	Public Sub _loadData() Handles Me.evLoadObjectData

        Dim mo As EmployeeInfo = DirectCast(Me.ModelObject(), EmployeeInfo)
        	Me.EIEmployeeId.value = mo.EIEmployeeId
	Me.Salary.value = mo.Salary
	Me.Address.value = mo.Address


		me.resetLastLoadedValues()
		Me.ErrProvider.Clear()

    End Sub


    ''' <summary>
	''' Loads the object from the database and then sets the proeperties 
	''' of the object from values on the controls
	''' </summary>
	''' <remarks></remarks>
	Public Sub _loadToObject() Handles Me.evLoadToObject

        Dim mo As EmployeeInfo = DirectCast(me.ModelObject, EmployeeInfo)
        	mo.setEIEmployeeId(Me.EIEmployeeId.text)
	mo.setSalary(Me.Salary.text)
	mo.setAddress(Me.Address.text)


    End Sub

    Public Sub _InitializeControl() Handles Me.InitializeControl

        If me.isInitialized = False Then
			'setup datasources
			
			me.isInitialized = true
		End if

	End Sub

#End Region


End Class


