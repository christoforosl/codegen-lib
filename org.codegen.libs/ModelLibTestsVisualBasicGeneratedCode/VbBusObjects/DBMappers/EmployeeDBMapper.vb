﻿'NOTE: DO NOT ADD REFERENCES TO COM.NETU.LIB HERE, INSTEAD ADD
'THE IMPORT ON THE REFERENCES OF THE PROJECT

'<comments>
'Template: DBMapperBase.visualBasic.txt
'************************************************************
' Class autogenerated on 01/06/2015 7:02:06 PM by ModelGenerator
' Extends base DBMapperBase object class
' *** DO NOT change code in this class.  
'     It will be re-generated and 
'     overwritten by the code generator ****
' Instead, change code in the extender class EmployeeDBMapper
'
'************************************************************
'</comments>

Namespace VbBusObjects.DBMappers
	<System.Runtime.InteropServices.ComVisible(False)> _
    Public Class EmployeeDBMapper
        Inherits DBMapper
		
#Region "Constructors "
    Public Sub New(ByVal _dbConn As DBUtils)
        MyBase.new(_dbConn)
    End Sub
        
        
    Public Sub New()
        MyBase.new()
    End Sub
#End Region

#Region "Overloaded Functions"
		
	Public Shadows Function findWhere(ByVal sWhereClause As String, _
                                        ByVal ParamArray params() As Object)  As Employee
		
        return DirectCast(MyBase.findWhere(sWhereClause, params), Employee)
    End Function
        

	Public Sub saveEmployee(ByVal mo As Employee)
        MyBase.save(mo)
    End Sub
        
    Public Shadows Function findByKey(ByVal keyval As object) As Employee

        Return DirectCast(MyBase.findByKey(keyval), Employee)

    End Function
        
#End Region

#Region "getUpdateDBCommand"
        Public Overrides Function getUpdateDBCommand(ByVal modelObj As IModelObject, ByVal sql As String) As IDbCommand

             Dim p As IDataParameter = Nothing
             Dim obj as Employee = DirectCast(modelObj,Employee)
             Dim stmt As IDbCommand = Me.dbConn.getCommand(sql)
				stmt.Parameters.Add(Me.dbConn.getParameter("@EmployeeName",obj.PrEmployeeName))
				stmt.Parameters.Add(Me.dbConn.getParameter("@EmployeeRankId",obj.PrEmployeeRankId))
				stmt.Parameters.Add(Me.dbConn.getParameter("@Salary",obj.PrSalary))
				stmt.Parameters.Add(Me.dbConn.getParameter("@Address",obj.PrAddress))
				stmt.Parameters.Add(Me.dbConn.getParameter("@Telephone",obj.PrTelephone))
				stmt.Parameters.Add(Me.dbConn.getParameter("@Mobile",obj.PrMobile))
				stmt.Parameters.Add(Me.dbConn.getParameter("@IdNumber",obj.PrIdNumber))
				stmt.Parameters.Add(Me.dbConn.getParameter("@SSINumber",obj.PrSSINumber))
				stmt.Parameters.Add(Me.dbConn.getParameter("@HireDate",obj.PrHireDate))
				stmt.Parameters.Add(Me.dbConn.getParameter("@NumDependents",obj.PrNumDependents))
				stmt.Parameters.Add(Me.dbConn.getParameter("@EmployeeTypeCode",obj.PrEmployeeTypeCode))
				stmt.Parameters.Add(Me.dbConn.getParameter("@createDate",obj.CreateDate))
				stmt.Parameters.Add(Me.dbConn.getParameter("@updateDate",obj.UpdateDate))
				stmt.Parameters.Add(Me.dbConn.getParameter("@createUser",obj.CreateUser))
				stmt.Parameters.Add(Me.dbConn.getParameter("@updateUser",obj.UpdateUser))
				stmt.Parameters.Add(Me.dbConn.getParameter("@sampleGuidField",obj.PrSampleGuidField))
				stmt.Parameters.Add(Me.dbConn.getParameter("@isActive",obj.PrIsActive))
				stmt.Parameters.Add(Me.dbConn.getParameter("@sampleBigInt",obj.PrSampleBigInt))
				stmt.Parameters.Add(Me.dbConn.getParameter("@sampleSmallInt",obj.PrSampleSmallInt))
				stmt.Parameters.Add(Me.dbConn.getParameter("@sampleNumericFieldInt",obj.PrSampleNumericFieldInt))
				stmt.Parameters.Add(Me.dbConn.getParameter("@sampleNumericField2Decimals",obj.PrSampleNumericField2Decimals))

			if obj.isNew Then
			Else
			'only add primary key if we are updating and as the last parameter
				stmt.Parameters.Add(Me.dbConn.getParameter("@EmployeeId",obj.PrEmployeeId))

			End if '

             return stmt

        End Function

