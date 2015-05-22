
<AttributeUsage(AttributeTargets.Class, Inherited:=True)> _
Public Class DefaultMapper
    Inherits Attribute

    Public Property MapperClass As Type

    Sub New(inclass As Type)

        Me.MapperClass = inClass
    End Sub

End Class