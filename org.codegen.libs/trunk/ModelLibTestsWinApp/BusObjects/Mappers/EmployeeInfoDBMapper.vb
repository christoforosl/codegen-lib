﻿'NOTE: DO NOT ADD REFERENCES TO COM.NETU.LIB HERE, INSTEAD ADD
'THE IMPORT ON THE REFERENCES OF THE PROJECT

'<comments>
'************************************************************
' Class autogenerated on 30/06/2012 5:15:14 PM by ModelGenerator
' Extends base DBMapperBase object class
' *** DO NOT change code in this class.  
'     It will be re-generated and 
'     overwritten by the code generator ****
' Instead, change code in the extender class EmployeeInfoDBMapper
'
'************************************************************
'</comments>

Namespace BusObjects.Mappers

    Public Class EmployeeInfoDBMapper
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
                                          ByVal ParamArray params() As Object)  As EmployeeInfo
		
            return DirectCast(MyBase.findWhere(sWhereClause, params), EmployeeInfo)
        End Function
        

		Public Sub saveEmployeeInfo(ByVal mo As EmployeeInfo)
            MyBase.save(mo)
        End Sub
        
        Public Shadows Function findByKey(ByVal keyval As Integer) As EmployeeInfo

            Return DirectCast(MyBase.findByKey(keyval), EmployeeInfo)

        End Function
        
#End Region

      
#Region "getUpdateDBCommand"
        Public Overrides Function getUpdateDBCommand(ByVal modelObj As IModelObject, _
                                                     ByVal sql As String) As IDbCommand

                                                   
             Dim p As IDataParameter = Nothing
             Dim obj as IEmployeeInfo = DirectCast(modelObj,IEmployeeInfo)
             Dim stmt As IDbCommand = Me.dbConn.getCommand(sql)


			stmt.Parameters.Add( Me.dbConn.getParameter("@EIEmployeeId",obj.EIEmployeeId))
			stmt.Parameters.Add( Me.dbConn.getParameter("@Salary",obj.Salary))
			stmt.Parameters.Add( Me.dbConn.getParameter("@Address",obj.Address))

			if obj.isNew = False then
			'only add primary key if we are updating and as the last parameter
							stmt.Parameters.Add( Me.dbConn.getParameter("@EmployeeInfoId",obj.EmployeeInfoId))
		end if


             return stmt

        End Function

#End Region

#Region "SQL Statements"
                
        Public Overrides Function getSQLStatement(ByVal skey As String) As String
			'because the EmployeeInfoSql.xml file is stored as an Embedded resource under
			'EmployeeInfoDBMapper file (DependentUpon in the project file), 
			'its resource name is the same as the DBMapper file.
            Return SQLStmtsRegistry.getStatement(Me.GetType.FullName, skey, Me.dbConn.sqldialect)

        End Function


#End Region





#Region "Find functions"		
		
		'''	<summary>Given an sql statement, it opens a result set, and for each record returned, 
		'''	it creates and loads a ModelObject. </summary>
		'''	<param name="sWhereClause">where clause to be applied to "selectall" statement 
		''' that returns one or more records from the database, corresponding to the ModelObject we are going to load </param>
		'''	 <param name="params"> Parameter values to be passed to sql statement </param>
		'''	 <returns> A List(Of EmployeeInfo) object containing all objects loaded </returns>
		'''	 
		Public shadows Function findList(ByVal sWhereClause As String, _
											ByVal ParamArray params() As Object) _
											As List(Of EmployeeInfo)

			dim sql as String = Me.getSqlWithWhereClause(sWhereClause)
			Dim rs As IDataReader = Nothing
			Dim molist As New List(Of EmployeeInfo)
						
			Try				
				rs = dbConn.getDataReaderWithParams(sql, params)
                Me.Loader.DataSource = rs
               
				Do While rs.Read
					Dim mo As IModelObject = Me.getModelInstance
					Me.Loader.load(mo)
                    molist.Add(DirectCast(mo, EmployeeInfo))
					
				Loop

				
			Finally
				Me.dbConn.closeDataReader(rs)
			End Try

			Return molist

		End Function
    
		'''    
		'''	 <summary>Returns all records from database for a coresponding ModelObject </summary>
		''' <returns>List(Of EmployeeInfo) </returns>
		Public Function findAll() As List(Of EmployeeInfo)
			Return Me.findList(String.Empty)
		End Function
		
		public Overrides Property Loader() As IModelObjectLoader
			Get
				if me._loader is Nothing then 
					me._loader = New EmployeeInfoDataReaderLoader
				end If
				return me._loader
            End Get
            Set(value as IModelObjectLoader)
				me._loader = value
            end Set
        End Property

#End Region
		
		Public Overrides Function getModelInstance() As IModelObject
			return EmployeeInfoFactory.Create()
        End Function
		
    End Class

