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
// Class autogenerated on14/06/2015 8:19:29 AM by ModelGenerator
// Extends base DBMapperBase object class
// *** DO NOT change code in this class.  
//     It will be re-generated and 
//     overwritten by the code generator ****
// Instead, change code in the extender class EmployeeEvaluationDBMapper
//
//************************************************************
//</comments>

namespace CsModelMappers {

	[System.Runtime.InteropServices.ComVisible(false)]
	public class EmployeeEvaluationDBMapper : DBMapper {

		#region "Constructors "
		public EmployeeEvaluationDBMapper(DBUtils _dbConn) : base(_dbConn) {
		}


		public EmployeeEvaluationDBMapper() : base() {
		}
		#endregion

		#region "Overloaded Functions"

		public new EmployeeEvaluation findWhere(string sWhereClause, params object[] @params) {

			return (EmployeeEvaluation)base.findWhere(sWhereClause, @params);
		}


		public void saveEmployeeEvaluation(EmployeeEvaluation mo) {
			base.save(mo);
		}

		public new EmployeeEvaluation findByKey(object keyval) {

			return (EmployeeEvaluation)base.findByKey(keyval);

		}

		#endregion


		#region "getUpdateDBCommand"
		public override IDbCommand getUpdateDBCommand(IModelObject modelObj, string sql) {
			EmployeeEvaluation obj = (EmployeeEvaluation)modelObj;
			IDbCommand stmt = this.dbConn.getCommand(sql);
			stmt.Parameters.Add(this.dbConn.getParameter(EmployeeEvaluation.STR_FLD_EVALUATOR_ID,obj.PrEvaluatorId));
			stmt.Parameters.Add(this.dbConn.getParameter(EmployeeEvaluation.STR_FLD_EVALUATION_DATE,obj.PrEvaluationDate));
			stmt.Parameters.Add(this.dbConn.getParameter(EmployeeEvaluation.STR_FLD_EMPLOYEE_ID,obj.PrEmployeeId));

			if ( obj.isNew ) {
			} else {
			//only add primary key if we are updating and as the last parameter
							stmt.Parameters.Add(this.dbConn.getParameter(EmployeeEvaluation.STR_FLD_EMPLOYEE_EVALUATION_ID,obj.PrEmployeeEvaluationId));
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
		///	 <returns> A List(Of EmployeeEvaluation) object containing all objects loaded </returns>
		///	 
		public new List<EmployeeEvaluation> findList(string sWhereClause, params object[] @params) {

			string sql = this.getSqlWithWhereClause(sWhereClause);
			IDataReader rs = null;
			List<EmployeeEvaluation> molist = new List<EmployeeEvaluation>();

			try {
				rs = dbConn.getDataReaderWithParams(sql, @params);
				this.Loader.DataSource = rs;

				while (rs.Read()) {
					IModelObject mo = this.getModelInstance();
					this.Loader.load(mo);
					molist.Add((EmployeeEvaluation)mo);

				}


			} finally {
				this.dbConn.closeDataReader(rs);
			}

			return molist;

		}

		///    
		///	 <summary>Returns all records from database for a coresponding ModelObject </summary>
		/// <returns>List(Of EmployeeEvaluation) </returns>
		public List<EmployeeEvaluation> findAll()
		{
			return this.findList(string.Empty);
		}

		public override IModelObjectLoader Loader {
			get {
				if (this._loader == null) {
					this._loader = new EmployeeEvaluationDataReaderLoader();
				}
				return this._loader;
			}
			set { this._loader = value; }
		}

		#endregion

		protected override String pkFieldName(){
			return "Employee_Evaluation_Id";
		}
		
		public override IModelObject getModelInstance() {
			return new EmployeeEvaluation();
		}

	}

	#region " EmployeeEvaluation Loader "
	[System.Runtime.InteropServices.ComVisible(false)]
	public class EmployeeEvaluationDataReaderLoader : DataReaderLoader {
		public override void load(IModelObject mo) {
			const int DATAREADER_FLD_EMPLOYEE_EVALUATION_ID = 0;
			const int DATAREADER_FLD_EVALUATOR_ID = 1;
			const int DATAREADER_FLD_EVALUATION_DATE = 2;
			const int DATAREADER_FLD_EMPLOYEE_ID = 3;

			EmployeeEvaluation obj = (EmployeeEvaluation)mo;
			obj.IsObjectLoading = true;

			if (!this.reader.IsDBNull(DATAREADER_FLD_EMPLOYEE_EVALUATION_ID) ) {
				obj.PrEmployeeEvaluationId = this.reader.GetInt32(DATAREADER_FLD_EMPLOYEE_EVALUATION_ID);
			}
			if (!this.reader.IsDBNull(DATAREADER_FLD_EVALUATOR_ID) ) {
				obj.PrEvaluatorId = this.reader.GetInt32(DATAREADER_FLD_EVALUATOR_ID);
			}
			if (!this.reader.IsDBNull(DATAREADER_FLD_EVALUATION_DATE) ) {
				obj.PrEvaluationDate = this.reader.GetDateTime(DATAREADER_FLD_EVALUATION_DATE);
			}
			if (!this.reader.IsDBNull(DATAREADER_FLD_EMPLOYEE_ID) ) {
				obj.PrEmployeeId = this.reader.GetInt32(DATAREADER_FLD_EMPLOYEE_ID);
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
	/// Final Class with convinience shared methods for loading/saving the EmployeeEvaluationRank ModelObject. 
	///</summary>
	[System.Runtime.InteropServices.ComVisible(false)]
	public sealed class EmployeeEvaluationDataUtils {

		#region "Shared ""get"" Functions "

		public static List<EmployeeEvaluation> findList(string @where, params object[] @params){

			EmployeeEvaluationDBMapper dbm = new EmployeeEvaluationDBMapper();
			return dbm.findList(@where, @params);

		}

		public static EmployeeEvaluation findOne(string @where, params object[] @params) {

			EmployeeEvaluationDBMapper dbm = new EmployeeEvaluationDBMapper();
			return (EmployeeEvaluation)dbm.findWhere(@where, @params);

		}

		public static List<EmployeeEvaluation> findList(){

			return new EmployeeEvaluationDBMapper().findAll();

		}

		public static EmployeeEvaluation findByKey(object id) {

			return (EmployeeEvaluation)new EmployeeEvaluationDBMapper().findByKey(id);

		}

		/// <summary>
		/// Reload the EmployeeEvaluation from the database
		/// </summary>
		/// <remarks>
		/// use this method when you want to relad the EmployeeEvaluation 
		/// from the database, discarding any changes
		/// </remarks>
		public static void reload(ref EmployeeEvaluation mo) {

			if (mo == null) {
				throw new System.ArgumentNullException("null object past to reload function");
			}

			mo = (EmployeeEvaluation)new EmployeeEvaluationDBMapper().findByKey(mo.Id);

		}

		#endregion

		#region "Shared Save and Delete Functions"
		/// <summary>
		/// Convinience method to save a EmployeeEvaluation Object.
		/// Important note: DO NOT CALL THIS IN A LOOP!
		/// </summary>
		/// <param name="EmployeeEvaluationObj"></param>
		/// <remarks>
		/// Important note: DO NOT CALL THIS IN A LOOP!  
		/// This method simply instantiates a EmployeeEvaluationDBMapper and calls the save method
		/// </remarks>
		public static void saveEmployeeEvaluation(params EmployeeEvaluation[] EmployeeEvaluationObj)
		{

			EmployeeEvaluationDBMapper dbm = new EmployeeEvaluationDBMapper();
			dbm.saveList(EmployeeEvaluationObj.ToList());


		}


		public static void deleteEmployeeEvaluation(EmployeeEvaluation EmployeeEvaluationObj)
		{

			EmployeeEvaluationDBMapper dbm = new EmployeeEvaluationDBMapper();
			dbm.delete(EmployeeEvaluationObj);

		}
		#endregion

		#region "Data Table and data row load/save "

		public static void saveEmployeeEvaluation(DataRow dr, ref EmployeeEvaluation mo) {
			if (mo == null) {
				mo = new EmployeeEvaluation();
			}

			foreach (DataColumn dc in dr.Table.Columns) {
				mo.setAttribute(dc.ColumnName, dr[dc.ColumnName]);
			}

			saveEmployeeEvaluation(mo);

		}

		public static void saveEmployeeEvaluation(DataTable dt, ref EmployeeEvaluation mo) {
			foreach (DataRow dr in dt.Rows) {
				saveEmployeeEvaluation(dr, ref mo);
			}

		}

		public static EmployeeEvaluation loadFromDataRow(DataRow r) {

			DataRowLoader a = new DataRowLoader();
			IModelObject mo = new EmployeeEvaluation();
			a.DataSource = r;
			a.load(mo);
			return (EmployeeEvaluation)mo;

		}

		#endregion

	}

}


