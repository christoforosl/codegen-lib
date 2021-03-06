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
// Class autogenerated on11/07/2015 11:47:56 AM by ModelGenerator
// Extends base DBMapperBase object class
// *** DO NOT change code in this class.  
//     It will be re-generated and 
//     overwritten by the code generator ****
// Instead, change code in the extender class ProjectDBMapper
//
//************************************************************
//</comments>

namespace CsModelMappers {

	[System.Runtime.InteropServices.ComVisible(false)]
	[Table(Name = "Project")]
	[SelectObject("Project")][KeyFieldName("ProjectId")]
	public class ProjectDBMapper : DBMapper {

		#region "Constructors "
		public ProjectDBMapper(DBUtils _dbConn) : base(_dbConn) {
		}


		public ProjectDBMapper() : base() {
		}
		#endregion

		#region "Overloaded Functions"

		public new Project findWhere(string sWhereClause, params object[] @params) {

			return (Project)base.findWhere(sWhereClause, @params);
		}


		public void saveProject(Project mo) {
			base.save(mo);
		}

		public new Project findByKey(object keyval) {

			return (Project)base.findByKey(keyval);

		}

		#endregion


		#region "getUpdateDBCommand"
		public override IDbCommand getUpdateDBCommand(IModelObject modelObj, string sql) {
			Project obj = (Project)modelObj;
			IDbCommand stmt = this.dbConn.getCommand(sql);
			stmt.Parameters.Add(this.dbConn.getParameter(Project.STR_FLD_PROJECTNAME,obj.PrProjectName));
			stmt.Parameters.Add(this.dbConn.getParameter(Project.STR_FLD_ISACTIVE,obj.PrIsActive));
			stmt.Parameters.Add(this.dbConn.getParameter(Project.STR_FLD_PROJECTTYPEID,obj.PrProjectTypeId == null? (long?)null : Convert.ToInt64(obj.PrProjectTypeId)));

			if (obj.isNew){
			} else {
			//only add primary key if we are updating and as the last parameter
				stmt.Parameters.Add(this.dbConn.getParameter(Project.STR_FLD_PROJECTID,obj.PrProjectId));
			}

			return stmt;
		}
		#endregion
#region "Save Children Code"
	public override void saveChildren(IModelObject mo) {

		Project ret = (Project)mo;
		//*Child Association:employeeprojects
		if (ret.EmployeeProjectsLoaded) { //scc 
			CsModelMappers.EmployeeProjectDBMapper employeeprojectsMapper = new CsModelMappers.EmployeeProjectDBMapper(this.dbConn);
			employeeprojectsMapper.saveList(ret.PrEmployeeProjects);
			employeeprojectsMapper.deleteList(ret.PrEmployeeProjectsGetDeleted());
		}
	}
#endregion



		#region "Find functions"

