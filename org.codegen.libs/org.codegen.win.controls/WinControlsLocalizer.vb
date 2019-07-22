
Imports System.IO

Public Class WinControlsLocalizer
    ' Fields
    Public Shared clang As TranslationServices.XMLanguageStrings = New TranslationServices.XMLanguageStrings()

    ' Methods
    Shared Sub New()
        Using strm As Stream = CommonUtils.getResourceStream("org.codegen.win.controls.langStrings2.xml", GetType(WinControlsLocalizer).Assembly)
            WinControlsLocalizer.clang.XMLStream = strm
        End Using

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



End Class
