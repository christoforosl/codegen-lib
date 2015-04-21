<TestClass()> _
Public Class TestParseDateTime

    <TestMethod()>
    Public Sub TestParseDateTime()

        Dim c As String = ""
        Dim tval As DateTime? = org.codegen.win.controls.CGTimeBox.parseTime(c)
        Assert.IsNull(tval)

        c = "10"
        Assert.AreEqual(org.codegen.win.controls.CGTimeBox.parseTime(c), TimeSerial(10, 0, 0))

        c = "10:00"
        Assert.AreEqual(org.codegen.win.controls.CGTimeBox.parseTime(c), TimeSerial(10, 0, 0))

        c = "13:00"
        Assert.AreEqual(org.codegen.win.controls.CGTimeBox.parseTime(c), TimeSerial(13, 0, 0))

        c = "18:90"
        tval = org.codegen.win.controls.CGTimeBox.parseTime(c)
        Assert.IsNull(tval)

        c = "25:90"
        tval = org.codegen.win.controls.CGTimeBox.parseTime(c)
        Assert.IsNull(tval)

        c = "5:10"
        Assert.AreEqual(org.codegen.win.controls.CGTimeBox.parseTime(c), TimeSerial(5, 10, 0))

        c = "5:10 PM"
        Assert.AreEqual(org.codegen.win.controls.CGTimeBox.parseTime(c), TimeSerial(17, 10, 0))

        c = "18:00 PM"
        tval = org.codegen.win.controls.CGTimeBox.parseTime(c)
        Assert.IsNull(tval)

        c = "18"
        tval = org.codegen.win.controls.CGTimeBox.parseTime(c)
        Assert.AreEqual(tval, TimeSerial(18, 0, 0))

        c = "18.1"
        tval = org.codegen.win.controls.CGTimeBox.parseTime(c)
        Assert.AreEqual(tval, TimeSerial(18, 1, 0))

        c = "3 10"
        tval = org.codegen.win.controls.CGTimeBox.parseTime(c)
        Assert.AreEqual(tval, TimeSerial(3, 10, 0))

        c = "13 10"
        tval = org.codegen.win.controls.CGTimeBox.parseTime(c)
        Assert.AreEqual(tval, TimeSerial(13, 10, 0))

        c = "3-10"
        tval = org.codegen.win.controls.CGTimeBox.parseTime(c)
        Assert.AreEqual(tval, TimeSerial(3, 10, 0))

        c = "13-10"
        tval = org.codegen.win.controls.CGTimeBox.parseTime(c)
        Assert.AreEqual(tval, TimeSerial(13, 10, 0))

    End Sub

End Class
