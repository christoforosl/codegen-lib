Imports System.Xml
Imports System.Runtime.InteropServices

Namespace Model
    ''' <summary>  Model Objects 
    ''' are often central to an application, since they usually model Database Tables to Objects.
    ''' Model objects 
    ''' often map roughly to the records of a corresponding database table 
    ''' are often used as return values for Data Access Object methods 
    ''' are easily tested using JUnit (or a similar tool) 
    ''' can be used to implement the Model in a Model-View-Controller pattern 
    '''  </summary>
    <Serializable()> _
    Public MustInherit Class ModelObject
        Implements IModelObject
        Implements ICloneable
        Implements IChildObject

        Public Const SYSTEM_INT32_DEFAULT As Integer = 0
        Public Const SYSTEM_DECIMAL_DEFAULT As Decimal = 0D
        Public Const SYSTEM_DATETIME_DEFAULT As DateTime = #12:00:00 AM#

        Friend Const STR_XML_ATTR_REL_TYPE As String = "relationType"
        Friend Const STR_XML_ATTR_REL_TYPE_STR As String = "relationTypeString"

        Friend Const STR_XML_ATTR_ASSEMBLY As String = "AssemblyName"
        Friend Const STR_XML_ATTR_TYPE_NAME As String = "moType"


#Region "Class"

        ''' <summary>
        ''' hashmap of Field name, Boolean that keeps track of changed fields
        ''' </summary>
        ''' <remarks></remarks>
        Private changedFields As Dictionary(Of String, Boolean)

        Private _New As Boolean = True
        Private _Dirty As Boolean = False


        '''<summary> Gets/Sets the Id of the object  </summary>
        ''' <returns> an int value stored in the Key object </returns>	 
        Public MustOverride Property Id() As Integer Implements IModelObject.Id

        Public Event IDChanged(ByVal mo As ModelObject) Implements IModelObject.IDChanged

        <Serialization.XmlIgnore()> _
        Private Property objectValidators As List(Of IModelObjectValidator)

        <Serialization.XmlIgnore()> _
        Private Property objectDeleteValidators As List(Of IModelObjectValidator)

        Public Sub addValidator(ByVal x As IModelObjectValidator)
            If Me.objectValidators Is Nothing Then
                Me.objectValidators = New List(Of IModelObjectValidator)
            End If
            Me.objectValidators.Add(x)
        End Sub

        Public Sub addDeleteValidator(ByVal x As IModelObjectValidator)
            If Me.objectDeleteValidators Is Nothing Then
                Me.objectDeleteValidators = New List(Of IModelObjectValidator)
            End If
            Me.objectDeleteValidators.Add(x)
        End Sub



        ''' <summary>
        ''' Sets a field as changed
        ''' </summary>
        ''' <param name="fieldname"></param>
        ''' <remarks></remarks>
        Protected Sub setFieldChanged(ByVal fieldname As String)
            If Me.changedFields Is Nothing Then
                Me.changedFields = New Dictionary(Of String, Boolean)

            End If
            If Not Me.changedFields.ContainsKey(fieldname) Then
                Me.changedFields.Add(fieldname, True)
            Else
                Me.changedFields.Item(fieldname) = True
            End If

        End Sub

        ''' <summary>
        ''' Shared function to convert a list of model objects into a 
        ''' </summary>
        ''' <param name="inval"></param>
        ''' <param name="field"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function getDictionaryFromList( _
                ByVal inval As List(Of ModelObject), ByVal field As String) _
                    As Dictionary(Of String, ModelObject)

            Dim v As New Dictionary(Of String, ModelObject)

            Call inval.ForEach(Sub(rws)
                                   v.Add(CStr(rws.getAttribute(field)), rws)
                               End Sub)

            Return v
        End Function



        ''' <summary>
        ''' Returns true if the field has changed value
        ''' </summary>
        ''' <param name="fieldname"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Protected Function isFieldChanged(ByVal fieldname As String) As Boolean
            If Me.changedFields IsNot Nothing AndAlso Me.changedFields.ContainsKey(fieldname) Then
                Return Me.changedFields.Item(fieldname)
            End If

        End Function

        Protected Function getStringHashCode(ByVal x As String) As Integer

            If String.IsNullOrEmpty(x) Then
                Return String.Empty.GetHashCode
            Else
                Return x.GetHashCode
            End If

        End Function

        Public Sub New()

            Me.Id = ModelObjectKeyGen.nextId()
            Me.isDirty = False

        End Sub

        '''<summary> 
        ''' Returns the names of fields in the object as a string array. 
        ''' Useful in automatically setting/getting values from UI objects (windows or web Form)
        '''</summary>
        ''' <returns> string array </returns>	 
        Public MustOverride Function getFieldList() As String() Implements IModelObject.getFieldList

        '''<summary> 
        '''return all field values as a string
        '''used in error messages
        '''</summary>
        Public Function valuesToString() As String Implements IModelObject.valuesToString

            Dim ret As String = String.Empty
            Dim fldArr As String() = Me.getFieldList
            For i As Integer = 0 To fldArr.Length - 1
                ret &= fldArr(i) & ":" & CStr(Me.getAttribute(fldArr(i))) & vbCrLf

            Next
            Return ret

        End Function
#End Region

#Region "isDitry, isNew flags"

        ''' <summary>
        ''' Gets/Sets the dirty flag of the object.
        ''' </summary>
        Public Property isDirty() As Boolean Implements IModelObject.isDirty, IChildObject.isDirty
            Get
                Return _Dirty
            End Get

            Set(ByVal value As Boolean)
                _Dirty = value
                If (value = False) Then
                    Me.changedFields = Nothing
                End If
            End Set

        End Property

        ''' <summary>
        ''' Returns true if the model object or any of its chidlren 
        ''' are dirty.  Checks the dirty flag or the dirty flag of any of
        ''' the childrent 
        ''' </summary>
        ''' 
        <Serialization.XmlIgnore()> _
        Public ReadOnly Property isObjectOrChildrenDirty() As Boolean Implements IModelObject.isObjectOrChildrenDirty
            Get
                Return Me.isDirty OrElse Me.areChildrenDirty()
            End Get


        End Property

        ''' <summary>
        ''' Returns true if the model object Needs save
        ''' Checks the dirty flag or the dirty flag of any of
        ''' the childrent and also if the isEmpty flag = false
        ''' </summary>
        ''' 
        <Serialization.XmlIgnore()> _
        Public ReadOnly Property NeedsSave() As Boolean Implements IModelObject.NeedsSave
            Get
                Return Me.isObjectOrChildrenDirty
            End Get

        End Property

        ''' <summary>
        ''' Private Property that checks dirty flag on all childrent of model object
        ''' </summary>
        ''' <value></value>
        ''' <returns>True if any of the children of the model object are Dirty</returns>
        ''' <remarks></remarks>
        <Serialization.XmlIgnore()> _
        Private ReadOnly Property areChildrenDirty() As Boolean
            Get
                Dim children As List(Of ModelObject) = Me.getChildren()
                If children.Count = 0 Then Return False
                For Each mo As Object In children

                    If CType(mo, ModelObject).isDirty() Then
                        Return True
                    End If

                Next

            End Get
        End Property

        ''' <summary>
        ''' Gets/Sets the "New" indicator of this object.  A new object is considered to be
        ''' an object that has not been persisted to a database or other persistance medium
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property isNew() As Boolean Implements IModelObject.isNew
            Get
                Return _New
            End Get
            Set(ByVal value As Boolean)
                Me._New = value
            End Set
        End Property

        ''' <summary>
        ''' Compares val1 to val2 and returns true if they are not equal
        ''' </summary>
        ''' <param name="val1"></param>
        ''' <param name="val2"></param>
        ''' <returns>true if val1 is not equal to val2, false if the two are equal</returns>
        ''' <remarks></remarks>
        Protected Shared Function valueChanged(ByVal val1 As Object, ByVal val2 As Object) As Boolean
            If val1 Is Nothing AndAlso val2 Is Nothing Then
                Return False

            ElseIf val1 IsNot Nothing AndAlso val2 Is Nothing Then
                Return True

            ElseIf val1 Is Nothing AndAlso val2 IsNot Nothing Then
                Return True

            ElseIf Not (val1.Equals(val2)) Then
                Return True

            Else
                Return False
            End If

        End Function
