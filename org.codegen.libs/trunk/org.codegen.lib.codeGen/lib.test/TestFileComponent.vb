Imports org.codegen.lib.codeGen.FileComponents
Imports org.codegen.lib.codeGen.Tokens
Imports System.IO

<ReplacementTokenAttribute(GetType(CurentDateToken), GetType(PKFieldRuntimeNameToken), _
            GetType(GeneratorToken), GetType(TableNameToken), _
            GetType(TestAssertAssociationsToken), _
            GetType(ModifyRandomFields), GetType(AssertRandomFields), _
            GetType(TestAssertEqualToken), _
            GetType(ClassAccessLevelToken), _
            GetType(PKFieldTableNameToken), _
            GetType(ClassNameSpaceToken), _
            GetType(DBMapperClassNameSpaceToken), _
            GetType(ModelObjectClassNameToken), _
            GetType(ClassAccessLevelToken))> _
Public Class TestFileComponent
    Inherits ModelObjectFileComponent

    Private Const STR_TEST As String = "Test"

    Public Sub New(ByVal inobjGen As IObjectToGenerate)
        MyBase.New(inobjGen)

    End Sub

    Public Overloads Shared Function KEY() As String
        Return STR_TEST
    End Function

    Public Overrides Function ClassName() As String
        Return MyBase.ClassName() & STR_TEST & "Base"
    End Function

    Public Overrides Function ClassNamespace() As String
        Return XMLClassGenerator.getRowValue(Me.objectToGenerate.XMLDefinition, _
                           XMLClassGenerator.XML_TABLE_ATTR_TEST_CLASS_NAMESPACE, _
                                                         String.Empty)
    End Function

    Public Overrides Function generatedFilenameWithPath() As String

        Dim tmp As String = ModelGenerator.Current().ProjectOutputDirTest & _
                               Me.ClassNamespace().Replace("."c, "\"c) & "\"

        If tmp.EndsWith("\") = False Then tmp &= "\"

        Dim f As New FileInfo(tmp)
        'just to make sure that this is a valid format path

        Return tmp & Me.generatedFilename

    End Function

End Class
