Imports Microsoft.VisualBasic
Imports System.Text
Imports org.codegen.lib.codeGen.FileComponents

Public Class CSharpAssociation
    Implements IAssociation

    Private Const STR_RELATION_PARENT As String = "PARENT"
    Private Const STR_RELATION_CLIENT As String = "CLIENT"

    Public Property ParentDatatype As String Implements IAssociation.ParentDatatype
    Public Property ChildDatatype As String Implements IAssociation.ChildDatatype
    Public Property DataType As String Implements IAssociation.DataType

    Private _dbMapperClass As String
    Private _relationType As String = "CHILD"
    Private _cardinality As String = ""
    Private _sortAsc As Boolean = True

    Private Shared _childManyTemplate As String
    Private Shared _childOneTemplate As String
    Private Shared _parentTemplate As String

    Public Property parentDBTable As IDBTable Implements IAssociation.ParentDBTable
    Public Property AccessLevel() As String = "public"

    Public Property associationNameSingular As String Implements IAssociation.associationNameSingular
    Public Overridable Property associationName() As String Implements IAssociation.associationName
    Public Property ChildFieldIsPK() As Boolean Implements IAssociation.ChildFieldIsPK

    Public Sub setCardinality(ByVal val As String) Implements IAssociation.setCardinality
        If (Not String.IsNullOrEmpty(val) AndAlso (val = "*" OrElse val = "1")) Then
            Me._cardinality = val
        Else
            Throw New System.ArgumentException("cardinality must be 1 or *")
        End If

    End Sub



    ''' <summary>
    ''' declartation of this in the properties of the interface
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getInterfaceDeclaration() As String Implements IAssociation.getInterfaceDeclaration
        Dim sbdr As StringBuilder = New StringBuilder()

        If Me.isCardinalityMany Then
            sbdr.Append("IEnumerable< " & Me.DataType & ">"). _
               Append(Me.associationName & " {get; set;}")

            sbdr.Append(vbCrLf)
            sbdr.Append(vbTab & vbTab & "void Add").Append(Me.associationNameSingular).Append("(").Append(Me.DataType).Append(" val);").Append(vbCrLf)
            sbdr.Append(vbTab & vbTab & "void Remove").Append(Me.associationNameSingular).Append("("). _
                                Append(Me.DataType).Append(" val);").Append(vbCrLf)

            sbdr.Append(vbTab & vbTab & "IEnumerable<").Append(Me.DataType).Append(">") _
                        .Append("getDeleted").Append(Me.associationName).Append("();").Append(vbCrLf)

            sbdr.Append(vbTab & vbTab & Me.DataType & " get").Append(Me.associationNameSingular).Append("( int i ) "). _
                                Append(";").Append(vbCrLf)
            '
        Else
            sbdr.Append(Me.getDataTypeVariable).Append(" ").Append(Me.associationName).Append(" {get;set;} //association")
        End If
        Return sbdr.ToString
    End Function

    Public Overridable Function getDataTypeVariable() As String Implements IAssociation.getDataTypeVariable

        If Me.isCardinalityMany Then
            Return "List< " & Me.DataType & ">"
        Else
            Return Me.DataType
        End If

    End Function

    Public Overridable Function getTestCode() As String Implements IAssociation.getTestCode

        Dim ret As String = "Assert."
        If Me._cardinality.Equals("*") Then
            ret &= "isTrue(p." & Me.getGet & " isNot Nothing)"

        Else
            ret &= "isTrue(p." & Me.getGet & " isNot Nothing)"
        End If

        Return ret

    End Function
    Public Overridable Function getVariableName() As String Implements IAssociation.getVariableName
        Return Me.associationName
    End Function
    Public Overridable Function getVariable() As String Implements IAssociation.getVariable

        Dim sb As System.Text.StringBuilder = New System.Text.StringBuilder() ' TODO type initialisation here
        sb.Append(vbTab & "private ")
        sb.Append(Me.getDataTypeVariable())
        sb.Append(" _")
        sb.Append(Me.getVariableName)
        sb.Append(" = null;  // initialize to nothing, for lazy load logic below !!!")
        sb.Append(vbCrLf)
        Return sb.ToString()
    End Function

    Public Overridable Function getDeletedVariable() As String Implements IAssociation.getDeletedVariable

        If Me.isCardinalityMany Then
            Dim sb As System.Text.StringBuilder = New System.Text.StringBuilder() ' TODO type initialisation here

            sb.Append(vbTab & " private List< " & Me.DataType & ">").Append(" _deleted").Append(Me.associationName)
            sb.Append(" = new ").Append("List< " & Me.DataType & ">();").Append("// initialize to empty list !!!")
            sb.Append(vbCrLf)
            Return sb.ToString()
        Else
            Return String.Empty
        End If

    End Function

    Public Overridable Function getSaveParentCode(ByVal parentMoObjType As String) As String Implements IAssociation.getSaveParentCode
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
                ret += vbTab + vbTab & "//***Readonly Parent Association:" & Me.associationName.ToLower() & " ***!" & vbCrLf
            Else

                Dim mapperClassName As String = GetAssociatedMapperClassName()
                Dim mappervar As String = Me.associationName.ToLower() & "Mapper"
                ret += vbTab + vbTab & "//*** Parent Association:" & Me.associationName.ToLower() & vbCrLf
                ret += vbTab + vbTab & "if (thisMo." & Me.getGet() & "Loaded && thisMo." & Me.getGet() & ".NeedsSave) {" & vbCrLf
                ret += vbTab + vbTab + vbTab + mapperClassName & " mappervar = new " & mapperClassName & "(this.dbConn);" & vbCrLf
                ret += vbTab + vbTab + vbTab + "mappervar.save(thisMo." & Me.getGet() & ");" & vbCrLf
                ret += vbTab + vbTab & vbTab + "thisMo." & DBTable.getRuntimeName(Me.ChildFieldName()) & " = thisMo." & Me.getGet() & "." & DBTable.getRuntimeName(Me.ParentFieldName()) & ";" & vbCrLf
                ret += vbTab + vbTab & "}" & vbCrLf
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
    Private Function GetAssociatedMapperClassName() As String

        Dim mapperClassName As String = ModelGenerator.Current.getObjectOfDataType(Me.DataType).FullyQualifiedMapperClassName
        Return mapperClassName

    End Function

    Public Overridable Function getSaveChildrenCode() As String Implements IAssociation.getSaveChildrenCode

        Dim ret As String = ""
        If isParent() Then
            ' parent relationship!
            ' need to update self when parent is 
            ' updated. Example: Employer->Address
            ' address is saved first, employer second.

        Else
            If Me.IsReadOnly Then
                ret += vbTab + vbTab & "//***Readonly Child Association:" & Me.associationName.ToLower() & " ***!" & vbCrLf
            Else

                Dim mapperClassName As String = GetAssociatedMapperClassName()
                Dim mappervar As String = Me.associationName.ToLower() & "Mapper"
                ret = vbTab + vbTab & "//***Child Association:" & Me.associationName.ToLower() & vbCrLf
                ret &= vbTab & vbTab & "if (ret." & Me.associationName & "Loaded) {" & vbCrLf
                ret &= vbTab & vbTab & vbTab & mapperClassName & " " & mappervar & _
                                " = new " & mapperClassName & "(this.dbConn);" & vbCrLf

                If Me.isCardinalityMany Then
                    ret &= vbTab & vbTab & vbTab & mappervar & ".saveList(ret." & Me.getGet() & ");" & vbCrLf
                    ret &= vbTab & vbTab & vbTab & mappervar & ".deleteList(ret.getDeleted" & Me.associationName() & "());" & vbCrLf
                Else
                    ret &= vbTab & vbTab & vbTab & mappervar & ".save(ret." & Me.getGet() & ");" & vbCrLf
                End If
                ret &= vbTab & vbTab & "}"
                ret &= vbCrLf

            End If

        End If

        Return ret

    End Function

    Public Overridable Function isParent() As Boolean Implements IAssociation.isParent
        Return Me.RelationType().Equals(STR_RELATION_PARENT)
    End Function

    Public Overridable Function getCanonicalName() As String Implements IAssociation.getCanonicalName

        Return Me.associationName.Substring(0, 1).ToUpper() + Me.associationName.Substring(1)

    End Function

    Public Overridable Function getSet() As String Implements IAssociation.getSet
        Return "" & getCanonicalName()
    End Function


    Public Overridable Function getGet() As String Implements IAssociation.getGet
        Return "" & getCanonicalName()
    End Function

    Public Property PropertiesImplementInterface() As String Implements IAssociation.PropertiesImplementInterface

    Public Overridable Function getSetterGetter() As String Implements IAssociation.getSetterGetter

        Dim sb As System.Text.StringBuilder = New System.Text.StringBuilder()
        Dim fieldName As String = Me.associationName
        Dim PropertyInterface As String = DirectCast( _
                ModelGenerator.Current.CurrentObjectBeingGenerated.FileGroup(ModelObjectFileComponent.KEY),  _
                DotNetClassFileComponent).ClassInterface

        If Me.isCardinalityMany And Me.RelationType = STR_RELATION_PARENT Then
            Throw New ApplicationException("PARENT relationship with cardinality ""MANY"" not allowed!")
        End If


        Dim stmpl As String = templateText

        stmpl = stmpl.Replace("<sort>", CStr(IIf(String.IsNullOrEmpty(Me.SortField) = False, _
                                                 "this._<association_name>.Sort()", "")))

        stmpl = stmpl.Replace("<association_name_singular>", Me.associationNameSingular)
        stmpl = stmpl.Replace("<association_name>", fieldName)
        stmpl = stmpl.Replace("<db_mapper>", _
                Me.GetAssociatedMapperClassName)
        stmpl = stmpl.Replace("<datatype>", Me.DataType)
        stmpl = stmpl.Replace("<parent_field_runtime>", DBTable.getRuntimeName(Me.ParentFieldName))
        stmpl = stmpl.Replace("<child_field_runtime>", DBTable.getRuntimeName(Me.ChildFieldName))
        stmpl = stmpl.Replace("<child_field_runtime_as_integer>", _
                              "CInt(this." & DBTable.getRuntimeName(Me.ChildFieldName) & ")")

        stmpl = stmpl.Replace("<child_field>", Me.ChildFieldName)
        stmpl = stmpl.Replace("<parent_field>", Me.ParentFieldName)
        stmpl = stmpl.Replace("<implements>", PropertyInterface)
        stmpl = stmpl.Replace("<iface>", PropertyInterface)
        sb.Append(stmpl)

        Return sb.ToString()

    End Function

    Public Property IsReadOnly() As Boolean Implements IAssociation.IsReadOnly

    Public Property ChildFieldName() As String Implements IAssociation.ChildFieldName

    Public Property ParentFieldName() As String Implements IAssociation.ParentFieldName

    Public Property RelationType() As String Implements IAssociation.RelationType
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
    Public Property isSortAsc() As Boolean Implements IAssociation.isSortAsc
    Public Property SortField() As String Implements IAssociation.SortField

    Public ReadOnly Property templateText() As String Implements IAssociation.templateText
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

    Public Shared Property ChildManyTemplate() As String
        Get
            If _childManyTemplate Is Nothing Then
                If ModelGenerator.Current.dotNetLanguage = ModelGenerator.enumLanguage.VB Then
                    _childManyTemplate = Utilities.getResourceFileText("org.codegen.lib.codeGen.associationChildMany.txt")
                Else
                    _childManyTemplate = Utilities.getResourceFileText("org.codegen.lib.codeGen.associationChildManyCSharp.txt")
                End If
            End If

            Return _childManyTemplate
        End Get
        Set(ByVal value As String)
            _childManyTemplate = value
        End Set
    End Property
    Public Shared Property ChildOneTemplate() As String
        Get
            If _childOneTemplate Is Nothing Then
                If ModelGenerator.Current.dotNetLanguage = ModelGenerator.enumLanguage.VB Then
                    _childOneTemplate = Utilities.getResourceFileText("org.codegen.lib.codeGen.associationChildOne.txt")
                Else
                    _childOneTemplate = Utilities.getResourceFileText("org.codegen.lib.codeGen.associationChildOneCSharp.txt")
                End If

            End If
                Return _childOneTemplate
        End Get
        Set(ByVal value As String)
            _childOneTemplate = value
        End Set
    End Property
    Public Shared Property ParentTemplate() As String
        Get
            If _parentTemplate Is Nothing Then
                If ModelGenerator.Current.dotNetLanguage = ModelGenerator.enumLanguage.VB Then
                    _parentTemplate = Utilities.getResourceFileText("org.codegen.lib.codeGen.associationParent.txt")
                Else
                    _parentTemplate = Utilities.getResourceFileText("org.codegen.lib.codeGen.associationParentCSharp.txt")
                End If


            End If
                Return _parentTemplate
        End Get
        Set(ByVal value As String)
            _parentTemplate = value
        End Set
    End Property

    Public Function isCardinalityMany() As Boolean Implements dotnet.IAssociation.isCardinalityMany
        Return Not String.IsNullOrEmpty(Me._cardinality) AndAlso Me._cardinality = "*"
    End Function
End Class
