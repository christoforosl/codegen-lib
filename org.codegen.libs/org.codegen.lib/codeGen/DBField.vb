Imports Microsoft.VisualBasic
Imports System.Collections.Generic
Imports System.Text

Public Class DBField
    Implements IDBField


    ' Private _userSpecifiedDataType As String
    Private _RuntimeType As System.Type

    Public Property AccessLevel() As String Implements IDBField.AccessLevel
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

    ''' <summary>
    ''' Is the field in the database nullable?
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property isDBFieldNullable() As Boolean Implements IDBField.isDBFieldNullable

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
        Dim ft As String = getFieldDataType()
        If ModelGenerator.Current.dotNetLanguage = ModelGenerator.enumLanguage.VB Then
            ret = vbTab & accessLevel & " _" & _
                            fname & " as " & ft

            If withInitialiser AndAlso Me.isNullableProperty Then ret &= " = Nothing"
        Else
            ret = vbTab & accessLevel & " " & ft & " _" & fname
            If withInitialiser AndAlso Me.isNullableProperty Then ret &= " = null"
            ret &= ";"
        End If


        Return ret & vbCrLf

    End Function

    Public Overridable Function getFieldDataType() As String Implements IDBField.getFieldDataType

        If Me.isBooleanFromInt OrElse Me.isEnumFromInt Then
            Return "System.Int64?"
        Else
            If Me.isNullableProperty Then
               
                Return Me._RuntimeTypeStr & "?"
            Else
                If ModelGenerator.Current.dotNetLanguage = ModelGenerator.enumLanguage.VB _
                    AndAlso Me.isBinaryField Then
                    Return "System.Byte()"
                Else
                    Return Me._RuntimeTypeStr
                End If

            End If
        End If

    End Function


    Public ReadOnly Property isNullableProperty() As Boolean Implements IDBField.isNullableProperty
        Get
            Return Me.isPrimaryKey = False AndAlso Me.isString = False AndAlso Me.isBoolean = False _
                AndAlso Me._OriginalRuntimeType IsNot System.Type.GetType("System.Byte[]")
        End Get
    End Property

    Public Overridable Function getPropertyDataType() As String Implements IDBField.getPropertyDataType

        Dim ret As String = ""

        If Me.isPrimaryKey Then
            ret = Me._RuntimeTypeStr

        ElseIf Me.isBooleanFromInt() Then
            ret = CStr(IIf(isCSharp(), "bool", "Boolean"))

        ElseIf Me.isEnumFromInt() Then
            ret = ModelGenerator.Current.EnumFieldsCollection.getEnumField(Me).enumTypeName & "?"

        ElseIf Me.FieldDataType = "System.String" Then
            ret = Me.FieldDataType

        ElseIf Me.isNullableProperty Then
            ret = Me.FieldDataType & "?"

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

        ElseIf Me._OriginalRuntimeType Is System.Type.GetType("System.Guid") Then
            Return "OleDbType.Guid"

        ElseIf Me._OriginalRuntimeType Is System.Type.GetType("System.Byte[]") Then
            Return "OleDbType.Binary"
        Else
            Dim msg As String = Me.ParentTable.TableName & "." & Me.FieldName & ":Unhandled TypeConverter for type:" & Me.RuntimeType.ToString
            System.Diagnostics.Debug.WriteLine(msg)
            Throw New ApplicationException(msg)
        End If

    End Function

    Public Overridable Function getSQLParameter() As String Implements IDBField.getSQLParameter

        Dim MeOrThis As String = "Me"
        If ModelGenerator.Current.dotNetLanguage = ModelGenerator.enumLanguage.VB Then
        Else
            MeOrThis = "this"
        End If

        Dim param As StringBuilder = New StringBuilder(vbTab).Append(vbTab).Append(vbTab)
        param.Append("stmt.Parameters.Add(").Append(MeOrThis).Append(".dbConn.getParameter(")
        param.Append(ModelGenerator.Current.CurrentObjectBeingGenerated.ClassName).Append(".").Append(Me.getConstantStr()).Append(",")

        If Me.FieldName.ToLower = "id" Then
            If Me.isString Then
                param.Append("Convert.ToString(obj.").Append(Me.PropertyName).Append(")))")
            ElseIf Me.isInteger Then
                param.Append("Convert.ToInt64(obj.").Append(Me.PropertyName).Append(")))")
            End If
        Else
            If Me.isEnumFromInt Then
                Dim xs As String
                If Me.isCSharp Then
                    xs = String.Format("obj.{0} == null? (long?)null : Convert.ToInt64(obj.{0})))", Me.PropertyName)
                Else
                    xs = String.Format("CType(IIf(obj.{0} Is Nothing, Nothing, Convert.ToInt64(obj.{0})), Long?)))", Me.PropertyName)
                End If

                param.Append(xs)
            Else
                param.Append("obj.").Append(Me.PropertyName).Append("))")
            End If

            End If

        If ModelGenerator.Current.dotNetLanguage = ModelGenerator.enumLanguage.CSHARP Then
            param.Append(";")
        End If

        param.Append(vbCrLf)
        Return param.ToString

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

    Public Function isBooleanFromInt() As Boolean Implements IDBField.isBooleanFromInt

        Dim iamBoolean As Boolean = ModelGenerator.Current.BooleanFieldsCollection.isBooleanField(Me)

        Dim isIntIndatabase = (Me.OriginalRuntimeType = Type.GetType("System.Int16") _
            OrElse Me.OriginalRuntimeType = Type.GetType("System.Int32") _
            OrElse Me.OriginalRuntimeType = Type.GetType("System.Byte") _
            OrElse Me.OriginalRuntimeType = Type.GetType("System.Int64"))

        If (iamBoolean AndAlso Not isIntIndatabase) Then
            Throw New ApplicationException(String.Format("Error: Boolean specified fields must be of type Integer in the database. It seems {0}.{1} is not!", Me.ParentTable.TableName, Me.FieldName))
        End If

        Return isIntIndatabase AndAlso iamBoolean


    End Function

    Public Function isEnumFromInt() As Boolean Implements IDBField.isEnumFromInt

        Dim iamEnum As Boolean = ModelGenerator.Current.EnumFieldsCollection.getEnumField(Me) IsNot Nothing

        Dim isIntIndatabase = (Me.OriginalRuntimeType = Type.GetType("System.Int16") _
            OrElse Me.OriginalRuntimeType = Type.GetType("System.Int32") _
            OrElse Me.OriginalRuntimeType = Type.GetType("System.Byte") _
            OrElse Me.OriginalRuntimeType = Type.GetType("System.Int64"))

        If (iamEnum AndAlso Not isIntIndatabase) Then
            Throw New ApplicationException(String.Format("Error: Enumeration specified fields must be of type Integer in the database. It seems {0}.{1} is not!", Me.ParentTable.TableName, Me.FieldName))
        End If

        Return isIntIndatabase AndAlso iamEnum

    End Function
    Public Function isBoolean() As Boolean Implements IDBField.isBoolean

        Return Me.RuntimeType = Type.GetType("System.Boolean") OrElse _
                ModelGenerator.Current.BooleanFieldsCollection.isBooleanField(Me)

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
                value Is Type.GetType("System.Double") OrElse _
                value Is Type.GetType("System.Byte") OrElse _
                value Is Type.GetType("System.Float") Then

                _RuntimeType = Type.GetType("System.Decimal")

            ElseIf value Is Type.GetType("System.Int16") OrElse _
                    value Is Type.GetType("System.Int32") OrElse _
                    value Is Type.GetType("System.Byte") Then

                _RuntimeType = Type.GetType("System.Int64")
            Else
                _RuntimeType = value
            End If

            'for oracle only!!
            If _RuntimeType Is Type.GetType("System.Decimal") AndAlso Me.Scale = 0 Then
                _RuntimeType = Type.GetType("System.Int64")

                _OriginalRuntimeType = Type.GetType("System.Decimal")

            End If

            _RuntimeTypeStr = _RuntimeType.ToString

        End Set

    End Property


    ''' <summary>
    ''' If Me.RuntimeType Is not the same as Me.RuntimeType, 
    ''' it returns the appropriate Convert.toXXX call
    ''' </summary>
    Public Function getDataReaderConverter() As String Implements IDBField.getDataReaderConverter

        If Me.RuntimeType Is Me.OriginalRuntimeType Then Return String.Empty

        If Me.RuntimeType Is System.Type.GetType("System.Date") OrElse _
               Me.RuntimeType Is System.Type.GetType("System.DateTime") Then

            Return "Convert.ToDateTime"

        ElseIf Me.RuntimeType Is System.Type.GetType("System.Int16") Then
            Return "Convert.ToInt16"

        ElseIf Me.RuntimeType Is System.Type.GetType("System.Int32") Then
            Return "Convert.ToInt32"

        ElseIf Me.RuntimeType Is System.Type.GetType("System.Int64") Then
            Return "Convert.ToInt64"

        ElseIf Me.RuntimeType Is System.Type.GetType("System.Decimal") Then
            Return "Convert.ToDecimal"

        ElseIf Me.RuntimeType Is System.Type.GetType("System.Double") Then
            Return "Convert.ToDouble"

        ElseIf Me.RuntimeType Is System.Type.GetType("System.Single") Then
            Return "Convert.ToSingle"

        ElseIf Me.RuntimeType Is System.Type.GetType("System.Float") Then
            Return "Convert.ToFloat"

        ElseIf Me.RuntimeType Is System.Type.GetType("System.String") Then
            Return String.Empty

        ElseIf Me.RuntimeType Is System.Type.GetType("System.Boolean") Then
            Return "Convert.ToBoolean"

        ElseIf Me.RuntimeType Is System.Type.GetType("System.Byte") Then
            Return "Convert.ToByte"

        ElseIf Me.RuntimeType Is System.Type.GetType("System.Guid") Then
            Return String.Empty

        Else
            Return String.Empty

        End If

    End Function


    ''' <summary>
    ''' The data type of the field as defined / customized in the xml generator file.
    ''' If no customized data type was defined, then the default data type of the field
    ''' as defined in the database table structure
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property FieldDataType() As String Implements IDBField.FieldDataType
        Get

            If (ModelGenerator.Current.dotNetLanguage = ModelGenerator.enumLanguage.VB) _
                AndAlso isBinaryField() Then

                Return "System.Byte()"
            Else
                Return Me.RuntimeTypeStr
            End If
        End Get

    End Property

    Public Property DBType As String Implements IDBField.DBType

    Public Function isBinaryField() As Boolean Implements IDBField.isBinaryField

        Return Me.RuntimeType Is System.Type.GetType("System.Byte[]")

    End Function

    Private Function isCSharp() As Boolean
        Return (ModelGenerator.Current.dotNetLanguage = ModelGenerator.enumLanguage.CSHARP)
    End Function

End Class
