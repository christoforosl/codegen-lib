﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using org.model.lib;
using org.model.lib.Model;
using org.model.lib.db;

using System.Linq;
using CsModelObjects;

//<comments>
// Template: DBMapperBase.csharp.txt
//************************************************************
// 
// Class autogenerated on06-Jul-15 3:26:49 PM by ModelGenerator
// Extends base DBMapperBase object class
// *** DO NOT change code in this class.  
//     It will be re-generated and 
//     overwritten by the code generator ****
// Instead, change code in the extender class EmployeeDBMapper
//
//************************************************************
//</comments>

namespace CsModelMappers {

	[System.Runtime.InteropServices.ComVisible(false)][SelectObject("<SELECT_FROM_NAME>")][KeyFieldName("EmployeeId")]
	public class EmployeeDBMapper : DBMapper {

		#region "Constructors "
		public EmployeeDBMapper(DBUtils _dbConn) : base(_dbConn) {
		}


		public EmployeeDBMapper() : base() {
		}
		#endregion

		#region "Overloaded Functions"

		public new Employee findWhere(string sWhereClause, params object[] @params) {

			return (Employee)base.findWhere(sWhereClause, @params);
		}


		public void saveEmployee(Employee mo) {
			base.save(mo);
		}

		public new Employee findByKey(object keyval) {

			return (Employee)base.findByKey(keyval);

		}

		#endregion


		#region "getUpdateDBCommand"
		public override IDbCommand getUpdateDBCommand(IModelObject modelObj, string sql) {
			Employee obj = (Employee)modelObj;
			IDbCommand stmt = this.dbConn.getCommand(sql);
			stmt.Parameters.Add(this.dbConn.getParameter(Employee.STR_FLD_EMPLOYEENAME,obj.PrEmployeeName));
			stmt.Parameters.Add(this.dbConn.getParameter(Employee.STR_FLD_EMPLOYEERANKID,obj.PrEmployeeRankId));
			stmt.Parameters.Add(this.dbConn.getParameter(Employee.STR_FLD_SALARY,obj.PrSalary));
			stmt.Parameters.Add(this.dbConn.getParameter(Employee.STR_FLD_ADDRESS,obj.PrAddress));
			stmt.Parameters.Add(this.dbConn.getParameter(Employee.STR_FLD_TELEPHONE,obj.PrTelephone));
			stmt.Parameters.Add(this.dbConn.getParameter(Employee.STR_FLD_MOBILE,obj.PrMobile));
			stmt.Parameters.Add(this.dbConn.getParameter(Employee.STR_FLD_IDNUMBER,obj.PrIdNumber));
			stmt.Parameters.Add(this.dbConn.getParameter(Employee.STR_FLD_SSINUMBER,obj.PrSSINumber));
			stmt.Parameters.Add(this.dbConn.getParameter(Employee.STR_FLD_HIREDATE,obj.PrHireDate));
			stmt.Parameters.Add(this.dbConn.getParameter(Employee.STR_FLD_NUMDEPENDENTS,obj.PrNumDependents));
			stmt.Parameters.Add(this.dbConn.getParameter(Employee.STR_FLD_EMPLOYEETYPECODE,obj.PrEmployeeTypeCode));
			stmt.Parameters.Add(this.dbConn.getParameter(Employee.STR_FLD_CREATEDATE,obj.CreateDate));
			stmt.Parameters.Add(this.dbConn.getParameter(Employee.STR_FLD_UPDATEDATE,obj.UpdateDate));
			stmt.Parameters.Add(this.dbConn.getParameter(Employee.STR_FLD_CREATEUSER,obj.CreateUser));
			stmt.Parameters.Add(this.dbConn.getParameter(Employee.STR_FLD_UPDATEUSER,obj.UpdateUser));
			stmt.Parameters.Add(this.dbConn.getParameter(Employee.STR_FLD_SAMPLEGUIDFIELD,obj.PrSampleGuidField));
			stmt.Parameters.Add(this.dbConn.getParameter(Employee.STR_FLD_ISACTIVE,obj.PrIsActive));
			stmt.Parameters.Add(this.dbConn.getParameter(Employee.STR_FLD_SAMPLEBIGINT,obj.PrSampleBigInt));
			stmt.Parameters.Add(this.dbConn.getParameter(Employee.STR_FLD_SAMPLESMALLINT,obj.PrSampleSmallInt));
			stmt.Parameters.Add(this.dbConn.getParameter(Employee.STR_FLD_SAMPLENUMERICFIELDINT,obj.PrSampleNumericFieldInt));
			stmt.Parameters.Add(this.dbConn.getParameter(Employee.STR_FLD_SAMPLENUMERICFIELD2DECIMALS,obj.PrSampleNumericField2Decimals));

			if (obj.isNew){
			} else {
			//only add primary key if we are updating and as the last parameter
				stmt.Parameters.Add(this.dbConn.getParameter(Employee.STR_FLD_EMPLOYEEID,obj.PrEmployeeId));
			}

			return stmt;
		}
		#endregion
#region "Save Children Code"
	public override void saveChildren(IModelObject mo) {

		Employee ret = (Employee)mo;
		//***Child Association:employeeinfo
		if (ret.EmployeeInfoLoaded) {
			CsModelMappers.EmployeeInfoDBMapper employeeinfoMapper = new CsModelMappers.EmployeeInfoDBMapper(this.dbConn);
			employeeinfoMapper.save(ret.PrEmployeeInfo);
		}
		//***Child Association:employeeprojects
		if (ret.EmployeeProjectsLoaded) {
			CsModelMappers.EmployeeProjectDBMapper employeeprojectsMapper = new CsModelMappers.EmployeeProjectDBMapper(this.dbConn);
			employeeprojectsMapper.saveList(ret.PrEmployeeProjects);
			employeeprojectsMapper.deleteList(ret.PrEmployeeProjectsGetDeleted());
		}
	}
#endregion

