Namespace BackroundWorkerProgressIndicator

    ''' <summary>
    ''' Class that encapsulates a form with a progress bar and 
    ''' a backround worker
    ''' </summary>
    ''' <remarks></remarks>
    Public Class BackroundWorkerProgressIndicator

        Private Const STR_FORM_PROGRESS As String = "frmProgress"

        Private _frmProgress As frmProgress

        Public Property showCancel As Boolean

        Private ReadOnly Property frmProgress As frmProgress
            Get
                If _frmProgress Is Nothing Then
                    If Application.OpenForms(STR_FORM_PROGRESS) Is Nothing Then
                        _frmProgress = New frmProgress
                    Else
                        _frmProgress = CType(Application.OpenForms(STR_FORM_PROGRESS), controls.BackroundWorkerProgressIndicator.frmProgress)
                    End If
                End If
                Return _frmProgress

            End Get
        End Property

        Public Property progressWindowTitle() As String
            Get
                Return frmProgress.Text
            End Get
            Set(ByVal value As String)

                ' InvokeRequired required compares the thread ID of the 
                ' calling thread to the thread ID of the creating thread. 
                ' If these threads are different, it returns true. 
                If Me.frmProgress.InvokeRequired Then
                    Dim d As New SetTextCallback(AddressOf SetText)
                    Me.frmProgress.Invoke(d, New Object() {value})
                Else
                    Me.frmProgress.Text = value
                End If


            End Set
        End Property

        Private Delegate Sub SetTextCallback(ByVal [text] As String)
        Private Delegate Sub CloseCallback()

        Private Sub SetText(ByVal x As String)
            frmProgress.Text = x
        End Sub

        Private Sub CloseForm()
            frmProgress.Close()
        End Sub


        Public Sub Start(ByVal workMethod As System.ComponentModel.DoWorkEventHandler)

            Me.showCancel = showCancel

            frmProgress.Text = Me.progressWindowTitle
            frmProgress.Refresh()
            frmProgress.btnCancel.Visible = Me.showCancel
            frmProgress.lblMessage.Text = String.Empty

            AddHandler frmProgress.ProgressCancelled, AddressOf progressCancelled
            AddHandler frmProgress.backroundWorkerProgress.DoWork, workMethod
            AddHandler frmProgress.backroundWorkerProgress.RunWorkerCompleted, _
                                AddressOf RunWorkerCompleted

            AddHandler frmProgress.backroundWorkerProgress.ProgressChanged, _
                               AddressOf ProgressChanged

            frmProgress.backroundWorkerProgress.RunWorkerAsync(frmProgress.lblMessage)

            frmProgress.TopMost = True
            frmProgress.Show()
            frmProgress.Activate()
            frmProgress.BringToFront()



        End Sub

        Public Sub [End]()

            If Me.frmProgress.InvokeRequired Then
                Dim d As New CloseCallback(AddressOf CloseForm)
                Me.frmProgress.Invoke(d, New Object() {})
            Else
                Me.frmProgress.Close()
            End If

        End Sub

        Private Sub RunWorkerCompleted(ByVal sender As Object, _
                     ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs)

            Me.frmProgress.Close()

        End Sub

        Private Sub ProgressChanged(ByVal sender As Object, _
                ByVal e As System.ComponentModel.ProgressChangedEventArgs)

            Dim msg As String = String.Empty
            If e.UserState IsNot Nothing Then
                msg = e.UserState.ToString()
            End If

            Call Me.frmProgress.Progress(e.ProgressPercentage, msg)

        End Sub

        Private Sub progressCancelled(ByVal sender As Object, ByVal e As EventArgs)

            Me.frmProgress.backroundWorkerProgress.CancelAsync()
            Me.frmProgress.Hide()

        End Sub

    End Class

End Namespace