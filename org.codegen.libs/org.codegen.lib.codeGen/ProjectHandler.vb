Imports System.Collections.Generic
Imports org.codegen.lib.codeGen
Imports System.Reflection
Imports System.IO
Imports System.Xml
Imports org.codegen.lib.codeGen.FileComponents
Imports System.Text


Public Interface IProjectHandler

    Sub addFilesToProject()

End Interface

Public Class VBProjectHandler
    Implements IProjectHandler

    Private Const STR_EMBEDDED_RESOURCE As String = "EmbeddedResource"
    Private Const STR_INCLUDE As String = "Include"
    Private Const STR_COMPILE As String = "Compile"
    Private Const STR_DEPEND_UPON As String = "DependentUpon"
    Private Const STR_GENERATED_VB As String = "Generated.vb"
    Private Const STR_VB As String = ".vb"
    Private Const STR_BASE_VB As String = "Base.vb"
    Private Const STR_SQL_XML As String = "Sql.xml"

    Private VbNetProjectFile As String
    Private xmlDoc As XmlDocument

    Dim _firstEmbeddedResourceNode As XmlNode = Nothing
    Dim _firstCompileNode As XmlNode = Nothing


    Public Sub New(ByVal inVbNetProject As String)

        Me.VbNetProjectFile = inVbNetProject

    End Sub

    Public Function compileNodeExists(ByVal sFile As String) As Boolean
        Return findNode(sFile, STR_COMPILE, STR_INCLUDE) IsNot Nothing
    End Function
    Public Function embeddedResourceExists(ByVal sFile As String) As Boolean
        Return findNode(sFile, STR_EMBEDDED_RESOURCE, STR_INCLUDE) IsNot Nothing
    End Function

    Public Function findCompileNode(ByVal sFile As String) As XmlNode
        Return findNode(sFile, STR_COMPILE, STR_INCLUDE)
    End Function

    Public Function findResourceNode(ByVal sFile As String) As XmlNode
        Return findNode(sFile, STR_EMBEDDED_RESOURCE, STR_INCLUDE)
    End Function

    Public Function findDependsOnNode(ByVal parentNode As XmlNode, ByVal sFile As String) As XmlNode

        For Each x As XmlNode In parentNode.ChildNodes

            If x.NodeType = XmlNodeType.Element _
                        AndAlso x.Name = STR_DEPEND_UPON _
                        AndAlso x.InnerText = sFile Then

                Return x

            End If

        Next
        Return Nothing

    End Function

    ''' <summary>
    ''' Searhes and finds a node 
    ''' </summary>
    ''' <param name="searchString">String to find</param>
    ''' <param name="tagName">The tag name of the nodes to search in</param>
    ''' <param name="attribName">The atribute name whose value will search for a match of search string</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function findNode(ByVal searchString As String, _
                             ByVal tagName As String, _
                             ByVal attribName As String) As XmlNode

        Dim dtNodes As XmlNodeList = xmlDoc.GetElementsByTagName(tagName)
        For Each xNode As XmlNode In dtNodes
            Dim lNAttribute As XmlAttribute = xNode.Attributes(attribName)
            If lNAttribute IsNot Nothing Then
                If lNAttribute.InnerText.ToLower = searchString.ToLower Then
                    Return xNode
                End If
            End If
        Next

        Return Nothing

    End Function

    Private Function GetNewElement(ByVal newFileShortPath As String, ByVal elementName As String, ByVal uri As String) As XmlNode

        'Dim newNode As XmlNode = xmlDoc.CreateNode(XmlNodeType.Element, elementName, String.Empty)
        Dim newnode As XmlElement = xmlDoc.CreateElement(elementName, uri)
        Dim xa As XmlAttribute = xmlDoc.CreateAttribute(STR_INCLUDE)
        xa.Value = newFileShortPath
        newnode.Attributes.Append(xa)
        Return newnode

    End Function


    Private Sub addDependsOnNode(ByVal newFile As String, _
                                 ByVal newNode As XmlNode, _
                                 ByVal namespaceURI As String)

        Dim depNode As XmlNode = xmlDoc.CreateNode(XmlNodeType.Element, STR_DEPEND_UPON, namespaceURI)
        Dim f As FileInfo = New FileInfo(newFile)
        depNode.InnerText = f.Name.Replace(STR_BASE_VB, STR_VB)
        newNode.AppendChild(depNode)

    End Sub

    Public Sub addNewFilesToProject() Implements IProjectHandler.addFilesToProject

        Dim hasMod As Boolean
        If File.Exists(Me.VbNetProjectFile) = False Then
            Throw New ApplicationException("vb.net project file does not exist")
        End If

        xmlDoc = New XmlDocument
        xmlDoc.LoadXml(Utilities.TextFromFileGet(Me.VbNetProjectFile))

        If xmlDoc.GetElementsByTagName(STR_COMPILE).Count > 0 Then
            _firstCompileNode = xmlDoc.GetElementsByTagName(STR_COMPILE)(0)
        End If
        If xmlDoc.GetElementsByTagName(STR_EMBEDDED_RESOURCE).Count > 0 Then
            _firstEmbeddedResourceNode = xmlDoc.GetElementsByTagName(STR_EMBEDDED_RESOURCE)(0)
        End If

        If _firstCompileNode Is Nothing Then
            Throw New ApplicationException("vb.net project file must have at least one file to Compile.  Files were generated but will not be added to the project.")
        End If
        If _firstEmbeddedResourceNode Is Nothing Then
            Throw New ApplicationException("vb.net project file must have at least one file as Embedded Resource.  Files were generated but will not be added to the project.")
        End If


        For i As Integer = 0 To ModelGenerator.Current.ObjectsToGenerate.Count - 1
            Dim t As ObjectToGenerate = ModelGenerator.Current.ObjectsToGenerate.Values(i)

            Dim modelObject As IVBProjectIncludedFile = DirectCast(t.FileGroup.Item(ModelObjectFileComponent.KEY), IVBProjectIncludedFile)
            Dim modelObjectBase As IVBProjectIncludedFile = DirectCast(t.FileGroup.Item("MOB"), IVBProjectIncludedFile)
            Dim mapper As IVBProjectIncludedFile = DirectCast(t.FileGroup.Item("Mapper"), IVBProjectIncludedFile)
            Dim sqlFile As IVBProjectIncludedFile = DirectCast(t.FileGroup.Item("SQL"), IVBProjectIncludedFile)

            Dim bfound As Boolean = False

            Dim lCompileNode As XmlNode = Me.findCompileNode(modelObject.FileNameForProject)
            Dim lBaseCompileNode As XmlNode = Me.findCompileNode(modelObjectBase.FileNameForProject)
            Dim lMapperCompileNode As XmlNode = Me.findCompileNode(mapper.FileNameForProject)
            Dim lXmlSQLNode As XmlNode = Me.findResourceNode(sqlFile.FileNameForProject)

            If lXmlSQLNode Is Nothing Then
                lXmlSQLNode = GetNewElement(sqlFile.FileNameForProject, _
                                                              STR_EMBEDDED_RESOURCE, _
                                                              _firstEmbeddedResourceNode.NamespaceURI)
                _firstEmbeddedResourceNode.ParentNode.AppendChild(lXmlSQLNode)
                hasMod = True
            End If

            If lMapperCompileNode Is Nothing Then
                lMapperCompileNode = GetNewElement(mapper.FileNameForProject, _
                                                              STR_COMPILE, _
                                                              _firstCompileNode.NamespaceURI)
                _firstCompileNode.ParentNode.AppendChild(lMapperCompileNode)
                hasMod = True
            End If

            If lCompileNode Is Nothing Then
                lCompileNode = GetNewElement(modelObject.FileNameForProject, _
                                                              STR_COMPILE, _
                                                              _firstCompileNode.NamespaceURI)

                _firstCompileNode.ParentNode.AppendChild(lCompileNode)
                hasMod = True
            End If

            If lBaseCompileNode Is Nothing Then
                lBaseCompileNode = GetNewElement(modelObjectBase.FileNameForProject, _
                                                              STR_COMPILE, _
                                                              _firstCompileNode.NamespaceURI)
                _firstCompileNode.ParentNode.AppendChild(lBaseCompileNode)
                hasMod = True
            End If

            If findDependsOnNode(lBaseCompileNode, modelObject.FileName) Is Nothing Then
                'add depends on
                Me.addDependsOnNode(modelObject.FileName, _
                                        lBaseCompileNode, _
                                        _firstCompileNode.NamespaceURI)

                hasMod = True

            End If

            If findDependsOnNode(lXmlSQLNode, mapper.FileName) Is Nothing Then
                'add depends on
                Me.addDependsOnNode(mapper.FileNameForProject, _
                                        lXmlSQLNode, _
                                        _firstEmbeddedResourceNode.NamespaceURI)

                hasMod = True

            End If

        Next

        If hasMod Then
            xmlDoc.Save(Me.VbNetProjectFile)
        End If

    End Sub



End Class