	public override void saveParents(IModelObject mo ){

		Employee thisMo  = ( Employee)mo;
		//*** Parent Association:rank
		if ((thisMo.PrRank!=null) && (thisMo.PrRank.NeedsSave)) {
			CsModelMappers.EmployeeRankDBMapper mappervar = new CsModelMappers.EmployeeRankDBMapper(this.dbConn);
			mappervar.save(thisMo.PrRank);
			thisMo.PrEmployeeRankId = thisMo.PrRank.PrRankId;
		}
		
	}

		#region "Find functions"

		///	<summary>Given an sql statement, it opens a result set, and for each record returned, 
		///	it creates and loads a ModelObject. </summary>
		///	<param name="sWhereClause">where clause to be applied to "selectall" statement 
		/// that returns one or more records from the database, corresponding to the ModelObject we are going to load </param>
		///	 <param name="params"> Parameter values to be passed to sql statement </param>
		///	 <returns> A List(Of Employee) object containing all objects loaded </returns>
		///	 
		public new List<Employee> findList(string sWhereClause, params object[] @params) {

			string sql = this.getSqlWithWhereClause(sWhereClause);
			IDataReader rs = null;
			List<Employee> molist = new List<Employee>();

			try {
				rs = dbConn.getDataReaderWithParams(sql, @params);
				this.Loader.DataSource = rs;

				while (rs.Read()) {
					IModelObject mo = this.getModelInstance();
					this.Loader.load(mo);
					molist.Add((Employee)mo);

				}


			} finally {
				this.dbConn.closeDataReader(rs);
			}

			return molist;

		}

		///    
		///	 <summary>Returns all records from database for a coresponding ModelObject </summary>
		/// <returns>List(Of Employee) </returns>
		public List<Employee> findAll()
		{
			return this.findList(string.Empty);
		}

		public override IModelObjectLoader Loader {
			get {
				if (this._loader == null) {
					this._loader = new EmployeeDataReaderLoader();
				}
				return this._loader;
			}
			set { this._loader = value; }
		}

		#endregion

		public override IModelObject getModelInstance() {
			return new Employee();
		}

	}

