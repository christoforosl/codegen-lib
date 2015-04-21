Imports org.codegen.lib.codeGen.Tokens
Imports System.Collections.Generic

Namespace org.codegen.lib.codeGen.Tokens
    Public Class GetControlInstantiations
        Inherits ReplacementToken

        Sub New()
            Me.StringToReplace = "CONTROLS_INSTANTIATIONS"
        End Sub

        Public Overrides Function getReplacementCode(ByVal t As IObjectToGenerate) As String

            'Me.Label1 = New System.Windows.Forms.Label()
            'Me.TextBox1 = New System.Windows.Forms.TextBox()

            Dim sb As System.Text.StringBuilder = New System.Text.StringBuilder(vbCrLf)
            Dim vec As List(Of IDBField) = t.DbTable.Fields(). _
                        Values.ToList.FindAll(Function(p)
                                                  Return p.IsTableField = True _
                                                      AndAlso p.isPrimaryKey = False _
                                                      AndAlso p.isAuditField = False
                                              End Function)


            Dim i As Integer = 0
            For Each field As DBField In vec
                
                    Dim fldName As String = DBTable.getRuntimeName(field.FieldName())
                    sb.Append(vbTab & "Me.").Append(fldName).Append("lbl = New System.Windows.Forms.Label")
                    sb.Append(vbCrLf)
                    sb.Append(vbTab & "Me.").Append(fldName).Append(" = New "). _
                            Append(GetControlDeclarations.getControlType(field))
                    sb.Append(vbCrLf)

                    i += 1

            Next

            Return sb.ToString()

        End Function
    End Class

End Namespace
