Imports System.ComponentModel
'EditControlTemplate.txt
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Class ucProjectDetails
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

       
	Me.ProjectNamelbl = New System.Windows.Forms.Label
	Me.ProjectName = New CGTextBox
	Me.IsActivelbl = New System.Windows.Forms.Label
	Me.IsActive = New CGCheckBox


        Me.SuspendLayout()
			'ProjectNamelbl.
	Me.ProjectNamelbl.AutoSize = False
	Me.ProjectNamelbl.Location = New System.Drawing.Point(5, 15)
	Me.ProjectNamelbl.Name = "ProjectNamelbl"
	Me.ProjectNamelbl.Size = New System.Drawing.Size(120, 20)
	Me.ProjectNamelbl.TabIndex = 0
	Me.ProjectNamelbl.Text = "ProjectName"
	Me.ProjectNamelbl.TextAlign = System.Drawing.ContentAlignment.MiddleRight

	'ProjectName
	Me.ProjectName.AutoSize = True
	Me.ProjectName.Location = New System.Drawing.Point(135, 15)
	Me.ProjectName.Name ="ProjectName"
	Me.ProjectName.Size = New System.Drawing.Size(200, 20)
	Me.ProjectName.MaxLength = 255
	Me.ProjectName.TabIndex = 0
	Me.ProjectName.visible = True
	Me.ProjectName.isMandatory = True
	Me.ProjectName.AssociatedLabel = me.ProjectNamelbl

	'IsActivelbl.
	Me.IsActivelbl.AutoSize = False
	Me.IsActivelbl.Location = New System.Drawing.Point(5, 45)
	Me.IsActivelbl.Name = "IsActivelbl"
	Me.IsActivelbl.Size = New System.Drawing.Size(120, 20)
	Me.IsActivelbl.TabIndex = 1
	Me.IsActivelbl.Text = "IsActive"
	Me.IsActivelbl.TextAlign = System.Drawing.ContentAlignment.MiddleRight

	'IsActive
	Me.IsActive.AutoSize = True
	Me.IsActive.Location = New System.Drawing.Point(135, 45)
	Me.IsActive.Name ="IsActive"
	Me.IsActive.TabIndex = 1
	Me.IsActive.visible = True
	Me.IsActive.AssociatedLabel = me.IsActivelbl


		
        'ucProject
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
       	me.Controls.add(me.ProjectNamelbl)
	me.Controls.add(me.ProjectName)
	me.Controls.add(me.IsActivelbl)
	me.Controls.add(me.IsActive)

		
		Me.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
        Me.Dock = DockStyle.Fill
        Me.Name = "ucProjectDetails"
        Me.Size = New System.Drawing.Size(548, 150)
		Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

	
	
	Friend WithEvents ProjectNamelbl As System.Windows.Forms.Label
	Friend WithEvents ProjectName As CGTextBox
	Friend WithEvents IsActivelbl As System.Windows.Forms.Label
	Friend WithEvents IsActive As CGCheckBox


#End Region

#Region "Methods"
    
	 ''' <summary>
    ''' Fills the controls on the screen from data in the object
    ''' </summary>
    ''' <remarks></remarks>
	Public Sub _loadData() Handles Me.evLoadObjectData

        Dim mo As Project = DirectCast(Me.ModelObject(), Project)
        	Me.ProjectName.value = mo.ProjectName
	Me.IsActive.value = mo.IsActive


		me.resetLastLoadedValues()
		Me.ErrProvider.Clear()

    End Sub


    ''' <summary>
	''' Loads the object from the database and then sets the proeperties 
	''' of the object from values on the controls
	''' </summary>
	''' <remarks></remarks>
	Public Sub _loadToObject() Handles Me.evLoadToObject

        Dim mo As Project = DirectCast(me.ModelObject, Project)
        	mo.setProjectName(Me.ProjectName.text)
	mo.setIsActive(Me.IsActive.text)


    End Sub

    Public Sub _InitializeControl() Handles Me.InitializeControl

        If me.isInitialized = False Then
			'setup datasources
			
			me.isInitialized = true
		End if

	End Sub

#End Region


End Class


