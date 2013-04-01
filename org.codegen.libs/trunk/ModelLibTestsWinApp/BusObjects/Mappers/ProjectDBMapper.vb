﻿'NOTE: DO NOT ADD REFERENCES TO COM.NETU.LIB HERE, INSTEAD ADD
'THE IMPORT ON THE REFERENCES OF THE PROJECT

'<comments>
'************************************************************
' Class autogenerated on 01/04/2013 7:45:18 PM by ModelGenerator
' Extends base DBMapperBase object class
' *** DO NOT change code in this class.  
'     It will be re-generated and 
'     overwritten by the code generator ****
' Instead, change code in the extender class ProjectDBMapper
'
'************************************************************
'</comments>

Namespace BusObjects.Mappers

    Public Class ProjectDBMapper
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
                                          ByVal ParamArray params() As Object)  As Project
		
            return DirectCast(MyBase.findWhere(sWhereClause, params), Project)
        End Function
        

		Public Sub saveProject(ByVal mo As Project)
            MyBase.save(mo)
        End Sub
        
        Public Shadows Function findByKey(ByVal keyval As Integer) As Project

            Return DirectCast(MyBase.findByKey(keyval), Project)

        End Function
        
#End Region

      
#Region "getUpdateDBCommand"
        Public Overrides Function getUpdateDBCommand(ByVal modelObj As IModelObject, _
                                                     ByVal sql As String) As IDbCommand

                                                   
             Dim p As IDataParameter = Nothing
             Dim obj as IProject = DirectCast(modelObj,IProject)
             Dim stmt As IDbCommand = Me.dbConn.getCommand(sql)


			stmt.Parameters.Add( Me.dbConn.getParameter("@ProjectName",obj.ProjectName))
			stmt.Parameters.Add( Me.dbConn.getParameter("@isActive",obj.IsActive))

			if obj.isNew = False then
			'only add primary key if we are updating and as the last parameter
							stmt.Parameters.Add( Me.dbConn.getParameter("@ProjectId",obj.ProjectId))
		end if


             return stmt

        End Function

#End Region

#Region "SQL Statements"
                
        Public Overrides Function getSQLStatement(ByVal skey As String) As String
			'because the ProjectSql.xml file is stored as an Embedded resource under
			'ProjectDBMapper file (DependentUpon in the project file), 
			'its resource name is the same as the DBMapper file.
            Return SQLStmtsRegistry.getStatement(Me.GetType.FullName, skey, Me.dbConn.sqldialect)

        End Function


#End Region

#Region "Save Children Code"
	Public overrides Sub saveChildren(mo as IModelObject )

		 dim ret as Project = DirectCast(mo, Project)
		'***Child Association:employeeprojects
		If ret.EmployeeProjectsLoaded = True then 
			Dim employeeprojectsMapper as BusObjects.Mappers.EmployeeProjectDBMapper = new BusObjects.Mappers.EmployeeProjectDBMapper(me.DBConn())
			employeeprojectsMapper.saveList(ret.EmployeeProjects())
			employeeprojectsMapper.deleteList(ret.getDeletedEmployeeProjects())
		End if
	End Sub
#End Region




#Region "Find functions"		
		
		'''	<summary>Given an sql statement, it opens a result set, and for each record returned, 
		'''	it creates and loads a ModelObject. </summary>
		'''	<param name="sWhereClause">where clause to be applied to "selectall" statement 
		''' that returns one or more records from the database, corresponding to the ModelObject we are going to load </param>
		'''	 <param name="params"> Parameter values to be passed to sql statement </param>
		'''	 <returns> A List(Of Project) object containing all objects loaded </returns>
		'''	 
		Public shadows Function findList(ByVal sWhereClause As String, _
											ByVal ParamArray params() As Object) _
											As List(Of Project)

			dim sql as String = Me.getSqlWithWhereClause(sWhereClause)
			Dim rs As IDataReader = Nothing
			Dim molist As New List(Of Project)
						
			Try				
				rs = dbConn.getDataReaderWithParams(sql, params)
                Me.Loader.DataSource = rs
               
				Do While rs.Read
					Dim mo As IModelObject = Me.getModelInstance
					Me.Loader.load(mo)
                    molist.Add(DirectCast(mo, Project))
					
				Loop

				
			Finally
				Me.dbConn.closeDataReader(rs)
			End Try

			Return molist

		End Function
    
		'''    
		'''	 <summary>Returns all records from database for a coresponding ModelObject </summary>
		''' <returns>List(Of Project) </returns>
		Public Function findAll() As List(Of Project)
			Return Me.findList(String.Empty)
		End Function
		
		public Overrides Property Loader() As IModelObjectLoader
			Get
				if me._loader is Nothing then 
					me._loader = New ProjectDataReaderLoader
				end If
				return me._loader
            End Get
            Set(value as IModelObjectLoader)
				me._loader = value
            end Set
        End Property