#End Region

#Region "Get/Set attributes"
        Public MustOverride Function getAttribute(ByVal fieldKey As Integer) As Object Implements IModelObject.getAttribute
        Public MustOverride Function getAttribute(ByVal fieldKey As String) As Object Implements IModelObject.getAttribute

        Public MustOverride Sub setAttribute(ByVal fieldKey As Integer, ByVal val As Object) Implements IModelObject.setAttribute
        Public MustOverride Sub setAttribute(ByVal fieldKey As String, ByVal val As Object) Implements IModelObject.setAttribute
#End Region

#Region "Associated Objects management"

        Public MustOverride Function getChildren() As List(Of ModelObject) Implements IModelObject.getChildren
        Public MustOverride Function getParents() As List(Of ModelObject) Implements IModelObject.getParents

#End Region

#Region "ID maintenace"
        '''    
        '''	 <summary>  
        ''' Procedure to handle event IDChanged, fired after a ModelObject is
        ''' saved to the database, and a new primary key as been created.  
        ''' Clients should override this and do necessarry operations when 
        ''' parent object changes ID. 
        ''' </summary>
        ''' <param name="parentMo"> 
        ''' parent Model Object of class 
        ''' </param>
        '''	 
        Public Overridable Sub handleParentIdChanged(ByVal parentMo As IModelObject) Implements IModelObject.handleParentIdChanged
            'if the code comes here, it means that a child has not overriden
            'the method to update the ids
            If Me.getParents.Count > 0 Then
                Dim err As String = "****Error: handleParentIdChanged not Overriden" & vbCrLf & _
                          " Parent type:" & TypeName(parentMo) & vbCrLf & _
                          " Me type:" & TypeName(Me) & vbCrLf & _
                          " New ID:" & parentMo.Id & vbCrLf & " ****" & vbCrLf

                Throw New ApplicationException(err)
            End If

            Return

        End Sub

        Public Sub raiseBroadcastIdChange() Implements IModelObject.raiseBroadcastIdChange
            RaiseEvent IDChanged(Me)
        End Sub

        Public Overridable Sub setupIDChangeListeners() Implements IModelObject.setupIDChangeListeners

            Dim children As List(Of ModelObject) = Me.getChildren
            For Each obj As ModelObject In children
                AddHandler Me.IDChanged, AddressOf obj.handleParentIdChanged
            Next

            Dim pars As List(Of ModelObject) = Me.getParents
            For Each obj As ModelObject In pars
                AddHandler obj.IDChanged, AddressOf Me.handleParentIdChanged
            Next

        End Sub


#End Region

#Region "Validation"



        '''    
        '''	<summary>  
        ''' Clients should override this and perform 
        ''' validations to the object BEFORE deletions. 
        ''' </summary>
        '''	 
        Public Overridable Sub validateDelete() Implements IModelObject.validateDelete

            If Me.objectDeleteValidators IsNot Nothing Then
                For Each x As IModelObjectValidator In Me.objectDeleteValidators
                    x.validate(Me)
                Next
            End If
        End Sub


        '''    
        '''	 <summary>  
        ''' Final method that is automatically called from within save of model object
        ''' It calls the following functions:
        ''' <ol>
        ''' <li><b>validate</b>: Clients should override this and perform validations</li>
        ''' </ol> 
        ''' </summary>
        '''	 
        Public Sub validateObject() Implements IModelObject.validateObject

            If Me.objectValidators IsNot Nothing Then
                For Each x As IModelObjectValidator In Me.objectValidators
                    x.validate(Me)
                Next
            End If

        End Sub

