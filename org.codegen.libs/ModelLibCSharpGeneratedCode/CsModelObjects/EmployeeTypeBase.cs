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
// Instead, change code in the extender class EmployeeType
//
//************************************************************
//</comments>
namespace CsModelObjects
{

	#region "Interface"
[System.Runtime.InteropServices.ComVisible(false)] 
	public interface IEmployeeType: IModelObject {
	System.String PrEmployeeTypeCode {get;set;} 
	System.String PrEmployeeType {get;set;} 
}
#endregion

	
	[DefaultMapperAttr(typeof(CsModelMappers.EmployeeTypeDBMapper)), ComVisible(false), Serializable(), System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
	public class EmployeeTypeBase : ModelObject, IEquatable<EmployeeTypeBase>, IEmployeeType {

		#region "Constructor"

		public EmployeeTypeBase() {
			this.addValidator(new EmployeeTypeRequiredFieldsValidator());
		}

		#endregion

		#region "Children and Parents"
		
		public override void loadObjectHierarchy() {

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
			
			return ret;
		}

		#endregion
		#region "Field CONSTANTS"

					public const String STR_FLD_EMPLOYEETYPECODE = "EmployeeTypeCode";
			public const String STR_FLD_EMPLOYEETYPE = "EmployeeType";


				public const int FLD_EMPLOYEETYPECODE = 0;
		public const int FLD_EMPLOYEETYPE = 1;



		///<summary> Returns the names of fields in the object as a string array.
		/// Useful in automatically setting/getting values from UI objects (windows or web Form)</summary>
		/// <returns> string array </returns>	 
		public override string[] getFieldList()
		{
			return new string[] {
				STR_FLD_EMPLOYEETYPECODE,STR_FLD_EMPLOYEETYPE
			};
		}

		#endregion

		#region "Field Declarations"

	private System.String _EmployeeTypeCode;
	private System.String _EmployeeType;

		#endregion

		#region "Field Properties"

	public virtual System.String PrEmployeeTypeCode{
	get{
		return _EmployeeTypeCode;
	}
	set {
		if (ModelObject.valueChanged(_EmployeeTypeCode, value)){
		if (value != null && value.Length > 10){
			throw new ModelObjectFieldTooLongException("EmployeeTypeCode");
		}
			if (this.IsObjectLoading == false) {
				this.isDirty = true;
				this.setFieldChanged(STR_FLD_EMPLOYEETYPECODE);
				this._EmployeeTypeCode=value;
			}

			this.raiseBroadcastIdChange();

		}
		}
	}
public void setEmployeeTypeCode( String val ) {
	if (! string.IsNullOrEmpty(val)) {
		this.PrEmployeeTypeCode = val;
	} else {
		this.PrEmployeeTypeCode = null;
	}
}
	public virtual System.String PrEmployeeType{
	get{
		return _EmployeeType;
	}
	set {
		if (ModelObject.valueChanged(_EmployeeType, value)){
		if (value != null && value.Length > 50){
			throw new ModelObjectFieldTooLongException("EmployeeType");
		}
			if (this.IsObjectLoading == false) {
				this.isDirty = true;
				this.setFieldChanged(STR_FLD_EMPLOYEETYPE);
				this._EmployeeType=value;
			}

		}
		}
	}
public void setEmployeeType( String val ) {
	if (! string.IsNullOrEmpty(val)) {
		this.PrEmployeeType = val;
	} else {
		this.PrEmployeeType = null;
	}
}

		#endregion

		#region "Getters/Setters of values by field index/name"
		public override object getAttribute(int fieldKey){

		switch (fieldKey) {
		case FLD_EMPLOYEETYPECODE:
			return this.PrEmployeeTypeCode;
		case FLD_EMPLOYEETYPE:
			return this.PrEmployeeType;
		default:
			return null;
		} //end switch

		}

		public override object getAttribute(string fieldKey) {
			fieldKey = fieldKey.ToLower();

		if (fieldKey==STR_FLD_EMPLOYEETYPECODE.ToLower() ) {
			return this.PrEmployeeTypeCode;
		} else if (fieldKey==STR_FLD_EMPLOYEETYPE.ToLower() ) {
			return this.PrEmployeeType;
		} else {
			return null;
		}
		}

		public override void setAttribute(int fieldKey, object val){
		switch (fieldKey) {
		case FLD_EMPLOYEETYPECODE:
			if (val == DBNull.Value || val == null ){
				throw new ApplicationException("Can't set Primary Key to null");
			} else {
				this.PrEmployeeTypeCode=(System.String)val;
			} //
			return;
		case FLD_EMPLOYEETYPE:
			if (val == DBNull.Value || val == null ){
				this.PrEmployeeType = null;
			} else {
				this.PrEmployeeType=(System.String)val;
			} //
			return;
		default:
			return;
		}

		}

		public override void setAttribute(string fieldKey, object val) {
			fieldKey = fieldKey.ToLower();
		if ( fieldKey==STR_FLD_EMPLOYEETYPECODE.ToLower()){
			if (val == DBNull.Value || val ==null ){
				throw new ApplicationException("Can't set Primary Key to null");
			} else {
				this.PrEmployeeTypeCode=(System.String)val;
			}
			return;
		} else if ( fieldKey==STR_FLD_EMPLOYEETYPE.ToLower()){
			if (val == DBNull.Value || val ==null ){
				this.PrEmployeeType = null;
			} else {
				this.PrEmployeeType=(System.String)val;
			}
			return;
		}
		}

		#endregion
		#region "Overrides of GetHashCode and Equals "
		public bool Equals(EmployeeTypeBase other)
		{

			//typesafe equals, checks for equality of fields
			if (other == null)
				return false;
			if (object.ReferenceEquals(other, this))
				return true;

			return this.PrEmployeeTypeCode == other.PrEmployeeTypeCode
				&& this.PrEmployeeType == other.PrEmployeeType;;

		}

		public override int GetHashCode()
		{
			//using Xor has the advantage of not overflowing the integer.
			return this.getStringHashCode(this.PrEmployeeTypeCode)
				 ^ this.getStringHashCode(this.PrEmployeeType);;

		}

		public override bool Equals(object Obj) {

			if (Obj != null && Obj is EmployeeTypeBase) {

				return this.Equals((EmployeeTypeBase)Obj);

			} else {
				return false;
			}

		}

		public static bool operator ==(EmployeeTypeBase obj1, EmployeeTypeBase obj2)
		{
			return object.Equals(obj1, obj2);
		}

		public static bool operator !=(EmployeeTypeBase obj1, EmployeeTypeBase obj2)
		{
			return !(obj1 == obj2);
		}

		#endregion

		#region "Copy and sort"

		public override IModelObject copy()
		{
			//creates a copy

			//NOTE: we can't cast from EmployeeTypeBase to EmployeeType, so below we 
			//instantiate a EmployeeType, NOT a EmployeeTypeBase object
			EmployeeType ret = EmployeeTypeFactory.Create();

		ret.PrEmployeeTypeCode = this.PrEmployeeTypeCode;
		ret.PrEmployeeType = this.PrEmployeeType;



			return ret;

		}

		#endregion




		#region "ID Property"

		public override object Id {
			get { return this._EmployeeTypeCode; }
			set {
				this._EmployeeTypeCode = Convert.ToString(value);
				this.raiseBroadcastIdChange();
			}
		}
		#endregion

		#region "Extra Code"

		#endregion

	}

	#region "Req Fields validator"
	[System.Runtime.InteropServices.ComVisible(false)]
	public class EmployeeTypeRequiredFieldsValidator : IModelObjectValidator
	{


		public void validate(org.model.lib.Model.IModelObject imo) {
			EmployeeType mo = (EmployeeType)imo;
if (string.IsNullOrEmpty( mo.PrEmployeeType)) {
		throw new ModelObjectRequiredFieldException("EmployeeType");
}

		}

	}
	#endregion

}


