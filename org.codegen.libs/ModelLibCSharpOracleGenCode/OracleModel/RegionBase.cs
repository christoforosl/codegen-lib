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
// Instead, change code in the extender class Region
//
//************************************************************
//</comments>
namespace OracleModel
{

	#region "Interface"
[System.Runtime.InteropServices.ComVisible(false)] 
	public interface IRegion: IModelObject {
	System.Int64 PrRegionId {get;set;} 
	System.String PrRegionName {get;set;} 
}
#endregion

	
	[DefaultMapperAttr(typeof(OracleMappers.RegionDBMapper)), ComVisible(false), Serializable(), System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
	public class RegionBase : ModelObject, IEquatable<RegionBase>, IRegion {

		#region "Constructor"

		public RegionBase() {
			this.addValidator(new RegionRequiredFieldsValidator());
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

					public const String STR_FLD_REGION_ID = "RegionId";
			public const String STR_FLD_REGION_NAME = "RegionName";


				public const int FLD_REGION_ID = 0;
		public const int FLD_REGION_NAME = 1;



		///<summary> Returns the names of fields in the object as a string array.
		/// Useful in automatically setting/getting values from UI objects (windows or web Form)</summary>
		/// <returns> string array </returns>	 
		public override string[] getFieldList()
		{
			return new string[] {
				STR_FLD_REGION_ID,STR_FLD_REGION_NAME
			};
		}

		#endregion

		#region "Field Declarations"

	private System.Int64 _RegionId;
	private System.String _RegionName;

		#endregion

		#region "Field Properties"

	public virtual System.Int64 PrRegionId{
	get{
		return _RegionId;
	}
	set {
		if (ModelObject.valueChanged(_RegionId, value)){
			if (!this.IsObjectLoading) {
				this.isDirty = true;
				this.setFieldChanged(STR_FLD_REGION_ID);
			}
				this._RegionId=value;

			this.raiseBroadcastIdChange();

		}
		}
	}
public void setRegionId(String val){
	if (Information.IsNumeric(val)) {
		this.PrRegionId = Convert.ToInt64(val);
	} else if (String.IsNullOrEmpty(val)) {
		throw new ApplicationException("Cant update Primary Key to Null");
	} else {
		throw new ApplicationException("Invalid Integer Number, field:RegionId, value:" + val);
	}
}
	public virtual System.String PrRegionName{
	get{
		return _RegionName;
	}
	set {
		if (ModelObject.valueChanged(_RegionName, value)){
		if (value != null && value.Length > 25){
			throw new ModelObjectFieldTooLongException("REGION_NAME");
		}
			if (!this.IsObjectLoading) {
				this.isDirty = true;
				this.setFieldChanged(STR_FLD_REGION_NAME);
			}
				this._RegionName=value;

		}
		}
	}
public void setRegionName( String val ) {
	if (! string.IsNullOrEmpty(val)) {
		this.PrRegionName = val;
	} else {
		this.PrRegionName = null;
	}
}

		#endregion

		#region "Getters/Setters of values by field index/name"
		public override object getAttribute(int fieldKey){

		switch (fieldKey) {
		case FLD_REGION_ID:
			return this.PrRegionId;
		case FLD_REGION_NAME:
			return this.PrRegionName;
		default:
			return null;
		} //end switch

		}

		public override object getAttribute(string fieldKey) {
			fieldKey = fieldKey.ToLower();

		if (fieldKey==STR_FLD_REGION_ID.ToLower() ) {
			return this.PrRegionId;
		} else if (fieldKey==STR_FLD_REGION_NAME.ToLower() ) {
			return this.PrRegionName;
		} else {
			return null;
		}
		}

		public override void setAttribute(int fieldKey, object val){
		switch (fieldKey) {
		case FLD_REGION_ID:
			if (val == DBNull.Value || val == null ){
				throw new ApplicationException("Can't set Primary Key to null");
			} else {
				this.PrRegionId=(System.Int64)val;
			} //
			return;
		case FLD_REGION_NAME:
			if (val == DBNull.Value || val == null ){
				this.PrRegionName = null;
			} else {
				this.PrRegionName=(System.String)val;
			} //
			return;
		default:
			return;
		}

		}

		public override void setAttribute(string fieldKey, object val) {
			fieldKey = fieldKey.ToLower();
		if ( fieldKey==STR_FLD_REGION_ID.ToLower()){
			if (val == DBNull.Value || val ==null ){
				throw new ApplicationException("Can't set Primary Key to null");
			} else {
				this.PrRegionId=(System.Int64)val;
			}
			return;
		} else if ( fieldKey==STR_FLD_REGION_NAME.ToLower()){
			if (val == DBNull.Value || val ==null ){
				this.PrRegionName = null;
			} else {
				this.PrRegionName=(System.String)val;
			}
			return;
		}
		}

		#endregion
		#region "Overrides of GetHashCode and Equals "
		public bool Equals(RegionBase other)
		{

			//typesafe equals, checks for equality of fields
			if (other == null)
				return false;
			if (object.ReferenceEquals(other, this))
				return true;

			return this.PrRegionId == other.PrRegionId
				&& this.PrRegionName == other.PrRegionName;;

		}

		public override int GetHashCode()
		{
			//using Xor has the advantage of not overflowing the integer.
			return this.PrRegionId.GetHashCode()
				 ^ this.getStringHashCode(this.PrRegionName);;

		}

		public override bool Equals(object Obj) {

			if (Obj != null && Obj is RegionBase) {

				return this.Equals((RegionBase)Obj);

			} else {
				return false;
			}

		}

		public static bool operator ==(RegionBase obj1, RegionBase obj2)
		{
			return object.Equals(obj1, obj2);
		}

		public static bool operator !=(RegionBase obj1, RegionBase obj2)
		{
			return !(obj1 == obj2);
		}

		#endregion

		#region "Copy and sort"

		public override IModelObject copy()
		{
			//creates a copy

			//NOTE: we can't cast from RegionBase to Region, so below we 
			//instantiate a Region, NOT a RegionBase object
			Region ret = RegionFactory.Create();

		ret.PrRegionId = this.PrRegionId;
		ret.PrRegionName = this.PrRegionName;



			return ret;

		}

		#endregion




		#region "ID Property"

		public override object Id {
			get { return this._RegionId; }
			set {
				this._RegionId = Convert.ToInt64(value);
				this.raiseBroadcastIdChange();
			}
		}
		#endregion

		#region "Extra Code"

		#endregion

	}

	#region "Req Fields validator"
	[System.Runtime.InteropServices.ComVisible(false)]
	public class RegionRequiredFieldsValidator : IModelObjectValidator
	{


		public void validate(org.model.lib.Model.IModelObject imo) {
			Region mo = (Region)imo;

		}

	}
	#endregion

}