#End Region
		
		Public Overrides Function getModelInstance() As IModelObject
			return ProjectFactory.Create()
        End Function
		
    End Class

#Region " Project Loader "
	
	Public Class ProjectDataReaderLoader
		Inherits DataReaderLoader

			Public Overrides sub load(ByVal mo As IModelObject)

			Const DATAREADER_FLD_PROJECTID as Integer = 0
			Const DATAREADER_FLD_PROJECTNAME as Integer = 1
			Const DATAREADER_FLD_ISACTIVE as Integer = 2

			
            Dim obj As Project = directCast(mo, Project)
            obj.IsObjectLoading = True

			if me.reader.IsDBNull(DATAREADER_FLD_PROJECTID) = false Then
				obj.ProjectId = me.reader.getInt32(DATAREADER_FLD_PROJECTID)
			End if
			if me.reader.IsDBNull(DATAREADER_FLD_PROJECTNAME) = false Then
				obj.ProjectName = me.reader.GetString(DATAREADER_FLD_PROJECTNAME)
			End if
			if me.reader.IsDBNull(DATAREADER_FLD_ISACTIVE) = false Then
				obj.IsActive = CBool(me.reader.getInt32(DATAREADER_FLD_ISACTIVE))
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
	Public NotInheritable Class ProjectDataUtils

#Region "Shared ""get"" Functions "

		Public Shared Function findList(ByVal where As String, ByVal ParamArray params() As Object) _
					As List(Of Project)

            dim dbm as ProjectDBMapper = new ProjectDBMapper()
            Return dbm.findList(where, params)

        End Function
		
		Public Shared Function findOne(ByVal where As String, ByVal ParamArray params() As Object) _
					As Project

            Dim dbm As ProjectDBMapper = New ProjectDBMapper()
            Return DirectCast(dbm.findWhere(where, params), Project)

        End Function
        
        
         Public Shared Function findList() As List(Of Project)
			
			return new ProjectDBMapper().findAll()
			
        End Function
        
        Public Shared Function findByKey(id as Integer) as Project
			
			return DirectCast( new ProjectDBMapper().findByKey(id),Project)
                        
        end function
        
		''' <summary>
        ''' Reload the Project from the database
        ''' </summary>
        ''' <remarks>use this method when you want to relad the Project from the database, discarding any changes</remarks>
		Public Shared Sub reload(ByVal mo As Project)

            mo = DirectCast(New ProjectDBMapper().findByKey(mo.Id), Project)
            
        End Sub

#End Region

#Region "Shared Save and Delete Functions"
		''' <summary>
        ''' Convinience method to save a Project Object.
        ''' Important note: DO NOT CALL THIS IN A LOOP!
        ''' </summary>
        ''' <param name="ProjectObj"></param>
        ''' <remarks>
		''' Important note: DO NOT CALL THIS IN A LOOP!  
		''' This method simply instantiates a ProjectDBMapper and calls the save method
		''' </remarks>
		public shared sub saveProject(ByVal ParamArray ProjectObj() As Project)
            
            dim dbm as ProjectDBMapper = new ProjectDBMapper()
            dbm.saveList(ProjectObj.ToList)

                       
       end sub
       

       public shared sub deleteProject(ByVal ProjectObj As Project)
            
            dim dbm as ProjectDBMapper = new ProjectDBMapper()
            dbm.delete(ProjectObj)
            
        end sub
#End Region

#Region "Data Table and data row load/save "        
        Public Shared Sub saveProject(ByVal dr As DataRow, _
                                                 Optional ByRef mo As Project = Nothing)

            if mo is Nothing then 
				mo = ProjectFactory.Create()
			end if
			
            For Each dc As DataColumn In dr.Table.Columns
                mo.setAttribute(dc.ColumnName, dr.Item(dc.ColumnName))
            Next

            call saveProject(mo)

        End Sub
        
         
        Public Shared Sub saveProject(ByVal dt As DataTable, _
                                                 Optional ByRef mo As Project = Nothing)

            For Each dr As DataRow In dt.Rows
            	call saveProject(dr,mo)
            Next
			
        End Sub
        
		 Public Shared Function loadFromDataRow(ByVal r As DataRow) As Project

            Dim a As New DataRowLoader
            Dim mo As IModelObject = ProjectFactory.Create
            a.DataSource = r
            a.load(mo)
            Return DirectCast(mo, Project)

        End Function

#End Region

    End Class
	
End Namespace

