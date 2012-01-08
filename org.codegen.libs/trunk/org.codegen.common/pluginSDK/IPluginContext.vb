Imports System.Configuration
Imports System.Xml
Imports System.Collections
Imports System.Collections.Generic

Namespace Plugins
    ''' <summary>
    ''' A public interface used to pass context to plugins
    ''' </summary>
    Public Interface IPluginContext

        Property CurrentDocumentText() As String

    End Interface
End Namespace
