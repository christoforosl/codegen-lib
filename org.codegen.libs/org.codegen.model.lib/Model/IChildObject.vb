Imports System.Runtime.InteropServices

Namespace Model

    ''' <summary>
    ''' Interface used as a type for child objects of ModelObjects.
    ''' Child objects of ModelObjects can be other ModelObjects or a ModelObjectList
    ''' so both ModelObject and ModelObjectList implement this interface.
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface IChildObject

        Property isDirty() As Boolean

    End Interface

End Namespace