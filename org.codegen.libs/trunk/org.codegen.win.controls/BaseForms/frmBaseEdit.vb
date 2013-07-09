Imports System.ComponentModel

''' <summary>
''' Enumeration for reults values of SaveData of frmBaseEdit
''' </summary>
''' <remarks></remarks>
Public Enum enumSaveDataResult

    ''' <summary>
    ''' Data was saved ok, close the form
    ''' </summary>
    ''' <remarks></remarks>
    SAVE_SUCESS_AND_CLOSE

    ''' <summary>
    ''' Data was saved OK, do not close the form
    ''' </summary>
    ''' <remarks></remarks>
    SAVE_SUCESS_AND_STAY

    ''' <summary>
    ''' Data as not saved ok, error message will be shown
    ''' </summary>
    ''' <remarks></remarks>
    SAVE_FAIL

End Enum

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

    ''' <summary>
    ''' Counts the number of sucessfull saves while the form is open.
    ''' This is because the user can select to leave form open after a save, and then 
    ''' click "Cancel", in which case we need to refresh grid
    ''' </summary>
    Public Property NumberOfRecordsSaved() As Integer = 0

    ''' <summary>
    ''' Returns the form that opened this edit form.
    ''' Usually this will be the list form that the user double clicked 
    ''' to edit a record
    ''' </summary>
    ''' <value></value>
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

    Private _showAdd As Boolean = True

    ''' <summary>
    ''' Gets/Sets indicator that decides if the form shows the "Add New" button
    ''' </summary>
    <Browsable(True)> _
    Public Property ShowAdd As Boolean
        Get
            Return _showAdd
        End Get
        Set(ByVal value As Boolean)
            _showAdd = value
            Me.UcEditToolar.ShowAdd = value
        End Set
    End Property

    Private _showPrint As Boolean = True

    ''' <summary>
    ''' Gets/Sets indicator that decides if the form shows the "Print" button
    ''' </summary>
    <Browsable(True)> _
    Public Property ShowPrint As Boolean
        Get
            Return _showPrint
        End Get
        Set(ByVal value As Boolean)
            _showPrint = value
            Me.UcEditToolar.ShowPrint = value
        End Set
    End Property

    Private _ShowSaveAs As Boolean = False

    ''' <summary>
    ''' Gets/Sets indicator that decides if the form shows the "Save As" button.
    ''' Default is FALSE
    ''' </summary>
    <Browsable(True)> _
    Public Property ShowSaveAs() As Boolean
        Get
            Return Me.UcEditToolar.ShowSaveAs
        End Get
        Set(ByVal value As Boolean)
            _ShowSaveAs = value
            Me.UcEditToolar.ShowSaveAs = value
        End Set
    End Property

    Private _ShowNavigationButtons As Boolean = True
    ''' <summary>
    ''' Gets/Sets indicator that decides if the form shows the Navigation Buttons (Next/Previous).
    ''' Default is TRUE
    ''' </summary>
    <Browsable(True)> _
    Public Property ShowNavigationButtons As Boolean
        Get
            Return _ShowNavigationButtons
        End Get

        Set(ByVal value As Boolean)
            _ShowNavigationButtons = value
            Me.UcEditToolar.ShowNavigationButtons = value
        End Set

    End Property

    Private _ShowDelete As Boolean = True
    ''' <summary>
    ''' Gets/Sets indicator that decides if the form shows the Delete Button
    ''' Default is TRUE
    ''' </summary>
    <Browsable(True)> _
    Public Property ShowDelete As Boolean
        Get
            Return _ShowDelete
        End Get

        Set(ByVal value As Boolean)
            _ShowDelete = value
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

    ''' <summary>
    ''' If this property is true, hitting the ENTER key on the keyboard will cause the edit form to validate, save and close.
    ''' The default value is iniitilized to that of FormsApplicationContext.current.SaveOnEnterKey which should be set on applciation startup.
    ''' This value can be customized on a per form basis
    ''' </summary>
    Public Property SaveOnEnterKey As Boolean = FormsApplicationContext.current.SaveOnEnterKey

    Private Sub frmBaseEdit_FormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles Me.FormClosing

        If e.CloseReason = CloseReason.UserClosing Then
            If canCancelAndClose() = False Then
                e.Cancel = True
            End If
        End If
    End Sub

    Public Overrides Sub handleEscapeKey()

        If Me.TopLevel Then
            If canCancelAndClose() Then
                Me.DialogResult = Windows.Forms.DialogResult.Cancel
            End If

        End If

    End Sub

    Private Sub frmBaseEdit_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles Me.KeyDown

        'the enter key is handled in the frmBase
        'If e.KeyCode = Keys.Escape Then
        '    'only close form if top level
        '    Call handleEscapeKey()
        '    e.Handled = True

        If e.KeyCode = Keys.Enter Then
            If Me.SaveOnEnterKey Then
                Me.ValidateAndSaveRecord()
                e.Handled = True
            End If

        ElseIf e.Control And e.KeyCode = Keys.S Then 'ctrl+s save
            Me.ValidateAndSaveRecord()
            e.Handled = True

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

    Public Overridable Sub setAlertStatusRecordSaveSuccess()

        Me.lblEditStatus.Text = WinControlsLocalizer.getString("record_saved")
        Me.lblEditStatus.ForeColor = Color.Red

    End Sub
    Public Overridable Sub setAlertStatusOK()

        Me.lblEditStatus.Text = String.Empty
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
            Dim saveResult As enumSaveDataResult = Me.SaveData
            If saveResult = enumSaveDataResult.SAVE_SUCESS_AND_CLOSE Then
                Me.NumberOfRecordsSaved += 1
                Me.DialogResult = Windows.Forms.DialogResult.OK

            ElseIf saveResult = enumSaveDataResult.SAVE_SUCESS_AND_STAY Then
                Me.NumberOfRecordsSaved += 1
                Me.setAlertStatusRecordSaveSuccess()

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

                    Dim ret As enumSaveDataResult = Me.SaveData()
                    Return ret <> enumSaveDataResult.SAVE_FAIL

                Case Else
                    Return True

            End Select
        Else
            Return True
        End If

    End Function

    ''' <summary>
    ''' If the form has dirty data *AND* the user confirms close, this function returns true
    ''' if the form has no dirty data, it return false (ie, go ahead and close form)
    ''' If the form 
    ''' </summary>
    ''' <returns>True if the form can be closed</returns>
    ''' <remarks></remarks>
    Private Function canCancelAndClose() As Boolean

        Dim confirMsg As String = WinControlsLocalizer.getString("confirm_close_and_loose_changes")
        Dim dirty As Boolean = Me.dataChanged
        'MsgBox("dirrty:" & dirty)
        If Me.UcEditToolar.cmdSave.Enabled AndAlso _
                dirty AndAlso _
                winUtils.MsgboxQuestion(confirMsg) = MsgBoxResult.No Then

            Return False

        End If

        Return True

    End Function

    Public Overridable Function SaveAndClose() As Boolean

        If Me.SaveData() = enumSaveDataResult.SAVE_FAIL Then Exit Function
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
    Public Overridable Function SaveData() As enumSaveDataResult

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

    Private Const STR_WARN_DELETE As String = "warn_delete"

    Private Sub UcEditToolar1_Delete_Click(ByVal sender As Object, ByVal e As EventArgs)

        If winUtils.MsgboxQuestion(WinControlsLocalizer.getString(STR_WARN_DELETE)) = MsgBoxResult.Yes Then

            Me.DeleteData()
            Me.DialogResult = Windows.Forms.DialogResult.OK

        End If

    End Sub

    Private Sub UcEditToolar1_Cancel_Click(ByVal sender As Object, ByVal e As EventArgs)

        If canCancelAndClose() Then
            Me.DialogResult = Windows.Forms.DialogResult.Cancel
        End If

    End Sub

#End Region


    
End Class