Public Interface ICGBaseControl
    Inherits IReadOnlyEnabled

    Property Value() As Object
    Property MinValue() As Object
    Property MaxValue() As Object

    Property isMandatory() As Boolean
    Property ErrProvider As ErrorProvider
    Property AssociatedLabel As Label
    Property [ReadOnly] As Boolean

    Function Label() As String

    Sub addError(ByVal errMesg As String)

End Interface

Public Interface IReadOnlyEnabled

    Sub setReadOnly()

End Interface