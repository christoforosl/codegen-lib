
Namespace ProgressIndicator
    Public Class ErrorProgressableEventArgs
        Inherits ProgressableEventArgs

        Sub New(ByVal ex As Exception)
            Me.progressMessage = String.Format("Error Occyred: {0}",
                                               ex.Message & vbCrLf & ex.StackTrace)
            Me.inError = True

        End Sub
        Sub New(ByVal current As Long, ByVal totalSteps As Long, ByVal ex As Exception)

            Me.currentStep = CInt(current * 100 / totalSteps) 'express it as a percentage
            Me.totalSteps = totalSteps
            Me.progressMessage = String.Format("Error at step: {0} of {1}: {2}",
                                               current, totalSteps, ex.Message & vbCrLf & ex.StackTrace)
            Me.inError = True

        End Sub

    End Class
    Public Class ProgressableEventArgs
        Inherits System.EventArgs

        ''' <summary>
        ''' Constructor to be used during running of process, 
        ''' to indicate progress and user message
        ''' </summary>
        ''' <param name="current">Current Step</param>
        ''' <param name="message">Message to be shown to the user</param>
        ''' <remarks></remarks>
        Sub New(ByVal current As Long, ByVal totalSteps As Long, ByVal message As String)

            Me.currentStep = CInt(current * 100 / totalSteps) 'express it as a percentage
            Me.totalSteps = totalSteps
            Me.progressMessage = message & String.Format("- {0} of {1}", current, totalSteps)

        End Sub

        Sub New( ByVal message As String)
        
        End Sub

        ''' <summary>
        ''' Constructor to be used during running of process, 
        ''' to indicate progress and user message
        ''' </summary>
        ''' <param name="current">Current Step</param>
        ''' <param name="totalSteps">Total Steps</param>
        ''' <remarks></remarks>
        Sub New(ByVal current As Long, ByVal totalSteps As Long)

            Me.currentStep = current
            Me.totalSteps = totalSteps

        End Sub

        ''' <summary>
        ''' Emptry constructor
        ''' </summary>
        ''' <remarks></remarks>
        Sub New()

        End Sub

        ''' <summary>
        ''' Constructor to be called at start of process, to set total steps
        ''' </summary>
        ''' <param name="totalSteps">Total steps of the progress</param>
        ''' <remarks></remarks>
        Sub New(ByVal totalSteps As Long)

            If (totalSteps = 0) Then
                Throw New ArgumentException("totalSteps cannot be 0")
            End If
            Me.currentStep = 0
            Me.totalSteps = totalSteps

        End Sub


        ''' <summary>
        ''' Boolean indicating that whether the progress operation has been cancelled by the user
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property wasCancelled As Boolean

        ''' <summary>
        ''' The current step number of the progress
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property currentStep As Long

        ''' <summary>
        ''' The total steps of the progress
        ''' </summary>
        Public Property totalSteps As Long

        ''' <summary>
        ''' The progress message to show to the user
        ''' </summary>
        Public Property progressMessage As String

        Public Property inError As Boolean

    End Class

End Namespace
