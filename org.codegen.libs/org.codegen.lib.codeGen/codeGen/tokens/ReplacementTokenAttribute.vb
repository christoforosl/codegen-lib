
<AttributeUsage(AttributeTargets.Class, Inherited:=True)> _
Public Class ReplacementTokenAttribute
    Inherits Attribute

    Public Property replacementTokens() As Type() 'Tokens.IReplacementToken()

    Sub New(ByVal ParamArray inClasses() As Type)
        Me.replacementTokens = inClasses
    End Sub

End Class

