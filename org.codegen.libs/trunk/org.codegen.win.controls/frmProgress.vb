Public Class frmProgress

    Private _isCancelled As Boolean
    Private _isInitialized As Boolean

    Public ReadOnly Property isCancelled As Boolean
        Get
            Return _isCancelled
        End Get
    End Property

    Public Property IsInitialized() As Boolean
        Get
            Return _isInitialized
        End Get
        Set(ByVal value As Boolean)
            _isInitialized = value
        End Set
    End Property

    Public Sub canCancel(ByVal b As Boolean)

        Me.btnCancel.Visible = Not b

    End Sub

    Private Sub frmProgress_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.btnCancel.Text = WinControlsLocalizer.getString("cmdCancel")
    End Sub


    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click

        If winUtils.MsgboxQuestion("Are you sure you want to cancel?") = vbYes Then
            Me._isCancelled = True
            Me.backroundWorkerProgress.CancelAsync()
        End If

    End Sub

    Sub initProgressBar(ByVal maxSteps As Integer)

        Me.ProgressBar.Minimum = 0
        Me.ProgressBar.Maximum = maxSteps
        Me._isCancelled = False
        Me._isInitialized = True

    End Sub

    Sub Progress(ByVal currentStep As Integer)

        If currentStep > Me.ProgressBar.Maximum Then currentStep = Me.ProgressBar.Maximum
        If currentStep < Me.ProgressBar.Minimum Then currentStep = Me.ProgressBar.Minimum

        Me.ProgressBar.Value = currentStep

    End Sub

    Private Sub backroundWorkerProgress_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles backroundWorkerProgress.ProgressChanged
        Me.ProgressBar.Value = e.ProgressPercentage
    End Sub

    Private Sub backroundWorkerProgress_RunWorkerCompleted(ByVal sender As Object, _
                                ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) _
                                    Handles backroundWorkerProgress.RunWorkerCompleted
        Me.Close()

    End Sub

    
   
End Class