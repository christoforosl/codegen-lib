﻿
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using org.model.lib.Model;
using OracleModel;

///<comments>
/// Template: TestTemplate.csharp.txt
///************************************************************
/// Class autogenerated on 12-May-15 11:20:46 AM by ModelGenerator
/// DO NOT MODIFY CODE IN THIS CLASS!!
///************************************************************
///</comments>
[TestClass()] public class RegionTestBase {

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


    [TestMethod()] public void TestLoadAndSaveRegion() {
		
		ModelContext.beginTrans();
		try {

			int pid  = ModelContext.CurrentDBUtils.getIntValue("select top 1 REGION_ID from REGIONS");
			if (pid == 0) {
				Assert.Inconclusive("No Region in database, table is empty");
			} else {

				OracleMappers.RegionDBMapper pdb = new OracleMappers.RegionDBMapper();
				Region p = pdb.findByKey(pid);
				Region p2 = (Region)p.copy();

				//Test equality and hash codes
				Assert.AreEqual(p.GetHashCode(), p2.GetHashCode());
				Assert.AreEqual(p, p2);
						
				p.isDirty = true ; // force save
				pdb.save(p);
			
				// now reload object from database
				p = null;
				p = pdb.findByKey(pid);
            
				//test fields to be equal before and after save
						Assert.IsTrue(p.PrRegionId == p2.PrRegionId,"Expected Field RegionId to be equal");
		Assert.IsTrue(p.PrRegionName == p2.PrRegionName,"Expected Field RegionName to be equal");

				
				
				p.isDirty = true; //to force save
				ModelContext.Current.saveModelObject(p);

				p = ModelContext.Current.loadModelObject< Region >(p.Id);
				
			}

		} finally {
            ModelContext.rollbackTrans(); // 'Nothing should be saved to the database!
        }

   }

}

