Imports System.Security.Principal
Imports System.Threading

Public Interface IAuditor

    Sub setAuditFields(mo As IModelObject)

End Interface


''' <summary>
''' This IAuditor implementation stores string usernames in table
''' </summary>
Public Class Auditor
    Implements IAuditor

    Public Sub setAuditFields(mo As IModelObject) Implements IAuditor.setAuditFields
        SyncLock mo


            If TypeOf mo Is IAuditable = False Then
                Return
            End If

            If mo.isDirty Then
                Dim userName As String = getPrincipal().Identity.Name
                Dim iAudit As IAuditable = DirectCast(mo, IAuditable)
                If mo.isNew Then
                    iAudit.CreateDate = Date.Now
                    iAudit.CreateUser = userName
                End If

                iAudit.UpdateDate = Date.Now
                iAudit.UpdateUser = userName

            End If

        End SyncLock
    End Sub

    Public Function getPrincipal() As IPrincipal

        Dim iPrincipal As IPrincipal = Nothing
        If ModelContext.Current Is Nothing Then
            iPrincipal = Thread.CurrentPrincipal
        Else
            iPrincipal = ModelContext.Current.CurrentUser
        End If

        If iPrincipal Is Nothing Then
            Throw New ApplicationException("Thread.CurrentPrincipal and ModelContext.getCurrentUser not set!")
        End If

        If iPrincipal.Identity Is Nothing Then
            Throw New ApplicationException("iPrincipal.Identity")
        End If
        If String.IsNullOrEmpty(iPrincipal.Identity.Name) Then
            Throw New ApplicationException("iPrincipal.Identity.Name is blank/empty string")
        End If
        Return iPrincipal
    End Function

End Class

''' <summary>
''' This IAuditor implementation stores numeric ids in username field of table
''' </summary>
Public Class Auditor2
    Inherits Auditor
    Implements IAuditor

    Public Overloads Sub setAuditFields(mo As IModelObject)
        SyncLock mo

            If TypeOf mo Is IAuditable2 = False Then
                Return
            End If

            If mo.isDirty Then

                
                Dim userid As Integer = CInt(getPrincipal().Identity.Name)
                Dim iAudit As IAuditable2 = DirectCast(mo, IAuditable2)
                If mo.isNew Then
                    iAudit.CreateDate = Date.Now
                    iAudit.CreateUser = userid
                End If

                iAudit.UpdateDate = Date.Now
                iAudit.UpdateUser = userid

            End If

        End SyncLock
    End Sub
End Class