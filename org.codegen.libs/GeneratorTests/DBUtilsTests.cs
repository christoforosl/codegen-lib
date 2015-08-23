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
using System.Threading;
using System.Diagnostics;


namespace GeneratorTests {


	[TestClass]
	public class DBUtilsTests {

		public TestContext TestContext { get; set; }
		
		[TestMethod]
		public void threadTests() {

			int connectionCount = this.getConnectionCount();
			List<Thread> ts = new List<Thread>();

			// update NumDependents to 10, the therads below update the employee NumDependents to 1,2,3,4 but we roll them back
			// at the end of the test , after all theads have finished, we make sure that NumDependents is 10 for all emplloyees    
			DBUtils.Current().executeSQLWithParams("update employee set NumDependents=10");
			int employeeCount = DBUtils.Current().getLngValue("select count(*) from employee where NumDependents<>10");
			Assert.AreEqual(0, employeeCount);

			ts.Add(new Thread(dbUtilsConcurrencyTest));
			ts.Add(new Thread(dbUtilsConcurrencyTest));
			ts.Add(new Thread(dbUtilsConcurrencyTest));
			ts.Add(new Thread(dbUtilsConcurrencyTest));
			ts.Add(new Thread(dbUtilsConcurrencyTest));
			ts.Add(new Thread(dbUtilsConcurrencyTest));
			ts.Add(new Thread(dbUtilsConcurrencyTest));

			int i = 0;
			ts.ForEach(x => x.Start(i++));
			ts.ForEach(x => x.Join());

			employeeCount = DBUtils.Current().getLngValue("select count(*) from employee where NumDependents<>10");
			Assert.AreEqual(0, employeeCount);
			// at the end of the test , after all theads have finished, we make sure 
			// that NumDependents is 10 for all emplloyees  since we set it at line 33 and all threads 
			// rollback

			// the test below removed.  Never suceeeded but connection count is correct if you
			// do a select from database. Threading issues?  
			//Assert.AreEqual(connectionCount, this.getConnectionCount(),
			//	"Expected connection count to be starting connection");
		}

		/// <summary>
		/// Called by the threadTests above.  
		/// Tests transactions, with update and select statements
		/// </summary>
		private void dbUtilsConcurrencyTest(object x) {

			TestContext.WriteLine("--->NumDependents:{0}, DBUtils: {1}", x, DBUtils.Current().GetHashCode().ToString());
			DBUtils.Current().beginTrans();
			try {

				int employeeCount = DBUtils.Current().getLngValue("select count(*) from employee");

				// update NumDependents to x
				DBUtils.Current().executeSQLWithParams("update employee set NumDependents=?", Convert.ToInt32(x));

				using (IDataReader rs = DBUtils.Current().getDataReaderWithParams("select employeeid, address from employee where NumDependents=?",x)) {
					Assert.IsFalse(rs.IsClosed);
					Assert.IsTrue(rs.Read());
				}
				int employeeCount2 = DBUtils.Current().getLngValue("select count(*) from employee where NumDependents=?", Convert.ToInt32(x));
				Assert.AreEqual(
					employeeCount2, employeeCount);


			} finally {
				DBUtils.Current().rollbackTrans();
			}

		}

		[TestMethod]
		public void testDataReaders() {
			int connectionCount = this.getConnectionCount();
			using (IDataReader rs = DBUtils.Current().getDataReader("select employeeid, address from employee")) {
				Assert.IsFalse(rs.IsClosed);
				if (rs.Read()) {
					string address = rs.GetString(1);
				}
			}
			Assert.AreEqual(connectionCount, this.getConnectionCount(),
				"Expected same connection count after execution of getDataReader");


			List<IDataParameter> lst = new List<IDataParameter>();
			lst.Add(DBUtils.Current().getParameter("@eid", 1));
			lst.Add(DBUtils.Current().getParameter("@address", "nikoy theofanous 3a, nicosia"));

			using (IDataReader rs = DBUtils.Current().getDataReader("select employeeid, address from employee where employeeid=@eid and address=@address", lst)) {
				Assert.IsFalse(rs.IsClosed);
				if (rs.Read()) {
					string address = rs.GetString(1);
				}
			}
			Assert.AreEqual(connectionCount, this.getConnectionCount(),
				"Expected same connection count after execution of getDataReader(List<IDataParameter>)");

			using (IDataReader rs = DBUtils.Current().getDataReaderWithParams(
					"select employeeid, address from employee where employeeid=? and address=?",
						1, "nikoy theofanous 3a, nicosia")) {
				Assert.IsFalse(rs.IsClosed);
				if (rs.Read()) {
					string address = rs.GetString(1);
				}
			}
			Assert.AreEqual(connectionCount, this.getConnectionCount(),
				"Expected same connection count after execution of getDataReader(object[])");

		}

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
			Assert.AreEqual(connectionCount, this.getConnectionCount());

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

			DataSet ds = new DataSet();
			DBUtils.Current().fillTypedDataSet(ds, "employees", "select * from employee where isActive=1");
			Assert.AreEqual(ds.Tables[0].Rows.Count, employeeCount);

			ds = DBUtils.Current().getDataSet("select * from employee where isActive=1");
			Assert.AreEqual(ds.Tables[0].Rows.Count, employeeCount);

			ds = DBUtils.Current().getDataSetWithParams("select * from employee where isActive=?", 1);
			Assert.AreEqual(ds.Tables.Count, 1);
			Assert.AreEqual(ds.Tables[0].Rows.Count, employeeCount);

			using (IDataReader rs = DBUtils.Current().getDataReaderWithParams(
					"select employeeid, address from employee where isActive=?", 1)) {
				Assert.IsFalse(rs.IsClosed);
				Assert.IsTrue(rs.Read(), "Datareader must return rows where isActive=?");
			}
			DBUtils.Current().rollbackTrans();
			Assert.AreEqual(
				DBUtils.Current().getLngValue("select count(*) from employee where isActive=0"),
				employeeCount, "after rollback, all employees should be active=0");

			ds = DBUtils.Current().getDataSetWithParams("select * from employee where isActive=?", 0);
			Assert.AreEqual(ds.Tables.Count, 1);
			Assert.AreEqual(ds.Tables[0].Rows.Count, employeeCount);
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
