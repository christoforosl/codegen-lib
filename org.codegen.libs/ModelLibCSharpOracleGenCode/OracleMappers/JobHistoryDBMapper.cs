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
// Class autogenerated on30-May-15 9:56:56 AM by ModelGenerator
// Extends base DBMapperBase object class
// *** DO NOT change code in this class.  
//     It will be re-generated and 
//     overwritten by the code generator ****
// Instead, change code in the extender class JobHistoryDBMapper
//
//************************************************************
//</comments>

namespace OracleMappers {

	[System.Runtime.InteropServices.ComVisible(false)]
	public class JobHistoryDBMapper : DBMapper {

		#region "Constructors "
		public JobHistoryDBMapper(DBUtils _dbConn) : base(_dbConn) {
		}


		public JobHistoryDBMapper() : base() {
		}
		#endregion

		#region "Overloaded Functions"

		public new JobHistory findWhere(string sWhereClause, params object[] @params) {

			return (JobHistory)base.findWhere(sWhereClause, @params);
		}


		public void saveJobHistory(JobHistory mo) {
			base.save(mo);
		}

		public new JobHistory findByKey(object keyval) {

			return (JobHistory)base.findByKey(keyval);

		}

		#endregion


		#region "getUpdateDBCommand"
		public override IDbCommand getUpdateDBCommand(IModelObject modelObj, string sql) {
			JobHistory obj = (JobHistory)modelObj;
			IDbCommand stmt = this.dbConn.getCommand(sql);
				stmt.Parameters.Add(this.dbConn.getParameter("@EMPLOYEE_ID",obj.PrEmployeeId));
				stmt.Parameters.Add(this.dbConn.getParameter("@START_DATE",obj.PrStartDate));
				stmt.Parameters.Add(this.dbConn.getParameter("@END_DATE",obj.PrEndDate));
				stmt.Parameters.Add(this.dbConn.getParameter("@JOB_ID",obj.PrJobId));
				stmt.Parameters.Add(this.dbConn.getParameter("@DEPARTMENT_ID",obj.PrDepartmentId));
				stmt.Parameters.Add(this.dbConn.getParameter("@CREATE_DATE",obj.CreateDate));
				stmt.Parameters.Add(this.dbConn.getParameter("@UPDATE_DATE",obj.UpdateDate));
				stmt.Parameters.Add(this.dbConn.getParameter("@CREATE_USER",obj.CreateUser));
				stmt.Parameters.Add(this.dbConn.getParameter("@UPDATE_USER",obj.UpdateUser));

			if ( obj.isNew ) {
			} else {
			//only add primary key if we are updating and as the last parameter
								stmt.Parameters.Add(this.dbConn.getParameter("@JOB_HISTORY_ID",obj.PrJobHistoryId));
		}

			return stmt;
		}
		#endregion



		#region "Find functions"

		///	<summary>Given an sql statement, it opens a result set, and for each record returned, 
		///	it creates and loads a ModelObject. </summary>
		///	<param name="sWhereClause">where clause to be applied to "selectall" statement 
		/// that returns one or more records from the database, corresponding to the ModelObject we are going to load </param>
		///	 <param name="params"> Parameter values to be passed to sql statement </param>
		///	 <returns> A List(Of JobHistory) object containing all objects loaded </returns>
		///	 
		public new List<JobHistory> findList(string sWhereClause, params object[] @params) {

			string sql = this.getSqlWithWhereClause(sWhereClause);
			IDataReader rs = null;
			List<JobHistory> molist = new List<JobHistory>();

			try {
				rs = dbConn.getDataReaderWithParams(sql, @params);
				this.Loader.DataSource = rs;

				while (rs.Read()) {
					IModelObject mo = this.getModelInstance();
					this.Loader.load(mo);
					molist.Add((JobHistory)mo);

				}


			} finally {
				this.dbConn.closeDataReader(rs);
			}

			return molist;

		}

		///    
		///	 <summary>Returns all records from database for a coresponding ModelObject </summary>
		/// <returns>List(Of JobHistory) </returns>
		public List<JobHistory> findAll()
		{
			return this.findList(string.Empty);
		}

		public override IModelObjectLoader Loader {
			get {
				if (this._loader == null) {
					this._loader = new JobHistoryDataReaderLoader();
				}
				return this._loader;
			}
			set { this._loader = value; }
		}

		#endregion

		public override IModelObject getModelInstance()
		{
			return new JobHistory();
		}

	}

