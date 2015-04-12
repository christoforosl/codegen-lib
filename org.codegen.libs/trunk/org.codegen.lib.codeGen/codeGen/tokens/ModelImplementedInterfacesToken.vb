Imports System.Collections.Generic
Imports org.codegen.lib.codeGen.FileComponents

Namespace Tokens
    Public Class ModelImplementedInterfacesToken
        Inherits ReplacementToken

        Sub New()
            Me.StringToReplace = "implements"
        End Sub

        Public Overrides Function getReplacementCode(ByVal og As IObjectToGenerate) As String

            Dim SortField As String = XMLClassGenerator.getRowValue(og.XMLDefinition, _
                                                    XMLClassGenerator.XML_ATTR_SORT_FIELD, String.Empty)

            If Not String.IsNullOrEmpty(SortField) Then
                If ModelGenerator.Current.dotNetLanguage = ModelGenerator.enumLanguage.VB Then
                    og.DbTable.addImplemetedInterface("System.IComparable(Of " & CType(og, ObjectToGenerate).ClassName & ")")
                Else
                    og.DbTable.addImplemetedInterface("System.IComparable< " & CType(og, ObjectToGenerate).ClassName & ">")
                End If

            End If

                Dim PropertyInterface As String = _
                    DirectCast(og.FileGroup(ModelObjectFileComponent.KEY), DotNetClassFileComponent).ClassInterface

                If Not String.IsNullOrEmpty(PropertyInterface) Then
                    og.DbTable.addImplemetedInterface(PropertyInterface)
                End If

                Return og.DbTable.ImplementsAsString

        End Function
    End Class
End Namespace
