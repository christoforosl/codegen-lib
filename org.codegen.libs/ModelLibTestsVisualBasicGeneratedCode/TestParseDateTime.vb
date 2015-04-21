
<TestClass()> _
Public Class TestParseDateTime

    <TestMethod()> _
    Public Sub TestModelObjectListOfT()

        ' TODO: Add test logic here
        Dim t As New ModelObjectList(Of TestModelObject)
        t.Add(New TestModelObject("A"))
        t.Add(New TestModelObject("B"))
        t.Add(New TestModelObject("C"))
        t.Add(New TestModelObject("D"))

        Assert.IsTrue(TypeOf t.Item(0) Is TestModelObject)
        Assert.IsTrue(TypeOf t.Item(0) Is ModelObject)

        t.Remove(t.Item(0))

        Assert.IsTrue(t.DeletedItems.Count = 1)
        t.DeleteAll()
        Assert.IsTrue(t.DeletedItems.Count = 4)

        t.Clear()
        Assert.IsTrue(t.DeletedItems.Count = 0)
        Assert.IsTrue(t.Count = 0)

    End Sub

End Class

Public Class TestModelObject : Inherits ModelObject

    Public Sub New(ByVal descr As String)
        Me._descr = descr
    End Sub

    Private _descr As String
    Private _id As Integer

    Public Overloads Overrides Function getAttribute(ByVal fieldKey As Integer) As Object
        Return Nothing
    End Function

    Public Overloads Overrides Function getAttribute(ByVal fieldKey As String) As Object
        Return Nothing
    End Function

    Public Overrides Function getFieldList() As String()
        Return New String() {""}
    End Function

    Public Property Descr() As String
        Get
            Return _descr
        End Get
        Set(ByVal value As String)
            _descr = value
        End Set
    End Property
    Public Overrides Property Id() As Integer
        Get
            Return Me._id
        End Get
        Set(ByVal value As Integer)
            _id = value
        End Set
    End Property

    Public Overloads Overrides Sub setAttribute(ByVal fieldKey As Integer, ByVal val As Object)

    End Sub

    Public Overloads Overrides Sub setAttribute(ByVal fieldKey As String, ByVal val As Object)

    End Sub

    Public Overrides Function copy() As IModelObject
        Return Me
    End Function

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

    Public Overrides Function getChildren() As List(Of ModelObject)

    End Function

    Public Overrides Function getParents() As List(Of ModelObject)

    End Function

    Public Overrides Sub merge(other As IModelObject)

    End Sub

End Class
