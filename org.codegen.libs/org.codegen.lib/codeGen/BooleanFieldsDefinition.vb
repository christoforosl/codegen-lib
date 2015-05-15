Public Class BooleanFieldsDefinition

    Public Property DBDatatype As String = "int"
    Public Property DBDatatypeLength As Integer = 1
    Public Property ExcludedFields As String = String.Empty

    Public Function isBooleanField(field As IDBField) As Boolean

        If SkipField(field.FieldName) Then
            Return False
        Else
            Return field.FieldDataType = Me.DBDatatype And field.Size = Me.DBDatatypeLength
        End If

    End Function

    Public Function SkipField(fieldname As String) As Boolean

        If (String.IsNullOrEmpty(ExcludedFields)) Then Return False

        If (Not ExcludedFields.EndsWith(",")) Then
            ExcludedFields = ExcludedFields & ","
        End If
        If (Not ExcludedFields.StartsWith(",")) Then
            ExcludedFields = "," & ExcludedFields & ","
        End If

        Return Me.ExcludedFields.Contains("," & fieldname & ",")

    End Function

End Class
