Imports System.Runtime.InteropServices

Namespace Model

    Public Class ModelObjectRequiredFieldException
        Inherits Exception

    Private _requiedFieldName As String

    Public Sub New(ByVal requiedFieldName As String)

        MyBase.New("Missing required Field " & requiedFieldName)
        Me._requiedFieldName = Fieldname

    End Sub

    Public ReadOnly Property Fieldname() As String
        Get
            Return _requiedFieldName
        End Get
    End Property

    End Class

    Public Class ModelObjectFieldTooLongException
        Inherits Exception

        Private _FieldName As String

        Public Sub New(ByVal fieldName As String)

            MyBase.New("Field data is too long for field: " & fieldName)
            Me._FieldName = fieldName

        End Sub

        Public ReadOnly Property FieldName() As String
            Get
                Return _FieldName
            End Get
        End Property

    End Class

    Public Class ModelObjectDuplicateRecordException
        Inherits Exception

        Public Sub New(ByVal objectName As String, ByVal fieldValue As String, ByVal fieldName As String)

            MyBase.New("Cannot save record """ & objectName & """ to database.  Another record with value """ & fieldValue & """ exists in field: """ & fieldName)


        End Sub

    End Class

End Namespace