#End Region
#Region "Save Children Code"
	Public overrides Sub saveChildren(mo as IModelObject )

		 dim ret as Employee = DirectCast(mo, Employee)
		'***Child Association:employeeinfo
		If ret._EmployeeInfoLoaded = True then 
			Dim employeeinfoMapper as VbBusObjects.DBMappers.EmployeeInfoDBMapper = new VbBusObjects.DBMappers.EmployeeInfoDBMapper(me.DBConn())
			employeeinfoMapper.save(ret.PrEmployeeInfo())
		End if
		'***Child Association:employeeprojects
		If ret._EmployeeProjectsLoaded = True then 
			Dim employeeprojectsMapper as VbBusObjects.DBMappers.EmployeeProjectDBMapper = new VbBusObjects.DBMappers.EmployeeProjectDBMapper(me.DBConn())
			employeeprojectsMapper.saveList(ret.PrEmployeeProjects())
			employeeprojectsMapper.deleteList(ret.PrEmployeeProjectsGetDeleted())
		End if
	End Sub
#End Region
	public overrides sub saveParents(mo as IModelObject)

		Dim thisMo as Employee = directCast(mo, Employee)
		'*** Parent Association:rank
		if (thisMo.PrRank is Nothing=false) AndAlso thisMo.PrRank().NeedsSave() Then
			Dim mappervar as VbBusObjects.DBMappers.EmployeeRankDBMapper= new VbBusObjects.DBMappers.EmployeeRankDBMapper(me.dbConn)
			mappervar.save(thisMo.PrRank)
			thisMo.PrEmployeeRankId = thisMo.PrRank.PrRankId
		end if
		
	end sub
