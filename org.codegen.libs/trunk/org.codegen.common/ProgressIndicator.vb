
Public Interface IProgressIndicator
    Event StartProgress(ByVal maxSteps As Integer)
    Event doProgress(ByVal currentStep As Integer)

    Sub nextStep()

    Property CurrentStep() As Integer
    Property MaxSteps() As Integer
    Property Status() As String
End Interface

Public Class ProgressIndicator
    Implements IProgressIndicator


    Private _status As String
    Private _MaxSteps As Integer
    Private _currentStep As Integer = 0

    Public Event StartProgress(ByVal maxSteps As Integer) Implements IProgressIndicator.StartProgress
    Public Event doProgress(ByVal currentStep As Integer) Implements IProgressIndicator.doProgress

    Public Property CurrentStep() As Integer Implements IProgressIndicator.CurrentStep
        Get
            Return _currentStep
        End Get
        Set(ByVal value As Integer)

            RaiseEvent doProgress(value)
            
            _currentStep = value
        End Set
    End Property

    Public Property MaxSteps() As Integer Implements IProgressIndicator.MaxSteps
        Get
            Return _MaxSteps
        End Get
        Set(ByVal value As Integer)
            _MaxSteps = value
        End Set
    End Property

    Public Property Status() As String Implements IProgressIndicator.Status
        Get
            Return _status
        End Get
        Set(ByVal value As String)
            _status = value
        End Set
    End Property

    Public Sub nextStep() Implements IProgressIndicator.nextStep
        Me.CurrentStep = Me.CurrentStep + 1
    End Sub
End Class
