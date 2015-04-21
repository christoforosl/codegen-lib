Imports System
Imports System.Text
Imports System.Collections.Generic
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports org.codegen.lib.Model

<TestClass()> Public Class testModelObjectList2

    Private testContextInstance As TestContext

    '''<summary>
    '''Gets or sets the test context which provides
    '''information about and functionality for the current test run.
    '''</summary>
    Public Property TestContext() As TestContext
        Get
            Return testContextInstance
        End Get
        Set(ByVal value As TestContext)
            testContextInstance = Value
        End Set
    End Property

#Region "Additional test attributes"
    '
    ' You can use the following additional attributes as you write your tests:
    '
    ' Use ClassInitialize to run code before running the first test in the class
    ' <ClassInitialize()> Public Shared Sub MyClassInitialize(ByVal testContext As TestContext)
    ' End Sub
    '
    ' Use ClassCleanup to run code after all tests in a class have run
    ' <ClassCleanup()> Public Shared Sub MyClassCleanup()
    ' End Sub
    '
    ' Use TestInitialize to run code before running each test
    ' <TestInitialize()> Public Sub MyTestInitialize()
    ' End Sub
    '
    ' Use TestCleanup to run code after each test has run
    ' <TestCleanup()> Public Sub MyTestCleanup()
    ' End Sub
    '
#End Region

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

    Public Overrides Function getRequiredFieldList() As String()
        Return New String() {"descr"}
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

    Public Overrides Function isEmpty() As Boolean
        Return False
    End Function
End Class