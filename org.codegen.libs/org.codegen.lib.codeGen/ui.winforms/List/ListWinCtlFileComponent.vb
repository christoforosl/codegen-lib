Imports org.codegen.lib.codeGen.org.codegen.lib.codeGen.Tokens
Imports org.codegen.lib.codeGen.FileComponents
Imports System.IO
Imports org.codegen.lib.codeGen.Tokens

<ReplacementTokenAttribute(GetType(TableNameToken), _
    GetType(ModelObjectClassNameToken), _
    GetType(PKFieldTableNameToken), _
    GetType(GetControlDeclarations), GetType(WinformsGridColumnsInstantiate), _
    GetType(WinformsGridColumnsAdd), GetType(WinformsGridColumnsDeclare), _
     GetType(WinformsGridColumns), _
    GetType(WinformsGridColumnsDataSources))> _
Public Class ListWinCtlFileComponent
    Inherits ModelObjectFileComponent

    Public Sub New(ByRef inObjGen As IObjectToGenerate)
        MyBase.New(inObjGen)
        Me.templateFileName = "org.codegen.lib.codeGen.ListControlTemplate.txt"
    End Sub

    Public Overrides Function ClassName() As String
        Return "uc" & MyBase.ClassName & "List"
    End Function

    Overloads Shared Function KEY() As String
        Return "ucList"
    End Function

    Public Overrides Function generatedFilenameWithPath() As String

        Dim tmp As String = ModelGenerator.Current().ProjectOutputDirUI & _
                               "Controls\List\"

        Dim f As New FileInfo(tmp)

        Return tmp & Me.generatedFilename

    End Function
End Class
