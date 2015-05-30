﻿
'NOTE: DO NOT ADD REFERENCES TO COM.NETU.LIB HERE, INSTEAD ADD
'THE IMPORT ON THE REFERENCES OF THE PROJECT
Imports System.Runtime.InteropServices
Imports System.Collections.Generic
Imports System.Xml.Serialization
Imports System.Runtime.Serialization

'<comments>
'************************************************************
' Temnplate: ModelBase2.visualBasic.txt
' Class autogenerated on 30-May-15 9:52:36 AM by ModelGenerator
' Extends base model object class
' *** DO NOT change code in this class.  
'     It will be re-generated and 
'     overwritten by the code generator ****
' Instead, change code in the extender class EmployeeInfo
'
'************************************************************
'</comments>
Namespace VbBusObjects

<DefaultMapperAttr(GetType(EmployeeInfoDBMapper)), DataContract, _
ComVisible(False),Serializable(), System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)> _
Public class EmployeeInfoBase
		Inherits ModelObject
		Implements IEquatable(Of EmployeeInfoBase) 

#Region "Constructor"
    
    public sub New()
		Me.addValidator(New EmployeeInfoRequiredFieldsValidator)
    End Sub

#End Region

#Region "Children and Parents"
	
	public Overrides sub loadObjectHierarchy()
		
	End Sub

	Public Overrides Function getChildren() As List(Of ModelObject) 
		Dim ret as New List(Of ModelObject)()
		
		return ret
	End Function
	
	Public Overrides Function getParents() As List(Of ModelObject)
		Dim ret as New List(Of ModelObject)()
		
		return ret
	End Function

