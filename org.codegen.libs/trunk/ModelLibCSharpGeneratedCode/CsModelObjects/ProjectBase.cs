﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

using org.model.lib.Model;
using org.model.lib;

using Microsoft.VisualBasic;
using System.Runtime.InteropServices;
using System.Xml.Serialization;

//<comments>
//************************************************************
// Template: ModelBase2.csharp.txt
// Class autogenerated on 09/06/2013 8:02:57 AM by ModelGenerator
// Extends base model object class
// *** DO NOT change code in this class.  
//     It will be re-generated and 
//     overwritten by the code generator ****
// Instead, change code in the extender class Project
//
//************************************************************
//</comments>
namespace CsModelObjects
{

	#region "Interface"
[System.Runtime.InteropServices.ComVisible(false)] 
	public interface IProject: IModelObject {
	System.Int64 PrProjectId {get;set;} 
	System.String PrProjectName {get;set;} 
	System.Boolean? PrIsActive {get;set;} 
	IEnumerable< CsModelObjects.EmployeeProject>PrEmployeeProjects {get; set;}
		void EmployeeProjectAdd(CsModelObjects.EmployeeProject val);
		void EmployeeProjectRemove(CsModelObjects.EmployeeProject val);
		IEnumerable<CsModelObjects.EmployeeProject>EmployeeProjectsGetDeleted();
		CsModelObjects.EmployeeProject EmployeeProjectGetAt( int i ) ;

}
#endregion

	
	[DefaultMapperAttr(typeof(CsModelMappers.ProjectDBMapper)), ComVisible(false), Serializable(), System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
	public class ProjectBase : ModelObject, IEquatable<ProjectBase>, IProject {

		#region "Constructor"

		public ProjectBase() {
			this.addValidator(new ProjectRequiredFieldsValidator());
		}

		#endregion

		#region "Children and Parents"
		
		public override void loadObjectHierarchy() {
		loadEmployeeProjects();

		}

		/// <summary>
		/// Returns the **loaded** children of this model object.
		/// Any records that are not loaded (ie the getter method was not called) are not returned.
		/// To get all child records tied to this object, call loadObjectHierarchy() method
		/// </summary>
		public override List<ModelObject> getChildren() {
			List<ModelObject> ret = new List<ModelObject>();
				if  (this.EmployeeProjectsLoaded) { // check if loaded first!
		List< ModelObject > lp = this._EmployeeProjects.ConvertAll(
				new Converter< CsModelObjects.EmployeeProject, ModelObject>((
			CsModelObjects.EmployeeProject pf )=> {				return (ModelObject)pf;}));
		ret.AddRange(lp);
	}

			return ret;
		}

		/// <summary>
		/// Returns the **loaded** parent objects of this model object.
		/// Any records are not loaded (ie the getter method was not called) are not returned.
		/// To get all parent records tied to this object, call loadObjectHierarchy() method
		/// </summary>
		public override List<ModelObject> getParents() {
			List<ModelObject> ret = new List<ModelObject>();
			
			return ret;
		}

		#endregion
		#region "Field CONSTANTS"

					public const String STR_FLD_PROJECTID = "ProjectId";
			public const String STR_FLD_PROJECTNAME = "ProjectName";
			public const String STR_FLD_ISACTIVE = "IsActive";


				public const int FLD_PROJECTID = 0;
		public const int FLD_PROJECTNAME = 1;
		public const int FLD_ISACTIVE = 2;



		///<summary> Returns the names of fields in the object as a string array.
		/// Useful in automatically setting/getting values from UI objects (windows or web Form)</summary>
		/// <returns> string array </returns>	 
		public override string[] getFieldList()
		{
			return new string[] {
				STR_FLD_PROJECTID,STR_FLD_PROJECTNAME,STR_FLD_ISACTIVE
			};
		}

		#endregion

		#region "Field Declarations"

	private System.Int64 _ProjectId;
	private System.String _ProjectName = null;
	private System.Int64? _IsActive = null;
	// ****** CHILD OBJECTS ********************
	private List< CsModelObjects.EmployeeProject> _EmployeeProjects = null;  // initialize to nothing, for lazy load logic below !!!
	 private List< CsModelObjects.EmployeeProject> _deletedEmployeeProjects = new List< CsModelObjects.EmployeeProject>();// initialize to empty list !!!

	// *****************************************
	// ****** END CHILD OBJECTS ********************

		#endregion

		#region "Field Properties"

	public virtual System.Int64 PrProjectId  {
	get {
		return _ProjectId;
	} 
	set {
		if (ModelObject.valueChanged(_ProjectId, value)) {
			if (!this.IsObjectLoading ) {
				this.isDirty = true;
				this.setFieldChanged(STR_FLD_PROJECTID);
			}
			this._ProjectId = value;

			this.raiseBroadcastIdChange();

		}
	}  
	}
public void setProjectId(String val){
	if (Information.IsNumeric(val)) {
		this.PrProjectId = Convert.ToInt32(val);
	} else if (String.IsNullOrEmpty(val)) {
		throw new ApplicationException("Cant update Primary Key to Null");
	} else {
		throw new ApplicationException("Invalid Integer Number, field:ProjectId, value:" + val);
	}
}
	public virtual System.String PrProjectName  {
	get {
		return _ProjectName;
	} 
	set {
		if (ModelObject.valueChanged(_ProjectName, value)) {
			if (!this.IsObjectLoading ) {
				this.isDirty = true;
				this.setFieldChanged(STR_FLD_PROJECTNAME);
			}
			this._ProjectName = value;

		}
	}  
	}
public void setProjectName( String val ) {
	if (! string.IsNullOrEmpty(val)) {
		this.PrProjectName = val;
	} else {
		this.PrProjectName = null;
	}
}
	public virtual System.Boolean? PrIsActive  {
	get {
		if ( _IsActive.HasValue ) {
			return _IsActive.GetValueOrDefault()==1;
		} else {
			return false;
		} //end customized check
	} 
	set {
		if (ModelObject.valueChanged(_IsActive, value)) {
			if (!this.IsObjectLoading ) {
				this.isDirty = true;
				this.setFieldChanged(STR_FLD_ISACTIVE);
			}
			this._IsActive = value.HasValue && value.Value? 1: 0;

		}
	}  
	}
public void setIsActive(String val ){
	if (String.IsNullOrEmpty(val)) {
		this.PrIsActive = null;
	} else {
	    bool newval = ("1"==val || "true"==val.ToLower()) ;
	    this.PrIsActive = newval;
	}
}

		// ASSOCIATIONS GETTERS/SETTERS BELOW!
		//associationChildManyCSharp.txt
		#region "Association EmployeeProjects"

		public bool EmployeeProjectsLoaded  {get;set;}

		public virtual CsModelObjects.EmployeeProject EmployeeProjectGetAt( int i ) {

            this.loadEmployeeProjects();
            if( this._EmployeeProjects.Count >= (i - 1)) {
                return this._EmployeeProjects[i];
            }
            return null;

        } //End Function        
		
		public virtual void EmployeeProjectAdd( CsModelObjects.EmployeeProject val )  {
			//1-Many , add a single item!
			this.loadEmployeeProjects();
			val.PrEPProjectId = this.PrProjectId;
			//AddHandler this.IDChanged, AddressOf val.handleParentIdChanged;
			this.IDChanged += val.handleParentIdChanged;
			this._EmployeeProjects.Add(val);

        }

		public virtual void EmployeeProjectsClear() {

            this.loadEmployeeProjects();
            this._deletedEmployeeProjects.AddRange(this._EmployeeProjects);
            this._EmployeeProjects.Clear();

        }

		public virtual void EmployeeProjectRemove( CsModelObjects.EmployeeProject val ) {
			
			this.loadEmployeeProjects();
			this._deletedEmployeeProjects.Add(val);
			this._EmployeeProjects.Remove(val);

        }
		
		public virtual IEnumerable< CsModelObjects.EmployeeProject >EmployeeProjectsGetDeleted() {
			
			return this._deletedEmployeeProjects;

        }

        public virtual IEnumerable< CsModelObjects.EmployeeProject > PrEmployeeProjects {

            get {
				//'1 to many relation
                //'LAZY LOADING! Only hit the database to get the child object if we need it
                if ( this._EmployeeProjects == null ) {
                    this.loadEmployeeProjects();
                } 
				
                return this._EmployeeProjects;
            }
            
			set {
				if (value == null ) {
					this._EmployeeProjects = null;
                } else {
                    this._EmployeeProjects = new List< CsModelObjects.EmployeeProject >();
                    this.addToEmployeeProjectsList(value);
                }
			}
        }

		/// <summary>
        /// Private method to add to the EmployeeProjects List. 
		/// The list must have aldready been initialized
        /// </summary>
		private void addToEmployeeProjectsList( IEnumerable< CsModelObjects.EmployeeProject> value ) {

			IEnumerator< CsModelObjects.EmployeeProject> enumtor = value.GetEnumerator();
        
		    while (enumtor.MoveNext()) {
                CsModelObjects.EmployeeProject v = enumtor.Current;
                v.IDChanged += this.handleParentIdChanged;
                this._EmployeeProjects.Add(v);
            }

        } //End Sub
        
        /// <summary>
        /// Loads child objects from dabatabase, if not loaded already
        /// </summary>
        public virtual void loadEmployeeProjects() {
			
			if (this.EmployeeProjectsLoaded)return;
			//init list
			this._EmployeeProjects = new List< CsModelObjects.EmployeeProject>();

			if (! this.isNew ) {
                this.addToEmployeeProjectsList( new CsModelMappers.EmployeeProjectDBMapper().findList("EPProjectId={0}", this.PrProjectId));
            }
            
			this.EmployeeProjectsLoaded = true;
        } 
		#endregion


		#endregion

		#region "Getters/Setters of values by field index/name"
		public override object getAttribute(int fieldKey){

		switch (fieldKey) {
		case FLD_PROJECTID:
			return this.PrProjectId;
		case FLD_PROJECTNAME:
			return this.PrProjectName;
		case FLD_ISACTIVE:
			return this.PrIsActive;
		default:
			return null;
		} //end switch

		}

		public override object getAttribute(string fieldKey) {
			fieldKey = fieldKey.ToLower();

		if (fieldKey==STR_FLD_PROJECTID.ToLower() ) {
			return this.PrProjectId;
		} else if (fieldKey==STR_FLD_PROJECTNAME.ToLower() ) {
			return this.PrProjectName;
		} else if (fieldKey==STR_FLD_ISACTIVE.ToLower() ) {
			return this.PrIsActive;
		} else {
			return null;
		}
		}

		public override void setAttribute(int fieldKey, object val){
		switch (fieldKey) {
		case FLD_PROJECTID:
			if (val == DBNull.Value || val == null ){
				throw new ApplicationException("Can't set Primary Key to null");
			}else{
				this.PrProjectId=(System.Int64)val;
			} //
			return;
		case FLD_PROJECTNAME:
			if (val == DBNull.Value || val == null ){
				this.PrProjectName = null;
			}else{
				this.PrProjectName=(System.String)val;
			} //
			return;
		case FLD_ISACTIVE:
			if (val == DBNull.Value || val == null ){
				this.PrIsActive = null;
			}else{
				this.PrIsActive=(System.Boolean)val;
			} //
			return;
		default:
			return;
		}

		}

		public override void setAttribute(string fieldKey, object val) {
			fieldKey = fieldKey.ToLower();
		if ( fieldKey==STR_FLD_PROJECTID.ToLower()){
			if (val == DBNull.Value || val ==null ){
				throw new ApplicationException("Can't set Primary Key to null");
			} else {
				this.PrProjectId=(System.Int64)val;
			}
			return;
		} else if ( fieldKey==STR_FLD_PROJECTNAME.ToLower()){
			if (val == DBNull.Value || val ==null ){
				this.PrProjectName = null;
			} else {
				this.PrProjectName=(System.String)val;
			}
			return;
		} else if ( fieldKey==STR_FLD_ISACTIVE.ToLower()){
			if (val == DBNull.Value || val ==null ){
				this.PrIsActive = null;
			} else {
				this.PrIsActive=(System.Boolean)val;
			}
			return;
		}
		}

		#endregion
		#region "Overrides of GetHashCode and Equals "
		public bool Equals(ProjectBase other)
		{

			//typesafe equals, checks for equality of fields
			if (other == null)
				return false;
			if (object.ReferenceEquals(other, this))
				return true;

			return this.PrProjectId == other.PrProjectId
				&& this.PrProjectName == other.PrProjectName
				&& this.PrIsActive.GetValueOrDefault() == other.PrIsActive.GetValueOrDefault();;

		}

		public override int GetHashCode()
		{
			//using Xor has the advantage of not overflowing the integer.
			return this.PrProjectId.GetHashCode()
				 ^ this.getStringHashCode(this.PrProjectName)
				 ^ this.PrIsActive.GetHashCode();;

		}

		public override bool Equals(object Obj) {

			if (Obj != null && Obj is ProjectBase) {

				return this.Equals((ProjectBase)Obj);

			} else {
				return false;
			}

		}

		public static bool operator ==(ProjectBase obj1, ProjectBase obj2)
		{
			return object.Equals(obj1, obj2);
		}

		public static bool operator !=(ProjectBase obj1, ProjectBase obj2)
		{
			return !(obj1 == obj2);
		}

		#endregion

		#region "Copy and sort"

		public override IModelObject copy()
		{
			//creates a copy

			//NOTE: we can't cast from ProjectBase to Project, so below we 
			//instantiate a Project, NOT a ProjectBase object
			Project ret = ProjectFactory.Create();

		ret.PrProjectId = this.PrProjectId;
		ret.PrProjectName = this.PrProjectName;
		ret.PrIsActive = this.PrIsActive;



			return ret;

		}

		public override void merge(IModelObject other)
		{
			//merges this Project model object (me) with the "other" instance 

			Project o = (Project)other;

if (! string.IsNullOrEmpty(o.PrProjectName) && 
		 string.IsNullOrEmpty(this.PrProjectName)){
		this.PrProjectName = o.PrProjectName;
}
if ( o.PrIsActive != null && 
		 this.PrIsActive == null){
		this.PrIsActive = o.PrIsActive;
}


		}

		

		#endregion




		#region "ID Property"

		public override object Id {
			get { return this._ProjectId; }
			set {
				this._ProjectId = Convert.ToInt64(value);
				this.raiseBroadcastIdChange();
			}
		}
		#endregion

		#region "Extra Code"

		#endregion

	}

	#region "Req Fields validator"
	[System.Runtime.InteropServices.ComVisible(false)]
	public class ProjectRequiredFieldsValidator : IModelObjectValidator
	{


		public void validate(org.model.lib.Model.IModelObject imo) {
			Project mo = (Project)imo;
if (string.IsNullOrEmpty( mo.PrProjectName)) {
		throw new ModelObjectRequiredFieldException("ProjectName");
}
if (mo.PrIsActive == null ) {
		throw new ModelObjectRequiredFieldException("IsActive");
}

		}

	}
	#endregion

}


