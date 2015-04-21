Public Interface IAuditor

    Sub setAuditFields(mo As IModelObject)

End Interface

Public Class Auditor
    Implements IAuditor

    Public Sub setAuditFields(mo As IModelObject) Implements IAuditor.setAuditFields
        SyncLock mo


            If TypeOf mo Is IAuditable = False Then
                Return
            End If

            If mo.isDirty Then

                If ModelContext.Current.CurrentUser Is Nothing OrElse _
                        String.IsNullOrEmpty(ModelContext.Current.CurrentUserName) Then
                    Throw New ApplicationException("ModelContext.getCurrentUser not set!")
                End If

                Dim userName As String = ModelContext.Current.CurrentUser.Identity.Name
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
End Class

Public Class Auditor2
    Implements IAuditor

    Public Sub setAuditFields(mo As IModelObject) Implements IAuditor.setAuditFields
        SyncLock mo

            If TypeOf mo Is IAuditable2 = False Then
                Return
            End If

            If mo.isDirty Then

                If ModelContext.Current.CurrentUser Is Nothing OrElse _
                        ModelContext.Current.CurrentUser.Identity.Name = String.Empty Then
                    Throw New ApplicationException("ModelContext.getCurrentUser not set, or UserId not set!")
                End If

                Dim userid As Integer = CInt(ModelContext.Current.CurrentUser.Identity.Name)
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