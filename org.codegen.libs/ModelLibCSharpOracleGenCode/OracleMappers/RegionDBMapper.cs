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
using System.Data.Linq.Mapping;

//<comments>
// Template: DBMapperBase.csharp.txt
//************************************************************
// 
// Class autogenerated on19-Dec-17 9:38:02 AM by ModelGenerator
// Extends base DBMapperBase object class
// *** DO NOT change code in this class.  
//     It will be re-generated and 
//     overwritten by the code generator ****
// Instead, change code in the extender class RegionDBMapper
//
//************************************************************
//</comments>

namespace OracleMappers {

	[System.Runtime.InteropServices.ComVisible(false)]
	[Table(Name = "REGIONS")]
	[SelectObject("REGIONS")][KeyFieldName("REGION_ID")]
	public class RegionDBMapper : OracleDBMapper {

		#region "Constructors "
		public RegionDBMapper(DBUtils _dbConn) : base(_dbConn) {
		}


		public RegionDBMapper() : base() {
		}
		#endregion

		#region "Overloaded Functions"

		public new Region findWhere(string sWhereClause, params object[] @params) {

			return (Region)base.findWhere(sWhereClause, @params);
		}


		public void saveRegion(Region mo) {
			base.save(mo);
		}

		public new Region findByKey(object keyval) {

			return (Region)base.findByKey(keyval);

		}

		#endregion


