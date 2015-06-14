Imports System.Collections.Generic

Namespace Tokens

   
    Public Class ObjectCopyToken
        Inherits MultiLingualReplacementToken

        Sub New()
            Me.StringToReplace = "COPY_FIELDS"
        End Sub
        Public Overrides Function getReplacementCodeCSharp(t As IObjectToGenerate) As String

            Dim sb As System.Text.StringBuilder = New System.Text.StringBuilder()
            Dim vec As Dictionary(Of String, IDBField) = t.DbTable.Fields()
            Dim i As Integer = 0

            For Each field As DBField In vec.Values

                'If field.isPrimaryKey = False Then
                sb.Append(vbTab + vbTab).Append("ret.").Append(field.PropertyName)
                sb.Append(" = this.").Append(field.PropertyName).Append(";").Append(vbCrLf)
                'End If

                i = i + 1
            Next

            Return sb.ToString()
        End Function
        Public Overrides Function getReplacementCodeVb(ByVal t As IObjectToGenerate) As String

            Dim sb As System.Text.StringBuilder = New System.Text.StringBuilder()
            Dim vec As Dictionary(Of String, IDBField) = t.DbTable.Fields()
            Dim i As Integer = 0

            For Each field As DBField In vec.Values

                'If field.isPrimaryKey = False Then
                sb.Append(vbTab + vbTab & "ret." & field.PropertyName & " = me." & _
                     field.PropertyName & _
                   vbCrLf)
                'End If

                i = i + 1
            Next

            Return sb.ToString()
        End Function
    End Class

    Public Class XOrFieldsToken
        Inherits MultiLingualReplacementToken


        Sub New()
            Me.StringToReplace = "X_OR_FIELS"
        End Sub

        Public Overrides Function getReplacementCodeCSharp(t As IObjectToGenerate) As String
            Dim sb As System.Text.StringBuilder = New System.Text.StringBuilder()
            Dim vec As Dictionary(Of String, IDBField) = t.DbTable.Fields()
            Dim i As Integer = 0

            For Each field As DBField In vec.Values
                If field.FieldDataType <> "System.Byte[]" Then
                    If i > 0 Then
                        sb.Append(vbCrLf)
                        sb.Append(vbTab & vbTab & vbTab & vbTab)
                        sb.Append(" ^ ")

                    End If

                    If field.RuntimeType Is Type.GetType("System.String") Then
                        sb.Append("this.getStringHashCode(this." & field.PropertyName & ")")
                    Else
                        sb.Append("this." & field.PropertyName & ".GetHashCode()")
                    End If
                    i = i + 1
                End If
            Next
            sb.Append(";")
            Return sb.ToString()
        End Function

        Public Overrides Function getReplacementCodeVB(ByVal t As IObjectToGenerate) As String

            Dim sb As System.Text.StringBuilder = New System.Text.StringBuilder()
            Dim vec As Dictionary(Of String, IDBField) = t.DbTable.Fields()
            Dim i As Integer = 0

            For Each field As IDBField In vec.Values

                If Not field.isBinaryField Then
                    If i > 0 Then
                        sb.Append(" _" & vbCrLf)
                        sb.Append(vbTab & vbTab & vbTab & vbTab)
                        sb.Append("Xor ")

                    End If

                    If field.RuntimeType Is Type.GetType("System.String") Then
                        sb.Append("me.getStringHashCode(Me." & field.PropertyName & ")")
                    Else
                        sb.Append("me." & field.PropertyName & ".GetHashCode")
                    End If
                    i = i + 1

                End If

            Next
            Return sb.ToString()
        End Function
    End Class

    Public Class ObjectEqualsToken
        Inherits MultiLingualReplacementToken

        Sub New()
            Me.StringToReplace = "EQUALS_FIELDS"
        End Sub

        Public Overrides Function getReplacementCodeCSharp(t As IObjectToGenerate) As String
            Dim sb As System.Text.StringBuilder = New System.Text.StringBuilder()
            Dim vec As Dictionary(Of String, IDBField) = t.DbTable.Fields()
            Dim i As Integer = 0

            For Each field As DBField In vec.Values
                If field.FieldDataType <> "System.Byte[]" Then
                    If i > 0 Then
                        sb.Append(vbCrLf)
                        sb.Append(vbTab & vbTab & vbTab & vbTab)
                        sb.Append("&& ")

                    End If

                    If field.isNullableProperty Then
                        sb.Append("this." & field.PropertyName & ".GetValueOrDefault() == other." & field.PropertyName & ".GetValueOrDefault()")
                    Else
                        sb.Append("this." & field.PropertyName & " == other." & field.PropertyName)
                    End If
                    i = i + 1
                End If

            Next
            sb.Append(";")
            Return sb.ToString()

        End Function
        Public Overrides Function getReplacementCodeVb(ByVal t As IObjectToGenerate) As String

            Dim sb As System.Text.StringBuilder = New System.Text.StringBuilder()
            Dim vec As Dictionary(Of String, IDBField) = t.DbTable.Fields()
            Dim i As Integer = 0

            For Each field As IDBField In vec.Values
                If Not field.isBinaryField Then
                    If i > 0 Then
                        sb.Append(" _" & vbCrLf)
                        sb.Append(vbTab & vbTab & vbTab & vbTab)
                        sb.Append("AndAlso ")

                    End If

                    If field.FieldName.ToLower = "id" Then
                        'case when the table has a field named "id"
                        sb.Append("me.Id.ToString = other.Id.ToString")
                    ElseIf field.isNullableProperty Then
                        sb.Append("me." & field.PropertyName & ".GetValueOrDefault = other." & field.PropertyName & ".GetValueOrDefault")
                    Else
                        sb.Append("me." & field.PropertyName & "= other." & field.PropertyName)
                    End If
                    i = i + 1
                End If



            Next
            Return sb.ToString()

        End Function


    End Class

End Namespace
