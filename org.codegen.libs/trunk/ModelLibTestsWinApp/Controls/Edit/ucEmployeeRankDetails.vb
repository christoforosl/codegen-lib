Imports System.ComponentModel

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Class ucEmployeeRankDetails
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

       
	Me.Ranklbl = New System.Windows.Forms.Label
	Me.Rank = New CGTextBox


        Me.SuspendLayout()
			'Ranklbl.
	Me.Ranklbl.AutoSize = False
	Me.Ranklbl.Location = New System.Drawing.Point(5, 15)
	Me.Ranklbl.Name = "Ranklbl"
	Me.Ranklbl.Size = New System.Drawing.Size(120, 20)
	Me.Ranklbl.TabIndex = 0
	Me.Ranklbl.Text = "Rank"
	Me.Ranklbl.TextAlign = System.Drawing.ContentAlignment.MiddleRight

	'Rank
	Me.Rank.AutoSize = True
	Me.Rank.Location = New System.Drawing.Point(135, 15)
	Me.Rank.Name ="Rank"
	Me.Rank.Size = New System.Drawing.Size(200, 20)
	Me.Rank.MaxLength = 255
	Me.Rank.TabIndex = 0
	Me.Rank.visible = True
	Me.Rank.isMandatory = True
	Me.Rank.AssociatedLabel = me.Ranklbl


		
        'ucEmployeeRank
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
       	me.Controls.add(me.Ranklbl)
	me.Controls.add(me.Rank)

		
		Me.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
        Me.Dock = DockStyle.Fill
        Me.Name = "ucEmployeeRank"
        Me.Size = New System.Drawing.Size(548, 150)
		Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

	
	
	Friend WithEvents Ranklbl As System.Windows.Forms.Label
	Friend WithEvents Rank As CGTextBox


#End Region

#Region "Methods"
    
	 ''' <summary>
    ''' Fills the controls on the screen from data in the object
    ''' </summary>
    ''' <remarks></remarks>
	Public Sub _loadData() Handles Me.evLoadObjectData

        Dim mo As EmployeeRank = DirectCast(Me.ModelObject(), EmployeeRank)
        	Me.Rank.value = mo.Rank


		me.resetLastLoadedValues()
		Me.ErrProvider.Clear()

    End Sub


    ''' <summary>
	''' Loads the object from the database and then sets the proeperties 
	''' of the object from values on the controls
	''' </summary>
	''' <remarks></remarks>
	Public Sub _loadToObject() Handles Me.evLoadToObject

        Dim mo As EmployeeRank = DirectCast(me.ModelObject, EmployeeRank)
        	mo.setRank(Me.Rank.text)


    End Sub

    Public Sub _InitializeControl() Handles Me.InitializeControl

        If me.isInitialized = False Then
			'setup datasources
			
			me.isInitialized = true
		End if

	End Sub

#End Region


End Class


