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
// Class autogenerated on17-Jun-15 6:41:15 PM by ModelGenerator
// Extends base DBMapperBase object class
// *** DO NOT change code in this class.  
//     It will be re-generated and 
//     overwritten by the code generator ****
// Instead, change code in the extender class CountryDBMapper
//
//************************************************************
//</comments>

namespace OracleMappers {

	[System.Runtime.InteropServices.ComVisible(false)]
[AttrIsPrimaryKeyAutogenerated(false)]
	public class CountryDBMapper : OracleDBMapper {

		#region "Constructors "
		public CountryDBMapper(DBUtils _dbConn) : base(_dbConn) {
		}


		public CountryDBMapper() : base() {
		}
		#endregion

		#region "Overloaded Functions"

		public new Country findWhere(string sWhereClause, params object[] @params) {

			return (Country)base.findWhere(sWhereClause, @params);
		}


		public void saveCountry(Country mo) {
			base.save(mo);
		}

		public new Country findByKey(object keyval) {

			return (Country)base.findByKey(keyval);

		}

		#endregion


		#region "getUpdateDBCommand"
		public override IDbCommand getUpdateDBCommand(IModelObject modelObj, string sql) {
			Country obj = (Country)modelObj;
			IDbCommand stmt = this.dbConn.getCommand(sql);
			stmt.Parameters.Add(this.dbConn.getParameter(Country.STR_FLD_COUNTRY_NAME,obj.PrCountryName));
			stmt.Parameters.Add(this.dbConn.getParameter(Country.STR_FLD_REGION_ID,obj.PrRegionId));
			stmt.Parameters.Add(this.dbConn.getParameter(Country.STR_FLD_SKIP_FIELD,obj.PrSkipField));
			stmt.Parameters.Add(this.dbConn.getParameter(Country.STR_FLD_LONG_FLD,obj.PrLongFld));
			stmt.Parameters.Add(this.dbConn.getParameter(Country.STR_FLD_LONG_FLD2,obj.PrLongFld2));

			stmt.Parameters.Add(this.dbConn.getParameter(Country.STR_FLD_COUNTRY_ID,obj.PrCountryId));


			return stmt;
		}
		#endregion



		#region "Find functions"

		///	<summary>Given an sql statement, it opens a result set, and for each record returned, 
		///	it creates and loads a ModelObject. </summary>
		///	<param name="sWhereClause">where clause to be applied to "selectall" statement 
		/// that returns one or more records from the database, corresponding to the ModelObject we are going to load </param>
		///	 <param name="params"> Parameter values to be passed to sql statement </param>
		///	 <returns> A List(Of Country) object containing all objects loaded </returns>
		///	 
		public new List<Country> findList(string sWhereClause, params object[] @params) {

			string sql = this.getSqlWithWhereClause(sWhereClause);
			IDataReader rs = null;
			List<Country> molist = new List<Country>();

			try {
				rs = dbConn.getDataReaderWithParams(sql, @params);
				this.Loader.DataSource = rs;

				while (rs.Read()) {
					IModelObject mo = this.getModelInstance();
					this.Loader.load(mo);
					molist.Add((Country)mo);

				}


			} finally {
				this.dbConn.closeDataReader(rs);
			}

			return molist;

		}

		///    
		///	 <summary>Returns all records from database for a coresponding ModelObject </summary>
		/// <returns>List(Of Country) </returns>
		public List<Country> findAll()
		{
			return this.findList(string.Empty);
		}

		public override IModelObjectLoader Loader {
			get {
				if (this._loader == null) {
					this._loader = new CountryDataReaderLoader();
				}
				return this._loader;
			}
			set { this._loader = value; }
		}

		#endregion

		protected override String pkFieldName(){
			return "COUNTRY_ID";
		}
		
		public override IModelObject getModelInstance() {
			return new Country();
		}

	}