#End Region
#Region "Field CONSTANTS"

			public Const STR_FLD_EMPLOYEEINFOID as String = "EmployeeInfoId"
			public Const STR_FLD_EIEMPLOYEEID as String = "EIEmployeeId"
			public Const STR_FLD_SALARY as String = "Salary"
			public Const STR_FLD_ADDRESS as String = "Address"


		public Const FLD_EMPLOYEEINFOID as Integer = 0
		public Const FLD_EIEMPLOYEEID as Integer = 1
		public Const FLD_SALARY as Integer = 2
		public Const FLD_ADDRESS as Integer = 3



        '''<summary> Returns the names of fields in the object as a string array.
        ''' Useful in automatically setting/getting values from UI objects (windows or web Form)</summary>
        ''' <returns> string array </returns>	 
        Public Overrides Function getFieldList() As String()
            Return New String() {STR_FLD_EMPLOYEEINFOID,STR_FLD_EIEMPLOYEEID,STR_FLD_SALARY,STR_FLD_ADDRESS}
        End Function
        
#END Region

#Region "Field Declarations"


	Private _EmployeeInfoId as System.Int64
	Private _EIEmployeeId as Nullable (of System.Int64) = Nothing
	Private _Salary as Nullable (of System.Decimal) = Nothing
	Private _Address as System.String


#END Region

#Region "Field Properties"

<DataMember>	 Overridable Property PrEmployeeInfoId as System.Int64
	Get 
		return _EmployeeInfoId
	End Get 
	Set(ByVal value As System.Int64)
		if ModelObject.valueChanged(_EmployeeInfoId, value) then
			if me.IsObjectLoading = false then
				me.isDirty = true
				me.setFieldChanged(STR_FLD_EMPLOYEEINFOID)
			End If
			me._EmployeeInfoId=value

			me.raiseBroadcastIdChange()

		End if
	End Set 
	End Property 
<DataMember>	 Overridable Property PrEIEmployeeId as Nullable (of System.Int64)
	Get 
		return _EIEmployeeId
	End Get 
	Set(ByVal value As Nullable (of System.Int64))
		if ModelObject.valueChanged(_EIEmployeeId, value) then
			if me.IsObjectLoading = false then
				me.isDirty = true
				me.setFieldChanged(STR_FLD_EIEMPLOYEEID)
			End If
			me._EIEmployeeId=value

		End if
	End Set 
	End Property 
<DataMember>	 Overridable Property PrSalary as Nullable (of System.Decimal)
	Get 
		return _Salary
	End Get 
	Set(ByVal value As Nullable (of System.Decimal))
		if ModelObject.valueChanged(_Salary, value) then
			if me.IsObjectLoading = false then
				me.isDirty = true
				me.setFieldChanged(STR_FLD_SALARY)
			End If
			me._Salary=value

		End if
	End Set 
	End Property 
<DataMember>	 Overridable Property PrAddress as System.String
	Get 
		return _Address
	End Get 
	Set(ByVal value As System.String)
		if ModelObject.valueChanged(_Address, value) then
		if value isNot Nothing andAlso value.Length > 600 Then
			Throw new ModelObjectFieldTooLongException("Address")
		End If
			if me.IsObjectLoading = false then
				me.isDirty = true
				me.setFieldChanged(STR_FLD_ADDRESS)
			End If
			me._Address=value

		End if
	End Set 
	End Property 

#End Region

#Region "Getters/Setters of values by field index/name"
    Public Overloads Overrides Function getAttribute(ByVal fieldKey As Integer) As Object
		

		select case fieldKey
		case FLD_EMPLOYEEINFOID
			return me.PrEmployeeInfoId
		case FLD_EIEMPLOYEEID
			return me.PrEIEmployeeId
		case FLD_SALARY
			return me.PrSalary
		case FLD_ADDRESS
			return me.PrAddress
		case else
			return nothing
		end select


    End Function

    Public Overloads Overrides Function getAttribute(ByVal fieldKey As String) As Object
		fieldKey = fieldKey.ToLower

		if fieldKey=STR_FLD_EMPLOYEEINFOID.ToLower() Then
			return me.PrEmployeeInfoId
		else if fieldKey=STR_FLD_EIEMPLOYEEID.ToLower() Then
			return me.PrEIEmployeeId
		else if fieldKey=STR_FLD_SALARY.ToLower() Then
			return me.PrSalary
		else if fieldKey=STR_FLD_ADDRESS.ToLower() Then
			return me.PrAddress
		else
			return nothing
		end If
    End Function

    Public Overloads Overrides Sub setAttribute(ByVal fieldKey As Integer, ByVal val As Object)
		
		Select Case fieldKey
		case FLD_EMPLOYEEINFOID
			If Val Is DBNull.Value OrElse Val Is Nothing Then
				Me.PrEmployeeInfoId = Nothing
			Else
				Me.PrEmployeeInfoId=CType(val,System.Int64)
			End If
			return
		case FLD_EIEMPLOYEEID
			If Val Is DBNull.Value OrElse Val Is Nothing Then
				Me.PrEIEmployeeId = Nothing
			Else
				Me.PrEIEmployeeId=CType(val,System.Int64)
			End If
			return
		case FLD_SALARY
			If Val Is DBNull.Value OrElse Val Is Nothing Then
				Me.PrSalary = Nothing
			Else
				Me.PrSalary=CType(val,System.Decimal)
			End If
			return
		case FLD_ADDRESS
			If Val Is DBNull.Value OrElse Val Is Nothing Then
				Me.PrAddress = Nothing
			Else
				Me.PrAddress=CType(val,System.String)
			End If
			return
		case else
			return
		end select


    End Sub

    Public Overloads Overrides Sub setAttribute(ByVal fieldKey As String, ByVal val As Object)
		
		fieldKey = fieldKey.ToLower
		
		if  fieldKey=STR_FLD_EMPLOYEEINFOID.ToLower() Then
			If Val Is DBNull.Value OrElse Val Is Nothing Then
				Me.PrEmployeeInfoId = Nothing
			Else
				Me.PrEmployeeInfoId=CType(val,System.Int64)
			End If
			return
		else if  fieldKey=STR_FLD_EIEMPLOYEEID.ToLower() Then
			If Val Is DBNull.Value OrElse Val Is Nothing Then
				Me.PrEIEmployeeId = Nothing
			Else
				Me.PrEIEmployeeId=CType(val,System.Int64)
			End If
			return
		else if  fieldKey=STR_FLD_SALARY.ToLower() Then
			If Val Is DBNull.Value OrElse Val Is Nothing Then
				Me.PrSalary = Nothing
			Else
				Me.PrSalary=CType(val,System.Decimal)
			End If
			return
		else if  fieldKey=STR_FLD_ADDRESS.ToLower() Then
			If Val Is DBNull.Value OrElse Val Is Nothing Then
				Me.PrAddress = Nothing
			Else
				Me.PrAddress=CType(val,System.String)
			End If
			return
		end If

    End Sub

#End Region
#Region "Overrides of GetHashCode and Equals "
	Public Overloads Function Equals(ByVal other As EmployeeInfoBase) As Boolean _     
		Implements System.IEquatable(Of EmployeeInfoBase).Equals       
		
			'typesafe equals, checks for equality of fields
			If other Is Nothing Then Return False       
			If other Is Me Then Return True
		
			Return me.PrEmployeeInfoId= other.PrEmployeeInfoId _
				AndAlso me.PrEIEmployeeId.GetValueOrDefault = other.PrEIEmployeeId.GetValueOrDefault _
				AndAlso me.PrSalary.GetValueOrDefault = other.PrSalary.GetValueOrDefault _
				AndAlso me.PrAddress= other.PrAddress
				
	End Function
	
	Public Overrides Function GetHashCode() As Integer
        'using Xor has the advantage of not overflowing the integer.
        Return me.PrEmployeeInfoId.GetHashCode _
				Xor me.PrEIEmployeeId.GetHashCode _
				Xor me.PrSalary.GetHashCode _
				Xor me.getStringHashCode(Me.PrAddress) 
    
    End Function
    
    Public Overloads Overrides Function Equals(ByVal Obj As Object) As Boolean
		
		Dim temp = TryCast(obj, EmployeeInfoBase)       
		If temp IsNot Nothing Then 
			Return Me.Equals(temp)
		Else
			Return False
		End If

    End Function
	
	Public Shared Operator =(ByVal obj1 As EmployeeInfoBase, ByVal obj2 As EmployeeInfoBase) As Boolean       
		Return Object.Equals(obj1 ,obj2)    
	End Operator    
	
	Public Shared Operator <>(ByVal obj1 As EmployeeInfoBase, ByVal obj2 As EmployeeInfoBase) As Boolean       
		Return Not (obj1 = obj2)    
	End Operator

#End Region

#Region "Copy and sort"

	Public Overrides Function copy() as IModelObject
		'creates a copy
		
		'NOTE: we can't cast from EmployeeInfoBase to EmployeeInfo, so below we 
        'instantiate a EmployeeInfo, NOT a EmployeeInfoBase object
        Dim ret as EmployeeInfo =  new EmployeeInfo()
            
				ret.PrEmployeeInfoId = me.PrEmployeeInfoId
		ret.PrEIEmployeeId = me.PrEIEmployeeId
		ret.PrSalary = me.PrSalary
		ret.PrAddress = me.PrAddress

		
		return ret
		
	End Function
	
#End Region

#Region "parentIdChanged"
	'below sub is called when parentIdChanged
	public Overrides Sub handleParentIdChanged(parentMo as IModelObject)
		' Assocations from VbBusObjects.Employee
		if (typeof parentMo is VbBusObjects.Employee) Then
			me.PrEIEmployeeId= DirectCast(parentMo, VbBusObjects.Employee).PrEmployeeId
		End If
	End Sub
#End Region


#Region "ID Property"
	
    <DataMember>Public Overrides Property Id() As object
        Get
            return me._EmployeeInfoId
        End Get
        Set(ByVal value As object)
             me._EmployeeInfoId = Clng(value)
             me.raiseBroadcastIdChange()
        End Set
    End Property
#End Region
	
#Region "Extra Code"
	
#End Region
	
	End Class

#Region "Req Fields validator"
	<System.Runtime.InteropServices.ComVisible(False)> _
    Public Class EmployeeInfoRequiredFieldsValidator
        Implements IModelObjectValidator

        Public Sub validate(ByVal imo As org.model.lib.Model.IModelObject) _
                    Implements org.model.lib.IModelObjectValidator.validate

            Dim mo As EmployeeInfo = CType(imo, EmployeeInfo)
			if mo.PrEIEmployeeId is Nothing then
		Throw new ModelObjectRequiredFieldException("EIEmployeeId")
End if 

			
        End Sub

    End Class
#End Region

End Namespace

