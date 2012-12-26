Imports System.ComponentModel
Imports System.Reflection
Imports System.Reflection.Assembly
Imports System.Collections.Generic

Namespace Grid

    Public MustInherit Class CGBaseGrid
        Inherits DataGridView

        Protected Const STR_FORMAT_CURRENCY As String = "currency"
        Protected Const STR_FORMAT_SHORT_DATE As String = "dd/mm/yyyy"
        Protected Const STR_SHORT_DATE As String = "short date"
        Protected Const STR_HH_MM As String = "hh:mm"

        Private _dataLoading As Boolean = False

        ''' <summary>
        ''' Object responsible for saving Datarow data to the database
        ''' </summary>
        ''' <remarks></remarks>
        Private _CGBaseGridRowPersistor As ICGGridRowPersistor

        ''' <summary>
        ''' the dbmapper responsible for saving a datarow to the database
        ''' </summary>
        ''' <remarks></remarks>
        Private _DBMapper As DBMapper

        Protected _SearchFields As List(Of String) = New List(Of String)
        Protected _lastUsedFilter As String 'last filter set from the search textbox. needed in order to clear it!


        Public WithEvents BindingSource As BindingSource

        Protected Const INT_PK_FIELD_INDEX_NOT_SET As Integer = -1
        Protected _pkFieldIndex As Integer = INT_PK_FIELD_INDEX_NOT_SET

        Protected _traceSrc As TraceSource = Nothing

#Region "Constructor"

        Public Sub New()
            MyBase.New()
            Me.Dock = System.Windows.Forms.DockStyle.Fill
            _traceSrc = New TraceSource("CGBaseGrid")
            ' This call is required by the designer.
            InitializeComponent()
            AddHandler Me.MouseDown, AddressOf Me.grdData_MouseDown
            Me.Font = winUtils.ApplicationDefaultFont
            Me.setSkin()


        End Sub
#End Region

#Region "Properties"


        <Browsable(False)> _
        Public Property ColumnIndexToHide As Integer

        ''' <summary>
        ''' Object responsible for saving Datarow data to the database.
        ''' By default (if null) the system returns an obejct instance of CGBaseGridRowPersistor
        ''' </summary>
        ''' <remarks></remarks>
        <Browsable(False), _
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
        Public Property CGBaseGridRowPersistor() As ICGGridRowPersistor
            Get

                If _CGBaseGridRowPersistor Is Nothing Then
                    _CGBaseGridRowPersistor = New CGGridRowPersistor
                    CType(_CGBaseGridRowPersistor, CGGridRowPersistor).DBMapper = Me.DBMapper
                End If

                Return _CGBaseGridRowPersistor
            End Get
            Set(ByVal value As ICGGridRowPersistor)
                _CGBaseGridRowPersistor = value
            End Set
        End Property

        ''' <summary>
        ''' Used to save/load data from the database.
        ''' Only used in editable grids
        ''' </summary>
        <Browsable(False)> _
        Public Property DBMapper() As DBMapper
            Get
                If Me.DesignMode = False Then
                    If _DBMapper Is Nothing Then
                        Throw New ApplicationException("For Editable Grids, the DBMapper must be set")
                    End If
                End If

                Return _DBMapper
            End Get
            Set(ByVal value As DBMapper)
                _DBMapper = value
            End Set
        End Property

        ''' <summary>
        ''' This property is set to true during load of data from 
        ''' an SQL statement
        ''' </summary>
        <Browsable(False)> _
        Public ReadOnly Property DataLoading() As Boolean
            Get
                Return _dataLoading
            End Get
        End Property

        <Browsable(False)> _
        Public Property GridColumnProvider As IGridColumnProvider

        <Browsable(True), Description("Gets/Sets the column name to sort the grid on after loading data.")> _
        Public Property gpSortColumn As String

        <Browsable(True), Description("Gets/Sets the list sort direction.")> _
        Public Property gpSortDirection As ListSortDirection = ListSortDirection.Ascending


        <Browsable(True), Description("Gets/Sets the alteranting row grid back color.")> _
        Private Property gpAlternatingBackColor() As Color = Color.AntiqueWhite

        <Browsable(True), Description("Indicates whether the grid is localizable or not.")> _
        Public Property isLocalizable() As Boolean

        <Browsable(False), Description("Returns the value of column ""fieldname"" of the current row in the datagrid.")> _
        Public ReadOnly Property FieldValue(ByVal fieldname As String) As String
            Get
                Dim index As Integer = Me.CurrentRow.Index
                If index = -1 Then Return String.Empty

                Dim num As Integer = Me.getFieldColIdx(fieldname)
                If num = -1 Then Return String.Empty

                Return CStr(Me.Item(num, index).Value)

            End Get
        End Property


        <Browsable(False), Description("Gets the column index of the specified column name.")> _
        Private ReadOnly Property getFieldColIdx(ByVal fname As String) As Integer
            Get
                If (Me.Columns.Item(fname) Is Nothing) Then
                    Return -1
                End If
                Return Me.Columns.Item(fname).Index
            End Get
        End Property

        ''' <summary>
        ''' Gets/Sets Primary Key field name of each record in the grid.
        ''' This creates the need for each table in the database to have a single primary key
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <Description("Gets/Sets the primary key column name in the datasource.")> _
        Public Property gpKeyColumnName() As String

        <Description("Fully qualified name of the form used to edit details of records in the grid.")> _
        Public Property gpEditForm As String 'the fully qualified class name of the windows form used to edit/add

        <Description("Get/Set the field names that the search textbox searches on when enter is hit."), _
          DesignerSerializationVisibility(DesignerSerializationVisibility.Content), _
          Editor("System.Windows.Forms.Design.StringCollectionEditor, System.Design", _
                      "System.Drawing.Design.UITypeEditor, System.Drawing")> _
        Public Property gpSearchFields() As List(Of String)
            Get
                Return Me._SearchFields
            End Get
            Set(ByVal value As List(Of String))

                Me._SearchFields = value

            End Set

        End Property

        <Description("Gets/Sets the primary key column index."), Browsable(False)> _
        Public ReadOnly Property gpKeyColumnIndex() As Integer
            Get
                If (Me._pkFieldIndex = INT_PK_FIELD_INDEX_NOT_SET) Then
                    If Me.gpKeyColumnName = String.Empty Then
                        Throw New ArgumentException("Property gpKeyColumn must be set in order to get the Key Column Index")
                    End If

                    Me._pkFieldIndex = Me.Columns.Item(Me.gpKeyColumnName).Index
                End If
                Return Me._pkFieldIndex
            End Get

        End Property

