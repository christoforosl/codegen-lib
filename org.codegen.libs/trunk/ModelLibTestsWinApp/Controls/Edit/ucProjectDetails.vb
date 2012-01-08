Imports System.ComponentModel

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
        Me.components = New System.ComponentModel.Container()
        Me.ProjectNamelbl = New System.Windows.Forms.Label()
        Me.ProjectName = New org.codegen.win.controls.CGTextBox(Me.components)
        Me.IsActivelbl = New System.Windows.Forms.Label()
        Me.IsActive = New org.codegen.win.controls.CGCheckBox(Me.components)
        CType(Me.ErrProvider, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ProjectNamelbl
        '
        Me.ProjectNamelbl.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ProjectNamelbl.Location = New System.Drawing.Point(5, 15)
        Me.ProjectNamelbl.Name = "ProjectNamelbl"
        Me.ProjectNamelbl.Size = New System.Drawing.Size(120, 20)
        Me.ProjectNamelbl.TabIndex = 0
        Me.ProjectNamelbl.Text = "ProjectName"
        Me.ProjectNamelbl.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'ProjectName
        '
        Me.ProjectName.AssociatedLabel = Me.ProjectNamelbl
        Me.ProjectName.BackColor = System.Drawing.Color.LightYellow
        Me.ProjectName.ErrProvider = Nothing
        Me.ProjectName.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ProjectName.FormatPattern = Nothing
        Me.ProjectName.isMandatory = True
        Me.ProjectName.Location = New System.Drawing.Point(135, 15)
        Me.ProjectName.MaxLength = 255
        Me.ProjectName.MaxValue = Nothing
        Me.ProjectName.MinValue = Nothing
        Me.ProjectName.Name = "ProjectName"
        Me.ProjectName.Size = New System.Drawing.Size(200, 21)
        Me.ProjectName.TabIndex = 0
        '
        'IsActivelbl
        '
        Me.IsActivelbl.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.IsActivelbl.Location = New System.Drawing.Point(5, 45)
        Me.IsActivelbl.Name = "IsActivelbl"
        Me.IsActivelbl.Size = New System.Drawing.Size(120, 20)
        Me.IsActivelbl.TabIndex = 1
        Me.IsActivelbl.Text = "IsActive"
        Me.IsActivelbl.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'IsActive
        '
        Me.IsActive.AssociatedLabel = Me.IsActivelbl
        Me.IsActive.AutoSize = True
        Me.IsActive.BackColor = System.Drawing.Color.Transparent
        Me.IsActive.ErrProvider = Nothing
        Me.IsActive.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.IsActive.intValue = 0
        Me.IsActive.isMandatory = False
        Me.IsActive.Location = New System.Drawing.Point(135, 45)
        Me.IsActive.MaxValue = Nothing
        Me.IsActive.MinValue = Nothing
        Me.IsActive.Name = "IsActive"
        Me.IsActive.ReadOnly = False
        Me.IsActive.Size = New System.Drawing.Size(53, 17)
        Me.IsActive.TabIndex = 1
        Me.IsActive.Text = "false"
        Me.IsActive.UseVisualStyleBackColor = False
        Me.IsActive.value = False
        '
        'ucProjectDetails
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
        Me.Controls.Add(Me.ProjectNamelbl)
        Me.Controls.Add(Me.ProjectName)
        Me.Controls.Add(Me.IsActivelbl)
        Me.Controls.Add(Me.IsActive)
        Me.Name = "ucProjectDetails"
        Me.Size = New System.Drawing.Size(775, 308)
        CType(Me.ErrProvider, System.ComponentModel.ISupportInitialize).EndInit()
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


