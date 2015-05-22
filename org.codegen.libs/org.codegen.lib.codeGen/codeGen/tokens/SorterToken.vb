Imports System.Collections.Generic

Namespace Tokens

    Public Class SorterToken
        Inherits ReplacementToken

        Sub New()
            Me.StringToReplace = "DEFAULT_SORTER"
        End Sub

        Public Overrides Function getReplacementCode(ByVal t As IObjectToGenerate) As String

            Dim SortField As String = XMLClassGenerator.getRowValue(t.XMLDefinition, XMLClassGenerator.XML_ATTR_SORT_FIELD, String.Empty)
            Dim SortAsc As Boolean = CBool(XMLClassGenerator.getRowValue(t.XMLDefinition, XMLClassGenerator.XML_ATTR_SORT_ASC, "1"))

            If String.IsNullOrEmpty(SortField) Then Return String.Empty
            Dim sortExpression As String = String.Empty

            If t.DbTable.hasFieldName(SortField.ToLower) Then
                If t.DbTable.Fields(SortField.ToLower).isNullableDataType Then
                    sortExpression = SortField & ".Value"
                Else
                    sortExpression = SortField
                End If

            Else
                sortExpression = SortField
            End If

            Dim ret As String = "Public Function CompareTo(ByVal other As " & CType(t, ObjectToGenerate).ClassName & ") As Integer _" & vbCrLf & _
                               vbTab & vbTab & " Implements System.IComparable(Of " & CType(t, ObjectToGenerate).ClassName & ").CompareTo" & vbCrLf & vbCrLf

            Dim sortAscDesc As String = CStr(IIf(SortAsc = False, "-1 * ", ""))
            ret += vbTab & vbTab & "Return " & sortAscDesc & " Me." & _
                      sortExpression & ".CompareTo(other." & sortExpression & ")" & vbCrLf

            ret += vbTab & "End Function" & vbCrLf
            Return ret
        End Function
    End Class

End Namespace