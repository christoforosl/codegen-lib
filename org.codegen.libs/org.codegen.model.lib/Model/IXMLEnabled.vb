Imports System.Runtime.InteropServices

Namespace Model

    Public Interface IXMLEnabled

        Enum enumXMLObjectRelationType
            SELF = 1
            CHILD
            PARENT
        End Enum

        Sub WriteXMLData(ByVal writer As Xml.XmlWriter, _
                         Optional ByVal objectType As IXMLEnabled.enumXMLObjectRelationType = IXMLEnabled.enumXMLObjectRelationType.SELF)


        Function ToXML() As String

    End Interface

End Namespace