Imports org.codegen.lib.codeGen.Tokens
Imports System.Collections.Generic

Namespace org.codegen.lib.codeGen.Tokens
    Public Class GetControlDeclarations
        Inherits ReplacementToken

        Sub New()
            Me.StringToReplace = "CONTROLS_DECLARATIONS"
        End Sub

        Public Overrides Function getReplacementCode(ByVal t As IObjectToGenerate) As String

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
                sb.Append(vbTab & "Friend WithEvents ").Append(fldName).Append("lbl As System.Windows.Forms.Label")
                sb.Append(vbCrLf)
                sb.Append(vbTab & "Friend WithEvents ").Append(fldName).Append(" As ").Append(getControlType(field))
                sb.Append(vbCrLf)

                i += 1

            Next

            Return sb.ToString

        End Function

        Public Shared Function getControlType(ByVal field As DBField) As String

            If field.ParentTable.LookupInfo.ContainsKey(field.FieldName.ToUpper) Then
                Return "CGComboBox"
            Else
                If field.isBoolean Then
                    Return "CGCheckBox"

                ElseIf field.isDate Then
                    Return "CGDateTextBox"

                ElseIf field.isInteger Then
                    Return "CGIntTextBox"

                ElseIf field.isDecimal Then
                    Return "CGDecimalTextBox"

                Else
                    Return "CGTextBox"
                End If
            End If

        End Function

    End Class

End Namespace