		///	<summary>Given an sql statement, it opens a result set, and for each record returned, 
		///	it creates and loads a ModelObject. 
		/// </summary>
		///	<param name="sWhereClause">where clause to be applied to "selectall" statement 
		/// that returns one or more records from the database, corresponding to the ModelObject we are going to load </param>
		///	<param name="params"> Parameter values to be passed to sql statement </param>
		///	<returns> A List(Of Project) object containing all objects loaded </returns>
		///	 
		public new List<Project> findList(string sWhereClause, params object[] @params) {

			string sql = this.getSqlWithWhereClause(sWhereClause);
			IDataReader rs = null;
			List<Project> molist = new List<Project>();

			try {
				rs = dbConn.getDataReaderWithParams(sql, @params);
				this.Loader.DataSource = rs;

				while (rs.Read()) {
					IModelObject mo = this.getModelInstance();
					this.Loader.load(mo);
					molist.Add((Project)mo);

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
		///	<returns> A List(Of Project) object containing all objects loaded </returns>
		///	 
		public List<Project> findList(string sWhereClause, List<IDataParameter> @params) {

			string sql = this.getSqlWithWhereClause(sWhereClause);
			IDataReader rs = null;
			List<Project> molist = new List<Project>();

			try {
				rs = dbConn.getDataReader(sql, @params);
				this.Loader.DataSource = rs;

				while (rs.Read()) {
					IModelObject mo = this.getModelInstance();
					this.Loader.load(mo);
					molist.Add((Project)mo);

				}


			} finally {
				this.dbConn.closeDataReader(rs);
			}

			return molist;

		}


		///    
		///	 <summary>Returns all records from database for a coresponding ModelObject </summary>
		/// <returns>List(Of Project) </returns>
		public List<Project> findAll()
		{
			return this.findList(string.Empty);
		}

		public override IModelObjectLoader Loader {
			get {
				if (this._loader == null) {
					this._loader = new ProjectDataReaderLoader();
				}
				return this._loader;
			}
			set { this._loader = value; }
		}

		#endregion

		public override IModelObject getModelInstance() {
			return new Project();
		}

	}

	#region " Project Loader "
	[System.Runtime.InteropServices.ComVisible(false)]
	public class ProjectDataReaderLoader : DataReaderLoader {
		public override void load(IModelObject mo) {
			const int DATAREADER_FLD_PROJECTID = 0;
			const int DATAREADER_FLD_PROJECTNAME = 1;
			const int DATAREADER_FLD_ISACTIVE = 2;
			const int DATAREADER_FLD_PROJECTTYPEID = 3;

			Project obj = (Project)mo;
			obj.IsObjectLoading = true;

			if (!this.reader.IsDBNull(DATAREADER_FLD_PROJECTID) ) {
				obj.PrProjectId = Convert.ToInt64(this.reader.GetInt32(DATAREADER_FLD_PROJECTID));
			}
			if (!this.reader.IsDBNull(DATAREADER_FLD_PROJECTNAME) ) {
				obj.PrProjectName = this.reader.GetString(DATAREADER_FLD_PROJECTNAME);
			}
			if (!this.reader.IsDBNull(DATAREADER_FLD_ISACTIVE) ) {
				obj.PrIsActive = this.reader.GetInt32(DATAREADER_FLD_ISACTIVE)!=0;
			}
			if (!this.reader.IsDBNull(DATAREADER_FLD_PROJECTTYPEID) ) {
				obj.PrProjectTypeId = (EnumProjectType?)this.reader.GetInt32(DATAREADER_FLD_PROJECTTYPEID);
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
	/// Final Class with convinience shared methods for loading/saving the ProjectRank ModelObject. 
	///</summary>
	[System.Runtime.InteropServices.ComVisible(false)]
	public sealed class ProjectDataUtils {

		#region "Shared ""get"" Functions "

		public static List<Project> findList(string @where, params object[] @params) {

			ProjectDBMapper dbm = new ProjectDBMapper();
			return dbm.findList(@where, @params);

		}

		public static List<Project> findList(string @where, List<IDataParameter> listOfIParams) {

			ProjectDBMapper dbm = new ProjectDBMapper();
			return dbm.findList(@where,listOfIParams);

		}

		public static Project findOne(string @where, params object[] @params) {

			ProjectDBMapper dbm = new ProjectDBMapper();
			return (Project)dbm.findWhere(@where, @params);

		}

		public static List<Project> findList(){

			return new ProjectDBMapper().findAll();

		}

		public static Project findByKey(object id) {

			return (Project)new ProjectDBMapper().findByKey(id);

		}

		/// <summary>
		/// Reload the Project from the database
		/// </summary>
		/// <remarks>
		/// use this method when you want to relad the Project 
		/// from the database, discarding any changes
		/// </remarks>
		public static void reload(ref Project mo) {

			if (mo == null) {
				throw new System.ArgumentNullException("null object past to reload function");
			}

			mo = (Project)new ProjectDBMapper().findByKey(mo.Id);

		}

		#endregion

		#region "Shared Save and Delete Functions"
		/// <summary>
		/// Convinience method to save a Project Object.
		/// Important note: DO NOT CALL THIS IN A LOOP!
		/// </summary>
		/// <param name="ProjectObj"></param>
		/// <remarks>
		/// Important note: DO NOT CALL THIS IN A LOOP!  
		/// This method simply instantiates a ProjectDBMapper and calls the save method
		/// </remarks>
		public static void saveProject(params Project[] ProjectObj)
		{

			ProjectDBMapper dbm = new ProjectDBMapper();
			dbm.saveList(ProjectObj.ToList());


		}


		public static void deleteProject(Project ProjectObj)
		{

			ProjectDBMapper dbm = new ProjectDBMapper();
			dbm.delete(ProjectObj);

		}
		#endregion

		#region "Data Table and data row load/save "

		public static void saveProject(DataRow dr, ref Project mo) {
			if (mo == null) {
				mo = new Project();
			}

			foreach (DataColumn dc in dr.Table.Columns) {
				mo.setAttribute(dc.ColumnName, dr[dc.ColumnName]);
			}

			saveProject(mo);

		}

		public static void saveProject(DataTable dt, ref Project mo) {
			foreach (DataRow dr in dt.Rows) {
				saveProject(dr, ref mo);
			}

		}

		public static Project loadFromDataRow(DataRow r) {

			DataRowLoader a = new DataRowLoader();
			IModelObject mo = new Project();
			a.DataSource = r;
			a.load(mo);
			return (Project)mo;

		}

		#endregion

	}

}


