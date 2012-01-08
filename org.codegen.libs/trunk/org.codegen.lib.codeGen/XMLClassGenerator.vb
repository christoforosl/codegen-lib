Imports System.Collections.Generic
Imports org.codegen.lib.codeGen
Imports System.Reflection
Imports System.IO
Imports System.Xml


Public Class XMLClassGenerator

#Region "Constants"

    Public Const XML_ATTR_TABLE As String = "table"
    Public Const XML_TABLE_ATTR_TABLE_NAME As String = "TableName"
    Public Const XML_TABLE_ATTR_DBMAPPER_NAMESPACE As String = "DBMapperNameSpace"
    Public Const XML_TABLE_ATTR_MOBJ_NAMESPACE As String = "Namespace"
    Public Const XML_TABLE_ATTR_TEST_CLASS_NAMESPACE As String = "TestClassNameSpace"
    Public Const XML_TABLE_ATTR_GENERATE_UI As String = "GenerateUI"
    Public Const XML_TABLE_ATTR_GENERATE_MAPPER As String = "GenerateMapper"
    Public Const XML_ATTR_PROJECT_TEST_OUT_DIR As String = "testProjectOutputDir"
    Public Const XML_ATTR_UI_TEST_OUT_DIR As String = "UIProjectOutputDir"
    Public Const XML_FIELD_ATTR_DATATYPE As String = "DataType"
    Public Const XML_TABLE_ATTR_IS_SINGLETON As String = "isSingleton"
    Public Const XML_ATTR_ASSOCIATION_NAME As String = "AssociationName"
    Public Const XML_ATTR_ASSOCIATION_NAME_SINGULAR As String = "AssociationSingular"

    Public Const XML_ATTR_CARDINALITY As String = "cardinality"
    Public Const XML_ATTR_CLASSNAME As String = "ClassName"

    Public Const XML_TABLE_ATTR_SELECT_OBJ As String = "SelectObject"
    Public Const XML_TABLE_ATTR_PKFIELD As String = "pkfield"
    Public Const XML_TABLE_ATTR_READONLY As String = "isReadOnly"
    Public Const XML_ATTR_REL_TYPE As String = "relationType"

    Public Const XML_PROJECT_ATTR_DEFAULT_NAMESPACE As String = "defaultNamespace"
    Public Const XML_ATTR_DEFAULT_MAPPER_NAMESPACE As String = "defaultDBMapperNameSpace"
    Public Const XML_PROJECT_ATTR_CONN_STRING As String = "dbConnectionString"
    Public Const XML_PROJECT_ATTR_DB_CONN_DIALECT As String = "dbConnectionDialect"
    Public Const XML_PROJECT_ATTR_DB_CONN_TYPE As String = "dbConnectionType"

    Public Const XML_PROJECT_ATTR_OUTPUT_DIR As String = "outputDir"

    Public Const XML_ATTR_PROJECT As String = "project"

    Public Const XML_ASS_PARENT_FIELD As String = "parentFieldName"
    Public Const XML_ASS_CHILD_FIELD As String = "childFieldName"

    Public Const XML_FIELD_ATTR_FIELDNAME As String = "FieldName"
    Public Const XML_FIELD_ATTR_ACCESS_LEVEL As String = "AccessLevel"

    Public Const XML_ATTR_SORT_FIELD As String = "SortField"
    Public Const XML_ATTR_SORT_ASC As String = "SortAscending"

    Public Const XML_RELATION_ASSOCIATION As String = "table_association"
    Public Const XML_CUSTOMIZED_FIELDS As String = "table_Field"
    Public Const XML_LOOKUP_FIELDS As String = "table_Lookups"
    Public Const XML_ATTR_FIELD As String = "Field"

    Public Const XML_FIELD_ATTR_SERIALIZATION_IGNORE As String = "XMLSerializationIgnore"
    Public Const XML_ATTR_MODEL_EXTRA_CODE As String = "ModelExtraCode"
    Public Const XML_FIELD_ATTR_EXCLUDE As String = "Exclude"
    Public Const XML_ATTR_PROPERTIES_GEN_INTERFACE As String = "PropertiesInterface"
    Public Const XML_ASS_READ_ONLY As String = "IsReadOnly"
    Public Const XML_TABLE_ATTR_MODEL_IMPLEMENTED_INTERFACES As String = "ModelImplementedInterfaces"
    Public Const XML_PROJECT_ATTR_VB_NET_PROJECT_FILE As String = "projectFile"
    Public Const XML_PROJECT_ATTR_IPROP_GEN_CLASS_NAME As String = "IPropertyGeneratorClassName"
    Public Const STR_PUBLIC As String = "Public"
    Private Const STR_False As String = "False"

    Private _xmlConfFile As String
    Private _VbNetProjectFile As String
    Private relativeDirectory As String
