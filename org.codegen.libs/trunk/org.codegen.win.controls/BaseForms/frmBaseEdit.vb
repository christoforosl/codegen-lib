Imports System.ComponentModel

Public Class frmBaseEdit
    Implements IReadOnlyEnabled

#Region "Fields"

    ''' <summary>
    ''' Caption inserted at the top of the form to indicate "Edit" operation
    ''' </summary>
    Private Shared _EditCaption As String

    ''' <summary>
    ''' Caption inserted at the top of the form to indicate "Add" operation
    ''' </summary>
    Private Shared _AddCaption As String

    ''' <summary>
    ''' IDValue (primary key ) value of record we are editing
    ''' </summary>
    ''' <remarks></remarks>
    Private _IdValue As Integer

    ''' <summary>
    ''' Boolean flag to indicate if we adding a new record
    ''' </summary>
    ''' <remarks></remarks>
    Private _newRecord As Boolean

    ''' <summary>
    ''' Class that provides move next/previous record functionality
    ''' </summary>
    ''' <remarks></remarks>
    Private _MoveNextProvider As IMoveNextPrevious

#End Region

#Region "constructors"

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.ShowPrint = False
        Me.ShowSaveAs = False

    End Sub

    Public Sub New(ByVal _IdValue As Integer)

        Me.new()
        Me.IdValue = _IdValue

    End Sub

#End Region

