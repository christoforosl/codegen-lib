Public Class frmProgress

    Public Property IsInitialized() As Boolean

    Private Sub frmProgress_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Me.btnCancel.Text = WinControlsLocalizer.getString("cmdCancel")
        Call winUtils.HourglassOn()

    End Sub


    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
                Handles btnCancel.Click

        Dim restoreTopMost As Boolean

        Try
            restoreTopMost = Me.TopMost
            If Me.TopMost Then
                Me.TopMost = False
            End If
            If winUtils.MsgboxQuestion("Are you sure you want to cancel?") = vbYes Then

                Me.backroundWorkerProgress.CancelAsync()
                Me.Hide()
            End If

        Finally
            Me.TopMost = restoreTopMost
        End Try
        
    End Sub


    Sub Progress(ByVal currentStep As Integer, ByVal msg As String)

        If currentStep > Me.ProgressBar.Maximum Then currentStep = Me.ProgressBar.Maximum
        If currentStep < Me.ProgressBar.Minimum Then currentStep = Me.ProgressBar.Minimum

        Me.ProgressBar.Value = currentStep
        Me.lblPercentage.Text = currentStep & "%"

        If String.IsNullOrEmpty(msg) = False Then
            Me.lblMessage.Text = msg
        End If

    End Sub

   
    Private Sub backroundWorkerProgress_ProgressChanged(ByVal sender As Object, _
                                                        ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles backroundWorkerProgress.ProgressChanged
        Dim msg As String = String.Empty
        If e.UserState IsNot Nothing Then
            msg = e.UserState.ToString()
        End If

        Call Progress(e.ProgressPercentage, msg)

    End Sub


End Class