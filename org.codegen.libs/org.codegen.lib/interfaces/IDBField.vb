Imports Microsoft.VisualBasic

Public Class FieldLookupInfo
    Public DataSource As String
    Public ValueMember As String
    Public DisplayMember As String
End Class

Public Interface IDBField

    ReadOnly Property FieldDataType() As String

    Function isBoolean() As Boolean
    Function isInteger() As Boolean
    Function isString() As Boolean
    Function isDate() As Boolean
    Function isDecimal() As Boolean
    Function isLookup() As Boolean
    Property AccessLevel() As String
    Property OriginalRuntimeType() As System.Type
    Function isAuditField() As Boolean
    Property isPrimaryKey() As Boolean
    Function getConstant() As String
    Function getConstantStr() As String

    Function getClassVariableDeclaration(Optional ByVal accessLevel As String = "private", Optional ByVal withInitialiser As Boolean = True) As String
    Function getFieldDataType() As String
    ReadOnly Property isNullableDataType() As Boolean

    
    ReadOnly Property isNullableProperty() As Boolean

    Function getPropertyDataType() As String
    Function getProperty() As String
    Function getSQLParameter() As String

	Property ParentTable() As IDBTable
	ReadOnly Property PropertyName() As String
    ReadOnly Property RuntimeFieldName() As String
    Property IsTableField() As Boolean
    Property FieldName() As String
    Property RuntimeType() As System.Type
    Property RuntimeTypeStr() As System.String
    Property Size() As Integer
    Property Precision() As Integer
    Property Scale() As Integer
    Property Nullable() As Boolean
    Property XMLSerializationIgnore() As Boolean

    ''' <summary>
    ''' The data type of the field as defined / customized in the xml generator file.
    ''' If no customized data type was defined, then the default data type of the field
    ''' as defined in the databse table structure
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property UserSpecifiedDataType() As String



End Interface
