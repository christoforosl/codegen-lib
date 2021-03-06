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

	[Table(Name = "Employee_Evaluation")]
	[DataContract][SelectObject("Employee_Evaluation")][KeyFieldName("Employee_Evaluation_Id")]
	[DefaultMapperAttr(typeof(CsModelMappers.EmployeeEvaluationDBMapper)), ComVisible(false), Serializable(), System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
	partial class EmployeeEvaluation:ModelObject,IEquatable<EmployeeEvaluation>  {

		#region "Constructor"

		public EmployeeEvaluation() {
			this.Id = ModelObjectKeyGen.nextId();
			this.addValidator(new EmployeeEvaluationRequiredFieldsValidator());
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

					public const String STR_FLD_EMPLOYEE_EVALUATION_ID = "EmployeeEvaluationId";
			public const String STR_FLD_EVALUATOR_ID = "EvaluatorId";
			public const String STR_FLD_EVALUATION_DATE = "EvaluationDate";
			public const String STR_FLD_EMPLOYEE_ID = "EmployeeId";


				public const int FLD_EMPLOYEE_EVALUATION_ID = 0;
		public const int FLD_EVALUATOR_ID = 1;
		public const int FLD_EVALUATION_DATE = 2;
		public const int FLD_EMPLOYEE_ID = 3;



		///<summary> Returns the names of fields in the object as a string array.
		/// Useful in automatically setting/getting values from UI objects (windows or web Form)</summary>
		/// <returns> string array </returns>	 
		public override string[] getFieldList()
		{
			return new string[] {
				STR_FLD_EMPLOYEE_EVALUATION_ID,STR_FLD_EVALUATOR_ID,STR_FLD_EVALUATION_DATE,STR_FLD_EMPLOYEE_ID
			};
		}

		#endregion

		#region "Field Declarations"

	private System.Int64 _EmployeeEvaluationId;
	private System.Int64? _EvaluatorId = null;
	private System.DateTime? _EvaluationDate = null;
	private System.Int64? _EmployeeId = null;

		#endregion

		#region "Field Properties"

		//Field Employee_Evaluation_Id
		[Required]
		[Column(Name="Employee_Evaluation_Id",Storage = "_EmployeeEvaluationId", IsPrimaryKey=true,DbType = "int NOT NULL",CanBeNull = false)]
		[DataMember]
		public virtual System.Int64 PrEmployeeEvaluationId{
			get{			
				return _EmployeeEvaluationId;
			}
			set {
				if (ModelObject.valueChanged(_EmployeeEvaluationId, value)){
					if (!this.IsObjectLoading) {
						this.isDirty = true; //
						this.setFieldChanged(STR_FLD_EMPLOYEE_EVALUATION_ID);
					}
					this._EmployeeEvaluationId = value;
					this.raiseBroadcastIdChange();
				}
			}
		}
		//Field evaluator_id
		[Key]
		[Column(Name="evaluator_id",Storage = "_EvaluatorId", IsPrimaryKey=false,DbType = "int",CanBeNull = true)]
		[DataMember]
		public virtual System.Int64? PrEvaluatorId{
			get{			
				return _EvaluatorId;
			}
			set {
				if (ModelObject.valueChanged(_EvaluatorId, value)){
					if (!this.IsObjectLoading) {
						this.isDirty = true; //
						this.setFieldChanged(STR_FLD_EVALUATOR_ID);
					}
					this._EvaluatorId = value;
				}
			}
		}
		//Field evaluation_date
		[Key]
		[Column(Name="evaluation_date",Storage = "_EvaluationDate", IsPrimaryKey=false,DbType = "datetime",CanBeNull = true)]
		[DataMember]
		public virtual System.DateTime? PrEvaluationDate{
			get{			
				return _EvaluationDate;
			}
			set {
				if (ModelObject.valueChanged(_EvaluationDate, value)){
					if (!this.IsObjectLoading) {
						this.isDirty = true; //
						this.setFieldChanged(STR_FLD_EVALUATION_DATE);
					}
					this._EvaluationDate = value;
				}
			}
		}
		//Field employee_id
		[Key]
		[Column(Name="employee_id",Storage = "_EmployeeId", IsPrimaryKey=false,DbType = "int",CanBeNull = true)]
		[DataMember]
		public virtual System.Int64? PrEmployeeId{
			get{			
				return _EmployeeId;
			}
			set {
				if (ModelObject.valueChanged(_EmployeeId, value)){
					if (!this.IsObjectLoading) {
						this.isDirty = true; //
						this.setFieldChanged(STR_FLD_EMPLOYEE_ID);
					}
					this._EmployeeId = value;
				}
			}
		}

		#endregion

		#region "Getters/Setters of values by field index/name"
		public override object getAttribute(int fieldKey){

		switch (fieldKey) {
		case FLD_EMPLOYEE_EVALUATION_ID:
			return this.PrEmployeeEvaluationId;
		case FLD_EVALUATOR_ID:
			return this.PrEvaluatorId;
		case FLD_EVALUATION_DATE:
			return this.PrEvaluationDate;
		case FLD_EMPLOYEE_ID:
			return this.PrEmployeeId;
		default:
			return null;
		} //end switch

		}

		public override object getAttribute(string fieldKey) {
			fieldKey = fieldKey.ToLower();

		if (fieldKey==STR_FLD_EMPLOYEE_EVALUATION_ID.ToLower() ) {
			return this.PrEmployeeEvaluationId;
		} else if (fieldKey==STR_FLD_EVALUATOR_ID.ToLower() ) {
			return this.PrEvaluatorId;
		} else if (fieldKey==STR_FLD_EVALUATION_DATE.ToLower() ) {
			return this.PrEvaluationDate;
		} else if (fieldKey==STR_FLD_EMPLOYEE_ID.ToLower() ) {
			return this.PrEmployeeId;
		} else {
			return null;
		}
		}

		public override void setAttribute(int fieldKey, object val){
			try {
		switch (fieldKey) {
		case FLD_EMPLOYEE_EVALUATION_ID:
			if (val == DBNull.Value || val == null ){
				throw new ApplicationException("Can't set Primary Key to null");
			} else {
				this.PrEmployeeEvaluationId=(System.Int64)val;
			} //
			return;
		case FLD_EVALUATOR_ID:
			if (val == DBNull.Value || val == null ){
				this.PrEvaluatorId = null;
			} else {
				this.PrEvaluatorId=(System.Int64?)val;
			} //
			return;
		case FLD_EVALUATION_DATE:
			if (val == DBNull.Value || val == null ){
				this.PrEvaluationDate = null;
			} else {
				this.PrEvaluationDate=(System.DateTime?)val;
			} //
			return;
		case FLD_EMPLOYEE_ID:
			if (val == DBNull.Value || val == null ){
				this.PrEmployeeId = null;
			} else {
				this.PrEmployeeId=(System.Int64?)val;
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
		if ( fieldKey==STR_FLD_EMPLOYEE_EVALUATION_ID.ToLower()){
			if (val == DBNull.Value || val ==null ){
				throw new ApplicationException("Can't set Primary Key to null");
			} else {
				this.PrEmployeeEvaluationId=Convert.ToInt64(val);
			}
			return;
		} else if ( fieldKey==STR_FLD_EVALUATOR_ID.ToLower()){
			if (val == DBNull.Value || val ==null ){
				this.PrEvaluatorId = null;
			} else {
				this.PrEvaluatorId=Convert.ToInt64(val);
			}
			return;
		} else if ( fieldKey==STR_FLD_EVALUATION_DATE.ToLower()){
			if (val == DBNull.Value || val ==null ){
				this.PrEvaluationDate = null;
			} else {
				this.PrEvaluationDate=Convert.ToDateTime(val);
			}
			return;
		} else if ( fieldKey==STR_FLD_EMPLOYEE_ID.ToLower()){
			if (val == DBNull.Value || val ==null ){
				this.PrEmployeeId = null;
			} else {
				this.PrEmployeeId=Convert.ToInt64(val);
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
		public bool Equals(EmployeeEvaluation other)
		{

			//typesafe equals, checks for equality of fields
			if (other == null)
				return false;
			if (object.ReferenceEquals(other, this))
				return true;

			return this.PrEmployeeEvaluationId == other.PrEmployeeEvaluationId
				&& this.PrEvaluatorId.GetValueOrDefault() == other.PrEvaluatorId.GetValueOrDefault()
				&& this.PrEvaluationDate.GetValueOrDefault() == other.PrEvaluationDate.GetValueOrDefault()
				&& this.PrEmployeeId.GetValueOrDefault() == other.PrEmployeeId.GetValueOrDefault();;

		}

		public override int GetHashCode()
		{
			//using Xor has the advantage of not overflowing the integer.
			return this.PrEmployeeEvaluationId.GetHashCode()
				 ^ this.PrEvaluatorId.GetHashCode()
				 ^ this.PrEvaluationDate.GetHashCode()
				 ^ this.PrEmployeeId.GetHashCode();;

		}

		public override bool Equals(object Obj) {

			if (Obj != null && Obj is EmployeeEvaluation) {

				return this.Equals((EmployeeEvaluation)Obj);

			} else {
				return false;
			}

		}

		public static bool operator ==(EmployeeEvaluation obj1, EmployeeEvaluation obj2)
		{
			return object.Equals(obj1, obj2);
		}

		public static bool operator !=(EmployeeEvaluation obj1, EmployeeEvaluation obj2) {
			return !(obj1 == obj2);
		}

		#endregion

		#region "Copy and sort"

		public override IModelObject copy() {
			//creates a copy
			EmployeeEvaluation ret = new EmployeeEvaluation();
		ret.PrEmployeeEvaluationId = this.PrEmployeeEvaluationId;
		ret.PrEvaluatorId = this.PrEvaluatorId;
		ret.PrEvaluationDate = this.PrEvaluationDate;
		ret.PrEmployeeId = this.PrEmployeeId;

			return ret;

		}

		#endregion




		#region "ID Property"

		[DataMember]public sealed override object Id {
			get { return this._EmployeeEvaluationId; }
			set {
				this._EmployeeEvaluationId = Convert.ToInt64(value);
				this.raiseBroadcastIdChange();
			}
		}
		#endregion

		#region "Extra Code"

		#endregion

	}

	#region "Req Fields validator"
	[System.Runtime.InteropServices.ComVisible(false)]
	public class EmployeeEvaluationRequiredFieldsValidator : IModelObjectValidator {

		public void validate(org.model.lib.Model.IModelObject imo) {
			EmployeeEvaluation mo = (EmployeeEvaluation)imo;
			
		}

	}
	#endregion

}


