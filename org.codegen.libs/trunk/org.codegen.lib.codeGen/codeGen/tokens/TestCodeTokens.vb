Imports System.Collections.Generic

Namespace Tokens

    Public Class TestAssertEqualToken
        Inherits MultiLingualReplacementToken

        Sub New()
            Me.StringToReplace = "ASSERT_EQUAL_FIELDS"
        End Sub

        Public Overrides Function getReplacementCodeCSharp(t As dotnet.IObjectToGenerate) As String
            Dim sb As System.Text.StringBuilder = New System.Text.StringBuilder()
            Dim vec As Dictionary(Of String, IDBField) = t.DbTable.Fields()
            Dim i As Integer = 0

            For Each field As DBField In vec.Values
                If field.isAuditField AndAlso field.RuntimeFieldName.ToLower = "updatedate" Then
                    sb.Append(vbTab + vbTab & _
                              "Assert.IsFalse(p." & field.RuntimeFieldName & _
                              ".GetValueOrDefault() == p2." & field.RuntimeFieldName & ".GetValueOrDefault(),""Expected Field " & field.RuntimeFieldName & " NOT to be equal"");")

                ElseIf field.isAuditField AndAlso field.RuntimeFieldName.ToLower = "updateuser" Then
                    sb.Append(vbTab + vbTab & "// skip update user!")
                Else
                    If field.isNullableDataType Then
                        sb.Append(vbTab + vbTab & "Assert.IsTrue(p." & field.RuntimeFieldName & ".GetValueOrDefault() == p2." & field.RuntimeFieldName & ".GetValueOrDefault(),""Expected Field " & field.RuntimeFieldName & " to be equal"");")
                    Else
                        sb.Append(vbTab + vbTab & "Assert.IsTrue(p." & field.RuntimeFieldName & " == p2." & field.RuntimeFieldName & ",""Expected Field " & field.RuntimeFieldName & " to be equal"");")
                    End If
                End If
                sb.Append(vbCrLf)
                i = i + 1
            Next

            Return sb.ToString()
        End Function
        Public Overrides Function getReplacementCodeVb(ByVal t As IObjectToGenerate) As String

            Dim sb As System.Text.StringBuilder = New System.Text.StringBuilder()
            Dim vec As Dictionary(Of String, IDBField) = t.DbTable.Fields()
            Dim i As Integer = 0

            For Each field As DBField In vec.Values
                If field.isAuditField AndAlso field.RuntimeFieldName.ToLower = "updatedate" Then
                    sb.Append(vbTab + vbTab & _
                              "Assert.IsFalse(p." & field.RuntimeFieldName & _
                              ".GetValueOrDefault = p2." & field.RuntimeFieldName & ".GetValueOrDefault,""Expected Field " & field.RuntimeFieldName & " NOT to be equal"")")

                ElseIf field.isAuditField AndAlso field.RuntimeFieldName.ToLower = "updateuser" Then
                    sb.Append(vbTab + vbTab & "'skip update user!")
                Else
                    If field.isNullableDataType Then
                        sb.Append(vbTab + vbTab & "Assert.IsTrue(p." & field.RuntimeFieldName & ".GetValueOrDefault = p2." & field.RuntimeFieldName & ".GetValueOrDefault,""Expected Field " & field.RuntimeFieldName & " to be equal"")")
                    Else
                        sb.Append(vbTab + vbTab & "Assert.IsTrue(p." & field.RuntimeFieldName & " = p2." & field.RuntimeFieldName & ",""Expected Field " & field.RuntimeFieldName & " to be equal"")")
                    End If
                End If
                sb.Append(vbCrLf)
                i = i + 1
            Next

            Return sb.ToString()
        End Function

    End Class

    Public Class AssertRandomFields
        Inherits ReplacementToken

        Sub New()
            Me.StringToReplace = "TEST_ASSERT_RANDOM_FIELDS"
        End Sub
        Public Overrides Function getReplacementCode(ByVal og As IObjectToGenerate) As String
            Return String.Empty
        End Function
    End Class

    Public Class ModifyRandomFields
        Inherits ReplacementToken

        Sub New()
            Me.StringToReplace = "TEST_CHANGE_RANDOM_FIELDS"
        End Sub
        Public Overrides Function getReplacementCode(ByVal og As IObjectToGenerate) As String
            Return String.Empty
        End Function
    End Class

    Public Class TestAssertAssociationsToken
        Inherits ReplacementToken

        Sub New()
            Me.StringToReplace = "ASSERT_ASSOCIATIONS"
        End Sub

        Public Overrides Function getReplacementCode(ByVal og As IObjectToGenerate) As String

            Dim sb As System.Text.StringBuilder = New System.Text.StringBuilder()

            If og.DbTable.Associations().Count() > 0 Then

                Dim vec As List(Of IAssociation) = og.DbTable.Associations()
                For Each association As IAssociation In vec
                    sb.Append(vbTab & association.getTestCode() & vbCrLf)
                Next

            End If
            Return sb.ToString
        End Function

    End Class

End Namespace