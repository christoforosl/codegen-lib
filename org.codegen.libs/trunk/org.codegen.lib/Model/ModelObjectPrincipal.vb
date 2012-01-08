Imports System.Runtime.InteropServices

Namespace Model

    <Serializable()> _
    Public Class ModelObjectPrincipal
        Inherits System.Security.Principal.GenericIdentity

        Public Sub New(ByVal username As String)
            MyBase.New(username)
        End Sub


        ''' <summary>
        ''' This constructor is here for serialization puproses
        ''' </summary>
        ''' <remarks></remarks>
        Friend Sub New()
            MyBase.New(String.Empty)
        End Sub

#Region "Fields"

        Private Shared ReadOnly SEP As String = Chr(2)
        Private Shared ReadOnly ROLES_SEP As String = ","

#End Region

#Region "Properties"

        Public Property UserId() As Integer
        Public Property Email() As String
           
        Public Property UserPassword() As String
            
        Public Property fullName() As String
                       
        Public Property UserRoles() As String()
            
        Public ReadOnly Property UserName() As String
            Get
                Return Me.Name
            End Get
        End Property
            

        Public Overrides ReadOnly Property isAuthenticated() As Boolean
            Get
                Return Not String.IsNullOrEmpty(Me.UserName)
            End Get

        End Property

#End Region

    End Class

End Namespace