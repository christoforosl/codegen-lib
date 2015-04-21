Imports System.Collections.Generic
Imports org.codegen.lib.codeGen.Tokens


Namespace org.codegen.lib.codeGen.Tokens
    Public Class WinEditFormHeight
        Inherits ReplacementToken

        Sub New()
            Me.StringToReplace = "EDIT_FORM_HEIGHT"
        End Sub

        Public Overrides Function getReplacementCode(ByVal t As dotnet.IObjectToGenerate) As String
            Return CStr((t.DbTable.Fields().Count * 35) + 10)
        End Function
    End Class
End Namespace
