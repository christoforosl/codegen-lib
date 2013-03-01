Public Class ProgressIndicator

    Private frmProgress As frmProgress

    Private TotalSteps As Integer

    Public Property showCancel As Boolean
    Public Property isCancelled As Boolean

    Public Property progressWindowTitle() As String

    Public Sub Start(ByVal workMethod As System.ComponentModel.DoWorkEventHandler)

        Me.isCancelled = False

        Me.showCancel = showCancel
        Me.TotalSteps = TotalSteps

        If Application.OpenForms("frmProgress") Is Nothing Then
            frmProgress = New frmProgress
        Else
            frmProgress = CType(Application.OpenForms("frmProgress"), controls.frmProgress)
        End If
        frmProgress.Text = Me.progressWindowTitle
        frmProgress.Refresh()
        frmProgress.btnCancel.Visible = Me.showCancel
        frmProgress.lblMessage.Text = String.Empty
        AddHandler frmProgress.backroundWorkerProgress.DoWork, workMethod
        AddHandler frmProgress.backroundWorkerProgress.RunWorkerCompleted, AddressOf RunWorkerCompleted

        frmProgress.backroundWorkerProgress.RunWorkerAsync(frmProgress.lblMessage)

        frmProgress.TopMost = True
        frmProgress.Show()
        frmProgress.Activate()
        frmProgress.BringToFront()

    End Sub

    Public Sub prgEnd()

        frmProgress.Close()

    End Sub

    Private Sub RunWorkerCompleted(ByVal sender As Object, _
                                   ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs)

        Call prgEnd()

    End Sub


End Class
