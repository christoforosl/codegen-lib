Imports Microsoft.VisualBasic
Imports System.Collections.Generic

Public Class DBField
    Implements IDBField

    Private _userSpecifiedDataType As String
    Private _RuntimeType As System.Type

    Public Property AccessLevel() As String = "Public" Implements IDBField.AccessLevel
    Public Property OriginalRuntimeType() As System.Type Implements IDBField.OriginalRuntimeType
    Public Property RuntimeTypeStr() As System.String Implements IDBField.RuntimeTypeStr
    Public Property ParentTable() As IDBTable Implements IDBField.ParentTable

    'shows that the field belongs to our table. In case the field
    'comes from a view (selectobject) this will be false.  If true, the field is Used to built the insert/update statements
    Public Property IsTableField() As Boolean Implements IDBField.IsTableField

    Public Property FieldName() As String Implements IDBField.FieldName
    Public Property Size() As Integer Implements IDBField.Size
    Public Property Precision() As Integer Implements IDBField.Precision
    Public Property Scale() As Integer Implements IDBField.Scale
    Public Property Nullable() As Boolean Implements IDBField.Nullable
    Public Property XMLSerializationIgnore() As Boolean Implements IDBField.XMLSerializationIgnore

    Public Function isAuditField() As Boolean Implements IDBField.isAuditField

        If Me.FieldName.ToLower = "update_date" _
             OrElse Me.FieldName.ToLower = "update_user" _
             OrElse Me.FieldName.ToLower = "create_date" _
             OrElse Me.FieldName.ToLower = "create_user" _
             OrElse Me.FieldName.ToLower = "updatedate" _
             OrElse Me.FieldName.ToLower = "updateuser" _
             OrElse Me.FieldName.ToLower = "createdate" _
             OrElse Me.FieldName.ToLower = "createuser" Then

            Return True

        End If

        Return False

    End Function

    Public Property isPrimaryKey() As Boolean Implements IDBField.isPrimaryKey

    Public Overridable Function getConstant() As String Implements IDBField.getConstant
        Return "FLD_" & Me.FieldName().ToUpper()
    End Function

    Public Overridable Function getConstantStr() As String Implements IDBField.getConstantStr
        Return "STR_" & Me.getConstant()
    End Function



    Public Overridable Function getClassVariableDeclaration( _
                    Optional ByVal accessLevel As String = "private", _
                    Optional ByVal withInitialiser As Boolean = True) As String Implements IDBField.getClassVariableDeclaration

        Dim fname As String = Me.RuntimeFieldName()
        Dim ret As String = String.Empty

        If ModelGenerator.Current.dotNetLanguage = ModelGenerator.enumLanguage.VB Then
            ret = vbTab & accessLevel & " _" & _
                            fname & " as " & _
                            getFieldDataType()

            If withInitialiser AndAlso Me.isPrimaryKey = False Then ret &= " = Nothing"
        Else
            ret = vbTab & accessLevel & " " & getFieldDataType() & " _" & fname
            If withInitialiser AndAlso Me.isPrimaryKey = False Then ret &= " = null"
            ret &= ";"
        End If


        Return ret & vbCrLf

    End Function

    Public Overridable Function getFieldDataType() As String Implements IDBField.getFieldDataType

        If Me.isNullableDataType Then
            If ModelGenerator.Current.dotNetLanguage = ModelGenerator.enumLanguage.VB Then
                Return "Nullable (of " & Me._RuntimeTypeStr & ")"
            Else
                Return Me._RuntimeTypeStr & "?"
            End If
        Else
            Return Me._RuntimeTypeStr
        End If

    End Function

    Public ReadOnly Property isNullableDataType() As Boolean Implements IDBField.isNullableDataType
        Get
            If Me.isPrimaryKey Then
                Return False

            ElseIf Me.RuntimeTypeStr = "System.String" Then
                Return False

            Else
                Return True

            End If
        End Get
    End Property

    Public Overridable Function getPropertyDataType() As String Implements IDBField.getPropertyDataType

        Dim ret As String = ""

        If Me.isPrimaryKey Then
            ret = Me._RuntimeTypeStr

        ElseIf Me.FieldDataType = "System.String" Then
            ret = Me.FieldDataType

        ElseIf Me.isNullableDataType Then
            If (ModelGenerator.Current.dotNetLanguage = ModelGenerator.enumLanguage.CSHARP) Then
                ret = Me.FieldDataType & "?"
            Else
                ret = "Nullable (of " & Me.FieldDataType & ")"
            End If


        Else
            ret = Me.FieldDataType
        End If

        Return ret

    End Function

    Public Overridable Function getProperty() As String Implements IDBField.getProperty

        Return ModelGenerator.Current.IPropertyGenerator.generateCode(Me)

    End Function

    Private Function getOLEDBParameterType() As String

        If Me._OriginalRuntimeType Is System.Type.GetType("System.Date") OrElse _
                    Me._OriginalRuntimeType Is System.Type.GetType("System.DateTime") Then

            Return "OleDbType.Date"

        ElseIf Me._OriginalRuntimeType Is System.Type.GetType("System.Int16") Then
            'Return "CShort"
            Return "OleDbType.SmallInt"

        ElseIf Me._OriginalRuntimeType Is System.Type.GetType("System.Int32") Then
            Return "OleDbType.Integer"

        ElseIf Me._OriginalRuntimeType Is System.Type.GetType("System.Int64") Then
            Return "OleDbType.BigInt"

        ElseIf Me._OriginalRuntimeType Is System.Type.GetType("System.Decimal") Then
            Return "OleDbType.Decimal"

        ElseIf Me._OriginalRuntimeType Is System.Type.GetType("System.Double") Then
            Return "OleDbType.Double"

        ElseIf Me._OriginalRuntimeType Is System.Type.GetType("System.Single") Then
            Return "OleDbType.Single"

        ElseIf Me._OriginalRuntimeType Is System.Type.GetType("System.String") Then
            Return "OleDbType.VarWChar"

        ElseIf Me._OriginalRuntimeType Is System.Type.GetType("System.Boolean") Then
            Return "OleDbType.Boolean"

        ElseIf Me._OriginalRuntimeType Is System.Type.GetType("System.Byte") Then
            Return "OleDbType.Integer"
        Else
            Throw New ApplicationException(Me.FieldName & ":Unhandled TypeConverter for type:" & Me.RuntimeType.ToString)
        End If

    End Function

    Public Overridable Function getSQLParameter() As String Implements IDBField.getSQLParameter

        Const paramPrefix As String = "@"
        Dim ret As String
        Dim param As String = vbTab & vbTab & vbTab & _
                           "stmt.Parameters.Add( Me.dbConn.getParameter(""" & paramPrefix & "{0}"",obj.{1}))"

        If ModelGenerator.Current.dotNetLanguage = ModelGenerator.enumLanguage.VB Then
        Else
            param = param & ";"
        End If
        ret = String.Format(param, Me.FieldName, Me.PropertyName)

        Return ret & vbCrLf

    End Function

	Public Overridable ReadOnly Property PropertyName() As String Implements IDBField.PropertyName
		Get
			If Me.isAuditField Then
				Return DBTable.getRuntimeName(Me.FieldName())
			Else
				Return ModelGenerator.Current.FieldPropertyPrefix & DBTable.getRuntimeName(Me.FieldName())
			End If

		End Get
	End Property

    Public Overridable ReadOnly Property RuntimeFieldName() As String Implements IDBField.RuntimeFieldName
        Get
            Return DBTable.getRuntimeName(Me.FieldName())
        End Get
    End Property

    Public Sub New()
    End Sub

    Public Sub New(ByVal fieldName As String, ByVal fieldType As System.Type)
        Me.FieldName = fieldName
        Me.RuntimeType = fieldType
    End Sub

    Public Function isDate() As Boolean Implements IDBField.isDate

        Return Me.RuntimeType = Type.GetType("System.Date") OrElse _
              Me.RuntimeType Is System.Type.GetType("System.DateTime")

    End Function

    Public Function isString() As Boolean Implements IDBField.isString
        Return Me.RuntimeType = Type.GetType("System.String")
    End Function

    Public Function isBoolean() As Boolean Implements IDBField.isBoolean

        Return Me.UserSpecifiedDataType IsNot Nothing AndAlso
                Me.UserSpecifiedDataType = "System.Boolean"

    End Function

    Public Function isInteger() As Boolean Implements IDBField.isInteger

        Return Me.RuntimeType Is Type.GetType("System.Int32") OrElse _
                Me.RuntimeType Is Type.GetType("System.Byte") OrElse _
                Me.RuntimeType Is Type.GetType("System.Int64") OrElse _
                        Me.RuntimeType Is Type.GetType("System.Int16")

    End Function

    Public Function isDecimal() As Boolean Implements IDBField.isDecimal

        Return Me.RuntimeType Is Type.GetType("System.Decimal")

    End Function

    Public Function isLookup() As Boolean Implements IDBField.isLookup

        Return Me.ParentTable.LookupInfo.ContainsKey(Me.FieldName.ToUpper)

    End Function


    Public Property RuntimeType() As System.Type Implements IDBField.RuntimeType

        Get
            Return _RuntimeType
        End Get

        Set(ByVal value As System.Type)
            _OriginalRuntimeType = value
            If value Is Type.GetType("System.Single") OrElse _
                value Is Type.GetType("System.Float") Then
                _RuntimeType = Type.GetType("System.Decimal")

            ElseIf value Is Type.GetType("System.Int16") OrElse _
                    value Is Type.GetType("System.Int32") OrElse _
                    value Is Type.GetType("System.Byte") Then

                _RuntimeType = Type.GetType("System.Int64")

            Else
                _RuntimeType = value

            End If
            If Me._userSpecifiedDataType IsNot Nothing Then
                _RuntimeTypeStr = _userSpecifiedDataType
            End If
            _RuntimeTypeStr = _RuntimeType.ToString

        End Set

    End Property



    ''' <summary>
    ''' The data type of the field as defined / customized in the xml generator file.
    ''' </summary>
    Public Property UserSpecifiedDataType() As String Implements IDBField.UserSpecifiedDataType
        Get
            Return _userSpecifiedDataType

        End Get

        Set(ByVal value As String)
            _userSpecifiedDataType = value

        End Set
    End Property

    ''' <summary>
    ''' The data type of the field as defined / customized in the xml generator file.
    ''' If no customized data type was defined, then the default data type of the field
    ''' as defined in the databse table structure
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property FieldDataType() As String Implements IDBField.FieldDataType
        Get
            If _userSpecifiedDataType Is Nothing Then
                Return Me.RuntimeTypeStr
            Else
                Return _userSpecifiedDataType
            End If
        End Get

    End Property

End Class
