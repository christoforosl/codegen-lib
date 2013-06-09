Imports System.Collections.Generic
Imports org.codegen.lib.codeGen
Imports System.Reflection
Imports System.IO
Imports System.Xml

Imports org.codegen.lib.codeGen.Tokens

Namespace FileComponents
    ''' <summary>
    ''' File component responsible for generating ModelObject classes
    ''' </summary>
    ''' <remarks></remarks>
    <ReplacementTokenAttribute(GetType(ClassNameSpaceToken), GetType(GeneratorToken), _
        GetType(CurentDateToken), GetType(ClassAccessLevelToken), _
        GetType(ModelObjectClassNameToken), _
        GetType(IfaceNameToken), GetType(ClassAccessLevelToken))> _
    Public Class ModelObjectFileComponent
        Inherits VBClassFileComponent

        Private Shared _modelObjectDefaultNamespace As String = Nothing

        Public Sub New(ByVal inobjGen As IObjectToGenerate)
            MyBase.New(inobjGen)
        End Sub

        ''' <summary>
        ''' Returns the class name of the Object
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Overrides Function ClassName() As String

            If _ClassName Is Nothing Then

                Dim tmp As String = XMLClassGenerator.getRowValue(Me.objectToGenerate.XMLDefinition, _
                                    XMLClassGenerator.XML_ATTR_CLASSNAME, _
                                     "")

                If String.IsNullOrEmpty(tmp) Then
                    tmp = DBTable.getRuntimeName(Me.objectToGenerate.DbTable.TableName)

                    If tmp.EndsWith("s") Then
                        Throw New ApplicationException("Class names cannot end in ""s"" unless explicitely specified in the classname xml attribute")
                    End If

                End If
                _ClassName = tmp
            End If

            Return _ClassName

        End Function

        Public Overrides Function ClassNamespace() As String
            Return XMLClassGenerator.getRowValue(Me.objectToGenerate.XMLDefinition, _
                               XMLClassGenerator.XML_TABLE_ATTR_MOBJ_NAMESPACE, _
                                                             getDefaultNameSpace())
        End Function


        Public Shared Function getDefaultNameSpace() As String

            If _modelObjectDefaultNamespace Is Nothing Then

                Dim projectInfo As DataTable = ModelGenerator.Current.XmlFileDataSet.Tables( _
                                        XMLClassGenerator.XML_ATTR_PROJECT)

                _modelObjectDefaultNamespace = XMLClassGenerator.getRowValue(projectInfo.Rows(0), _
                                             XMLClassGenerator.XML_PROJECT_ATTR_DEFAULT_NAMESPACE, True)

            End If

            Return _modelObjectDefaultNamespace

        End Function

        Shared Function KEY() As String
            Return "MO"
        End Function

    End Class
End Namespace
