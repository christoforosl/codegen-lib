Public Class SampleEmployeeGlobalValidator
    Implements IModelObjectValidator

    Public Sub validate(ByVal mo As org.codegen.model.lib.Model.IModelObject) Implements org.codegen.model.lib.IModelObjectValidator.validate
        If mo.isNew Then
            Debug.Print("new object")

        End If
    End Sub
End Class
