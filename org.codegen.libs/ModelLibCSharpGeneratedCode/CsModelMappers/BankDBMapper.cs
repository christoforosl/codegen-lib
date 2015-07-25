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
using System.Data.Linq.Mapping;

//<comments>
// Template: DBMapperBase.csharp.txt
//************************************************************
// 
// Class autogenerated on11/07/2015 12:52:19 PM by ModelGenerator
// Extends base DBMapperBase object class
// *** DO NOT change code in this class.  
//     It will be re-generated and 
//     overwritten by the code generator ****
// Instead, change code in the extender class BankDBMapper
//
//************************************************************
//</comments>

namespace CsModelMappers {

	[System.Runtime.InteropServices.ComVisible(false)]
	[Table(Name = "Bank")]
	[SelectObject("Bank")][KeyFieldName("BANKID")]
	public class BankDBMapper : DBMapper {

		#region "Constructors "
		public BankDBMapper(DBUtils _dbConn) : base(_dbConn) {
		}


		public BankDBMapper() : base() {
		}
		#endregion

		#region "Overloaded Functions"

		public new Bank findWhere(string sWhereClause, params object[] @params) {

			return (Bank)base.findWhere(sWhereClause, @params);
		}


		public void saveBank(Bank mo) {
			base.save(mo);
		}

		public new Bank findByKey(object keyval) {

			return (Bank)base.findByKey(keyval);

		}

		#endregion


		#region "getUpdateDBCommand"
		public override IDbCommand getUpdateDBCommand(IModelObject modelObj, string sql) {
			Bank obj = (Bank)modelObj;
			IDbCommand stmt = this.dbConn.getCommand(sql);
			stmt.Parameters.Add(this.dbConn.getParameter(Bank.STR_FLD_BANKNAME,obj.PrBankName));
			stmt.Parameters.Add(this.dbConn.getParameter(Bank.STR_FLD_BANKCODE,obj.PrBankCode));
			stmt.Parameters.Add(this.dbConn.getParameter(Bank.STR_FLD_BANKSWIFTCODE,obj.PrBankSWIFTCode));

			if (obj.isNew){
			} else {
			//only add primary key if we are updating and as the last parameter
				stmt.Parameters.Add(this.dbConn.getParameter(Bank.STR_FLD_BANKID,obj.PrBANKID));
			}

			return stmt;
		}
		#endregion



		#region "Find functions"

		///	<summary>Given an sql statement, it opens a result set, and for each record returned, 
		///	it creates and loads a ModelObject. 
		/// </summary>
		///	<param name="sWhereClause">where clause to be applied to "selectall" statement 
		/// that returns one or more records from the database, corresponding to the ModelObject we are going to load </param>
		///	<param name="params"> Parameter values to be passed to sql statement </param>
		///	<returns> A List(Of Bank) object containing all objects loaded </returns>
		///	 
		public new List<Bank> findList(string sWhereClause, params object[] @params) {

			string sql = this.getSqlWithWhereClause(sWhereClause);
			IDataReader rs = null;
			List<Bank> molist = new List<Bank>();

			try {
				rs = dbConn.getDataReaderWithParams(sql, @params);
				this.Loader.DataSource = rs;

				while (rs.Read()) {
					IModelObject mo = this.getModelInstance();
					this.Loader.load(mo);
					molist.Add((Bank)mo);

				}


			} finally {
				this.dbConn.closeDataReader(rs);
			}

			return molist;

		}

		///	<summary>Given an sql statement, it opens a result set, and for each record returned, 
		///	it creates and loads a ModelObject. </summary>
		///	<param name="sWhereClause">where clause to be applied to "selectall" statement 
		/// that returns one or more records from the database, corresponding to the ModelObject we are going to load </param>
		///	<param name="params"> List of IDataParameters to be passed to sql statement </param>
		///	<returns> A List(Of Bank) object containing all objects loaded </returns>
		///	 
		public List<Bank> findList(string sWhereClause, List<IDataParameter> @params) {

			string sql = this.getSqlWithWhereClause(sWhereClause);
			IDataReader rs = null;
			List<Bank> molist = new List<Bank>();

			try {
				rs = dbConn.getDataReader(sql, @params);
				this.Loader.DataSource = rs;

				while (rs.Read()) {
					IModelObject mo = this.getModelInstance();
					this.Loader.load(mo);
					molist.Add((Bank)mo);

				}


			} finally {
				this.dbConn.closeDataReader(rs);
			}

			return molist;

		}


		///    
		///	 <summary>Returns all records from database for a coresponding ModelObject </summary>
		/// <returns>List(Of Bank) </returns>
		public List<Bank> findAll()
		{
			return this.findList(string.Empty);
		}

