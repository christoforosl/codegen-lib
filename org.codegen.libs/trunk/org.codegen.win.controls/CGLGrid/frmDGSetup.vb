Imports System
Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Drawing
Imports System.Runtime.CompilerServices
Imports System.Windows.Forms

Namespace Grid
    Public Class frmDGSetup
        Inherits Form
        ' Methods
        Public Sub New()
            Me.InitializeComponent()
        End Sub

        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If (disposing AndAlso (Not Me.components Is Nothing)) Then
                Me.components.Dispose()
            End If
            MyBase.Dispose(disposing)
        End Sub

        Private Sub grdSetup_DataError(ByVal sender As Object, ByVal e As DataGridViewDataErrorEventArgs)
            e.ThrowException = False
        End Sub

        <DebuggerStepThrough()> _
        Private Sub InitializeComponent()
            Me.grdSetup = New DataGridView
            Me.cmdOK = New Button
            Me.cmdCancel = New Button
            DirectCast(Me.grdSetup, ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            Me.grdSetup.AllowUserToAddRows = False
            Me.grdSetup.AllowUserToDeleteRows = False
            Me.grdSetup.AllowUserToOrderColumns = True
            Me.grdSetup.Anchor = (AnchorStyles.Right Or (AnchorStyles.Left Or (AnchorStyles.Bottom Or AnchorStyles.Top)))
            Me.grdSetup.BackgroundColor = SystemColors.Window
            Me.grdSetup.BorderStyle = BorderStyle.None
            Dim point As New Point(8, 10)
            Me.grdSetup.Location = point
            Me.grdSetup.Name = "grdSetup"
            Dim size As New Size(&H2EA, &H1BB)
            Me.grdSetup.Size = size
            Me.grdSetup.TabIndex = 1
            Me.cmdOK.Anchor = (AnchorStyles.Right Or AnchorStyles.Bottom)
            Me.cmdOK.DialogResult = DialogResult.OK
            point = New Point(&H282, &H1CD)
            Me.cmdOK.Location = point
            Me.cmdOK.Name = "cmdOK"
            size = New Size(&H70, &H20)
            Me.cmdOK.Size = size
            Me.cmdOK.TabIndex = 2
            Me.cmdOK.Text = "OK"
            Me.cmdCancel.Anchor = (AnchorStyles.Right Or AnchorStyles.Bottom)
            Me.cmdCancel.DialogResult = DialogResult.Cancel
            point = New Point(&H20A, &H1CD)
            Me.cmdCancel.Location = point
            Me.cmdCancel.Name = "cmdCancel"
            size = New Size(&H70, &H20)
            Me.cmdCancel.Size = size
            Me.cmdCancel.TabIndex = 3
            Me.cmdCancel.Text = "Cancel"
            size = New Size(5, 13)
            Me.AutoScaleBaseSize = size
            size = New Size(&H2FA, &H1F2)
            Me.ClientSize = size
            Me.Controls.Add(Me.cmdCancel)
            Me.Controls.Add(Me.cmdOK)
            Me.Controls.Add(Me.grdSetup)
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "frmDGSetup"
            Me.ShowInTaskbar = False
            Me.StartPosition = FormStartPosition.CenterParent
            Me.Text = "Column Setup"
            DirectCast(Me.grdSetup, ISupportInitialize).EndInit()
            Me.ResumeLayout(False)
        End Sub


        ' Properties
        Friend Overridable Property cmdCancel() As Button
            Get
                Return Me._cmdCancel
            End Get
            Set(ByVal WithEventsValue As Button)
                Me._cmdCancel = WithEventsValue
            End Set
        End Property

        Friend Overridable Property cmdOK() As Button
            Get
                Return Me._cmdOK
            End Get
            Set(ByVal WithEventsValue As Button)
                Me._cmdOK = WithEventsValue
            End Set
        End Property

        Friend Overridable Property f_alignment() As DataGridViewComboBoxColumn
            Get
                Return Me._f_alignment
            End Get
            Set(ByVal WithEventsValue As DataGridViewComboBoxColumn)
                Me._f_alignment = WithEventsValue
            End Set
        End Property

        Friend Overridable Property f_controlTypeID() As DataGridViewComboBoxColumn
            Get
                Return Me._f_controlTypeID
            End Get
            Set(ByVal WithEventsValue As DataGridViewComboBoxColumn)
                Me._f_controlTypeID = WithEventsValue
            End Set
        End Property

        Friend Overridable Property f_DataGridID() As DataGridViewTextBoxColumn
            Get
                Return Me._f_DataGridID
            End Get
            Set(ByVal WithEventsValue As DataGridViewTextBoxColumn)
                Me._f_DataGridID = WithEventsValue
            End Set
        End Property

        Friend Overridable Property f_DataGridSetupId() As DataGridViewTextBoxColumn
            Get
                Return Me._f_DataGridSetupId
            End Get
            Set(ByVal WithEventsValue As DataGridViewTextBoxColumn)
                Me._f_DataGridSetupId = WithEventsValue
            End Set
        End Property

        Friend Overridable Property f_datasource() As DataGridViewTextBoxColumn
            Get
                Return Me._f_datasource
            End Get
            Set(ByVal WithEventsValue As DataGridViewTextBoxColumn)
                Me._f_datasource = WithEventsValue
            End Set
        End Property

        Friend Overridable Property f_fieldcaption() As DataGridViewTextBoxColumn
            Get
                Return Me._f_fieldcaption
            End Get
            Set(ByVal WithEventsValue As DataGridViewTextBoxColumn)
                Me._f_fieldcaption = WithEventsValue
            End Set
        End Property

        Friend Overridable Property f_fieldcaption_en() As DataGridViewTextBoxColumn
            Get
                Return Me._f_fieldcaption_en
            End Get
            Set(ByVal WithEventsValue As DataGridViewTextBoxColumn)
                Me._f_fieldcaption_en = WithEventsValue
            End Set
        End Property

        Friend Overridable Property f_fieldName() As DataGridViewTextBoxColumn
            Get
                Return Me._f_fieldName
            End Get
            Set(ByVal WithEventsValue As DataGridViewTextBoxColumn)
                Me._f_fieldName = WithEventsValue
            End Set
        End Property

        Friend Overridable Property f_format() As DataGridViewTextBoxColumn
            Get
                Return Me._f_format
            End Get
            Set(ByVal WithEventsValue As DataGridViewTextBoxColumn)
                Me._f_format = WithEventsValue
            End Set
        End Property

        Friend Overridable Property f_sequence() As DataGridViewTextBoxColumn
            Get
                Return Me._f_sequence
            End Get
            Set(ByVal WithEventsValue As DataGridViewTextBoxColumn)
                Me._f_sequence = WithEventsValue
            End Set
        End Property

        Friend Overridable Property f_visible() As DataGridViewCheckBoxColumn
            Get
                Return Me._f_visible
            End Get
            Set(ByVal WithEventsValue As DataGridViewCheckBoxColumn)
                Me._f_visible = WithEventsValue
            End Set
        End Property

        Friend Overridable Property f_width() As DataGridViewTextBoxColumn
            Get
                Return Me._f_width
            End Get
            Set(ByVal WithEventsValue As DataGridViewTextBoxColumn)
                Me._f_width = WithEventsValue
            End Set
        End Property

        Public Overridable Property grdSetup() As DataGridView
            Get
                Return Me._grdSetup
            End Get
            Set(ByVal WithEventsValue As DataGridView)
                If (Not Me._grdSetup Is Nothing) Then
                    RemoveHandler Me._grdSetup.DataError, New DataGridViewDataErrorEventHandler(AddressOf Me.grdSetup_DataError)
                End If
                Me._grdSetup = WithEventsValue
                If (Not Me._grdSetup Is Nothing) Then
                    AddHandler Me._grdSetup.DataError, New DataGridViewDataErrorEventHandler(AddressOf Me.grdSetup_DataError)
                End If
            End Set
        End Property


        ' Fields
        <AccessedThroughProperty("cmdCancel")> _
        Private _cmdCancel As Button
        <AccessedThroughProperty("cmdOK")> _
        Private _cmdOK As Button
        <AccessedThroughProperty("f_alignment")> _
        Private _f_alignment As DataGridViewComboBoxColumn
        <AccessedThroughProperty("f_controlTypeID")> _
        Private _f_controlTypeID As DataGridViewComboBoxColumn
        <AccessedThroughProperty("f_DataGridID")> _
        Private _f_DataGridID As DataGridViewTextBoxColumn
        <AccessedThroughProperty("f_DataGridSetupId")> _
        Private _f_DataGridSetupId As DataGridViewTextBoxColumn
        <AccessedThroughProperty("f_datasource")> _
        Private _f_datasource As DataGridViewTextBoxColumn
        <AccessedThroughProperty("f_fieldcaption")> _
        Private _f_fieldcaption As DataGridViewTextBoxColumn
        <AccessedThroughProperty("f_fieldcaption_en")> _
        Private _f_fieldcaption_en As DataGridViewTextBoxColumn
        <AccessedThroughProperty("f_fieldName")> _
        Private _f_fieldName As DataGridViewTextBoxColumn
        <AccessedThroughProperty("f_format")> _
        Private _f_format As DataGridViewTextBoxColumn
        <AccessedThroughProperty("f_sequence")> _
        Private _f_sequence As DataGridViewTextBoxColumn
        <AccessedThroughProperty("f_visible")> _
        Private _f_visible As DataGridViewCheckBoxColumn
        <AccessedThroughProperty("f_width")> _
        Private _f_width As DataGridViewTextBoxColumn
        <AccessedThroughProperty("grdSetup")> _
        Private _grdSetup As DataGridView
        Private components As IContainer
    End Class
End Namespace