#Region "Find functions"

	'''	<summary>Given an sql statement, it opens a result set, and for each record returned, it creates and loads a ModelObject. </summary>
	'''	<param name="sWhereClause">where clause to be applied to "selectall" statement 
	''' that returns one or more records from the database, corresponding to the ModelObject we are going to load </param>
	'''	<param name="params"> Parameter values to be passed to sql statement </param>
	'''	<returns> A List(Of Employee) object containing all objects loaded </returns>
	'''	 
	Public shadows Function findList(ByVal sWhereClause As String, _
										ByVal ParamArray params() As Object) _
										As List(Of Employee)

		dim sql as String = Me.getSqlWithWhereClause(sWhereClause)
		Dim rs As IDataReader = Nothing
		Dim molist As New List(Of Employee)
						
		Try				
			rs = dbConn.getDataReaderWithParams(sql, params)
            Me.Loader.DataSource = rs
               
			Do While rs.Read
				Dim mo As IModelObject = Me.getModelInstance
				Me.Loader.load(mo)
                molist.Add(DirectCast(mo, Employee))
					
			Loop

				
		Finally
			Me.dbConn.closeDataReader(rs)
		End Try

		Return molist

	End Function
    
	Public Shadows Function findList(ByVal sWhereClause As String, _
        ByVal params As List(Of IDataParameter)) _
        As List(Of Employee)

        Dim sql As String = Me.getSqlWithWhereClause(sWhereClause)
        Dim rs As IDataReader = Nothing
        Dim molist As New List(Of Employee)

        Try
            rs = dbConn.getDataReader(sql, params)
            Me.Loader.DataSource = rs

            Do While rs.Read
                Dim mo As IModelObject = Me.getModelInstance
                Me.Loader.load(mo)
                molist.Add(DirectCast(mo, Employee))

            Loop


        Finally
            Me.dbConn.closeDataReader(rs)
        End Try

        Return molist

    End Function

	'''    
	'''	 <summary>Returns all records from database for a coresponding ModelObject </summary>
	''' <returns>List(Of Employee) </returns>
	Public Function findAll() As List(Of Employee)
		Return Me.findList(String.Empty)
	End Function
		
	public Overrides Property Loader() As IModelObjectLoader
		Get
			if me._loader is Nothing then 
				me._loader = New EmployeeDataReaderLoader
			end If
			return me._loader
        End Get
        Set(value as IModelObjectLoader)
			me._loader = value
        end Set
    End Property

#End Region

	protected Overrides Function pkFieldName() As String
		return "EmployeeId"
    End Function
		
	Public Overrides Function getModelInstance() As IModelObject
		return new Employee()
    End Function
		
End Class

#Region " Employee Loader "
	<System.Runtime.InteropServices.ComVisible(False)> _
	Public Class EmployeeDataReaderLoader
		Inherits DataReaderLoader

			Public Overrides sub load(ByVal mo As IModelObject)

			Const DATAREADER_FLD_EMPLOYEEID as Integer = 0
			Const DATAREADER_FLD_EMPLOYEENAME as Integer = 1
			Const DATAREADER_FLD_EMPLOYEERANKID as Integer = 2
			Const DATAREADER_FLD_SALARY as Integer = 3
			Const DATAREADER_FLD_ADDRESS as Integer = 4
			Const DATAREADER_FLD_TELEPHONE as Integer = 5
			Const DATAREADER_FLD_MOBILE as Integer = 6
			Const DATAREADER_FLD_IDNUMBER as Integer = 7
			Const DATAREADER_FLD_SSINUMBER as Integer = 8
			Const DATAREADER_FLD_HIREDATE as Integer = 9
			Const DATAREADER_FLD_NUMDEPENDENTS as Integer = 10
			Const DATAREADER_FLD_EMPLOYEETYPECODE as Integer = 11
			Const DATAREADER_FLD_CREATEDATE as Integer = 12
			Const DATAREADER_FLD_UPDATEDATE as Integer = 13
			Const DATAREADER_FLD_CREATEUSER as Integer = 14
			Const DATAREADER_FLD_UPDATEUSER as Integer = 15
			Const DATAREADER_FLD_SAMPLEGUIDFIELD as Integer = 16
			Const DATAREADER_FLD_ISACTIVE as Integer = 17
			Const DATAREADER_FLD_SAMPLEBIGINT as Integer = 18
			Const DATAREADER_FLD_SAMPLESMALLINT as Integer = 19
			Const DATAREADER_FLD_SAMPLENUMERICFIELDINT as Integer = 20
			Const DATAREADER_FLD_SAMPLENUMERICFIELD2DECIMALS as Integer = 21

			
            Dim obj As Employee = directCast(mo, Employee)
            obj.IsObjectLoading = True

			if me.reader.IsDBNull(DATAREADER_FLD_EMPLOYEEID) = false Then
				obj.PrEmployeeId = me.reader.GetInt32(DATAREADER_FLD_EMPLOYEEID)
			End if
			if me.reader.IsDBNull(DATAREADER_FLD_EMPLOYEENAME) = false Then
				obj.PrEmployeeName = me.reader.GetString(DATAREADER_FLD_EMPLOYEENAME)
			End if
			if me.reader.IsDBNull(DATAREADER_FLD_EMPLOYEERANKID) = false Then
				obj.PrEmployeeRankId = me.reader.GetInt32(DATAREADER_FLD_EMPLOYEERANKID)
			End if
			if me.reader.IsDBNull(DATAREADER_FLD_SALARY) = false Then
				obj.PrSalary = me.reader.GetDecimal(DATAREADER_FLD_SALARY)
			End if
			if me.reader.IsDBNull(DATAREADER_FLD_ADDRESS) = false Then
				obj.PrAddress = me.reader.GetString(DATAREADER_FLD_ADDRESS)
			End if
			if me.reader.IsDBNull(DATAREADER_FLD_TELEPHONE) = false Then
				obj.PrTelephone = me.reader.GetString(DATAREADER_FLD_TELEPHONE)
			End if
			if me.reader.IsDBNull(DATAREADER_FLD_MOBILE) = false Then
				obj.PrMobile = me.reader.GetString(DATAREADER_FLD_MOBILE)
			End if
			if me.reader.IsDBNull(DATAREADER_FLD_IDNUMBER) = false Then
				obj.PrIdNumber = me.reader.GetString(DATAREADER_FLD_IDNUMBER)
			End if
			if me.reader.IsDBNull(DATAREADER_FLD_SSINUMBER) = false Then
				obj.PrSSINumber = me.reader.GetString(DATAREADER_FLD_SSINUMBER)
			End if
			if me.reader.IsDBNull(DATAREADER_FLD_HIREDATE) = false Then
				obj.PrHireDate = me.reader.GetDateTime(DATAREADER_FLD_HIREDATE)
			End if
			if me.reader.IsDBNull(DATAREADER_FLD_NUMDEPENDENTS) = false Then
				obj.PrNumDependents = me.reader.GetInt32(DATAREADER_FLD_NUMDEPENDENTS)
			End if
			if me.reader.IsDBNull(DATAREADER_FLD_EMPLOYEETYPECODE) = false Then
				obj.PrEmployeeTypeCode = me.reader.GetString(DATAREADER_FLD_EMPLOYEETYPECODE)
			End if
			if me.reader.IsDBNull(DATAREADER_FLD_CREATEDATE) = false Then
				obj.CreateDate = me.reader.GetDateTime(DATAREADER_FLD_CREATEDATE)
			End if
			if me.reader.IsDBNull(DATAREADER_FLD_UPDATEDATE) = false Then
				obj.UpdateDate = me.reader.GetDateTime(DATAREADER_FLD_UPDATEDATE)
			End if
			if me.reader.IsDBNull(DATAREADER_FLD_CREATEUSER) = false Then
				obj.CreateUser = me.reader.GetString(DATAREADER_FLD_CREATEUSER)
			End if
			if me.reader.IsDBNull(DATAREADER_FLD_UPDATEUSER) = false Then
				obj.UpdateUser = me.reader.GetString(DATAREADER_FLD_UPDATEUSER)
			End if
			if me.reader.IsDBNull(DATAREADER_FLD_SAMPLEGUIDFIELD) = false Then
				obj.PrSampleGuidField = me.reader.GetGuid(DATAREADER_FLD_SAMPLEGUIDFIELD)
			End if
			if me.reader.IsDBNull(DATAREADER_FLD_ISACTIVE) = false Then
				obj.PrIsActive = me.reader.GetBoolean(DATAREADER_FLD_ISACTIVE)
			End if
			if me.reader.IsDBNull(DATAREADER_FLD_SAMPLEBIGINT) = false Then
				obj.PrSampleBigInt = me.reader.GetInt64(DATAREADER_FLD_SAMPLEBIGINT)
			End if
			if me.reader.IsDBNull(DATAREADER_FLD_SAMPLESMALLINT) = false Then
				obj.PrSampleSmallInt = me.reader.GetInt16(DATAREADER_FLD_SAMPLESMALLINT)
			End if
			if me.reader.IsDBNull(DATAREADER_FLD_SAMPLENUMERICFIELDINT) = false Then
				obj.PrSampleNumericFieldInt = me.reader.GetInt64(DATAREADER_FLD_SAMPLENUMERICFIELDINT)
			End if
			if me.reader.IsDBNull(DATAREADER_FLD_SAMPLENUMERICFIELD2DECIMALS) = false Then
				obj.PrSampleNumericField2Decimals = me.reader.GetDecimal(DATAREADER_FLD_SAMPLENUMERICFIELD2DECIMALS)
			End if

            
            obj.isNew = false ' since we've just loaded from database, we mark as "old"
            obj.isDirty = False
			obj.IsObjectLoading = False
			obj.afterLoad()

            return
            
        End sub
	
	End Class

#End Region
		
	'''<summary>
    ''' Final Class with convinience shared methods for loading/saving the EmployeeRank ModelObject. 
    '''</summary>
	<System.Runtime.InteropServices.ComVisible(False)> _
	Public NotInheritable Class EmployeeDataUtils

