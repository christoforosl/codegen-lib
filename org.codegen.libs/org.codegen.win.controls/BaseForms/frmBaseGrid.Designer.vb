<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmBaseGrid
    Inherits frmBase

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        Me.GridMode = frmGridMode.MODE_LIST
        ' Add any initialization after the InitializeComponent() call.

    End Sub

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmBaseGrid))
        Me.mnActions = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.mnAdd = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnEdit = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnDelete = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnReports = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.pnlGrid = New System.Windows.Forms.Panel()
        Me.pnlSelectToolbar = New System.Windows.Forms.Panel()
        Me.tlSripSelectMode = New System.Windows.Forms.ToolStrip()
        Me.cmdSelectAndClose = New System.Windows.Forms.ToolStripButton()
        Me.cmdSelectCancel = New System.Windows.Forms.ToolStripButton()
        Me.tsepSearch2 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsLblSearch2 = New System.Windows.Forms.ToolStripDropDownButton()
        Me.tsTxtSearch2 = New System.Windows.Forms.ToolStripTextBox()
        Me.pnlEditToolbar = New System.Windows.Forms.Panel()
        Me.tlStripList = New System.Windows.Forms.ToolStrip()
        Me.cmdAdd = New System.Windows.Forms.ToolStripButton()
        Me.cmdEdit = New System.Windows.Forms.ToolStripButton()
        Me.cmdDelete = New System.Windows.Forms.ToolStripButton()
        Me.tsepPrintAndExcel = New System.Windows.Forms.ToolStripSeparator()
        Me.cmdPrint = New System.Windows.Forms.ToolStripButton()
        Me.cmdExcel = New System.Windows.Forms.ToolStripButton()
        Me.cmdConfigureGrid = New System.Windows.Forms.ToolStripButton()
        Me.tsepSearch = New System.Windows.Forms.ToolStripSeparator()
        Me.tsLblSearch = New System.Windows.Forms.ToolStripDropDownButton()
        Me.tsTxtSearch = New System.Windows.Forms.ToolStripTextBox()
        Me.mnActions.SuspendLayout()
        Me.pnlSelectToolbar.SuspendLayout()
        Me.tlSripSelectMode.SuspendLayout()
        Me.pnlEditToolbar.SuspendLayout()
        Me.tlStripList.SuspendLayout()
        Me.SuspendLayout()
        '
        'mnActions
        '
        Me.mnActions.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnAdd, Me.mnEdit, Me.mnDelete})
        Me.mnActions.Name = "mnActions"
        Me.mnActions.Size = New System.Drawing.Size(153, 92)
        '
        'mnAdd
        '
        Me.mnAdd.Name = "mnAdd"
        Me.mnAdd.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.N), System.Windows.Forms.Keys)
        Me.mnAdd.Size = New System.Drawing.Size(152, 22)
        Me.mnAdd.Text = "Add"
        '
        'mnEdit
        '
        Me.mnEdit.Name = "mnEdit"
        Me.mnEdit.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.E), System.Windows.Forms.Keys)
        Me.mnEdit.Size = New System.Drawing.Size(152, 22)
        Me.mnEdit.Text = "Edit"
        '
        'mnDelete
        '
        Me.mnDelete.Name = "mnDelete"
        Me.mnDelete.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.D), System.Windows.Forms.Keys)
        Me.mnDelete.Size = New System.Drawing.Size(152, 22)
        Me.mnDelete.Text = "Delete"
        '
        'mnReports
        '
        Me.mnReports.Name = "mnReports"
        Me.mnReports.Size = New System.Drawing.Size(61, 4)
        '
        'pnlGrid
        '
        Me.pnlGrid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlGrid.Location = New System.Drawing.Point(0, 53)
        Me.pnlGrid.Name = "pnlGrid"
        Me.pnlGrid.Size = New System.Drawing.Size(832, 301)
        Me.pnlGrid.TabIndex = 3
        '
        'pnlSelectToolbar
        '
        Me.pnlSelectToolbar.Controls.Add(Me.tlSripSelectMode)
        Me.pnlSelectToolbar.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlSelectToolbar.Location = New System.Drawing.Point(0, 0)
        Me.pnlSelectToolbar.Name = "pnlSelectToolbar"
        Me.pnlSelectToolbar.Size = New System.Drawing.Size(832, 28)
        Me.pnlSelectToolbar.TabIndex = 0
        '
        'tlSripSelectMode
        '
        Me.tlSripSelectMode.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmdSelectAndClose, Me.cmdSelectCancel, Me.tsepSearch2, Me.tsLblSearch2, Me.tsTxtSearch2})
        Me.tlSripSelectMode.Location = New System.Drawing.Point(0, 0)
        Me.tlSripSelectMode.Name = "tlSripSelectMode"
        Me.tlSripSelectMode.Size = New System.Drawing.Size(832, 25)
        Me.tlSripSelectMode.TabIndex = 0
        Me.tlSripSelectMode.Text = "ToolStrip1"
        '
        'cmdSelectAndClose
        '
        Me.cmdSelectAndClose.Image = CType(resources.GetObject("cmdSelectAndClose.Image"), System.Drawing.Image)
        Me.cmdSelectAndClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdSelectAndClose.Name = "cmdSelectAndClose"
        Me.cmdSelectAndClose.Size = New System.Drawing.Size(115, 22)
        Me.cmdSelectAndClose.Text = "Select And Close"
        '
        'cmdSelectCancel
        '
        Me.cmdSelectCancel.Image = Global.org.codegen.win.controls.My.Resources.Resources.synch
        Me.cmdSelectCancel.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdSelectCancel.Name = "cmdSelectCancel"
        Me.cmdSelectCancel.Size = New System.Drawing.Size(63, 22)
        Me.cmdSelectCancel.Text = "Cancel"
        '
        'tsepSearch2
        '
        Me.tsepSearch2.Name = "tsepSearch2"
        Me.tsepSearch2.Size = New System.Drawing.Size(6, 25)
        '
        'tsLblSearch2
        '
        Me.tsLblSearch2.Name = "tsLblSearch2"
        Me.tsLblSearch2.Size = New System.Drawing.Size(58, 22)
        Me.tsLblSearch2.Text = "Search:"
        '
        'tsTxtSearch2
        '
        Me.tsTxtSearch2.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.tsTxtSearch2.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.HistoryList
        Me.tsTxtSearch2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.tsTxtSearch2.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(161, Byte))
        Me.tsTxtSearch2.Name = "tsTxtSearch2"
        Me.tsTxtSearch2.Size = New System.Drawing.Size(100, 25)
        '
        'pnlEditToolbar
        '
        Me.pnlEditToolbar.Controls.Add(Me.tlStripList)
        Me.pnlEditToolbar.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlEditToolbar.Location = New System.Drawing.Point(0, 28)
        Me.pnlEditToolbar.Name = "pnlEditToolbar"
        Me.pnlEditToolbar.Size = New System.Drawing.Size(832, 25)
        Me.pnlEditToolbar.TabIndex = 0
        '
        'tlStripList
        '
        Me.tlStripList.AccessibleRole = System.Windows.Forms.AccessibleRole.None
        Me.tlStripList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlStripList.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmdAdd, Me.cmdEdit, Me.cmdDelete, Me.tsepPrintAndExcel, Me.cmdPrint, Me.cmdExcel, Me.cmdConfigureGrid, Me.tsepSearch, Me.tsLblSearch, Me.tsTxtSearch})
        Me.tlStripList.Location = New System.Drawing.Point(0, 0)
        Me.tlStripList.Name = "tlStripList"
        Me.tlStripList.Size = New System.Drawing.Size(832, 25)
        Me.tlStripList.TabIndex = 3
        Me.tlStripList.Text = "tlStripList"
        '
        'cmdAdd
        '
        Me.cmdAdd.Image = Global.org.codegen.win.controls.My.Resources.Resources.AddTableHS
        Me.cmdAdd.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdAdd.Name = "cmdAdd"
        Me.cmdAdd.Size = New System.Drawing.Size(51, 22)
        Me.cmdAdd.Text = "New"
        '
        'cmdEdit
        '
        Me.cmdEdit.Image = Global.org.codegen.win.controls.My.Resources.Resources.EditTableHS
        Me.cmdEdit.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdEdit.Name = "cmdEdit"
        Me.cmdEdit.Size = New System.Drawing.Size(47, 22)
        Me.cmdEdit.Text = "Edit"
        '
        'cmdDelete
        '
        Me.cmdDelete.Image = Global.org.codegen.win.controls.My.Resources.Resources.DeleteHS
        Me.cmdDelete.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdDelete.Name = "cmdDelete"
        Me.cmdDelete.Size = New System.Drawing.Size(60, 22)
        Me.cmdDelete.Text = "Delete"
        '
        'tsepPrintAndExcel
        '
        Me.tsepPrintAndExcel.Name = "tsepPrintAndExcel"
        Me.tsepPrintAndExcel.Size = New System.Drawing.Size(6, 25)
        '
        'cmdPrint
        '
        Me.cmdPrint.Image = Global.org.codegen.win.controls.My.Resources.Resources.PrintHS
        Me.cmdPrint.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.Size = New System.Drawing.Size(52, 22)
        Me.cmdPrint.Text = "Print"
        '
        'cmdExcel
        '
        Me.cmdExcel.Image = Global.org.codegen.win.controls.My.Resources.Resources.toExcel
        Me.cmdExcel.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdExcel.Name = "cmdExcel"
        Me.cmdExcel.Size = New System.Drawing.Size(70, 22)
        Me.cmdExcel.Text = "To Excel"
        '
        'cmdConfigureGrid
        '
        Me.cmdConfigureGrid.Image = Global.org.codegen.win.controls.My.Resources.Resources.synch
        Me.cmdConfigureGrid.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdConfigureGrid.Name = "cmdConfigureGrid"
        Me.cmdConfigureGrid.Size = New System.Drawing.Size(118, 22)
        Me.cmdConfigureGrid.Text = "Choose Columns"
        '
        'tsepSearch
        '
        Me.tsepSearch.Name = "tsepSearch"
        Me.tsepSearch.Size = New System.Drawing.Size(6, 25)
        '
        'tsLblSearch
        '
        Me.tsLblSearch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.tsLblSearch.Name = "tsLblSearch"
        Me.tsLblSearch.Size = New System.Drawing.Size(58, 22)
        Me.tsLblSearch.Text = "Search:"
        '
        'tsTxtSearch
        '
        Me.tsTxtSearch.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.tsTxtSearch.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.HistoryList
        Me.tsTxtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.tsTxtSearch.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(161, Byte))
        Me.tsTxtSearch.Name = "tsTxtSearch"
        Me.tsTxtSearch.Size = New System.Drawing.Size(100, 25)
        '
        'frmBaseGrid
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(832, 354)
        Me.Controls.Add(Me.pnlGrid)
        Me.Controls.Add(Me.pnlEditToolbar)
        Me.Controls.Add(Me.pnlSelectToolbar)
        Me.MaximizeBox = True
        Me.MinimizeBox = True
        Me.Name = "frmBaseGrid"
        Me.Text = "frmBaseGrid"
        Me.mnActions.ResumeLayout(False)
        Me.pnlSelectToolbar.ResumeLayout(False)
        Me.pnlSelectToolbar.PerformLayout()
        Me.tlSripSelectMode.ResumeLayout(False)
        Me.tlSripSelectMode.PerformLayout()
        Me.pnlEditToolbar.ResumeLayout(False)
        Me.pnlEditToolbar.PerformLayout()
        Me.tlStripList.ResumeLayout(False)
        Me.tlStripList.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Public WithEvents mnActions As System.Windows.Forms.ContextMenuStrip
    Public WithEvents mnReports As System.Windows.Forms.ContextMenuStrip
    Public WithEvents pnlGrid As System.Windows.Forms.Panel

    Public WithEvents tlStripList As System.Windows.Forms.ToolStrip
    Public WithEvents cmdAdd As System.Windows.Forms.ToolStripButton
    Public WithEvents cmdEdit As System.Windows.Forms.ToolStripButton
    Public WithEvents cmdDelete As System.Windows.Forms.ToolStripButton
    Public WithEvents tsepPrintAndExcel As System.Windows.Forms.ToolStripSeparator
    Public WithEvents cmdPrint As System.Windows.Forms.ToolStripButton
    Public WithEvents cmdExcel As System.Windows.Forms.ToolStripButton
    Public WithEvents cmdConfigureGrid As System.Windows.Forms.ToolStripButton
    Public WithEvents tsepSearch As System.Windows.Forms.ToolStripSeparator
    Public WithEvents tsLblSearch As System.Windows.Forms.ToolStripDropDownButton
	Public WithEvents tsTxtSearch As System.Windows.Forms.ToolStripTextBox
	Friend WithEvents pnlEditToolbar As System.Windows.Forms.Panel
	Friend WithEvents pnlSelectToolbar As System.Windows.Forms.Panel
	Friend WithEvents tlSripSelectMode As System.Windows.Forms.ToolStrip
	Friend WithEvents cmdSelectAndClose As System.Windows.Forms.ToolStripButton
	Friend WithEvents cmdSelectCancel As System.Windows.Forms.ToolStripButton
	Public WithEvents tsTxtSearch2 As System.Windows.Forms.ToolStripTextBox
    Public WithEvents tsLblSearch2 As System.Windows.Forms.ToolStripDropDownButton
	Public WithEvents tsepSearch2 As System.Windows.Forms.ToolStripSeparator
	Friend WithEvents mnAdd As System.Windows.Forms.ToolStripMenuItem
	Friend WithEvents mnEdit As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnDelete As System.Windows.Forms.ToolStripMenuItem



End Class
