Imports System.Configuration
Imports System.Xml
Imports System.Collections
Imports System.Collections.Generic

Namespace Plugins

    ''' <summary>
    ''' A public interface to be used by all custom plugins
    ''' </summary>
    Public Interface IPlugin

        ReadOnly Property Name() As String
        Sub PerformAction(ByVal context As IPluginContext)

    End Interface

End Namespace
