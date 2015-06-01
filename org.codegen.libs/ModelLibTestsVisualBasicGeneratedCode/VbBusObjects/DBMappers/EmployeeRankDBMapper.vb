﻿'NOTE: DO NOT ADD REFERENCES TO COM.NETU.LIB HERE, INSTEAD ADD
'THE IMPORT ON THE REFERENCES OF THE PROJECT

'<comments>
'Template: DBMapperBase.visualBasic.txt
'************************************************************
' Class autogenerated on 31/05/2015 11:00:37 PM by ModelGenerator
' Extends base DBMapperBase object class
' *** DO NOT change code in this class.  
'     It will be re-generated and 
'     overwritten by the code generator ****
' Instead, change code in the extender class EmployeeRankDBMapper
'
'************************************************************
'</comments>

Namespace VbBusObjects.DBMappers
	<System.Runtime.InteropServices.ComVisible(False)> _
    Public Class EmployeeRankDBMapper
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
                                        ByVal ParamArray params() As Object)  As EmployeeRank
		
        return DirectCast(MyBase.findWhere(sWhereClause, params), EmployeeRank)
    End Function
        

	Public Sub saveEmployeeRank(ByVal mo As EmployeeRank)
        MyBase.save(mo)
    End Sub
        
    Public Shadows Function findByKey(ByVal keyval As object) As EmployeeRank

        Return DirectCast(MyBase.findByKey(keyval), EmployeeRank)

    End Function
        
#End Region

#Region "getUpdateDBCommand"
        Public Overrides Function getUpdateDBCommand(ByVal modelObj As IModelObject, ByVal sql As String) As IDbCommand

             Dim p As IDataParameter = Nothing
             Dim obj as EmployeeRank = DirectCast(modelObj,EmployeeRank)
             Dim stmt As IDbCommand = Me.dbConn.getCommand(sql)
				stmt.Parameters.Add(Me.dbConn.getParameter("@Rank",obj.PrRank))

			if obj.isNew Then
			Else
			'only add primary key if we are updating and as the last parameter
				stmt.Parameters.Add(Me.dbConn.getParameter("@RankId",obj.PrRankId))

			End if '

             return stmt

        End Function

#End Region

