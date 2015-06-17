using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using org.model.lib.Model;
using org.model.lib;
using System.Threading;
using System.Globalization;
using OracleModel;
using OracleMappers;

namespace GeneratorTests {

	/// <summary>
	/// Test validator functionality. Code below will execute before saving 
	/// the object
	/// </summary>
	public class EmployeeValidator : IModelObjectValidator {
		public void validate(IModelObject mo) {
			if (mo.isNew) {
				((Employee)mo).PrPhoneNumber = "12345XX";
			}
		}
	}

	[TestClass]
	public class VBObjectTests {

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
		public void createOracletestRecords() {

            ModelContext.Current.config.DoCascadeDeletes = true;
			ModelContext.beginTrans();
			ModelContext.Current.addGlobalModelValidator(typeof(Employee), typeof(EmployeeValidator));

			try {
								
				Employee e = EmployeeFactory.Create();
				e.PrFirstName= "test";
                e.PrLastName = "Lastname";
				e.PrSALARY = 100m;
                e.PrEMAIL = "test@test.com.cy";
				e.PrPhoneNumber= "1030045";
				e.PrHireDate = new DateTime(DateTime.Now.Year, 1, 1);
                e.PrJobId = JobDataUtils.findList()[0].PrJobId;

                e.PrTrainingHistoryAdd( EmployeeTrainingHistoryFactory.Create() );
                EmployeeTrainingHistory emplProj = e.PrTrainingHistoryGetAt(0);
				emplProj.PrDateFrom = new DateTime(DateTime.Now.Year, 3, 1);
                emplProj.PrDateTo = new DateTime(DateTime.Now.Year, 6, 1);

                emplProj.PrTrainingCourse = TrainingCourseFactory.Create();
                emplProj.PrTrainingCourse.PrCODE = "X1";
                emplProj.PrTrainingCourse.PrDescrEn = "New Course";
                emplProj.PrTrainingCourse.PrDescrGr = "Νέο";
				Assert.IsTrue(e.isNew);
				Assert.IsTrue(e.isDirty);
				Assert.IsTrue(e.NeedsSave);
				
				// 3 ways to persist to database
				// method 1: use ModelContext.Current().save

				Assert.IsTrue(e.CreateDate == null, "Before save, created date is null");
				Assert.IsTrue(e.UpdateDate == null, "Before save, UpdateDate is not null");
				ModelContext.Current.saveModelObject(e);
				Assert.IsTrue(e.PrPhoneNumber == "12345XX", "12345XX value in PrPhoneNumber is Proof that validator was called");
				Assert.IsTrue(e.CreateDate != null, "Before save, created date is not null");
				Assert.IsTrue(e.UpdateDate != null, "Before save, UpdateDate is not null");
				Assert.IsTrue(e.CreateUser != null, "Before save, CreateUser date is not null");
				Assert.IsTrue(e.UpdateUser != null, "Before save, UpdateUser is not null");
				//Assert.IsTrue(e.UpdateDate.Value.Ticks == e.CreateDate.Value.Ticks, "update date = create date after saving new");
				Assert.IsTrue(e.UpdateUser == e.CreateUser, "update date = create date after saving new");

				long x = e.PrEmployeeId;
				Assert.IsFalse(e.isNew, "After save, model object isNew property must return false");
				Assert.IsFalse(e.isDirty, "After save to db, model object isDirty property must return false");

				e = EmployeeDataUtils.findByKey(x);

				Assert.IsNotNull(e, "New employee not found");
				
				Assert.IsFalse(e.isNew, "After load from db, model object isNew property returns false");
				Assert.IsFalse(e.isDirty, "After load from db, model object isDirty property returns false");

                Assert.AreEqual(e.PrDepartment.PrDepartmentName, "My New Dept");
				Assert.AreEqual(e.PrSALARY, 100m);
                Assert.AreEqual(e.PrLastName, "Lastname");
				Assert.AreEqual(e.PrPhoneNumber, "12345XX");
				Assert.AreEqual(e.PrHireDate, new DateTime(2015, 1, 1));
				Assert.AreEqual(e.PrTrainingHistory.ToList().Count, 1);
                Assert.AreEqual(e.PrTrainingHistoryGetAt(0).PrTrainingCourse.PrDescrEn, "New Course");
				
				//change some values on child and parent objects
                e.PrTrainingHistoryGetAt(0).PrDateTo = new DateTime(DateTime.Now.Year, 6, 1);
                e.PrTrainingHistoryGetAt(0).PrTrainingCourse.PrDescrEn = "New Course Updated"; // here we are updating parent record of child object of employee!
				Assert.IsTrue(e.NeedsSave, "After changing parent or child obejcts values, e.NeedsSave must be true");
				Assert.IsFalse(e.isDirty, "After changing parent or child obejcts values, e.isDirty must be false since we did not change anything on the Model Object");
				
				// method 2: call [ModelObject]DataUtils.save
				EmployeeDataUtils.saveEmployee(e);
				//Assert.IsTrue(e.UpdateDate > e.CreateDate, "after update of record, update must be date > create date ");
				// note that above test cannot be sucess since save is happening too fast

                Assert.AreEqual(e.PrTrainingHistoryGetAt(0).PrDateTo, new DateTime(DateTime.Now.Year, 6, 1));
                Assert.AreEqual(e.PrTrainingHistoryGetAt(0).PrTrainingCourse.PrDescrEn, "New Course Updated", "Expected to have parent record of child updated!");

				e.PrPhoneNumber = "XXXXX";
				Assert.IsTrue(e.NeedsSave, "After changing value, e.NeedsSave must be true");
				Assert.IsTrue(e.isDirty, "After changing value e.isDirty must be true");
				
				// method 3: call [ModelObject]dbMapper.save
				new EmployeeDBMapper().saveEmployee(e);
				e = EmployeeDataUtils.findByKey(x);
				Assert.AreEqual(e.PrPhoneNumber, "XXXXX");
                Assert.AreEqual(e.PrTrainingHistoryGetAt(0).PrDateTo, new DateTime(DateTime.Now.Year, 6, 1));
                Assert.AreEqual(e.PrTrainingHistoryGetAt(0).PrTrainingCourse.PrDescrEn, "New Course Updated", "Expected to have parent record of child updated!");

                e.PrTrainingHistoryClear();
                Assert.AreEqual(e.PrTrainingHistory.ToList().Count, 0, "Expected to have no Projects linked after call to clear");
				EmployeeDataUtils.saveEmployee(e);
				
				e = EmployeeDataUtils.findByKey(x);
                Assert.AreEqual(e.PrTrainingHistory.ToList().Count, 0, "Expected to have no Projects linked, after reloading from db");

				EmployeeDataUtils.deleteEmployee(e);
				e = EmployeeDataUtils.findByKey(x);
				Assert.IsNull(e, "New employee must have been deleted!");

				// now let's test string primary key
				Country et = CountryFactory.Create();
				et.PrCountryName = "A Description";
				et.PrCountryId = "XX";

				Country et1 = CountryFactory.Create();
                et1.PrCountryName = "A Description 1";
                et1.PrCountryId = "XX1";

				Country et2 = CountryFactory.Create();
                et2.PrCountryName = "A Description 2";
                et2.PrCountryId = "XX2";

				CountryDataUtils.saveCountry(et, et1, et2);

				et2 = CountryDataUtils.findByKey("XX2");
				Assert.IsNotNull(et2, "New Country must have been created!");
				et1 = CountryDataUtils.findByKey("XX1");
				Assert.IsNotNull(et1, "New Country must have been created!");

			} finally {
				ModelContext.rollbackTrans();
			}

		}
	}
}
