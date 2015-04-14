using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CsModelObjects;
using CsModelMappers;
using org.model.lib.Model;
using org.model.lib;

namespace GeneratorTests {
	
	/// <summary>
	/// Test validator functionality. Code below will execute before saving 
	/// the object
	/// </summary>
	public class CsharpEmployeeValidator : IModelObjectValidator {
		public void validate(IModelObject mo) {
			if (mo.isNew) {
				((Employee)mo).PrSSINumber = "12345XX";
			}
		}
	}

	[TestClass]
	public class CSharpObjectTests {

		///
		/// You can use the following additional attributes as you write your tests:
		///
		/// Use ClassInitialize to run code before running the first test in the class
		[ClassInitialize()]
		public static void MyClassInitialize(TestContext testContext) {
			ModelContext.newForUnitTests();
		}

		/// Use ClassCleanup to run code after all tests in a class have run
		[ClassCleanup()]
		public static void MyClassCleanup() {
			ModelContext.release();
		}

		[TestMethod]
		public void createVbRecords() {

			ModelContext.Current().doCascadeDeletes = true;
			ModelContext.beginTrans();
			ModelContext.Current().addGlobalModelValidator(typeof(Employee), typeof(CsharpEmployeeValidator));

			try {

				EmployeeRank er = EmployeeRankFactory.Create();
				er.PrRank = "My New Rank";

				Employee e = EmployeeFactory.Create();
				e.PrRank = er;
				e.PrEmployeeName = "test employee";
				e.PrSalary = 100m;
				e.PrSSINumber = "1030045";
				e.PrTelephone = "2234455";
				e.PrHireDate = new DateTime(DateTime.Now.Year, 1, 1);

				e.EmployeeProjectAdd(EmployeeProjectFactory.Create());
				EmployeeProject emplProj = e.EmployeeProjectGetAt(0);
				emplProj.PrAssignDate = new DateTime(DateTime.Now.Year, 3, 1);
				emplProj.PrEndDate = new DateTime(DateTime.Now.Year, 6, 1);
				emplProj.PrEPProjectId = 1;
				emplProj.PrProject = ProjectFactory.Create();
				emplProj.PrProject.PrProjectName = "MyProject";

				Assert.IsTrue(e.isNew);
				Assert.IsTrue(e.isDirty);
				Assert.IsTrue(e.NeedsSave);

				// 3 ways to persist to database
				// method 1: use ModelContext.Current().save

				Assert.IsTrue(e.CreateDate == null, "Before save, created date is null");
				Assert.IsTrue(e.UpdateDate == null, "Before save, UpdateDate is not null");
				ModelContext.Current().saveModelObject(e);
				Assert.IsTrue(e.PrSSINumber == "12345XX", "12345XX value in PrSSINumber is Proof that validator was called");
				Assert.IsTrue(e.CreateDate != null, "Before save, created date is not null");
				Assert.IsTrue(e.UpdateDate != null, "Before save, UpdateDate is not null");
				Assert.IsTrue(e.CreateUser != null, "Before save, CreateUser date is not null");
				Assert.IsTrue(e.UpdateUser != null, "Before save, UpdateUser is not null");
				Assert.IsTrue(e.UpdateDate == e.CreateDate, "update date = create date after saving new");
				Assert.IsTrue(e.UpdateUser == e.CreateUser, "update date = create date after saving new");

				long x = e.PrEmployeeId;
				Assert.IsFalse(e.isNew, "After save, model object isNew property must return false");
				Assert.IsFalse(e.isDirty, "After save to db, model object isDirty property must return false");

				e = EmployeeDataUtils.findByKey(x);

				Assert.IsNotNull(e, "New employee not found");

				Assert.IsFalse(e.isNew, "After load from db, model object isNew property returns false");
				Assert.IsFalse(e.isDirty, "After load from db, model object isDirty property returns false");

				Assert.AreEqual(e.PrRank.PrRank, "My New Rank");
				Assert.AreEqual(e.PrSalary, 100m);
				Assert.AreEqual(e.PrEmployeeName, "test employee");
				Assert.AreEqual(e.PrSSINumber, "12345XX");
				Assert.AreEqual(e.PrHireDate, new DateTime(2015, 1, 1));
				Assert.AreEqual(e.PrEmployeeProjects.ToList().Count, 1);
				Assert.AreEqual(e.EmployeeProjectGetAt(0).PrProject.PrProjectName, "MyProject");

				//change some values on child and parent objects
				e.EmployeeProjectGetAt(0).PrEndDate = new DateTime(DateTime.Now.Year, 6, 1);
				e.EmployeeProjectGetAt(0).PrProject.PrProjectName = "MyProject Updated"; // here we are updating parent record of child object of employee!
				Assert.IsTrue(e.NeedsSave, "After changing parent or child obejcts values, e.NeedsSave must be true");
				Assert.IsFalse(e.isDirty, "After changing parent or child obejcts values, e.isDirty must be false since we did not change anything on the Model Object");

				// method 2: call [ModelObject]DataUtils.save
				EmployeeDataUtils.saveEmployee(e);
				//Assert.IsTrue(e.UpdateDate > e.CreateDate, "after update of record, update must be date > create date ");
				// note that above test cannot be sucess since save is happening too fast

				Assert.AreEqual(e.EmployeeProjectGetAt(0).PrEndDate, new DateTime(DateTime.Now.Year, 6, 1));
				Assert.AreEqual(e.EmployeeProjectGetAt(0).PrProject.PrProjectName, "MyProject Updated", "Expected to have parent record of child updated!");

				e.PrSSINumber = "XXXXX";
				Assert.IsTrue(e.NeedsSave, "After changing value, e.NeedsSave must be true");
				Assert.IsTrue(e.isDirty, "After changing value e.isDirty must be true");

				// method 3: call [ModelObject]dbMapper.save
				new EmployeeDBMapper().saveEmployee(e);
				e = EmployeeDataUtils.findByKey(x);
				Assert.AreEqual(e.PrSSINumber, "XXXXX");
				Assert.AreEqual(e.EmployeeProjectGetAt(0).PrEndDate, new DateTime(DateTime.Now.Year, 6, 1));
				Assert.AreEqual(e.EmployeeProjectGetAt(0).PrProject.PrProjectName, "MyProject Updated", "Expected to have parent record of child updated!");

				e.EmployeeProjectsClear();
				Assert.AreEqual(e.PrEmployeeProjects.ToList().Count, 0, "Expected to have no Projects linked after call to clear");
				EmployeeDataUtils.saveEmployee(e);

				e = EmployeeDataUtils.findByKey(x);
				Assert.AreEqual(e.PrEmployeeProjects.ToList().Count, 0, "Expected to have no Projects linked, after reloading from db");

				EmployeeDataUtils.deleteEmployee(e);
				e = EmployeeDataUtils.findByKey(x);
				Assert.IsNull(e, "New employee must have been deleted!");

				// now let's test string primary key
				EmployeeType et = EmployeeTypeFactory.Create();
				et.PrEmployeeType = "A Description";
				et.PrEmployeeTypeCode = "XX";

				EmployeeType et1 = EmployeeTypeFactory.Create();
				et1.PrEmployeeType = "A Description 1";
				et1.PrEmployeeTypeCode = "XX1";

				EmployeeType et2 = EmployeeTypeFactory.Create();
				et2.PrEmployeeType = "A Description 2";
				et2.PrEmployeeTypeCode = "XX2";

				EmployeeTypeDataUtils.saveEmployeeType(et, et1, et2);

				et2 = EmployeeTypeDataUtils.findByKey("XX2");
				Assert.IsNotNull(et2, "New employeetype must have been created!");
				et1 = EmployeeTypeDataUtils.findByKey("XX1");
				Assert.IsNotNull(et1, "New employeetype must have been created!");

			} finally {
				ModelContext.rollbackTrans();
			}

		

		}
	}
}
