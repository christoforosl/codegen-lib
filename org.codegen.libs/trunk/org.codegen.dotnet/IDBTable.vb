Imports Microsoft.VisualBasic
Imports System.Collections
Imports System.Collections.Generic


Public Interface IDBTable

    Property isReadOnly() As Boolean

    Property LookupInfo() As Dictionary(Of String, FieldLookupInfo)

    Sub addAssociation(ByVal n As IAssociation)
    'Sub addAssociation(ByVal datatype As String, ByVal associationName As String, ByVal cardinality As String, ByVal linkField As String, ByVal dbMapperClass As String, ByVal relType As String, ByVal childLinkField As String, ByVal parentLinkField As String, ByVal acessLevel As String, ByVal isReadOnly As Boolean, ByVal sortField As String, ByVal sortAsc As Boolean)
    Sub addCustomizedField(ByVal fld As IDBField)

    ''' <summary>
    ''' Adds a field in the exluded field collection of the object
    ''' Such fields will not be included in code generation or sql generation
    ''' </summary>
    ''' <param name="fname"></param>
    ''' <remarks></remarks>
    Sub addExludedField(ByVal fname As String)

    Function ChildrenAssociationCount() As Integer
    Function ParentAssociationCount() As Integer
    Function getTableFields() As Dictionary(Of String, IDBField)

    Property SelectObject() As String
    Property TableName() As String

    ReadOnly Property hasFieldName(ByVal fname As String) As Boolean
    ReadOnly Property hasAuditFields() As Boolean
    Property Fields() As Dictionary(Of String, IDBField)

    ''' <summary>
    ''' The set of fields that have been defined and customized in the generator xml 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property CustomizedFields() As Dictionary(Of String, IDBField)
    Property Associations() As List(Of IAssociation)

    Property PrimaryKey() As String
    ReadOnly Property quotedTableName() As String
    ReadOnly Property quotedSelectObject() As String

    Function getPrimaryKeyName() As String
    Function getPrimaryKeyField() As IDBField
    Function getPrimaryKeyDType() As String

    Property ImplementedInterfaces() As List(Of String)
    Sub addImplemetedInterface(ByVal str As String)

    Property ImplementsAsString() As String

    Function getAuditInterface() As String

End Interface
