﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using org.model.lib;
using org.model.lib.Model;
using org.model.lib.db;

using System.Linq;
using OracleModel;

//<comments>
// Template: DBMapperBase.csharp.txt
//************************************************************
// 
// Class autogenerated on12-May-15 11:20:43 AM by ModelGenerator
// Extends base DBMapperBase object class
// *** DO NOT change code in this class.  
//     It will be re-generated and 
//     overwritten by the code generator ****
// Instead, change code in the extender class EmployeeDBMapper
//
//************************************************************
//</comments>

namespace OracleMappers {

	[System.Runtime.InteropServices.ComVisible(false)]
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
			IEmployee obj = (IEmployee)modelObj;
			IDbCommand stmt = this.dbConn.getCommand(sql);
				stmt.Parameters.Add(this.dbConn.getParameter("@FIRST_NAME",obj.PrFirstName));
				stmt.Parameters.Add(this.dbConn.getParameter("@LAST_NAME",obj.PrLastName));
				stmt.Parameters.Add(this.dbConn.getParameter("@EMAIL",obj.PrEMAIL));
				stmt.Parameters.Add(this.dbConn.getParameter("@PHONE_NUMBER",obj.PrPhoneNumber));
				stmt.Parameters.Add(this.dbConn.getParameter("@HIRE_DATE",obj.PrHireDate));
				stmt.Parameters.Add(this.dbConn.getParameter("@JOB_ID",obj.PrJobId));
				stmt.Parameters.Add(this.dbConn.getParameter("@SALARY",obj.PrSALARY));
				stmt.Parameters.Add(this.dbConn.getParameter("@COMMISSION_PCT",obj.PrCommissionPct));
				stmt.Parameters.Add(this.dbConn.getParameter("@MANAGER_ID",obj.PrManagerId));
				stmt.Parameters.Add(this.dbConn.getParameter("@DEPARTMENT_ID",obj.PrDepartmentId));
				stmt.Parameters.Add(this.dbConn.getParameter("@CREATE_DATE",obj.CreateDate));
				stmt.Parameters.Add(this.dbConn.getParameter("@UPDATE_DATE",obj.UpdateDate));
				stmt.Parameters.Add(this.dbConn.getParameter("@CREATE_USER",obj.CreateUser));
				stmt.Parameters.Add(this.dbConn.getParameter("@UPDATE_USER",obj.UpdateUser));

			if ( obj.isNew ) {
			} else {
			//only add primary key if we are updating and as the last parameter
								stmt.Parameters.Add(this.dbConn.getParameter("@EMPLOYEE_ID",obj.PrEmployeeId));
		}

			return stmt;
		}
		#endregion
#region "Save Children Code"
	public override void saveChildren(IModelObject mo) {

		Employee ret = (Employee)mo;
		//***Child Association:jobhistory
		if (ret.JobHistoryLoaded) {
			OracleMappers.JobHistoryDBMapper jobhistoryMapper = new OracleMappers.JobHistoryDBMapper(this.dbConn);
			jobhistoryMapper.saveList(ret.PrJobHistory);
			jobhistoryMapper.deleteList(ret.PrJobHistoryGetDeleted());
		}
		//***Child Association:courses
		if (ret.CoursesLoaded) {
			OracleMappers.EmployeeTrainingHistoryDBMapper coursesMapper = new OracleMappers.EmployeeTrainingHistoryDBMapper(this.dbConn);
			coursesMapper.saveList(ret.PrCourses);
			coursesMapper.deleteList(ret.PrCoursesGetDeleted());
		}
		//***Child Association:traininghistory
		if (ret.TrainingHistoryLoaded) {
			OracleMappers.EmployeeTrainingHistoryDBMapper traininghistoryMapper = new OracleMappers.EmployeeTrainingHistoryDBMapper(this.dbConn);
			traininghistoryMapper.saveList(ret.PrTrainingHistory);
			traininghistoryMapper.deleteList(ret.PrTrainingHistoryGetDeleted());
		}
	}
