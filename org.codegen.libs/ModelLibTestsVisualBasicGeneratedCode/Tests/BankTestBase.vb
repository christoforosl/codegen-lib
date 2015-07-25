﻿
Imports System.Text
Imports System.Collections.Generic
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports Newtonsoft.Json

'<comments>
'Template: TestTemplate.visualbasic.txt
'************************************************************
' Class autogenerated on 12/07/2015 11:44:20 AM by ModelGenerator
' DO NOT MODIFY CODE IN THIS CLASS
'************************************************************
'</comments>
<TestClass()> Public Class BankTestBase

    Private testContextInstance As TestContext

    '''<summary>
    '''Gets/sets the test context which provides
    '''information about and functionality for the current test run.
    '''</summary>
    Public Property TestContext() As TestContext

#Region "Additional test attributes"
    ' You can use the following additional attributes as you write your tests:
    ' Use ClassInitialize to run code before running the first test in the class
    <ClassInitialize()> Public Shared Sub MyClassInitialize(ByVal testContext As TestContext)
        ModelContext.newForUnitTests()
    End Sub
    '
    ' Use ClassCleanup to run code after all tests in a class have run
    <ClassCleanup()> Public Shared Sub MyClassCleanup()
        ModelContext.release()
    End Sub

    'Use TestInitialize to run code before running each test
    '<TestInitialize()> _
    'Public Sub MyTestInitialize()
    'End Sub
    '
    ' Use TestCleanup to run code after each test has run
    <TestCleanup()> Public Sub MyTestCleanup()
    End Sub
    '
#End Region

    <TestMethod()> Public Sub TestLoadAndSaveBank()
		
		ModelContext.beginTrans()
		Try

			Dim pdb As New BankDBMapper()
			dim count as Long = pdb.RecordCount()
			
			If pdb.SelectFromObjectName <> pdb.ManagedTableName Then
                Dim countFromSelectObject As Long = pdb.dbConn.getLngValue("select count(*) from " & pdb.SelectFromObjectName)
                Assert.AreEqual(count, countFromSelectObject, _ 
					"Count of records in managedTableName {0} and SelectFromObjectName {1} should be equal, as there needs to be exactly 1 to 1 match between records in managed table and selectFromObject.", _
						pdb.ManagedTableName, pdb.SelectFromObjectName)
            End If
		
			If count = 0 Then
				Assert.Inconclusive("No Bank in database, table is empty")
			Else
				Dim pid As Object = ModelContext.CurrentDBUtils.getObjectValue("select top 1 " + pdb.pkFieldName + " from " + pdb.ManagedTableName)
				
				Dim p As Bank = pdb.findByKey(pid)
				Dim p2 As Bank = directCast(p.copy(),Bank)

				'Test equality and hash codes
				Assert.AreEqual(p.GetHashCode(), p2.GetHashCode())
				Assert.AreEqual(p, p2)
						
				p.isDirty = True 'force save
				pdb.save(p)
			
				'now reload object from database
				p = Nothing
				p = pdb.findByKey(pid)
            
				'test fields to be equal before and after save
				Assert.IsTrue(p.PrBANKID=p2.PrBANKID,"Expected Field BANKID to be equal")
				Assert.IsTrue(p.PrBankName=p2.PrBankName,"Expected Field BankName to be equal")
				Assert.IsTrue(p.PrBankCode=p2.PrBankCode,"Expected Field BankCode to be equal")
				Assert.IsTrue(p.PrBankSWIFTCode=p2.PrBankSWIFTCode,"Expected Field BankSWIFTCode to be equal")

				
				p.isDirty = True 'to force save
				ModelContext.Current.saveModelObject(p)
				p = ModelContext.Current.loadModelObject(Of Bank)(p.Id)
				
				p.loadObjectHierarchy()

				Dim xcs As  Newtonsoft.Json.JsonSerializerSettings = New JsonSerializerSettings()
                xcs.ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                Dim json As String = JsonConvert.SerializeObject(p, Formatting.Indented, xcs)
				Dim jf As New System.IO.FileInfo(".\Bank.json")
				System.IO.File.WriteAllText(jf.FullName, json)

				If pdb.isPrimaryKeyAutogenerated Then
					p.isNew = True
					p.isDirty = True
					try 
						pdb.save(p)
					catch e as System.Exception 
						Assert.IsTrue(e.Message.ToUpper().Contains("UNIQUE INDEX") OrElse e.Message.Contains("Violation of UNIQUE KEY constraint"),
							"Insert statement produced error other than violation of unique key.")

					end try
				End If


			End If

		Finally
            ModelContext.rollbackTrans() 'Nothing should be saved to the database!
        End Try

    End Sub

End Class
