Namespace ProgressIndicator

    Public Interface IProgressable

        Sub startProcess()

        ''' <summary>
        ''' Shows if the process has been cancelled by the user
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Property wasCancelled As Boolean

        ''' <summary>
        ''' raised when process to be tracked by progress indicator has been started
        ''' </summary>
        ''' <param name="sender">Object of type IProgressable</param>
        ''' <param name="e">ProgressableEventArgs</param>
        ''' <remarks></remarks>
        Event processStart(ByVal sender As Object, ByVal e As ProgressableEventArgs)

        ''' <summary>
        ''' raised when process to be tracked by progress indicator is "in flight"
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Event processGoing(ByVal sender As Object, ByVal e As ProgressableEventArgs)

        ''' <summary>
        ''' Raised when process has finished
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Event processFinished(ByVal sender As Object, ByVal e As ProgressableEventArgs)

    End Interface

End Namespace
