Imports System.Runtime.InteropServices

''' <summary>
''' <exclude />
''' </summary>
''' <remarks></remarks>
Public Interface IAuditable

    Property CreateDate() As DateTime?
    Property UpdateDate() As DateTime?

    Property CreateUser() As String
    Property UpdateUser() As String

End Interface

''' <summary>
''' Same as IAuditable but user Audit Fields are Integers (for user ids)
''' </summary>
''' <remarks></remarks>
Public Interface IAuditable2

    Property CreateDate() As DateTime?
    Property UpdateDate() As DateTime?

    Property CreateUser() As Long?
    Property UpdateUser() As Long?

End Interface