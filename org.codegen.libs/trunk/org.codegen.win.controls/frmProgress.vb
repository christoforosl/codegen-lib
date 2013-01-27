Public Class frmProgress

    Public Sub canCancel(ByVal b As Boolean)

        Me.btnCancel.Visible = Not b
        Me.bwProgress.WorkerSupportsCancellation = Not b

    End Sub

    Private Sub frmProgress_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.btnCancel.Text = WinControlsLocalizer.getString("cmdCancel")
    End Sub

    Private Sub bwProgress_DoWork(ByVal sender As System.Object, _
                                  ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwProgress.DoWork

        Me.btnCancel.Enabled = True

    End Sub

    Private Sub bwProgress_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles bwProgress.ProgressChanged
        Me.ProgressBar.Value = e.ProgressPercentage
    End Sub

    Private Sub bwProgress_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwProgress.RunWorkerCompleted
        ' First, handle the case where an exception was thrown. 
        If (e.Error IsNot Nothing) Then
            Call winUtils.MsgboxInfo(e.Error.Message)

        ElseIf e.Cancelled Then
            ' Next, handle the case where the user canceled the  
            ' operation. 
            ' Note that due to a race condition in  
            ' the DoWork event handler, the Cancelled 
            ' flag may not have been set, even though 
            ' CancelAsync was called.
            Me.lblMessage.Text = "Canceled"
        Else
            ' Finally, handle the case where the operation succeeded.

        End If

        Me.btnCancel.Enabled = False

    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click

        If Me.bwProgress.IsBusy Then
            Me.bwProgress.CancelAsync()
        End If

    End Sub
End Class