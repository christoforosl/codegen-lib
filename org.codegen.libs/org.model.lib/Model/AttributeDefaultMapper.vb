
<AttributeUsage(AttributeTargets.Class, Inherited:=True)> _
Public Class DefaultMapperAttr
    Inherits Attribute

    Public Property defaultMapper As Type

    Sub New(ByVal inClasses As Type)
        Me.defaultMapper = inClasses
    End Sub

End Class

