Imports System.Collections.Generic

Namespace Tokens

    Public Class GetAttrStrToken
        Inherits ReplacementToken

        Sub New()
            Me.StringToReplace = "GET_ATTRS_STR"
        End Sub

        Public Overrides Function getReplacementCode(ByVal og As IObjectToGenerate) As String

            Dim sb As System.Text.StringBuilder = New System.Text.StringBuilder(vbCrLf)
            Dim vec As Dictionary(Of String, IDBField) = og.DbTable.Fields()

            Dim i As Integer = 0
            For Each field As DBField In vec.Values

                If (i > 0) Then
                    sb.Append(vbTab + vbTab & "else if ")
                Else
                    sb.Append(vbTab + vbTab & "if ")
                End If

                sb.Append("fieldKey=" & field.getConstantStr() & ".ToLower() Then")
                sb.Append(vbCrLf)
                sb.Append(vbTab + vbTab + vbTab & "return me." & DBTable.getRuntimeName(field.FieldName()) & "" & vbCrLf)
                i += 1

            Next
            sb.Append(vbTab + vbTab + "else" & vbCrLf)
            sb.Append(vbTab + vbTab + vbTab & "return nothing" & vbCrLf)

            sb.Append(vbTab + vbTab + "end If")
            Return sb.ToString()
        End Function
    End Class

    Public Class SetAttrStrToken
        Inherits ReplacementToken

        Sub New()
            Me.StringToReplace = "SET_ATTRS_STR"
        End Sub

        Public Overrides Function getReplacementCode(ByVal og As IObjectToGenerate) As String

            Dim sb As System.Text.StringBuilder = New System.Text.StringBuilder()
            Dim vec As Dictionary(Of String, IDBField) = og.DbTable.Fields()
            Dim i As Integer = 0

            For Each field As DBField In vec.Values
                sb.Append(vbTab + vbTab)

                If (i > 0) Then
                    sb.Append("else if ")
                Else
                    sb.Append("if ")
                End If
                sb.Append(" fieldKey=" & field.getConstantStr() & ".ToLower() Then")

                'sb.Append(" fieldKey=" & field.getConstantStr() & " Then")
                sb.Append(vbCrLf)

                sb.Append(vbTab + vbTab + vbTab & "If Val Is DBNull.Value OrElse Val Is Nothing Then" & vbCrLf)
                sb.Append(vbTab + vbTab + vbTab + vbTab & "Me." & DBTable.getRuntimeName(field.FieldName()) & " = Nothing" & vbCrLf)
                sb.Append(vbTab + vbTab + vbTab & "Else" & vbCrLf)

                sb.Append(vbTab + vbTab + vbTab + vbTab & "Me." & DBTable.getRuntimeName(field.FieldName()) & "=")

                sb.Append("CType(val,").Append(field.FieldDataType).Append(")").Append(vbCrLf)
                

                sb.Append(vbTab + vbTab + vbTab & "End If" & vbCrLf)
                sb.Append(vbTab + vbTab + vbTab & "return" & vbCrLf)

                i = i + 1
            Next

            sb.Append(vbTab + vbTab + "end If")
            Return sb.ToString()

        End Function

        'Friend Shared Function getTypeConverter2(ByVal field As IDBField) As String

        '    If field.UserSpecifiedDataType IsNot Nothing Then
        '        Return LoadFromDataRowToken.getVBTypeConverter(field.UserSpecifiedDataType)
        '    Else
        '        Return LoadFromDataRowToken.getVBTypeConverter(field.RuntimeTypeStr)
        '    End If

        'End Function

    End Class

    Public Class SetAttrToken
        Inherits ReplacementToken

        Sub New()
            Me.StringToReplace = "SET_ATTRS"
        End Sub

        Public Overrides Function getReplacementCode(ByVal og As IObjectToGenerate) As String

            Dim sb As System.Text.StringBuilder = New System.Text.StringBuilder()
            Dim vec As Dictionary(Of String, IDBField) = og.DbTable.Fields()

            sb.Append(vbTab + vbTab + "Select Case fieldKey" & vbCrLf)

            For Each field As DBField In vec.Values

                sb.Append(vbTab + vbTab & "case " & field.getConstant())
                sb.Append(vbCrLf)

                sb.Append(vbTab + vbTab + vbTab & "If Val Is DBNull.Value OrElse Val Is Nothing Then" & vbCrLf)
                sb.Append(vbTab + vbTab + vbTab + vbTab & "Me." & DBTable.getRuntimeName(field.FieldName()) & " = Nothing" & vbCrLf)
                sb.Append(vbTab + vbTab + vbTab & "Else" & vbCrLf)

                sb.Append(vbTab + vbTab + vbTab + vbTab & "Me." & DBTable.getRuntimeName(field.FieldName()) & "=")
                sb.Append("CType(val,").Append(field.FieldDataType).Append(")").Append(vbCrLf)
                
                sb.Append(vbTab + vbTab + vbTab & "End If" & vbCrLf)
                sb.Append(vbTab + vbTab + vbTab & "return" & vbCrLf)


            Next

            sb.Append(vbTab + vbTab + "case else" & vbCrLf)
            sb.Append(vbTab + vbTab + vbTab & "return" & vbCrLf)
            sb.Append(vbTab + vbTab + "end select" & vbCrLf)

            Return sb.ToString()

        End Function
    End Class

    Public Class GetAttrToken
        Inherits ReplacementToken

        Sub New()
            Me.StringToReplace = "GET_ATTRS"
        End Sub

        Public Overrides Function getReplacementCode(ByVal og As IObjectToGenerate) As String

            Dim sb As System.Text.StringBuilder = New System.Text.StringBuilder(vbCrLf)
            Dim vec As Dictionary(Of String, IDBField) = og.DbTable.Fields()

            sb.Append(vbTab + vbTab + "select case fieldKey" & vbCrLf)

            For Each field As DBField In vec.Values

                sb.Append(vbTab + vbTab & "case ")
                sb.Append(field.getConstant())

                sb.Append(vbCrLf)
                sb.Append(vbTab + vbTab + vbTab & "return me." & DBTable.getRuntimeName(field.FieldName()) & "" & vbCrLf)

            Next
            sb.Append(vbTab + vbTab + "case else" & vbCrLf)
            sb.Append(vbTab + vbTab + vbTab & "return nothing" & vbCrLf)

            sb.Append(vbTab + vbTab + "end select" & vbCrLf)

            Return sb.ToString()

        End Function
    End Class

End Namespace