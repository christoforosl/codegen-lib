Public Interface ICGBaseControl
    Inherits IReadOnlyEnabled

    Property Value() As Object
    Property MinValue() As String
    Property MaxValue() As String

    Property isMandatory() As Boolean

    Property ErrProvider As ErrorProvider
    Property AssociatedLabel As Label
    Property [ReadOnly] As Boolean

    ''' <summary>
    ''' The field that this control is getting/saving data from
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property DataPropertyName As String

    Function Label() As String

    Sub addError(ByVal errMesg As String)

End Interface

Public Interface IReadOnlyEnabled

    Sub setReadOnly()

End Interface