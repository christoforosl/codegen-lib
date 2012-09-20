'---------------------------------------------------------------------
'  This file is part of the Microsoft .NET Framework SDK Code Samples.
' 
'  Copyright (C) Microsoft Corporation.  All rights reserved.
' 
' This source code is intended only as a supplement to Microsoft
' Development Tools and/or on-line documentation.  See these other
' materials for detailed information regarding Microsoft code samples.
' 
' THIS CODE AND INFORMATION ARE PROVIDED AS IS WITHOUT WARRANTY OF ANY
' KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
' IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
' PARTICULAR PURPOSE.
'---------------------------------------------------------------------

Public NotInheritable Class WinEventLogger

    Private Const LOG_NAME As String = "Application"

    Private Shared cEventSource As String = winUtils.ApplicationTitle

    Public Shared Sub WriteError(ByVal errorMessage As String)
        Try
            'the event source should be created during the installation process
            If EventLog.SourceExists(cEventSource) Then

                'write the message as an error
                Dim msg As EventLog = New EventLog(LOG_NAME)
                msg.Source = cEventSource
                msg.WriteEntry(errorMessage, EventLogEntryType.Error)
            Else
                'try to create the event source for the next error (this requires admin rights)
                EventLog.CreateEventSource(cEventSource, LOG_NAME)
            End If
        Catch e As Exception
            MsgBox("Error in WinEventLogger.WriteError: " & e.Message, MsgBoxStyle.Exclamation, cEventSource)
        End Try
    End Sub

    Public Shared Sub WriteInfo(ByVal errorMessage As String)
        Try
            'the event source should be created during the installation process
            If EventLog.SourceExists(cEventSource) Then

                'write the message as an error
                Dim msg As EventLog = New EventLog(LOG_NAME)
                msg.Source = cEventSource
                msg.WriteEntry(errorMessage, EventLogEntryType.Information)
            Else
                'try to create the event source for the next error (this requires admin rights)
                EventLog.CreateEventSource(cEventSource, LOG_NAME)
            End If
        Catch e As Exception
            MsgBox("Error in WinEventLogger.WriteInfo: " & e.Message, MsgBoxStyle.Exclamation, cEventSource)
        End Try
    End Sub

    Public Shared Sub WriteError(ByVal e As Exception)

        Dim msg As String = e.Message & vbCrLf
        msg &= msg
        Call WriteError(msg)

    End Sub

End Class
