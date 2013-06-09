﻿
Imports System.Text
Imports System.Collections.Generic
Imports Microsoft.VisualStudio.TestTools.UnitTesting

'<comments>
'************************************************************
' Class autogenerated on 09/06/2013 8:58:38 AM by ModelGenerator
' DO NOT MODIFY CODE IN THIS CLASS!!
'************************************************************
'</comments>
<TestClass()> Public Class EmployeeTestBase

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


    <TestMethod()> Public Sub TestLoadAndSaveEmployee()
		
		ModelContext.beginTrans()
		Try

			Dim pid As Integer = ModelContext.CurrentDBUtils.getIntValue("select top 1 EmployeeId from Employee")
			If pid = 0 Then
				Assert.Inconclusive("No Employee in database, table is empty")
			Else

				Dim pdb As New EmployeeDBMapper()
				Dim p As Employee = pdb.findByKey(pid)
				Dim p2 As Employee = directCast(p.copy(),Employee)

				'Test equality and hash codes
				Assert.AreEqual(p.GetHashCode(), p2.GetHashCode())
				Assert.AreEqual(p, p2)
						
				p.isDirty = True 'force save
				pdb.save(p)
			
				'now reload object from database
				p = Nothing
				p = pdb.findByKey(pid)
            
				'test fields to be equal before and after save
						Assert.IsTrue(p.EmployeeId = p2.EmployeeId,"Expected Field EmployeeId to be equal")
		Assert.IsTrue(p.EmployeeName = p2.EmployeeName,"Expected Field EmployeeName to be equal")
		Assert.IsTrue(p.EmployeeRankId.GetValueOrDefault = p2.EmployeeRankId.GetValueOrDefault,"Expected Field EmployeeRankId to be equal")
		Assert.IsTrue(p.Salary.GetValueOrDefault = p2.Salary.GetValueOrDefault,"Expected Field Salary to be equal")
		Assert.IsTrue(p.Address = p2.Address,"Expected Field Address to be equal")
		Assert.IsTrue(p.Telephone = p2.Telephone,"Expected Field Telephone to be equal")
		Assert.IsTrue(p.Mobile = p2.Mobile,"Expected Field Mobile to be equal")
		Assert.IsTrue(p.IdNumber = p2.IdNumber,"Expected Field IdNumber to be equal")
		Assert.IsTrue(p.SSINumber = p2.SSINumber,"Expected Field SSINumber to be equal")
		Assert.IsTrue(p.HireDate.GetValueOrDefault = p2.HireDate.GetValueOrDefault,"Expected Field HireDate to be equal")
		Assert.IsTrue(p.NumDependents.GetValueOrDefault = p2.NumDependents.GetValueOrDefault,"Expected Field NumDependents to be equal")

            
				'*** Test loading of child/parents **
	Assert.isTrue(p.Rank isNot Nothing)
	Assert.isTrue(p.EmployeeInfo isNot Nothing)
	Assert.isTrue(p.EmployeeProjects isNot Nothing)

            
				
				p.isDirty = True 'to force save
				ModelContext.Current.saveModelObject(p)

				p = ModelContext.Current.loadModelObject(Of Employee)(p.Id)
				

			End If

		Finally
            ModelContext.rollbackTrans() 'Nothing should be saved to the database!
        End Try

    End Sub

End Class