#End Region

#Region "Before and After Load, Save routines"

        '''    
        '''	<summary>
        ''' Procedure that is called after the ModelObject 
        ''' is loaded from the DBMapper  
        ''' </summary>
        Public Overridable Sub afterLoad() Implements IModelObject.afterLoad
            Return
        End Sub






        ''' <summary>
        ''' Sub to set the audit fields CreateDate, UpdateDate, CreateUser, UpdateUser
        ''' </summary>
        ''' <remarks></remarks>
        Public Overridable Sub setAuditFields() Implements IModelObject.setAuditFields

            If Me.getAuditor IsNot Nothing Then
                Me.getAuditor.setAuditFields(Me)
            End If
        End Sub

#End Region

#Region "Clone"

        Public Function Clone() As Object Implements System.ICloneable.Clone

            Dim buffer As New System.IO.MemoryStream
            Dim formatter As New System.Runtime.Serialization.Formatters.Binary.BinaryFormatter
            formatter.Serialize(buffer, Me)
            buffer.Position = 0
            Return formatter.Deserialize(buffer)

        End Function

#End Region

        Public MustOverride Function copy() As IModelObject Implements IModelObject.copy

        Public Property IsObjectLoading() As Boolean Implements IModelObject.IsObjectLoading

        ''' <summary>
        ''' Returns the object responsible for mainaining the 4 Audit fields 
        ''' of the model object.  
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks>
        ''' IAudiable is an interface with 4 fields: CreateDate, CreateUser (String), UpdateDate and UpdateUser (String)
        ''' IAudiable2 is an interface with 4 fields: CreateDate, CreateUser (Int), UpdateDate and UpdateUser (Int)
        ''' </remarks>
        Function getAuditor() As IAuditor Implements IModelObject.getAuditor

            If TypeOf Me Is IAuditable Then
                Return New Auditor

            ElseIf TypeOf Me Is IAuditable2 Then
                Return New Auditor2

            Else
                Return Nothing
            End If

        End Function

    End Class

End Namespace