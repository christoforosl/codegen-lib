
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Public Class ucDateRange
    Inherits UserControl

    Public Event dtKeyDownEvent(ByVal sender As Object, ByVal e As KeyEventArgs)
    Public Event dtRangeChangedEvent(ByVal sender As Object, ByVal e As EventArgs)

    ' Methods
    Public Sub New()
        AddHandler MyBase.Load, New EventHandler(AddressOf Me.dateRange_Load)
        Me._fromLabel = "From"
        Me._tolabel = "To"
        Me.InitializeComponent()
    End Sub

    Private Sub cmdMenu_Click(ByVal sender As Object, ByVal e As EventArgs)
        Me.cmsDRange.Show(Control.MousePosition)
    End Sub

    Private Sub dateRange_Load(ByVal sender As Object, ByVal e As EventArgs)
        Me.from_label.Text = Me.FromLabel
        Me.to_label.Text = Me.Tolabel
    End Sub

    <DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If (disposing AndAlso (Not Me.components Is Nothing)) Then
                Me.components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    Private Sub dtFrom_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs)
        
        RaiseEvent dtKeyDownEvent(sender, e)

    End Sub


    Private Sub dtFrom_Validated(ByVal sender As Object, ByVal e As EventArgs)
        
        RaiseEvent dtRangeChangedEvent(Me, e)

    End Sub

    Private Sub dtTo_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs)
        RaiseEvent dtKeyDownEvent(sender, e)

    End Sub

    Private Sub dtTo_LostFocus(ByVal sender As Object, ByVal e As EventArgs)
    End Sub

    Private Sub dtTo_Validated(ByVal sender As Object, ByVal e As EventArgs)
        
        RaiseEvent dtRangeChangedEvent(Me, e)

    End Sub

    <DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.from_label = New Label
        Me.to_label = New Label
        Me.cmsDRange = New ContextMenuStrip(Me.components)
        Me.ThisMonthToolStripMenuItem = New ToolStripMenuItem
        Me.LastMonthToolStripMenuItem = New ToolStripMenuItem
        Me.MonthsAgoToolStripMenuItem = New ToolStripMenuItem
        Me.ToolStripSeparator1 = New ToolStripSeparator
        Me.ThisQuarterToolStripMenuItem = New ToolStripMenuItem
        Me.LastQuarterToolStripMenuItem = New ToolStripMenuItem
        Me.ToolStripSeparator2 = New ToolStripSeparator
        Me.ThisYearToolStripMenuItem = New ToolStripMenuItem
        Me.LastYearToolStripMenuItem = New ToolStripMenuItem
        Me.cmdMenu = New Button
        Me.dtTo = New CGDateTextBox(Me.components)
        Me.dtFrom = New CGDateTextBox(Me.components)
        Me.cmsDRange.SuspendLayout()
        Me.SuspendLayout()
        Me.from_label.AutoSize = True
        Dim point As New Point(4, 6)
        Me.from_label.Location = point
        Me.from_label.Name = "from_label"
        Dim size As New Size(30, 13)
        Me.from_label.Size = size
        Me.from_label.TabIndex = 2
        Me.from_label.Text = "From"
        Me.to_label.Anchor = (AnchorStyles.Right Or AnchorStyles.Top)
        Me.to_label.AutoSize = True
        point = New Point(&HA1, 6)
        Me.to_label.Location = point
        Me.to_label.Name = "to_label"
        size = New Size(20, 13)
        Me.to_label.Size = size
        Me.to_label.TabIndex = 3
        Me.to_label.Text = "To"
        Me.cmsDRange.Items.AddRange(New ToolStripItem() {Me.ThisMonthToolStripMenuItem, Me.LastMonthToolStripMenuItem, Me.MonthsAgoToolStripMenuItem, Me.ToolStripSeparator1, Me.ThisQuarterToolStripMenuItem, Me.LastQuarterToolStripMenuItem, Me.ToolStripSeparator2, Me.ThisYearToolStripMenuItem, Me.LastYearToolStripMenuItem})
        Me.cmsDRange.Name = "cmsDRange"
        Me.cmsDRange.ShowImageMargin = False
        Me.cmsDRange.ShowItemToolTips = False
        size = New Size(&H7F, 170)
        Me.cmsDRange.Size = size
        Me.ThisMonthToolStripMenuItem.Name = "ThisMonthToolStripMenuItem"
        size = New Size(&H7E, &H16)
        Me.ThisMonthToolStripMenuItem.Size = size
        Me.ThisMonthToolStripMenuItem.Text = "This Month"
        Me.LastMonthToolStripMenuItem.Name = "LastMonthToolStripMenuItem"
        size = New Size(&H7E, &H16)
        Me.LastMonthToolStripMenuItem.Size = size
        Me.LastMonthToolStripMenuItem.Text = "Last Month"
        Me.MonthsAgoToolStripMenuItem.Name = "MonthsAgoToolStripMenuItem"
        size = New Size(&H7E, &H16)
        Me.MonthsAgoToolStripMenuItem.Size = size
        Me.MonthsAgoToolStripMenuItem.Text = "2 Months Ago"
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        size = New Size(&H7B, 6)
        Me.ToolStripSeparator1.Size = size
        Me.ThisQuarterToolStripMenuItem.Name = "ThisQuarterToolStripMenuItem"
        size = New Size(&H7E, &H16)
        Me.ThisQuarterToolStripMenuItem.Size = size
        Me.ThisQuarterToolStripMenuItem.Text = "This Quarter"
        Me.LastQuarterToolStripMenuItem.Name = "LastQuarterToolStripMenuItem"
        size = New Size(&H7E, &H16)
        Me.LastQuarterToolStripMenuItem.Size = size
        Me.LastQuarterToolStripMenuItem.Text = "Last Quarter"
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        size = New Size(&H7B, 6)
        Me.ToolStripSeparator2.Size = size
        Me.ThisYearToolStripMenuItem.Name = "ThisYearToolStripMenuItem"
        size = New Size(&H7E, &H16)
        Me.ThisYearToolStripMenuItem.Size = size
        Me.ThisYearToolStripMenuItem.Text = "This Year"
        Me.LastYearToolStripMenuItem.Name = "LastYearToolStripMenuItem"
        size = New Size(&H7E, &H16)
        Me.LastYearToolStripMenuItem.Size = size
        Me.LastYearToolStripMenuItem.Text = "Last Year"
        Me.cmdMenu.Anchor = (AnchorStyles.Right Or (AnchorStyles.Bottom Or AnchorStyles.Top))
        Me.cmdMenu.BackgroundImage = My.Resources.doubleDownArrows
        Me.cmdMenu.BackgroundImageLayout = ImageLayout.None
        Me.cmdMenu.FlatAppearance.BorderSize = 0
        Me.cmdMenu.FlatStyle = FlatStyle.Flat
        point = New Point(&H108, 6)
        Me.cmdMenu.Location = point
        Me.cmdMenu.Name = "cmdMenu"
        size = New Size(14, 15)
        Me.cmdMenu.Size = size
        Me.cmdMenu.TabIndex = 5
        Me.cmdMenu.TextImageRelation = TextImageRelation.ImageAboveText
        Me.cmdMenu.UseVisualStyleBackColor = True
        Me.dtTo.Anchor = (AnchorStyles.Right Or (AnchorStyles.Bottom Or AnchorStyles.Top))
        Me.dtTo.DBFieldName = "dtTo"
        Me.dtTo.FormatPattern = ""
        Me.dtTo.isMandatory = False
        Me.dtTo.label = Nothing
        point = New Point(&HBA, 3)
        Me.dtTo.Location = point
        Me.dtTo.Name = "dtTo"
        Me.dtTo.numoflines = 1
        size = New Size(&H49, 20)
        Me.dtTo.Size = size
        Me.dtTo.TabIndex = 1
        Me.dtTo.Text = "03/01/2007"
        Me.dtTo.value = "03/01/2007"
        Me.dtFrom.Anchor = (AnchorStyles.Right Or (AnchorStyles.Bottom Or AnchorStyles.Top))
        Me.dtFrom.DBFieldName = "dtFrom"
        Me.dtFrom.FormatPattern = ""
        Me.dtFrom.isMandatory = False
        Me.dtFrom.label = Nothing
        point = New Point(&H56, 3)
        Me.dtFrom.Location = point
        Me.dtFrom.MaxLength = 10
        Me.dtFrom.Name = "dtFrom"
        Me.dtFrom.numoflines = 1
        size = New Size(&H45, 20)
        Me.dtFrom.Size = size
        Me.dtFrom.TabIndex = 0
        Me.dtFrom.Text = "11/06/2007"
        Me.dtFrom.value = "11/06/2007"
        Dim ef As New SizeF(6.0!, 13.0!)
        Me.AutoScaleDimensions = ef
        Me.AutoScaleMode = AutoScaleMode.Font
        Me.Controls.Add(Me.cmdMenu)
        Me.Controls.Add(Me.to_label)
        Me.Controls.Add(Me.from_label)
        Me.Controls.Add(Me.dtTo)
        Me.Controls.Add(Me.dtFrom)
        Me.Name = "ucDateRange"
        size = New Size(&H11A, &H1B)
        Me.Size = size
        Me.cmsDRange.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()
    End Sub

    Private Sub LastMonthToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)

        Dim dateValue As DateTime = DateAndTime.DateAdd(DateInterval.Month, -1, DateAndTime.Now)
        Me.dtFrom.value = CStr(DateAndTime.DateSerial(DateAndTime.Year(dateValue), DateAndTime.Month(dateValue), 1))
        Me.dtTo.value = CStr(DateAndTime.DateAdd(DateInterval.Day, -1, _
                    DateAndTime.DateAdd(DateInterval.Month, 1, Me.dDatefrom.GetValueOrDefault)))

        RaiseEvent dtRangeChangedEvent(Me, e)

    End Sub

    Private Sub LastQuarterToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)

        Dim dateValue As DateTime = DateAndTime.DateAdd(DateInterval.Quarter, -1, DateAndTime.Now)
        Me.dtFrom.value = CStr(DateAndTime.DateSerial(DateAndTime.Year(dateValue), DateAndTime.Month(dateValue), 1))
        Me.dtTo.value = CStr(DateAndTime.DateAdd(DateInterval.Day, -1, _
                                                 DateAndTime.DateAdd(DateInterval.Quarter, 1, Me.dDatefrom.GetValueOrDefault)))

        RaiseEvent dtRangeChangedEvent(Me, e)

    End Sub

    Private Sub LastYearToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)

        Dim dateValue As DateTime = DateAndTime.DateAdd(DateInterval.Year, -1, DateAndTime.Now)
        Me.dtFrom.value = CStr(DateAndTime.DateSerial(DateAndTime.Year(dateValue), 1, 1))
        Me.dtTo.value = CStr(DateAndTime.DateSerial(DateAndTime.Year(dateValue), 12, &H1F))

        RaiseEvent dtRangeChangedEvent(Me, e)

    End Sub

    Private Sub MonthsAgoToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim dateValue As DateTime = DateAndTime.DateAdd(DateInterval.Month, -2, DateAndTime.Now)
        Me.dtFrom.value = CStr(DateAndTime.DateSerial(DateAndTime.Year(dateValue), DateAndTime.Month(dateValue), 1))
        Me.dtTo.value = CStr(DateAndTime.DateAdd(DateInterval.Day, -1, _
                                                 DateAndTime.DateAdd(DateInterval.Month, 1, Me.dDatefrom.GetValueOrDefault)))
        RaiseEvent dtRangeChangedEvent(Me, e)
    End Sub

    Private Sub ThisMonthToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        Me.dtFrom.value = CStr(DateAndTime.DateSerial(DateAndTime.Year(DateAndTime.Now), DateAndTime.Month(DateAndTime.Now), 1))
        Me.dtTo.value = CStr(DateAndTime.DateAdd(DateInterval.Day, -1, _
                        DateAndTime.DateAdd(DateInterval.Month, 1, Me.dDatefrom.GetValueOrDefault)))
        RaiseEvent dtRangeChangedEvent(Me, e)
    End Sub

    Private Sub ThisQuarterToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim dateValue As DateTime = DateAndTime.DateAdd(DateInterval.Quarter, -1, DateAndTime.Now)
        dateValue = DateAndTime.DateAdd(DateInterval.Quarter, 1, DateAndTime.DateSerial(DateAndTime.Year(dateValue), DateAndTime.Month(dateValue), 1))
        Me.dtFrom.value = CStr(dateValue)
        Me.dtTo.value = CStr(DateAndTime.DateAdd(DateInterval.Day, -1, _
                                                 DateAndTime.DateAdd(DateInterval.Quarter, 1, Me.dDatefrom.GetValueOrDefault)))
        RaiseEvent dtRangeChangedEvent(Me, e)
    End Sub

    Private Sub ThisYearToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)

        Me.dtFrom.value = CStr(DateAndTime.DateSerial(DateAndTime.Year(DateAndTime.Now), 1, 1))
        Me.dtTo.value = CStr(DateAndTime.DateSerial(DateAndTime.Year(DateAndTime.Now), 12, &H1F))

        RaiseEvent dtRangeChangedEvent(Me, e)

    End Sub


    ' Properties
    Friend Overridable Property cmdMenu() As Button
        Get
            Return Me._cmdMenu
        End Get
        Set(ByVal WithEventsValue As Button)
            If (Not Me._cmdMenu Is Nothing) Then
                RemoveHandler Me._cmdMenu.Click, New EventHandler(AddressOf Me.cmdMenu_Click)
            End If
            Me._cmdMenu = WithEventsValue
            If (Not Me._cmdMenu Is Nothing) Then
                AddHandler Me._cmdMenu.Click, New EventHandler(AddressOf Me.cmdMenu_Click)
            End If
        End Set
    End Property

    Friend Overridable Property cmsDRange() As ContextMenuStrip
        Get
            Return Me._cmsDRange
        End Get
        Set(ByVal WithEventsValue As ContextMenuStrip)
            Me._cmsDRange = WithEventsValue
        End Set
    End Property

    Public Property dateFrom() As String
        Get
            If Information.IsDate(Me.dtFrom.Text) Then
                Return CStr(CDate(Me.dtFrom.Text))
            End If
            Return String.Empty
        End Get
        Set(ByVal value As String)
            If Information.IsDate(value) Then
                Me.dtFrom.Text = value
            Else
                Me.dtFrom.Text = String.Empty
            End If
        End Set
    End Property

    Public Property dateTo() As String
        Get
            If Information.IsDate(Me.dtTo.Text) Then
                Return CStr(CDate(Me.dtTo.Text))
            End If
            Return String.Empty
        End Get
        Set(ByVal value As String)
            If Information.IsDate(value) Then
                Me.dtTo.Text = value
            Else
                Me.dtTo.Text = String.Empty
            End If
        End Set
    End Property

    Public Property DBFieldName() As String
        Get
            If (Me._dbFieldName = String.Empty) Then
                Return Me.Name
            End If
            Return Me._dbFieldName
        End Get
        Set(ByVal value As String)
            Me._dbFieldName = value
        End Set
    End Property

    Public ReadOnly Property dDatefrom() As DateTime?
        Get
            If Information.IsDate(Me.dtFrom.Text) Then
                Return CDate(Me.dtFrom.Text)
            End If
            Return Nothing
        End Get
    End Property

    Public ReadOnly Property dDateTo() As DateTime?
        Get
            If Information.IsDate(Me.dtTo.Text) Then
                Return CDate(Me.dtTo.Text)
            End If
            Return Nothing
        End Get
    End Property

    Friend Overridable Property dtFrom() As CGDateTextBox
        Get
            Return Me._dtFrom
        End Get
        Set(ByVal WithEventsValue As CGDateTextBox)
            If (Not Me._dtFrom Is Nothing) Then
                RemoveHandler Me._dtFrom.KeyDown, New KeyEventHandler(AddressOf Me.dtFrom_KeyDown)
                RemoveHandler Me._dtFrom.Validated, New EventHandler(AddressOf Me.dtFrom_Validated)
            End If
            Me._dtFrom = WithEventsValue
            If (Not Me._dtFrom Is Nothing) Then
                AddHandler Me._dtFrom.KeyDown, New KeyEventHandler(AddressOf Me.dtFrom_KeyDown)
                AddHandler Me._dtFrom.Validated, New EventHandler(AddressOf Me.dtFrom_Validated)
            End If
        End Set
    End Property

    Friend Overridable Property dtTo() As CGDateTextBox
        Get
            Return Me._dtTo
        End Get
        Set(ByVal WithEventsValue As CGDateTextBox)
            If (Not Me._dtTo Is Nothing) Then
                RemoveHandler Me._dtTo.LostFocus, New EventHandler(AddressOf Me.dtTo_LostFocus)
                RemoveHandler Me._dtTo.KeyDown, New KeyEventHandler(AddressOf Me.dtTo_KeyDown)
                RemoveHandler Me._dtTo.Validated, New EventHandler(AddressOf Me.dtTo_Validated)
            End If
            Me._dtTo = WithEventsValue
            If (Not Me._dtTo Is Nothing) Then
                AddHandler Me._dtTo.LostFocus, New EventHandler(AddressOf Me.dtTo_LostFocus)
                AddHandler Me._dtTo.KeyDown, New KeyEventHandler(AddressOf Me.dtTo_KeyDown)
                AddHandler Me._dtTo.Validated, New EventHandler(AddressOf Me.dtTo_Validated)
            End If
        End Set
    End Property

    Friend Overridable Property from_label() As Label
        Get
            Return Me._from_label
        End Get
        Set(ByVal WithEventsValue As Label)
            Me._from_label = WithEventsValue
        End Set
    End Property

    Public Property FromLabel() As String
        Get
            Return Me._fromLabel
        End Get
        Set(ByVal value As String)
            Me._fromLabel = value
        End Set
    End Property

    Public ReadOnly Property isValidDateRange() As Boolean
        Get
            Return (Information.IsDate(Me.dtFrom.Text) AndAlso Information.IsDate(Me.dtTo.Text))
        End Get
    End Property

    Friend Overridable Property LastMonthToolStripMenuItem() As ToolStripMenuItem
        Get
            Return Me._LastMonthToolStripMenuItem
        End Get
        Set(ByVal WithEventsValue As ToolStripMenuItem)
            If (Not Me._LastMonthToolStripMenuItem Is Nothing) Then
                RemoveHandler Me._LastMonthToolStripMenuItem.Click, New EventHandler(AddressOf Me.LastMonthToolStripMenuItem_Click)
            End If
            Me._LastMonthToolStripMenuItem = WithEventsValue
            If (Not Me._LastMonthToolStripMenuItem Is Nothing) Then
                AddHandler Me._LastMonthToolStripMenuItem.Click, New EventHandler(AddressOf Me.LastMonthToolStripMenuItem_Click)
            End If
        End Set
    End Property

    Friend Overridable Property LastQuarterToolStripMenuItem() As ToolStripMenuItem
        Get
            Return Me._LastQuarterToolStripMenuItem
        End Get
        Set(ByVal WithEventsValue As ToolStripMenuItem)
            If (Not Me._LastQuarterToolStripMenuItem Is Nothing) Then
                RemoveHandler Me._LastQuarterToolStripMenuItem.Click, New EventHandler(AddressOf Me.LastQuarterToolStripMenuItem_Click)
            End If
            Me._LastQuarterToolStripMenuItem = WithEventsValue
            If (Not Me._LastQuarterToolStripMenuItem Is Nothing) Then
                AddHandler Me._LastQuarterToolStripMenuItem.Click, New EventHandler(AddressOf Me.LastQuarterToolStripMenuItem_Click)
            End If
        End Set
    End Property

    Friend Overridable Property LastYearToolStripMenuItem() As ToolStripMenuItem
        Get
            Return Me._LastYearToolStripMenuItem
        End Get
        Set(ByVal WithEventsValue As ToolStripMenuItem)
            If (Not Me._LastYearToolStripMenuItem Is Nothing) Then
                RemoveHandler Me._LastYearToolStripMenuItem.Click, New EventHandler(AddressOf Me.LastYearToolStripMenuItem_Click)
            End If
            Me._LastYearToolStripMenuItem = WithEventsValue
            If (Not Me._LastYearToolStripMenuItem Is Nothing) Then
                AddHandler Me._LastYearToolStripMenuItem.Click, New EventHandler(AddressOf Me.LastYearToolStripMenuItem_Click)
            End If
        End Set
    End Property

    Friend Overridable Property MonthsAgoToolStripMenuItem() As ToolStripMenuItem
        Get
            Return Me._MonthsAgoToolStripMenuItem
        End Get
        Set(ByVal WithEventsValue As ToolStripMenuItem)
            If (Not Me._MonthsAgoToolStripMenuItem Is Nothing) Then
                RemoveHandler Me._MonthsAgoToolStripMenuItem.Click, New EventHandler(AddressOf Me.MonthsAgoToolStripMenuItem_Click)
            End If
            Me._MonthsAgoToolStripMenuItem = WithEventsValue
            If (Not Me._MonthsAgoToolStripMenuItem Is Nothing) Then
                AddHandler Me._MonthsAgoToolStripMenuItem.Click, New EventHandler(AddressOf Me.MonthsAgoToolStripMenuItem_Click)
            End If
        End Set
    End Property

    Friend Overridable Property ThisMonthToolStripMenuItem() As ToolStripMenuItem
        Get
            Return Me._ThisMonthToolStripMenuItem
        End Get
        Set(ByVal WithEventsValue As ToolStripMenuItem)
            If (Not Me._ThisMonthToolStripMenuItem Is Nothing) Then
                RemoveHandler Me._ThisMonthToolStripMenuItem.Click, New EventHandler(AddressOf Me.ThisMonthToolStripMenuItem_Click)
            End If
            Me._ThisMonthToolStripMenuItem = WithEventsValue
            If (Not Me._ThisMonthToolStripMenuItem Is Nothing) Then
                AddHandler Me._ThisMonthToolStripMenuItem.Click, New EventHandler(AddressOf Me.ThisMonthToolStripMenuItem_Click)
            End If
        End Set
    End Property

    Friend Overridable Property ThisQuarterToolStripMenuItem() As ToolStripMenuItem
        Get
            Return Me._ThisQuarterToolStripMenuItem
        End Get
        Set(ByVal WithEventsValue As ToolStripMenuItem)
            If (Not Me._ThisQuarterToolStripMenuItem Is Nothing) Then
                RemoveHandler Me._ThisQuarterToolStripMenuItem.Click, New EventHandler(AddressOf Me.ThisQuarterToolStripMenuItem_Click)
            End If
            Me._ThisQuarterToolStripMenuItem = WithEventsValue
            If (Not Me._ThisQuarterToolStripMenuItem Is Nothing) Then
                AddHandler Me._ThisQuarterToolStripMenuItem.Click, New EventHandler(AddressOf Me.ThisQuarterToolStripMenuItem_Click)
            End If
        End Set
    End Property

    Friend Overridable Property ThisYearToolStripMenuItem() As ToolStripMenuItem
        Get
            Return Me._ThisYearToolStripMenuItem
        End Get
        Set(ByVal WithEventsValue As ToolStripMenuItem)
            If (Not Me._ThisYearToolStripMenuItem Is Nothing) Then
                RemoveHandler Me._ThisYearToolStripMenuItem.Click, New EventHandler(AddressOf Me.ThisYearToolStripMenuItem_Click)
            End If
            Me._ThisYearToolStripMenuItem = WithEventsValue
            If (Not Me._ThisYearToolStripMenuItem Is Nothing) Then
                AddHandler Me._ThisYearToolStripMenuItem.Click, New EventHandler(AddressOf Me.ThisYearToolStripMenuItem_Click)
            End If
        End Set
    End Property

    Friend Overridable Property to_label() As Label
        Get
            Return Me._to_label
        End Get
        Set(ByVal WithEventsValue As Label)
            Me._to_label = WithEventsValue
        End Set
    End Property

    Public Property Tolabel() As String
        Get
            Return Me._tolabel
        End Get
        Set(ByVal value As String)
            Me._tolabel = value
        End Set
    End Property

    Friend Overridable Property ToolStripSeparator1() As ToolStripSeparator
        Get
            Return Me._ToolStripSeparator1
        End Get
        Set(ByVal WithEventsValue As ToolStripSeparator)
            Me._ToolStripSeparator1 = WithEventsValue
        End Set
    End Property

    Friend Overridable Property ToolStripSeparator2() As ToolStripSeparator
        Get
            Return Me._ToolStripSeparator2
        End Get
        Set(ByVal WithEventsValue As ToolStripSeparator)
            Me._ToolStripSeparator2 = WithEventsValue
        End Set
    End Property


    ' Fields
    Private _cmdMenu As Button
    Private _cmsDRange As ContextMenuStrip
    Private _dbFieldName As String
    Private _dtFrom As CGDateTextBox
    Private _dtTo As CGDateTextBox
    Private _from_label As Label
    Private _fromLabel As String
    Private _LastMonthToolStripMenuItem As ToolStripMenuItem
    Private _LastQuarterToolStripMenuItem As ToolStripMenuItem
    Private _LastYearToolStripMenuItem As ToolStripMenuItem
    Private _MonthsAgoToolStripMenuItem As ToolStripMenuItem
    Private _ThisMonthToolStripMenuItem As ToolStripMenuItem
    Private _ThisQuarterToolStripMenuItem As ToolStripMenuItem
    Private _ThisYearToolStripMenuItem As ToolStripMenuItem
    Private _to_label As Label
    Private _tolabel As String
    Private _ToolStripSeparator1 As ToolStripSeparator
    Private _ToolStripSeparator2 As ToolStripSeparator
    Private components As System.ComponentModel.IContainer


End Class
