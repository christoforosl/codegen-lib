Imports System.IO
Imports System.Text
Imports System.Xml
Imports System.Collections.Generic
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports org.codegen.common
Imports org.codegen.lib.db

<TestClass()> Public Class LanguageTest

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
            testContextInstance = value
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

    <TestMethod()> Public Sub oby()

       

    End Sub
    <TestMethod()> Public Sub TestDBLangSrings()

        Dim dbl As Translation.DBLanguageStrings = New Translation.DBLanguageStrings()
        Const connString As String = "Password=ETEK123;Persist Security Info=True;User ID=ETEK;Initial Catalog=ETEK_DEV;Data Source=SAVSERVER"
        dbl.dbConn = DBUtilsBase.getFromConnString(connString, DBUtilsBase.enumConnType.CONN_MSSQL, DBUtilsBase.enumSqlDialect.MSSQL)

        Dim skey As String = dbl.getStringDB("frmLogin_caption", Translation.Lang.LANG_ENGLISH)

        Assert.AreEqual("Login", skey)

        Diagnostics.Debug.WriteLine(dbl.getStringDB("frmLogin_caption", Translation.Lang.LANG_GREEK))
    End Sub

End Class
