Namespace ProgressIndicator

    Public Interface IProgressable

        Property wasCancelled As Boolean

        Event processStart(ByVal sender As Object, ByVal e As ProgressableEventArgs)
        Event processGoing(ByVal sender As Object, ByVal e As ProgressableEventArgs)
        Event processFinished(ByVal sender As Object, ByVal e As ProgressableEventArgs)

    End Interface

End Namespace