	#region " JobHistory Loader "
	[System.Runtime.InteropServices.ComVisible(false)]
	public class JobHistoryDataReaderLoader : DataReaderLoader {
		public override void load(IModelObject mo) {
			const int DATAREADER_FLD_JOB_HISTORY_ID = 0;
			const int DATAREADER_FLD_EMPLOYEE_ID = 1;
			const int DATAREADER_FLD_START_DATE = 2;
			const int DATAREADER_FLD_END_DATE = 3;
			const int DATAREADER_FLD_JOB_ID = 4;
			const int DATAREADER_FLD_DEPARTMENT_ID = 5;
			const int DATAREADER_FLD_CREATE_DATE = 6;
			const int DATAREADER_FLD_UPDATE_DATE = 7;
			const int DATAREADER_FLD_CREATE_USER = 8;
			const int DATAREADER_FLD_UPDATE_USER = 9;

			JobHistory obj = (JobHistory)mo;
			obj.IsObjectLoading = true;

			if (!this.reader.IsDBNull(DATAREADER_FLD_JOB_HISTORY_ID) ) {
				obj.PrJobHistoryId = this.reader.GetInt64(DATAREADER_FLD_JOB_HISTORY_ID);
			}
			if (!this.reader.IsDBNull(DATAREADER_FLD_EMPLOYEE_ID) ) {
				obj.PrEmployeeId = this.reader.GetInt64(DATAREADER_FLD_EMPLOYEE_ID);
			}
			if (!this.reader.IsDBNull(DATAREADER_FLD_START_DATE) ) {
				obj.PrStartDate = this.reader.GetDateTime(DATAREADER_FLD_START_DATE);
			}
			if (!this.reader.IsDBNull(DATAREADER_FLD_END_DATE) ) {
				obj.PrEndDate = this.reader.GetDateTime(DATAREADER_FLD_END_DATE);
			}
			if (!this.reader.IsDBNull(DATAREADER_FLD_JOB_ID) ) {
				obj.PrJobId = this.reader.GetString(DATAREADER_FLD_JOB_ID);
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
	/// Final Class with convinience shared methods for loading/saving the JobHistoryRank ModelObject. 
	///</summary>
	[System.Runtime.InteropServices.ComVisible(false)]
	public sealed class JobHistoryDataUtils {

		#region "Shared ""get"" Functions "

		public static List<JobHistory> findList(string @where, params object[] @params){

			JobHistoryDBMapper dbm = new JobHistoryDBMapper();
			return dbm.findList(@where, @params);

		}

		public static JobHistory findOne(string @where, params object[] @params) {

			JobHistoryDBMapper dbm = new JobHistoryDBMapper();
			return (JobHistory)dbm.findWhere(@where, @params);

		}

		public static List<JobHistory> findList(){

			return new JobHistoryDBMapper().findAll();

		}

		public static JobHistory findByKey(object id) {

			return (JobHistory)new JobHistoryDBMapper().findByKey(id);

		}

		/// <summary>
		/// Reload the JobHistory from the database
		/// </summary>
		/// <remarks>
		/// use this method when you want to relad the JobHistory 
		/// from the database, discarding any changes
		/// </remarks>
		public static void reload(ref JobHistory mo) {

			if (mo == null) {
				throw new System.ArgumentNullException("null object past to reload function");
			}

			mo = (JobHistory)new JobHistoryDBMapper().findByKey(mo.Id);

		}

		#endregion

		#region "Shared Save and Delete Functions"
		/// <summary>
		/// Convinience method to save a JobHistory Object.
		/// Important note: DO NOT CALL THIS IN A LOOP!
		/// </summary>
		/// <param name="JobHistoryObj"></param>
		/// <remarks>
		/// Important note: DO NOT CALL THIS IN A LOOP!  
		/// This method simply instantiates a JobHistoryDBMapper and calls the save method
		/// </remarks>
		public static void saveJobHistory(params JobHistory[] JobHistoryObj)
		{

			JobHistoryDBMapper dbm = new JobHistoryDBMapper();
			dbm.saveList(JobHistoryObj.ToList());


		}


		public static void deleteJobHistory(JobHistory JobHistoryObj)
		{

			JobHistoryDBMapper dbm = new JobHistoryDBMapper();
			dbm.delete(JobHistoryObj);

		}
		#endregion

		#region "Data Table and data row load/save "

		public static void saveJobHistory(DataRow dr, ref JobHistory mo) {
			if (mo == null) {
				mo = new JobHistory();
			}

			foreach (DataColumn dc in dr.Table.Columns) {
				mo.setAttribute(dc.ColumnName, dr[dc.ColumnName]);
			}

			saveJobHistory(mo);

		}

		public static void saveJobHistory(DataTable dt, ref JobHistory mo) {
			foreach (DataRow dr in dt.Rows) {
				saveJobHistory(dr, ref mo);
			}

		}

		public static JobHistory loadFromDataRow(DataRow r) {

			DataRowLoader a = new DataRowLoader();
			IModelObject mo = new JobHistory();
			a.DataSource = r;
			a.load(mo);
			return (JobHistory)mo;

		}

		#endregion

	}

}


