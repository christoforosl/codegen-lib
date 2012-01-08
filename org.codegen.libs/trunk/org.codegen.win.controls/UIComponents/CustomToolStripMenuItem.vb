Public Class CustomToolStripMenuItem

    Private _onAction As String
    Public Property onAction() As String
        Get
            Return _onAction
        End Get
        Set(ByVal value As String)
            _onAction = value
        End Set
    End Property


End Class
