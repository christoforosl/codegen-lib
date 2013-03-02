Imports org.codegen.win.controls.Grid
Imports System.ComponentModel
Imports System.Reflection
Imports System.Collections.Generic

''' <summary>
''' Enumeration to indicate the mode of the list form
''' LIST mode: shows edit buttons (add/edit/delete)
''' SELECT mode: shows select/close button
''' </summary>
''' <remarks></remarks>
Public Enum enumGridFormMode
    MODE_LIST
    MODE_SELECT
End Enum


Public Class frmBaseGrid

    Private Const STR_CMD_HIDE_COLUMN As String = "cmdHideColumn"
    Private Const STR_WARN_DELETE As String = "warn_delete"
    Private Const STR_WARN_DELETE_MULTIPLE As String = "warn_delete_multiple"


    Private _GridMode As enumGridFormMode = enumGridFormMode.MODE_LIST

    ''' <summary>
    ''' Event fires after search of grid is completed
    ''' </summary>
    Public Event gridSearchExecuted(ByVal sender As System.Object)

    ''' <summary>
    ''' Event fires after the user selected "Yes" to delete a record
    ''' </summary>
    ''' <param name="sender">Grid</param>
    ''' <remarks></remarks>
    Public Event gridDeleteRecordConfirmed(ByVal sender As System.Object)

    ''' <summary>
    ''' fires after user has added/deleted/edited/searched the grid
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <remarks></remarks>
    Public Event gridRowCountChanged(ByVal sender As System.Object)

    ''' <summary>
    ''' determines whether the form processes a delete operation when 
    ''' multiple rows are selected in the datagrid.  By default 
    ''' this property is false
    ''' </summary>
    ''' <remarks></remarks>
    Public Property AllowDeleteMultipleSelection As Boolean = False

    Private _allowEdit As Boolean = True
    Private _allowAddNew As Boolean = True
    Private _AllowDelete As Boolean = True

    ''' <summary>
    ''' Disables AddNew, Edit and delete buttons and menues
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub setReadOnly(Optional ByVal bReadOnly As Boolean = True)

        Me.AllowAddNew = Not bReadOnly 'only allow addnew if not read only
        Me.AllowEdit = Not bReadOnly 'only allow edit if not read only
        Me.AllowDelete = Not bReadOnly 'only allow delete if not read only

    End Sub

    Public Sub setEditButtonText(ByVal stext As String)

        Me.cmdEdit.Text = stext
        Me.mnEdit.Text = stext

    End Sub

    Public Sub setAddButtonText(ByVal stext As String)

        Me.cmdAdd.Text = stext
        Me.mnAdd.Text = stext

    End Sub

    Public Sub setDeleteButtonText(ByVal stext As String)

        Me.cmdDelete.Text = stext
        Me.mnDelete.Text = stext

    End Sub

    <Browsable(True)> _
    Public Property AllowEdit As Boolean
        Get
            Return _allowEdit
        End Get
        Set(ByVal value As Boolean)

            _allowEdit = value

            Me.cmdEdit.Visible = value
            Me.cmdEdit.Enabled = Not value
            Me.mnEdit.Visible = value
            Me.mnEdit.Enabled = Not value

        End Set
    End Property

    <Browsable(True)> _
    Public Property AllowAddNew As Boolean
        Get
            Return _allowAddNew
        End Get
        Set(ByVal value As Boolean)
            _allowAddNew = value

            Me.cmdAdd.Visible = value
            Me.cmdAdd.Enabled = Not value
            Me.mnAdd.Visible = value
            Me.mnAdd.Enabled = Not value

        End Set
    End Property

    <Browsable(True)> _
    Public Property AllowDelete As Boolean
        Get
            Return _AllowDelete
        End Get
        Set(ByVal value As Boolean)
            _AllowDelete = value
            Me.cmdDelete.Visible = value
            Me.cmdDelete.Enabled = Not value
            Me.mnDelete.Visible = value
            Me.mnDelete.Enabled = Not value

        End Set

    End Property

    <Browsable(False)> _
    Public Property GridMode As enumGridFormMode
        Get
            Return _GridMode
        End Get
        Set(ByVal value As enumGridFormMode)
            _GridMode = value

            Me.pnlSelectToolbar.Visible = (Me._GridMode = enumGridFormMode.MODE_SELECT)
            Me.pnlEditToolbar.Visible = (Me._GridMode = enumGridFormMode.MODE_LIST)

            If (Me._GridMode = enumGridFormMode.MODE_SELECT) Then
                Me.MaximizeBox = False
                Me.MinimizeBox = False
                Me.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedDialog
            Else
                Me.MaximizeBox = True
                Me.MinimizeBox = True
                Me.FormBorderStyle = Windows.Forms.FormBorderStyle.Sizable
            End If

        End Set

    End Property

    Private Sub frmBaseGrid_KeyDown(ByVal sender As Object, _
                                    ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown

        If e.KeyCode = Keys.Delete Then
            If Me.ActiveControl IsNot Nothing AndAlso _
                        (Me.ActiveControl Is Me.grdData.Parent _
                                OrElse Me.ActiveControl Is Me.grdData) Then

                Call ListDeleteRecord()
                e.Handled = True

            End If
        End If

        If e.KeyCode = Keys.Enter Then

            If Me.ActiveControl IsNot Nothing AndAlso _
                        (Me.ActiveControl Is Me.grdData.Parent _
                                OrElse Me.ActiveControl Is Me.grdData) Then
                Call ListEditRecord(Me.grdData.IdValue)
                e.Handled = True
            End If

        End If


    End Sub

    Private Sub frmBaseGrid_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Me.DesignMode Then
            If Me.grdData Is Nothing Then
                Throw New ApplicationException("Method grdData must be overwritten by inheritors")
            End If

            If Me.Modal Then
                Me.MaximizeBox = False
                Me.MinimizeBox = False
                Me.FormBorderStyle = FormBorderStyle.Fixed3D
            End If

        End If

        AddHandler Me.cmdConfigureGrid.Click, AddressOf Me.mnConfigureGrid_Click
        AddHandler Me.cmdAdd.Click, AddressOf Me.mnAdd_Click
        AddHandler Me.cmdEdit.Click, AddressOf Me.mnEdit_Click
        AddHandler Me.cmdDelete.Click, AddressOf Me.mnDelete_Click
        AddHandler Me.cmdExcel.Click, AddressOf Me.mnToExcel_click

        Me.ShowConfigButton = False
        Me.ShowPrintButton = False

        If Not Me.DesignMode Then

            AddHandler Me.grdData.GridDataLoaded, AddressOf gridDataLoaded

            Me.tsReportButton.Visible = False
            Me.addMenues()

            Call setAddButtonText(WinControlsLocalizer.getString("cmdAdd"))
            Call setEditButtonText(WinControlsLocalizer.getString("cmdEdit"))
            Call setDeleteButtonText(WinControlsLocalizer.getString("cmdDelete"))

            Me.cmdExcel.Text = WinControlsLocalizer.getString("cmdExcel")
            Me.cmdPrint.Text = WinControlsLocalizer.getString("cmdPrint")
            Me.cmdConfigureGrid.Text = WinControlsLocalizer.getString("cmdChooseGridFields")

            Me.tsLblSearch.Text = WinControlsLocalizer.getString("Search")
            Me.tsLblSearch2.Text = WinControlsLocalizer.getString("Search")



            'only show the search textbox if search fields are defined
            Me.ShowSearch = Me.ShowSearch AndAlso Me.grdData.gpSearchFields.Count > 0
            Me.ShowConfigButton = False 'for the moment do not show the config button


            If Me.grdData.gpSearchFields IsNot Nothing AndAlso _
                        Me.grdData.gpSearchFields.Count > 0 Then

                Dim i As Integer = 0


                For Each sf As String In Me.grdData.gpSearchFields
                    i += i
                    Dim tsmiSearch As New System.Windows.Forms.ToolStripMenuItem
                    tsmiSearch.Checked = True
                    tsmiSearch.CheckOnClick = True
                    tsmiSearch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text

                    tsmiSearch.Name = "tsmi" & CStr(i)
                    tsmiSearch.Size = New System.Drawing.Size(152, 22)
                    tsmiSearch.Text = sf
                    tsmiSearch.Visible = True

                    If Me.GridMode = enumGridFormMode.MODE_LIST Then
                        Me.tsLblSearch.DropDownItems.Add(tsmiSearch)
                    Else
                        Me.tsLblSearch2.DropDownItems.Add(tsmiSearch)
                    End If


                Next


            End If

            Call winUtils.sizeMdiChild(Me)

        End If



    End Sub

#Region "Properties"

    Private _lastUsedFilter As String
    Private _ShowToolbar As Boolean

    <Description("Show or Hide the Print button on the toolbar.")> _
    Public Property ShowPrintButton() As Boolean
        Get
            Return Me.cmdPrint.Visible
        End Get
        Set(ByVal value As Boolean)
            Me.cmdPrint.Visible = value
            Me.tsepPrintAndExcel.Visible = Me.cmdExcel.Visible AndAlso _
              Me.cmdPrint.Visible AndAlso _
              Me.cmdConfigureGrid.Visible
        End Set

    End Property


    <Description("Show or Hide the Configure Grid button on the toolbar.")> _
    Public Property ShowConfigButton() As Boolean
        Get
            Return Me.cmdConfigureGrid.Visible
        End Get
        Set(ByVal value As Boolean)
            Me.cmdConfigureGrid.Visible = value
            Me.tsepPrintAndExcel.Visible = Me.cmdExcel.Visible AndAlso _
              Me.cmdPrint.Visible AndAlso _
              Me.cmdConfigureGrid.Visible
        End Set

    End Property



    <Description("Show or Hide the Export to Excel button on the toolbar.")> _
    Public Property ShowExcelButton() As Boolean
        Get
            Return Me.cmdExcel.Visible
        End Get
        Set(ByVal value As Boolean)
            Me.cmdExcel.Visible = value
            Me.tsepPrintAndExcel.Visible = Me.tsepPrintAndExcel.Visible = Me.cmdExcel.Visible AndAlso _
              Me.cmdPrint.Visible AndAlso _
              Me.cmdConfigureGrid.Visible
        End Set

    End Property

    <Description("Show or Hide the search tetbox on the toolbar.")> _
    Public Property ShowSearch() As Boolean
        Get
            Return Me.tsTxtSearch.Visible OrElse Me.tsTxtSearch2.Visible
        End Get
        Set(ByVal value As Boolean)
            Me.tsTxtSearch.Visible = value
            Me.tsLblSearch.Visible = value
            Me.tsepSearch.Visible = value

            Me.tsTxtSearch2.Visible = value
            Me.tsLblSearch2.Visible = value
            Me.tsepSearch2.Visible = value

        End Set

    End Property

    <Browsable(True), Description("Indicates whether the standard toolbar is visible.")> _
    Public Property gpShowToolbar() As Boolean
        Get
            Return Me._ShowToolbar
        End Get

        Set(ByVal value As Boolean)
            Me._ShowToolbar = value
            Me.tlStripList.Visible = Me._ShowToolbar

        End Set

    End Property

#End Region

#Region "Event Handlers"

    Private Sub mnHideCol_click(ByVal sender As Object, ByVal e As EventArgs)
        Me.grdData.hideColumn(Me.grdData.ColumnIndexToHide)
    End Sub

    Private Sub tsTxtSearch_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) _
                Handles tsTxtSearch.KeyDown, tsTxtSearch2.KeyDown

        If e.KeyCode <> Keys.Enter Then Exit Sub
        Dim searchTerm As String = CType(sender, ToolStripTextBox).Text.Trim
        Call Me.executeSearch(searchTerm)

    End Sub

    Private Sub executeSearch(ByVal searchTerm As String)

        If Me.grdData.gpSearchFields Is Nothing OrElse Me.grdData.gpSearchFields.Count = 0 Then Exit Sub

        Dim arr As List(Of String) = New List(Of String)
        Dim newSearchFilter As String = String.Empty

        If String.IsNullOrEmpty(searchTerm) Then
            'clear the _gridFilterFromSearch
            newSearchFilter = String.Empty

        ElseIf searchTerm.IndexOf("%") > -1 Then
            'partial search
            For i As Integer = 0 To Me.grdData.gpSearchFields.Count - 1

                Dim lsearchCol As DataGridViewColumn = GetSearchColumn(i)

                If lsearchCol.ValueType Is System.Type.GetType("System.String") Then
                    arr.Add(Me.grdData.gpSearchFields(i) & " LIKE " & Me.quoteSearchTerm(searchTerm))
                Else
                    arr.Add(" convert([" & Me.grdData.gpSearchFields(i) & "], 'System.String') LIKE " & Me.quoteSearchTerm(searchTerm))
                End If

            Next

        Else 'exact value search
            For i As Integer = 0 To Me.grdData.gpSearchFields.Count - 1
                Dim lsearchCol As DataGridViewColumn = GetSearchColumn(i)

                ' if field is numeric, only include it if search term is numeric!
                If lsearchCol.ValueType Is System.Type.GetType("System.Decimal") _
                    OrElse lsearchCol.ValueType Is System.Type.GetType("System.Int16") _
                            OrElse lsearchCol.ValueType Is System.Type.GetType("System.Int32") _
                            OrElse lsearchCol.ValueType Is System.Type.GetType("System.Int64") Then

                    If IsNumeric(searchTerm) Then
                        arr.Add(Me.grdData.gpSearchFields(i) & "=" & searchTerm)
                    End If

                Else
                    arr.Add(Me.grdData.gpSearchFields(i) & "=" & Me.quoteSearchTerm(searchTerm))
                End If

            Next

        End If

        If arr.Count > 0 Then
            newSearchFilter = "(" & String.Join(" OR ", arr) & ")"
        End If

        Dim finalfilter As String

        If String.IsNullOrEmpty(Me.grdData.BindingSource.Filter) = False _
          AndAlso Me.grdData.BindingSource.Filter.IndexOf(newSearchFilter) > -1 Then

            finalfilter = Me.grdData.BindingSource.Filter.Replace(Me._lastUsedFilter, newSearchFilter)
            If finalfilter.Trim.ToUpper.StartsWith("AND") Then
                finalfilter = "1=1 " & finalfilter
            End If
        Else
            finalfilter = newSearchFilter
        End If

        Me.grdData.BindingSource.Filter = finalfilter
        Me._lastUsedFilter = newSearchFilter

        RaiseEvent gridSearchExecuted(Me.grdData)
        RaiseEvent gridRowCountChanged(Me.grdData)

    End Sub
    Protected Sub mnToExcel_click(ByVal sender As Object, ByVal e As EventArgs)
        Me.toExcel()
    End Sub

    Private Sub mnConfigureGrid_Click(ByVal sender As Object, ByVal e As EventArgs)
        'Me.gridConfig()
    End Sub

    Private Sub mnAdd_Click(ByVal sender As Object, ByVal e As EventArgs)

        Call ListEditRecord(0)

    End Sub

    Private Sub Grid_DoubleClick(ByVal sender As Object, ByVal e As EventArgs)

        Call ListEditRecord(Me.grdData.IdValue)

    End Sub

    Private Sub mnDelete_Click(ByVal sender As Object, ByVal e As EventArgs)

        Call ListDeleteRecord()

    End Sub

    Protected Overridable Sub mnActions_Opening(ByVal sender As Object, ByVal e As CancelEventArgs)

        If (Not Me.mnActions.Items.Item(STR_CMD_HIDE_COLUMN) Is Nothing) Then
            Me.mnActions.Items.Item(STR_CMD_HIDE_COLUMN).Enabled = (Me.grdData.ColumnIndexToHide > -1)
        End If

        If Me.mnActions.Items.Count > 0 Then Me.mnActions.Items(0).Enabled = Me.cmdAdd.Enabled
        If Me.mnActions.Items.Count > 0 Then Me.mnActions.Items(1).Enabled = Me.cmdEdit.Enabled
        If Me.mnActions.Items.Count > 0 Then Me.mnActions.Items(2).Enabled = Me.cmdDelete.Enabled


    End Sub

    <Description("Sends grid data to Excel.")> _
    Public Sub toExcel()

        Dim exp As New ExcelGridExporter()
        exp.export(Me.grdData)

    End Sub

    Private Sub mnEdit_Click(ByVal sender As Object, ByVal e As EventArgs)


        Call ListEditRecord(Me.grdData.IdValue)


    End Sub

