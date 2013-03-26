Namespace Exceptions

    Public NotInheritable Class EmptyRequiredFieldException
        Inherits ArgumentException

        Public Sub New(ByVal msg As String)
            MyBase.New(msg)
        End Sub

    End Class

End Namespace

