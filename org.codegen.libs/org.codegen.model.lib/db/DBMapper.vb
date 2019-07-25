Option Strict On
Imports org.codegen.model.lib.Model

'''
''' <summary>  
'''  Base Class for Database Data Mapper Pattern
'''  This class has the responsibility of loading 
'''  data into Model Objects, and saving them back to 
'''  the Database </summary>
''' 
Public MustInherit Class DBMapper
    Implements IDataMapper

#Region "Fields"

    Protected Const SQL_SELECT_ALL As String = "selectall"
    Protected Const SQL_SELECT_ONE As String = "selectone"
    Protected Const SQL_INSERT As String = "insert"
    Protected Const SQL_UPDATE As String = "update"
    Protected Const SQL_DELETE As String = "delete"

    Private _dbConn As DBUtils

    Public primaryKeyParameterIndex As Integer
#End Region

#Region "Constructors"

    '''    
    '''	 <summary> Pass a valid dbConn for constructor.
    '''	 We need a dbConn because most of the times we will be in a transaction </summary>
    '''	 <param name="c"> </param>
    '''	 
    Public Sub New(ByVal c As DBUtils)
        Me.dbConn = c
    End Sub


    '''    
    '''	 <summary>Instantiation without a Connection </summary>
    '''	 
    Public Sub New()
        Me.dbConn = ModelContext.CurrentDBUtils
    End Sub

#End Region


    Public Overridable Function getSqlWithWhereClause(ByVal sWhereClause As String) As String

        Const STR_WHERE As String = "WHERE"
        Const SPACE As String = " "

        If String.IsNullOrEmpty(sWhereClause) = False Then
            If sWhereClause.Trim().ToUpper().StartsWith(STR_WHERE) = False Then
                sWhereClause = STR_WHERE + SPACE + sWhereClause
            End If
        End If

        Dim sql As String = Me.getSQLStatement(SQL_SELECT_ALL) & " " & sWhereClause
        Return sql

    End Function

    ''' <summary>
    ''' Retrieves a modelobject based on the key value passed
    ''' </summary>
    ''' <param name="sWhereClause">Where clause to be applied to selectall</param>
    ''' <returns>loaded ModelObject class instance</returns>
    ''' <remarks></remarks>
    ''' 
    Public Function findWhere(ByVal sWhereClause As String,
                                          ByVal ParamArray params() As Object) _
                                          As IModelObject

        Dim sql As String = Me.getSqlWithWhereClause(sWhereClause)
        Dim rs As IDataReader = Nothing
        Dim mo As IModelObject = Nothing

        Try

            rs = dbConn.getDataReaderWithParams(sql, params)
            Me.Loader.DataSource = rs

            If rs.Read Then

                'if datareader has records, then instantiate the object and load it
                mo = Me.getModelInstance()
                Me.Loader.load(mo)
                mo.isDirty = False
                mo.afterLoad()

            End If


        Finally
            Me.dbConn.closeDataReader(rs)
        End Try

        Return mo

    End Function


    ''' <summary>
    ''' Retrieves a modelobject based on the key value passed
    ''' </summary>
    ''' <param name="IdValue">Primary Key value</param>
    ''' <returns>loaded ModelObject class instance</returns>
    ''' <remarks></remarks>
    Public Function findByKey(ByVal IdValue As Integer) As IModelObject

        Dim rs As IDataReader = Nothing
        Dim mo As IModelObject = Me.getModelInstance

        Try
            'no need to hit the database if the IdValue is less than 0
            If IdValue > 0 Then
                rs = dbConn.getDataReaderWithParams(Me.getSQLStatement(SQL_SELECT_ONE), IdValue)
                Me.Loader.DataSource = rs

                If rs.Read Then
                    Me.Loader.load(mo)
                    mo.isDirty = False
                    mo.afterLoad()

                End If
            End If

        Finally
            Me.dbConn.closeDataReader(rs)
        End Try

        Return mo

    End Function

    ''' <summary>
    ''' Retrieves an enumerable if IModelObject based on the key value passed
    ''' </summary>
    ''' <returns>List of loaded ModelObject class instances</returns>
    ''' <remarks></remarks>
    Public Function findList(ByVal sWhereClause As String,
                                          ByVal ParamArray params() As Object) _
                                          As IEnumerable(Of IModelObject)

        Dim rs As IDataReader = Nothing
        Dim ret As List(Of IModelObject) = New List(Of IModelObject)

        Try

            rs = dbConn.getDataReaderWithParams(Me.getSqlWithWhereClause(sWhereClause), params)
            Me.Loader.DataSource = rs

            Do While rs.Read
                Dim mo As IModelObject = Me.getModelInstance
                Me.Loader.load(mo)
                mo.isDirty = False
                mo.afterLoad()
                ret.Add(mo)
            Loop

        Finally
            Me.dbConn.closeDataReader(rs)
        End Try

        Return ret


    End Function

#Region "Transaction Support"

    ''' <summary>
    ''' Boolean to indicate whether the save operation has started
    ''' a transaction.  At the save of the Function Definition, the code checks whether 
    ''' the dbutils object has started a transaction.
    ''' If not, it starts it and sets this flag to TRUE.  
    ''' At the end of the save operation, the transaction is committed, if this flag is true
    ''' At the FINALLY clause on the save, the transaction is rolled back, if this flag is true
    ''' </summary>
    ''' <remarks></remarks>
    Private _transStarted As Boolean

    ''' <summary>
    ''' Check if the DBUtils connection is uin a transaction.  IF not, start a transaction
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub beginTrans()

        If Me.dbConn.inTrans = False Then
            _transStarted = True
            Me.dbConn.beginTrans()
        End If

    End Sub

    ''' <summary>
    ''' Check if the DBUtils started a transaction, the roll it back 
    ''' and set the _transStarted flag.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub rollbackTrans()

        If _transStarted = True Then
            Me.dbConn.rollbackTrans()
            _transStarted = False
        End If

    End Sub

    ''' <summary>
    ''' Check if the DBUtils started a transaction, the commit it 
    ''' and set the _transStarted flag.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub commitTrans()

        If _transStarted = True Then
            Me.dbConn.commitTrans()
            _transStarted = False
        End If

    End Sub