#Region "properties"

    Public Overloads Property Owner As Form
        Get
            Return MyBase.Owner
        End Get
        Set(ByVal value As Form)
            MyBase.Owner = value
            If (TypeOf (value) Is frmBaseGrid) Then
                Me._MoveNextProvider = New GridMoveNextPrevious(Me, CType(value, frmBaseGrid).grdData)
            End If

        End Set
    End Property


    <Browsable(True)> _
    Public Property ShowAdd As Boolean
        Get
            Return Me.UcEditToolar.ShowAdd
        End Get
        Set(ByVal value As Boolean)
            Me.UcEditToolar.ShowAdd = value
        End Set
    End Property

    <Browsable(True)> _
    Public Property ShowPrint As Boolean
        Get
            Return Me.UcEditToolar.ShowPrint
        End Get
        Set(ByVal value As Boolean)
            Me.UcEditToolar.ShowPrint = value
        End Set
    End Property

    <Browsable(True)> _
    Public Property ShowSaveAs() As Boolean
        Get
            Return Me.UcEditToolar.ShowSaveAs
        End Get
        Set(ByVal value As Boolean)
            Me.UcEditToolar.ShowSaveAs = value
        End Set
    End Property

    <Browsable(True)> _
    Public Property ShowNavigationButtons As Boolean
        Get
            Return Me.UcEditToolar.ShowNavigationButtons
        End Get

        Set(ByVal value As Boolean)
            Me.UcEditToolar.ShowNavigationButtons = value
        End Set

    End Property

    <Browsable(True)> _
    Public Property ShowDelete As Boolean
        Get
            Return Me.UcEditToolar.ShowDelete
        End Get

        Set(ByVal value As Boolean)
            Me.UcEditToolar.ShowDelete = value
        End Set

    End Property


    ''' <summary>
    ''' The id value (primary key value) of the record we are editing.
    ''' if value is 0, then the _newRecord boolean proeprty is set to true, else false
    ''' </summary>
    <Browsable(False)> _
    Public Property IdValue() As Integer
        Get
            Return _IdValue
        End Get

        Set(ByVal value As Integer)
            _IdValue = value
            _newRecord = (value <= 0)
            Me.setToolbarControls()
        End Set

    End Property

    ''' <summary>
    ''' Returns true if the form is editing / enterign a new record.
    ''' </summary>
    Public Property NewRecord As Boolean
        Get
            Return Me._newRecord
        End Get
        Set(ByVal value As Boolean)
            Me._newRecord = value
        End Set
    End Property
#End Region

#Region "Form Events"

    Private Sub frmBaseEdit_FormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles Me.FormClosing

        If e.CloseReason = CloseReason.UserClosing Then
            If CancelAndClose() = False Then
                e.Cancel = True
            End If
        End If
    End Sub

    Private Sub frmBaseEdit_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles Me.KeyDown

        If e.KeyCode = Keys.Escape Then
            'only close form if top level
            If Me.TopLevel Then
                Me.DialogResult = Windows.Forms.DialogResult.Cancel
            End If

        ElseIf e.KeyCode = Keys.Enter Then
            Me.ValidateAndSaveRecord()

        End If

    End Sub


    Private Sub frmBaseEdit_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

        AddHandler UcEditToolar.cmdSave.Click, AddressOf UcEditToolar1_Save_Click
        AddHandler UcEditToolar.cmdDelete.Click, AddressOf UcEditToolar1_Delete_Click
        AddHandler UcEditToolar.cmdSaveAs.Click, AddressOf UcEditToolar1_SaveAs_Click
        AddHandler UcEditToolar.cmdNext.Click, AddressOf UcEditToolar1_Next_Click
        AddHandler UcEditToolar.cmdPrevious.Click, AddressOf UcEditToolar1_Previous_Click
        AddHandler UcEditToolar.cmdCancel.Click, AddressOf UcEditToolar1_Cancel_Click
        AddHandler UcEditToolar.cmdAdd.Click, AddressOf UcEditToolar1_Add_Click

    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Makes all input controls on the form ReadOnly
    ''' </summary>
    Public Sub setReadOnly() Implements IReadOnlyEnabled.setReadOnly

        Call setReadOnly(Me)

        Me.UcEditToolar.cmdSave.Enabled = False
        Me.UcEditToolar.cmdAdd.Enabled = False
        Me.UcEditToolar.cmdDelete.Enabled = False
        Me.UcEditToolar.cmdSaveAs.Enabled = False

    End Sub

    Public Sub setReadOnly(ByVal cparent As Control)

        For Each c As Control In cparent.Controls

            If TypeOf c Is IReadOnlyEnabled Then
                CType(c, IReadOnlyEnabled).setReadOnly()
            End If
            Me.setReadOnly(c)

        Next

    End Sub

    ''' <summary>
    ''' Adds a button to the standard toolbar of the form.
    ''' </summary>
    ''' <param name="text">Caption of the button</param>
    ''' <param name="handler">Event Handler to handle the click event</param>
    ''' <param name="witdh">Width of the button, default 60 pixels</param>
    ''' <param name="img">Image of button</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function addToolbarActionButton(ByVal text As String, _
                ByVal handler As System.EventHandler, _
                Optional ByVal witdh As Integer = 60, _
                Optional ByVal img As Drawing.Image = Nothing) _
               As ToolStripItem

        Return frmBase.addStripItem(Me.UcEditToolar.tlStripEdit, New ToolStripButton, _
                 text, handler, witdh, img)


    End Function

    ''' <summary>
    ''' The localized text string that says "EDIT", appended to the caption of the form
    ''' </summary>
    Private Shared ReadOnly Property EditCaption() As String
        Get
            If _EditCaption Is Nothing Then
                _EditCaption = WinControlsLocalizer.getString("Form_Edit_Text")
            End If
            Return _EditCaption
        End Get
    End Property

    ''' <summary>
    ''' The localized text string that says "ADD", appended to the caption of the form
    ''' </summary>
    Private Shared ReadOnly Property AddCaption() As String
        Get
            If _AddCaption Is Nothing Then
                _AddCaption = WinControlsLocalizer.getString("Form_Add_Text")
            End If
            Return _AddCaption
        End Get
    End Property


    Public Sub setRecordLoadedStatus(ByVal recordId As Integer)

        Dim idstr As String = CStr(recordId)
        If recordId <= 0 Then
            idstr = WinControlsLocalizer.getString("new_record")
        End If
        Me.UpdateStatus(WinControlsLocalizer.getString("record_loaded_ok", idstr))

        Dim additionalFormText As String = CStr(IIf(Me.NewRecord, _
                AddCaption, _
                EditCaption))

        If Me.Text.EndsWith(additionalFormText) = False Then
            Me.Text = Me.Text & " - " & additionalFormText
        End If


    End Sub

    ''' <summary>
    ''' Sets the status text of the form, ie the text that appears on the 
    ''' status bar at the bottom of the form
    ''' </summary>
   Public Sub UpdateStatus(ByVal txt As String)
        Me.lblEditStatus.Text = txt
        Me.lblEditStatus.ForeColor = Color.Black
    End Sub

    ''' <summary>
    ''' Sets the status text of the form, (ie the text that appears on the 
    ''' status bar at the bottom of the form) in RED color
    ''' </summary>
    Public Overridable Sub setAlertStatusRecordFailed()

        Me.lblEditStatus.Text = WinControlsLocalizer.getString("record_not_saved")
        Me.lblEditStatus.ForeColor = Color.Red

    End Sub


    Public Overridable Sub setAlertStatusOK()

        Me.lblEditStatus.Text = "OK"
        Me.lblEditStatus.ForeColor = Color.Black

    End Sub

    Protected Overridable Sub MovePrevious()
        If Me._MoveNextProvider IsNot Nothing Then
            Me._MoveNextProvider.MovePrevious()
        End If

    End Sub

    Protected Overridable Sub MoveNext()

        If Me._MoveNextProvider IsNot Nothing Then
            Me._MoveNextProvider.MoveNext()
        End If

    End Sub

    Protected Sub setToolbarControls()

        Me.UcEditToolar.cmdDelete.Enabled = Me._IdValue > 0 OrElse Me.DesignMode = True
        Me.UcEditToolar.cmdPrint.Enabled = Me._IdValue > 0 OrElse Me.DesignMode = True
        Me.UcEditToolar.cmdSaveAs.Enabled = Me._IdValue > 0 OrElse Me.DesignMode = True
        Me.UcEditToolar.cmdNext.Visible = Me._MoveNextProvider Is Nothing = False OrElse Me.DesignMode = True
        Me.UcEditToolar.cmdPrevious.Visible = Me._MoveNextProvider Is Nothing = False OrElse Me.DesignMode = True

        Me.UcEditToolar.cmdAdd.Enabled = Me._IdValue > 0 OrElse Me.DesignMode = True

    End Sub
    ''' <summary>
    ''' ValidateAndSaveRecord: Calls the valiation and Saving routines
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub ValidateAndSaveRecord()
        Try

            If Me.SaveData() = True Then
                Me.DialogResult = Windows.Forms.DialogResult.OK
            Else
                Me.setAlertStatusRecordFailed()
            End If

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

    ''' <summary>
    ''' Examines whether data has changed on the screen sice we loaded the data in the controls
    ''' </summary>
    Public Overridable Function dataChanged() As Boolean

        Return True

    End Function

    Public Function SaveBeforeMove() As Boolean

        If Me.dataChanged Then
            Select Case MsgBox(WinControlsLocalizer.getString("warn_move_rec"), _
                     MsgBoxStyle.Question Or MsgBoxStyle.YesNoCancel)
                Case MsgBoxResult.Cancel
                    Return False

                Case MsgBoxResult.Yes
                    Return Me.SaveData() ' save data returns true if we saved ok to the database

                Case Else
                    Return True

            End Select
        Else
            Return True
        End If

    End Function

    Private Function CancelAndClose() As Boolean

        If Me.UcEditToolar.cmdSave.Enabled AndAlso Me.dataChanged Then
            If MsgBox("Are you sure you want to close and cancel any of your changes?", _
                      CType(MsgBoxStyle.Question + MsgBoxStyle.YesNo, MsgBoxStyle)) = MsgBoxResult.Yes Then
                Return True
            End If
            Return False
        Else
            Return True
        End If

    End Function

    Public Overridable Function SaveAndClose() As Boolean

        If Me.SaveData() = False Then Exit Function
        Me.DialogResult = Windows.Forms.DialogResult.OK

    End Function

