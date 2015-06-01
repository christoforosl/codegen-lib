using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CsModelObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using org.model.lib.db;
using org.model.lib.Model;

namespace GeneratorTests {

	[TestClass]
	public class DBUtilsTests {


		[TestMethod]
		public void testPagedList() {
			
			IEnumerable<EmployeeType> pl = new PagedModelObjectList< EmployeeType>().
				Skip(10).Take(30).
				OrderBy(EmployeeType.STR_FLD_EMPLOYEETYPECODE).getList();

			
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
