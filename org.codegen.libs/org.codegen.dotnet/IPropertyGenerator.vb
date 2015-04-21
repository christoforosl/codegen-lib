Public Interface IPropertyGenerator
    Function generateCode(ByVal field As IDBField) As String
    Function generateInterfaceDeclaration(ByVal field As IDBField) As String
End Interface