		public override IModelObjectLoader Loader {
			get {
				if (this._loader == null) {
					this._loader = new BankDataReaderLoader();
				}
				return this._loader;
			}
			set { this._loader = value; }
		}

		#endregion

		public override IModelObject getModelInstance() {
			return new Bank();
		}

	}

	#region " Bank Loader "
	[System.Runtime.InteropServices.ComVisible(false)]
	public class BankDataReaderLoader : DataReaderLoader {
		public override void load(IModelObject mo) {
			const int DATAREADER_FLD_BANKID = 0;
			const int DATAREADER_FLD_BANKNAME = 1;
			const int DATAREADER_FLD_BANKCODE = 2;
			const int DATAREADER_FLD_BANKSWIFTCODE = 3;

			Bank obj = (Bank)mo;
			obj.IsObjectLoading = true;

			if (!this.reader.IsDBNull(DATAREADER_FLD_BANKID) ) {
				obj.PrBANKID = Convert.ToInt64(this.reader.GetInt32(DATAREADER_FLD_BANKID));
			}
			if (!this.reader.IsDBNull(DATAREADER_FLD_BANKNAME) ) {
				obj.PrBankName = this.reader.GetString(DATAREADER_FLD_BANKNAME);
			}
			if (!this.reader.IsDBNull(DATAREADER_FLD_BANKCODE) ) {
				obj.PrBankCode = this.reader.GetString(DATAREADER_FLD_BANKCODE);
			}
			if (!this.reader.IsDBNull(DATAREADER_FLD_BANKSWIFTCODE) ) {
				obj.PrBankSWIFTCode = this.reader.GetString(DATAREADER_FLD_BANKSWIFTCODE);
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
	/// Final Class with convinience shared methods for loading/saving the BankRank ModelObject. 
	///</summary>
	[System.Runtime.InteropServices.ComVisible(false)]
	public sealed class BankDataUtils {

		#region "Shared ""get"" Functions "

		public static List<Bank> findList(string @where, params object[] @params) {

			BankDBMapper dbm = new BankDBMapper();
			return dbm.findList(@where, @params);

		}

		public static List<Bank> findList(string @where, List<IDataParameter> listOfIParams) {

			BankDBMapper dbm = new BankDBMapper();
			return dbm.findList(@where,listOfIParams);

		}

		public static Bank findOne(string @where, params object[] @params) {

			BankDBMapper dbm = new BankDBMapper();
			return (Bank)dbm.findWhere(@where, @params);

		}

		public static List<Bank> findList(){

			return new BankDBMapper().findAll();

		}

		public static Bank findByKey(object id) {

			return (Bank)new BankDBMapper().findByKey(id);

		}

		/// <summary>
		/// Reload the Bank from the database
		/// </summary>
		/// <remarks>
		/// use this method when you want to relad the Bank 
		/// from the database, discarding any changes
		/// </remarks>
		public static void reload(ref Bank mo) {

			if (mo == null) {
				throw new System.ArgumentNullException("null object past to reload function");
			}

			mo = (Bank)new BankDBMapper().findByKey(mo.Id);

		}

		#endregion

		#region "Shared Save and Delete Functions"
		/// <summary>
		/// Convinience method to save a Bank Object.
		/// Important note: DO NOT CALL THIS IN A LOOP!
		/// </summary>
		/// <param name="BankObj"></param>
		/// <remarks>
		/// Important note: DO NOT CALL THIS IN A LOOP!  
		/// This method simply instantiates a BankDBMapper and calls the save method
		/// </remarks>
		public static void saveBank(params Bank[] BankObj)
		{

			BankDBMapper dbm = new BankDBMapper();
			dbm.saveList(BankObj.ToList());


		}


		public static void deleteBank(Bank BankObj)
		{

			BankDBMapper dbm = new BankDBMapper();
			dbm.delete(BankObj);

		}
		#endregion

		#region "Data Table and data row load/save "

		public static void saveBank(DataRow dr, ref Bank mo) {
			if (mo == null) {
				mo = new Bank();
			}

			foreach (DataColumn dc in dr.Table.Columns) {
				mo.setAttribute(dc.ColumnName, dr[dc.ColumnName]);
			}

			saveBank(mo);

		}

		public static void saveBank(DataTable dt, ref Bank mo) {
			foreach (DataRow dr in dt.Rows) {
				saveBank(dr, ref mo);
			}

		}

		public static Bank loadFromDataRow(DataRow r) {

			DataRowLoader a = new DataRowLoader();
			IModelObject mo = new Bank();
			a.DataSource = r;
			a.load(mo);
			return (Bank)mo;

		}

		#endregion

	}

}