#Region " EmployeeInfo Loader "
	
	Public Class EmployeeInfoDataReaderLoader
		Inherits DataReaderLoader

			Public Overrides sub load(ByVal mo As IModelObject)

			Const DATAREADER_FLD_EMPLOYEEINFOID as Integer = 0
			Const DATAREADER_FLD_EIEMPLOYEEID as Integer = 1
			Const DATAREADER_FLD_SALARY as Integer = 2
			Const DATAREADER_FLD_ADDRESS as Integer = 3

			
            Dim obj As EmployeeInfo = directCast(mo, EmployeeInfo)
            obj.IsObjectLoading = True

			if me.reader.IsDBNull(DATAREADER_FLD_EMPLOYEEINFOID) = false Then
				obj.EmployeeInfoId = me.reader.getInt32(DATAREADER_FLD_EMPLOYEEINFOID)
			End if
			if me.reader.IsDBNull(DATAREADER_FLD_EIEMPLOYEEID) = false Then
				obj.EIEmployeeId = me.reader.getInt32(DATAREADER_FLD_EIEMPLOYEEID)
			End if
			if me.reader.IsDBNull(DATAREADER_FLD_SALARY) = false Then
				obj.Salary = me.reader.getDecimal(DATAREADER_FLD_SALARY)
			End if
			if me.reader.IsDBNull(DATAREADER_FLD_ADDRESS) = false Then
				obj.Address = me.reader.GetString(DATAREADER_FLD_ADDRESS)
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
	Public NotInheritable Class EmployeeInfoDataUtils

#Region "Shared ""get"" Functions "

		Public Shared Function findList(ByVal where As String, ByVal ParamArray params() As Object) _
					As List(Of EmployeeInfo)

            dim dbm as EmployeeInfoDBMapper = new EmployeeInfoDBMapper()
            Return dbm.findList(where, params)

        End Function
		
		Public Shared Function findOne(ByVal where As String, ByVal ParamArray params() As Object) _
					As EmployeeInfo

            Dim dbm As EmployeeInfoDBMapper = New EmployeeInfoDBMapper()
            Return DirectCast(dbm.findWhere(where, params), EmployeeInfo)

        End Function
        
        
         Public Shared Function findList() As List(Of EmployeeInfo)
			
			Dim ret as List(Of EmployeeInfo)
			Dim dbm as EmployeeInfoDBMapper = new EmployeeInfoDBMapper()
			ret = dbm.findAll()
			return ret
			
        End Function
        
        Public Shared Function findByKey(id as Integer) as EmployeeInfo
			
			Dim ret as EmployeeInfo = Nothing
			Dim dbm as EmployeeInfoDBMapper = new EmployeeInfoDBMapper()
			ret = DirectCast(dbm.findByKey(id),EmployeeInfo)
            
            return ret
            
        end function
        
#End Region

#Region "Shared Save and Delete Functions"
		''' <summary>
        ''' Convinience method to save a EmployeeInfo Object.
        ''' Important note: DO NOT CALL THIS IN A LOOP!
        ''' </summary>
        ''' <param name="EmployeeInfoObj"></param>
        ''' <remarks>
		''' Important note: DO NOT CALL THIS IN A LOOP!  
		''' This method simply instantiates a EmployeeInfoDBMapper and calls the save method
		''' </remarks>
		public shared sub saveEmployeeInfo(ByVal ParamArray EmployeeInfoObj() As EmployeeInfo)
            
            dim dbm as EmployeeInfoDBMapper = new EmployeeInfoDBMapper()
            dbm.saveList(EmployeeInfoObj.ToList)

                       
       end sub
       

       public shared sub deleteEmployeeInfo(ByVal EmployeeInfoObj As EmployeeInfo)
            
            dim dbm as EmployeeInfoDBMapper = new EmployeeInfoDBMapper()
            dbm.delete(EmployeeInfoObj)
            
        end sub
#End Region

#Region "Data Table and data row load/save "        
        Public Shared Sub saveEmployeeInfo(ByVal dr As DataRow, _
                                                 Optional ByRef mo As EmployeeInfo = Nothing)

            if mo is Nothing then 
				mo = EmployeeInfoFactory.Create()
			end if
			
            For Each dc As DataColumn In dr.Table.Columns
                mo.setAttribute(dc.ColumnName, dr.Item(dc.ColumnName))
            Next

            call saveEmployeeInfo(mo)

        End Sub
        
         
        Public Shared Sub saveEmployeeInfo(ByVal dt As DataTable, _
                                                 Optional ByRef mo As EmployeeInfo = Nothing)

            For Each dr As DataRow In dt.Rows
            	call saveEmployeeInfo(dr,mo)
            Next
			
        End Sub
        
		 Public Shared Function loadFromDataRow(ByVal r As DataRow) As EmployeeInfo

            Dim a As New DataRowLoader
            Dim mo As IModelObject = EmployeeInfoFactory.Create
            a.DataSource = r
            a.load(mo)
            Return DirectCast(mo, EmployeeInfo)

        End Function

#End Region

    End Class
	
End Namespace

