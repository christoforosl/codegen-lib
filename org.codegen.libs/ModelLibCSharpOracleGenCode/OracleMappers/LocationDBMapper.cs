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
// Class autogenerated on31/05/2015 11:00:45 PM by ModelGenerator
// Extends base DBMapperBase object class
// *** DO NOT change code in this class.  
//     It will be re-generated and 
//     overwritten by the code generator ****
// Instead, change code in the extender class LocationDBMapper
//
//************************************************************
//</comments>

namespace OracleMappers {

	[System.Runtime.InteropServices.ComVisible(false)]
	public class LocationDBMapper : DBMapper {

		#region "Constructors "
		public LocationDBMapper(DBUtils _dbConn) : base(_dbConn) {
		}


		public LocationDBMapper() : base() {
		}
		#endregion

		#region "Overloaded Functions"

		public new Location findWhere(string sWhereClause, params object[] @params) {

			return (Location)base.findWhere(sWhereClause, @params);
		}


		public void saveLocation(Location mo) {
			base.save(mo);
		}

		public new Location findByKey(object keyval) {

			return (Location)base.findByKey(keyval);

		}

		#endregion


		#region "getUpdateDBCommand"
		public override IDbCommand getUpdateDBCommand(IModelObject modelObj, string sql) {
			Location obj = (Location)modelObj;
			IDbCommand stmt = this.dbConn.getCommand(sql);
				stmt.Parameters.Add(this.dbConn.getParameter("@STREET_ADDRESS",obj.PrStreetAddress));
				stmt.Parameters.Add(this.dbConn.getParameter("@POSTAL_CODE",obj.PrPostalCode));
				stmt.Parameters.Add(this.dbConn.getParameter("@CITY",obj.PrCITY));
				stmt.Parameters.Add(this.dbConn.getParameter("@STATE_PROVINCE",obj.PrStateProvince));
				stmt.Parameters.Add(this.dbConn.getParameter("@COUNTRY_ID",obj.PrCountryId));
				stmt.Parameters.Add(this.dbConn.getParameter("@CREATE_DATE",obj.CreateDate));
				stmt.Parameters.Add(this.dbConn.getParameter("@UPDATE_DATE",obj.UpdateDate));
				stmt.Parameters.Add(this.dbConn.getParameter("@CREATE_USER",obj.CreateUser));
				stmt.Parameters.Add(this.dbConn.getParameter("@UPDATE_USER",obj.UpdateUser));

			if ( obj.isNew ) {
			} else {
			//only add primary key if we are updating and as the last parameter
								stmt.Parameters.Add(this.dbConn.getParameter("@LOCATION_ID",obj.PrLocationId));
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
		///	 <returns> A List(Of Location) object containing all objects loaded </returns>
		///	 
		public new List<Location> findList(string sWhereClause, params object[] @params) {

			string sql = this.getSqlWithWhereClause(sWhereClause);
			IDataReader rs = null;
			List<Location> molist = new List<Location>();

			try {
				rs = dbConn.getDataReaderWithParams(sql, @params);
				this.Loader.DataSource = rs;

				while (rs.Read()) {
					IModelObject mo = this.getModelInstance();
					this.Loader.load(mo);
					molist.Add((Location)mo);

				}


			} finally {
				this.dbConn.closeDataReader(rs);
			}

			return molist;

		}

		///    
		///	 <summary>Returns all records from database for a coresponding ModelObject </summary>
		/// <returns>List(Of Location) </returns>
		public List<Location> findAll()
		{
			return this.findList(string.Empty);
		}

		public override IModelObjectLoader Loader {
			get {
				if (this._loader == null) {
					this._loader = new LocationDataReaderLoader();
				}
				return this._loader;
			}
			set { this._loader = value; }
		}

		#endregion

		protected override String pkFieldName(){
			return "LOCATION_ID";
		}
		
		public override IModelObject getModelInstance() {
			return new Location();
		}

	}

	#region " Location Loader "
	[System.Runtime.InteropServices.ComVisible(false)]
	public class LocationDataReaderLoader : DataReaderLoader {
		public override void load(IModelObject mo) {
			const int DATAREADER_FLD_LOCATION_ID = 0;
			const int DATAREADER_FLD_STREET_ADDRESS = 1;
			const int DATAREADER_FLD_POSTAL_CODE = 2;
			const int DATAREADER_FLD_CITY = 3;
			const int DATAREADER_FLD_STATE_PROVINCE = 4;
			const int DATAREADER_FLD_COUNTRY_ID = 5;
			const int DATAREADER_FLD_CREATE_DATE = 6;
			const int DATAREADER_FLD_UPDATE_DATE = 7;
			const int DATAREADER_FLD_CREATE_USER = 8;
			const int DATAREADER_FLD_UPDATE_USER = 9;

			Location obj = (Location)mo;
			obj.IsObjectLoading = true;

			if (!this.reader.IsDBNull(DATAREADER_FLD_LOCATION_ID) ) {
				obj.PrLocationId = this.reader.GetInt64(DATAREADER_FLD_LOCATION_ID);
			}
			if (!this.reader.IsDBNull(DATAREADER_FLD_STREET_ADDRESS) ) {
				obj.PrStreetAddress = this.reader.GetString(DATAREADER_FLD_STREET_ADDRESS);
			}
			if (!this.reader.IsDBNull(DATAREADER_FLD_POSTAL_CODE) ) {
				obj.PrPostalCode = this.reader.GetString(DATAREADER_FLD_POSTAL_CODE);
			}
			if (!this.reader.IsDBNull(DATAREADER_FLD_CITY) ) {
				obj.PrCITY = this.reader.GetString(DATAREADER_FLD_CITY);
			}
			if (!this.reader.IsDBNull(DATAREADER_FLD_STATE_PROVINCE) ) {
				obj.PrStateProvince = this.reader.GetString(DATAREADER_FLD_STATE_PROVINCE);
			}
			if (!this.reader.IsDBNull(DATAREADER_FLD_COUNTRY_ID) ) {
				obj.PrCountryId = this.reader.GetString(DATAREADER_FLD_COUNTRY_ID);
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
	/// Final Class with convinience shared methods for loading/saving the LocationRank ModelObject. 
	///</summary>
	[System.Runtime.InteropServices.ComVisible(false)]
	public sealed class LocationDataUtils {

		#region "Shared ""get"" Functions "

		public static List<Location> findList(string @where, params object[] @params){

			LocationDBMapper dbm = new LocationDBMapper();
			return dbm.findList(@where, @params);

		}

		public static Location findOne(string @where, params object[] @params) {

			LocationDBMapper dbm = new LocationDBMapper();
			return (Location)dbm.findWhere(@where, @params);

		}

		public static List<Location> findList(){

			return new LocationDBMapper().findAll();

		}

		public static Location findByKey(object id) {

			return (Location)new LocationDBMapper().findByKey(id);

		}

		/// <summary>
		/// Reload the Location from the database
		/// </summary>
		/// <remarks>
		/// use this method when you want to relad the Location 
		/// from the database, discarding any changes
		/// </remarks>
		public static void reload(ref Location mo) {

			if (mo == null) {
				throw new System.ArgumentNullException("null object past to reload function");
			}

			mo = (Location)new LocationDBMapper().findByKey(mo.Id);

		}

		#endregion

		#region "Shared Save and Delete Functions"
		/// <summary>
		/// Convinience method to save a Location Object.
		/// Important note: DO NOT CALL THIS IN A LOOP!
		/// </summary>
		/// <param name="LocationObj"></param>
		/// <remarks>
		/// Important note: DO NOT CALL THIS IN A LOOP!  
		/// This method simply instantiates a LocationDBMapper and calls the save method
		/// </remarks>
		public static void saveLocation(params Location[] LocationObj)
		{

			LocationDBMapper dbm = new LocationDBMapper();
			dbm.saveList(LocationObj.ToList());


		}


		public static void deleteLocation(Location LocationObj)
		{

			LocationDBMapper dbm = new LocationDBMapper();
			dbm.delete(LocationObj);

		}
		#endregion

		#region "Data Table and data row load/save "

		public static void saveLocation(DataRow dr, ref Location mo) {
			if (mo == null) {
				mo = new Location();
			}

			foreach (DataColumn dc in dr.Table.Columns) {
				mo.setAttribute(dc.ColumnName, dr[dc.ColumnName]);
			}

			saveLocation(mo);

		}

		public static void saveLocation(DataTable dt, ref Location mo) {
			foreach (DataRow dr in dt.Rows) {
				saveLocation(dr, ref mo);
			}

		}

		public static Location loadFromDataRow(DataRow r) {

			DataRowLoader a = new DataRowLoader();
			IModelObject mo = new Location();
			a.DataSource = r;
			a.load(mo);
			return (Location)mo;

		}

		#endregion

	}

}


