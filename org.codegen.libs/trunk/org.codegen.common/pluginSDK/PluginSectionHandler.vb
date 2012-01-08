Imports System.Configuration
Imports System.Xml
Imports System.Collections
Imports System.Collections.Generic

Namespace Plugins

    ''' <summary>
    ''' fully qualified name of class: org.codegen.common.Plugins.PluginSectionHandler
    ''' </summary>
    ''' <remarks></remarks>
    Public Class PluginSectionHandler
        Implements IConfigurationSectionHandler

        Public Sub New()
        End Sub

        ' Iterate through all the child nodes
        '   of the XMLNode that was passed in and create instances
        '   of the specified Types by reading the attribite values of the nodes
        '   we use a try/Catch here because some of the nodes
        '   might contain an invalid reference to a plugin type
        Public Function Create(ByVal parent As Object, _
                               ByVal configContext As Object, _
                               ByVal section As System.Xml.XmlNode) As Object _
                                        Implements IConfigurationSectionHandler.Create

            Dim plugins As New Dictionary(Of String, IPlugin)
            'Code goes here to instantiate
            'and invoke the plugins
            For Each node As XmlNode In section.ChildNodes
                Dim pluginName As String = node.Attributes("name").Value
                Dim plugObject As Object = _
                Activator.CreateInstance(Type.GetType(node.Attributes("type").Value))

                'Cast this to an IPlugin interface and add to the collection
                plugins.Add(pluginName, DirectCast(plugObject, IPlugin))

            Next
            Return plugins

        End Function

        Private Shared splugins As Dictionary(Of String, IPlugin)

        Public Shared Function hasPlugin(ByVal pname As String) As Boolean

            If splugins Is Nothing Then
                splugins = CType(System.Configuration.ConfigurationManager.GetSection("Plugins"),  _
                                Dictionary(Of String, IPlugin))
            End If
            If splugins Is Nothing Then Return False
            Return splugins.ContainsKey(pname)

        End Function

        Public Shared Function getPlugin(ByVal pname As String) As IPlugin
            If splugins Is Nothing Then
                splugins = CType(System.Configuration.ConfigurationManager.GetSection("Plugins"),  _
                                Dictionary(Of String, IPlugin))
            End If

            If splugins.ContainsKey(pname) Then
                Return splugins.Item(pname)
            Else
                Return Nothing
            End If

        End Function

    End Class


End Namespace
