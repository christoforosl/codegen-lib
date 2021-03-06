﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Runtime.Serialization;
using org.model.lib.Model;
using org.model.lib;

using Microsoft.VisualBasic;
using System.Runtime.InteropServices;
using System.Xml.Serialization;
using System.Data.Linq.Mapping;
using System.ComponentModel.DataAnnotations;


//<comments>
//************************************************************
// Template: ModelBase2.csharp.txt
// Class autogenerated on 09/06/2013 8:02:57 AM by ModelGenerator
// Extends base model object class
// *** DO NOT change code in this class.  
//     It will be re-generated and 
//     overwritten by the code generator ****
//</comments>
namespace CsModelObjects {

	[Table(Name = "Bank")]
	[DataContract][SelectObject("Bank")][KeyFieldName("BANKID")]
	[DefaultMapperAttr(typeof(CsModelMappers.BankDBMapper)), ComVisible(false), Serializable(), System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
	partial class Bank:ModelObject,IEquatable<Bank>  {

		#region "Constructor"

		public Bank() {
			this.Id = ModelObjectKeyGen.nextId();
			this.addValidator(new BankRequiredFieldsValidator());
		}

		#endregion

		#region "Children and Parents"
		
		[OnDeserializing]
        public void OnDeserializingMethod(StreamingContext context) {
            this.IsObjectLoading = true;
        }

		[OnDeserialized]
        public void OnDeserializedMethod(StreamingContext context) {

			this.IsObjectLoading = false;
			this.isDirty = true;
        }

		public override void loadObjectHierarchy() {

		}

		/// <summary>
		/// Returns the *loaded* children of this model object.
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

					public const String STR_FLD_BANKID = "Bankid";
			public const String STR_FLD_BANKNAME = "BankName";
			public const String STR_FLD_BANKCODE = "BankCode";
			public const String STR_FLD_BANKSWIFTCODE = "BankSWIFTCode";


				public const int FLD_BANKID = 0;
		public const int FLD_BANKNAME = 1;
		public const int FLD_BANKCODE = 2;
		public const int FLD_BANKSWIFTCODE = 3;



		///<summary> Returns the names of fields in the object as a string array.
		/// Useful in automatically setting/getting values from UI objects (windows or web Form)</summary>
		/// <returns> string array </returns>	 
		public override string[] getFieldList()
		{
			return new string[] {
				STR_FLD_BANKID,STR_FLD_BANKNAME,STR_FLD_BANKCODE,STR_FLD_BANKSWIFTCODE
			};
		}

		#endregion

		#region "Field Declarations"

	private System.Int64 _Bankid;
	private System.String _BankName;
	private System.String _BankCode;
	private System.String _BankSWIFTCode;

		#endregion

		#region "Field Properties"

		//Field BANKID
		[Required]
		[Column(Name="BANKID",Storage = "_Bankid", IsPrimaryKey=true,DbType = "int NOT NULL",CanBeNull = false)]
		[DataMember]
		public virtual System.Int64 PrBankid{
			get{			
				return _Bankid;
			}
			set {
				if (ModelObject.valueChanged(_Bankid, value)){
					if (!this.IsObjectLoading) {
						this.isDirty = true; //
						this.setFieldChanged(STR_FLD_BANKID);
					}
					this._Bankid = value;
					this.raiseBroadcastIdChange();
				}
			}
		}
		//Field BankName
		[Key]
		[StringLength(50, ErrorMessage="BankName must be 50 characters or less")]
		[Column(Name="BankName",Storage = "_BankName", IsPrimaryKey=false,DbType = "nvarchar",CanBeNull = true)]
		[DataMember]
		public virtual System.String PrBankName{
			get{			
				return _BankName;
			}
			set {
				if (ModelObject.valueChanged(_BankName, value)){
					if (value != null && value.Length > 50){
						throw new ModelObjectFieldTooLongException("BankName");
					}
					if (!this.IsObjectLoading) {
						this.isDirty = true; //
						this.setFieldChanged(STR_FLD_BANKNAME);
					}
					this._BankName = value;
				}
			}
		}
		//Field BankCode
		[Key]
		[StringLength(20, ErrorMessage="BankCode must be 20 characters or less")]
		[Column(Name="BankCode",Storage = "_BankCode", IsPrimaryKey=false,DbType = "nvarchar",CanBeNull = true)]
		[DataMember]
		public virtual System.String PrBankCode{
			get{			
				return _BankCode;
			}
			set {
				if (ModelObject.valueChanged(_BankCode, value)){
					if (value != null && value.Length > 20){
						throw new ModelObjectFieldTooLongException("BankCode");
					}
					if (!this.IsObjectLoading) {
						this.isDirty = true; //
						this.setFieldChanged(STR_FLD_BANKCODE);
					}
					this._BankCode = value;
				}
			}
		}
		//Field BankSWIFTCode
		[Key]
		[StringLength(200, ErrorMessage="BankSWIFTCode must be 200 characters or less")]
		[Column(Name="BankSWIFTCode",Storage = "_BankSWIFTCode", IsPrimaryKey=false,DbType = "varchar",CanBeNull = true)]
		[DataMember]
		public virtual System.String PrBankSWIFTCode{
			get{			
				return _BankSWIFTCode;
			}
			set {
				if (ModelObject.valueChanged(_BankSWIFTCode, value)){
					if (value != null && value.Length > 200){
						throw new ModelObjectFieldTooLongException("BankSWIFTCode");
					}
					if (!this.IsObjectLoading) {
						this.isDirty = true; //
						this.setFieldChanged(STR_FLD_BANKSWIFTCODE);
					}
					this._BankSWIFTCode = value;
				}
			}
		}

		#endregion

		#region "Getters/Setters of values by field index/name"
		public override object getAttribute(int fieldKey){

		switch (fieldKey) {
		case FLD_BANKID:
			return this.PrBankid;
		case FLD_BANKNAME:
			return this.PrBankName;
		case FLD_BANKCODE:
			return this.PrBankCode;
		case FLD_BANKSWIFTCODE:
			return this.PrBankSWIFTCode;
		default:
			return null;
		} //end switch

		}

		public override object getAttribute(string fieldKey) {
			fieldKey = fieldKey.ToLower();

		if (fieldKey==STR_FLD_BANKID.ToLower() ) {
			return this.PrBankid;
		} else if (fieldKey==STR_FLD_BANKNAME.ToLower() ) {
			return this.PrBankName;
		} else if (fieldKey==STR_FLD_BANKCODE.ToLower() ) {
			return this.PrBankCode;
		} else if (fieldKey==STR_FLD_BANKSWIFTCODE.ToLower() ) {
			return this.PrBankSWIFTCode;
		} else {
			return null;
		}
		}

		public override void setAttribute(int fieldKey, object val){
			try {
		switch (fieldKey) {
		case FLD_BANKID:
			if (val == DBNull.Value || val == null ){
				throw new ApplicationException("Can't set Primary Key to null");
			} else {
				this.PrBankid=(System.Int64)val;
			} //
			return;
		case FLD_BANKNAME:
			if (val == DBNull.Value || val == null ){
				this.PrBankName = null;
			} else {
				this.PrBankName=(System.String)val;
			} //
			return;
		case FLD_BANKCODE:
			if (val == DBNull.Value || val == null ){
				this.PrBankCode = null;
			} else {
				this.PrBankCode=(System.String)val;
			} //
			return;
		case FLD_BANKSWIFTCODE:
			if (val == DBNull.Value || val == null ){
				this.PrBankSWIFTCode = null;
			} else {
				this.PrBankSWIFTCode=(System.String)val;
			} //
			return;
		default:
			return;
		}

			} catch ( Exception ex ) {
				throw new ApplicationException(
						String.Format("Error setting field with index {0}, value \"{1}\" : {2}", 
								fieldKey, val, ex.Message));
			}
		}

		public override void setAttribute(string fieldKey, object val) {
			fieldKey = fieldKey.ToLower();
			try {
		if ( fieldKey==STR_FLD_BANKID.ToLower()){
			if (val == DBNull.Value || val ==null ){
				throw new ApplicationException("Can't set Primary Key to null");
			} else {
				this.PrBankid=Convert.ToInt64(val);
			}
			return;
		} else if ( fieldKey==STR_FLD_BANKNAME.ToLower()){
			if (val == DBNull.Value || val ==null ){
				this.PrBankName = null;
			} else {
				this.PrBankName=Convert.ToString(val);
			}
			return;
		} else if ( fieldKey==STR_FLD_BANKCODE.ToLower()){
			if (val == DBNull.Value || val ==null ){
				this.PrBankCode = null;
			} else {
				this.PrBankCode=Convert.ToString(val);
			}
			return;
		} else if ( fieldKey==STR_FLD_BANKSWIFTCODE.ToLower()){
			if (val == DBNull.Value || val ==null ){
				this.PrBankSWIFTCode = null;
			} else {
				this.PrBankSWIFTCode=Convert.ToString(val);
			}
			return;
		}
			} catch ( Exception ex ) {
				throw new ApplicationException(
					String.Format("Error setting field with index {0}, value \"{1}\" : {2}", 
							fieldKey, val, ex.Message));
			}
		}

		#endregion
		#region "Overrides of GetHashCode and Equals "
		public bool Equals(Bank other)
		{

			//typesafe equals, checks for equality of fields
			if (other == null)
				return false;
			if (object.ReferenceEquals(other, this))
				return true;

			return this.PrBankid == other.PrBankid
				&& this.PrBankName == other.PrBankName
				&& this.PrBankCode == other.PrBankCode
				&& this.PrBankSWIFTCode == other.PrBankSWIFTCode;;

		}

		public override int GetHashCode()
		{
			//using Xor has the advantage of not overflowing the integer.
			return this.PrBankid.GetHashCode()
				 ^ this.getStringHashCode(this.PrBankName)
				 ^ this.getStringHashCode(this.PrBankCode)
				 ^ this.getStringHashCode(this.PrBankSWIFTCode);;

		}

		public override bool Equals(object Obj) {

			if (Obj != null && Obj is Bank) {

				return this.Equals((Bank)Obj);

			} else {
				return false;
			}

		}

		public static bool operator ==(Bank obj1, Bank obj2)
		{
			return object.Equals(obj1, obj2);
		}

		public static bool operator !=(Bank obj1, Bank obj2) {
			return !(obj1 == obj2);
		}

		#endregion

		#region "Copy and sort"

		public override IModelObject copy() {
			//creates a copy
			Bank ret = new Bank();
		ret.PrBankid = this.PrBankid;
		ret.PrBankName = this.PrBankName;
		ret.PrBankCode = this.PrBankCode;
		ret.PrBankSWIFTCode = this.PrBankSWIFTCode;

			return ret;

		}

		#endregion




		#region "ID Property"

		[DataMember]public sealed override object Id {
			get { return this._Bankid; }
			set {
				this._Bankid = Convert.ToInt64(value);
				this.raiseBroadcastIdChange();
			}
		}
		#endregion

		#region "Extra Code"

		#endregion

	}

	#region "Req Fields validator"
	[System.Runtime.InteropServices.ComVisible(false)]
	public class BankRequiredFieldsValidator : IModelObjectValidator {

		public void validate(org.model.lib.Model.IModelObject imo) {
			Bank mo = (Bank)imo;
			
		}

	}
	#endregion

}


