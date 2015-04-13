using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CsModelObjects;
using CsModelMappers;
using org.model.lib.Model;

namespace GeneratorTests {

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
		public void createCsRecords() {

			Employee e = EmployeeFactory.Create();
			e.EmployeeName = "test employee";
			e.Salary = 100m;
			e.SSINumber = "1030045";
			e.Telephone = "2234455";
			e.EmployeeRankId = 1;
			EmployeeDataUtils.saveEmployee(e);

		}
	}
}
