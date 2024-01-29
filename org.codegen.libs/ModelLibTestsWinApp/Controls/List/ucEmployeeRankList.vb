Imports System.ComponentModel
Imports ModelLibTestsWinApp.Forms.Edit

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Public Class ucEmployeeRankList
    Inherits System.Windows.Forms.UserControl

#Region "Designer"
    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()

        Me.RankId = New DataGridViewTextBoxColumn
        Me.Rank = New DataGridViewTextBoxColumn

        Me.grdEmployeeRank = New org.codegen.win.controls.Grid.CGSQLGrid()
        Me.SuspendLayout()
        '
        'grdEmployeeRank
        '
        Me.grdEmployeeRank.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdEmployeeRank.gpEditForm = frmEmployeeRankDetails.GetType()
        Me.grdEmployeeRank.gpSelectFields = "*"
        Me.grdEmployeeRank.gpSelectFrom = "EmployeeRank"

        Me.grdEmployeeRank.GridColumnProvider = Nothing
        Me.grdEmployeeRank.gpKeyColumnName = "RankId"
        Me.grdEmployeeRank.Location = New System.Drawing.Point(0, 0)
        Me.grdEmployeeRank.Name = "grdEmployeeRank"
        Me.grdEmployeeRank.Size = New System.Drawing.Size(573, 262)
        Me.grdEmployeeRank.TabIndex = 0

        ' column: RankId
        RankId.CellTemplate = New DataGridViewTextBoxCell
        RankId.Name = "RankId"
        RankId.DataPropertyName = "RankId"
        RankId.ReadOnly = True
        RankId.HeaderText = "RankId"
        RankId.HeaderCell.Value = "RankId"
        RankId.DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight
        RankId.HeaderCell.Style.Alignment = DataGridViewContentAlignment.TopRight
        RankId.Width = 50
        RankId.Visible = False
        '**** End Setup of column: RankId


        ' column: Rank
        Rank.CellTemplate = New DataGridViewTextBoxCell
        Rank.Name = "Rank"
        Rank.DataPropertyName = "Rank"
        Rank.ReadOnly = True
        Rank.HeaderText = "Rank"
        Rank.HeaderCell.Value = "Rank"
        Rank.DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft
        Rank.HeaderCell.Style.Alignment = DataGridViewContentAlignment.TopLeft
        Rank.Width = 100
        Rank.Visible = True
        '**** End Setup of column: Rank




        '
        'ucListEmployeeRank
        '
        Me.Dock = DockStyle.Fill
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(573, 262)
        Me.Controls.Add(Me.grdEmployeeRank)
        Me.grdEmployeeRank.Columns.Add(RankId)
        Me.grdEmployeeRank.Columns.Add(Rank)

        Me.Name = "ucEmployeeRankList"
        Me.Text = "ucEmployeeRankList"
        Me.ResumeLayout(False)

    End Sub

    Public WithEvents grdEmployeeRank As org.codegen.win.controls.Grid.CGSQLGrid
    Private WithEvents RankId As DataGridViewTextBoxColumn
    Private WithEvents Rank As DataGridViewTextBoxColumn


#End Region

#Region "Standard Code"

    ''' <summary>
    ''' On load of control, we set the event handler for delete
    ''' </summary>
    Private Sub ucListEmployeeRank_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Me.setColumnDataSources()


    End Sub


    'use this method to assign datasources to columns that are combo boxes!
    Protected Sub setColumnDataSources()
        If Me.DesignMode Then Exit Sub


    End Sub


#End Region

End Class

