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
        If (Me.lst Is Nothing) Then Return Nothing
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


Public Class EnumFieldsCollection

    Private lst As List(Of EnumFieldDefinition)

    Public Sub addEnumFieldDefinition(tablename As String, fieldname As String, enumTypeName As String)
        If (Me.lst Is Nothing) Then
            Me.lst = New List(Of EnumFieldDefinition)
        End If
        Me.lst.Add(New EnumFieldDefinition(tablename, fieldname, enumTypeName))
    End Sub

    Public Function getEnumField(field As IDBField) As EnumFieldDefinition

        If (Me.lst Is Nothing) Then Return Nothing

        For Each bf As EnumFieldDefinition In lst

            If field.FieldName.ToLower = bf.fieldName.ToLower AndAlso _
                field.ParentTable.TableName.ToLower = bf.tableName.ToLower Then
                Return bf
            End If

        Next

        Return Nothing

    End Function

End Class

Public Class EnumFieldDefinition

    Public Property tableName As String
    Public Property fieldName As String
    Public Property enumTypeName As String

    Public Sub New(tableName As String, fieldName As String, enumTypeName As String)

        Me.fieldName = fieldName
        Me.tableName = tableName
        Me.enumTypeName = enumTypeName

    End Sub



End Class
