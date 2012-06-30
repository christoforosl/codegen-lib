﻿'NOTE: DO NOT ADD REFERENCES TO COM.NETU.LIB HERE, INSTEAD ADD
'THE IMPORT ON THE REFERENCES OF THE PROJECT

'<comments>
'************************************************************
' Class autogenerated on 23/09/2011 9:51:37 PM by ModelGenerator
' Extends base DBMapperBase object class
' *** DO NOT change code in this class.  
'     It will be re-generated and 
'     overwritten by the code generator ****
' Instead, change code in the extender class EmployeeDBMapper
'
'************************************************************
'</comments>

Namespace BusObjects.Mappers

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
        
        Public Shadows Function findByKey(ByVal keyval As Integer) As Employee

            Return DirectCast(MyBase.findByKey(keyval), Employee)

        End Function
        
#End Region

      
#Region "getUpdateDBCommand"
        Public Overrides Function getUpdateDBCommand(ByVal modelObj As IModelObject, _
                                                     ByVal sql As String) As IDbCommand

                                                   
             Dim p As IDataParameter = Nothing
             Dim obj as IEmployee = DirectCast(modelObj,IEmployee)
             Dim stmt As IDbCommand = Me.dbConn.getCommand(sql)


			stmt.Parameters.Add( Me.dbConn.getParameter("@EmployeeName",obj.EmployeeName))
			stmt.Parameters.Add( Me.dbConn.getParameter("@EmployeeRankId",obj.EmployeeRankId))
			stmt.Parameters.Add( Me.dbConn.getParameter("@Address",obj.Address))
			stmt.Parameters.Add( Me.dbConn.getParameter("@Telephone",obj.Telephone))
			stmt.Parameters.Add( Me.dbConn.getParameter("@Mobile",obj.Mobile))
			stmt.Parameters.Add( Me.dbConn.getParameter("@IdNumber",obj.IdNumber))
			stmt.Parameters.Add( Me.dbConn.getParameter("@SSINumber",obj.SSINumber))
			stmt.Parameters.Add( Me.dbConn.getParameter("@HireDate",obj.HireDate))
			stmt.Parameters.Add( Me.dbConn.getParameter("@NumDependents",obj.NumDependents))

			if obj.isNew = False then
			'only add primary key if we are updating and as the last parameter
							stmt.Parameters.Add( Me.dbConn.getParameter("@EmployeeId",obj.EmployeeId))
		end if


             return stmt

        End Function

#End Region

#Region "SQL Statements"
                
        Public Overrides Function getSQLStatement(ByVal skey As String) As String
			'because the EmployeeSql.xml file is stored as an Embedded resource under
			'EmployeeDBMapper file (DependentUpon in the project file), 
			'its resource name is the same as the DBMapper file.
            Return SQLStmtsRegistry.getStatement(Me.GetType.FullName, skey, Me.dbConn.sqldialect)

        End Function


#End Region

#Region "Save Children Code"
	Public overrides Sub saveChildren(mo as IModelObject )

		 dim ret as Employee = DirectCast(mo, Employee)
		If ret.EmployeeInfoLoaded = True then 
			Dim employeeinfoMapper as BusObjects.Mappers.EmployeeInfoDBMapper = new BusObjects.Mappers.EmployeeInfoDBMapper(me.DBConn())
			employeeinfoMapper.save(ret.EmployeeInfo())
		End if
		If ret.EmployeeProjectsLoaded = True then 
			Dim employeeprojectsMapper as BusObjects.Mappers.EmployeeProjectDBMapper = new BusObjects.Mappers.EmployeeProjectDBMapper(me.DBConn())
			employeeprojectsMapper.saveList(ret.EmployeeProjects())
			employeeprojectsMapper.deleteList(ret.getDeletedEmployeeProjects())
		End if
	End Sub
#End Region


	public overrides sub saveParents(mo as IModelObject)

		Dim thisMo as Employee = directCast(mo, Employee)
		if thisMo.Rank().NeedsSave() Then
			dim mappervar as BusObjects.Mappers.EmployeeRankDBMapper= new BusObjects.Mappers.EmployeeRankDBMapper(me.dbConn())
			mappervar.save(thisMo.Rank)
			thisMo.EmployeeRankId = thisMo.Rank.RankId
		end if
		
	end sub

