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
// Instead, change code in the extender class Location
//
//************************************************************
//</comments>
namespace OracleModel
{

	#region "Interface"
[System.Runtime.InteropServices.ComVisible(false)] 
	public interface ILocation: IModelObject {
	System.Int64 PrLocationId {get;set;} 
	System.String PrStreetAddress {get;set;} 
	System.String PrPostalCode {get;set;} 
	System.String PrCITY {get;set;} 
	System.String PrStateProvince {get;set;} 
	System.String PrCountryId {get;set;} 
	System.DateTime? CreateDate {get;set;} 
	System.DateTime? UpdateDate {get;set;} 
	System.String CreateUser {get;set;} 
	System.String UpdateUser {get;set;} 
}
#endregion

	
	[DefaultMapperAttr(typeof(OracleMappers.LocationDBMapper)), ComVisible(false), Serializable(), System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
	public class LocationBase : ModelObject, IEquatable<LocationBase>, IAuditable,ILocation {

		#region "Constructor"

		public LocationBase() {
			this.addValidator(new LocationRequiredFieldsValidator());
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

					public const String STR_FLD_LOCATION_ID = "LocationId";
			public const String STR_FLD_STREET_ADDRESS = "StreetAddress";
			public const String STR_FLD_POSTAL_CODE = "PostalCode";
			public const String STR_FLD_CITY = "CITY";
			public const String STR_FLD_STATE_PROVINCE = "StateProvince";
			public const String STR_FLD_COUNTRY_ID = "CountryId";
			public const String STR_FLD_CREATE_DATE = "CreateDate";
			public const String STR_FLD_UPDATE_DATE = "UpdateDate";
			public const String STR_FLD_CREATE_USER = "CreateUser";
			public const String STR_FLD_UPDATE_USER = "UpdateUser";


				public const int FLD_LOCATION_ID = 0;
		public const int FLD_STREET_ADDRESS = 1;
		public const int FLD_POSTAL_CODE = 2;
		public const int FLD_CITY = 3;
		public const int FLD_STATE_PROVINCE = 4;
		public const int FLD_COUNTRY_ID = 5;
		public const int FLD_CREATE_DATE = 6;
		public const int FLD_UPDATE_DATE = 7;
		public const int FLD_CREATE_USER = 8;
		public const int FLD_UPDATE_USER = 9;



		///<summary> Returns the names of fields in the object as a string array.
		/// Useful in automatically setting/getting values from UI objects (windows or web Form)</summary>
		/// <returns> string array </returns>	 
		public override string[] getFieldList()
		{
			return new string[] {
				STR_FLD_LOCATION_ID,STR_FLD_STREET_ADDRESS,STR_FLD_POSTAL_CODE,STR_FLD_CITY,STR_FLD_STATE_PROVINCE,STR_FLD_COUNTRY_ID,STR_FLD_CREATE_DATE,STR_FLD_UPDATE_DATE,STR_FLD_CREATE_USER,STR_FLD_UPDATE_USER
			};
		}

		#endregion

		#region "Field Declarations"

	private System.Int64 _LocationId;
	private System.String _StreetAddress;
	private System.String _PostalCode;
	private System.String _CITY;
	private System.String _StateProvince;
	private System.String _CountryId;
	private System.DateTime? _CreateDate = null;
	private System.DateTime? _UpdateDate = null;
	private System.String _CreateUser;
	private System.String _UpdateUser;

		#endregion

		#region "Field Properties"

	public virtual System.Int64 PrLocationId{
	get{
		return _LocationId;
	}
	set {
		if (ModelObject.valueChanged(_LocationId, value)){
			if (!this.IsObjectLoading) {
				this.isDirty = true;
				this.setFieldChanged(STR_FLD_LOCATION_ID);
			}
				this._LocationId=value;

			this.raiseBroadcastIdChange();

		}
		}
	}
public void setLocationId(String val){
	if (Information.IsNumeric(val)) {
		this.PrLocationId = Convert.ToInt64(val);
	} else if (String.IsNullOrEmpty(val)) {
		throw new ApplicationException("Cant update Primary Key to Null");
	} else {
		throw new ApplicationException("Invalid Integer Number, field:LocationId, value:" + val);
	}
}
	public virtual System.String PrStreetAddress{
	get{
		return _StreetAddress;
	}
	set {
		if (ModelObject.valueChanged(_StreetAddress, value)){
		if (value != null && value.Length > 40){
			throw new ModelObjectFieldTooLongException("STREET_ADDRESS");
		}
			if (!this.IsObjectLoading) {
				this.isDirty = true;
				this.setFieldChanged(STR_FLD_STREET_ADDRESS);
			}
				this._StreetAddress=value;

		}
		}
	}
public void setStreetAddress( String val ) {
	if (! string.IsNullOrEmpty(val)) {
		this.PrStreetAddress = val;
	} else {
		this.PrStreetAddress = null;
	}
}
	public virtual System.String PrPostalCode{
	get{
		return _PostalCode;
	}
	set {
		if (ModelObject.valueChanged(_PostalCode, value)){
		if (value != null && value.Length > 12){
			throw new ModelObjectFieldTooLongException("POSTAL_CODE");
		}
			if (!this.IsObjectLoading) {
				this.isDirty = true;
				this.setFieldChanged(STR_FLD_POSTAL_CODE);
			}
				this._PostalCode=value;

		}
		}
	}
public void setPostalCode( String val ) {
	if (! string.IsNullOrEmpty(val)) {
		this.PrPostalCode = val;
	} else {
		this.PrPostalCode = null;
	}
}
	public virtual System.String PrCITY{
	get{
		return _CITY;
	}
	set {
		if (ModelObject.valueChanged(_CITY, value)){
		if (value != null && value.Length > 30){
			throw new ModelObjectFieldTooLongException("CITY");
		}
			if (!this.IsObjectLoading) {
				this.isDirty = true;
				this.setFieldChanged(STR_FLD_CITY);
			}
				this._CITY=value;

		}
		}
	}
public void setCITY( String val ) {
	if (! string.IsNullOrEmpty(val)) {
		this.PrCITY = val;
	} else {
		this.PrCITY = null;
	}
}
	public virtual System.String PrStateProvince{
	get{
		return _StateProvince;
	}
	set {
		if (ModelObject.valueChanged(_StateProvince, value)){
		if (value != null && value.Length > 25){
			throw new ModelObjectFieldTooLongException("STATE_PROVINCE");
		}
			if (!this.IsObjectLoading) {
				this.isDirty = true;
				this.setFieldChanged(STR_FLD_STATE_PROVINCE);
			}
				this._StateProvince=value;

		}
		}
	}
public void setStateProvince( String val ) {
	if (! string.IsNullOrEmpty(val)) {
		this.PrStateProvince = val;
	} else {
		this.PrStateProvince = null;
	}
}
	public virtual System.String PrCountryId{
	get{
		return _CountryId;
	}
	set {
		if (ModelObject.valueChanged(_CountryId, value)){
		if (value != null && value.Length > 2){
			throw new ModelObjectFieldTooLongException("COUNTRY_ID");
		}
			if (!this.IsObjectLoading) {
				this.isDirty = true;
				this.setFieldChanged(STR_FLD_COUNTRY_ID);
			}
				this._CountryId=value;

		}
		}
	}
public void setCountryId( String val ) {
	if (! string.IsNullOrEmpty(val)) {
		this.PrCountryId = val;
	} else {
		this.PrCountryId = null;
	}
}
	public virtual System.DateTime? CreateDate{
	get{
		return _CreateDate;
	}
	set {
		if (ModelObject.valueChanged(_CreateDate, value)){
			if (!this.IsObjectLoading) {
				this.isDirty = true;
				this.setFieldChanged(STR_FLD_CREATE_DATE);
			}
				this._CreateDate=value;

		}
		}
	}
public void setCreateDate( String val ){
	if (Information.IsDate(val)) {
		this.CreateDate = Convert.ToDateTime(val);
	} else if (String.IsNullOrEmpty(val) ) {
		this.CreateDate = null;
	} else {
		throw new ApplicationException("Invalid Date, field:CreateDate, value:" + val);
	}
}
	public virtual System.DateTime? UpdateDate{
	get{
		return _UpdateDate;
	}
	set {
		if (ModelObject.valueChanged(_UpdateDate, value)){
			if (!this.IsObjectLoading) {
				this.isDirty = true;
				this.setFieldChanged(STR_FLD_UPDATE_DATE);
			}
				this._UpdateDate=value;

		}
		}
	}
public void setUpdateDate( String val ){
	if (Information.IsDate(val)) {
		this.UpdateDate = Convert.ToDateTime(val);
	} else if (String.IsNullOrEmpty(val) ) {
		this.UpdateDate = null;
	} else {
		throw new ApplicationException("Invalid Date, field:UpdateDate, value:" + val);
	}
}
	public virtual System.String CreateUser{
	get{
		return _CreateUser;
	}
	set {
		if (ModelObject.valueChanged(_CreateUser, value)){
		if (value != null && value.Length > 20){
			throw new ModelObjectFieldTooLongException("CREATE_USER");
		}
			if (!this.IsObjectLoading) {
				this.isDirty = true;
				this.setFieldChanged(STR_FLD_CREATE_USER);
			}
				this._CreateUser=value;

		}
		}
	}
public void setCreateUser( String val ) {
	if (! string.IsNullOrEmpty(val)) {
		this.CreateUser = val;
	} else {
		this.CreateUser = null;
	}
}
	public virtual System.String UpdateUser{
	get{
		return _UpdateUser;
	}
	set {
		if (ModelObject.valueChanged(_UpdateUser, value)){
		if (value != null && value.Length > 20){
			throw new ModelObjectFieldTooLongException("UPDATE_USER");
		}
			if (!this.IsObjectLoading) {
				this.isDirty = true;
				this.setFieldChanged(STR_FLD_UPDATE_USER);
			}
				this._UpdateUser=value;

		}
		}
	}
public void setUpdateUser( String val ) {
	if (! string.IsNullOrEmpty(val)) {
		this.UpdateUser = val;
	} else {
		this.UpdateUser = null;
	}
}

		#endregion

		#region "Getters/Setters of values by field index/name"
		public override object getAttribute(int fieldKey){

		switch (fieldKey) {
		case FLD_LOCATION_ID:
			return this.PrLocationId;
		case FLD_STREET_ADDRESS:
			return this.PrStreetAddress;
		case FLD_POSTAL_CODE:
			return this.PrPostalCode;
		case FLD_CITY:
			return this.PrCITY;
		case FLD_STATE_PROVINCE:
			return this.PrStateProvince;
		case FLD_COUNTRY_ID:
			return this.PrCountryId;
		case FLD_CREATE_DATE:
			return this.CreateDate;
		case FLD_UPDATE_DATE:
			return this.UpdateDate;
		case FLD_CREATE_USER:
			return this.CreateUser;
		case FLD_UPDATE_USER:
			return this.UpdateUser;
		default:
			return null;
		} //end switch

		}

		public override object getAttribute(string fieldKey) {
			fieldKey = fieldKey.ToLower();

		if (fieldKey==STR_FLD_LOCATION_ID.ToLower() ) {
			return this.PrLocationId;
		} else if (fieldKey==STR_FLD_STREET_ADDRESS.ToLower() ) {
			return this.PrStreetAddress;
		} else if (fieldKey==STR_FLD_POSTAL_CODE.ToLower() ) {
			return this.PrPostalCode;
		} else if (fieldKey==STR_FLD_CITY.ToLower() ) {
			return this.PrCITY;
		} else if (fieldKey==STR_FLD_STATE_PROVINCE.ToLower() ) {
			return this.PrStateProvince;
		} else if (fieldKey==STR_FLD_COUNTRY_ID.ToLower() ) {
			return this.PrCountryId;
		} else if (fieldKey==STR_FLD_CREATE_DATE.ToLower() ) {
			return this.CreateDate;
		} else if (fieldKey==STR_FLD_UPDATE_DATE.ToLower() ) {
			return this.UpdateDate;
		} else if (fieldKey==STR_FLD_CREATE_USER.ToLower() ) {
			return this.CreateUser;
		} else if (fieldKey==STR_FLD_UPDATE_USER.ToLower() ) {
			return this.UpdateUser;
		} else {
			return null;
		}
		}

		public override void setAttribute(int fieldKey, object val){
		switch (fieldKey) {
		case FLD_LOCATION_ID:
			if (val == DBNull.Value || val == null ){
				throw new ApplicationException("Can't set Primary Key to null");
			} else {
				this.PrLocationId=(System.Int64)val;
			} //
			return;
		case FLD_STREET_ADDRESS:
			if (val == DBNull.Value || val == null ){
				this.PrStreetAddress = null;
			} else {
				this.PrStreetAddress=(System.String)val;
			} //
			return;
		case FLD_POSTAL_CODE:
			if (val == DBNull.Value || val == null ){
				this.PrPostalCode = null;
			} else {
				this.PrPostalCode=(System.String)val;
			} //
			return;
		case FLD_CITY:
			if (val == DBNull.Value || val == null ){
				this.PrCITY = null;
			} else {
				this.PrCITY=(System.String)val;
			} //
			return;
		case FLD_STATE_PROVINCE:
			if (val == DBNull.Value || val == null ){
				this.PrStateProvince = null;
			} else {
				this.PrStateProvince=(System.String)val;
			} //
			return;
		case FLD_COUNTRY_ID:
			if (val == DBNull.Value || val == null ){
				this.PrCountryId = null;
			} else {
				this.PrCountryId=(System.String)val;
			} //
			return;
		case FLD_CREATE_DATE:
			if (val == DBNull.Value || val == null ){
				this.CreateDate = null;
			} else {
				this.CreateDate=(System.DateTime)val;
			} //
			return;
		case FLD_UPDATE_DATE:
			if (val == DBNull.Value || val == null ){
				this.UpdateDate = null;
			} else {
				this.UpdateDate=(System.DateTime)val;
			} //
			return;
		case FLD_CREATE_USER:
			if (val == DBNull.Value || val == null ){
				this.CreateUser = null;
			} else {
				this.CreateUser=(System.String)val;
			} //
			return;
		case FLD_UPDATE_USER:
			if (val == DBNull.Value || val == null ){
				this.UpdateUser = null;
			} else {
				this.UpdateUser=(System.String)val;
			} //
			return;
		default:
			return;
		}

		}

		public override void setAttribute(string fieldKey, object val) {
			fieldKey = fieldKey.ToLower();
		if ( fieldKey==STR_FLD_LOCATION_ID.ToLower()){
			if (val == DBNull.Value || val ==null ){
				throw new ApplicationException("Can't set Primary Key to null");
			} else {
				this.PrLocationId=(System.Int64)val;
			}
			return;
		} else if ( fieldKey==STR_FLD_STREET_ADDRESS.ToLower()){
			if (val == DBNull.Value || val ==null ){
				this.PrStreetAddress = null;
			} else {
				this.PrStreetAddress=(System.String)val;
			}
			return;
		} else if ( fieldKey==STR_FLD_POSTAL_CODE.ToLower()){
			if (val == DBNull.Value || val ==null ){
				this.PrPostalCode = null;
			} else {
				this.PrPostalCode=(System.String)val;
			}
			return;
		} else if ( fieldKey==STR_FLD_CITY.ToLower()){
			if (val == DBNull.Value || val ==null ){
				this.PrCITY = null;
			} else {
				this.PrCITY=(System.String)val;
			}
			return;
		} else if ( fieldKey==STR_FLD_STATE_PROVINCE.ToLower()){
			if (val == DBNull.Value || val ==null ){
				this.PrStateProvince = null;
			} else {
				this.PrStateProvince=(System.String)val;
			}
			return;
		} else if ( fieldKey==STR_FLD_COUNTRY_ID.ToLower()){
			if (val == DBNull.Value || val ==null ){
				this.PrCountryId = null;
			} else {
				this.PrCountryId=(System.String)val;
			}
			return;
		} else if ( fieldKey==STR_FLD_CREATE_DATE.ToLower()){
			if (val == DBNull.Value || val ==null ){
				this.CreateDate = null;
			} else {
				this.CreateDate=(System.DateTime)val;
			}
			return;
		} else if ( fieldKey==STR_FLD_UPDATE_DATE.ToLower()){
			if (val == DBNull.Value || val ==null ){
				this.UpdateDate = null;
			} else {
				this.UpdateDate=(System.DateTime)val;
			}
			return;
		} else if ( fieldKey==STR_FLD_CREATE_USER.ToLower()){
			if (val == DBNull.Value || val ==null ){
				this.CreateUser = null;
			} else {
				this.CreateUser=(System.String)val;
			}
			return;
		} else if ( fieldKey==STR_FLD_UPDATE_USER.ToLower()){
			if (val == DBNull.Value || val ==null ){
				this.UpdateUser = null;
			} else {
				this.UpdateUser=(System.String)val;
			}
			return;
		}
		}

		#endregion
		#region "Overrides of GetHashCode and Equals "
		public bool Equals(LocationBase other)
		{

			//typesafe equals, checks for equality of fields
			if (other == null)
				return false;
			if (object.ReferenceEquals(other, this))
				return true;

			return this.PrLocationId == other.PrLocationId
				&& this.PrStreetAddress == other.PrStreetAddress
				&& this.PrPostalCode == other.PrPostalCode
				&& this.PrCITY == other.PrCITY
				&& this.PrStateProvince == other.PrStateProvince
				&& this.PrCountryId == other.PrCountryId
				&& this.CreateDate.GetValueOrDefault() == other.CreateDate.GetValueOrDefault()
				&& this.UpdateDate.GetValueOrDefault() == other.UpdateDate.GetValueOrDefault()
				&& this.CreateUser == other.CreateUser
				&& this.UpdateUser == other.UpdateUser;;

		}

		public override int GetHashCode()
		{
			//using Xor has the advantage of not overflowing the integer.
			return this.PrLocationId.GetHashCode()
				 ^ this.getStringHashCode(this.PrStreetAddress)
				 ^ this.getStringHashCode(this.PrPostalCode)
				 ^ this.getStringHashCode(this.PrCITY)
				 ^ this.getStringHashCode(this.PrStateProvince)
				 ^ this.getStringHashCode(this.PrCountryId)
				 ^ this.CreateDate.GetHashCode()
				 ^ this.UpdateDate.GetHashCode()
				 ^ this.getStringHashCode(this.CreateUser)
				 ^ this.getStringHashCode(this.UpdateUser);;

		}

		public override bool Equals(object Obj) {

			if (Obj != null && Obj is LocationBase) {

				return this.Equals((LocationBase)Obj);

			} else {
				return false;
			}

		}

		public static bool operator ==(LocationBase obj1, LocationBase obj2)
		{
			return object.Equals(obj1, obj2);
		}

		public static bool operator !=(LocationBase obj1, LocationBase obj2)
		{
			return !(obj1 == obj2);
		}

		#endregion

		#region "Copy and sort"

		public override IModelObject copy()
		{
			//creates a copy

			//NOTE: we can't cast from LocationBase to Location, so below we 
			//instantiate a Location, NOT a LocationBase object
			Location ret = LocationFactory.Create();

		ret.PrLocationId = this.PrLocationId;
		ret.PrStreetAddress = this.PrStreetAddress;
		ret.PrPostalCode = this.PrPostalCode;
		ret.PrCITY = this.PrCITY;
		ret.PrStateProvince = this.PrStateProvince;
		ret.PrCountryId = this.PrCountryId;
		ret.CreateDate = this.CreateDate;
		ret.UpdateDate = this.UpdateDate;
		ret.CreateUser = this.CreateUser;
		ret.UpdateUser = this.UpdateUser;



			return ret;

		}

		#endregion




		#region "ID Property"

		public override object Id {
			get { return this._LocationId; }
			set {
				this._LocationId = Convert.ToInt64(value);
				this.raiseBroadcastIdChange();
			}
		}
		#endregion

		#region "Extra Code"

		#endregion

	}

	#region "Req Fields validator"
	[System.Runtime.InteropServices.ComVisible(false)]
	public class LocationRequiredFieldsValidator : IModelObjectValidator
	{


		public void validate(org.model.lib.Model.IModelObject imo) {
			Location mo = (Location)imo;
if (string.IsNullOrEmpty( mo.PrCITY)) {
		throw new ModelObjectRequiredFieldException("CITY");
}

		}

	}
	#endregion

}

