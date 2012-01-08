Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports org.codegen.common


<TestClass()> _
Public Class EmailTest

    <TestMethod()> _
    Public Sub TestEmailGreek()

        Emailer.sentHtmlEmail("donotreply@cgl.local", "chris.lambrou@cglcomputer.com", "ETEK Web Site Test", "<html><body>Test - Δοκιμή</body></html>")

    End Sub
End Class
