Imports System
Imports System.ComponentModel
Imports System.ComponentModel.Design
Imports System.Diagnostics
Imports System.Drawing
Imports System.Drawing.Design
Imports System.Threading
Imports System.Windows.Forms
Imports System.Windows.Forms.Design

Namespace UIComponents


    Public Class TitleStrip
        Inherits ToolStrip

        Private _renderer As TitleStripRenderer
        Private _headerLabel As ToolStripLabel

        <System.Diagnostics.DebuggerNonUserCode()> _
        Public Sub New(ByVal container As System.ComponentModel.IContainer)
            MyClass.New()

            'Required for Windows.Forms Class Composition Designer support
            If (container IsNot Nothing) Then
                container.Add(Me)
            End If

        End Sub

        <System.Diagnostics.DebuggerNonUserCode()> _
        Public Sub New()
            MyBase.New()

            'This call is required by the Component Designer.

            InitializeComponent()

        End Sub

        'Component overrides dispose to clean up the component list.
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

        'Required by the Component Designer
        Private components As System.ComponentModel.IContainer

        'NOTE: The following procedure is required by the Component Designer
        'It can be modified using the Component Designer.
        'Do not modify it using the code editor.
        <System.Diagnostics.DebuggerStepThrough()> _
        Private Sub InitializeComponent()

            components = New System.ComponentModel.Container()

            Me.Dock = DockStyle.Top
            Me.GripStyle = ToolStripGripStyle.Hidden

            ' Set renderer
            _renderer = New TitleStripRenderer()

            ' Look for headerText
            ' Add children
            _headerLabel = New ToolStripLabel()

            ' Add Label and Image
            _headerLabel.DisplayStyle = ToolStripItemDisplayStyle.Text
            _headerLabel.Text = "[Enter Text]"

            Me.Items.AddRange(New ToolStripItem() {_headerLabel})

            ' Hook Draw Events
            Me.Renderer = _renderer

        End Sub



#Region "Public Properties"
        Public ReadOnly Property HeaderText() As ToolStripLabel
            Get
                Return _headerLabel
            End Get
        End Property


        Public Property GradientStartColor() As Color
            Get
                Return _renderer.StartColor
            End Get
            Set(ByVal value As Color)
                _renderer.StartColor = value
            End Set
        End Property

        Public Property Lines() As Int16
            Get
                Return _renderer.Lines
            End Get
            Set(ByVal value As Int16)
                _renderer.Lines = value
            End Set
        End Property

        Public Property GradientEndColor() As Color
            Get
                Return _renderer.EndColor
            End Get
            Set(ByVal value As Color)
                _renderer.EndColor = value
            End Set
        End Property


        Public Property DrawEndLine() As Boolean
            Get
                Return _renderer.DrawEndLine
            End Get
            Set(ByVal value As Boolean)
                _renderer.DrawEndLine = value
            End Set
        End Property

        Public Overrides Property Text As String
            Get
                Return Me._headerLabel.Text
            End Get
            Set(ByVal value As String)
                Me._headerLabel.Text = value
            End Set
        End Property

#End Region

    End Class
End Namespace