#End Region

#Region "Methods"
    <Description("Adds the shortcut menues displayed when a user righ-clicks on the data grid.")> _
    Private Sub addMenues()

        Dim item As ToolStripMenuItem

        Me.mnActions.Items.Clear()

        If Me.GridMode = enumGridFormMode.MODE_SELECT Then Exit Sub

        AddHandler Me.grdData.DoubleClick, AddressOf Me.Grid_DoubleClick

        item = Me.mnAdd
        item.Text = WinControlsLocalizer.getString("cmdAdd")
        item.ShortcutKeys = (Keys.Control Or Keys.N)
        AddHandler item.Click, AddressOf Me.mnAdd_Click
        Me.mnActions.Items.Add(item)


        item = Me.mnEdit
        item.Text = WinControlsLocalizer.getString("cmdEdit")
        item.ShortcutKeys = (Keys.Control Or Keys.E)
        AddHandler item.Click, AddressOf Me.mnEdit_Click
        Me.mnActions.Items.Add(item)


        item = Me.mnDelete
        item.Text = WinControlsLocalizer.getString("cmdDelete")
        item.ShortcutKeys = (Keys.Control Or Keys.D)
        AddHandler item.Click, AddressOf Me.mnDelete_Click
        Me.mnActions.Items.Add(item)


        item = New ToolStripMenuItem
        item.Text = WinControlsLocalizer.getString("cmdExcel")
        item.ShortcutKeys = (Keys.Control Or Keys.X)
        AddHandler item.Click, AddressOf Me.mnToExcel_click
        Me.mnActions.Items.Add(item)

        Me.mnActions.Items.Add(New ToolStripSeparator)

        'item = New ToolStripMenuItem
        'item.Name = STR_CMD_HIDE_COLUMN
        'item.Text = WinControlsLocalizer.getString(STR_CMD_HIDE_COLUMN)
        'AddHandler item.Click, AddressOf Me.mnHideCol_click

        'uncomment when configure grid is ready
        'item = New ToolStripMenuItem
        'item.Name = "cmdConfigureGrid"
        'item.Text = WinControlsLocalizer.getString("cmdConfigureGrid")
        'AddHandler item.Click, AddressOf Me.mnConfigureGrid_Click

        AddHandler mnActions.Opening, AddressOf Me.mnActions_Opening
        Me.mnActions.Items.Add(item)
        Me.grdData.ContextMenuStrip = Me.mnActions

    End Sub

    Public Sub addToolbarActionButton(ByVal text As String, _
                                       ByVal handler As System.EventHandler, _
                                       ByVal tbtn As ToolStripButton,
                                       ByVal mn As ToolStripMenuItem,
                                       Optional ByVal witdh As Integer = 60, _
                                       Optional ByVal img As Drawing.Image = Nothing)

        Call frmBase.addStripItem(Me.tlStripList, CType(tbtn, ToolStripItem), _
           text, handler, witdh, img)

        mn.Text = text

        AddHandler mn.Click, handler
        Me.mnActions.Items.Add(mn)

    End Sub

    Public Sub addToolbarActionButton(ByVal text As String, _
                            ByVal handler As System.EventHandler, _
                            Optional ByVal witdh As Integer = 60, _
                            Optional ByVal img As Drawing.Image = Nothing)

        Call frmBase.addStripItem(Me.tlStripList, New ToolStripButton, _
           text, handler, witdh, img)

        Dim item As New ToolStripMenuItem
        item.Text = text

        AddHandler item.Click, handler
        Me.mnActions.Items.Add(item)

    End Sub


    Public Sub addToReportMenu(ByVal btnText As String, _
                               ByVal handler As System.EventHandler, _
                               Optional ByVal tag As String = "")

        Dim item As ToolStripMenuItem = New ToolStripMenuItem
        item.Text = btnText
        item.Name = "reportBtn" & CStr(Me.tsReportButton.DropDownItems.Count + 1)
        If (String.IsNullOrEmpty(tag) = False) Then
            item.Tag = tag
        End If

        AddHandler item.Click, handler
        Me.tsReportButton.DropDownItems.Add(item)

        Me.tsReportButton.Visible = True

    End Sub

    ''' <summary>
    ''' Adds a menu item to the right click context menu of the grid of the page
    ''' </summary>
    ''' <param name="btnText">Button Text</param>
    ''' <param name="handler">Pointer to the handler of the click event of the menu</param>
    ''' <param name="withSeparator">Boolean that if true, inserts a separator before the menu item</param>
    ''' <param name="tag">Tag of item</param>
    ''' <remarks></remarks>
    Public Sub addToGridContextMenu(ByVal btnText As String, _
                               ByVal handler As System.EventHandler, _
                               Optional ByVal withSeparator As Boolean = False, _
                               Optional ByVal tag As String = "")

        Dim item As ToolStripMenuItem = New ToolStripMenuItem
        item.Text = btnText
        item.Name = "actionBtn" & CStr(Me.mnActions.Items.Count + 1)
        If (String.IsNullOrEmpty(tag) = False) Then
            item.Tag = tag
        End If

        If withSeparator Then
            Me.mnActions.Items.Add(New ToolStripSeparator)
        End If

        AddHandler item.Click, handler
        Me.mnActions.Items.Add(item)

    End Sub

