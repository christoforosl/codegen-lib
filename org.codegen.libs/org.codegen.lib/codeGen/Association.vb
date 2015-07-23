Imports Microsoft.VisualBasic
Imports System.Text
Imports org.codegen.lib.FileComponents

Public Class Association

    Protected Const STR_RELATION_PARENT As String = "PARENT"
    Protected Const STR_RELATION_CLIENT As String = "CLIENT"

    Public Property ParentDatatype As String 'Implements IAssociation.ParentDatatype
    Public Property ChildDatatype As String 'Implements IAssociation.ChildDatatype
    Public Property DataType As String 'Implements IAssociation.DataType

    Protected _dbMapperClass As String
    Protected _relationType As String = "CHILD"
    Protected _cardinality As String = ""
    Protected _sortAsc As Boolean = True

    Protected _childManyTemplate As String
    Protected _childOneTemplate As String
    Protected _parentTemplate As String

    Public Property parentDBTable As IDBTable
    Public Property AccessLevel() As String = "Public"

    Public Property associationNameSingular As String
    Public Overridable Property associationName() As String 'Implements IAssociation.associationName
    Public Property ChildFieldIsPK() As Boolean 'Implements IAssociation.ChildFieldIsPK

    Public Sub setCardinality(ByVal val As String) 'Implements IAssociation.setCardinality
        If (Not String.IsNullOrEmpty(val) AndAlso (val = "*" OrElse val = "1")) Then
            Me._cardinality = val
        Else
            Throw New System.ArgumentException("cardinality must be 1 or *")
        End If

    End Sub


    ''' <summary>
    ''' Returns the DataType of the association variable.
    ''' If Cardinality is many (*), then it returns a List(Of ) the association datatype, 
    ''' and If Cardinality is 1 , then it returns the association datatype
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overridable ReadOnly Property DataTypeVariable() As String 'Implements IAssociation.getDataTypeVariable
        Get

            If Me.isCardinalityMany Then
                Return "List(Of " & Me.DataType & ")"
            Else
                Return Me.DataType
            End If
        End Get
    End Property

    Public Overridable Function getTestCode() As String 'Implements IAssociation.getTestCode

        Dim ret As String = String.Empty
        If Me._cardinality.Equals("*") Then
            ret &= "Assert.isTrue(p." & Me.PropertyName & " isNot Nothing)"
        End If

        Return ret

    End Function

    Public ReadOnly Property LoadedFlagVariableName As String

        Get
            Return Me.associationName & "Loaded"
        End Get

    End Property

    ''' <summary>
    ''' Returns the variable that will be generated for this association in the Model Object
    ''' </summary>
    Public Overridable Function getVariableName() As String
        Return "_" & Me.associationName
    End Function

    ''' <summary>
    ''' Returns the code that declares the association
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overridable Function getVariableDeclarationCode() As String

        Dim sb As System.Text.StringBuilder = New System.Text.StringBuilder()
        sb.Append(vbTab).Append("<DataMember(Name:=""" + Me.PropertyName + """)>").Append(vbCrLf)
        sb.Append(vbTab).Append("private ")
        sb.Append(Me.getVariableName)
        sb.Append(" as ")
        sb.Append(Me.DataTypeVariable())
        sb.Append(" = nothing '' initialize to nothing, for lazy load logic below !!!")
        sb.Append(vbCrLf)
        Return sb.ToString()

    End Function

    Public Function getDeletedMethodName() As String
        Return Me.PropertyName & "GetDeleted"
    End Function
    Public Overridable Function getDeletedVariable() As String 'Implements IAssociation.getDeletedVariable

        If Me.isCardinalityMany Then
            Dim sb As System.Text.StringBuilder = New System.Text.StringBuilder() ' TODO type initialisation here
            sb.Append(vbTab & "private _deleted").Append(Me.associationName)
            sb.Append(" as List(Of " & Me.DataType & ")")
            sb.Append(" = new ").Append("List(Of " & Me.DataType & ")").Append("''''' initialize to empty list !!!")
            sb.Append(vbCrLf)
            Return sb.ToString()
        Else
            Return String.Empty
        End If

    End Function

    Public Overridable Function getSaveParentCode(ByVal parentMoObjType As String) As String 'Implements IAssociation.getSaveParentCode
        Dim ret As String = ""
        If isParent() Then
            ' parent relationship!
            ' need to update self when parent is 
            ' updated. Example: Employer->Address. with link
            ' field address_id on the Employer table.
            '
            ' for example: address is saved first, employer second.

            If Me.IsReadOnly Then
                'in case of readonly association, just append a comment
                ret += vbTab + vbTab & "'***Readonly Parent Association:" & Me.associationName.ToLower() & " ***!" & vbCrLf
            Else

                Dim mapperClassName As String = GetAssociatedMapperClassName()
                Dim mappervar As String = Me.associationName.ToLower() & "Mapper"
                ret += vbTab + vbTab & "'*** Parent Association:" & Me.associationName.ToLower() & vbCrLf
                If Me.isCardinalityMany Then
                    ret += vbTab + vbTab & "if thisMo." & Me.LoadedFlagVariableName & " AndAlso thisMo." & Me.PropertyName() & "().NeedsSave() Then" & vbCrLf
                Else
                    ret += vbTab + vbTab & "if (thisMo." & Me.PropertyName & " is Nothing=false) AndAlso thisMo." & Me.PropertyName() & "().NeedsSave() Then" & vbCrLf
                End If

                ret += vbTab + vbTab + vbTab + "Dim mappervar as " & mapperClassName & "= new " & mapperClassName & "(me.dbConn)" & vbCrLf
                ret += vbTab + vbTab + vbTab + "mappervar.save(thisMo." & Me.PropertyName() & ")" & vbCrLf
                ret += vbTab + vbTab & vbTab + "thisMo." & Me.ChildField.PropertyName & " = thisMo." & Me.PropertyName() & "." & Me.ParentField.PropertyName & vbCrLf
                ret += vbTab + vbTab & "end if" & vbCrLf
                ret += vbTab + vbTab + vbCrLf

            End If
        End If
        Return ret

    End Function

    ''' <summary>
    ''' Returns the mapper of the parent object of this association
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Function GetAssociatedMapperClassName() As String

        Dim otog As ObjectToGenerate = ModelGenerator.Current.getObjectOfDataType(Me.DataType)

        If (otog Is Nothing) Then
            Throw New ApplicationException("Could not find object to gerenarate from type:" & Me.DataType)
        End If
        Dim mapperClassName As String = otog.FullyQualifiedMapperClassName
        Return mapperClassName

    End Function

    Public Overridable Function getSaveChildrenCode() As String 'Implements IAssociation.getSaveChildrenCode

        Dim ret As String = ""
        If isParent() Then
            ' parent relationship!
            ' need to update self when parent is 
            ' updated. Example: Employer->Address
            ' address is saved first, employer second.

        Else
            If Me.IsReadOnly Then
                ret += vbTab + vbTab & "'***Readonly Child Association:" & Me.associationName.ToLower() & " ***!" & vbCrLf
            Else

                Dim mapperClassName As String = GetAssociatedMapperClassName()
                Dim mappervar As String = Me.associationName.ToLower() & "Mapper"
                ret = vbTab + vbTab & "'** Child Association:" & Me.associationName.ToLower() & vbCrLf
                ret &= vbTab & vbTab & "If ret." & Me.LoadedFlagVariableName & " = True then " & vbCrLf
                ret &= vbTab & vbTab & vbTab & "Dim " & mappervar & " as " & _
                              mapperClassName & " = new " & mapperClassName & "(me.DBConn())" & vbCrLf

                If Me.isCardinalityMany Then
                    ret &= vbTab & vbTab & vbTab & mappervar & ".saveList(ret." & Me.PropertyName() & "())" & vbCrLf
                    ret &= vbTab & vbTab & vbTab & mappervar & ".deleteList(ret." & Me.PropertyName & "GetDeleted())" & vbCrLf
                Else
                    ret &= vbTab & vbTab & vbTab & mappervar & ".save(ret." & Me.PropertyName() & "())" & vbCrLf
                End If
                ret &= vbTab & vbTab & "End if"
                ret &= vbCrLf

            End If

        End If

        Return ret

    End Function

    Public Overridable Function isParent() As Boolean 'Implements IAssociation.isParent
        Return Me.RelationType().Equals(STR_RELATION_PARENT)
    End Function

    Private Function getCanonicalName() As String

        Return Me.associationName.Substring(0, 1).ToUpper() + Me.associationName.Substring(1)

    End Function

    ''' <summary>
    ''' Returns the PropertyName that will be generated for this association. This is the Property Prefix + the Accociation Name
    ''' </summary>
    Public ReadOnly Property PropertyName() As String 'Implements IAssociation.getGet
        Get
            Return ModelGenerator.Current.FieldPropertyPrefix & getCanonicalName()
        End Get
    End Property

    Public Property PropertiesImplementInterface() As String 'Implements IAssociation.PropertiesImplementInterface

    Public Function ParentObjectToGenerate() As ObjectToGenerate

        Dim parentOg As ObjectToGenerate = ModelGenerator.Current.getObjectOfDataType(Me.ParentDatatype)
        If (parentOg Is Nothing) Then Throw New ApplicationException(String.Format("No PARENT object for generation exists with name {0}", Me.ParentDatatype))
        Return parentOg

    End Function


    Public Function ChildObjectToGenerate() As ObjectToGenerate

        Dim childOg As ObjectToGenerate = ModelGenerator.Current.getObjectOfDataType(Me.ChildDatatype)
        If (childOg Is Nothing) Then Throw New ApplicationException(String.Format("No CHILD object for generation exists with name {0}", Me.ChildDatatype))
        Return childOg

    End Function

    ''' <summary>
    ''' Returns the IDBField instance of the child field of the association
    ''' </summary>
    Public Function ChildField() As IDBField

        Dim cfield = Me.ChildObjectToGenerate.DbTable.getFieldByName(Me.ChildFieldName)
        If (cfield Is Nothing) Then Throw New ApplicationException(String.Format("CHILD object {0} does not have a field with name {1}", Me.ChildDatatype, Me.ChildFieldName))
        Return cfield

    End Function

    Public Function ParentField() As IDBField

        Dim pfield = Me.ParentObjectToGenerate.DbTable.getFieldByName(Me.ParentFieldName)
        If (pfield Is Nothing) Then Throw New ApplicationException(String.Format("PARENT object {0} does not have a field with name {1}", Me.ParentDatatype, Me.ParentFieldName))
        Return pfield

    End Function

    Public Overridable Function getCodeFromTemplate() As String

        Dim sb As System.Text.StringBuilder = New System.Text.StringBuilder()


        If Me.isCardinalityMany And Me.RelationType = STR_RELATION_PARENT Then
            Throw New ApplicationException("PARENT relationship with cardinality ""MANY"" not allowed!")
        End If

        Dim stmpl As String = templateText

        stmpl = stmpl.Replace("<sort>", CStr(IIf(String.IsNullOrEmpty(Me.SortField) = False, _
                                                 "this._<association_name>.Sort();", "")))

        '<getDeletedMethodName>
        stmpl = stmpl.Replace("<getDeletedMethodName>", Me.getDeletedMethodName)
        stmpl = stmpl.Replace("<LoadAssociationMethodName>", Me.getLoadAssociationMethodName)
        stmpl = stmpl.Replace("<LoadedFlagVariableName>", Me.LoadedFlagVariableName)
        stmpl = stmpl.Replace("<association_name_singular>", Me.associationNameSingular)
        stmpl = stmpl.Replace("<prop_prefix>", ModelGenerator.Current.FieldPropertyPrefix)
        stmpl = stmpl.Replace("<association_name>", Me.associationName)
        stmpl = stmpl.Replace("<property_name>", Me.PropertyName)
        stmpl = stmpl.Replace("<db_mapper>", _
                Me.GetAssociatedMapperClassName)
        stmpl = stmpl.Replace("<datatype>", Me.DataType)

        stmpl = stmpl.Replace("<parent_field_runtime>", Me.ParentField.PropertyName)
        stmpl = stmpl.Replace("<child_field_runtime>", Me.ChildField.PropertyName)
        stmpl = stmpl.Replace("<child_field_runtime_as_integer>", _
                              "CInt(this." & Me.ChildField.PropertyName & ")")

        stmpl = stmpl.Replace("<child_field>", Me.ChildFieldName)
        stmpl = stmpl.Replace("<parent_field>", Me.ParentFieldName)
        stmpl = stmpl.Replace("<implements>", String.Empty)
        stmpl = stmpl.Replace("<iface>", String.Empty)
        sb.Append(stmpl)

        Return sb.ToString()

    End Function


    Public Property IsReadOnly() As Boolean 'Implements IAssociation.IsReadOnly

    Public Property ChildFieldName() As String 'Implements IAssociation.ChildFieldName

    Public Property ParentFieldName() As String 'Implements IAssociation.ParentFieldName

    Public Property RelationType() As String 'Implements IAssociation.RelationType
        Get
            If Not (Me._relationType.Equals(STR_RELATION_PARENT)) Then
                Return STR_RELATION_CLIENT
            Else
                Return STR_RELATION_PARENT
            End If
        End Get
        Set(ByVal value As String)
            If Not (value.Equals(STR_RELATION_PARENT)) Then
                Me._relationType = STR_RELATION_CLIENT
            Else
                Me._relationType = STR_RELATION_PARENT
            End If
        End Set
    End Property
    Public Property isSortAsc() As Boolean
    Public Property SortField() As String

    Public ReadOnly Property templateText() As String
        Get
            If Me._relationType = STR_RELATION_CLIENT Then
                If Me.isCardinalityMany Then
                    Return ChildManyTemplate
                Else
                    Return ChildOneTemplate
                End If
            Else
                Return ParentTemplate
            End If
        End Get

    End Property

    Public Property ChildManyTemplate() As String
        Get
            If _childManyTemplate Is Nothing Then
                If ModelGenerator.Current.dotNetLanguage = ModelGenerator.enumLanguage.VB Then
                    _childManyTemplate = Utilities.getResourceFileText(GetType(Association), "org.codegen.lib.associationChildMany.txt")
                Else
                    _childManyTemplate = Utilities.getResourceFileText(GetType(Association), "org.codegen.lib.associationChildManyCSharp.txt")
                End If
            End If
            Return _childManyTemplate
        End Get
        Set(ByVal value As String)
            _childManyTemplate = value
        End Set
    End Property

    Public Property ChildOneTemplate() As String
        Get
            If _childOneTemplate Is Nothing Then
                If ModelGenerator.Current.dotNetLanguage = ModelGenerator.enumLanguage.VB Then
                    _childOneTemplate = Utilities.getResourceFileText(GetType(Association), "org.codegen.lib.associationChildOne.txt")
                Else
                    _childOneTemplate = Utilities.getResourceFileText(GetType(Association), "org.codegen.lib.associationChildOneCSharp.txt")
                End If

            End If
            Return _childOneTemplate
        End Get
        Set(ByVal value As String)
            _childOneTemplate = value
        End Set
    End Property

    Public Property ParentTemplate() As String
        Get
            If _parentTemplate Is Nothing Then
                If ModelGenerator.Current.dotNetLanguage = ModelGenerator.enumLanguage.VB Then
                    _parentTemplate = Utilities.getResourceFileText(GetType(Association), "org.codegen.lib.associationParent.txt")
                Else
                    _parentTemplate = Utilities.getResourceFileText(GetType(Association), "org.codegen.lib.associationParentCSharp.txt")
                End If


            End If
            Return _parentTemplate
        End Get
        Set(ByVal value As String)
            _parentTemplate = value
        End Set
    End Property

    Public Function isCardinalityMany() As Boolean 'Implements IAssociation.isCardinalityMany
        Return Not String.IsNullOrEmpty(Me._cardinality) AndAlso Me._cardinality = "*"
    End Function

    Public Function getLoadAssociationMethodName() As String
        Return "Load" & Me.PropertyName
    End Function

End Class
