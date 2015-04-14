﻿
Imports System.Text
Imports System.Collections.Generic
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports VbBusObjects

'<comments>
'Template: TestTemplate.visualbasic.txt
'************************************************************
' Class autogenerated on 14-04-2015 10:01:51 by ModelGenerator
' DO NOT MODIFY CODE IN THIS CLASS
'************************************************************
'</comments>
<TestClass()> Public Class EmployeeInfoTestBase

    Private testContextInstance As TestContext

    '''<summary>
    '''Gets/sets the test context which provides
    '''information about and functionality for the current test run.
    '''</summary>
    Public Property TestContext() As TestContext

#Region "Additional test attributes"
    '
    ' You can use the following additional attributes as you write your tests:
    '
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


    <TestMethod()> Public Sub TestLoadAndSaveEmployeeInfo()
		
		ModelContext.beginTrans()
		Try

			Dim pid As long = ModelContext.CurrentDBUtils.getIntValue("select top 1 EmployeeInfoId from EmployeeInfo")
			If pid = 0 Then
				Assert.Inconclusive("No EmployeeInfo in database, table is empty")
			Else

				Dim pdb As New EmployeeInfoDBMapper()
				Dim p As EmployeeInfo = pdb.findByKey(pid)
				Dim p2 As EmployeeInfo = directCast(p.copy(),EmployeeInfo)

				'Test equality and hash codes
				Assert.AreEqual(p.GetHashCode(), p2.GetHashCode())
				Assert.AreEqual(p, p2)
						
				p.isDirty = True 'force save
				pdb.save(p)
			
				'now reload object from database
				p = Nothing
				p = pdb.findByKey(pid)
            
				'test fields to be equal before and after save
						Assert.IsTrue(p.EmployeeInfoId = p2.EmployeeInfoId,"Expected Field EmployeeInfoId to be equal")
		Assert.IsTrue(p.EIEmployeeId.GetValueOrDefault = p2.EIEmployeeId.GetValueOrDefault,"Expected Field EIEmployeeId to be equal")
		Assert.IsTrue(p.Salary.GetValueOrDefault = p2.Salary.GetValueOrDefault,"Expected Field Salary to be equal")
		Assert.IsTrue(p.Address = p2.Address,"Expected Field Address to be equal")

            
				
            
				
				p.isDirty = True 'to force save
				ModelContext.Current.saveModelObject(p)

				p = ModelContext.Current.loadModelObject(Of EmployeeInfo)(p.Id)
				

			End If

		Finally
            ModelContext.rollbackTrans() 'Nothing should be saved to the database!
        End Try

    End Sub

End Class

