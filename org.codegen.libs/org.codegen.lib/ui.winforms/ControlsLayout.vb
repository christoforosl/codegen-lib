
Imports org.codegen.lib.Tokens
Imports System.Collections.Generic

Namespace org.codegen.lib.Tokens
    Public Class ControlsLayout
        Inherits ReplacementToken

        Sub New()
            Me.StringToReplace = "CONTROLS_LAYOUT"
        End Sub

        Public Overrides Function getReplacementCode(ByVal t As IObjectToGenerate) As String

            Dim sb As System.Text.StringBuilder = New System.Text.StringBuilder()
            Dim vec As List(Of IDBField) = t.DbTable.Fields(). _
                        Values.ToList.FindAll(Function(p)
                                                  Return p.IsTableField = True _
                                                      AndAlso p.isPrimaryKey = False _
                                                      AndAlso p.isAuditField = False
                                              End Function)


            Dim tabIndex As Integer = 0

            Const LABEL_X As Integer = 5
            Const LABEL_WIDTH As Integer = 120
            Const CONTROL_X As Integer = LABEL_WIDTH + LABEL_X + 10

            'Dim lblWidth As Integer = 2
            Dim ctlY As Integer = 0 'start from 0

            For Each field As DBField In vec
                If Not field.isBinaryField Then
                    Dim fldName As String = DBTable.getRuntimeName(field.FieldName())


                    ctlY = ctlY + 15
                    sb.Append(vbTab & "'").Append(fldName).Append("lbl.").Append(vbCrLf)
                    sb.Append(vbTab & "Me.").Append(fldName).Append("lbl.AutoSize = False" & vbCrLf)
                    sb.Append(vbTab & "Me.").Append(fldName).Append("lbl.Location = "). _
                             Append("New System.Drawing.Point(").Append(LABEL_X).Append(", ").Append(ctlY).Append(")").Append(vbCrLf)

                    sb.Append(vbTab & "Me.").Append(fldName).Append("lbl.Name = """).Append(fldName).Append("lbl""").Append(vbCrLf)
                    sb.Append(vbTab & "Me.").Append(fldName).Append("lbl.Size = New System.Drawing.Size(").Append(LABEL_WIDTH).Append(", 20)").Append(vbCrLf)
                    sb.Append(vbTab & "Me.").Append(fldName).Append("lbl.TabIndex = ").Append(tabIndex).Append(vbCrLf)
                    sb.Append(vbTab & "Me.").Append(fldName).Append("lbl.Text = """). _
                            Append(fldName).Append("""").Append(vbCrLf)

                    sb.Append(vbTab & "Me.").Append(fldName).Append("lbl.TextAlign = System.Drawing.ContentAlignment.MiddleRight").Append(vbCrLf)
                    sb.Append(vbCrLf)
                    tabIndex = +tabIndex

                    sb.Append(vbTab & "'").Append(fldName).Append(vbCrLf)
                    sb.Append(vbTab & "Me.").Append(fldName).Append(".AutoSize = True" & vbCrLf)
                    sb.Append(vbTab & "Me.").Append(fldName).Append(".Location = "). _
                             Append("New System.Drawing.Point(").Append(CONTROL_X).Append(", ").Append(ctlY).Append(")").Append(vbCrLf)

                    sb.Append(vbTab & "Me.").Append(fldName).Append(".Name =""").Append(fldName).Append("""" & vbCrLf)

                    If field.isBoolean = False Then
                        sb.Append(vbTab & "Me.").Append(fldName).Append(".Size = New System.Drawing.Size(200, 20)").Append(vbCrLf)

                        If field.isDecimal Then
                            sb.Append(vbTab & "Me.").Append(fldName).Append(".MaxLength = ") _
                                .Append(CStr(field.Precision)).Append(vbCrLf)
                            sb.Append(vbTab & "Me.").Append(fldName).Append(".FormatPattern = """) _
                                    .Append(New String("0"c, field.Precision - field.Scale)).Append(".") _
                                    .Append(New String("0"c, field.Scale)).Append("""") _
                                    .Append(vbCrLf)
                        Else
                            sb.Append(vbTab & "Me.").Append(fldName).Append(".MaxLength = ").Append(CStr(field.Scale)).Append(vbCrLf)
                        End If

                    End If

                    sb.Append(vbTab & "Me.").Append(fldName).Append(".TabIndex = ").Append(tabIndex).Append(vbCrLf)
                    sb.Append(vbTab & "Me.").Append(fldName).Append(".visible = ").Append(CStr(Not field.isPrimaryKey)).Append(vbCrLf)


                    If field.isDBFieldNullable = False AndAlso field.isBoolean = False Then

                        'sb.Append(vbTab & "Me.").Append(fldName).Append(".BackColor = System.Drawing.Color.LightYellow").Append(vbCrLf)
                        sb.Append(vbTab & "Me.").Append(fldName).Append(".isMandatory = True").Append(vbCrLf)

                    End If

                    sb.Append(vbTab & "Me.").Append(fldName).Append(".AssociatedLabel = me.").Append(fldName).Append("lbl").Append(vbCrLf)

                    sb.Append(vbCrLf)

                    ctlY = ctlY + 15
                    tabIndex += 1


                End If
            Next

            Return sb.ToString

        End Function

    End Class

End Namespace