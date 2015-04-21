Imports System.Collections.Generic
Imports org.codegen.lib.Tokens

Namespace org.codegen.lib.Tokens
    Public Class LoadToControls
        Inherits ReplacementToken

        Sub New()
            Me.StringToReplace = "LOAD_TO_CONTROLS"
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

                sb.Append(vbTab & "Me.").Append(fldName).Append(".value").Append(" = mo.").Append(fldName)
                sb.Append(vbCrLf)

                
            Next

            Return sb.ToString

        End Function

    End Class
End Namespace