#Region "Shared ""get"" Functions "

		Public Shared Function findList(ByVal where As String, ByVal ParamArray params() As Object) _
					As List(Of Employee)

            dim dbm as EmployeeDBMapper = new EmployeeDBMapper()
            Return dbm.findList(where, params)

        End Function
		
		Public Shared Function findList(ByVal where As String, ByVal params As List(Of IDataParameter)) _
										As List(Of Employee)

            Dim dbm As EmployeeDBMapper = New EmployeeDBMapper()
            Return dbm.findList(where, params)

        End Function

		Public Shared Function findOne(ByVal where As String, ByVal ParamArray params() As Object) _
					As Employee

            Dim dbm As EmployeeDBMapper = New EmployeeDBMapper()
            Return DirectCast(dbm.findWhere(where, params), Employee)

        End Function
        
        
         Public Shared Function findList() As List(Of Employee)
			
			return new EmployeeDBMapper().findAll()
			
        End Function
        
        Public Shared Function findByKey(id as object) as Employee
			
			return DirectCast( new EmployeeDBMapper().findByKey(id),Employee)
                        
        end function
        
		''' <summary>
        ''' Reload the Employee from the database
        ''' </summary>
        ''' <remarks>
		''' use this method when you want to relad the Employee 
		''' from the database, discarding any changes
		''' </remarks>
		Public Shared Sub reload(ByRef mo As Employee)
			
			If mo Is Nothing Then
                Throw New System.ArgumentNullException("null object past to reload function")
            End If
            
			mo = DirectCast(New EmployeeDBMapper().findByKey(mo.Id), Employee)
            
        End Sub

#End Region

#Region "Shared Save and Delete Functions"
		''' <summary>
        ''' Convinience method to save a Employee Object.
        ''' Important note: DO NOT CALL THIS IN A LOOP!
        ''' </summary>
        ''' <param name="EmployeeObj"></param>
        ''' <remarks>
		''' Important note: DO NOT CALL THIS IN A LOOP!  
		''' This method simply instantiates a EmployeeDBMapper and calls the save method
		''' </remarks>
		public shared sub saveEmployee(ByVal ParamArray EmployeeObj() As Employee)
            
            dim dbm as EmployeeDBMapper = new EmployeeDBMapper()
            dbm.saveList(EmployeeObj.ToList)

                       
       end sub
       

       public shared sub deleteEmployee(ByVal EmployeeObj As Employee)
            
            dim dbm as EmployeeDBMapper = new EmployeeDBMapper()
            dbm.delete(EmployeeObj)
            
        end sub
#End Region

#Region "Data Table and data row load/save "        
        Public Shared Sub saveEmployee(ByVal dr As DataRow, _
                                                 Optional ByRef mo As Employee = Nothing)

            if mo is Nothing then 
				mo = new Employee()
			end if
			
            For Each dc As DataColumn In dr.Table.Columns
                mo.setAttribute(dc.ColumnName, dr.Item(dc.ColumnName))
            Next

            call saveEmployee(mo)

        End Sub
        
         
        Public Shared Sub saveEmployee(ByVal dt As DataTable, _
                                                 Optional ByRef mo As Employee = Nothing)

            For Each dr As DataRow In dt.Rows
            	call saveEmployee(dr,mo)
            Next
			
        End Sub
        
		 Public Shared Function loadFromDataRow(ByVal r As DataRow) As Employee

            Dim a As New DataRowLoader
            Dim mo As IModelObject = new Employee()
            a.DataSource = r
            a.load(mo)
            Return DirectCast(mo, Employee)

        End Function

#End Region

    End Class
	
End Namespace

