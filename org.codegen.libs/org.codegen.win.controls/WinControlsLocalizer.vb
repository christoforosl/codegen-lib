
Public Class WinControlsLocalizer

    ' Methods
    Shared Sub New()
        WinControlsLocalizer.clang.ResourceName = "org.codegen.win.controls.langStrings2.xml"
    End Sub

    Public Shared Function getString(ByVal skey As String) As String
        Return WinControlsLocalizer.clang.getString(skey, "", "")
    End Function

    Public Shared Function getString(ByVal skey As String, ByVal ParamArray replacements As String()) As String

        Dim str As String = getString(skey)
        If replacements Is Nothing OrElse replacements.Length = 0 Then
            'just return the stmmt
        Else
            str = String.Format(str, replacements)
        End If
        Return str

    End Function

    ' Fields
    Public Shared clang As TranslationServices.XMLanguageStrings = New TranslationServices.XMLanguageStrings()

End Class