#End Region

#Region "Properties"

    Public Sub New(ByVal xmlConfFile As String)

        Me._xmlConfFile = xmlConfFile
        Dim f As New FileInfo(xmlConfFile)
        If f.Exists = False Then
            Throw New ApplicationException("XML Conf file does not exist:" & xmlConfFile)

        End If
        Me.relativeDirectory = f.Directory.FullName

    End Sub


#End Region

    Private Property VbNetProjectFile As String
        Set(value As String)
            Me._VbNetProjectFile = ModelGenerator.resolveRelativePathsAndCheck(value, Me.relativeDirectory)
        End Set
        Get
            Return Me._VbNetProjectFile
        End Get
    End Property

    Public Shared Sub GenerateClassesFromFile(ByVal xmlConfFile As String)

        Dim gen As XMLClassGenerator
        Dim cds As DataSet = Nothing

        gen = New XMLClassGenerator(xmlConfFile)
        cds = New DataSet
        cds.Namespace = "ClassGenerator3"
        cds.ReadXmlSchema(Utilities.getResourceStream("org.codegen.lib.codeGen.classFenerator.xsd"))
        cds.ReadXml(xmlConfFile)
        gen.parseConfFile(cds)
        gen.genClasses()


    End Sub




    Public Sub genClasses()

        ' first run: enumerate thru all tables to setup 
        ' associations
        'ModelGenerator.Current.UpdateAssociationEndsIds = New Dictionary(Of String, String())
        Dim objectCount As Integer = ModelGenerator.Current.ObjectsToGenerate.Values.Count

        If Progress Is Nothing = False Then
            Progress.MaxSteps = objectCount
        End If

        For i As Integer = 0 To objectCount - 1
            Dim t As ObjectToGenerate = ModelGenerator.Current.ObjectsToGenerate.Values(i)
            t.generateCode()

            If Progress Is Nothing = False Then
                Progress.nextStep()
            End If

        Next i

        If File.Exists(Me._VbNetProjectFile) Then

            Dim x As New VBProjectHandler(Me._VbNetProjectFile)
            x.addNewFilesToProject()

        Else
            Throw New ApplicationException("vb.net project " & Me._VbNetProjectFile & " file does not exist")
        End If

    End Sub

    Public Shared Function getRowValue(ByVal r As DataRow, ByVal col As String) As String

        If IsDBNull(r.Item(col)) OrElse r.Item(col) Is Nothing Then
            Return String.Empty
        Else
            Return CStr(r.Item(col))
        End If

    End Function

    Public Shared Function getRowValue(ByVal r As DataRow, ByVal col As String, ByVal required As Boolean) As String

        If r.Table.Columns(col) Is Nothing = False AndAlso r.Item(col) Is DBNull.Value = False Then
            Return CStr(r.Item(col))
        Else
            If required Then
                Throw New ApplicationException("Attribute " & col & " is required")
            Else
                Return String.Empty
            End If
        End If

    End Function

    Public Shared Function getBooleanRowValue(ByVal r As DataRow, _
                ByVal col As String) As Boolean

        Return getRowValue(r, col, STR_False).ToLower = "true" OrElse _
                    getRowValue(r, col, STR_False).ToLower = "1"

    End Function
    Public Shared Function getBooleanRowValue(ByVal r As DataRow, _
               ByVal col As String, ByVal bDefaultIfEmpty As Boolean) As Boolean

        If getRowValue(r, col) = String.Empty Then
            Return bDefaultIfEmpty
        Else

            Return getRowValue(r, col, STR_False).ToLower = "true" OrElse _
                        getRowValue(r, col, STR_False).ToLower = "1"
        End If
    End Function

    Public Shared Function getRowValue(ByVal r As DataRow, ByVal col As String, ByVal defaultVal As String) As String

        If r.Table.Columns(col) Is Nothing = False AndAlso IsDBNull(r.Item(col)) = False Then
            Return CStr(r.Item(col))
        Else
            Return defaultVal
        End If

    End Function

    Private Sub parseAssociations(ByVal ogen As ObjectToGenerate)

        Dim accosiationRows As DataRow() = ogen.XMLDefinition.GetChildRows(XML_RELATION_ASSOCIATION)
        Dim associationCnt As Integer = accosiationRows.Length
        Try


            'associations code
            If associationCnt = 0 Then
                Return

            End If

            For acnt As Integer = 0 To associationCnt - 1
                Dim assRow As DataRow = accosiationRows(acnt)
                Dim associationDatatype As String = getRowValue(assRow, XML_FIELD_ATTR_DATATYPE, True)
                Dim assname As String = getRowValue(assRow, XML_ATTR_ASSOCIATION_NAME, True)
                Dim assnameSingle As String = getRowValue(assRow, XML_ATTR_ASSOCIATION_NAME_SINGULAR, assname)
                Dim cardinality As String = getRowValue(assRow, XML_ATTR_CARDINALITY, "*")
                Dim relType As String = getRowValue(assRow, XML_ATTR_REL_TYPE, "CHILD")

                Dim parentTableLinkField As String = getRowValue(assRow, XML_ASS_PARENT_FIELD, True)
                Dim childTableLinkField As String = getRowValue(assRow, XML_ASS_CHILD_FIELD, parentTableLinkField)
                Dim accLevel As String = getRowValue(assRow, XML_FIELD_ATTR_ACCESS_LEVEL, STR_PUBLIC)

                Dim n As New Association()
                n.DataType = associationDatatype
                n.associationName = assname
                n.associationNameSingular = assnameSingle
                n.setCardinality(cardinality)
                n.RelationType = relType
                n.ParentFieldName = parentTableLinkField
                n.ChildFieldName = childTableLinkField
                n.AccessLevel = accLevel
                n.IsReadOnly = CBool(getRowValue(assRow, XML_ASS_READ_ONLY, "0"))
                n.isSortAsc = CBool(getRowValue(assRow, XML_ATTR_SORT_ASC, "1"))
                n.SortField = getRowValue(assRow, XML_ATTR_SORT_FIELD, "")

                If n.isParent Then
                    n.ParentDatatype = associationDatatype
                    n.ChildDatatype = ogen.FullyQualifiedClassName

                Else
                    n.ParentDatatype = ogen.FullyQualifiedClassName
                    n.ChildDatatype = associationDatatype

                End If

                ogen.DbTable.addAssociation(n)
                ModelGenerator.Current.addAssociation(n)

            Next acnt
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Public Sub parseConfFile(ByVal cds As DataSet)

        Dim projectInfo As DataTable = cds.Tables(XMLClassGenerator.XML_ATTR_PROJECT)
        Dim totalTables As Integer = cds.Tables(XML_ATTR_TABLE).Rows.Count
        Dim fieldDt As DataTable = cds.Tables(XML_ATTR_FIELD)

        Dim generatorVersion As Integer = CInt(getRowValue(projectInfo.Rows(0), "GeneratorVersion", True))
        Dim defaultNamespace As String = getRowValue(projectInfo.Rows(0), XML_PROJECT_ATTR_DEFAULT_NAMESPACE)

        Dim propertyGeneratorClassName As String = getRowValue(projectInfo.Rows(0), XML_PROJECT_ATTR_IPROP_GEN_CLASS_NAME, False)

        Me.VbNetProjectFile = getRowValue(projectInfo.Rows(0), XML_PROJECT_ATTR_VB_NET_PROJECT_FILE, "")

        Dim t As ModelGenerator = ModelGenerator.create(CType(generatorVersion, ModelGenerator.enumVERSION))
        t.relativeDirectory = Me.relativeDirectory
        t.XmlFileDataSet = cds
        t.DbConnString = getRowValue(projectInfo.Rows(0), XML_PROJECT_ATTR_CONN_STRING, True)
        t.DbConnStringType = getRowValue(projectInfo.Rows(0), XML_PROJECT_ATTR_DB_CONN_TYPE, True)
        t.DbConnStringDialect = getRowValue(projectInfo.Rows(0), XML_PROJECT_ATTR_DB_CONN_DIALECT, True)

        t.ProjectOutputDirModel = getRowValue(projectInfo.Rows(0), XML_PROJECT_ATTR_OUTPUT_DIR, True)
        t.ProjectOutputDirTest = getRowValue(projectInfo.Rows(0), XML_ATTR_PROJECT_TEST_OUT_DIR, False)
        t.ProjectOutputDirUI = getRowValue(projectInfo.Rows(0), XML_ATTR_UI_TEST_OUT_DIR, False)

        For tblName As Integer = 0 To totalTables - 1

            Dim thisRow As DataRow = cds.Tables(XML_ATTR_TABLE).Rows.Item(tblName)
            Dim table As String = getRowValue(thisRow, XML_TABLE_ATTR_TABLE_NAME, True)

            Dim xmlDefaultMONamespace As String = getRowValue(thisRow, XML_TABLE_ATTR_MOBJ_NAMESPACE, defaultNamespace)

            Dim selectobject As String = getRowValue(thisRow, XML_TABLE_ATTR_SELECT_OBJ, table)
            Dim pkField As String = getRowValue(thisRow, XML_TABLE_ATTR_PKFIELD, False)
            Dim xmlReadonly As String = getRowValue(thisRow, XML_TABLE_ATTR_READONLY, "0")

            Dim ogen As New ObjectToGenerate
            ogen.XMLDefinition = thisRow
            ogen.setTableName(table, pkField)
            ogen.GenerateUI = getBooleanRowValue(thisRow, XML_TABLE_ATTR_GENERATE_UI, False)
            ogen.GenerateMapper = getBooleanRowValue(thisRow, XML_TABLE_ATTR_GENERATE_MAPPER, True)

            If getBooleanRowValue(thisRow, XML_TABLE_ATTR_IS_SINGLETON) Then
                'ogen.DbTable = True
            End If

            If xmlReadonly.Equals("1") Then
                ogen.DbTable().isReadOnly = True
            End If

            ogen.DbTable.SelectObject = selectobject
            ogen.DbTable.ImplementsAsString = getRowValue(thisRow, XML_TABLE_ATTR_MODEL_IMPLEMENTED_INTERFACES, False)
            ogen.loadFileGroups()

            Dim LookupRows As DataRow() = thisRow.GetChildRows(XML_LOOKUP_FIELDS)
            Dim LookupRowsCnt As Integer = LookupRows.Length
            If LookupRowsCnt > 0 Then
                For acnt As Integer = 0 To LookupRowsCnt - 1
                    Dim lookupRow As DataRow = LookupRows(acnt)
                    Dim lk As New FieldLookupInfo
                    Dim fldname As String = getRowValue(lookupRow, "FieldName", True)

                    lk.DataSource = getRowValue(lookupRow, "DataSource", True)
                    lk.DisplayMember = getRowValue(lookupRow, "DisplayMember", True)
                    lk.ValueMember = getRowValue(lookupRow, "ValueMember", True)
                    ogen.DbTable.LookupInfo.Add(fldname, lk)

                Next
            End If

            Me.parseAssociations(ogen)

            Dim fieldRows As DataRow() = thisRow.GetChildRows(XML_CUSTOMIZED_FIELDS)

            Dim fieldsCnt As Integer = fieldRows.Length
            If fieldsCnt > 0 Then
                For acnt As Integer = 0 To fieldsCnt - 1
                    Dim FieldRow As DataRow = fieldRows(acnt)
                    Dim f As New DBField
                    f.FieldName = getRowValue(FieldRow, XML_FIELD_ATTR_FIELDNAME, True)

                    Dim xmltype As String = getRowValue(FieldRow, XML_FIELD_ATTR_DATATYPE, String.Empty)
                    If String.IsNullOrEmpty(xmltype) = False Then
                        If Type.GetType(xmltype) Is Nothing Then
                            Throw New ApplicationException("Unknown System Type:" & xmltype)
                        End If
                        f.UserSpecifiedDataType = Type.GetType(xmltype)
                    End If

                    f.AccessLevel = getRowValue(FieldRow, XML_FIELD_ATTR_ACCESS_LEVEL, String.Empty)
                    f.XMLSerializationIgnore = CBool(getRowValue(FieldRow, XML_FIELD_ATTR_SERIALIZATION_IGNORE, "0"))

                    Dim exclude As Boolean = CBool(getRowValue(FieldRow, XML_FIELD_ATTR_EXCLUDE, "0"))
                    If exclude Then
                        ogen.DbTable.addExludedField(f.FieldName)
                    Else
                        ogen.DbTable.addCustomizedField(f)
                    End If

                Next
            End If

            ' Debug.Print(ogen.ClassName)
            t.addObjectForGeneration(ogen)

        Next tblName

        Return
    End Sub

    Public Property Progress() As IProgressIndicator

End Class
