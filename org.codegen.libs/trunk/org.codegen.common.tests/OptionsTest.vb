Imports System
Imports System.Text
Imports System.Collections.Generic
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports org.codegen.common
Imports org.codegen.lib.db

<TestClass()> Public Class OptionsTest

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

    <TestMethod()> Public Sub TestDBOptions()

        Dim dbOptions As DBOptions = New DBOptions()
        Dim dbutils As DBUtilsBase = DBUtilsBase.getFromConnString("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\vs2008Projects\Mastris.mdb;Persist Security Info=True;Jet OLEDB:Database Password=apoel123", DBUtilsBase.enumConnType.CONN_OLEDB, DBUtilsBase.enumSqlDialect.JET)

        dbutils.executeSQL("delete from options where okey='TEST'")
        dbOptions.dbconn = dbutils
        Options.RegisterStore(dbOptions)

        Assert.AreEqual(Options.getOption("test"), "")
        Assert.AreEqual(Options.getOption(CStr(Date.Now)), "", "Expected Empty String on Non-Existent key")

        Options.setOption("test", "1243")
        Options.persistOptions()
        Options.reset()

        Assert.AreEqual(Options.getOption("test"), "1243")

        Options.RegisterStore(New MemOptions)
        Options.setOption("test", "test123")
        Assert.AreEqual(Options.getOption("test"), "test123")

        Assert.AreEqual(Options.getOption(CStr(Date.Now)), "", "Expected Empty String on Non-Existent key")


    End Sub

End Class
