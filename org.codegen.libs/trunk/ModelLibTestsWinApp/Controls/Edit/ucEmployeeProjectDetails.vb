Imports System.ComponentModel
'EditControlTemplate.txt
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Class ucEmployeeProjectDetails
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

       
	Me.EPEmployeeIdlbl = New System.Windows.Forms.Label
	Me.EPEmployeeId = New CGIntTextBox
	Me.EPProjectIdlbl = New System.Windows.Forms.Label
	Me.EPProjectId = New CGIntTextBox
	Me.AssignDatelbl = New System.Windows.Forms.Label
	Me.AssignDate = New CGDateTextBox
	Me.EndDatelbl = New System.Windows.Forms.Label
	Me.EndDate = New CGDateTextBox
	Me.Ratelbl = New System.Windows.Forms.Label
	Me.Rate = New CGDecimalTextBox


        Me.SuspendLayout()
			'EPEmployeeIdlbl.
	Me.EPEmployeeIdlbl.AutoSize = False
	Me.EPEmployeeIdlbl.Location = New System.Drawing.Point(5, 15)
	Me.EPEmployeeIdlbl.Name = "EPEmployeeIdlbl"
	Me.EPEmployeeIdlbl.Size = New System.Drawing.Size(120, 20)
	Me.EPEmployeeIdlbl.TabIndex = 0
	Me.EPEmployeeIdlbl.Text = "EPEmployeeId"
	Me.EPEmployeeIdlbl.TextAlign = System.Drawing.ContentAlignment.MiddleRight

	'EPEmployeeId
	Me.EPEmployeeId.AutoSize = True
	Me.EPEmployeeId.Location = New System.Drawing.Point(135, 15)
	Me.EPEmployeeId.Name ="EPEmployeeId"
	Me.EPEmployeeId.Size = New System.Drawing.Size(200, 20)
	Me.EPEmployeeId.MaxLength = 255
	Me.EPEmployeeId.TabIndex = 0
	Me.EPEmployeeId.visible = True
	Me.EPEmployeeId.isMandatory = True
	Me.EPEmployeeId.AssociatedLabel = me.EPEmployeeIdlbl

	'EPProjectIdlbl.
	Me.EPProjectIdlbl.AutoSize = False
	Me.EPProjectIdlbl.Location = New System.Drawing.Point(5, 45)
	Me.EPProjectIdlbl.Name = "EPProjectIdlbl"
	Me.EPProjectIdlbl.Size = New System.Drawing.Size(120, 20)
	Me.EPProjectIdlbl.TabIndex = 1
	Me.EPProjectIdlbl.Text = "EPProjectId"
	Me.EPProjectIdlbl.TextAlign = System.Drawing.ContentAlignment.MiddleRight

	'EPProjectId
	Me.EPProjectId.AutoSize = True
	Me.EPProjectId.Location = New System.Drawing.Point(135, 45)
	Me.EPProjectId.Name ="EPProjectId"
	Me.EPProjectId.Size = New System.Drawing.Size(200, 20)
	Me.EPProjectId.MaxLength = 255
	Me.EPProjectId.TabIndex = 1
	Me.EPProjectId.visible = True
	Me.EPProjectId.isMandatory = True
	Me.EPProjectId.AssociatedLabel = me.EPProjectIdlbl

	'AssignDatelbl.
	Me.AssignDatelbl.AutoSize = False
	Me.AssignDatelbl.Location = New System.Drawing.Point(5, 75)
	Me.AssignDatelbl.Name = "AssignDatelbl"
	Me.AssignDatelbl.Size = New System.Drawing.Size(120, 20)
	Me.AssignDatelbl.TabIndex = 2
	Me.AssignDatelbl.Text = "AssignDate"
	Me.AssignDatelbl.TextAlign = System.Drawing.ContentAlignment.MiddleRight

	'AssignDate
	Me.AssignDate.AutoSize = True
	Me.AssignDate.Location = New System.Drawing.Point(135, 75)
	Me.AssignDate.Name ="AssignDate"
	Me.AssignDate.Size = New System.Drawing.Size(200, 20)
	Me.AssignDate.MaxLength = 255
	Me.AssignDate.TabIndex = 2
	Me.AssignDate.visible = True
	Me.AssignDate.AssociatedLabel = me.AssignDatelbl

	'EndDatelbl.
	Me.EndDatelbl.AutoSize = False
	Me.EndDatelbl.Location = New System.Drawing.Point(5, 105)
	Me.EndDatelbl.Name = "EndDatelbl"
	Me.EndDatelbl.Size = New System.Drawing.Size(120, 20)
	Me.EndDatelbl.TabIndex = 3
	Me.EndDatelbl.Text = "EndDate"
	Me.EndDatelbl.TextAlign = System.Drawing.ContentAlignment.MiddleRight

	'EndDate
	Me.EndDate.AutoSize = True
	Me.EndDate.Location = New System.Drawing.Point(135, 105)
	Me.EndDate.Name ="EndDate"
	Me.EndDate.Size = New System.Drawing.Size(200, 20)
	Me.EndDate.MaxLength = 255
	Me.EndDate.TabIndex = 3
	Me.EndDate.visible = True
	Me.EndDate.AssociatedLabel = me.EndDatelbl

	'Ratelbl.
	Me.Ratelbl.AutoSize = False
	Me.Ratelbl.Location = New System.Drawing.Point(5, 135)
	Me.Ratelbl.Name = "Ratelbl"
	Me.Ratelbl.Size = New System.Drawing.Size(120, 20)
	Me.Ratelbl.TabIndex = 4
	Me.Ratelbl.Text = "Rate"
	Me.Ratelbl.TextAlign = System.Drawing.ContentAlignment.MiddleRight

	'Rate
	Me.Rate.AutoSize = True
	Me.Rate.Location = New System.Drawing.Point(135, 135)
	Me.Rate.Name ="Rate"
	Me.Rate.Size = New System.Drawing.Size(200, 20)
	Me.Rate.MaxLength = 10
	Me.Rate.FormatPattern = "00000000.00"
	Me.Rate.TabIndex = 4
	Me.Rate.visible = True
	Me.Rate.AssociatedLabel = me.Ratelbl


		
        'ucEmployeeProject
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
       	me.Controls.add(me.EPEmployeeIdlbl)
	me.Controls.add(me.EPEmployeeId)
	me.Controls.add(me.EPProjectIdlbl)
	me.Controls.add(me.EPProjectId)
	me.Controls.add(me.AssignDatelbl)
	me.Controls.add(me.AssignDate)
	me.Controls.add(me.EndDatelbl)
	me.Controls.add(me.EndDate)
	me.Controls.add(me.Ratelbl)
	me.Controls.add(me.Rate)

		
		Me.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
        Me.Dock = DockStyle.Fill
        Me.Name = "ucEmployeeProjectDetails"
        Me.Size = New System.Drawing.Size(548, 150)
		Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

	
	
	Friend WithEvents EPEmployeeIdlbl As System.Windows.Forms.Label
	Friend WithEvents EPEmployeeId As CGIntTextBox
	Friend WithEvents EPProjectIdlbl As System.Windows.Forms.Label
	Friend WithEvents EPProjectId As CGIntTextBox
	Friend WithEvents AssignDatelbl As System.Windows.Forms.Label
	Friend WithEvents AssignDate As CGDateTextBox
	Friend WithEvents EndDatelbl As System.Windows.Forms.Label
	Friend WithEvents EndDate As CGDateTextBox
	Friend WithEvents Ratelbl As System.Windows.Forms.Label
	Friend WithEvents Rate As CGDecimalTextBox