#endregion

	public override void saveParents(IModelObject mo ){

		Employee thisMo  = ( Employee)mo;
		//*** Parent Association:department
		if ((thisMo.PrDepartment!=null) && (thisMo.PrDepartment.NeedsSave)) {
			OracleMappers.DepartmentDBMapper mappervar = new OracleMappers.DepartmentDBMapper(this.dbConn);
			mappervar.save(thisMo.PrDepartment);
			thisMo.PrDepartmentId = thisMo.PrDepartment.PrDepartmentId;
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

		public override IModelObject getModelInstance()
		{
			return EmployeeFactory.Create();
		}

	}

	#region " Employee Loader "
	[System.Runtime.InteropServices.ComVisible(false)]
	public class EmployeeDataReaderLoader : DataReaderLoader {
		public override void load(IModelObject mo) {
			const int DATAREADER_FLD_EMPLOYEE_ID = 0;
			const int DATAREADER_FLD_FIRST_NAME = 1;
			const int DATAREADER_FLD_LAST_NAME = 2;
			const int DATAREADER_FLD_EMAIL = 3;
			const int DATAREADER_FLD_PHONE_NUMBER = 4;
			const int DATAREADER_FLD_HIRE_DATE = 5;
			const int DATAREADER_FLD_JOB_ID = 6;
			const int DATAREADER_FLD_SALARY = 7;
			const int DATAREADER_FLD_COMMISSION_PCT = 8;
			const int DATAREADER_FLD_MANAGER_ID = 9;
			const int DATAREADER_FLD_DEPARTMENT_ID = 10;
			const int DATAREADER_FLD_CREATE_DATE = 11;
			const int DATAREADER_FLD_UPDATE_DATE = 12;
			const int DATAREADER_FLD_CREATE_USER = 13;
			const int DATAREADER_FLD_UPDATE_USER = 14;
			const int DATAREADER_FLD_LOCATION_ID = 15;
			const int DATAREADER_FLD_COUNTRY_ID = 16;
			const int DATAREADER_FLD_DEPARTMENT_NAME = 17;
			const int DATAREADER_FLD_JOB_TITLE = 18;
			const int DATAREADER_FLD_CITY = 19;
			const int DATAREADER_FLD_STATE_PROVINCE = 20;
			const int DATAREADER_FLD_COUNTRY_NAME = 21;
			const int DATAREADER_FLD_REGION_NAME = 22;
			const int DATAREADER_FLD_MANAGER_NAME = 23;

			Employee obj = (Employee)mo;
			obj.IsObjectLoading = true;

			if (!this.reader.IsDBNull(DATAREADER_FLD_EMPLOYEE_ID) ) {
				obj.PrEmployeeId = this.reader.GetInt64(DATAREADER_FLD_EMPLOYEE_ID);
			}
			if (!this.reader.IsDBNull(DATAREADER_FLD_FIRST_NAME) ) {
				obj.PrFirstName = this.reader.GetString(DATAREADER_FLD_FIRST_NAME);
			}
			if (!this.reader.IsDBNull(DATAREADER_FLD_LAST_NAME) ) {
				obj.PrLastName = this.reader.GetString(DATAREADER_FLD_LAST_NAME);
			}
			if (!this.reader.IsDBNull(DATAREADER_FLD_EMAIL) ) {
				obj.PrEMAIL = this.reader.GetString(DATAREADER_FLD_EMAIL);
			}
			if (!this.reader.IsDBNull(DATAREADER_FLD_PHONE_NUMBER) ) {
				obj.PrPhoneNumber = this.reader.GetString(DATAREADER_FLD_PHONE_NUMBER);
			}
			if (!this.reader.IsDBNull(DATAREADER_FLD_HIRE_DATE) ) {
				obj.PrHireDate = this.reader.GetDateTime(DATAREADER_FLD_HIRE_DATE);
			}
			if (!this.reader.IsDBNull(DATAREADER_FLD_JOB_ID) ) {
				obj.PrJobId = this.reader.GetString(DATAREADER_FLD_JOB_ID);
			}
			if (!this.reader.IsDBNull(DATAREADER_FLD_SALARY) ) {
				obj.PrSALARY = this.reader.GetDecimal(DATAREADER_FLD_SALARY);
			}
			if (!this.reader.IsDBNull(DATAREADER_FLD_COMMISSION_PCT) ) {
				obj.PrCommissionPct = this.reader.GetDecimal(DATAREADER_FLD_COMMISSION_PCT);
			}
			if (!this.reader.IsDBNull(DATAREADER_FLD_MANAGER_ID) ) {
				obj.PrManagerId = this.reader.GetInt64(DATAREADER_FLD_MANAGER_ID);
			}
			if (!this.reader.IsDBNull(DATAREADER_FLD_DEPARTMENT_ID) ) {
				obj.PrDepartmentId = this.reader.GetInt64(DATAREADER_FLD_DEPARTMENT_ID);
			}
			if (!this.reader.IsDBNull(DATAREADER_FLD_CREATE_DATE) ) {
				obj.CreateDate = this.reader.GetDateTime(DATAREADER_FLD_CREATE_DATE);
			}
			if (!this.reader.IsDBNull(DATAREADER_FLD_UPDATE_DATE) ) {
				obj.UpdateDate = this.reader.GetDateTime(DATAREADER_FLD_UPDATE_DATE);
			}
			if (!this.reader.IsDBNull(DATAREADER_FLD_CREATE_USER) ) {
				obj.CreateUser = this.reader.GetString(DATAREADER_FLD_CREATE_USER);
			}
			if (!this.reader.IsDBNull(DATAREADER_FLD_UPDATE_USER) ) {
				obj.UpdateUser = this.reader.GetString(DATAREADER_FLD_UPDATE_USER);
			}
			if (!this.reader.IsDBNull(DATAREADER_FLD_LOCATION_ID) ) {
				obj.PrLocationId = this.reader.GetInt64(DATAREADER_FLD_LOCATION_ID);
			}
			if (!this.reader.IsDBNull(DATAREADER_FLD_COUNTRY_ID) ) {
				obj.PrCountryId = this.reader.GetString(DATAREADER_FLD_COUNTRY_ID);
			}
			if (!this.reader.IsDBNull(DATAREADER_FLD_DEPARTMENT_NAME) ) {
				obj.PrDepartmentName = this.reader.GetString(DATAREADER_FLD_DEPARTMENT_NAME);
			}
			if (!this.reader.IsDBNull(DATAREADER_FLD_JOB_TITLE) ) {
				obj.PrJobTitle = this.reader.GetString(DATAREADER_FLD_JOB_TITLE);
			}
			if (!this.reader.IsDBNull(DATAREADER_FLD_CITY) ) {
				obj.PrCITY = this.reader.GetString(DATAREADER_FLD_CITY);
			}
			if (!this.reader.IsDBNull(DATAREADER_FLD_STATE_PROVINCE) ) {
				obj.PrStateProvince = this.reader.GetString(DATAREADER_FLD_STATE_PROVINCE);
			}
			if (!this.reader.IsDBNull(DATAREADER_FLD_COUNTRY_NAME) ) {
				obj.PrCountryName = this.reader.GetString(DATAREADER_FLD_COUNTRY_NAME);
			}
			if (!this.reader.IsDBNull(DATAREADER_FLD_REGION_NAME) ) {
				obj.PrRegionName = this.reader.GetString(DATAREADER_FLD_REGION_NAME);
			}
			if (!this.reader.IsDBNull(DATAREADER_FLD_MANAGER_NAME) ) {
				obj.PrManagerName = this.reader.GetString(DATAREADER_FLD_MANAGER_NAME);
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
				mo = EmployeeFactory.Create();
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
			IModelObject mo = EmployeeFactory.Create();
			a.DataSource = r;
			a.load(mo);
			return (Employee)mo;

		}

		#endregion

	}

}