#Region "Find functions"		
		
		'''	<summary>Given an sql statement, it opens a result set, and for each record returned, 
		'''	it creates and loads a ModelObject. </summary>
		'''	<param name="sWhereClause">where clause to be applied to "selectall" statement 
		''' that returns one or more records from the database, corresponding to the ModelObject we are going to load </param>
		'''	 <param name="params"> Parameter values to be passed to sql statement </param>
		'''	 <returns> A List(Of Employee) object containing all objects loaded </returns>
		'''	 
        Public Shadows Function findList(ByVal sWhereClause As String, _
           ByVal ParamArray params() As Object) _
           As List(Of Employee)

            Dim sql As String = Me.getSqlWithWhereClause(sWhereClause)
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
		
		Public Overrides Function getModelInstance() As IModelObject
			return EmployeeFactory.Create()
        End Function
		
    End Class

#Region " Employee Loader "
	
	Public Class EmployeeDataReaderLoader
		Inherits DataReaderLoader

			Public Overrides sub load(ByVal mo As IModelObject)

			Const DATAREADER_FLD_EMPLOYEEID as Integer = 0
			Const DATAREADER_FLD_EMPLOYEENAME as Integer = 1
			Const DATAREADER_FLD_EMPLOYEERANKID as Integer = 2
			Const DATAREADER_FLD_ADDRESS as Integer = 3
			Const DATAREADER_FLD_TELEPHONE as Integer = 4
			Const DATAREADER_FLD_MOBILE as Integer = 5
			Const DATAREADER_FLD_IDNUMBER as Integer = 6
			Const DATAREADER_FLD_SSINUMBER as Integer = 7
			Const DATAREADER_FLD_HIREDATE as Integer = 8
			Const DATAREADER_FLD_NUMDEPENDENTS as Integer = 9

			
            Dim obj As Employee = directCast(mo, Employee)
            obj.IsObjectLoading = True

			if me.reader.IsDBNull(DATAREADER_FLD_EMPLOYEEID) = false Then
				obj.EmployeeId = me.reader.getInt32(DATAREADER_FLD_EMPLOYEEID)
			End if
			if me.reader.IsDBNull(DATAREADER_FLD_EMPLOYEENAME) = false Then
				obj.EmployeeName = me.reader.GetString(DATAREADER_FLD_EMPLOYEENAME)
			End if
			if me.reader.IsDBNull(DATAREADER_FLD_EMPLOYEERANKID) = false Then
				obj.EmployeeRankId = me.reader.getInt32(DATAREADER_FLD_EMPLOYEERANKID)
			End if
			if me.reader.IsDBNull(DATAREADER_FLD_ADDRESS) = false Then
				obj.Address = me.reader.GetString(DATAREADER_FLD_ADDRESS)
			End if
			if me.reader.IsDBNull(DATAREADER_FLD_TELEPHONE) = false Then
				obj.Telephone = me.reader.GetString(DATAREADER_FLD_TELEPHONE)
			End if
			if me.reader.IsDBNull(DATAREADER_FLD_MOBILE) = false Then
				obj.Mobile = me.reader.GetString(DATAREADER_FLD_MOBILE)
			End if
			if me.reader.IsDBNull(DATAREADER_FLD_IDNUMBER) = false Then
				obj.IdNumber = me.reader.GetString(DATAREADER_FLD_IDNUMBER)
			End if
			if me.reader.IsDBNull(DATAREADER_FLD_SSINUMBER) = false Then
				obj.SSINumber = me.reader.GetString(DATAREADER_FLD_SSINUMBER)
			End if
			if me.reader.IsDBNull(DATAREADER_FLD_HIREDATE) = false Then
				obj.HireDate = me.reader.GetDateTime(DATAREADER_FLD_HIREDATE)
			End if
			if me.reader.IsDBNull(DATAREADER_FLD_NUMDEPENDENTS) = false Then
				obj.NumDependents = me.reader.getInt32(DATAREADER_FLD_NUMDEPENDENTS)
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
	Public NotInheritable Class EmployeeDataUtils

#Region "Shared ""get"" Functions "

		Public Shared Function findList(ByVal where As String, ByVal ParamArray params() As Object) _
					As List(Of Employee)

            dim dbm as EmployeeDBMapper = new EmployeeDBMapper()
            Return dbm.findList(where, params)

        End Function
		
		Public Shared Function findOne(ByVal where As String, ByVal ParamArray params() As Object) _
					As Employee

            Dim dbm As EmployeeDBMapper = New EmployeeDBMapper()
            Return DirectCast(dbm.findWhere(where, params), Employee)

        End Function
        
        
         Public Shared Function findList() As List(Of Employee)
			
			Dim ret as List(Of Employee)
			Dim dbm as EmployeeDBMapper = new EmployeeDBMapper()
			ret = dbm.findAll()
			return ret
			
        End Function
        
        Public Shared Function findByKey(id as Integer) as Employee
			
			Dim ret as Employee = Nothing
			Dim dbm as EmployeeDBMapper = new EmployeeDBMapper()
			ret = DirectCast(dbm.findByKey(id),Employee)
            
            return ret
            
        end function
        
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

        End Sub

       public shared sub deleteEmployee(ByVal EmployeeObj As Employee)
            
            dim dbm as EmployeeDBMapper = new EmployeeDBMapper()
            dbm.delete(EmployeeObj)
            
        end sub
#End Region

#Region "Data Table and data row load/save "        
        Public Shared Sub saveEmployee(ByVal dr As DataRow, _
                                                 Optional ByRef mo As Employee = Nothing)

            if mo is Nothing then 
				mo = EmployeeFactory.Create()
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
            Dim mo As IModelObject = EmployeeFactory.Create
            a.DataSource = r
            a.load(mo)
            Return DirectCast(mo, Employee)

        End Function

#End Region

    End Class
	
End Namespace