#End Region

#Region "Load, Save, Delete"
    ''' <summary>
    ''' LoadData 
    ''' </summary>
    ''' <remarks></remarks>
    Public Overridable Sub LoadData()

        Throw New NotImplementedException("clients must override this to load record.")

    End Sub

    ''' <summary>
    ''' SaveData 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overridable Function SaveData() As Boolean

        Throw New NotImplementedException("clients must override this to save record.")

    End Function

    Public Overridable Sub DeleteData()

        Throw New NotImplementedException("Client forms must override this method")

    End Sub

#End Region

#Region "Edit Toolbar Click event handlers"
    ''' <summary>
    ''' Moves to the next record in the grid and loads the data
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub UcEditToolar1_Next_Click(ByVal sender As Object, ByVal e As EventArgs)

        Me.MoveNext()

    End Sub
    ''' <summary>
    ''' Moves to the previous record in the grid and loads data on the page
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub UcEditToolar1_Add_Click(ByVal sender As Object, ByVal e As EventArgs)

        If Me.SaveBeforeMove Then

            Me.IdValue = 0
            Me.LoadData()

        End If

    End Sub

    Private Sub UcEditToolar1_Previous_Click(ByVal sender As Object, ByVal e As EventArgs)

        MovePrevious()

    End Sub

    Private Sub UcEditToolar1_SaveAs_Click(ByVal sender As Object, ByVal e As EventArgs)

        Me.ValidateAndSaveRecord()

    End Sub

    Private Sub UcEditToolar1_Save_Click(ByVal sender As Object, ByVal e As EventArgs)

        Me.ValidateAndSaveRecord()

    End Sub

    Private Sub UcEditToolar1_Delete_Click(ByVal sender As Object, ByVal e As EventArgs)

        If MsgBox("Are you sure you want to delete this record?", _
                 CType(MsgBoxStyle.Question + MsgBoxStyle.YesNo, MsgBoxStyle)) = MsgBoxResult.Yes Then

            Me.DeleteData()
            Me.DialogResult = Windows.Forms.DialogResult.OK

        End If

    End Sub

    Private Sub UcEditToolar1_Cancel_Click(ByVal sender As Object, ByVal e As EventArgs)

        If CancelAndClose() Then
            Me.DialogResult = Windows.Forms.DialogResult.Cancel
        End If

    End Sub

#End Region


End Class