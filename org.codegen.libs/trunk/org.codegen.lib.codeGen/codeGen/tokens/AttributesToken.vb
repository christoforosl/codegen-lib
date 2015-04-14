Imports System.Collections.Generic

Namespace Tokens

    Public Class GetAttrStrToken
        Inherits MultiLingualReplacementToken

        Sub New()
            Me.StringToReplace = "GET_ATTRS_STR"
        End Sub
        Public Overrides Function getReplacementCodeCSharp(t As dotnet.IObjectToGenerate) As String

            Dim sb As System.Text.StringBuilder = New System.Text.StringBuilder(vbCrLf)
            Dim vec As Dictionary(Of String, IDBField) = t.DbTable.Fields()

            Dim i As Integer = 0
            For Each field As DBField In vec.Values

                If (i > 0) Then
                    sb.Append(vbTab + vbTab & "} else if (")
                Else
                    sb.Append(vbTab + vbTab & "if (")
                End If

                sb.Append("fieldKey==" & field.getConstantStr() & ".ToLower() ) {")
                sb.Append(vbCrLf)
				sb.Append(vbTab + vbTab + vbTab & "return this." & field.PropertyName & ";" & vbCrLf)
                i += 1

            Next
            sb.Append(vbTab + vbTab + "} else {" & vbCrLf)
            sb.Append(vbTab + vbTab + vbTab & "return null;" & vbCrLf)

            sb.Append(vbTab + vbTab + "}")
            Return sb.ToString()
        End Function


        Public Overrides Function getReplacementCodeVb(ByVal og As IObjectToGenerate) As String

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
				sb.Append(vbTab + vbTab + vbTab & "return me." & field.PropertyName & "" & vbCrLf)
                i += 1

            Next
            sb.Append(vbTab + vbTab + "else" & vbCrLf)
            sb.Append(vbTab + vbTab + vbTab & "return nothing" & vbCrLf)

            sb.Append(vbTab + vbTab + "end If")
            Return sb.ToString()
        End Function
    End Class

    Public Class SetAttrStrToken
        Inherits MultiLingualReplacementToken

        Sub New()
            Me.StringToReplace = "SET_ATTRS_STR"
        End Sub

        Public Overrides Function getReplacementCodeCSharp(t As dotnet.IObjectToGenerate) As String

            Dim sb As System.Text.StringBuilder = New System.Text.StringBuilder()
            Dim vec As Dictionary(Of String, IDBField) = t.DbTable.Fields()
            Dim i As Integer = 0

            For Each field As DBField In vec.Values
                sb.Append(vbTab + vbTab)

                If (i > 0) Then
                    sb.Append("} else if (")
                Else
                    sb.Append("if (")
                End If
                sb.Append(" fieldKey==" & field.getConstantStr() & ".ToLower()){")

                'sb.Append(" fieldKey=" & field.getConstantStr() & " Then")
                sb.Append(vbCrLf)

                sb.Append(vbTab + vbTab + vbTab & "if (val == DBNull.Value || val ==null ){" & vbCrLf)
                'sb.Append(vbTab + vbTab + vbTab + vbTab & "this." & DBTable.getRuntimeName(field.FieldName()) & " = null;" & vbCrLf)

                If (field.isPrimaryKey) Then
                    sb.Append(vbTab + vbTab + vbTab + vbTab & "throw new ApplicationException(""Can't set Primary Key to null"");" & vbCrLf)
                Else
					sb.Append(vbTab + vbTab + vbTab + vbTab & "this." & field.PropertyName & " = null;" & vbCrLf)
                End If

                sb.Append(vbTab + vbTab + vbTab & "} else {" & vbCrLf)

				sb.Append(vbTab + vbTab + vbTab + vbTab & "this." & field.PropertyName & "=")

                sb.Append("(").Append(field.FieldDataType).Append(")val;").Append(vbCrLf)


                sb.Append(vbTab + vbTab + vbTab & "}" & vbCrLf)
                sb.Append(vbTab + vbTab + vbTab & "return;" & vbCrLf)

                i = i + 1
            Next

            sb.Append(vbTab + vbTab + "}")
            Return sb.ToString()
        End Function

        Public Overrides Function getReplacementCodeVB(ByVal og As IObjectToGenerate) As String

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
				sb.Append(vbTab + vbTab + vbTab + vbTab & "Me." & field.PropertyName & " = Nothing" & vbCrLf)
                sb.Append(vbTab + vbTab + vbTab & "Else" & vbCrLf)

				sb.Append(vbTab + vbTab + vbTab + vbTab & "Me." & field.PropertyName & "=")

                sb.Append("CType(val,").Append(field.FieldDataType).Append(")").Append(vbCrLf)


                sb.Append(vbTab + vbTab + vbTab & "End If" & vbCrLf)
                sb.Append(vbTab + vbTab + vbTab & "return" & vbCrLf)

                i = i + 1
            Next

            sb.Append(vbTab + vbTab + "end If")
            Return sb.ToString()

        End Function

        

    End Class

    Public Class SetAttrToken
        Inherits MultiLingualReplacementToken

        Sub New()
            Me.StringToReplace = "SET_ATTRS"
        End Sub
        Public Overrides Function getReplacementCodeCSharp(t As dotnet.IObjectToGenerate) As String
            Dim sb As System.Text.StringBuilder = New System.Text.StringBuilder()
            Dim vec As Dictionary(Of String, IDBField) = t.DbTable.Fields()

            sb.Append(vbTab + vbTab + "switch (fieldKey) {" & vbCrLf)

            For Each field As DBField In vec.Values

                sb.Append(vbTab + vbTab & "case " & field.getConstant()).Append(":")
                sb.Append(vbCrLf)

                sb.Append(vbTab + vbTab + vbTab & "if (val == DBNull.Value || val == null ){" & vbCrLf)
                If (field.isPrimaryKey) Then
                    sb.Append(vbTab + vbTab + vbTab + vbTab & "throw new ApplicationException(""Can't set Primary Key to null"");" & vbCrLf)
                Else
					sb.Append(vbTab + vbTab + vbTab + vbTab & "this." & field.PropertyName & " = null;" & vbCrLf)
                End If

                sb.Append(vbTab + vbTab + vbTab & "}else{" & vbCrLf)

				sb.Append(vbTab + vbTab + vbTab + vbTab & "this." & field.PropertyName & "=")
                sb.Append("(" & field.FieldDataType).Append(")val;").Append(vbCrLf)

                sb.Append(vbTab + vbTab + vbTab & "} //" & vbCrLf)
                sb.Append(vbTab + vbTab + vbTab & "return;" & vbCrLf)


            Next

            sb.Append(vbTab + vbTab + "default:" & vbCrLf)
            sb.Append(vbTab + vbTab + vbTab & "return;" & vbCrLf)
            sb.Append(vbTab + vbTab + "}" & vbCrLf)

            Return sb.ToString()
        End Function

        Public Overrides Function getReplacementCodeVb(ByVal og As IObjectToGenerate) As String

            Dim sb As System.Text.StringBuilder = New System.Text.StringBuilder()
            Dim vec As Dictionary(Of String, IDBField) = og.DbTable.Fields()

            sb.Append(vbTab + vbTab + "Select Case fieldKey" & vbCrLf)

            For Each field As DBField In vec.Values

                sb.Append(vbTab + vbTab & "case " & field.getConstant())
                sb.Append(vbCrLf)

                sb.Append(vbTab + vbTab + vbTab & "If Val Is DBNull.Value OrElse Val Is Nothing Then" & vbCrLf)
				sb.Append(vbTab + vbTab + vbTab + vbTab & "Me." & field.PropertyName & " = Nothing" & vbCrLf)
                sb.Append(vbTab + vbTab + vbTab & "Else" & vbCrLf)

				sb.Append(vbTab + vbTab + vbTab + vbTab & "Me." & field.PropertyName & "=")
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
        Inherits MultiLingualReplacementToken

        Sub New()
            Me.StringToReplace = "GET_ATTRS"
        End Sub

        Public Overrides Function getReplacementCodeCSharp(t As dotnet.IObjectToGenerate) As String
            Dim sb As System.Text.StringBuilder = New System.Text.StringBuilder(vbCrLf)
            Dim vec As Dictionary(Of String, IDBField) = t.DbTable.Fields()

            sb.Append(vbTab + vbTab + "switch (fieldKey) {" & vbCrLf)

            For Each field As DBField In vec.Values

                sb.Append(vbTab + vbTab & "case ")
                sb.Append(field.getConstant()).Append(":")

                sb.Append(vbCrLf)
				sb.Append(vbTab + vbTab + vbTab & "return this." & field.PropertyName & ";" & vbCrLf)

            Next
            sb.Append(vbTab + vbTab + "default:" & vbCrLf)
            sb.Append(vbTab + vbTab + vbTab & "return null;" & vbCrLf)

            sb.Append(vbTab + vbTab + "} //end switch" & vbCrLf)

            Return sb.ToString()

        End Function

        Public Overrides Function getReplacementCodeVb(ByVal og As IObjectToGenerate) As String

            Dim sb As System.Text.StringBuilder = New System.Text.StringBuilder(vbCrLf)
            Dim vec As Dictionary(Of String, IDBField) = og.DbTable.Fields()

            sb.Append(vbTab + vbTab + "select case fieldKey" & vbCrLf)

            For Each field As DBField In vec.Values

                sb.Append(vbTab + vbTab & "case ")
                sb.Append(field.getConstant())

                sb.Append(vbCrLf)
				sb.Append(vbTab + vbTab + vbTab & "return me." & field.PropertyName & "" & vbCrLf)

            Next
            sb.Append(vbTab + vbTab + "case else" & vbCrLf)
            sb.Append(vbTab + vbTab + vbTab & "return nothing" & vbCrLf)

            sb.Append(vbTab + vbTab + "end select" & vbCrLf)

            Return sb.ToString()

        End Function
    End Class

End Namespace