#End Region

#Region "Search Box"

    ''' <summary>
    ''' Loads and returns a reference to the edit form
    ''' </summary>
    Public Function LoadEditForm() As frmBaseEdit

        If String.IsNullOrEmpty(Me.grdData.gpEditForm) Then
            Throw New ApplicationException("Please set the EditForm property.")
        End If

        Dim f As frmBaseEdit = Nothing
        Try
            Dim seditForm As String = Me.grdData.gpEditForm
            Dim assname As String = Assembly.GetEntryAssembly.GetName.Name
            If seditForm.StartsWith(assname & ".") = False Then
                seditForm = String.Format("{0}.{1}", assname, seditForm)
            End If

            Dim fo As Object = Assembly.GetEntryAssembly.CreateInstance(seditForm, _
              False, _
              BindingFlags.CreateInstance, _
              Nothing, Nothing, Nothing, Nothing)

            f = DirectCast(fo, frmBaseEdit)

        Catch e As Exception
            Throw New ApplicationException("Failed to instantiate edit form. Have you set the EditForm property?" & e.Message)
        End Try

        Return f

    End Function

    ''' <summary>
    ''' Prepares the edit form intance loadded from "LoadEditForm".  It is called from ListEditRecord.
    ''' If this function returns true, then the form is shown.  If the function returns false, then the
    ''' form is not shown.  Inside PrepareEditForm, the system sets the ID of the 
    ''' record and loads data on the screen.
    ''' </summary>
    ''' <param name="IdValue">The primary key value being edited, 0 if new record.</param>
    ''' <param name="editForm">And Instance of frmBaseEdit</param>
    ''' <remarks>This function does 3 things: 
    ''' (1) Sets the parent property of the EditForm, 
    ''' (2) sets the id value, 
    ''' (3) calls the load data sub on the edit form.
    ''' </remarks>
    Public Overridable Function PrepareEditForm(ByVal IdValue As Integer, _
                                                ByVal editForm As frmBaseEdit) As Boolean

        If editForm Is Nothing Then Return False

        editForm.Owner = Me
        editForm.IdValue = IdValue
        editForm.LoadData()

        Return True

    End Function

    ''' <summary>
    ''' Procedure that gets called on edit button click.  
    ''' </summary>
    ''' <remarks>
    ''' This procedure is final and not overridable.
    ''' If you want to override the showing of the edit form, override procedure PrepareEditForm.
    ''' </remarks>
    <Description("Procedure that gets called on edit button click.")> _
    Public Overridable Sub ListEditRecord(ByVal IdValue As Integer)

        If Me.GridMode = enumGridFormMode.MODE_SELECT Then Exit Sub
        If IdValue <> 0 AndAlso (Me.AllowEdit = False) Then Exit Sub
        If IdValue = 0 AndAlso (Me.AllowAddNew = False) Then Exit Sub

        Dim f As frmBaseEdit

        Try
            Call winUtils.HourglassOn()
            f = Me.LoadEditForm()

            If Me.PrepareEditForm(IdValue, f) Then

                If f.ShowDialog = Windows.Forms.DialogResult.OK Then
                    Call Me.grdData.requery()
                    RaiseEvent gridRowCountChanged(Me.grdData)

                End If
            End If

        Finally
            If f IsNot Nothing Then
                f.Close()
                f.Dispose()
                f = Nothing
            End If

            Call winUtils.HourglassOff()
        End Try
    End Sub

#End Region

    ''' <summary>
    ''' This method does not actually Delete a record from the undelying grid of the form.
    ''' It asks for confirmation and if the user answered yes, it raises event gridDeleteRecordConfirmed
    ''' </summary>
    ''' <remarks>
    ''' This method exits without doing anything if: ReadOnly is true, or button cmd delete is disabled.
    ''' After delete is called, the grid is refreshed, regardless if the deletion was successfull
    ''' </remarks>
    Public Sub ListDeleteRecord()

        If Me.GridMode = enumGridFormMode.MODE_SELECT Then Exit Sub
        If Me.AllowDelete = False Then Exit Sub
        If Me.cmdDelete.Enabled = False Then Exit Sub
        If Me.grdData.SelectedRows.Count = 0 Then Exit Sub

        If Me.grdData.SelectedRows.Count > 1 AndAlso Me.AllowDeleteMultipleSelection = False Then
            winUtils.MsgboxQuestion(WinControlsLocalizer.getString("multi_select_delete_disabled"))
            Exit Sub
        End If

        Dim msg As String = GetDeleteConfirmMsg()


        If winUtils.MsgboxQuestion(msg) = MsgBoxResult.Yes Then

            RaiseEvent gridDeleteRecordConfirmed(Me.grdData)
            Me.grdData.requery()
            RaiseEvent gridRowCountChanged(Me.grdData)

        End If


    End Sub

    ''' <summary>
    ''' This returns nothing and all inheritor forms must override this to return the 
    ''' grid on the page
    ''' </summary>
    Public Overridable Function grdData() As CGBaseGrid

        Return Nothing

    End Function

    Private Sub cmdSelectAndClose_Click(ByVal sender As System.Object, _
                                        ByVal e As System.EventArgs) _
                                    Handles cmdSelectAndClose.Click

        If Me.grdData.SelectedRows.Count = 0 Then
            Call winUtils.MsgboxStop("Nothing is selected in the list.  Please select one or more rows and try again.")
            Return
        End If

        Me.DialogResult = DialogResult.OK

    End Sub

    Private Sub cmdCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) _
                    Handles cmdSelectCancel.Click

        Me.DialogResult = DialogResult.Cancel

    End Sub

    Private Function quoteSearchTerm(ByVal searchTerm As String) As String

        Return "'" & searchTerm.Replace("'", "''") & "'"

    End Function

    Private Sub gridDataLoaded(ByVal sender As Object)
        RaiseEvent gridRowCountChanged(sender)
    End Sub

    ''' <summary>
    ''' This is the message shown to the user in order to confirm the deletion of records.
    ''' Clients can override this method and return a customized message
    '''
    ''' By default this returns generic warning messages  like 
    ''' "Are you sure you want to delete this record?" 
    ''' 
    ''' Clients should override 
    ''' this method and provide a better warning message of the record(s) to be deleted by including 
    ''' the name or number of the record to be deleted. For example.
    ''' to delete a "person" object, returing the name of the person would
    ''' offer a much more clearer idea to the user of what will be deleted.
    ''' </summary>
    Public Overridable Function GetDeleteConfirmMsg() As String
        'get Delete Confirm Message
        Dim msg As String
        If Me.grdData.SelectedRows.Count = 1 Then
            msg = WinControlsLocalizer.getString(STR_WARN_DELETE)

        Else
            msg = WinControlsLocalizer.getString(STR_WARN_DELETE_MULTIPLE)
        End If
        Return msg
    End Function


    Private _SearchColumnCache As Dictionary(Of Integer, DataGridViewColumn)

    Private Function GetSearchColumn(ByVal i As Integer) As DataGridViewColumn

        If _SearchColumnCache Is Nothing Then
            _SearchColumnCache = New Dictionary(Of Integer, DataGridViewColumn)
        End If

        If _SearchColumnCache.ContainsKey(i) = False Then
            Dim lsearchCol As DataGridViewColumn = Me.grdData.Columns(Me.grdData.gpSearchFields(i))
            If lsearchCol Is Nothing Then
                Throw New ApplicationException(String.Format("Search Column {0} does not exist", Me.grdData.gpSearchFields(i)))
            End If
            _SearchColumnCache(i) = lsearchCol
        End If

        Return _SearchColumnCache.Item(i)

    End Function

   
End Class