using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using CsModelObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using org.model.lib.db;
using org.model.lib.Model;

namespace GeneratorTests {

	[TestClass]
	public class DBUtilsTests {


		[TestMethod]
		public void testPagedList() {

			using (DataContext ctx = DBUtils.Current().dbContext()) {

				var query = ctx.ExecuteQuery<Employee>(@"SELECT Employeeid,EmployeeName FROM employee").Skip(1).Take(5);
				var lst = query.ToList();

				Assert.AreEqual(lst.Count, 5, "Expected to receive 5 employee records");
				string output = JsonConvert.SerializeObject(lst);
				System.Diagnostics.Debug.WriteLine(output);
			}
			

			
		}

		[TestMethod]
		public void testParsePositionalParamsForSQLServer() {

			string x;

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
