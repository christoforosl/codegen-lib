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
// Instead, change code in the extender class EmployeeTrainingHistory
//
//************************************************************
//</comments>
namespace OracleModel
{

	#region "Interface"
[System.Runtime.InteropServices.ComVisible(false)] 
	public interface IEmployeeTrainingHistory: IModelObject {
	System.Int64 PrEmployeeTrainingHistoryId {get;set;} 
	System.Int64? PrEmployeeId {get;set;} 
	System.DateTime? PrDateFrom {get;set;} 
	System.DateTime? PrDateTo {get;set;} 
	System.String PrTrainingCourseCode {get;set;} 
	OracleModel.TrainingCourse PrTrainingCourse {get;set;} //association
}
#endregion

	
	[DefaultMapperAttr(typeof(OracleMappers.EmployeeTrainingHistoryDBMapper)), ComVisible(false), Serializable(), System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
	public class EmployeeTrainingHistoryBase : ModelObject, IEquatable<EmployeeTrainingHistoryBase>, IEmployeeTrainingHistory {

		#region "Constructor"

		public EmployeeTrainingHistoryBase() {
			this.addValidator(new EmployeeTrainingHistoryRequiredFieldsValidator());
		}

		#endregion

		#region "Children and Parents"
		
		public override void loadObjectHierarchy() {
		loadTrainingCourse();

		}

		/// <summary>
		/// Returns the **loaded** children of this model object.
		/// Any records that are not loaded (ie the getter method was not called) are not returned.
		/// To get all child records tied to this object, call loadObjectHierarchy() method
		/// </summary>
		public override List<ModelObject> getChildren() {
			List<ModelObject> ret = new List<ModelObject>();
			
			return ret;
		}

		/// <summary>
		/// Returns the **loaded** parent objects of this model object.
		/// Any records are not loaded (ie the getter method was not called) are not returned.
		/// To get all parent records tied to this object, call loadObjectHierarchy() method
		/// </summary>
		public override List<ModelObject> getParents() {
			List<ModelObject> ret = new List<ModelObject>();
			if  ( this._TrainingCourse!=null && this.TrainingCourseLoaded) {
	ret.Add(this.PrTrainingCourse);
}

			return ret;
		}

		#endregion
		#region "Field CONSTANTS"

					public const String STR_FLD_EMPLOYEE_TRAINING_HISTORY_ID = "EmployeeTrainingHistoryId";
			public const String STR_FLD_EMPLOYEE_ID = "EmployeeId";
			public const String STR_FLD_DATE_FROM = "DateFrom";
			public const String STR_FLD_DATE_TO = "DateTo";
			public const String STR_FLD_TRAINING_COURSE_CODE = "TrainingCourseCode";


				public const int FLD_EMPLOYEE_TRAINING_HISTORY_ID = 0;
		public const int FLD_EMPLOYEE_ID = 1;
		public const int FLD_DATE_FROM = 2;
		public const int FLD_DATE_TO = 3;
		public const int FLD_TRAINING_COURSE_CODE = 4;



		///<summary> Returns the names of fields in the object as a string array.
		/// Useful in automatically setting/getting values from UI objects (windows or web Form)</summary>
		/// <returns> string array </returns>	 
		public override string[] getFieldList()
		{
			return new string[] {
				STR_FLD_EMPLOYEE_TRAINING_HISTORY_ID,STR_FLD_EMPLOYEE_ID,STR_FLD_DATE_FROM,STR_FLD_DATE_TO,STR_FLD_TRAINING_COURSE_CODE
			};
		}

		#endregion

		#region "Field Declarations"

	private System.Int64 _EmployeeTrainingHistoryId;
	private System.Int64? _EmployeeId = null;
	private System.DateTime? _DateFrom = null;
	private System.DateTime? _DateTo = null;
	private System.String _TrainingCourseCode;
	// ****** CHILD OBJECTS ********************
	private OracleModel.TrainingCourse _TrainingCourse = null;  // initialize to nothing, for lazy load logic below !!!

	// *****************************************
	// ****** END CHILD OBJECTS ********************

		#endregion

		#region "Field Properties"

	public virtual System.Int64 PrEmployeeTrainingHistoryId{
	get{
		return _EmployeeTrainingHistoryId;
	}
	set {
		if (ModelObject.valueChanged(_EmployeeTrainingHistoryId, value)){
			if (!this.IsObjectLoading) {
				this.isDirty = true;
				this.setFieldChanged(STR_FLD_EMPLOYEE_TRAINING_HISTORY_ID);
			}
				this._EmployeeTrainingHistoryId=value;

			this.raiseBroadcastIdChange();

		}
		}
	}
public void setEmployeeTrainingHistoryId(String val){
	if (Information.IsNumeric(val)) {
		this.PrEmployeeTrainingHistoryId = Convert.ToInt64(val);
	} else if (String.IsNullOrEmpty(val)) {
		throw new ApplicationException("Cant update Primary Key to Null");
	} else {
		throw new ApplicationException("Invalid Integer Number, field:EmployeeTrainingHistoryId, value:" + val);
	}
}
	public virtual System.Int64? PrEmployeeId{
	get{
		return _EmployeeId;
	}
	set {
		if (ModelObject.valueChanged(_EmployeeId, value)){
			if (!this.IsObjectLoading) {
				this.isDirty = true;
				this.setFieldChanged(STR_FLD_EMPLOYEE_ID);
			}
				this._EmployeeId=value;

		}
		}
	}
public void setEmployeeId(String val){
	if (Information.IsNumeric(val)) {
		this.PrEmployeeId = Convert.ToInt64(val);
	} else if (String.IsNullOrEmpty(val)) {
		this.PrEmployeeId = null;
	} else {
		throw new ApplicationException("Invalid Integer Number, field:EmployeeId, value:" + val);
	}
}
	public virtual System.DateTime? PrDateFrom{
	get{
		return _DateFrom;
	}
	set {
		if (ModelObject.valueChanged(_DateFrom, value)){
			if (!this.IsObjectLoading) {
				this.isDirty = true;
				this.setFieldChanged(STR_FLD_DATE_FROM);
			}
				this._DateFrom=value;

		}
		}
	}
public void setDateFrom( String val ){
	if (Information.IsDate(val)) {
		this.PrDateFrom = Convert.ToDateTime(val);
	} else if (String.IsNullOrEmpty(val) ) {
		this.PrDateFrom = null;
	} else {
		throw new ApplicationException("Invalid Date, field:DateFrom, value:" + val);
	}
}
	public virtual System.DateTime? PrDateTo{
	get{
		return _DateTo;
	}
	set {
		if (ModelObject.valueChanged(_DateTo, value)){
			if (!this.IsObjectLoading) {
				this.isDirty = true;
				this.setFieldChanged(STR_FLD_DATE_TO);
			}
				this._DateTo=value;

		}
		}
	}
public void setDateTo( String val ){
	if (Information.IsDate(val)) {
		this.PrDateTo = Convert.ToDateTime(val);
	} else if (String.IsNullOrEmpty(val) ) {
		this.PrDateTo = null;
	} else {
		throw new ApplicationException("Invalid Date, field:DateTo, value:" + val);
	}
}
	public virtual System.String PrTrainingCourseCode{
	get{
		return _TrainingCourseCode;
	}
	set {
		if (ModelObject.valueChanged(_TrainingCourseCode, value)){
		if (value != null && value.Length > 5){
			throw new ModelObjectFieldTooLongException("TRAINING_COURSE_CODE");
		}
			if (!this.IsObjectLoading) {
				this.isDirty = true;
				this.setFieldChanged(STR_FLD_TRAINING_COURSE_CODE);
			}
				this._TrainingCourseCode=value;

		}
		}
	}
public void setTrainingCourseCode( String val ) {
	if (! string.IsNullOrEmpty(val)) {
		this.PrTrainingCourseCode = val;
	} else {
		this.PrTrainingCourseCode = null;
	}
}

		// ASSOCIATIONS GETTERS/SETTERS BELOW!
		//associationParentCSharp.txt
		#region "Association TrainingCourse"

		private bool TrainingCourseLoaded {get;set;}

		/// <summary>
        /// Gets/Sets parent object
        /// </summary>
		public virtual OracleModel.TrainingCourse PrTrainingCourse {
		    //1-1 parent association
            set {
                this._TrainingCourse = value;
				if ( value != null ) {
					this.PrTrainingCourseCode = value.PrCODE;
					//AddHandler value.IDChanged, AddressOf this.handleParentIdChanged;
					value.IDChanged += this.handleParentIdChanged;
                } else {
					this.PrTrainingCourseCode = null;
				}

            }


            get {
                //LAZY LOADING! Only hit the database to get the child object if we need it
                if ( this._TrainingCourse == null ) {
					this.loadTrainingCourse();
                }
				
                return this._TrainingCourse;
            }
        }
        
        /// <summary>
        /// Loads parent object and sets the appropriate properties
        /// </summary>
        private void loadTrainingCourse() {
			
			if (this.TrainingCourseLoaded) return;
			
			if ( this._TrainingCourse == null && this.PrTrainingCourseCode != null ) {
                
				//call the setter here, not the private variable!
                this.PrTrainingCourse = new OracleMappers.TrainingCourseDBMapper().findByKey(this.PrTrainingCourseCode);
                
            }

            this.TrainingCourseLoaded=true;
			            
       }
		#endregion


		#endregion

		#region "Getters/Setters of values by field index/name"
		public override object getAttribute(int fieldKey){

		switch (fieldKey) {
		case FLD_EMPLOYEE_TRAINING_HISTORY_ID:
			return this.PrEmployeeTrainingHistoryId;
		case FLD_EMPLOYEE_ID:
			return this.PrEmployeeId;
		case FLD_DATE_FROM:
			return this.PrDateFrom;
		case FLD_DATE_TO:
			return this.PrDateTo;
		case FLD_TRAINING_COURSE_CODE:
			return this.PrTrainingCourseCode;
		default:
			return null;
		} //end switch

		}

		public override object getAttribute(string fieldKey) {
			fieldKey = fieldKey.ToLower();

		if (fieldKey==STR_FLD_EMPLOYEE_TRAINING_HISTORY_ID.ToLower() ) {
			return this.PrEmployeeTrainingHistoryId;
		} else if (fieldKey==STR_FLD_EMPLOYEE_ID.ToLower() ) {
			return this.PrEmployeeId;
		} else if (fieldKey==STR_FLD_DATE_FROM.ToLower() ) {
			return this.PrDateFrom;
		} else if (fieldKey==STR_FLD_DATE_TO.ToLower() ) {
			return this.PrDateTo;
		} else if (fieldKey==STR_FLD_TRAINING_COURSE_CODE.ToLower() ) {
			return this.PrTrainingCourseCode;
		} else {
			return null;
		}
		}

		public override void setAttribute(int fieldKey, object val){
		switch (fieldKey) {
		case FLD_EMPLOYEE_TRAINING_HISTORY_ID:
			if (val == DBNull.Value || val == null ){
				throw new ApplicationException("Can't set Primary Key to null");
			} else {
				this.PrEmployeeTrainingHistoryId=(System.Int64)val;
			} //
			return;
		case FLD_EMPLOYEE_ID:
			if (val == DBNull.Value || val == null ){
				this.PrEmployeeId = null;
			} else {
				this.PrEmployeeId=(System.Int64)val;
			} //
			return;
		case FLD_DATE_FROM:
			if (val == DBNull.Value || val == null ){
				this.PrDateFrom = null;
			} else {
				this.PrDateFrom=(System.DateTime)val;
			} //
			return;
		case FLD_DATE_TO:
			if (val == DBNull.Value || val == null ){
				this.PrDateTo = null;
			} else {
				this.PrDateTo=(System.DateTime)val;
			} //
			return;
		case FLD_TRAINING_COURSE_CODE:
			if (val == DBNull.Value || val == null ){
				this.PrTrainingCourseCode = null;
			} else {
				this.PrTrainingCourseCode=(System.String)val;
			} //
			return;
		default:
			return;
		}

		}

		public override void setAttribute(string fieldKey, object val) {
			fieldKey = fieldKey.ToLower();
		if ( fieldKey==STR_FLD_EMPLOYEE_TRAINING_HISTORY_ID.ToLower()){
			if (val == DBNull.Value || val ==null ){
				throw new ApplicationException("Can't set Primary Key to null");
			} else {
				this.PrEmployeeTrainingHistoryId=(System.Int64)val;
			}
			return;
		} else if ( fieldKey==STR_FLD_EMPLOYEE_ID.ToLower()){
			if (val == DBNull.Value || val ==null ){
				this.PrEmployeeId = null;
			} else {
				this.PrEmployeeId=(System.Int64)val;
			}
			return;
		} else if ( fieldKey==STR_FLD_DATE_FROM.ToLower()){
			if (val == DBNull.Value || val ==null ){
				this.PrDateFrom = null;
			} else {
				this.PrDateFrom=(System.DateTime)val;
			}
			return;
		} else if ( fieldKey==STR_FLD_DATE_TO.ToLower()){
			if (val == DBNull.Value || val ==null ){
				this.PrDateTo = null;
			} else {
				this.PrDateTo=(System.DateTime)val;
			}
			return;
		} else if ( fieldKey==STR_FLD_TRAINING_COURSE_CODE.ToLower()){
			if (val == DBNull.Value || val ==null ){
				this.PrTrainingCourseCode = null;
			} else {
				this.PrTrainingCourseCode=(System.String)val;
			}
			return;
		}
		}

		#endregion
		#region "Overrides of GetHashCode and Equals "
		public bool Equals(EmployeeTrainingHistoryBase other)
		{

			//typesafe equals, checks for equality of fields
			if (other == null)
				return false;
			if (object.ReferenceEquals(other, this))
				return true;

			return this.PrEmployeeTrainingHistoryId == other.PrEmployeeTrainingHistoryId
				&& this.PrEmployeeId.GetValueOrDefault() == other.PrEmployeeId.GetValueOrDefault()
				&& this.PrDateFrom.GetValueOrDefault() == other.PrDateFrom.GetValueOrDefault()
				&& this.PrDateTo.GetValueOrDefault() == other.PrDateTo.GetValueOrDefault()
				&& this.PrTrainingCourseCode == other.PrTrainingCourseCode;;

		}

		public override int GetHashCode()
		{
			//using Xor has the advantage of not overflowing the integer.
			return this.PrEmployeeTrainingHistoryId.GetHashCode()
				 ^ this.PrEmployeeId.GetHashCode()
				 ^ this.PrDateFrom.GetHashCode()
				 ^ this.PrDateTo.GetHashCode()
				 ^ this.getStringHashCode(this.PrTrainingCourseCode);;

		}

		public override bool Equals(object Obj) {

			if (Obj != null && Obj is EmployeeTrainingHistoryBase) {

				return this.Equals((EmployeeTrainingHistoryBase)Obj);

			} else {
				return false;
			}

		}

		public static bool operator ==(EmployeeTrainingHistoryBase obj1, EmployeeTrainingHistoryBase obj2)
		{
			return object.Equals(obj1, obj2);
		}

		public static bool operator !=(EmployeeTrainingHistoryBase obj1, EmployeeTrainingHistoryBase obj2)
		{
			return !(obj1 == obj2);
		}

		#endregion

		#region "Copy and sort"

		public override IModelObject copy()
		{
			//creates a copy

			//NOTE: we can't cast from EmployeeTrainingHistoryBase to EmployeeTrainingHistory, so below we 
			//instantiate a EmployeeTrainingHistory, NOT a EmployeeTrainingHistoryBase object
			EmployeeTrainingHistory ret = EmployeeTrainingHistoryFactory.Create();

		ret.PrEmployeeTrainingHistoryId = this.PrEmployeeTrainingHistoryId;
		ret.PrEmployeeId = this.PrEmployeeId;
		ret.PrDateFrom = this.PrDateFrom;
		ret.PrDateTo = this.PrDateTo;
		ret.PrTrainingCourseCode = this.PrTrainingCourseCode;



			return ret;

		}

		#endregion

#region "parentIdChanged"
	//below sub is called when parentIdChanged
	public override void handleParentIdChanged(IModelObject parentMo ){
		// Assocations from OracleModel.TrainingCourse
		if ( parentMo is OracleModel.TrainingCourse) {
			this.PrTrainingCourseCode= ((OracleModel.TrainingCourse)parentMo).PrCODE;
		}
		// Assocations from OracleModel.Employee
		if ( parentMo is OracleModel.Employee) {
			this.PrEmployeeId= ((OracleModel.Employee)parentMo).PrEmployeeId;
		}
		// Assocations from OracleModel.Employee
		if ( parentMo is OracleModel.Employee) {
			this.PrEmployeeId= ((OracleModel.Employee)parentMo).PrEmployeeId;
		}
	}
#endregion



		#region "ID Property"

		public override object Id {
			get { return this._EmployeeTrainingHistoryId; }
			set {
				this._EmployeeTrainingHistoryId = Convert.ToInt64(value);
				this.raiseBroadcastIdChange();
			}
		}
		#endregion

		#region "Extra Code"

		#endregion

	}

	#region "Req Fields validator"
	[System.Runtime.InteropServices.ComVisible(false)]
	public class EmployeeTrainingHistoryRequiredFieldsValidator : IModelObjectValidator
	{


		public void validate(org.model.lib.Model.IModelObject imo) {
			EmployeeTrainingHistory mo = (EmployeeTrainingHistory)imo;
if (mo.PrEmployeeId == null ) {
		throw new ModelObjectRequiredFieldException("EmployeeId");
}

		}

	}
	#endregion

}

