Option Strict On
Imports System.Net.Mail
Imports System.Collections.Generic

Public Class Emailer

    Shared Sub sentEmail(ByVal fromEmail As String, ByVal recipientEmail As String, ByVal sSubject As String, ByVal sBody As String)

        Dim sourceEncoding As System.Text.Encoding = System.Text.Encoding.UTF8
        'Debug.WriteLine("Windows charset: " & sourceEncoding.HeaderName)
        'Debug.WriteLine("Windows code page: " & sourceEncoding.CodePage)

        'text.AppendFormat( "Content-Type: text/plain;\r\n\tcharset=\"{0:G}\"\r\n", _
        '                sourceEncoder.HeaderName )

        '(1) Create the MailMessage instance
        Dim mm As New MailMessage(fromEmail, recipientEmail)
        mm.BodyEncoding = sourceEncoding

        '(2) Assign the MailMessage's properties
        mm.Subject = sSubject
        mm.Body = sBody
        mm.IsBodyHtml = False

        '(3) Create the SmtpClient object
        Dim smtp As New SmtpClient

        '(4) Send the MailMessage (will use the Web.config settings)
        smtp.Send(mm)

    End Sub


    Shared Sub sentHtmlEmail(ByVal fromEmail As String, _
                             ByVal recipientEmail As String, _
                             ByVal sSubject As String, _
                             ByVal sHtmlBody As String)

        Dim sourceEncoding As System.Text.Encoding = System.Text.Encoding.UTF8
        'Debug.WriteLine("Windows charset: " & sourceEncoding.HeaderName)
        'Debug.WriteLine("Windows code page: " & sourceEncoding.CodePage)

        'text.AppendFormat( "Content-Type: text/plain;\r\n\tcharset=\"{0:G}\"\r\n", _
        '                sourceEncoder.HeaderName )

        '(1) Create the MailMessage instance
        Dim mm As New MailMessage(fromEmail, recipientEmail)
        mm.BodyEncoding = sourceEncoding

        '(2) Assign the MailMessage's properties
        mm.Subject = sSubject
        mm.Body = sHtmlBody
        mm.IsBodyHtml = True

        '(3) Create the SmtpClient object
        Dim smtp As New SmtpClient

        '(4) Send the MailMessage (will use the Web.config settings)
        smtp.Send(mm)

    End Sub

End Class