#End Region

    Public Overridable Sub deleteList(ByVal olst As IEnumerable(Of ModelObject))
        Try

            Me.beginTrans()

            For Each o As IModelObject In olst
                Me.delete(o)
            Next

            Me.commitTrans()

        Finally
            Me.rollbackTrans()
        End Try

    End Sub

    Public Overridable Sub saveList(ByVal olst As IEnumerable(Of ModelObject))
        Try

            Me.beginTrans()

            For Each o As IModelObject In olst
                Me.save(o)
            Next

            Me.commitTrans()

        Finally
            Me.rollbackTrans()
        End Try

    End Sub

    '''	 <summary>Save a ModelObject to the database
    '''	This method first calls saveModelObject and then saveChildren </summary>
    '''	<param name="o">ModelObject to save </param>
    Public Overridable Sub save(ByVal o As IModelObject) Implements IDataMapper.save

        Try

            Me.beginTrans()

            If o.isDirty() = True Then

                o.setAuditFields()

                ' KEEP BEFORE SAVE ***AFTER the validateObject call ***
                ' The reason is that beforeSave() may change the values 
                ' of the object, so we must validate after the beforeSave()
                o.validateObject()

            End If

            Me.saveParents(o)
            Me.saveModelObject(o)
            Me.saveChildren(o)

            Me.commitTrans()


        Finally
            Me.rollbackTrans()

        End Try
    End Sub

    '''    
    '''	<summary>
    ''' routine to Save data of ModelObject to database
    ''' The database is hit ONLY if the object has changed (Dirty flag=True) 
    ''' and also isEmpty function returns false
    ''' After save, the dirty flag is set to False </summary>
    ''' <param name="o"> ModelObject to save </param>
    ''' <seealso></seealso>
    Private Sub saveModelObject(ByVal o As IModelObject)

        ' only Save data if the object has changed, or is not empty
        If o.isDirty() = True Then

            If o.isNew() Then
                Me.insert(o)
            Else
                Me.update(o)
            End If
            o.isDirty = False
        End If

    End Sub

    '''    
    '''	<summary>Saves any **Parent** ModelObjects associated with the ModelObject to the database.
    ''' Clients should override this for any Parent objects that the Model Object mo carries
    ''' Example:
    ''' <ul>
    ''' <li>Person and Address, 1-to-1 relationship, with the AddressID on the Person object</li>
    ''' <li>Address must be saved first in order to save Person.</li>
    ''' <li>Address object must be a parent</li>
    ''' </ul> </summary>
    ''' <param name="mo"> ModelObject that parent belongs to </param>
    '''	 
    Public Overridable Sub saveParents(ByVal mo As IModelObject) Implements IDataMapper.saveParents

        If mo.getParents.Count > 0 Then
            Throw New ApplicationException("** saveParents must be overriden **")
        End If

        Return

    End Sub

    '''    
    '''	 <summary>  Saves any child ModelObjects associated with the ModelObject to the database.
    ''' Clients should override this for any children objects that the Model Object mo carries </summary>
    ''' <param name="mo"> ModelObject that children belong to </param>
    '''	 
    Public Overridable Sub saveChildren(ByVal mo As IModelObject) Implements IDataMapper.saveChildren
        Return
    End Sub


    '''    
    '''	 <summary>  Performs an <b>update</b> operation to the database </summary>
    ''' <param name="o"> ModelObject to save to database </param>
    Public Overridable Sub update(ByVal o As IModelObject) Implements IDataMapper.update

        Dim pstmt As IDbCommand = Nothing

        Try
            Dim sqlStmt As String = Me.getSQLStatement(SQL_UPDATE)
            pstmt = Me.getUpdateDBCommand(o, sqlStmt)
            pstmt.ExecuteNonQuery()


        Finally
            If pstmt IsNot Nothing Then pstmt.Dispose()
        End Try

    End Sub

    ''' <summary>
    ''' Returns a new instance of the ModelObject we are handling
    ''' with the dbMapper
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public MustOverride Function getModelInstance() As IModelObject

    ''' <summary>
    ''' Returns an IDBCommand object, filled with parameters for insert or update.
    ''' </summary>
    ''' <param name="obj">ModelObject that holds data</param>
    ''' <remarks></remarks>
    Public MustOverride Function getUpdateDBCommand(ByVal obj As IModelObject,
                                                    ByVal sql As String) As IDbCommand

    '''    
    '''	 <summary>  Performs an <b>insert</b> operation to the database </summary>
    ''' <param name="mo"> ModelObject to save to database </param>
    Public Overridable Sub insert(ByVal mo As IModelObject) Implements IDataMapper.insert

        Dim pstmt As IDbCommand = Nothing
        Dim vLStatement As String = String.Empty

        Try

            vLStatement = Me.getSQLStatement(SQL_INSERT)

            pstmt = Me.getUpdateDBCommand(mo, vLStatement)
            pstmt.ExecuteNonQuery()

            ' set the ID field here
            mo.Id = Me.getIdentity()
            mo.isDirty = False
            mo.isNew = False

        Finally
            If pstmt IsNot Nothing Then pstmt.Dispose()

        End Try
    End Sub

    Private Function getIdentity() As Integer

        Dim cmd As IDbCommand = Nothing
        Try
            cmd = Me.dbConn.getCommand("SELECT @@IDENTITY")
            Return CInt(cmd.ExecuteScalar())

        Finally
            If cmd IsNot Nothing Then cmd.Dispose()
        End Try

    End Function

    '''    
    '''<summary>  Performs an <b>delete</b> operation to the database </summary>
    '''<param name="mo"> ModelObject to save to database </param> 
    Public Overridable Sub delete(ByVal mo As IModelObject) _
                            Implements IDataMapper.delete

        mo.validateDelete()
        Me.deleteByKey(mo.Id)

    End Sub

    '''    
    '''<summary>  Performs an <b>delete</b> operation to the database </summary>
    '''<param name="Id"> Id of modelobject to delete from database </param>
    Public Overridable Sub deleteByKey(ByVal id As Integer) _
            Implements IDataMapper.deleteByKey

        Dim pstmt As IDbCommand = Nothing

        Try

            Me.beginTrans()

            pstmt = Me.dbConn().getCommand(Me.getSQLStatement(SQL_DELETE))
            pstmt.Parameters.Add(Me.dbConn.getParameter("0", id))
            pstmt.ExecuteNonQuery()

            Me.commitTrans()


        Finally

            Me.rollbackTrans()
            If pstmt IsNot Nothing Then pstmt.Dispose()

        End Try
    End Sub

#Region "Object Loader"

    Protected _loader As IModelObjectLoader

    Public MustOverride Property Loader() As IModelObjectLoader Implements IDataMapper.Loader

#End Region

    Public Property dbConn() As DBUtils
        Get
            If _dbConn Is Nothing Then
                Throw New ApplicationException("No Connection!")
            End If

            Return _dbConn
        End Get
        Set(ByVal value As DBUtils)
            Me._dbConn = value
        End Set
    End Property

    '''<summary>  Loads an SQL statement string.  By default, the implementation of this method
    ''' loads Insert,Update,Delete,SelectOne and SelectAll statements from an XML
    ''' file, stored as embedded resource in the dll.  Clients can override this method 
    ''' and implement their own logic to get
    ''' SQL statements.  In order to load from a statements from a file, 
    ''' this function first checks method getSQLStatementsPropetyFile
    ''' and if this returns empty string, it exits, otherwise it loads the 
    ''' xml file with the sql statements of the class.  At minimum, the class should have 5 keys,
    ''' with the corresponding SQL statements:
    ''' <table>
    ''' <tr><td><b>Properties File Key</b></td><td><b>Constant</b></td><td><b>Example</b></td></tr>
    ''' <tr><td>update</td><td>SQL_UPDATE</td><td>dim sql as String = getSQLStatement(SQLStatementsProvider.SQL_UPDATE)</td></tr>
    ''' <tr><td>delete</td><td>SQL_DELETE</td><td>String sql = getSQLStatement(SQLStatementsProvider.SQL_DELETE)</td></tr>
    ''' <tr><td>insert</td><td>SQL_INSERT</td><td>String sql = getSQLStatement(SQLStatementsProvider.SQL_INSERT)</td></tr>
    ''' <tr><td>selectone</td><td>SQL_SELECT_ONE</td><td>String sql = getSQLStatement(SQLStatementsProvider.SQL_SELECT_ONE)</td></tr>
    ''' <tr><td>selectall</td><td>SQL_SELECT_ALL</td><td>String sql = getSQLStatement(SQLStatementsProvider.SQL_SELECT_ALL)</td></tr>
    ''' </table>
    ''' Other, custom sql statements can be added below the above standard statements. </summary> 
    Public MustOverride Function getSQLStatement(ByVal skey As String) As String

End Class
