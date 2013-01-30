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
        Me.ControlBox = b

    End Sub

    Private Sub frmProgress_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.btnCancel.Text = WinControlsLocalizer.getString("cmdCancel")
        Call winUtils.HourglassOn()

    End Sub


    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
                Handles btnCancel.Click

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

    Sub Progress(ByVal currentStep As Integer, ByVal msg As String)

        If currentStep > Me.ProgressBar.Maximum Then currentStep = Me.ProgressBar.Maximum
        If currentStep < Me.ProgressBar.Minimum Then currentStep = Me.ProgressBar.Minimum

        Me.ProgressBar.Value = currentStep
        Me.lblPercentage.Text = currentStep & "%"
        If String.IsNullOrEmpty(msg) = False Then
            Me.lblMessage.Text = msg
        End If
    End Sub

    Sub Progress(ByVal currentStep As Integer)

        Call Progress(currentStep, String.Empty)

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