#Region "Find functions"

	'''	<summary>Given an sql statement, it opens a result set, and for each record returned, it creates and loads a ModelObject. </summary>
	'''	<param name="sWhereClause">where clause to be applied to "selectall" statement 
	''' that returns one or more records from the database, corresponding to the ModelObject we are going to load </param>
	'''	<param name="params"> Parameter values to be passed to sql statement </param>
	'''	<returns> A List(Of EmployeeRank) object containing all objects loaded </returns>
	'''	 
	Public shadows Function findList(ByVal sWhereClause As String, _
										ByVal ParamArray params() As Object) _
										As List(Of EmployeeRank)

		dim sql as String = Me.getSqlWithWhereClause(sWhereClause)
		Dim rs As IDataReader = Nothing
		Dim molist As New List(Of EmployeeRank)
						
		Try				
			rs = dbConn.getDataReaderWithParams(sql, params)
            Me.Loader.DataSource = rs
               
			Do While rs.Read
				Dim mo As IModelObject = Me.getModelInstance
				Me.Loader.load(mo)
                molist.Add(DirectCast(mo, EmployeeRank))
					
			Loop

				
		Finally
			Me.dbConn.closeDataReader(rs)
		End Try

		Return molist

	End Function
    
	Public Shadows Function findList(ByVal sWhereClause As String, _
        ByVal params As List(Of IDataParameter)) _
        As List(Of EmployeeRank)

        Dim sql As String = Me.getSqlWithWhereClause(sWhereClause)
        Dim rs As IDataReader = Nothing
        Dim molist As New List(Of EmployeeRank)

        Try
            rs = dbConn.getDataReader(sql, params)
            Me.Loader.DataSource = rs

            Do While rs.Read
                Dim mo As IModelObject = Me.getModelInstance
                Me.Loader.load(mo)
                molist.Add(DirectCast(mo, EmployeeRank))

            Loop


        Finally
            Me.dbConn.closeDataReader(rs)
        End Try

        Return molist

    End Function

	'''    
	'''	 <summary>Returns all records from database for a coresponding ModelObject </summary>
	''' <returns>List(Of EmployeeRank) </returns>
	Public Function findAll() As List(Of EmployeeRank)
		Return Me.findList(String.Empty)
	End Function
		
	public Overrides Property Loader() As IModelObjectLoader
		Get
			if me._loader is Nothing then 
				me._loader = New EmployeeRankDataReaderLoader
			end If
			return me._loader
        End Get
        Set(value as IModelObjectLoader)
			me._loader = value
        end Set
    End Property

#End Region

	protected Overrides Function pkFieldName() As String
		return "RankId"
    End Function
		
	Public Overrides Function getModelInstance() As IModelObject
		return new EmployeeRank()
    End Function
		
End Class

#Region " EmployeeRank Loader "
	<System.Runtime.InteropServices.ComVisible(False)> _
	Public Class EmployeeRankDataReaderLoader
		Inherits DataReaderLoader

			Public Overrides sub load(ByVal mo As IModelObject)

			Const DATAREADER_FLD_RANKID as Integer = 0
			Const DATAREADER_FLD_RANK as Integer = 1

			
            Dim obj As EmployeeRank = directCast(mo, EmployeeRank)
            obj.IsObjectLoading = True

			if me.reader.IsDBNull(DATAREADER_FLD_RANKID) = false Then
				obj.PrRankId = me.reader.GetInt32(DATAREADER_FLD_RANKID)
			End if
			if me.reader.IsDBNull(DATAREADER_FLD_RANK) = false Then
				obj.PrRank = me.reader.GetString(DATAREADER_FLD_RANK)
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
	Public NotInheritable Class EmployeeRankDataUtils

#Region "Shared ""get"" Functions "

		Public Shared Function findList(ByVal where As String, ByVal ParamArray params() As Object) _
					As List(Of EmployeeRank)

            dim dbm as EmployeeRankDBMapper = new EmployeeRankDBMapper()
            Return dbm.findList(where, params)

        End Function
		
		Public Shared Function findList(ByVal where As String, ByVal params As List(Of IDataParameter)) _
										As List(Of EmployeeRank)

            Dim dbm As EmployeeRankDBMapper = New EmployeeRankDBMapper()
            Return dbm.findList(where, params)

        End Function

		Public Shared Function findOne(ByVal where As String, ByVal ParamArray params() As Object) _
					As EmployeeRank

            Dim dbm As EmployeeRankDBMapper = New EmployeeRankDBMapper()
            Return DirectCast(dbm.findWhere(where, params), EmployeeRank)

        End Function
        
        
         Public Shared Function findList() As List(Of EmployeeRank)
			
			return new EmployeeRankDBMapper().findAll()
			
        End Function
        
        Public Shared Function findByKey(id as object) as EmployeeRank
			
			return DirectCast( new EmployeeRankDBMapper().findByKey(id),EmployeeRank)
                        
        end function
        
		''' <summary>
        ''' Reload the EmployeeRank from the database
        ''' </summary>
        ''' <remarks>
		''' use this method when you want to relad the EmployeeRank 
		''' from the database, discarding any changes
		''' </remarks>
		Public Shared Sub reload(ByRef mo As EmployeeRank)
			
			If mo Is Nothing Then
                Throw New System.ArgumentNullException("null object past to reload function")
            End If
            
			mo = DirectCast(New EmployeeRankDBMapper().findByKey(mo.Id), EmployeeRank)
            
        End Sub

#End Region

#Region "Shared Save and Delete Functions"
		''' <summary>
        ''' Convinience method to save a EmployeeRank Object.
        ''' Important note: DO NOT CALL THIS IN A LOOP!
        ''' </summary>
        ''' <param name="EmployeeRankObj"></param>
        ''' <remarks>
		''' Important note: DO NOT CALL THIS IN A LOOP!  
		''' This method simply instantiates a EmployeeRankDBMapper and calls the save method
		''' </remarks>
		public shared sub saveEmployeeRank(ByVal ParamArray EmployeeRankObj() As EmployeeRank)
            
            dim dbm as EmployeeRankDBMapper = new EmployeeRankDBMapper()
            dbm.saveList(EmployeeRankObj.ToList)

                       
       end sub
       

       public shared sub deleteEmployeeRank(ByVal EmployeeRankObj As EmployeeRank)
            
            dim dbm as EmployeeRankDBMapper = new EmployeeRankDBMapper()
            dbm.delete(EmployeeRankObj)
            
        end sub
#End Region

#Region "Data Table and data row load/save "        
        Public Shared Sub saveEmployeeRank(ByVal dr As DataRow, _
                                                 Optional ByRef mo As EmployeeRank = Nothing)

            if mo is Nothing then 
				mo = new EmployeeRank()
			end if
			
            For Each dc As DataColumn In dr.Table.Columns
                mo.setAttribute(dc.ColumnName, dr.Item(dc.ColumnName))
            Next

            call saveEmployeeRank(mo)

        End Sub
        
         
        Public Shared Sub saveEmployeeRank(ByVal dt As DataTable, _
                                                 Optional ByRef mo As EmployeeRank = Nothing)

            For Each dr As DataRow In dt.Rows
            	call saveEmployeeRank(dr,mo)
            Next
			
        End Sub
        
		 Public Shared Function loadFromDataRow(ByVal r As DataRow) As EmployeeRank

            Dim a As New DataRowLoader
            Dim mo As IModelObject = new EmployeeRank()
            a.DataSource = r
            a.load(mo)
            Return DirectCast(mo, EmployeeRank)

        End Function

#End Region

    End Class
	
End Namespace

