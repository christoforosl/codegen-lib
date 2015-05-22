Imports Microsoft.VisualBasic


Public Interface IAssociation

    Property ParentDBTable() As IDBTable

    ''' <summary>
    ''' The association datatype, as defined in the XML definition
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property DataType As String
    Property ParentDatatype As String
    Property ChildDatatype As String
    Property associationName() As String
    Property associationNameSingular As String
    Property ChildFieldIsPK() As Boolean


    ''' <summary>
    ''' declartation of this in the properties of the interface
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function getInterfaceDeclaration() As String
    Function getDataTypeVariable() As String
    Function getTestCode() As String
    Function isCardinalityMany() As Boolean
    Property PropertiesImplementInterface() As String
    Property IsReadOnly() As Boolean
    Property ChildFieldName() As String
    Property ParentFieldName() As String
    Property RelationType() As String
    Property isSortAsc() As Boolean
    Property SortField() As String

    Sub setCardinality(ByVal val As String)

    Function getVariable() As String
    Function getDeletedVariable() As String
    Function getSaveParentCode(ByVal parentMoObjType As String) As String
    Function getSaveChildrenCode() As String
    Function isParent() As Boolean
    Function getCanonicalName() As String
    Function getSet() As String
    Function getGet() As String
    Function getSetterGetter() As String

    ReadOnly Property templateText() As String
End Interface
