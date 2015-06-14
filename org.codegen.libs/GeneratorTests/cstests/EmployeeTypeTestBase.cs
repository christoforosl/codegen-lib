﻿
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using org.model.lib.Model;
using CsModelObjects;

///<comments>
/// Template: TestTemplate.csharp.txt
///************************************************************
/// Class autogenerated on 14/06/2015 8:10:24 AM by ModelGenerator
/// DO NOT MODIFY CODE IN THIS CLASS!!
///************************************************************
///</comments>
[TestClass()] public class EmployeeTypeTestBase {

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


    [TestMethod()] public void TestLoadAndSaveEmployeeType() {
		
		ModelContext.beginTrans();
		try {

			object pid  = ModelContext.CurrentDBUtils.getObjectValue("select top 1 EmployeeTypeCode from EmployeeType");
			if (pid == null) {
				Assert.Inconclusive("No EmployeeType in database, table is empty");
			} else {

				CsModelMappers.EmployeeTypeDBMapper pdb = new CsModelMappers.EmployeeTypeDBMapper();
				EmployeeType p = pdb.findByKey(pid);
				EmployeeType p2 = (EmployeeType)p.copy();

				//Test equality and hash codes
				Assert.AreEqual(p.GetHashCode(), p2.GetHashCode());
				Assert.AreEqual(p, p2);
						
				p.isDirty = true ; // force save
				pdb.save(p);
			
				// now reload object from database
				p = null;
				p = pdb.findByKey(pid);
            
				//test fields to be equal before and after save
						Assert.IsTrue(p.PrEmployeeTypeCode == p2.PrEmployeeTypeCode,"Expected Field EmployeeTypeCode to be equal");
		Assert.IsTrue(p.PrEmployeeType == p2.PrEmployeeType,"Expected Field EmployeeType to be equal");

				
				
				p.isDirty = true; //to force save
				ModelContext.Current.saveModelObject(p);

				p = ModelContext.Current.loadModelObject< EmployeeType >(p.Id);
				p.loadObjectHierarchy();
			}

		} finally {
            ModelContext.rollbackTrans(); // 'Nothing should be saved to the database!
        }

   }

}

