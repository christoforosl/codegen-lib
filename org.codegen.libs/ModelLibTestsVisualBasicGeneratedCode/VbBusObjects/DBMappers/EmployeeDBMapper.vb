﻿'NOTE: DO NOT ADD REFERENCES TO COM.NETU.LIB HERE, INSTEAD ADD
'THE IMPORT ON THE REFERENCES OF THE PROJECT

'<comments>
'Template: DBMapperBase.visualBasic.txt
'************************************************************
' Class autogenerated on 11/07/2015 9:27:44 AM by ModelGenerator
' Extends base DBMapperBase object class
' *** DO NOT change code in this class.  
'     It will be re-generated and 
'     overwritten by the code generator ****
' Instead, change code in the extender class EmployeeDBMapper
'
'************************************************************
'</comments>

Namespace VbBusObjects.DBMappers
    <System.Runtime.InteropServices.ComVisible(False)> <SelectObject("Employee")> <KeyFieldName("EmployeeId")> _
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
                                            ByVal ParamArray params() As Object) As Employee

            Return DirectCast(MyBase.findWhere(sWhereClause, params), Employee)
        End Function


        Public Sub saveEmployee(ByVal mo As Employee)
            MyBase.save(mo)
        End Sub

        Public Shadows Function findByKey(ByVal keyval As Object) As Employee

            Return DirectCast(MyBase.findByKey(keyval), Employee)

        End Function

#End Region

#Region "getUpdateDBCommand"
        Public Overrides Function getUpdateDBCommand(ByVal modelObj As IModelObject, ByVal sql As String) As IDbCommand

            Dim p As IDataParameter = Nothing
            Dim obj As Employee = DirectCast(modelObj, Employee)
            Dim stmt As IDbCommand = Me.dbConn.getCommand(sql)
            stmt.Parameters.Add(Me.dbConn.getParameter(Employee.STR_FLD_EMPLOYEENAME, obj.PrEmployeeName))
            stmt.Parameters.Add(Me.dbConn.getParameter(Employee.STR_FLD_EMPLOYEERANKID, obj.PrEmployeeRankId))
            stmt.Parameters.Add(Me.dbConn.getParameter(Employee.STR_FLD_SALARY, obj.PrSalary))
            stmt.Parameters.Add(Me.dbConn.getParameter(Employee.STR_FLD_ADDRESS, obj.PrAddress))
            stmt.Parameters.Add(Me.dbConn.getParameter(Employee.STR_FLD_TELEPHONE, obj.PrTelephone))
            stmt.Parameters.Add(Me.dbConn.getParameter(Employee.STR_FLD_MOBILE, obj.PrMobile))
            stmt.Parameters.Add(Me.dbConn.getParameter(Employee.STR_FLD_IDNUMBER, obj.PrIdNumber))
            stmt.Parameters.Add(Me.dbConn.getParameter(Employee.STR_FLD_SSINUMBER, obj.PrSSINumber))
            stmt.Parameters.Add(Me.dbConn.getParameter(Employee.STR_FLD_HIREDATE, obj.PrHireDate))
            stmt.Parameters.Add(Me.dbConn.getParameter(Employee.STR_FLD_NUMDEPENDENTS, obj.PrNumDependents))
            stmt.Parameters.Add(Me.dbConn.getParameter(Employee.STR_FLD_EMPLOYEETYPECODE, obj.PrEmployeeTypeCode))
            stmt.Parameters.Add(Me.dbConn.getParameter(Employee.STR_FLD_CREATEDATE, obj.CreateDate))
            stmt.Parameters.Add(Me.dbConn.getParameter(Employee.STR_FLD_UPDATEDATE, obj.UpdateDate))
            stmt.Parameters.Add(Me.dbConn.getParameter(Employee.STR_FLD_CREATEUSER, obj.CreateUser))
            stmt.Parameters.Add(Me.dbConn.getParameter(Employee.STR_FLD_UPDATEUSER, obj.UpdateUser))
            stmt.Parameters.Add(Me.dbConn.getParameter(Employee.STR_FLD_SAMPLEGUIDFIELD, obj.PrSampleGuidField))
            stmt.Parameters.Add(Me.dbConn.getParameter(Employee.STR_FLD_ISACTIVE, obj.PrIsActive))
            stmt.Parameters.Add(Me.dbConn.getParameter(Employee.STR_FLD_SAMPLEBIGINT, obj.PrSampleBigInt))
            stmt.Parameters.Add(Me.dbConn.getParameter(Employee.STR_FLD_SAMPLESMALLINT, obj.PrSampleSmallInt))
            stmt.Parameters.Add(Me.dbConn.getParameter(Employee.STR_FLD_SAMPLENUMERICFIELDINT, obj.PrSampleNumericFieldInt))
            stmt.Parameters.Add(Me.dbConn.getParameter(Employee.STR_FLD_SAMPLENUMERICFIELD2DECIMALS, obj.PrSampleNumericField2Decimals))

            If (obj.isNew) Then
            Else
                'only add primary key if we are updating and as the last parameter
                stmt.Parameters.Add(Me.dbConn.getParameter(Employee.STR_FLD_EMPLOYEEID, obj.PrEmployeeId))
            End If

            Return stmt

        End Function

