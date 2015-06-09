Imports System.Collections.Generic
Imports org.codegen.lib.Tokens

Namespace org.codegen.lib.Tokens

    Public Class ControlsAdd
        Inherits ReplacementToken

        Sub New()
            Me.StringToReplace = "CONTROLS_ADD"
        End Sub

        Public Overrides Function getReplacementCode(ByVal t as IObjectToGenerate) As String

            Dim sb As System.Text.StringBuilder = New System.Text.StringBuilder()
            Dim vec As List(Of IDBField) = t.DbTable.Fields(). _
                        Values.ToList.FindAll(Function(p)
                                                  Return p.IsTableField = True _
                                                      AndAlso p.isPrimaryKey = False _
                                                      AndAlso p.isAuditField = False
                                              End Function)

            For Each field As DBField In vec
                If Not field.isBinaryField Then

                    Dim fldName As String = DBTable.getRuntimeName(field.FieldName())

                    sb.Append(vbTab & "me.Controls.add(me.").Append(fldName).Append("lbl)")
                    sb.Append(vbCrLf)
                    sb.Append(vbTab & "me.Controls.add(me.").Append(fldName).Append(")")
                    sb.Append(vbCrLf)
                End If

            Next

            Return sb.ToString
        End Function
    End Class
End Namespace