#End Region

#Region "Methods"
    
	 ''' <summary>
    ''' Fills the controls on the screen from data in the object
    ''' </summary>
    ''' <remarks></remarks>
	Public Sub _loadData() Handles Me.evLoadObjectData

        Dim mo As EmployeeProject = DirectCast(Me.ModelObject(), EmployeeProject)
        	Me.EPEmployeeId.value = mo.EPEmployeeId
	Me.EPProjectId.value = mo.EPProjectId
	Me.AssignDate.value = mo.AssignDate
	Me.EndDate.value = mo.EndDate
	Me.Rate.value = mo.Rate


		me.resetLastLoadedValues()
		Me.ErrProvider.Clear()

    End Sub


    ''' <summary>
	''' Loads the object from the database and then sets the proeperties 
	''' of the object from values on the controls
	''' </summary>
	''' <remarks></remarks>
	Public Sub _loadToObject() Handles Me.evLoadToObject

        Dim mo As EmployeeProject = DirectCast(me.ModelObject, EmployeeProject)
        	mo.setEPEmployeeId(Me.EPEmployeeId.text)
	mo.setEPProjectId(Me.EPProjectId.text)
	mo.setAssignDate(Me.AssignDate.text)
	mo.setEndDate(Me.EndDate.text)
	mo.setRate(Me.Rate.text)


    End Sub

    Public Sub _InitializeControl() Handles Me.InitializeControl

        If me.isInitialized = False Then
			'setup datasources
			
			me.isInitialized = true
		End if

	End Sub

#End Region


End Class