#End Region
#Region "Save Children Code"
        Public Overrides Sub saveChildren(mo As IModelObject)

            Dim ret As Employee = DirectCast(mo, Employee)
            '** Child Association:employeeinfo
            If ret.EmployeeInfoLoaded = True Then
                Dim employeeinfoMapper As VbBusObjects.DBMappers.EmployeeInfoDBMapper = New VbBusObjects.DBMappers.EmployeeInfoDBMapper(Me.DBConn())
                employeeinfoMapper.save(ret.PrEmployeeInfo())
            End If
            '** Child Association:employeeprojects
            If ret.EmployeeProjectsLoaded = True Then
                Dim employeeprojectsMapper As VbBusObjects.DBMappers.EmployeeProjectDBMapper = New VbBusObjects.DBMappers.EmployeeProjectDBMapper(Me.DBConn())
                employeeprojectsMapper.saveList(ret.PrEmployeeProjects())
                employeeprojectsMapper.deleteList(ret.PrEmployeeProjectsGetDeleted())
            End If
        End Sub
#End Region
        Public Overrides Sub saveParents(mo As IModelObject)

            Dim thisMo As Employee = DirectCast(mo, Employee)
            '*** Parent Association:rank
            If (thisMo.PrRank Is Nothing = False) AndAlso thisMo.PrRank().NeedsSave() Then
                Dim mappervar As VbBusObjects.DBMappers.EmployeeRankDBMapper = New VbBusObjects.DBMappers.EmployeeRankDBMapper(Me.dbConn)
                mappervar.save(thisMo.PrRank)
                thisMo.PrEmployeeRankId = thisMo.PrRank.PrRankId
            End If

        End Sub
