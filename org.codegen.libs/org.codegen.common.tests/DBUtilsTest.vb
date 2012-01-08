Imports System.Collections
Imports System.Data
Imports System.Text
Imports System.Collections.Generic
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports org.codegen.common
Imports org.codegen.lib.db

<TestClass()> Public Class DBUtilsTest

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

#Region "Parameters Example, with output parameter"

    '<TestMethod()> Public Sub TestOutParameters()

    '    Const connstr As String = "Password=ETEK123;Persist Security Info=True;User ID=ETEK;Initial Catalog=ETEK_DEV;Data Source=CHRISTOFOROSL"

    '    'get a DB utilities object from the above connection string
    '    Dim dbconn As DBUtilsBase = DBUtilsBase.getFromConnString(connstr, DBUtilsBase.enumConnType.CONN_MSSQL, DBUtilsBase.enumSqlDialect.MSSQL)

    '    'sql to execute
    '    Dim sql As String = "exec [getNextEtekNo] @etek_type_id, @next_number OUT"

    '    'first parameter
    '    Dim param As IDataParameter = dbconn.getParameter("etek_type_id", 1)

    '    'second parameter, is an output parameter
    '    Dim param2 As IDataParameter = dbconn.getParameterOut("next_number", String.Empty)

    '    'declare a List(Of IDataParameter) and populate it with the 2 parameters 
    '    Dim params As List(Of IDataParameter) = New List(Of IDataParameter)
    '    params.Add(param)
    '    params.Add(param2)

    '    'execute it
    '    dbconn.executeSQLWithParams(sql, params)

    '    'the call below retrieves param2 value
    '    Assert.IsTrue(param2.Value <> "")

    '    Console.Write(param2.Value)

    'End Sub

#End Region

    <TestMethod()> Public Sub TestDataReaderWithParams()

        Const connstr As String = "Password=ETEK123;Persist Security Info=True;User ID=ETEK;Initial Catalog=ETEK_DEV;Data Source=CHRISTOFOROSL"

        'get a DB utilities object from the above connection string
        Dim dbconn As DBUtilsBase = DBUtilsBase.getFromConnString(connstr, DBUtilsBase.enumConnType.CONN_MSSQL, DBUtilsBase.enumSqlDialect.MSSQL)

        'sql to execute
        Dim sql As String = "select * from h_etek_type where etek_type_id={0}"

        Dim dr As IDataReader = Nothing

        Try

            'open the datareader
            dr = dbconn.getDataReaderWithParams(sql, 1)
            While dr.Read
                Console.WriteLine(dr.GetValue(0))
            End While
        Finally
            dbconn.closeDataReader(dr)

        End Try


    End Sub

End Class
