''' <summary>
''' Utility class to check for DBNulls.
''' <code source="C:\vs2008Projects\org.codegen.lib.vb\org.codegen.lib.db\examples.vb" region="DataReader and NullChecker Examples" lang="vbnet" title="DataReader Example" />
''' </summary>
''' <remarks></remarks>
Public Class NullChecker

    Private Sub New()
        'private contructor to prevent instance
    End Sub

#Region "Null Checks"
    ''' <summary>
    ''' NULL_DATE value of #12:00:00 AM# 
    ''' Any date variable with this value is treated as a NULL date
    ''' </summary>
    ''' <remarks></remarks>
    Public Const NULL_DATE As Date = #12:00:00 AM#

    ''' <summary>
    ''' Returns an empty string(String.Empty) if dbvalue is Nothing, or DBNull.value
    ''' </summary>
    ''' <param name="dbvalue">Value to check</param>
    ''' <param name="bTrimIt">if true, trim result before returning it. Default is true</param>
    ''' <returns>String value of dbvalue</returns>
    ''' <remarks></remarks>
    Public Shared Function strNull(ByVal dbvalue As Object, Optional ByVal bTrimIt As Boolean = True) As String

        If IsDBNull(dbvalue) Then
            Return String.Empty

        ElseIf dbvalue Is Nothing Then
            Return String.Empty

        Else
            If bTrimIt Then
                Return dbvalue.ToString.Trim
            Else
                Return dbvalue.ToString
            End If
        End If

    End Function

    ''' <summary>
    ''' Returns a default value if dbvalue is Nothing, or DBNull.value
    ''' </summary>
    ''' <param name="dbvalue"></param>
    ''' <param name="defaultIfDBNull"></param>
    ''' <returns>String value of dbvalue, or defaultIfDBNull if dbvalue is Nothing, or DBNull.value</returns>
    ''' <remarks></remarks>
    Public Shared Function strNullOrDefault(ByVal dbvalue As Object, ByVal defaultIfDBNull As String) As String

        If IsDBNull(dbvalue) Then
            Return defaultIfDBNull

        ElseIf dbvalue Is Nothing Then
            Return defaultIfDBNull

        Else
            Return dbvalue.ToString
        End If

    End Function
    ''' <summary>
    ''' Checks a value for DBNull and if it isNumeric(), and returns a Double
    ''' </summary>
    ''' <param name="obj"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function numNull(ByVal obj As Object) As Double

        If IsDBNull(obj) OrElse IsNumeric(obj) = False Then
            Return 0
        Else
            Return CDbl(obj)

        End If

    End Function
    ''' <summary>
    ''' Checks a value for DBNull and if it isNumeric(), and returns a Decimal
    ''' </summary>
    ''' <param name="obj"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function decNull(ByVal obj As Object) As Decimal

        If IsDBNull(obj) OrElse IsNumeric(obj) = False Then
            Return 0
        Else
            Return CDec(obj)
        End If

    End Function

    ''' <summary>
    ''' Checks a value for DBNull and if it isNumeric(), and returns a Long
    ''' </summary>
    ''' <param name="obj"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function longNull(ByVal obj As Object) As Long

        If IsDBNull(obj) OrElse IsNumeric(obj) = False Then
            Return 0
        Else
            Return CLng(obj)
        End If

    End Function
    ''' <summary>
    ''' Checks a value for DBNull and if it isNumeric(), and returns a Single
    ''' </summary>
    ''' <param name="obj"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function SngNull(ByVal obj As Object) As Single
        If IsDBNull(obj) OrElse IsNumeric(obj) = False Then
            Return 0
        Else
            Return CSng(obj)
        End If

    End Function

    ''' <summary>
    ''' Checks a value for DBNull and if it isNumeric(), and returns an Integer or 0
    ''' </summary>
    ''' <param name="obj"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function intNull(ByVal obj As Object) As Integer

        Return intNullOrDefault(obj, 0)

    End Function

    ''' <summary>
    ''' Checks a value for DBNull and if it isNumeric(), and returns an Integer or defVal
    ''' </summary>
    ''' <param name="obj"></param>
    ''' <param name="defVal"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function intNullOrDefault(ByVal obj As Object, ByVal defVal As Integer) As Integer

        If IsDBNull(obj) OrElse IsNumeric(obj) = False Then
            Return defVal
        Else
            Return CInt(obj)
        End If

    End Function

    ''' <summary>
    ''' Checks a value for DBNull and if it isNumeric(), and returns a Short
    ''' </summary>
    ''' <param name="obj"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function shortNull(ByVal obj As Object) As Short

        Return shortNullOrDefault(obj, 0)

    End Function

    ''' <summary>
    ''' Checks a value for DBNull and if it isNumeric(), and returns a Short or defVal
    ''' </summary>
    ''' <param name="obj"></param>
    ''' <param name="defaultIfNull"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function shortNullOrDefault(ByVal obj As Object, ByVal defaultIfNull As Short) As Short

        If IsDBNull(obj) OrElse IsNumeric(obj) = False Then
            Return defaultIfNull
        Else
            Return CShort(obj)
        End If

    End Function

    ''' <summary>
    ''' Checks a value for DBNull and if isDate(), and returns a Date value
    ''' </summary>
    ''' <param name="obj"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function dateNull(ByVal obj As Object) As Date

        If IsDBNull(obj) OrElse IsDate(obj) = False Then
            Return NULL_DATE
        Else
            Return CDate(obj)
        End If

    End Function


#End Region

End Class
