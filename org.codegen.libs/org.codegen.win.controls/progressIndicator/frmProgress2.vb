Namespace BackroundProgressIndicator

    Public Class frmProgress2

        Public Event ProgressCancelled(ByVal sender As Object, ByVal e As EventArgs)

        Public Sub setShowCancelButton(ByVal showit As Boolean)

            Me.btnCancel.Visible = showit

        End Sub

        Public Sub setMessageLabelText(ByVal text As String)

            Me.lblMessage.Text = text

        End Sub

        Private Sub frmProgress_Load(ByVal sender As Object, _
                                     ByVal e As System.EventArgs) Handles Me.Load

            Me.btnCancel.Text = WinControlsLocalizer.getString("cmdCancel")

        End Sub


        Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
                    Handles btnCancel.Click

            Dim restoreTopMost As Boolean

            Try
                restoreTopMost = Me.TopMost
                If Me.TopMost Then
                    Me.TopMost = False
                End If
                If winUtils.MsgboxQuestion(WinControlsLocalizer.getString("cancel_progress")) = vbYes Then

                    RaiseEvent ProgressCancelled(Me, New EventArgs())

                End If

            Finally
                Me.TopMost = restoreTopMost
            End Try

        End Sub

        Public Sub resetProgress(ByVal msg As String)

            Me.ProgressBar.Value = 0
            Me.lblPercentage.Text = "0 %"
            If String.IsNullOrEmpty(msg) = False Then
                Me.lblMessage.Text = msg
            End If
            Me.Refresh()

        End Sub

        Public Sub ReportProgress(ByVal currentStep As Integer, ByVal msg As String)

            If currentStep > Me.ProgressBar.Maximum Then currentStep = Me.ProgressBar.Maximum
            If currentStep < Me.ProgressBar.Minimum Then currentStep = Me.ProgressBar.Minimum

            Me.ProgressBar.Value = currentStep
            Me.lblPercentage.Text = currentStep & " %"

            If String.IsNullOrEmpty(msg) = False Then
                Me.lblMessage.Text = msg
            End If
            Me.Refresh()

        End Sub

    End Class

End Namespace