	#region " Country Loader "
	[System.Runtime.InteropServices.ComVisible(false)]
	public class CountryDataReaderLoader : DataReaderLoader {
		public override void load(IModelObject mo) {
			const int DATAREADER_FLD_COUNTRY_ID = 0;
			const int DATAREADER_FLD_COUNTRY_NAME = 1;
			const int DATAREADER_FLD_REGION_ID = 2;
			const int DATAREADER_FLD_SKIP_FIELD = 3;
			const int DATAREADER_FLD_LONG_FLD = 4;
			const int DATAREADER_FLD_LONG_FLD2 = 5;

			Country obj = (Country)mo;
			obj.IsObjectLoading = true;

			if (!this.reader.IsDBNull(DATAREADER_FLD_COUNTRY_ID) ) {
				obj.PrCountryId = this.reader.GetString(DATAREADER_FLD_COUNTRY_ID);
			}
			if (!this.reader.IsDBNull(DATAREADER_FLD_COUNTRY_NAME) ) {
				obj.PrCountryName = this.reader.GetString(DATAREADER_FLD_COUNTRY_NAME);
			}
			if (!this.reader.IsDBNull(DATAREADER_FLD_REGION_ID) ) {
				obj.PrRegionId = Convert.ToInt64(this.reader.GetDecimal(DATAREADER_FLD_REGION_ID));
			}
			if (!this.reader.IsDBNull(DATAREADER_FLD_SKIP_FIELD) ) {
				obj.PrSkipField = this.reader.GetString(DATAREADER_FLD_SKIP_FIELD);
			}
			if (!this.reader.IsDBNull(DATAREADER_FLD_LONG_FLD) ) {
				obj.PrLongFld = Convert.ToInt64(this.reader.GetDecimal(DATAREADER_FLD_LONG_FLD));
			}
			if (!this.reader.IsDBNull(DATAREADER_FLD_LONG_FLD2) ) {
				obj.PrLongFld2 = Convert.ToInt64(this.reader.GetDecimal(DATAREADER_FLD_LONG_FLD2));
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
	/// Final Class with convinience shared methods for loading/saving the CountryRank ModelObject. 
	///</summary>
	[System.Runtime.InteropServices.ComVisible(false)]
	public sealed class CountryDataUtils {

		#region "Shared ""get"" Functions "

		public static List<Country> findList(string @where, params object[] @params){

			CountryDBMapper dbm = new CountryDBMapper();
			return dbm.findList(@where, @params);

		}

		public static Country findOne(string @where, params object[] @params) {

			CountryDBMapper dbm = new CountryDBMapper();
			return (Country)dbm.findWhere(@where, @params);

		}

		public static List<Country> findList(){

			return new CountryDBMapper().findAll();

		}

		public static Country findByKey(object id) {

			return (Country)new CountryDBMapper().findByKey(id);

		}

		/// <summary>
		/// Reload the Country from the database
		/// </summary>
		/// <remarks>
		/// use this method when you want to relad the Country 
		/// from the database, discarding any changes
		/// </remarks>
		public static void reload(ref Country mo) {

			if (mo == null) {
				throw new System.ArgumentNullException("null object past to reload function");
			}

			mo = (Country)new CountryDBMapper().findByKey(mo.Id);

		}

		#endregion

		#region "Shared Save and Delete Functions"
		/// <summary>
		/// Convinience method to save a Country Object.
		/// Important note: DO NOT CALL THIS IN A LOOP!
		/// </summary>
		/// <param name="CountryObj"></param>
		/// <remarks>
		/// Important note: DO NOT CALL THIS IN A LOOP!  
		/// This method simply instantiates a CountryDBMapper and calls the save method
		/// </remarks>
		public static void saveCountry(params Country[] CountryObj)
		{

			CountryDBMapper dbm = new CountryDBMapper();
			dbm.saveList(CountryObj.ToList());


		}


		public static void deleteCountry(Country CountryObj)
		{

			CountryDBMapper dbm = new CountryDBMapper();
			dbm.delete(CountryObj);

		}
		#endregion

		#region "Data Table and data row load/save "

		public static void saveCountry(DataRow dr, ref Country mo) {
			if (mo == null) {
				mo = new Country();
			}

			foreach (DataColumn dc in dr.Table.Columns) {
				mo.setAttribute(dc.ColumnName, dr[dc.ColumnName]);
			}

			saveCountry(mo);

		}

		public static void saveCountry(DataTable dt, ref Country mo) {
			foreach (DataRow dr in dt.Rows) {
				saveCountry(dr, ref mo);
			}

		}

		public static Country loadFromDataRow(DataRow r) {

			DataRowLoader a = new DataRowLoader();
			IModelObject mo = new Country();
			a.DataSource = r;
			a.load(mo);
			return (Country)mo;

		}

		#endregion

	}

}


