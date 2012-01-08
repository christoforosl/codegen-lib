﻿
'NOTE: DO NOT ADD REFERENCES TO COM.NETU.LIB HERE, INSTEAD ADD
'THE IMPORT ON THE REFERENCES OF THE PROJECT

Imports System.Collections.Generic
Imports System.Xml.Serialization
'<comments>
'************************************************************
'
' Class autogenerated on 23/09/2011 2:05:07 PM by ModelGenerator
' Extends base model object class
' *** DO NOT change code in this class.  
'     It will be re-generated and 
'     overwritten by the code generator ****
' Instead, change code in the extender class EmployeeInfo
'
'************************************************************
'</comments>
Namespace BusObjects

#Region "Interface"
Public Interface IEmployeeInfo: Inherits IModelObject
	Property EmployeeInfoId as System.Int32
	Property EIEmployeeId as Nullable (of System.Int32)
	Property Salary as Nullable (of System.Decimal)
	Property Address as System.String
End Interface
#End region 


	<Serializable(), System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)> _
	Public class EmployeeInfoBase
		Inherits ModelObject
		Implements IEquatable(Of EmployeeInfoBase),
		IEmployeeInfo 

#Region "Constructor"
    
    public sub New()
		Me.addValidator(New EmployeeInfoRequiredFieldsValidator)
    End Sub

#End Region

#Region "Children and Parents"
	
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


	Private _EmployeeInfoId as System.Int32
	Private _EIEmployeeId as Nullable (of System.Int32) = Nothing
	Private _Salary as Nullable (of System.Decimal) = Nothing
	Private _Address as System.String = Nothing


#END Region

#Region "Field Properties"

	Public Overridable Property EmployeeInfoId as System.Int32 _ 
		Implements IEmployeeInfo.EmployeeInfoId
	Get 
		return _EmployeeInfoId
	End Get 
	Set
		if ModelObject.valueChanged(_EmployeeInfoId, value) then
			if me.IsObjectLoading = false then
				me.isDirty = true
				me.setFieldChanged(STR_FLD_EMPLOYEEINFOID)
			End If
			me._EmployeeInfoId = value

			me.raiseBroadcastIdChange()

		End if
	End Set 
	End Property 
Public Sub setEmployeeInfoId(ByVal val As String)
	If IsNumeric(val) Then
		Me.EmployeeInfoId = CInt(val)
	ElseIf String.IsNullOrEmpty(val) Then
		Me.EmployeeInfoId = Nothing
	Else
		Throw new ApplicationException("Invalid Integer Number, field:EmployeeInfoId, value:" & val)
	End If
End Sub
	Public Overridable Property EIEmployeeId as Nullable (of System.Int32) _ 
		Implements IEmployeeInfo.EIEmployeeId
	Get 
		return _EIEmployeeId
	End Get 
	Set(ByVal value As Nullable (of System.Int32))
		if ModelObject.valueChanged(_EIEmployeeId, value) then
			if me.IsObjectLoading = false then
				me.isDirty = true
				me.setFieldChanged(STR_FLD_EIEMPLOYEEID)
			End If
			me._EIEmployeeId = value

		End if
	End Set 
	End Property 
Public Sub setEIEmployeeId(ByVal val As String)
	If IsNumeric(val) Then
		Me.EIEmployeeId = CInt(val)
	ElseIf String.IsNullOrEmpty(val) Then
		Me.EIEmployeeId = Nothing
	Else
		Throw new ApplicationException("Invalid Integer Number, field:EIEmployeeId, value:" & val)
	End If
End Sub
	Public Overridable Property Salary as Nullable (of System.Decimal) _ 
		Implements IEmployeeInfo.Salary
	Get 
		return _Salary
	End Get 
	Set(ByVal value As Nullable (of System.Decimal))
		if ModelObject.valueChanged(_Salary, value) then
			if me.IsObjectLoading = false then
				me.isDirty = true
				me.setFieldChanged(STR_FLD_SALARY)
			End If
			me._Salary = value

		End if
	End Set 
	End Property 
Public Sub setSalary(ByVal val As String)
	If IsNumeric(val) Then
		Me.Salary = CDec(val)
	ElseIf String.IsNullOrEmpty(val) Then
		Me.Salary = Nothing
	Else
		Throw new ApplicationException("Invalid Decimal Number, field:Salary, value:" & val)
	End If
End Sub
	Public Overridable Property Address as System.String _ 
		Implements IEmployeeInfo.Address
	Get 
		return _Address
	End Get 
	Set
		if value isNot Nothing andAlso value.Length > 600 Then
			Throw new ModelObjectFieldTooLongException("Address")
		End If
		if ModelObject.valueChanged(_Address, value) then
			if me.IsObjectLoading = false then
				me.isDirty = true
				me.setFieldChanged(STR_FLD_ADDRESS)
			End If
			me._Address = value

		End if
	End Set 
	End Property 
Public Sub setAddress(ByVal val As String)
	If not String.isNullOrEmpty(val) Then
		Me.Address = val
	Else
		Me.Address = Nothing
	End If
End Sub

#End Region

