Imports System.Collections.Generic
Imports org.codegen.lib.codeGen.Tokens

Namespace org.codegen.lib.codeGen.Tokens
    Public Class LoadToObject
        Inherits ReplacementToken

        Sub New()
            Me.StringToReplace = "LOAD_TO_OBJECT"
        End Sub

        Public Overrides Function getReplacementCode(ByVal t As IObjectToGenerate) As String

            Dim sb As System.Text.StringBuilder = New System.Text.StringBuilder()
            Dim vec As List(Of IDBField) = t.DbTable.Fields(). _
                         Values.ToList.FindAll(Function(p)
                                                   Return p.IsTableField = True _
                                                       AndAlso p.isPrimaryKey = False _
                                                       AndAlso p.isAuditField = False
                                               End Function)


            For Each field As DBField In vec


                Dim fldName As String = DBTable.getRuntimeName(field.FieldName())

                If field.isLookup() Then
                    sb.Append(vbTab & "mo.").Append(fldName).Append("= Me.") _
                                   .Append(fldName)

                    If field.isDecimal Then
                        sb.Append(".DecValue")

                    ElseIf field.isInteger Then
                        sb.Append(".IntValue")

                    ElseIf field.isString Then
                        sb.Append(".StrValue")

                    End If

                    sb.Append(vbCrLf)
                Else


                    sb.Append(vbTab & "mo.set").Append(fldName).Append("(Me.") _
                                   .Append(fldName).Append(".text)")
                    sb.Append(vbCrLf)
                End If


            Next

            Return sb.ToString

        End Function

    End Class
End Namespace
