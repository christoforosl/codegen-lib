﻿Imports System.Collections.Generic

Namespace Tokens


    Public Class ObjectMergeToken
        Inherits ReplacementToken


        Sub New()
            Me.StringToReplace = "MERGE_FIELDS"
        End Sub

        Public Overrides Function getReplacementCode(ByVal t As IObjectToGenerate) As String
            Dim sb As System.Text.StringBuilder = New System.Text.StringBuilder()
            Dim vec As Dictionary(Of String, IDBField) = t.DbTable.Fields()
            
            For Each field As DBField In vec.Values
                If field.isPrimaryKey Then
                Else


                    sb.Append("If ")
                    If field.RuntimeType Is Type.GetType("System.String") Then
                        sb.Append("not String.isNullOrEmpty(o." & field.RuntimeFieldName & ")")
                    Else
                        sb.Append("not o." & field.RuntimeFieldName & " is Nothing")
                    End If
                    sb.Append(" AndAlso _").Append(vbCrLf).Append(vbTab).Append(vbTab)

                    If field.RuntimeType Is Type.GetType("System.String") Then
                        sb.Append(" String.isNullOrEmpty(me." & field.RuntimeFieldName & ")")
                    Else
                        sb.Append(" me." & field.RuntimeFieldName & " is Nothing")
                    End If
                    sb.Append(" Then ").Append(vbCrLf)

                    sb.Append(vbTab + vbTab & "me." & field.RuntimeFieldName & " = o." & _
                                        field.RuntimeFieldName & _
                                vbCrLf)

                    sb.Append("End If")
                    sb.Append(vbCrLf)

                End If
            Next

            Return sb.ToString()
        End Function
    End Class

    Public Class ObjectCopyToken
        Inherits ReplacementToken


        Sub New()
            Me.StringToReplace = "COPY_FIELDS"
        End Sub

        Public Overrides Function getReplacementCode(ByVal t As IObjectToGenerate) As String
            Dim sb As System.Text.StringBuilder = New System.Text.StringBuilder()
            Dim vec As Dictionary(Of String, IDBField) = t.DbTable.Fields()
            Dim i As Integer = 0

            For Each field As DBField In vec.Values

                'If field.isPrimaryKey = False Then
                sb.Append(vbTab + vbTab & "ret." & field.RuntimeFieldName & " = me." & _
                                    DBTable.getRuntimeName(field.FieldName()) & _
                            vbCrLf)
                'End If

                i = i + 1
            Next

            Return sb.ToString()
        End Function
    End Class

    Public Class XOrFieldsToken
        Inherits ReplacementToken


        Sub New()
            Me.StringToReplace = "X_OR_FIELS"
        End Sub

        Public Overrides Function getReplacementCode(ByVal t As IObjectToGenerate) As String

            Dim sb As System.Text.StringBuilder = New System.Text.StringBuilder()
            Dim vec As Dictionary(Of String, IDBField) = t.DbTable.Fields()
            Dim i As Integer = 0

            For Each field As DBField In vec.Values

                If i > 0 Then
                    sb.Append(" _" & vbCrLf)
                    sb.Append(vbTab & vbTab & vbTab & vbTab)
                    sb.Append("Xor ")

                End If
                If field.RuntimeType Is Type.GetType("System.String") Then
                    sb.Append("me.getStringHashCode(Me." & field.RuntimeFieldName & ")")
                Else
                    sb.Append("me." & field.RuntimeFieldName & ".GetHashCode")
                End If

                i = i + 1

            Next
            Return sb.ToString()
        End Function
    End Class

    Public Class ObjectEqualsToken
        Inherits ReplacementToken

        Sub New()
            Me.StringToReplace = "EQUALS_FIELDS"
        End Sub

        Public Overrides Function getReplacementCode(ByVal t As IObjectToGenerate) As String

            Dim sb As System.Text.StringBuilder = New System.Text.StringBuilder()
            Dim vec As Dictionary(Of String, IDBField) = t.DbTable.Fields()
            Dim i As Integer = 0

            For Each field As DBField In vec.Values

                If i > 0 Then
                    sb.Append(" _" & vbCrLf)
                    sb.Append(vbTab & vbTab & vbTab & vbTab)
                    sb.Append("AndAlso ")

                End If

                If field.isNullableDataType Then
                    sb.Append("me." & field.RuntimeFieldName & ".GetValueOrDefault = other." & field.RuntimeFieldName & ".GetValueOrDefault")
                Else
                    sb.Append("me." & field.RuntimeFieldName & "= other." & field.RuntimeFieldName)
                End If


                i = i + 1

            Next
            Return sb.ToString()

        End Function


    End Class

End Namespace
