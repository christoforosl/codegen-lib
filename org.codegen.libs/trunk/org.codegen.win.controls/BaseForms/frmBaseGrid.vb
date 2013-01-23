Imports org.codegen.win.controls.Grid
Imports System.ComponentModel
Imports System.Reflection

''' <summary>
''' Enumeration to indicate the mode of the list form
''' LIST mode: shows edit buttons (add/edit/delete)
''' SELECT mode: shows select/close button
''' </summary>
''' <remarks></remarks>
Public Enum frmGridMode
    MODE_LIST
    MODE_SELECT
End Enum


Public Class frmBaseGrid

    Private Const STR_CMD_HIDE_COLUMN As String = "cmdHideColumn"
    Private _GridMode As frmGridMode

    ''' <summary>
    ''' Event fires after search of grid is completed
    ''' </summary>
    ''' <remarks></remarks>
    Public Event gridSearchExecuted()

    <Browsable(True)> _
    Public Property [ReadOnly] As Boolean
        Get
            Return Not Me.cmdAdd.Enabled
        End Get
        Set(ByVal value As Boolean)
            Me.cmdAdd.Enabled = Not value
            Me.cmdEdit.Enabled = Not value
            Me.cmdDelete.Enabled = Not value
        End Set
    End Property

    <Browsable(False)> _
    Public Property GridMode As frmGridMode
        Get
            Return _GridMode
        End Get
        Set(ByVal value As frmGridMode)
            _GridMode = value

            Me.pnlSelectToolbar.Visible = (Me._GridMode = frmGridMode.MODE_SELECT)
            Me.pnlEditToolbar.Visible = (Me._GridMode = frmGridMode.MODE_LIST)

            If (Me._GridMode = frmGridMode.MODE_SELECT) Then
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
            Me.tsReportButton.Visible = False
            Me.addMenues()
            Me.cmdAdd.Text = WinControlsLocalizer.getString("cmdAdd")
            Me.cmdEdit.Text = WinControlsLocalizer.getString("cmdEdit")
            Me.cmdExcel.Text = WinControlsLocalizer.getString("cmdExcel")
            Me.cmdPrint.Text = WinControlsLocalizer.getString("cmdPrint")
            Me.cmdConfigureGrid.Text = WinControlsLocalizer.getString("cmdChooseGridFields")
            Me.tsLblSearch.Text = WinControlsLocalizer.getString("Search")
            Me.tsLblSearch2.Text = WinControlsLocalizer.getString("Search")

            Me.cmdDelete.Text = WinControlsLocalizer.getString("cmdDelete")

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

                    If Me.GridMode = frmGridMode.MODE_LIST Then
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
        If Me.grdData.gpSearchFields Is Nothing OrElse Me.grdData.gpSearchFields.Count = 0 Then Exit Sub


        Dim arr(0 To Me.grdData.gpSearchFields.Count - 1) As String
        Dim newSearchFilter As String = String.Empty
        Dim searchTerm As String = CType(sender, ToolStripTextBox).Text.Trim

        If String.IsNullOrEmpty(searchTerm) Then
            'clear the _gridFilterFromSearch
            newSearchFilter = String.Empty

        ElseIf searchTerm.IndexOf("%") > -1 Then
            'partial search
            For i As Integer = 0 To Me.grdData.gpSearchFields.Count - 1

                arr(i) = Me.grdData.gpSearchFields(i) & " LIKE " & Me.quoteSearchTerm(searchTerm)
            Next
            newSearchFilter = "(" & String.Join(" OR ", arr) & ")"

        Else 'exact value search
            For i As Integer = 0 To Me.grdData.gpSearchFields.Count - 1
                arr(i) = Me.grdData.gpSearchFields(i) & "=" & Me.quoteSearchTerm(searchTerm)
            Next
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

        RaiseEvent gridSearchExecuted()

    End Sub

    Protected Sub mnToExcel_click(ByVal sender As Object, ByVal e As EventArgs)
        Me.toExcel()
    End Sub

    Private Sub mnConfigureGrid_Click(ByVal sender As Object, ByVal e As EventArgs)
        'Me.gridConfig()
    End Sub

    Private Sub mnAdd_Click(ByVal sender As Object, ByVal e As EventArgs)

        If Me.cmdAdd.Enabled = False Then Exit Sub
        Call ListEditRecord(0)

    End Sub

    Private Sub Grid_DoubleClick(ByVal sender As Object, ByVal e As EventArgs)

        If Not Me.cmdEdit.Enabled Then Return

        'On double click, Only bring up the edit form if the grid is readonly
        If Me.grdData.ReadOnly Then
            Call ListEditRecord(Me.grdData.IdValue)
        End If

    End Sub

    Private Sub mnDelete_Click(ByVal sender As Object, ByVal e As EventArgs)

        If IsNumeric(Me.grdData.IdValue) _
                AndAlso winUtils.MsgboxQuestion("Are you sure you want to delete this record?") _
                = MsgBoxResult.Yes Then

            Call deleteRecord(Me.grdData.IdValue)
            Me.grdData.requery()

        End If


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

        If Me.cmdEdit.Enabled = False Then Exit Sub
        Call ListEditRecord(Me.grdData.IdValue)


    End Sub

#End Region

#Region "Methods"
    <Description("Adds the shortcut menues displayed when a user righ-clicks on the data grid.")> _
    Private Sub addMenues()

        Dim item As ToolStripMenuItem

        Me.mnActions.Items.Clear()


        If Me.GridMode = frmGridMode.MODE_SELECT Then Exit Sub

        AddHandler Me.grdData.DoubleClick, AddressOf Me.Grid_DoubleClick

        item = New ToolStripMenuItem
        item.Text = WinControlsLocalizer.getString("cmdAdd")
        item.ShortcutKeys = (Keys.Control Or Keys.N)
        AddHandler item.Click, AddressOf Me.mnAdd_Click

        Me.mnActions.Items.Add(item)
        item = New ToolStripMenuItem
        item.Text = WinControlsLocalizer.getString("cmdEdit")
        item.ShortcutKeys = (Keys.Control Or Keys.E)

        AddHandler item.Click, AddressOf Me.mnEdit_Click

        Me.mnActions.Items.Add(item)
        item = New ToolStripMenuItem
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
    ''' form is not shown.  Insided PrepareEditForm, the system sets the ID of the 
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
        Dim f As frmBaseEdit

        Try
            Call winUtils.HourglassOn()
            f = Me.LoadEditForm()

            If Me.PrepareEditForm(IdValue, f) Then

                If f.ShowDialog = Windows.Forms.DialogResult.OK Then
                    Call Me.grdData.requery()
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

    Protected Overridable Sub deleteRecord(ByVal pkval As Integer)
        Throw New ApplicationException("Method deleteRecord must be overwritten by inheritors")
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

    Private Sub cmdCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSelectCancel.Click
        Me.DialogResult = DialogResult.Cancel
    End Sub

    Private Function quoteSearchTerm(ByVal searchTerm As String) As String
        Return "'" & searchTerm.Replace("'", "''") & "'"
    End Function


End Class