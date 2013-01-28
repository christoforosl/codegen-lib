Public Class ProgressIndicator

    Private _frmProgress As frmProgress
    Private _fmsg As String
    Private _TotalSteps As Integer

    Public showCancel As Boolean
    Public isCancelled As Boolean

    Public Property progressMessage() As String
        Get
            Return _fmsg
        End Get
        Set(ByVal value As String)
            _fmsg = Value
        End Set
    End Property

    Public Sub prgProgress(ByVal currentStep As Integer)

        If _frmProgress Is Nothing Then Exit Sub
        
        If _frmProgress.IsInitialized = False Then
            Call _frmProgress.initProgressBar(Me._TotalSteps)
        End If

        Call _frmProgress.Progress(currentStep)
        Me.isCancelled = _frmProgress.isCancelled

    End Sub

    Public Sub prgStart(ByVal message As String, _
                        ByVal totalSteps As Integer, _
                        ByVal showCancel As Boolean,
                        ByVal workMethod As System.ComponentModel.DoWorkEventHandler)

        Me.isCancelled = False
        Me.progressMessage = message
        Me.showCancel = showCancel
        Me._TotalSteps = totalSteps

        If Application.OpenForms("frmProgress") Is Nothing Then
            _frmProgress = New frmProgress
            _frmProgress.Show()

        End If
        _frmProgress.Refresh()
        _frmProgress = CType(Application.OpenForms("frmProgress"), controls.frmProgress)
        _frmProgress.btnCancel.Visible = Me.showCancel
        _frmProgress.lblMessage.Text = Me.progressMessage
        AddHandler _frmProgress.backroundWorkerProgress.DoWork, workMethod
        _frmProgress.backroundWorkerProgress.RunWorkerAsync()



    End Sub

    Public Sub prgEnd()

        _frmProgress.Hide()
        _frmProgress.Close()

    End Sub


End Class
