
Namespace ProgressIndicator

    Public Class ProgressableEventArgs
        Inherits System.EventArgs

        Sub New(ByVal current As Long, ByVal total As Long)
            If (total = 0) Then
                Throw New ArgumentException("total cannot be 0")
            End If
            Me.currentLength = current
            Me.totalLength = total
        End Sub

        Sub New(ByVal total As Long)
            If (total = 0) Then
                Throw New ArgumentException("total cannot be 0")
            End If
            Me.currentLength = 0
            Me.totalLength = total

        End Sub

        Public Property wasCancelled As Boolean
        Public Property currentLength As Long
        Public Property totalLength As Long

    End Class

End Namespace
