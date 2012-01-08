Imports System.Runtime.InteropServices

Namespace Model

    ''' <summary>
    ''' Generates ids for newly created Model objects.
    ''' Every model object must have a unique identifier.
    ''' </summary>
    ''' <remarks></remarks>
    Public Class ModelObjectKeyGen

        Private Shared _nextId As Integer = 0

        Public Shared Function nextId() As Integer

            _nextId -= 1
            Return _nextId

        End Function

    End Class

End Namespace