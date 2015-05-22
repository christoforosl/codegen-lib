Imports System.Xml
Imports System.Runtime.InteropServices

Namespace Model

    Public Interface IModelObject

        Property Id() As Integer

        ''' <summary>
        ''' Event fired when ID (primary key) of model object has changed
        ''' </summary>
        ''' <param name="mo"></param>
        ''' <remarks></remarks>
        Event IDChanged(ByVal mo As ModelObject)

        ''' <summary> 
        ''' Returns the names of fields in the object as a string array. 
        ''' Useful in automatically setting/getting values from UI objects (windows or web Form)
        ''' </summary>
        ''' <returns> string array </returns>
        Function getFieldList() As String()

        ''' <summary> 
        ''' return all field values as a string
        ''' used in error messages
        ''' </summary>
        Function valuesToString() As String

        ''' <summary>
        ''' Gets/Sets the dirty flag of the object.
        ''' </summary>
        Property isDirty() As Boolean

        ''' <summary>
        ''' Returns true if the model object or any of its chidlren 
        ''' are dirty.  Checks the dirty flag or the dirty flag of any of
        ''' the childrent 
        ''' </summary>
        ReadOnly Property isObjectOrChildrenDirty() As Boolean

        ''' <summary>
        ''' Returns true if the model object Needs save
        ''' Checks the dirty flag or the dirty flag of any of
        ''' the childrent and also if the isEmpty flag = false
        ''' </summary>
        ReadOnly Property NeedsSave() As Boolean

        ''' <summary>
        ''' Gets/Sets the "New" indicator of this object.  A new object is considered to be
        ''' an object that has not been persisted to a database or other persistance medium
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Property isNew() As Boolean

        ''' <summary>
        ''' Flag set to true when object is loading from a datareader or a dataset
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Property IsObjectLoading() As Boolean

        Function getAttribute(ByVal fieldKey As Integer) As Object
        Function getAttribute(ByVal fieldKey As String) As Object

        Sub setAttribute(ByVal fieldKey As Integer, ByVal val As Object)
        Sub setAttribute(ByVal fieldKey As String, ByVal val As Object)

        Function getParents() As List(Of ModelObject)
        Function getChildren() As List(Of ModelObject)

        ''' <summary>  
        ''' Procedure to handle event IDChanged, fired after a ModelObject is
        ''' saved to the database, and a new primary key as been created.  
        ''' Clients should override this and do necessarry operations when 
        ''' parent object changes ID. 
        ''' </summary>
        ''' <param name="parentMo"> 
        ''' parent Model Object of class 
        ''' </param>
        Sub handleParentIdChanged(ByVal parentMo As IModelObject)

        Sub raiseBroadcastIdChange()

        ''' <summary>  
        ''' Clients should override this and perform 
        ''' validations to the object BEFORE deletions. 
        ''' </summary>
        Sub validateDelete()

        ''' <summary>  
        ''' Final method that is automatically called from within save of model object
        ''' It calls the following functions:
        ''' <ol>
        ''' <li>
        ''' <b>validate</b>: Clients should override this and perform validations</li>
        ''' </ol> 
        ''' </summary>
        Sub validateObject()

        ''' <summary>
        ''' Procedure that is called after the ModelObject 
        ''' is loaded from the DBMapper  
        ''' </summary>
        Sub afterLoad()


        ''' <summary>
        ''' Sub to set the audit fields CreateDate, UpdateDate, CreateUser, UpdateUser
        ''' </summary>
        ''' <remarks></remarks>
        Sub setAuditFields()


        Function copy() As IModelObject

        Function getAuditor() As IAuditor

    End Interface

End Namespace
