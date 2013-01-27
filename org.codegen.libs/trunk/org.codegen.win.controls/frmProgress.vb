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

        Me._isCancelled = True

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

End Class