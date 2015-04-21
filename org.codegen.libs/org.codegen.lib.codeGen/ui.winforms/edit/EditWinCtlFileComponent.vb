Imports org.codegen.lib.codeGen.org.codegen.lib.codeGen.Tokens
Imports org.codegen.lib.codeGen.FileComponents
Imports System.IO
Imports org.codegen.lib.codeGen.Tokens

<ReplacementTokenAttribute(GetType(ControlsLayout), _
        GetType(ModelObjectClassNameToken), GetType(ConcatValues), _
        GetType(GetControlInstantiations), _
        GetType(ControlsAdd), _
        GetType(ControlsLoadCode), _
        GetType(LoadToObject), _
        GetType(LoadToControls), _
        GetType(GetControlDeclarations))> _
Public Class EditWinCtlFileComponent
    Inherits ModelObjectFileComponent

    Public Sub New(ByRef inObjGen As IObjectToGenerate)
        MyBase.New(inObjGen)
        Me.templateFileName = "org.codegen.lib.codeGen.EditControlTemplate.txt"
    End Sub

    Public Overrides Function ClassName() As String
        Return "uc" & MyBase.ClassName & "Details"
    End Function

    Overloads Shared Function KEY() As String
        Return "UC"
    End Function

    Public Overrides Function generatedFilenameWithPath() As String

        Dim tmp As String = ModelGenerator.Current().ProjectOutputDirUI & _
                                "Controls\Edit\"

        If tmp.EndsWith("\") = False Then tmp &= "\"

        Dim f As New FileInfo(tmp)
        'just to make sure that this is a valid format path

        Return tmp & Me.generatedFilename

    End Function
End Class
