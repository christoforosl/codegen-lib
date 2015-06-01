﻿
Imports System.Text
Imports System.Collections.Generic
Imports Microsoft.VisualStudio.TestTools.UnitTesting


'<comments>
'Template: TestTemplate.visualbasic.txt
'************************************************************
' Class autogenerated on 01/06/2015 7:02:06 PM by ModelGenerator
' DO NOT MODIFY CODE IN THIS CLASS
'************************************************************
'</comments>
<TestClass()> Public Class EmployeeRankTestBase

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

    <TestMethod()> Public Sub TestLoadAndSaveEmployeeRank()
		
		ModelContext.beginTrans()
		Try

			Dim pid As Object = ModelContext.CurrentDBUtils.getObjectValue("select top 1 RankId from EmployeeRank")
			If pid is Nothing Then
				Assert.Inconclusive("No EmployeeRank in database, table is empty")
			Else

				Dim pdb As New EmployeeRankDBMapper()
				Dim p As EmployeeRank = pdb.findByKey(pid)
				Dim p2 As EmployeeRank = directCast(p.copy(),EmployeeRank)

				'Test equality and hash codes
				Assert.AreEqual(p.GetHashCode(), p2.GetHashCode())
				Assert.AreEqual(p, p2)
						
				p.isDirty = True 'force save
				pdb.save(p)
			
				'now reload object from database
				p = Nothing
				p = pdb.findByKey(pid)
            
				'test fields to be equal before and after save
						Assert.IsTrue(p.PrRankId = p2.PrRankId,"Expected Field PrRankId to be equal")
		Assert.IsTrue(p.PrRank = p2.PrRank,"Expected Field PrRank to be equal")

				
				
				p.isDirty = True 'to force save
				ModelContext.Current.saveModelObject(p)
				p = ModelContext.Current.loadModelObject(Of EmployeeRank)(p.Id)
				
			End If

		Finally
            ModelContext.rollbackTrans() 'Nothing should be saved to the database!
        End Try

    End Sub

End Class

