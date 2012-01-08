Imports System.Collections.Generic
Imports System.Text

Namespace Tokens
    ''' <summary>
    ''' Model Object field declarations.  This applies only to file type ModelObjectBase
    ''' </summary>
    ''' <remarks></remarks>
    Public Class ClassFieldDeclarationsToken
        Inherits ReplacementToken

        Sub New()
            Me.StringToReplace = "FIELD_DECLARATIONS"
        End Sub

        Public Overrides Function getReplacementCode(ByVal t As IObjectToGenerate) As String
            Dim sb As System.Text.StringBuilder = New System.Text.StringBuilder(vbCrLf)
            Dim vec As Dictionary(Of String, IDBField) = t.DbTable.Fields()

            Dim i As Integer = 0
            For Each field As DBField In vec.Values

                sb.Append(field.getClassVariableDeclaration("Private"))

            Next

            sb.Append(getAssociationsVars(CType(t.DbTable, DBTable)))
            Return sb.ToString()

        End Function

        Private Function getAssociationsVars(ByVal t As DBTable) As String

            Dim sb As System.Text.StringBuilder = New System.Text.StringBuilder()

            If t.Associations().Count() > 0 Then

                sb.Append(vbCrLf & vbTab & "' *****************************************" & vbCrLf)
                sb.Append(vbTab & "' ****** CHILD OBJECTS ********************" & vbCrLf)
                Dim vec As List(Of IAssociation) = t.Associations()

                For Each association As Association In vec
                    sb.Append(association.getVariable())
                    sb.Append(association.getDeletedVariable())
                Next

                sb.Append(vbCrLf & vbTab & "' *****************************************" & vbCrLf)
                sb.Append(vbTab & "' ****** END CHILD OBJECTS ********************" & vbCrLf)

            End If

            Return sb.ToString()
        End Function
    End Class
End Namespace
