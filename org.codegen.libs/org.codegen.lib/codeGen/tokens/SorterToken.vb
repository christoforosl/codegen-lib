Imports System.Collections.Generic

Namespace Tokens

    Public Class SorterToken
        Inherits MultiLingualReplacementToken

        Sub New()
            Me.StringToReplace = "DEFAULT_SORTER"
        End Sub

        Public Overrides Function getReplacementCodeCSharp(t as IObjectToGenerate) As String
            Dim SortField As String = XMLClassGenerator.getRowValue(t.XMLDefinition, XMLClassGenerator.XML_ATTR_SORT_FIELD, String.Empty)
            Dim SortAsc As Boolean = CBool(XMLClassGenerator.getRowValue(t.XMLDefinition, XMLClassGenerator.XML_ATTR_SORT_ASC, "1"))

            If String.IsNullOrEmpty(SortField) Then Return String.Empty
			Dim sortExpression As String = ModelGenerator.Current.FieldPropertyPrefix

            If t.DbTable.hasFieldName(SortField.ToLower) Then
                Dim sfield = t.DbTable.getFieldByName(SortField.ToLower)
                If sfield.isNullableProperty Then
                    sortExpression = sfield.PropertyName & ".Value"
                Else
                    sortExpression = sfield.PropertyName
                End If

            Else
                sortExpression = SortField
            End If

            Dim ret As String = vbCrLf & vbTab & "public int CompareTo(" & CType(t, ObjectToGenerate).ClassName & " other ) {" & vbCrLf & vbTab & "// generated sort" & vbCrLf

            Dim sortAscDesc As String = CStr(IIf(SortAsc = False, "-1 * ", ""))
            ret += vbTab & vbTab & "return " & sortAscDesc & " this." & _
                sortExpression & ".CompareTo(other." & sortExpression & ");" & vbCrLf

            ret += vbTab & "}" & vbCrLf
            Return ret
        End Function

        Public Overrides Function getReplacementCodeVb(ByVal t As IObjectToGenerate) As String

            Dim SortField As String = XMLClassGenerator.getRowValue(t.XMLDefinition, XMLClassGenerator.XML_ATTR_SORT_FIELD, String.Empty)
            Dim SortAsc As Boolean = CBool(XMLClassGenerator.getRowValue(t.XMLDefinition, XMLClassGenerator.XML_ATTR_SORT_ASC, "1"))

            If String.IsNullOrEmpty(SortField) Then Return String.Empty
			Dim sortExpression As String

            If t.DbTable.hasFieldName(SortField.ToLower) Then
                Dim sfield = t.DbTable.getFieldByName(SortField.ToLower)
                If t.DbTable.Fields(SortField.ToLower).isNullableProperty Then
                    sortExpression = sfield.PropertyName & ".Value"
                Else
                    sortExpression = sfield.PropertyName
                End If

            Else
                sortExpression = SortField
            End If

            Dim ret As String = vbCrLf & vbTab & "Public Function CompareTo(ByVal other As " & CType(t, ObjectToGenerate).ClassName & ") As Integer _" & vbCrLf & _
                               vbTab & vbTab & " Implements System.IComparable(Of " & CType(t, ObjectToGenerate).ClassName & ").CompareTo" & vbCrLf & vbCrLf

            Dim sortAscDesc As String = CStr(IIf(SortAsc = False, "-1 * ", ""))
            ret += vbTab & vbTab & "Return " & sortAscDesc & " Me." & _
             sortExpression & ".CompareTo(other." & sortExpression & ")" & vbCrLf

            ret += vbTab & "End Function" & vbCrLf
            Return ret
        End Function
    End Class

End Namespace