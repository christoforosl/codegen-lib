Public Class ProgressIndicator

    Private frmProgress As frmProgress

    Private _progressMessage As String
    Private TotalSteps As Integer

    Public Property showCancel As Boolean
    Public Property isCancelled As Boolean

    ''' <summary>
    ''' The message show to the user while the operation is running
    ''' </summary>
    Public Property progressMessage() As String
        Get
            Return _progressMessage
        End Get
        Set(ByVal value As String)
            _progressMessage = value
        End Set
    End Property

    Public Sub Start(ByVal message As String, _
                        ByVal totalSteps As Integer, _
                        ByVal showCancel As Boolean,
                        ByVal workMethod As System.ComponentModel.DoWorkEventHandler)

        Me.isCancelled = False
        Me.progressMessage = message
        Me.showCancel = showCancel
        Me.TotalSteps = totalSteps

        If Application.OpenForms("frmProgress") Is Nothing Then
            frmProgress = New frmProgress
        Else
            frmProgress = CType(Application.OpenForms("frmProgress"), controls.frmProgress)
        End If

        frmProgress.Refresh()
        frmProgress.btnCancel.Visible = Me.showCancel
        frmProgress.lblMessage.Text = Me.progressMessage
        AddHandler frmProgress.backroundWorkerProgress.DoWork, workMethod
        AddHandler frmProgress.backroundWorkerProgress.RunWorkerCompleted, AddressOf RunWorkerCompleted

        frmProgress.backroundWorkerProgress.RunWorkerAsync(frmProgress.lblMessage)

#If Not Debug Then
        frmProgress.topmost = true
#End If

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
