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

        End If
        frmProgress.Refresh()
        frmProgress.btnCancel.Visible = Me.showCancel
        frmProgress.lblMessage.Text = Me.progressMessage
        AddHandler frmProgress.backroundWorkerProgress.DoWork, workMethod
        frmProgress.backroundWorkerProgress.RunWorkerAsync(frmProgress.lblMessage)
        frmProgress.Show()


    End Sub

    Public Sub prgEnd()

        frmProgress.Hide()
        frmProgress.Close()

    End Sub


End Class
