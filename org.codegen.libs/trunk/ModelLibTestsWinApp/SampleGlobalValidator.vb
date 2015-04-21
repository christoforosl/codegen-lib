Public Class SampleEmployeeGlobalValidator
	Implements IModelObjectValidator

	Public Sub validate(ByVal mo As org.model.lib.Model.IModelObject) Implements org.model.lib.IModelObjectValidator.validate
		If mo.isNew Then
			Debug.Print("new object")

		End If
	End Sub
End Class
