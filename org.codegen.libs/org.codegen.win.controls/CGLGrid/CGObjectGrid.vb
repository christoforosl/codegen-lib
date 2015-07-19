
Namespace Grid

    Public Class CGObjectGrid
        Inherits CGBaseGrid

        Protected Overrides Sub bindToData()

            Me.BindingSource = New BindingSource
            Me.BindingSource.DataSource = Me.DataSource

        End Sub

    End Class

End Namespace