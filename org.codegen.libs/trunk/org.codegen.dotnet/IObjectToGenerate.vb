Imports System.IO
Imports System.Collections.Generic



Public Interface IObjectToGenerate

    Property GenerateMapper As Boolean
    Property GenerateUI As Boolean
    Property XMLDefinition As DataRow
    Property ConstructorCode() As String

    Property DbTable() As IDBTable

    Sub loadFileGroups()
    Sub setTableName(ByVal sTableName As String, ByVal pkFieldName As String)
    Sub generateCode()

    Property FileGroup As Dictionary(Of String, IGeneratedFileComponent)

End Interface
