
using NUnit.Framework;
using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using ModelLibVBGenCode.VbBusObjects;
using ModelLibVBGenCode.VbBusObjects.DBMappers;
using org.model.lib.Model;
using org.model.lib;
using System.Threading;
using System.Globalization;

namespace GeneratorTests.VB {
	
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

	[NUnit.Framework.TestFixture]
	public class VBObjectTests {

		///
		/// You can use the following additional attributes as you write your tests:
		///
		/// Use ClassInitialize to run code before running the first test in the class
		[NUnit.Framework.SetUp]
		public static void MyClassInitialize() {
			
            org.model.lib.db.DBUtils.Current().ConnString="Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=modelTest;Data Source=.\\SQLEXPRESS";
            ModelContext.newForUnitTests();
		}

		/// Use ClassCleanup to run code after all tests in a class have run
		[NUnit.Framework.TearDown]
		public static void MyClassCleanup() {
			ModelContext.release();
		}


		[NUnit.Framework.Test]
		public void createVbRecords() {

			ModelContext.Current.config.DoCascadeDeletes = true;
			ModelContext.beginTrans();
			ModelContext.Current.addGlobalModelValidator(typeof(Employee), typeof(CsharpEmployeeValidator));

			try {

				EmployeeRank er = EmployeeRankFactory.Create();
				er.PrRank = "My New Rank";

				Employee employee = EmployeeFactory.Create();
				employee.PrRank = er;
				employee.PrEmployeeName = "test employee";
				employee.PrSalary = 100m;
				employee.PrSSINumber = "1030045";
				employee.PrTelephone = "2234455";
				employee.PrHireDate = new DateTime(DateTime.Now.Year, 1, 1);
                
                Guid g = Guid.NewGuid();
                employee.PrSampleGuidField = g;
                employee.PrEmployeeProjectAdd(EmployeeProjectFactory.Create());
				EmployeeProject emplProj = employee.PrEmployeeProjectGetAt(0);
				emplProj.PrAssignDate = new DateTime(DateTime.Now.Year, 3, 1);
				emplProj.PrEndDate = new DateTime(DateTime.Now.Year, 6, 1);
				emplProj.PrEPProjectId = 1;
				emplProj.PrProject = ProjectFactory.Create();
				emplProj.PrProject.PrProjectName = "MyProject";

				NUnit.Framework.Assert.IsTrue(employee.isNew);
				NUnit.Framework.Assert.IsTrue(employee.isDirty);
				NUnit.Framework.Assert.IsTrue(employee.NeedsSave);

				// 3 ways to persist to database
				// method 1: use ModelContext.Current().save

				NUnit.Framework.Assert.IsTrue(employee.CreateDate == null, "Before save, created date is null");
				NUnit.Framework.Assert.IsTrue(employee.UpdateDate == null, "Before save, UpdateDate is not null");
				
                ModelContext.Current.saveModelObject(employee);

				NUnit.Framework.Assert.IsTrue(employee.PrSSINumber == "12345XX", "12345XX value in PrSSINumber is Proof that validator was called");
				NUnit.Framework.Assert.IsTrue(employee.CreateDate != null, "Before save, created date is not null");
				NUnit.Framework.Assert.IsTrue(employee.UpdateDate != null, "Before save, UpdateDate is not null");
				NUnit.Framework.Assert.IsTrue(employee.CreateUser != null, "Before save, CreateUser date is not null");
				NUnit.Framework.Assert.IsTrue(employee.UpdateUser != null, "Before save, UpdateUser is not null");
                NUnit.Framework.Assert.IsTrue(employee.UpdateDate.GetValueOrDefault().ToString("dd/MM/yyyy") == employee.CreateDate.GetValueOrDefault().ToString("dd/MM/yyyy"), "update date = create date after saving new");
				NUnit.Framework.Assert.IsTrue(employee.UpdateUser == employee.CreateUser, "update date = create date after saving new");

				long x = employee.PrEmployeeId;
				NUnit.Framework.Assert.IsFalse(employee.isNew, "After save, model object isNew property must return false");
				NUnit.Framework.Assert.IsFalse(employee.isDirty, "After save to db, model object isDirty property must return false");

				employee = EmployeeDataUtils.findByKey(x);

				NUnit.Framework.Assert.IsNotNull(employee, "New employee not found");

				NUnit.Framework.Assert.IsFalse(employee.isNew, "After load from db, model object isNew property returns false");
				NUnit.Framework.Assert.IsFalse(employee.isDirty, "After load from db, model object isDirty property returns false");
                NUnit.Framework.Assert.AreEqual(employee.PrSampleGuidField, g);
				NUnit.Framework.Assert.AreEqual(employee.PrRank.PrRank, "My New Rank");
				NUnit.Framework.Assert.AreEqual(employee.PrSalary, 100m);
				NUnit.Framework.Assert.AreEqual(employee.PrEmployeeName, "test employee");
				NUnit.Framework.Assert.AreEqual(employee.PrSSINumber, "12345XX");
				NUnit.Framework.Assert.AreEqual(employee.PrHireDate, new DateTime(2015, 1, 1));
				NUnit.Framework.Assert.AreEqual(employee.PrEmployeeProjects.ToList().Count, 1);
				NUnit.Framework.Assert.AreEqual(employee.PrEmployeeProjectGetAt(0).PrProject.PrProjectName, "MyProject");

				//change some values on child and parent objects
				employee.PrEmployeeProjectGetAt(0).PrEndDate = new DateTime(DateTime.Now.Year, 6, 1);
                employee.PrEmployeeProjectGetAt(0).PrProject.PrProjectName = "MyProject Updated"; // here we are updating parent record of child object of employee!
				NUnit.Framework.Assert.IsTrue(employee.NeedsSave, "After changing parent or child obejcts values, e.NeedsSave must be true");
				NUnit.Framework.Assert.IsFalse(employee.isDirty, "After changing parent or child obejcts values, e.isDirty must be false since we did not change anything on the Model Object");

				// method 2: call [ModelObject]DataUtils.save
				EmployeeDataUtils.saveEmployee(employee);

                employee = EmployeeDataUtils.findByKey(x);
				//NUnit.Framework.Assert.IsTrue(e.UpdateDate > e.CreateDate, "after update of record, update must be date > create date ");
				// note that above test cannot be sucess since save is happening too fast

                NUnit.Framework.Assert.AreEqual(employee.PrEmployeeProjectGetAt(0).PrEndDate, new DateTime(DateTime.Now.Year, 6, 1));
                NUnit.Framework.Assert.AreEqual(employee.PrEmployeeProjectGetAt(0).PrProject.PrProjectName, "MyProject Updated", "Expected to have parent record of child updated!");

				employee.PrSSINumber = "XXXXX";
				NUnit.Framework.Assert.IsTrue(employee.NeedsSave, "After changing value, e.NeedsSave must be true");
				NUnit.Framework.Assert.IsTrue(employee.isDirty, "After changing value e.isDirty must be true");

				// method 3: call [ModelObject]dbMapper.save
				new EmployeeDBMapper().save(employee);
				employee = EmployeeDataUtils.findByKey(x);
				NUnit.Framework.Assert.AreEqual(employee.PrSSINumber, "XXXXX");
                NUnit.Framework.Assert.AreEqual(employee.PrEmployeeProjectGetAt(0).PrEndDate, new DateTime(DateTime.Now.Year, 6, 1));
                NUnit.Framework.Assert.AreEqual(employee.PrEmployeeProjectGetAt(0).PrProject.PrProjectName, "MyProject Updated", "Expected to have parent record of child updated!");

				employee.PrEmployeeProjectsClear();
				NUnit.Framework.Assert.AreEqual(employee.PrEmployeeProjects.ToList().Count, 0, "Expected to have no Projects linked after call to clear");
				EmployeeDataUtils.saveEmployee(employee);

				employee = EmployeeDataUtils.findByKey(x);
				NUnit.Framework.Assert.AreEqual(employee.PrEmployeeProjects.ToList().Count, 0, "Expected to have no Projects linked, after reloading from db");

                List<Employee> empls = EmployeeDataUtils.findList("EmployeeName={0} and Salary between {1} and {2} and HireDate={3}", "test employee", 0, 100000, new DateTime(2015, 1, 1));
                NUnit.Framework.Assert.IsTrue(empls.Count > 0);

				EmployeeDataUtils.deleteEmployee(employee);
				employee = EmployeeDataUtils.findByKey(x);
				NUnit.Framework.Assert.IsNull(employee, "New employee must have been deleted!");

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
				NUnit.Framework.Assert.IsNotNull(et2, "New employeetype must have been created!");
				et1 = EmployeeTypeDataUtils.findByKey("XX1");
				NUnit.Framework.Assert.IsNotNull(et1, "New employeetype must have been created!");
                

			} finally {
				ModelContext.rollbackTrans();
			}

		

		}
	}
}
