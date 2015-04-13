using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModelLibVBGenCode.VbBusObjects;
using ModelLibVBGenCode.VbBusObjects.DBMappers;
using org.model.lib.Model;

namespace GeneratorTests {

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
		public void createVbRecords() {

			ModelContext.Current().doCascadeDeletes = true;
			ModelContext.beginTrans();

			try {
				EmployeeRank er = EmployeeRankFactory.Create();
				er.Rank = "My New Rank";

				Employee e = EmployeeFactory.Create();
				e.Rank = er;
				e.EmployeeName = "test employee";
				e.Salary = 100m;
				e.SSINumber = "1030045";
				e.Telephone = "2234455";
				e.AddEmployeeProject(EmployeeProjectFactory.Create());
				e.getEmployeeProject(0).AssignDate = new DateTime(DateTime.Now.Year, 3, 1);
				e.getEmployeeProject(0).EndDate = new DateTime(DateTime.Now.Year, 6, 1);
				e.getEmployeeProject(0).EPProjectId = 1;

				e.HireDate = new DateTime(DateTime.Now.Year, 1, 1);
				EmployeeDataUtils.saveEmployee(e);
				long x = e.EmployeeId;

				e = EmployeeDataUtils.findByKey(x);
				Assert.IsNotNull(e, "New employee not found");
				Assert.AreEqual(e.Rank.Rank, "My New Rank");
				Assert.AreEqual(e.Salary, 100m);
				Assert.AreEqual(e.EmployeeName, "test employee");
				Assert.AreEqual(e.SSINumber, "1030045");
				Assert.AreEqual(e.HireDate, new DateTime(2015, 1, 1));
				Assert.AreEqual(e.EmployeeProjects.ToList().Count,1);

				e.SSINumber = "XXXXX";
				e.getEmployeeProject(0).EndDate = new DateTime(DateTime.Now.Year, 6, 1);
				EmployeeDataUtils.saveEmployee(e);
				e = EmployeeDataUtils.findByKey(x);
				Assert.AreEqual(e.SSINumber, "XXXXX");
				Assert.AreEqual(e.getEmployeeProject(0).EndDate , new DateTime(DateTime.Now.Year, 6, 1));

				e.ClearEmployeeProjects();
				EmployeeDataUtils.deleteEmployee(e);
				e = EmployeeDataUtils.findByKey(x);
				Assert.IsNull(e, "New employee must have been deleted!");

				// now let's test string primary key
				EmployeeType et = EmployeeTypeFactory.Create();
				et.EmployeeType = "A Description";
				et.EmployeeTypeCode = "XX";

				EmployeeType et1 = EmployeeTypeFactory.Create();
				et1.EmployeeType = "A Description 1";
				et1.EmployeeTypeCode = "XX1";

				EmployeeType et2 = EmployeeTypeFactory.Create();
				et2.EmployeeType = "A Description 2";
				et2.EmployeeTypeCode = "XX2";

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