#End Region

#Region "Methods"

        <Description("Clears all selected rows from a grid.")> _
        Public Sub ClearSelectedRows()

            Do While Me.SelectedRows.Count > 0
                Me.SelectedRows(0).Selected = False
            Loop

        End Sub

        <Description("Hides the column of the specified column index.")> _
        Public Sub hideColumn(ByVal sColName As String)
            If (Not Me.Columns.Item(sColName) Is Nothing) Then
                Me.Columns.Item(sColName).Visible = False
            End If
        End Sub


        <Description("Hides the column of the specified column index.")> _
        Public Sub hideColumn(ByVal colIdx As Integer)

            If colIdx <= -1 Then
                Exit Sub
            End If

            If (Not Me.Columns.Item(colIdx) Is Nothing) Then
                Me.Columns.Item(colIdx).Visible = False
            End If
        End Sub

        Private Sub grdData_MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs)

            Dim info As DataGridView.HitTestInfo = _
                       DirectCast(sender, DataGridView).HitTest(e.X, e.Y)

            Me.ColumnIndexToHide = -1
            Dim type As DataGridViewHitTestType = info.Type

            Select Case type
                Case DataGridViewHitTestType.None, _
                            DataGridViewHitTestType.TopLeftHeader

                    Exit Select
                Case DataGridViewHitTestType.Cell

                Case DataGridViewHitTestType.ColumnHeader

                    If (e.Button = MouseButtons.Right) Then
                        Me.ColumnIndexToHide = info.ColumnIndex

                    Else

                        'Me.SortGridInternal(info.ColumnIndex)
                        'do not sort if column is any of the filtered columns 
                        'RaiseEvent SortGrid(DirectCast(sender, DataGridView), info.ColumnIndex)

                    End If

                Case Else
                    'nothing to do here

            End Select

        End Sub


        'Private Sub _CellValidated(ByVal sender As Object, ByVal e As EventArgs) _
        '            Handles Me.CellValidated

        '    ''related to this:http://connect.microsoft.com/VisualStudio/feedback/details/167059/cellendedit-event-of-datagridview-iscurrentrowdirty-false-if-user-clicks-in-new-row
        '    ''we have moved the code to save data to the grid in the cell validated event
        '    'If Me.DataLoading Then Return
        '    'If Me.ReadOnly Then Return
        '    'Me.SaveRowToStore(Me.CurrentRow)

        'End Sub


        Public Function IdValue() As Integer
            If Me.SelectedRows.Count = 0 Then Return 0
            Return CInt(Me.SelectedRows(0).Cells(Me.gpKeyColumnIndex).Value)
        End Function

#End Region

