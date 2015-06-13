﻿
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using org.model.lib.Model;
using CsModelObjects;

///<comments>
/// Template: TestTemplate.csharp.txt
///************************************************************
/// Class autogenerated on 13/06/2015 3:52:50 PM by ModelGenerator
/// DO NOT MODIFY CODE IN THIS CLASS!!
///************************************************************
///</comments>
[TestClass()] public class EmployeeTestBase {

    ///<summary>
    ///Gets or sets the test context which provides
    ///information about and functionality for the current test run.
    ///</summary>
    public TestContext TestContext {get;set;}
    

#region "Additional test attributes"
    ///
    /// You can use the following additional attributes as you write your tests:
    ///
    /// Use ClassInitialize to run code before running the first test in the class
    [ClassInitialize()] public static void MyClassInitialize(TestContext testContext ) {
        ModelContext.newForUnitTests();
    }
    
    /// Use ClassCleanup to run code after all tests in a class have run
    [ClassCleanup()] public static void MyClassCleanup() {
        ModelContext.release();
    }


    //Use TestInitialize to run code before running each test
    [TestInitialize()]
    public void MyTestInitialize() {
    }
    
    // Use TestCleanup to run code after each test has run
    [TestCleanup()] public void MyTestCleanup() {
    
	}
    
#endregion


    [TestMethod()] public void TestLoadAndSaveEmployee() {
		
		ModelContext.beginTrans();
		try {

			object pid  = ModelContext.CurrentDBUtils.getObjectValue("select top 1 EmployeeId from Employee");
			if (pid == null) {
				Assert.Inconclusive("No Employee in database, table is empty");
			} else {

				CsModelMappers.EmployeeDBMapper pdb = new CsModelMappers.EmployeeDBMapper();
				Employee p = pdb.findByKey(pid);
				Employee p2 = (Employee)p.copy();

				//Test equality and hash codes
				Assert.AreEqual(p.GetHashCode(), p2.GetHashCode());
				Assert.AreEqual(p, p2);
						
				p.isDirty = true ; // force save
				pdb.save(p);
			
				// now reload object from database
				p = null;
				p = pdb.findByKey(pid);
            
				//test fields to be equal before and after save
						Assert.IsTrue(p.PrEmployeeId == p2.PrEmployeeId,"Expected Field EmployeeId to be equal");
		Assert.IsTrue(p.PrEmployeeName == p2.PrEmployeeName,"Expected Field EmployeeName to be equal");
		Assert.IsTrue(p.PrEmployeeRankId.GetValueOrDefault() == p2.PrEmployeeRankId.GetValueOrDefault(),"Expected Field EmployeeRankId to be equal");
		Assert.IsTrue(p.PrSalary.GetValueOrDefault() == p2.PrSalary.GetValueOrDefault(),"Expected Field Salary to be equal");
		Assert.IsTrue(p.PrAddress == p2.PrAddress,"Expected Field Address to be equal");
		Assert.IsTrue(p.PrTelephone == p2.PrTelephone,"Expected Field Telephone to be equal");
		Assert.IsTrue(p.PrMobile == p2.PrMobile,"Expected Field Mobile to be equal");
		Assert.IsTrue(p.PrIdNumber == p2.PrIdNumber,"Expected Field IdNumber to be equal");
		Assert.IsTrue(p.PrSSINumber == p2.PrSSINumber,"Expected Field SSINumber to be equal");
		Assert.IsTrue(p.PrHireDate.GetValueOrDefault() == p2.PrHireDate.GetValueOrDefault(),"Expected Field HireDate to be equal");
		Assert.IsTrue(p.PrNumDependents.GetValueOrDefault() == p2.PrNumDependents.GetValueOrDefault(),"Expected Field NumDependents to be equal");
		Assert.IsTrue(p.PrEmployeeTypeCode == p2.PrEmployeeTypeCode,"Expected Field EmployeeTypeCode to be equal");
		Assert.IsTrue(p.CreateDate.GetValueOrDefault() == p2.CreateDate.GetValueOrDefault(),"Expected Field CreateDate to be equal");
		Assert.IsFalse(p.UpdateDate.GetValueOrDefault() == p2.UpdateDate.GetValueOrDefault(),"Expected Field UpdateDate NOT to be equal");
		Assert.IsTrue(p.CreateUser == p2.CreateUser,"Expected Field CreateUser to be equal");
		// skip update user!
		Assert.IsTrue(p.PrSampleGuidField.GetValueOrDefault() == p2.PrSampleGuidField.GetValueOrDefault(),"Expected Field SampleGuidField to be equal");
		Assert.IsTrue(p.PrIsActive == p2.PrIsActive,"Expected Field IsActive to be equal");
		Assert.IsTrue(p.PrSampleBigInt.GetValueOrDefault() == p2.PrSampleBigInt.GetValueOrDefault(),"Expected Field SampleBigInt to be equal");
		Assert.IsTrue(p.PrSampleSmallInt.GetValueOrDefault() == p2.PrSampleSmallInt.GetValueOrDefault(),"Expected Field SampleSmallInt to be equal");
		Assert.IsTrue(p.PrSampleNumericFieldInt.GetValueOrDefault() == p2.PrSampleNumericFieldInt.GetValueOrDefault(),"Expected Field SampleNumericFieldInt to be equal");
		Assert.IsTrue(p.PrSampleNumericField2Decimals.GetValueOrDefault() == p2.PrSampleNumericField2Decimals.GetValueOrDefault(),"Expected Field SampleNumericField2Decimals to be equal");

					Assert.IsTrue(p.PrRank != null);
	Assert.IsTrue(p.PrEmployeeInfo != null);
	Assert.IsTrue(p.PrEmployeeProjects != null);

				
				p.isDirty = true; //to force save
				ModelContext.Current.saveModelObject(p);

				p = ModelContext.Current.loadModelObject< Employee >(p.Id);
				
			}

		} finally {
            ModelContext.rollbackTrans(); // 'Nothing should be saved to the database!
        }

   }

}

