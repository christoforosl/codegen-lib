Imports org.codegen.lib.Tokens

Namespace FileComponents

	<ReplacementTokenAttribute(GetType(ClassAccessLevelToken), _
	   GetType(ObjectEqualsToken), _
	   GetType(ObjectCopyToken), GetType(ObjectMergeToken), _
	   GetType(XOrFieldsToken), _
	   GetType(DBMapperClassNameSpaceToken), _
	   GetType(ClassNameSpaceToken), _
	   GetType(ModelImplementedInterfacesToken), _
	   GetType(RequiredFieldValidatorToken), _
	   GetType(SorterToken), _
	   GetType(IdFieldImplementedInterfaceToken), _
	   GetType(UpdateChildrenFieldCodeToken), _
	   GetType(PKConverter), _
	   GetType(PKFieldRuntimeNameToken), _
	   GetType(ClassPropertiesToken), _
	   GetType(ClassFieldIntConstantsToken), _
	   GetType(ClassFieldStringConstantsToken), _
	   GetType(FieldListToken), _
	   GetType(ClassFieldDeclarationsToken), _
	   GetType(GeneratedInterfaceToken), _
	   GetType(ClassFieldDeclarationsToken), _
	   GetType(GetAttrStrToken), _
	   GetType(SetAttrStrToken), _
	   GetType(GetAttrToken), _
	   GetType(SetAttrToken), _
	   GetType(ObjectCopyToken), _
	   GetType(ModelExtraCodeToken), _
	   GetType(ModelObjectChildArrayToken), _
	   GetType(LoadObjectHierarchyToken), _
	   GetType(ModelObjectParentArrayToken), _
	   GetType(GeneratorToken), _
	   GetType(CurentDateToken), GetType(ModelObjectClassNameToken) _
	   )> _
	Public Class ModelObjectBaseFileComponent
		Inherits ModelObjectFileComponent

		Public Sub New(ByVal inobjGen As IObjectToGenerate)
			MyBase.New(inobjGen)
		End Sub

		Public Overrides Function ClassName() As String

			Return MyBase.ClassName & "Base"

		End Function

		Public Overloads Shared Function KEY() As String
			Return "MOB"
		End Function

	End Class


End Namespace