#Region "Events"

        Public Event GridDataLoaded(ByVal iRows As Integer)

        ''' <summary>
        ''' Refreshes the datagrid with data, keeping the current row
        ''' </summary>
        ''' <remarks></remarks>
        ''' 
        Public Sub requery()

            'Dim col As Integer = Me.FirstDisplayedCell.ColumnIndex
            'Dim row As Integer = Me.FirstDisplayedCell.RowIndex
            Dim selectedValue As Object = Nothing

            If Me.SelectedRows.Count > 0 Then
                selectedValue = Me.IdValue
            End If

            Me.DataSource = Nothing
            Me.loadGrid()

            ' try to reposition the selected row to the first one that 
            ' was selected before the requery
            'Me.FirstDisplayedCell = Me.Item(col, row)

            If selectedValue IsNot Nothing Then
                Dim pos As Integer = Me.BindingSource.Find(Me.gpKeyColumnName, _
                     selectedValue)
                If pos >= 0 Then
                    Me.BindingSource.Position = pos
                End If

            End If

        End Sub


        <Description("Loads the system grid id from the database, builts an sql select statement and fills the data grid.")> _
        Public Overridable Sub loadGrid()

            If Not Me.DesignMode Then
                Me._dataLoading = True

                Try
                    Me.bindToData()
                    If Not String.IsNullOrEmpty(Me.gpSortColumn) Then
                        Me.Sort(Me.Columns(Me.gpSortColumn), Me.gpSortDirection)
                    End If
                    RaiseEvent GridDataLoaded(Me.Rows.Count)

                Finally
                    Me._dataLoading = False

                End Try
            End If

        End Sub

#End Region


        Public Sub SaveRowToStore(ByVal dataRow As DataGridViewRow)

            _traceSrc.TraceInformation("Calling SaveRowToStore")
            Me.CGBaseGridRowPersistor.SaveRowToStore(Me, Me.CurrentRow)

        End Sub

        Protected MustOverride Sub bindToData()

        ''' <summary>
        ''' Allows in place editing of grid data
        ''' </summary>
        ''' <param name="inDbMapper">The Mapper object responsible for loading and saving model objects</param>
        ''' <remarks></remarks>
        Public Sub setEditable(ByVal inDbMapper As DBMapper)

            Me.ReadOnly = False
            Me.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2
            Me.DBMapper = inDbMapper

        End Sub

        Protected Overridable Sub setSkin()

            Me.AutoGenerateColumns = False
            Me.EnableHeadersVisualStyles = True
            Me.DefaultCellStyle.NullValue = String.Empty
            Me.AllowUserToOrderColumns = True

            Me.AlternatingRowsDefaultCellStyle.BackColor = Me.gpAlternatingBackColor

            Dim rowHeadStyle As New DataGridViewCellStyle
            rowHeadStyle.BackColor = SystemColors.Desktop
            rowHeadStyle.Font = winUtils.ApplicationDefaultFont ' New Font("Tahoma", 8.25!, FontStyle.Regular, GraphicsUnit.Point, 0)
            rowHeadStyle.ForeColor = SystemColors.WindowText
            rowHeadStyle.SelectionBackColor = SystemColors.Highlight
            rowHeadStyle.SelectionForeColor = SystemColors.HighlightText

            Me.RowHeadersDefaultCellStyle = rowHeadStyle
            Me.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
            Me.GridColor = SystemColors.Control
            Me.RowTemplate.Height = 20
            Me.BackgroundColor = SystemColors.Window
            Me.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal
            Me.RowHeadersVisible = False
            Me.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing
            Me.SelectionMode = DataGridViewSelectionMode.FullRowSelect

            Me.AllowUserToAddRows = False
            Me.AllowUserToDeleteRows = False
            Me.AllowUserToResizeRows = False

            Me.EditMode = DataGridViewEditMode.EditProgrammatically
            'Me.Font = winUtils.ApplicationDefaultFont 'New Font("Tahoma", 8.25!, FontStyle.Regular, GraphicsUnit.Point, 0)

        End Sub

        Private Sub grdBindSource_ListChanged(ByVal sender As Object, _
             ByVal e As System.ComponentModel.ListChangedEventArgs) _
            Handles BindingSource.ListChanged

            RaiseEvent GridDataLoaded(Me.Rows.Count)

        End Sub

       
        Private Sub CGBaseGrid_CellValidated(ByVal sender As Object, _
                                             ByVal e As DataGridViewCellEventArgs) _
                                         Handles Me.CellValidated
            If Me.DataLoading Then Return
            If Me.ReadOnly Then Return

            If Me.IsCurrentRowDirty = False Then Return

            _traceSrc.TraceInformation("Save Row from CellValidated")
            Me.CGBaseGridRowPersistor.SaveRowToStore(Me, Me.CurrentRow)
        End Sub
    End Class

End Namespace
