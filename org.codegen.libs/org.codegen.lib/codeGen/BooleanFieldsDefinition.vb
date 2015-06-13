Imports System.Collections.Generic

Public Class BooleanFieldsCollection

    Private lst As List(Of BooleanFieldDefinition)

    Public Sub addBooleanFieldDefinition(tablename As String, fieldname As String)
        If (Me.lst Is Nothing) Then
            Me.lst = New List(Of BooleanFieldDefinition)
        End If
        Me.lst.Add(New BooleanFieldDefinition(tablename, fieldname))
    End Sub

    Public Function isBooleanField(field As IDBField) As Boolean

        For Each bf As BooleanFieldDefinition In lst

            If field.FieldName.ToLower = bf.fieldName.ToLower AndAlso _
                field.ParentTable.TableName.ToLower = bf.tableName.ToLower Then
                Return True
            End If

        Next

        Return False

    End Function

End Class

Public Class BooleanFieldDefinition

    Public Property tableName As String
    Public Property fieldName As String

    Public Sub New(tableName As String, fieldName As String)

        Me.fieldName = fieldName
        Me.tableName = tableName

    End Sub

   

End Class
