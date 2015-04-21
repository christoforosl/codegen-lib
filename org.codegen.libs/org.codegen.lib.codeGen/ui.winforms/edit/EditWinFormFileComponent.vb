Imports org.codegen.lib.codeGen.org.codegen.lib.codeGen.Tokens
Imports org.codegen.lib.codeGen.FileComponents
Imports System.IO
Imports org.codegen.lib.codeGen.Tokens

<ReplacementTokenAttribute(GetType(WinEditFormHeight), _
        GetType(ModelObjectClassNameToken), _
        GetType(GetControlDeclarations))> _
Public Class EditWinFormFileComponent
    Inherits ModelObjectFileComponent

    Public Sub New(ByRef inObjGen As IObjectToGenerate)
        MyBase.New(inObjGen)
        Me.templateFileName = "org.codegen.lib.codeGen.EditFormTemplate.txt"
    End Sub

    Public Overrides Function ClassName() As String
        Return "frm" & MyBase.ClassName & "Details"
    End Function

    Overloads Shared Function KEY() As String
        Return "EDIT_FORM"
    End Function

    Public Overrides Function generatedFilenameWithPath() As String

        Dim tmp As String = ModelGenerator.Current().ProjectOutputDirUI & _
                                "Forms\Edit\"

        If tmp.EndsWith("\") = False Then tmp &= "\"

        Dim f As New FileInfo(tmp)
        'just to make sure that this is a valid format path

        Return tmp & Me.generatedFilename

    End Function
End Class
