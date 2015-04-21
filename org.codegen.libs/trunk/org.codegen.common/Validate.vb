Imports System.Reflection
Imports System.IO
Imports System.Text
Imports System.Threading
Imports System.Security.Principal
Imports System.Globalization
Imports System.Configuration

Public Interface IErrorKeeper

    Sub addToErrorMessage(msg As String, ParamArray args As Object())

End Interface

Public Class Validate

    Public Shared Sub isTrue(val As Boolean, _
                    Optional msg As String = "Validate.isTrue assertion failed", _
                    Optional errkeeper As IErrorKeeper = Nothing)

        If (Not val) Then

            If errkeeper IsNot Nothing Then
                errkeeper.addToErrorMessage(msg)
            Else
                Throw New ApplicationException(msg)
            End If

        End If

    End Sub

    Public Shared Sub isTrue(val As Boolean, msg As String, ParamArray args() As Object)

        If (Not val) Then
            Throw New ApplicationException(String.Format(msg, args))
        End If

    End Sub

    Public Shared Sub isTrue(val As Boolean, msg As String, errkeeper As IErrorKeeper, ParamArray args() As Object)

        If (Not val) Then
            errkeeper.addToErrorMessage(msg, args)
        End If

    End Sub

    Public Shared Sub isNotNull(val As Object, _
                                Optional errkeeper As IErrorKeeper = Nothing, _
                                Optional msg As String = "Validate.isNotNull assertion failed")

        If (val Is Nothing) Then
            If errkeeper IsNot Nothing Then
                errkeeper.addToErrorMessage(msg)
            Else
                Throw New ApplicationException(msg)
            End If
        End If

    End Sub

    Public Shared Sub isNotNull(val As Object, errkeeper As IErrorKeeper, template As String, ParamArray args() As String)

        If (val Is Nothing) Then
            errkeeper.addToErrorMessage(String.Format(template, args))
        End If

    End Sub

    Public Shared Sub isNotNull(val As Object, msg As String, ParamArray args() As String)

        If (val Is Nothing) Then
            Throw New ApplicationException(String.Format(msg, args))
        End If

    End Sub


    Public Shared Sub isNotEmptyString(val As String, Optional msg As String = "Validate.isNotEmptyString assertion failed")

        If (String.IsNullOrEmpty(val)) Then
            Throw New ApplicationException(msg)
        End If

    End Sub

    ''' <summary>
    ''' Checks if val is null or empty, and if it is it adds it to the error string of IErrorKeeper
    ''' </summary>
    ''' <param name="val">value to check</param>
    ''' <param name="errkeeper">object to receive error string</param>
    ''' <param name="template">Message Template of error</param>
    ''' <param name="args">Message Template arguments of error</param>
    ''' <remarks></remarks>
    Public Shared Sub isNotEmptyString(val As String, errkeeper As IErrorKeeper, template As String, ParamArray args() As String)

        If (String.IsNullOrEmpty(val)) Then
            errkeeper.addToErrorMessage(String.Format(template, args))
        End If

    End Sub

End Class