		#region "getUpdateDBCommand"
		public override IDbCommand getUpdateDBCommand(IModelObject modelObj, string sql) {
			Region obj = (Region)modelObj;
			IDbCommand stmt = this.dbConn.getCommand(sql);
			stmt.Parameters.Add(this.dbConn.getParameter(Region.STR_FLD_REGION_NAME,obj.PrRegionName));

			if (obj.isNew){
				IDataParameter prm = this.dbConn.getParameterInOut(Region.STR_FLD_REGION_ID);
				prm.DbType = DbType.Int64;
				prm.Direction = ParameterDirection.Output;
				stmt.Parameters.Add(prm);

			} else {
			//only add primary key if we are updating and as the last parameter
				stmt.Parameters.Add(this.dbConn.getParameter(Region.STR_FLD_REGION_ID,obj.PrRegionId));
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
		///	<returns> A List(Of Region) object containing all objects loaded </returns>
		///	 
		public new List<Region> findList(string sWhereClause, params object[] @params) {

			string sql = this.getSqlWithWhereClause(sWhereClause);
			IDataReader rs = null;
			List<Region> molist = new List<Region>();

			try {
				rs = dbConn.getDataReaderWithParams(sql, @params);
				this.Loader.DataSource = rs;

				while (rs.Read()) {
					IModelObject mo = this.getModelInstance();
					this.Loader.load(mo);
					molist.Add((Region)mo);

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
		///	<returns> A List(Of Region) object containing all objects loaded </returns>
		///	 
		public List<Region> findList(string sWhereClause, List<IDataParameter> @params) {

			string sql = this.getSqlWithWhereClause(sWhereClause);
			IDataReader rs = null;
			List<Region> molist = new List<Region>();

			try {
				rs = dbConn.getDataReader(sql, @params);
				this.Loader.DataSource = rs;

				while (rs.Read()) {
					IModelObject mo = this.getModelInstance();
					this.Loader.load(mo);
					molist.Add((Region)mo);

				}


			} finally {
				this.dbConn.closeDataReader(rs);
			}

			return molist;

		}


		///    
		///	 <summary>Returns all records from database for a coresponding ModelObject </summary>
		/// <returns>List(Of Region) </returns>
		public List<Region> findAll()
		{
			return this.findList(string.Empty);
		}

		public override IModelObjectLoader Loader {
			get {
				if (this._loader == null) {
					this._loader = new RegionDataReaderLoader();
				}
				return this._loader;
			}
			set { this._loader = value; }
		}

		#endregion

		public override IModelObject getModelInstance() {
			return new Region();
		}

	}

	#region " Region Loader "
	[System.Runtime.InteropServices.ComVisible(false)]
	public class RegionDataReaderLoader : DataReaderLoader {
		public override void load(IModelObject mo) {
			const int DATAREADER_FLD_REGION_ID = 0;
			const int DATAREADER_FLD_REGION_NAME = 1;

			Region obj = (Region)mo;
			obj.IsObjectLoading = true;

			if (!this.reader.IsDBNull(DATAREADER_FLD_REGION_ID) ) {
				obj.PrRegionId = this.reader.GetInt64(DATAREADER_FLD_REGION_ID);
			}
			if (!this.reader.IsDBNull(DATAREADER_FLD_REGION_NAME) ) {
				obj.PrRegionName = this.reader.GetString(DATAREADER_FLD_REGION_NAME);
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
	/// Final Class with convinience shared methods for loading/saving the RegionRank ModelObject. 
	///</summary>
	[System.Runtime.InteropServices.ComVisible(false)]
	public sealed class RegionDataUtils {

		#region "Shared ""get"" Functions "

		public static List<Region> findList(string @where, params object[] @params) {

			RegionDBMapper dbm = new RegionDBMapper();
			return dbm.findList(@where, @params);

		}

		public static List<Region> findList(string @where, List<IDataParameter> listOfIParams) {

			RegionDBMapper dbm = new RegionDBMapper();
			return dbm.findList(@where,listOfIParams);

		}

		public static Region findOne(string @where, params object[] @params) {

			RegionDBMapper dbm = new RegionDBMapper();
			return (Region)dbm.findWhere(@where, @params);

		}

		public static List<Region> findList(){

			return new RegionDBMapper().findAll();

		}

		public static Region findByKey(object id) {

			return (Region)new RegionDBMapper().findByKey(id);

		}

		/// <summary>
		/// Reload the Region from the database
		/// </summary>
		/// <remarks>
		/// use this method when you want to relad the Region 
		/// from the database, discarding any changes
		/// </remarks>
		public static void reload(ref Region mo) {

			if (mo == null) {
				throw new System.ArgumentNullException("null object past to reload function");
			}

			mo = (Region)new RegionDBMapper().findByKey(mo.Id);

		}

		#endregion

		#region "Shared Save and Delete Functions"
		/// <summary>
		/// Convinience method to save a Region Object.
		/// Important note: DO NOT CALL THIS IN A LOOP!
		/// </summary>
		/// <param name="RegionObj"></param>
		/// <remarks>
		/// Important note: DO NOT CALL THIS IN A LOOP!  
		/// This method simply instantiates a RegionDBMapper and calls the save method
		/// </remarks>
		public static void saveRegion(params Region[] RegionObj)
		{

			RegionDBMapper dbm = new RegionDBMapper();
			dbm.saveList(RegionObj.ToList());


		}


		public static void deleteRegion(Region RegionObj)
		{

			RegionDBMapper dbm = new RegionDBMapper();
			dbm.delete(RegionObj);

		}
		#endregion

		#region "Data Table and data row load/save "

		public static void saveRegion(DataRow dr, ref Region mo) {
			if (mo == null) {
				mo = new Region();
			}

			foreach (DataColumn dc in dr.Table.Columns) {
				mo.setAttribute(dc.ColumnName, dr[dc.ColumnName]);
			}

			saveRegion(mo);

		}

		public static void saveRegion(DataTable dt, ref Region mo) {
			foreach (DataRow dr in dt.Rows) {
				saveRegion(dr, ref mo);
			}

		}

		public static Region loadFromDataRow(DataRow r) {

			DataRowLoader a = new DataRowLoader();
			IModelObject mo = new Region();
			a.DataSource = r;
			a.load(mo);
			return (Region)mo;

		}

		#endregion

	}

}


