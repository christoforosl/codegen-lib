Imports System.Collections.Generic
Imports org.codegen.lib.codeGen.Tokens

Namespace org.codegen.lib.codeGen.Tokens

    Public Class ControlsLoadCode
        Inherits ReplacementToken

        Sub New()
            Me.StringToReplace = "CONTROLS_LOAD_CODE"
        End Sub

        Public Overrides Function getReplacementCode(ByVal t As dotnet.IObjectToGenerate) As String
            Dim sb As System.Text.StringBuilder = New System.Text.StringBuilder()
            Dim vec As List(Of IDBField) = t.DbTable.Fields(). _
                        Values.ToList.FindAll(Function(p)
                                                  Return p.IsTableField = True _
                                                      AndAlso p.isPrimaryKey = False _
                                                      AndAlso p.isAuditField = False
                                              End Function)


            For Each field As DBField In vec

                Dim fldName As String = DBTable.getRuntimeName(field.FieldName())
                If field.isLookup Then
                    Dim lk As FieldLookupInfo = t.DbTable.LookupInfo.Item(fldName)
                    Dim ds As String = "new " & GetAssociatedMapperClassName(lk.DataSource) & "().findAll()"
                    
                    sb.Append(vbTab & "Me.").Append(fldName).Append(".DataSource = ").Append(ds).Append(vbCrLf)
                    sb.Append(vbTab & "Me.").Append(fldName).Append(".DisplayMember = """) _
                            .Append(lk.DisplayMember).Append("""").Append(vbCrLf)

                    sb.Append(vbTab & "Me.").Append(fldName).Append(".ValueMember = """) _
                                .Append(lk.ValueMember).Append("""").Append(vbCrLf)
                End If

				
            Next

            Return sb.ToString
        End Function

        Public Shared Function GetAssociatedMapperClassName(ByVal DataType As String) As String

            Dim mapperClassName As String = ModelGenerator.Current.getObjectOfDataType(DataType).FullyQualifiedMapperClassName
            Return mapperClassName

        End Function

    End Class
End Namespace