	#region " Employee Loader "
	[System.Runtime.InteropServices.ComVisible(false)]
	public class EmployeeDataReaderLoader : DataReaderLoader {
		public override void load(IModelObject mo) {
			const int DATAREADER_FLD_EMPLOYEEID = 0;
			const int DATAREADER_FLD_EMPLOYEENAME = 1;
			const int DATAREADER_FLD_EMPLOYEERANKID = 2;
			const int DATAREADER_FLD_SALARY = 3;
			const int DATAREADER_FLD_ADDRESS = 4;
			const int DATAREADER_FLD_TELEPHONE = 5;
			const int DATAREADER_FLD_MOBILE = 6;
			const int DATAREADER_FLD_IDNUMBER = 7;
			const int DATAREADER_FLD_SSINUMBER = 8;
			const int DATAREADER_FLD_HIREDATE = 9;
			const int DATAREADER_FLD_NUMDEPENDENTS = 10;
			const int DATAREADER_FLD_EMPLOYEETYPECODE = 11;
			const int DATAREADER_FLD_CREATEDATE = 12;
			const int DATAREADER_FLD_UPDATEDATE = 13;
			const int DATAREADER_FLD_CREATEUSER = 14;
			const int DATAREADER_FLD_UPDATEUSER = 15;
			const int DATAREADER_FLD_SAMPLEGUIDFIELD = 16;
			const int DATAREADER_FLD_ISACTIVE = 17;
			const int DATAREADER_FLD_SAMPLEBIGINT = 18;
			const int DATAREADER_FLD_SAMPLESMALLINT = 19;
			const int DATAREADER_FLD_SAMPLENUMERICFIELDINT = 20;
			const int DATAREADER_FLD_SAMPLENUMERICFIELD2DECIMALS = 21;

			Employee obj = (Employee)mo;
			obj.IsObjectLoading = true;

			if (!this.reader.IsDBNull(DATAREADER_FLD_EMPLOYEEID) ) {
				obj.PrEmployeeId = Convert.ToInt64(this.reader.GetInt32(DATAREADER_FLD_EMPLOYEEID));
			}
			if (!this.reader.IsDBNull(DATAREADER_FLD_EMPLOYEENAME) ) {
				obj.PrEmployeeName = this.reader.GetString(DATAREADER_FLD_EMPLOYEENAME);
			}
			if (!this.reader.IsDBNull(DATAREADER_FLD_EMPLOYEERANKID) ) {
				obj.PrEmployeeRankId = Convert.ToInt64(this.reader.GetInt32(DATAREADER_FLD_EMPLOYEERANKID));
			}
			if (!this.reader.IsDBNull(DATAREADER_FLD_SALARY) ) {
				obj.PrSalary = this.reader.GetDecimal(DATAREADER_FLD_SALARY);
			}
			if (!this.reader.IsDBNull(DATAREADER_FLD_ADDRESS) ) {
				obj.PrAddress = this.reader.GetString(DATAREADER_FLD_ADDRESS);
			}
			if (!this.reader.IsDBNull(DATAREADER_FLD_TELEPHONE) ) {
				obj.PrTelephone = this.reader.GetString(DATAREADER_FLD_TELEPHONE);
			}
			if (!this.reader.IsDBNull(DATAREADER_FLD_MOBILE) ) {
				obj.PrMobile = this.reader.GetString(DATAREADER_FLD_MOBILE);
			}
			if (!this.reader.IsDBNull(DATAREADER_FLD_IDNUMBER) ) {
				obj.PrIdNumber = this.reader.GetString(DATAREADER_FLD_IDNUMBER);
			}
			if (!this.reader.IsDBNull(DATAREADER_FLD_SSINUMBER) ) {
				obj.PrSSINumber = this.reader.GetString(DATAREADER_FLD_SSINUMBER);
			}
			if (!this.reader.IsDBNull(DATAREADER_FLD_HIREDATE) ) {
				obj.PrHireDate = this.reader.GetDateTime(DATAREADER_FLD_HIREDATE);
			}
			if (!this.reader.IsDBNull(DATAREADER_FLD_NUMDEPENDENTS) ) {
				obj.PrNumDependents = Convert.ToInt64(this.reader.GetInt32(DATAREADER_FLD_NUMDEPENDENTS));
			}
			if (!this.reader.IsDBNull(DATAREADER_FLD_EMPLOYEETYPECODE) ) {
				obj.PrEmployeeTypeCode = this.reader.GetString(DATAREADER_FLD_EMPLOYEETYPECODE);
			}
			if (!this.reader.IsDBNull(DATAREADER_FLD_CREATEDATE) ) {
				obj.CreateDate = this.reader.GetDateTime(DATAREADER_FLD_CREATEDATE);
			}
			if (!this.reader.IsDBNull(DATAREADER_FLD_UPDATEDATE) ) {
				obj.UpdateDate = this.reader.GetDateTime(DATAREADER_FLD_UPDATEDATE);
			}
			if (!this.reader.IsDBNull(DATAREADER_FLD_CREATEUSER) ) {
				obj.CreateUser = this.reader.GetString(DATAREADER_FLD_CREATEUSER);
			}
			if (!this.reader.IsDBNull(DATAREADER_FLD_UPDATEUSER) ) {
				obj.UpdateUser = this.reader.GetString(DATAREADER_FLD_UPDATEUSER);
			}
			if (!this.reader.IsDBNull(DATAREADER_FLD_SAMPLEGUIDFIELD) ) {
				obj.PrSampleGuidField = this.reader.GetGuid(DATAREADER_FLD_SAMPLEGUIDFIELD);
			}
			if (!this.reader.IsDBNull(DATAREADER_FLD_ISACTIVE) ) {
				obj.PrIsActive = this.reader.GetBoolean(DATAREADER_FLD_ISACTIVE);
			}
			if (!this.reader.IsDBNull(DATAREADER_FLD_SAMPLEBIGINT) ) {
				obj.PrSampleBigInt = this.reader.GetInt64(DATAREADER_FLD_SAMPLEBIGINT);
			}
			if (!this.reader.IsDBNull(DATAREADER_FLD_SAMPLESMALLINT) ) {
				obj.PrSampleSmallInt = Convert.ToInt64(this.reader.GetInt16(DATAREADER_FLD_SAMPLESMALLINT));
			}
			if (!this.reader.IsDBNull(DATAREADER_FLD_SAMPLENUMERICFIELDINT) ) {
				obj.PrSampleNumericFieldInt = Convert.ToInt64(this.reader.GetDecimal(DATAREADER_FLD_SAMPLENUMERICFIELDINT));
			}
			if (!this.reader.IsDBNull(DATAREADER_FLD_SAMPLENUMERICFIELD2DECIMALS) ) {
				obj.PrSampleNumericField2Decimals = this.reader.GetDecimal(DATAREADER_FLD_SAMPLENUMERICFIELD2DECIMALS);
			}


			obj.isNew = false;
			// since we've just loaded from database, we mark as "old"
			obj.isDirty = false;
			obj.IsObjectLoading = false;
			obj.afterLoad();

			return;

		}

	}