#Region "Find functions"

        '''	<summary>Given an sql statement, it opens a result set, and for each record returned, it creates and loads a ModelObject. </summary>
        '''	<param name="sWhereClause">where clause to be applied to "selectall" statement 
        ''' that returns one or more records from the database, corresponding to the ModelObject we are going to load </param>
        '''	<param name="params"> Parameter values to be passed to sql statement </param>
        '''	<returns> A List(Of Employee) object containing all objects loaded </returns>
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

        Public Overrides Property Loader() As IModelObjectLoader
            Get
                If Me._loader Is Nothing Then
                    Me._loader = New EmployeeDataReaderLoader
                End If
                Return Me._loader
            End Get
            Set(value As IModelObjectLoader)
                Me._loader = value
            End Set
        End Property

#End Region

        Public Overrides Function getModelInstance() As IModelObject
            Return New Employee()
        End Function

    End Class

#Region " Employee Loader "
    <System.Runtime.InteropServices.ComVisible(False)> _
    Public Class EmployeeDataReaderLoader
        Inherits DataReaderLoader

        Public Overrides Sub load(ByVal mo As IModelObject)

            Const DATAREADER_FLD_EMPLOYEEID As Integer = 0
            Const DATAREADER_FLD_EMPLOYEENAME As Integer = 1
            Const DATAREADER_FLD_EMPLOYEERANKID As Integer = 2
            Const DATAREADER_FLD_SALARY As Integer = 3
            Const DATAREADER_FLD_ADDRESS As Integer = 4
            Const DATAREADER_FLD_TELEPHONE As Integer = 5
            Const DATAREADER_FLD_MOBILE As Integer = 6
            Const DATAREADER_FLD_IDNUMBER As Integer = 7
            Const DATAREADER_FLD_SSINUMBER As Integer = 8
            Const DATAREADER_FLD_HIREDATE As Integer = 9
            Const DATAREADER_FLD_NUMDEPENDENTS As Integer = 10
            Const DATAREADER_FLD_EMPLOYEETYPECODE As Integer = 11
            Const DATAREADER_FLD_CREATEDATE As Integer = 12
            Const DATAREADER_FLD_UPDATEDATE As Integer = 13
            Const DATAREADER_FLD_CREATEUSER As Integer = 14
            Const DATAREADER_FLD_UPDATEUSER As Integer = 15
            Const DATAREADER_FLD_SAMPLEGUIDFIELD As Integer = 16
            Const DATAREADER_FLD_ISACTIVE As Integer = 17
            Const DATAREADER_FLD_SAMPLEBIGINT As Integer = 18
            Const DATAREADER_FLD_SAMPLESMALLINT As Integer = 19
            Const DATAREADER_FLD_SAMPLENUMERICFIELDINT As Integer = 20
            Const DATAREADER_FLD_SAMPLENUMERICFIELD2DECIMALS As Integer = 21


            Dim obj As Employee = DirectCast(mo, Employee)
            obj.IsObjectLoading = True

            If Me.reader.IsDBNull(DATAREADER_FLD_EMPLOYEEID) = False Then
                obj.PrEmployeeId = Convert.ToInt64(Me.reader.GetInt32(DATAREADER_FLD_EMPLOYEEID))
            End If
            If Me.reader.IsDBNull(DATAREADER_FLD_EMPLOYEENAME) = False Then
                obj.PrEmployeeName = Me.reader.GetString(DATAREADER_FLD_EMPLOYEENAME)
            End If
            If Me.reader.IsDBNull(DATAREADER_FLD_EMPLOYEERANKID) = False Then
                obj.PrEmployeeRankId = Convert.ToInt64(Me.reader.GetInt32(DATAREADER_FLD_EMPLOYEERANKID))
            End If
            If Me.reader.IsDBNull(DATAREADER_FLD_SALARY) = False Then
                obj.PrSalary = Me.reader.GetDecimal(DATAREADER_FLD_SALARY)
            End If
            If Me.reader.IsDBNull(DATAREADER_FLD_ADDRESS) = False Then
                obj.PrAddress = Me.reader.GetString(DATAREADER_FLD_ADDRESS)
            End If
            If Me.reader.IsDBNull(DATAREADER_FLD_TELEPHONE) = False Then
                obj.PrTelephone = Me.reader.GetString(DATAREADER_FLD_TELEPHONE)
            End If
            If Me.reader.IsDBNull(DATAREADER_FLD_MOBILE) = False Then
                obj.PrMobile = Me.reader.GetString(DATAREADER_FLD_MOBILE)
            End If
            If Me.reader.IsDBNull(DATAREADER_FLD_IDNUMBER) = False Then
                obj.PrIdNumber = Me.reader.GetString(DATAREADER_FLD_IDNUMBER)
            End If
            If Me.reader.IsDBNull(DATAREADER_FLD_SSINUMBER) = False Then
                obj.PrSSINumber = Me.reader.GetString(DATAREADER_FLD_SSINUMBER)
            End If
            If Me.reader.IsDBNull(DATAREADER_FLD_HIREDATE) = False Then
                obj.PrHireDate = Me.reader.GetDateTime(DATAREADER_FLD_HIREDATE)
            End If
            If Me.reader.IsDBNull(DATAREADER_FLD_NUMDEPENDENTS) = False Then
                obj.PrNumDependents = Convert.ToInt64(Me.reader.GetInt32(DATAREADER_FLD_NUMDEPENDENTS))
            End If
            If Me.reader.IsDBNull(DATAREADER_FLD_EMPLOYEETYPECODE) = False Then
                obj.PrEmployeeTypeCode = Me.reader.GetString(DATAREADER_FLD_EMPLOYEETYPECODE)
            End If
            If Me.reader.IsDBNull(DATAREADER_FLD_CREATEDATE) = False Then
                obj.CreateDate = Me.reader.GetDateTime(DATAREADER_FLD_CREATEDATE)
            End If
            If Me.reader.IsDBNull(DATAREADER_FLD_UPDATEDATE) = False Then
                obj.UpdateDate = Me.reader.GetDateTime(DATAREADER_FLD_UPDATEDATE)
            End If
            If Me.reader.IsDBNull(DATAREADER_FLD_CREATEUSER) = False Then
                obj.CreateUser = Me.reader.GetString(DATAREADER_FLD_CREATEUSER)
            End If
            If Me.reader.IsDBNull(DATAREADER_FLD_UPDATEUSER) = False Then
                obj.UpdateUser = Me.reader.GetString(DATAREADER_FLD_UPDATEUSER)
            End If
            If Me.reader.IsDBNull(DATAREADER_FLD_SAMPLEGUIDFIELD) = False Then
                obj.PrSampleGuidField = Me.reader.GetGuid(DATAREADER_FLD_SAMPLEGUIDFIELD)
            End If
            If Me.reader.IsDBNull(DATAREADER_FLD_ISACTIVE) = False Then
                obj.PrIsActive = Me.reader.GetBoolean(DATAREADER_FLD_ISACTIVE)
            End If
            If Me.reader.IsDBNull(DATAREADER_FLD_SAMPLEBIGINT) = False Then
                obj.PrSampleBigInt = Me.reader.GetInt64(DATAREADER_FLD_SAMPLEBIGINT)
            End If
            If Me.reader.IsDBNull(DATAREADER_FLD_SAMPLESMALLINT) = False Then
                obj.PrSampleSmallInt = Convert.ToInt64(Me.reader.GetInt16(DATAREADER_FLD_SAMPLESMALLINT))
            End If
            If Me.reader.IsDBNull(DATAREADER_FLD_SAMPLENUMERICFIELDINT) = False Then
                obj.PrSampleNumericFieldInt = Convert.ToInt64(Me.reader.GetDecimal(DATAREADER_FLD_SAMPLENUMERICFIELDINT))
            End If
            If Me.reader.IsDBNull(DATAREADER_FLD_SAMPLENUMERICFIELD2DECIMALS) = False Then
                obj.PrSampleNumericField2Decimals = Me.reader.GetDecimal(DATAREADER_FLD_SAMPLENUMERICFIELD2DECIMALS)
            End If


            obj.isNew = False ' since we've just loaded from database, we mark as "old"
            obj.isDirty = False
            obj.IsObjectLoading = False
            obj.afterLoad()

            Return

        End Sub

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

            Dim dbm As EmployeeDBMapper = New EmployeeDBMapper()
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

            Return New EmployeeDBMapper().findAll()

        End Function

        Public Shared Function findByKey(id As Object) As Employee

            Return DirectCast(New EmployeeDBMapper().findByKey(id), Employee)

        End Function

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
        Public Shared Sub saveEmployee(ByVal ParamArray EmployeeObj() As Employee)

            Dim dbm As EmployeeDBMapper = New EmployeeDBMapper()
            dbm.saveList(EmployeeObj.ToList)


        End Sub


        Public Shared Sub deleteEmployee(ByVal EmployeeObj As Employee)

            Dim dbm As EmployeeDBMapper = New EmployeeDBMapper()
            dbm.delete(EmployeeObj)

        End Sub
#End Region

#Region "Data Table and data row load/save "
        Public Shared Sub saveEmployee(ByVal dr As DataRow, _
                                                 Optional ByRef mo As Employee = Nothing)

            If mo Is Nothing Then
                mo = New Employee()
            End If

            For Each dc As DataColumn In dr.Table.Columns
                mo.setAttribute(dc.ColumnName, dr.Item(dc.ColumnName))
            Next

            Call saveEmployee(mo)

        End Sub


        Public Shared Sub saveEmployee(ByVal dt As DataTable, _
                                                 Optional ByRef mo As Employee = Nothing)

            For Each dr As DataRow In dt.Rows
                Call saveEmployee(dr, mo)
            Next

        End Sub

        Public Shared Function loadFromDataRow(ByVal r As DataRow) As Employee

            Dim a As New DataRowLoader
            Dim mo As IModelObject = New Employee()
            a.DataSource = r
            a.load(mo)
            Return DirectCast(mo, Employee)

        End Function

#End Region

    End Class

End Namespace