#Region "Getters/Setters of values by field index/name"
    Public Overloads Overrides Function getAttribute(ByVal fieldKey As Integer) As Object
		

		select case fieldKey
		case FLD_EMPLOYEEINFOID
			return me.EmployeeInfoId
		case FLD_EIEMPLOYEEID
			return me.EIEmployeeId
		case FLD_SALARY
			return me.Salary
		case FLD_ADDRESS
			return me.Address
		case else
			return nothing
		end select


    End Function

    Public Overloads Overrides Function getAttribute(ByVal fieldKey As String) As Object
		fieldKey = fieldKey.ToLower

		if fieldKey=STR_FLD_EMPLOYEEINFOID.ToLower() Then
			return me.EmployeeInfoId
		else if fieldKey=STR_FLD_EIEMPLOYEEID.ToLower() Then
			return me.EIEmployeeId
		else if fieldKey=STR_FLD_SALARY.ToLower() Then
			return me.Salary
		else if fieldKey=STR_FLD_ADDRESS.ToLower() Then
			return me.Address
		else
			return nothing
		end If
    End Function

    Public Overloads Overrides Sub setAttribute(ByVal fieldKey As Integer, ByVal val As Object)
		
		Select Case fieldKey
		case FLD_EMPLOYEEINFOID
			If Val Is DBNull.Value OrElse Val Is Nothing Then
				Me.EmployeeInfoId = Nothing
			Else
				Me.EmployeeInfoId=CInt(val)
			End If
			return
		case FLD_EIEMPLOYEEID
			If Val Is DBNull.Value OrElse Val Is Nothing Then
				Me.EIEmployeeId = Nothing
			Else
				Me.EIEmployeeId=CInt(val)
			End If
			return
		case FLD_SALARY
			If Val Is DBNull.Value OrElse Val Is Nothing Then
				Me.Salary = Nothing
			Else
				Me.Salary=CDec(val)
			End If
			return
		case FLD_ADDRESS
			If Val Is DBNull.Value OrElse Val Is Nothing Then
				Me.Address = Nothing
			Else
				Me.Address=Cstr(val)
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
				Me.EmployeeInfoId = Nothing
			Else
				Me.EmployeeInfoId=CInt(val)
			End If
			return
		else if  fieldKey=STR_FLD_EIEMPLOYEEID.ToLower() Then
			If Val Is DBNull.Value OrElse Val Is Nothing Then
				Me.EIEmployeeId = Nothing
			Else
				Me.EIEmployeeId=CInt(val)
			End If
			return
		else if  fieldKey=STR_FLD_SALARY.ToLower() Then
			If Val Is DBNull.Value OrElse Val Is Nothing Then
				Me.Salary = Nothing
			Else
				Me.Salary=CDec(val)
			End If
			return
		else if  fieldKey=STR_FLD_ADDRESS.ToLower() Then
			If Val Is DBNull.Value OrElse Val Is Nothing Then
				Me.Address = Nothing
			Else
				Me.Address=Cstr(val)
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
		
			Return me.EmployeeInfoId= other.EmployeeInfoId _
				AndAlso me.EIEmployeeId.GetValueOrDefault = other.EIEmployeeId.GetValueOrDefault _
				AndAlso me.Salary.GetValueOrDefault = other.Salary.GetValueOrDefault _
				AndAlso me.Address= other.Address
				
	End Function
	
	Public Overrides Function GetHashCode() As Integer
        'using Xor has the advantage of not overflowing the integer.
        Return me.EmployeeInfoId.GetHashCode _
				Xor me.EIEmployeeId.GetHashCode _
				Xor me.Salary.GetHashCode _
				Xor me.getStringHashCode(Me.Address) 
    
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
		
		'NOTE: we can't cast from HolidayBase to Holiday, so below we 
        'instantiate a Holiday, NOT a HolidayBase object
        Dim ret as EmployeeInfo = EmployeeInfoFactory.Create()
            
				ret.EmployeeInfoId = me.EmployeeInfoId
		ret.EIEmployeeId = me.EIEmployeeId
		ret.Salary = me.Salary
		ret.Address = me.Address

		
		return ret
		
	End Function
	
	
	
#End Region

#Region "parentIdChanged"
	'below sub is called when parentIdChanged
	public Overrides Sub handleParentIdChanged(parentMo as IModelObject)
		' Assocations from BusObjects.Employee
		if (typeof parentMo is BusObjects.Employee) Then
			me.EIEmployeeId= DirectCast(parentMo, BusObjects.Employee).EmployeeId
		End If
	End Sub
#End Region


#Region "ID Property"
	
    Public Overrides Property Id() As Integer 
        Get
            return me._EmployeeInfoId
        End Get
        Set(ByVal value As Integer)
             me._EmployeeInfoId = value
             me.raiseBroadcastIdChange()
        End Set
    End Property
#End Region
	
#Region "Extra Code"
	
#End Region
	
	End Class

#Region "Req Fields validator"
    Public Class EmployeeInfoRequiredFieldsValidator
        Implements IModelObjectValidator

        Public Sub validate(ByVal imo As org.model.lib.Model.IModelObject) _
                    Implements org.model.lib.IModelObjectValidator.validate

            Dim mo As EmployeeInfo = CType(imo, EmployeeInfo)
			if mo.EIEmployeeId is Nothing then
		Throw new ModelObjectRequiredFieldException("EIEmployeeId")
End if 

			
        End Sub

    End Class
#End Region

End Namespace

