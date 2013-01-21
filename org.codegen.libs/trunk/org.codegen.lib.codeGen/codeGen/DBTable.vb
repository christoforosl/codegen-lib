Imports Microsoft.VisualBasic
Imports System.Collections

Imports System.Collections.Generic

Public Class DBTable
    Implements IDBTable

    Private Const STR_IAUDITABLE_INTERFACE As String = "IAuditable"
    Private Const STR_IAUDITABLE_INTERFACE_USER_INT As String = "IAuditable2"
    Private _exludedFields As List(Of String) = New List(Of String)
    Private _fields As Dictionary(Of String, IDBField)
    Private _selectObject As String ' in case you want more than the select * from Table name...
    Private _childlrenAssociationCount As Integer = 0
    Private _parentAssociationCount As Integer = 0

    ''' <summary>
    ''' The set of fields that have been defined and customized in the generator xml 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CustomizedFields() As Dictionary(Of String, IDBField) = New Dictionary(Of String, IDBField)() Implements IDBTable.CustomizedFields

    ''' <summary>
    ''' List of Associations
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Associations() As List(Of IAssociation) = New List(Of IAssociation) Implements IDBTable.Associations

    Public Property LookupInfo() As Dictionary(Of String, FieldLookupInfo) _
        = New Dictionary(Of String, FieldLookupInfo) Implements IDBTable.LookupInfo

    Public Overridable Sub addAssociation(ByVal n As IAssociation) Implements IDBTable.addAssociation
        If n.isParent() Then
            _parentAssociationCount += 1
        Else
            _childlrenAssociationCount += 1
        End If

        n.parentDBTable = Me
        Me.Associations.Add(n)
    End Sub

    Public Function ChildrenAssociationCount() As Integer Implements IDBTable.ChildrenAssociationCount

        Return _childlrenAssociationCount

    End Function

    Public Function ParentAssociationCount() As Integer Implements IDBTable.ParentAssociationCount

        Return _parentAssociationCount

    End Function


    Public Sub addCustomizedField(ByVal fld As IDBField) Implements IDBTable.addCustomizedField

        fld.ParentTable = Me
        Me.CustomizedFields.Add(fld.FieldName.ToLower, fld)

    End Sub


    Private Sub loadFields()

        Dim tblFields As Dictionary(Of String, IDBField) = New Dictionary(Of String, IDBField)
        Dim objStmt As IDataReader = Nothing

        Try
            If (String.IsNullOrEmpty(Me.PrimaryKeyFieldName)) Then
                Me.loadPrimaryKey()
            End If

            'load all field from "selectObject" which could be a view.
            'note: do not quote [] name, to support other databases
            objStmt = ModelGenerator.Current.dbConn.getDataReader("select * from " & Me.SelectObject & " where 1=0")
            Dim objectFields As Dictionary(Of String, IDBField) = extractFieldsFromMetadata(objStmt)
            ModelGenerator.Current.dbConn.closeDataReader(objStmt)

            If Me.SelectObject = Me.TableName Then
                For Each field As DBField In objectFields.Values
                    field.IsTableField = True
                Next
            Else
                'if we are selecting from a view, load table fields and mark each 
                'field in the FIELDS collection whether isTable or not
                objStmt = ModelGenerator.Current.dbConn.getDataReader("select * from " & Me.quotedTableName & " where 1=0")
                tblFields = extractFieldsFromMetadata(objStmt)

                For Each skey As String In objectFields.Keys
                    If tblFields.ContainsKey(skey) Then
                        objectFields.Item(skey).IsTableField = True
                    Else
                        objectFields.Item(skey).IsTableField = False
                    End If
                Next

            End If

            Me.Fields = objectFields
            Me.determineIAuditable()
            Me.applyXMLCustomizations()

        Catch e As Exception

            Throw New ApplicationException(e.Message)
        Finally
            ModelGenerator.Current.dbConn.closeDataReader(objStmt)
        End Try

    End Sub


    ''' <summary>
    ''' Adds a field in the exluded field collection of the object
    ''' Such fields will not be included in code generation or sql generation
    ''' </summary>
    ''' <param name="fname"></param>
    ''' <remarks></remarks>
    Public Sub addExludedField(ByVal fname As String) Implements IDBTable.addExludedField

        If Me.Fields.ContainsKey(fname.ToLower) = False Then
            'Throw New ApplicationException("Field " & f.FieldName & " Not Found! Remember to specify field by Canonical, Runtime Name.")
            Throw New ApplicationException("Field " & Me.TableName & "." & fname & _
                                           "Not Found! Remember to specify field by database Name.")
        End If

        Me.Fields.Remove(fname.ToLower)
        Me._exludedFields.Add(fname.ToLower)

    End Sub

    Private Function extractFieldsFromMetadata(ByVal rs As IDataReader) As Dictionary(Of String, IDBField)

        Dim vec As Dictionary(Of String, IDBField) = New Dictionary(Of String, IDBField)
        Dim dbField As DBField
        Dim rsMetaData As DataTable = rs.GetSchemaTable()
        Dim numberOfColumns As Integer = rsMetaData.Rows.Count()
        Dim dr As DataRow

        For i As Integer = 0 To numberOfColumns - 1
            dr = rsMetaData.Rows(i)
            dbField = New DBField()
            dbField.FieldName = CStr(dr.Item("ColumnName"))
            dbField.RuntimeType = CType(dr.Item("DataType"), System.Type)
            dbField.Scale = CInt(dr.Item("NumericScale"))
            dbField.Precision = CInt(dr.Item("NumericPrecision"))
            dbField.Size = CInt(dr.Item("ColumnSize"))
            dbField.Nullable = CBool(NullChecker.intNull(dr.Item("AllowDBNull")))
            'dbField.SQLType = CInt(dr.Item("ProviderType"))
            dbField.isPrimaryKey = dbField.FieldName.ToUpper = Me.getPrimaryKeyName.ToUpper
            'note: this does not work: CBool(NullChecker.intNull(dr.Item("IsKey"))) '

            dbField.ParentTable = Me
            If _exludedFields.Contains(dbField.FieldName.ToLower) = True Then
                'skip this field!
                Debug.WriteLine("skipped " & dbField.FieldName)
            Else

                vec.Add(dbField.FieldName.ToLower, dbField)
            End If

        Next i

        Return vec

    End Function


    Private Sub loadPrimaryKey()

        Dim pkFieldName As String = String.Empty
        Dim sql As String = String.Empty
        Dim rs As IDataReader = Nothing

        Try

            If ModelGenerator.Current.dbConn.sqldialect = DBUtils.enumSqlDialect.ORACLE Then
                pkFieldName = ModelGenerator.Current.dbConn.getSValue("SELECT COLUMN_NAME, POSITION FROM user_CONS_COLUMNS a, user_constraints b WHERE a.TABLE_NAME ='" & _TableName & "' AND b.constraint_type = 'P' and a.table_name=b.table_name and a.constraint_name = b.constraint_name")

            ElseIf ModelGenerator.Current.dbConn.sqldialect = DBUtils.enumSqlDialect.MSSQL Then
                pkFieldName = ModelGenerator.Current.dbConn.getSValue("SELECT a.Column_Name FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE AS a CROSS JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS AS b WHERE (b.constraint_type = 'PRIMARY KEY') AND (a.constraint_name = b.constraint_name) AND a.table_name ='" & _TableName & "'")  'order by a.table_name, a.column_name

            ElseIf ModelGenerator.Current.dbConn.sqldialect = DBUtils.enumSqlDialect.JET Or _
                   ModelGenerator.Current.dbConn.sqldialect = DBUtils.enumSqlDialect.MSSQL Then

                Dim mySchema As DataTable = CType(ModelGenerator.Current.dbConn.Connection, OleDb.OleDbConnection).GetOleDbSchemaTable(OleDb.OleDbSchemaGuid.Primary_Keys, _
                    New Object() {Nothing, Nothing, Me._TableName})

                If mySchema.Rows.Count = 0 Then
                    pkFieldName = ""
                Else
                    pkFieldName = mySchema.Rows(0).ItemArray(3).ToString()
                End If


            End If


        Catch ex As Exception
            Throw New ApplicationException("Failed to get Primary Key field name for table:**" & Me._TableName & "**" & _
                                           Constants.vbCrLf & "DBObject requires the table to have a primary key field, consisting of just one column." & ex.Message())


        End Try

        If pkFieldName.Equals(String.Empty) Then
            Throw New ApplicationException("Faield to get Primary Key field name for table **" & Me._TableName & "**" & Constants.vbCrLf & "DBObject requires the table to have a primary key field, consisting of just one column.")
        End If

        Me.PrimaryKeyFieldName = pkFieldName

    End Sub


    Public ReadOnly Property hasFieldName(ByVal fname As String) As Boolean Implements IDBTable.hasFieldName
        Get
            Return Me.Fields.ContainsKey(fname.ToLower)
        End Get
    End Property

    Public ReadOnly Property hasAuditFields() As Boolean Implements IDBTable.hasAuditFields

        Get
            Dim hasAudit As Boolean = Me.hasFieldName("create_date") AndAlso _
                    Me.hasFieldName("update_date") AndAlso _
                    Me.hasFieldName("create_user") AndAlso _
                    Me.hasFieldName("update_user")

            hasAudit = hasAudit Or _
                    Me.hasFieldName("createdate") AndAlso _
                    Me.hasFieldName("updatedate") AndAlso _
                    Me.hasFieldName("createuser") AndAlso _
                    Me.hasFieldName("updateuser")
            Return hasAudit
        End Get

    End Property

    Public Property Fields() As Dictionary(Of String, IDBField) Implements IDBTable.Fields
        Get
            If _fields Is Nothing Then
                Me.loadFields()

            End If
            Return _fields
        End Get

        Set(ByVal value As Dictionary(Of String, IDBField))
            _fields = value
        End Set
    End Property




    ''' <summary>
    ''' If a field has been customized / specified in the xml generator specification
    ''' we apply the customization here
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub applyXMLCustomizations()

        For Each fld As DBField In Me.Fields.Values

            If Me.CustomizedFields.ContainsKey(fld.FieldName.ToLower) Then

                Dim f As DBField = CType(Me.CustomizedFields.Item(fld.FieldName.ToLower), DBField)

                If f.AccessLevel <> String.Empty Then
                    fld.AccessLevel = f.AccessLevel
                End If
                If f.UserSpecifiedDataType IsNot Nothing Then
                    fld.UserSpecifiedDataType = f.UserSpecifiedDataType
                End If
                fld.XMLSerializationIgnore = f.XMLSerializationIgnore

            End If

        Next

    End Sub

    Public Overridable Function getTableFields() As Dictionary(Of String, IDBField) Implements IDBTable.getTableFields

        Dim ret As Dictionary(Of String, IDBField) = New Dictionary(Of String, IDBField)
        For Each fkey As String In Me.Fields.Keys
            If Me.Fields.Item(fkey).IsTableField Then
                ret.Add(fkey, Me.Fields.Item(fkey))
            End If
        Next
        Return ret

    End Function

    Public Property PrimaryKeyFieldName() As String Implements IDBTable.PrimaryKeyFieldName


    Public ReadOnly Property quotedTableName() As String Implements IDBTable.quotedTableName

        Get
            Return ModelGenerator.Current.dbConn.quoteObjectName(_TableName)
        End Get

    End Property
    Public ReadOnly Property quotedSelectObject() As String Implements IDBTable.quotedSelectObject

        Get
            Return ModelGenerator.Current.dbConn.quoteObjectName(_selectObject)
        End Get

    End Property

    Public Overridable Function getPrimaryKeyName() As String Implements IDBTable.getPrimaryKeyName

        If PrimaryKeyFieldName Is Nothing OrElse Me.PrimaryKeyFieldName.Equals("") Then
            loadPrimaryKey()
        End If

        Return Me.PrimaryKeyFieldName
    End Function

    Public Shared Function getRuntimeName(ByVal name As String) As String

        If name = "NEW" Then name = "dbNew"

        Dim javaName As System.Text.StringBuilder = New System.Text.StringBuilder()
        Dim words() As String = name.ToLower().Split("_"c)

        If words.Length > 1 Then

            For i As Integer = 0 To words.Length - 1

                If (words(i).Length > 0) Then
                    Dim first As Char = Char.ToUpper(words(i).Chars(0))
                    javaName.Append(first + words(i).Substring(1))
                End If

            Next i
        Else
            Dim first As Char = Char.ToUpper(name.Chars(0))
            javaName.Append(first + name.Substring(1))
        End If

        Return javaName.ToString()

    End Function


    Public Function getPrimaryKeyField() As IDBField Implements IDBTable.getPrimaryKeyField

        Dim sb As String = ""
        Dim vec As Dictionary(Of String, IDBField) = Me.Fields()

        For Each field As DBField In vec.Values
            If field.FieldName().Equals(Me.getPrimaryKeyName()) Then
                Return field
            End If

        Next

        Throw New ApplicationException("No primary key for table " & Me.TableName)
        'Return Nothing

    End Function
    Public Overridable Function getPrimaryKeyDType() As String Implements IDBTable.getPrimaryKeyDType

        Dim sb As String = String.Empty
        Dim vec As Dictionary(Of String, IDBField) = Me.Fields()

        For Each field As DBField In vec.Values
            If field.FieldName().Equals(Me.getPrimaryKeyName()) Then
                Return field.RuntimeTypeStr()
            End If

        Next

        Return sb

    End Function


    Public Sub New(ByVal _intableName As String, ByVal _pkfield As String)

        Me._TableName = _intableName
        Me.PrimaryKeyFieldName = _pkfield

    End Sub


    Public Overridable Property SelectObject() As String Implements IDBTable.SelectObject
        Get
            If String.IsNullOrEmpty(_selectObject) Then
                Return Me.TableName()
            End If

            Return _selectObject

        End Get
        Set(ByVal value As String)
            Me._selectObject = value
        End Set
    End Property

    Public Property isReadOnly() As Boolean Implements IDBTable.isReadOnly

    Public Property TableName() As String Implements IDBTable.TableName

    ''' <summary>
    ''' Implemented Interface(s) by the class
    ''' </summary>
    ''' <remarks></remarks>
    Public Property ImplementedInterfaces() As List(Of String) = New List(Of String)() Implements IDBTable.ImplementedInterfaces

    Public Sub addImplemetedInterface(ByVal str As String) Implements IDBTable.addImplemetedInterface

        If (Not Me._ImplementedInterfaces.Contains(str)) Then
            Me._ImplementedInterfaces.Add(str)
        End If

    End Sub

    Public Property ImplementsAsString() As String Implements IDBTable.ImplementsAsString
        Get
            Dim ret As String = String.Empty
            If Not Me.ImplementedInterfaces Is Nothing Then
                For Each s As String In Me.ImplementedInterfaces

                    If (String.IsNullOrEmpty(s.Trim)) = False Then
                        If (String.IsNullOrEmpty(ret) = False) Then ret &= ","
                        ret &= s.Trim
                    End If

                Next
            End If

            Return ret
        End Get
        Set(ByVal value As String)
            If String.IsNullOrEmpty(value) Then
                _ImplementedInterfaces = New List(Of String)
            Else

                Dim arr() As String = value.Split(CChar(","))
                If (arr.Length > 0) Then
                    _ImplementedInterfaces.AddRange(arr)
                End If
            End If
        End Set
    End Property

    Private Sub determineIAuditable()
        'If Me.TableName = "paycheck" Then Stop
        If Me.hasAuditFields Then

            'determine if IAutitable2 or IAudiatble
            If Me.hasFieldName("create_user") Then
                If Me.Fields("create_user").RuntimeTypeStr = "System.String" Then
                    If Me.ImplementedInterfaces.Contains(STR_IAUDITABLE_INTERFACE) = False Then
                        Me.ImplementedInterfaces.Add(STR_IAUDITABLE_INTERFACE)
                    End If
                Else
                    If Me.ImplementedInterfaces.Contains(STR_IAUDITABLE_INTERFACE_USER_INT) = False Then
                        Me.ImplementedInterfaces.Add(STR_IAUDITABLE_INTERFACE_USER_INT)
                    End If
                End If
            ElseIf Me.hasFieldName("createuser") Then
                If Me.Fields("createuser").RuntimeTypeStr = "System.String" Then
                    If Me.ImplementedInterfaces.Contains(STR_IAUDITABLE_INTERFACE) = False Then
                        Me.ImplementedInterfaces.Add(STR_IAUDITABLE_INTERFACE)
                    End If
                Else
                    If Me.ImplementedInterfaces.Contains(STR_IAUDITABLE_INTERFACE_USER_INT) = False Then
                        Me.ImplementedInterfaces.Add(STR_IAUDITABLE_INTERFACE_USER_INT)
                    End If
                End If
            End If
            

        End If
    End Sub

    Function getAuditInterface() As String Implements IDBTable.getAuditInterface

        If Me.ImplementedInterfaces.Contains(STR_IAUDITABLE_INTERFACE_USER_INT) Then
            Return STR_IAUDITABLE_INTERFACE_USER_INT
        End If
        If Me.ImplementedInterfaces.Contains(STR_IAUDITABLE_INTERFACE) Then
            Return STR_IAUDITABLE_INTERFACE
        End If
        Return String.Empty
    End Function

End Class
