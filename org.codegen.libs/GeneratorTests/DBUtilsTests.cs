using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data;
using System.Linq;
using System.Text;
using CsModelObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using org.model.lib.db;
using org.model.lib.Model;
using System.Security.Principal;


namespace GeneratorTests {

	[TestClass]
	public class DBUtilsTests {

		[TestMethod]
		public void testexecuteSQL() {

			int connectionCount = this.getConnectionCount();
			int employeeCount = DBUtils.Current().getLngValue("select count(*) from employee");

			List<IDataParameter> lst = new List<IDataParameter>();
			lst.Add(DBUtils.Current().getParameter("@eid", 1));
			lst.Add(DBUtils.Current().getParameter("@address", "nikoy theofanous 3a, nicosia"));
			DBUtils.Current().executeSQLWithParams("update employee set address=@address where employeeid=@eid", lst);
			Assert.AreEqual(connectionCount, this.getConnectionCount(), "Expected same connection count after execution of executeSQLWithParams(List<IDataParameter>)");
			Assert.AreEqual(DBUtils.Current().getLngValue("select count(*) from employee where address='nikoy theofanous 3a, nicosia'"), 1);

			DBUtils.Current().executeSQL("update employee set isActive=1");
			Assert.AreEqual(connectionCount, this.getConnectionCount(), "Expected same connection count after execution of executeSQL (no params)");

			Assert.AreEqual(DBUtils.Current().getLngValue("select count(*) from employee where isActive is null"), 0);
			Assert.AreEqual(DBUtils.Current().getLngValue("select count(*) from employee where isActive=1"), employeeCount);

			DBUtils.Current().executeSQLWithParams("update employee set isActive=?", 0);
			Assert.AreEqual(connectionCount, this.getConnectionCount(), "Expected same connection count after execution of executeSQLWithParams");

			Assert.AreEqual(DBUtils.Current().getLngValue("select count(*) from employee where isActive is null"), 0);
			Assert.AreEqual(DBUtils.Current().getLngValue("select count(*) from employee where isActive=0"), employeeCount);

			DBUtils.Current().executeSQLWithParams("update employee set isActive=? where employeeid=?", 0, 1);
			Assert.AreEqual(connectionCount, this.getConnectionCount(), "Expected same connection count after execution of executeSQLWithParams");
			Assert.AreEqual(DBUtils.Current().getLngValue("select count(*) from employee where isActive is null"), 0);
			Assert.AreEqual(DBUtils.Current().getLngValue("select count(*) from employee where isActive=? and employeeid=?", 0, 1), 1);
		}

		[TestMethod]
		public void testTransactions() {

			int employeeCount = DBUtils.Current().getLngValue("select count(*) from employee");
			DBUtils.Current().executeSQLWithParams("update employee set isActive=1");
			int connectionCount = this.getConnectionCount();
			
			DBUtils.Current().beginTrans();
			Assert.IsTrue(DBUtils.Current().inTrans, "after beginTrans, intrans shouldbe true");
			DBUtils.Current().executeSQLWithParams("update employee set isActive=0");
			DBUtils.Current().commitTrans();

			Assert.IsFalse(DBUtils.Current().inTrans, "after commit, intrans should be false");
			Assert.AreEqual(
				DBUtils.Current().getLngValue("select count(*) from employee where isActive=0"), 
				employeeCount, "after commit, all employees should be active=0");


			DBUtils.Current().beginTrans();
			DBUtils.Current().executeSQLWithParams("update employee set isActive=1");
			Assert.AreEqual(
				DBUtils.Current().getLngValue("select count(*) from employee where isActive=1"),
				employeeCount, "in transaction, selecting employees where active=1 should return all employees");

			DBUtils.Current().rollbackTrans();
			Assert.AreEqual(
				DBUtils.Current().getLngValue("select count(*) from employee where isActive=0"),
				employeeCount, "after rollback, all employees should be active=0");

		}

		private int getConnectionCount() {

			string sql = "SELECT COUNT(dbid) as NumberOfConnections FROM sys.sysprocesses " +
				" WHERE DB_NAME(dbid) =? and loginame = ?";
			string name = WindowsIdentity.GetCurrent().Name;
			return DBUtils.Current().getLngValueWithParams(sql, "modelTest", name);

		}

		
		[TestMethod]
		public void testPagedList() {

			int x = DBUtils.Current().getIntValue("select count(*) from employee");
			if (x > 3) {
				x = 3;
			}

			using (DataContext ctx = DBUtils.Current().dbContext()) {

				var query = ctx.ExecuteQuery<Employee>(@"SELECT Employeeid,EmployeeName FROM employee").Skip(1).Take(x);
				var lst = query.ToList();

				Assert.AreEqual(lst.Count, x, "Expected to receive " + x + " employee records, got: " + lst.Count);
				string output = JsonConvert.SerializeObject(lst);
				System.Diagnostics.Debug.WriteLine(output);
			}

		}


		[TestMethod]
		public void testParametersArray() {
			List<IDataParameter> parameters = new List<IDataParameter>();
			parameters.Add(ModelContext.CurrentDBUtils.getParameter("1", "XX"));
			parameters.Add(ModelContext.CurrentDBUtils.getParameter("2", 1300D));

			var pdb = new ModelLibVBGenCode.VbBusObjects.DBMappers.EmployeeDBMapper();
			var lstResults = pdb.findList("EmployeeName=@1 and Salary=@2 ", parameters);

			var pdbCs = new CsModelMappers.EmployeeDBMapper();
			List<IDataParameter> parameters2 = new List<IDataParameter>();
			parameters2.Add(ModelContext.CurrentDBUtils.getParameter("1", "XX"));
			parameters2.Add(ModelContext.CurrentDBUtils.getParameter("2", 1300D));
			var lstResultsCs = pdbCs.findList("EmployeeName=@1 and Salary=@2 ", parameters2);

		}

		[TestMethod]
		public void testParsePositionalParamsForSQLServer() {

			string x;

			x = DBUtils.Current().replaceParameterPlaceHolders("update employee set isActive=? and employeeid=?");
			Assert.AreEqual(x, "update employee set isActive=@0 and employeeid=@1");

			x = DBUtils.Current().replaceParameterPlaceHolders("Telephone=? and Telephone=? and ?=x");
			Assert.AreEqual(x, "Telephone=@0 and Telephone=@1 and @2=x");

			x = DBUtils.Current().replaceParameterPlaceHolders("where Telephone=? and Telephone=?", "x", "y");
			Assert.AreEqual(x, "where Telephone=@0 and Telephone=@1");

			x = DBUtils.Current().replaceParameterPlaceHolders("where Telephone=? and Telephone=?", "x", "y");
			Assert.AreEqual(x, "where Telephone=@0 and Telephone=@1");

			x = DBUtils.Current().replaceParameterPlaceHolders("where Telephone=? and Telephone=?", "x", "y");
			Assert.AreEqual(x, "where Telephone=@0 and Telephone=@1");

			x = DBUtils.Current().replaceParameterPlaceHolders("where Telephone=? and Telephone=? and ?=x");
			Assert.AreEqual(x, "where Telephone=@0 and Telephone=@1 and @2=x");

			x = DBUtils.Current().replaceParameterPlaceHolders("where Telephone BETWEEN ? and ?");
			Assert.AreEqual(x, "where Telephone BETWEEN @0 and @1");

		}

	}
}
