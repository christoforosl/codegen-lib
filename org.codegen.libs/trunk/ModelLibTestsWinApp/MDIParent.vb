Imports System.Windows.Forms
Imports System.Reflection.Assembly
Imports System.Reflection
Imports System.Threading
Imports System.ComponentModel
Imports ModelLibTestsWinApp.Forms.Edit

Public Class MDIParent

    Private formsMenuMap As New Dictionary(Of ToolStripMenuItem, String)

    Private Sub MDIParent_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        FormsApplicationContext.current.ApplicationDefaultFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, _
                                System.Drawing.GraphicsUnit.Point, CType(0, Byte))

        Me.Text = FormsApplicationContext.current.ApplicationTitle
        Me.setupMenues()

        ModelContext.Current.addGlobalModelValidator(GetType(Employee), GetType(SampleEmployeeGlobalValidator))

        'Dim pro As New BackroundProgressIndicator.BackroundProgressIndicator
        'pro.showCancel = False
        'pro.progressWindowTitle = "this is a test"
        'pro.Start(AddressOf dowork)

    End Sub



    Private Sub ShowListForm(ByVal sender As Object, ByVal e As EventArgs)

        Dim f As Form
        Try
            Call winUtils.HourglassOn()
            Dim listform As String = Me.formsMenuMap.Item(CType(sender, ToolStripMenuItem))
            Dim fo As Object = GetEntryAssembly.CreateInstance(listform, _
                                            False, _
                                            BindingFlags.CreateInstance, _
                                            Nothing, Nothing, Nothing, Nothing)

            f = DirectCast(fo, Form)
            If f.IsMdiChild Then
                f.MdiParent = Me
            End If

            f.Show()

        Catch ex As Exception
            Throw New ApplicationException("Failed to instantiate edit form." & ex.Message)
        Finally
            Call winUtils.HourglassOff()
        End Try



    End Sub
    Private Sub ShowNewForm(ByVal sender As Object, ByVal e As EventArgs)

        Dim f As frmBaseEdit
        Try
            Dim editform As String = Me.formsMenuMap.Item(CType(sender, ToolStripMenuItem))
            Dim fo As Object = GetEntryAssembly.CreateInstance(editform, _
                                            False, _
                                            BindingFlags.CreateInstance, _
                                            Nothing, Nothing, Nothing, Nothing)

            f = DirectCast(fo, frmBaseEdit)

        Catch ex As Exception
            Throw New ApplicationException("Failed to instantiate edit form." & ex.Message)
        End Try

        f.IdValue = 0
        f.MdiParent = Me

        ChildFormCount += 1

        f.Show()

    End Sub


    Private Sub ExitToolsStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ExitToolStripMenuItem.Click
        Me.Close()
    End Sub

    Private Sub CascadeToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles CascadeToolStripMenuItem.Click
        Me.LayoutMdi(MdiLayout.Cascade)
    End Sub

    Private Sub TileVerticalToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles TileVerticalToolStripMenuItem.Click
        Me.LayoutMdi(MdiLayout.TileVertical)
    End Sub

    Private Sub TileHorizontalToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles TileHorizontalToolStripMenuItem.Click
        Me.LayoutMdi(MdiLayout.TileHorizontal)
    End Sub

    Private Sub ArrangeIconsToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ArrangeIconsToolStripMenuItem.Click
        Me.LayoutMdi(MdiLayout.ArrangeIcons)
    End Sub

    Private Sub CloseAllToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles CloseAllToolStripMenuItem.Click
        ' Close all child forms of the parent.
        For Each ChildForm As Form In Me.MdiChildren
            ChildForm.Close()
        Next
    End Sub

    Private ChildFormCount As Integer




    Private Sub setupMenues()

        'Me.setupMenue("EmployeeInfoList", "Employee Info List", ".Forms.List.frmEmployeeInfoList")
        Me.setupMenue("EmployeeList", "Employee List", ".Forms.List.frmEmployeeList")
        Me.setupMenue("ProjectList", "Project List Test, 1 Row", ".Forms.List.frmEditableProjectListSingleRow")
        Me.setupMenue("ProjectList", "Project List Test, All Rows", ".Forms.List.frmEditableProjectList")

        Me.setupMenue("EmployeeRank", "Rank List", ".Forms.List.frmEmployeeRankList")
        Me.setupMenue("readonlytest", "Read Only Test", ".Forms.frmReadOnlyProjectDetails")
        Me.setupMenue("TimeControlTest", "Time Control Test", ".frmTimeControlTest")

        Dim newMenu As ToolStripMenuItem = New ToolStripMenuItem
        newMenu.Name = "EmployeeEditTest"
        newMenu.Text = "frm Employee Details 2"
        Me.ActionsMenu.DropDownItems.Add(newMenu)
        AddHandler newMenu.Click, AddressOf ShowEmployeeEditTest

    End Sub

    Private Sub setupMenue(ByVal menuname As String, ByVal text As String, ByVal editForm As String)

        Dim mnPrdDetails As ToolStripMenuItem = New ToolStripMenuItem
        mnPrdDetails.Name = menuname
        mnPrdDetails.Text = text

        Me.ActionsMenu.DropDownItems.Add(mnPrdDetails)

        Dim asml As String = Assembly.GetEntryAssembly.GetName.Name
        Me.formsMenuMap.Add(mnPrdDetails, asml & editForm)

        AddHandler mnPrdDetails.Click, AddressOf ShowListForm

    End Sub

    Private Sub dowork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs)

        Dim worker As BackgroundWorker = CType(sender, BackgroundWorker)

        For i As Integer = 1 To 10
            Thread.Sleep(100)
            worker.ReportProgress(CInt(i / 10 * 100), "test " & i)
            If worker.CancellationPending Then
                worker.CancelAsync()
                MsgBox("cancelled")
                Exit For
            End If
        Next

    End Sub

    Private Sub ShowEmployeeEditTest(sender As Object, e As EventArgs)
        'Dim f As New frmEmployeeDetails2
        'f.Show()

    End Sub


End Class