	#endregion

	///<summary>
	/// Final Class with convinience shared methods for loading/saving the EmployeeRank ModelObject. 
	///</summary>
	[System.Runtime.InteropServices.ComVisible(false)]
	public sealed class EmployeeDataUtils {

		#region "Shared ""get"" Functions "

		public static List<Employee> findList(string @where, params object[] @params){

			EmployeeDBMapper dbm = new EmployeeDBMapper();
			return dbm.findList(@where, @params);

		}

		public static Employee findOne(string @where, params object[] @params) {

			EmployeeDBMapper dbm = new EmployeeDBMapper();
			return (Employee)dbm.findWhere(@where, @params);

		}

		public static List<Employee> findList(){

			return new EmployeeDBMapper().findAll();

		}

		public static Employee findByKey(object id) {

			return (Employee)new EmployeeDBMapper().findByKey(id);

		}

		/// <summary>
		/// Reload the Employee from the database
		/// </summary>
		/// <remarks>
		/// use this method when you want to relad the Employee 
		/// from the database, discarding any changes
		/// </remarks>
		public static void reload(ref Employee mo) {

			if (mo == null) {
				throw new System.ArgumentNullException("null object past to reload function");
			}

			mo = (Employee)new EmployeeDBMapper().findByKey(mo.Id);

		}

		#endregion

		#region "Shared Save and Delete Functions"
		/// <summary>
		/// Convinience method to save a Employee Object.
		/// Important note: DO NOT CALL THIS IN A LOOP!
		/// </summary>
		/// <param name="EmployeeObj"></param>
		/// <remarks>
		/// Important note: DO NOT CALL THIS IN A LOOP!  
		/// This method simply instantiates a EmployeeDBMapper and calls the save method
		/// </remarks>
		public static void saveEmployee(params Employee[] EmployeeObj)
		{

			EmployeeDBMapper dbm = new EmployeeDBMapper();
			dbm.saveList(EmployeeObj.ToList());


		}


		public static void deleteEmployee(Employee EmployeeObj)
		{

			EmployeeDBMapper dbm = new EmployeeDBMapper();
			dbm.delete(EmployeeObj);

		}
		#endregion

		#region "Data Table and data row load/save "

		public static void saveEmployee(DataRow dr, ref Employee mo) {
			if (mo == null) {
				mo = new Employee();
			}

			foreach (DataColumn dc in dr.Table.Columns) {
				mo.setAttribute(dc.ColumnName, dr[dc.ColumnName]);
			}

			saveEmployee(mo);

		}

		public static void saveEmployee(DataTable dt, ref Employee mo) {
			foreach (DataRow dr in dt.Rows) {
				saveEmployee(dr, ref mo);
			}

		}

		public static Employee loadFromDataRow(DataRow r) {

			DataRowLoader a = new DataRowLoader();
			IModelObject mo = new Employee();
			a.DataSource = r;
			a.load(mo);
			return (Employee)mo;

		}

		#endregion

	}

}


