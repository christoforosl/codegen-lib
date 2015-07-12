Imports System.Collections.Generic

Namespace Tokens

    Public Class TestAssertEqualToken
        Inherits ReplacementToken

        Sub New()
            Me.StringToReplace = "ASSERT_EQUAL_FIELDS"
           
        End Sub

        Public Overrides Function getReplacementCode(t As IObjectToGenerate) As String

            Dim sb As System.Text.StringBuilder = New System.Text.StringBuilder()
            Dim vec As Dictionary(Of String, IDBField) = t.DbTable.Fields()
            Dim i As Integer = 0

            For Each field As DBField In vec.Values
                If field.isAuditField AndAlso field.RuntimeFieldName.ToLower = "updatedate" Then
                    sb.Append(vbTab).Append(vbTab).Append(vbTab).Append(vbTab)
                    sb.Append("Assert.IsFalse(p.").Append(field.PropertyName)
                    sb.Append(".GetValueOrDefault() ").Append(strEquals).Append(" p2.").Append(field.PropertyName)
                    sb.Append(".GetValueOrDefault(),""Expected Field ").Append(field.RuntimeFieldName & " NOT to be equal"")").Append(lineEnd)

                ElseIf field.isAuditField AndAlso field.RuntimeFieldName.ToLower = "createdate" Then
                    sb.Append(vbTab).Append(vbTab).Append(vbTab).Append(vbTab)
                    sb.Append("Assert.IsTrue(p.").Append(field.PropertyName).Append(".GetValueOrDefault().ToString(""MM/dd/yy H:mm:ss zzz"") ").Append(strEquals).Append("p2.")
                    sb.Append(field.PropertyName).Append(".GetValueOrDefault().ToString(""MM/dd/yy H:mm:ss zzz""),""Expected Field ")
                    sb.Append(field.RuntimeFieldName).Append(" to be equal"")").Append(lineEnd)

                ElseIf field.isAuditField AndAlso field.RuntimeFieldName.ToLower = "updateuser" Then
                    sb.Append(vbTab).Append(vbTab).Append(vbTab).Append(vbTab).Append(commentMarker).Append("skip update user!")

                ElseIf field.isBinaryField Then
                    sb.Append(vbTab).Append(vbTab).Append(vbTab).Append(vbTab).Append(commentMarker)
                    sb.Append("skip long field:")
                    sb.Append(field.FieldName)
                Else
                    sb.Append(vbTab).Append(vbTab).Append(vbTab).Append(vbTab)
                    If field.isNullableProperty Then
                        sb.Append("Assert.IsTrue(p.").Append(field.PropertyName).Append(".GetValueOrDefault() ").Append(strEquals).Append("p2.")
                        sb.Append(field.PropertyName).Append(".GetValueOrDefault(),""Expected Field ")
                        sb.Append(field.RuntimeFieldName).Append(" to be equal"")").Append(lineEnd)
                    Else
                        sb.Append("Assert.IsTrue(p.").Append(field.PropertyName)
                        sb.Append(strEquals).Append("p2." & field.PropertyName).Append(",""Expected Field ")
                        sb.Append(field.RuntimeFieldName).Append(" to be equal"")").Append(lineEnd)
                    End If
                End If
                sb.Append(vbCrLf)
                i = i + 1
            Next

            Return sb.ToString()
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
                sb.Append(vbCrLf)
                Dim vec As List(Of Association) = og.DbTable.Associations()
                For Each association As Association In vec
                    sb.Append(vbTab & association.getTestCode() & vbCrLf)
                Next

            End If
            Return sb.ToString
        End Function

    End Class

End Namespace