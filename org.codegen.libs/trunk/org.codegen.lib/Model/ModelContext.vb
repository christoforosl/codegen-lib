Imports System.Threading
Imports System.Security.Principal

Namespace Model
    '''
    '''<summary>
    ''' Class to fascilitate communication between UserInterace and Model Objects
    ''' An ModelContext exists for each Thread.<br/>
    '''<p>
    '''The class contains a Locale object, a UserObject currently running the application
    '''and a database connection.  It can also receive other attributes, by calling
    '''<code>setAttribute(String, Object)</code> and <code>getAttribute(String)</code></p>
    '''<b>IMPORTANT: The ModelContext must always be released in a finally clause</b>
    '''Typical Usage:
    ''' <example>
    '''<code lang="vbnet">
    '''		Try 
    '''	    	ModelContext.newCurrent(principal);
    '''			ModelContext.Current().setAttribute("something", someObject);
    '''     	...
    '''     	Object me = ModelContext.Current().getAttribute("something");
    '''     Finally
    '''     	ModelContext.release();
    '''     End Try
    '''</code>
    ''' </example>
    ''' </summary>
    ''' 
    Public Class ModelContext

        Private _locale As System.Globalization.CultureInfo = System.Globalization.CultureInfo.CurrentCulture
        Private _principal As IPrincipal = Nothing
        Private _attributes As Hashtable = Nothing
        Private _dbUtils As DBUtils

        <ThreadStatic()> _
        Private Shared _current As ModelContext = Nothing

        Private Sub New(ByVal principal As IPrincipal, _
                        ByVal locale As System.Globalization.CultureInfo)

            Me.new()
            ' private constructor...
            Me._principal = principal
            If Not locale Is Nothing Then
                Me._locale = locale
            End If


        End Sub

        Private Sub New(ByVal principal As IPrincipal)

            Me.New(principal, System.Globalization.CultureInfo.CurrentCulture())

        End Sub

        Private Sub New()
            Me._attributes = New Hashtable
            ' private constructor...
        End Sub

        '''    
        '''	 <summary>Creates a new intance of ModelContext and sets the Connection and Principal objects,
        '''	 and marks it as current. </summary>
        '''	 <param name="principal"> User object for Security checks </param>
        '''	 
        Public Shared Sub newCurrent(ByVal principal As IPrincipal)
            setCurrent(New ModelContext(principal))
        End Sub

        '''    
        '''	 <summary>
        ''' Creates a new intance of ModelContext and sets the Connection,Principal and Lcoale objects,
        '''	and marks it as current. </summary>
        '''	<param name="principal"> User object for Security checks </param>
        '''	 
        Public Shared Sub newCurrent(ByVal principal As IPrincipal, ByVal locale As System.Globalization.CultureInfo)
            setCurrent(New ModelContext(principal, locale))
        End Sub

        '''    
        '''	 <summary>
        ''' Creates a new intance of ModelContext and sets the Principal and Locale objects,
        '''	and marks it as current. 
        ''' <b>This method should only be used for testing</b>
        ''' </summary>
        '''	<param name="username"> User name of ModelObjectIdentity for Security checks </param>
        '''	 
        Public Shared Sub newCurrent(ByVal username As String, ByVal locale As System.Globalization.CultureInfo)
            Dim p As IPrincipal = New GenericPrincipal(New GenericIdentity(username), Nothing)
            setCurrent(New ModelContext(p, locale))
        End Sub

        Public Shared Sub newCurrent(ByVal username As String)
            Dim p As IPrincipal = New GenericPrincipal(New GenericIdentity(username), Nothing)
            setCurrent(New ModelContext(p))
        End Sub

        '''    
        '''	<summary>
        ''' Creates a new intance of ModelContext and sets the Principal and Locale objects,
        '''	and marks it as current. <b>This method should only be used for testing</b>
        ''' </summary>
        Public Shared Sub newForUnitTests()
            Dim p As IPrincipal = New GenericPrincipal(New GenericIdentity("-1"), Nothing)
            setCurrent(New ModelContext(p))
        End Sub

        '''    
        '''	<summary>
        ''' Creates a new intance of ModelContext and marks it as current. </summary>
        '''	 
        Public Shared Sub newCurrent()
            setCurrent(New ModelContext())
        End Sub

        Public Shared Sub release()
            setCurrent(Nothing)
        End Sub

        '''    
        '''	 <summary>Creates a new intance of ModelContext and marks it as current. </summary>
        '''	 
        Private Shared Sub setCurrent(ByVal uao As ModelContext)
            _current = uao
        End Sub

        '''    
        '''	 <summary>Retrieves the current intance of ModelContext </summary>
        '''
        Public Shared Function Current() As ModelContext

            If _current Is Nothing Then
                newCurrent()
            End If
            Return _current

        End Function

        Public Property Locale() As System.Globalization.CultureInfo
            Get
                Return Me._locale
            End Get
            Set(ByVal value As System.Globalization.CultureInfo)
                Me._locale = value
            End Set
        End Property

        Public Property CurrentUser() As IPrincipal
            Get
                Return Me._principal
            End Get
            Set(ByVal value As IPrincipal)
                Me._principal = value
            End Set
        End Property

        Public Property CurrentUserName() As String
            Get
                If (Me._principal Is Nothing) Then
                    Return String.Empty
                End If
                If (Me._principal.Identity Is Nothing) Then
                    Return String.Empty
                End If

                Return Me._principal.Identity.Name

            End Get
            Set(ByVal value As String)
                Dim p As IPrincipal = New GenericPrincipal(New GenericIdentity(value), Nothing)
                Me.CurrentUser = p
            End Set
        End Property


        Public Shared Property CurrentDBUtils() As DBUtils
            Get
                If Current._dbUtils Is Nothing Then
                    Current._dbUtils = DBUtils.Current
                End If
                Return Current._dbUtils
            End Get
            Set(ByVal value As DBUtils)
                Current._dbUtils = value
            End Set
        End Property


        Private Shared Function getAttributes() As Hashtable
            If Current._attributes Is Nothing Then
                Current._attributes = New Hashtable(10)
            End If
            Return Current._attributes
        End Function

        Public Shared Sub setAttribute(ByVal key As String, ByVal val As Object)
            getAttributes().Add(key, val)
        End Sub

        Public Shared Function getAttribute(ByVal key As String) As Object
            Return getAttributes().Item(key)
        End Function


        Public Shared Sub beginTrans()
            If Current() Is Nothing Then
                Return
            End If

            If CurrentDBUtils Is Nothing Then
                Throw New ApplicationException("Application service connection is null!")
            End If

            CurrentDBUtils.beginTrans()

        End Sub

        Public Shared Sub commitTrans()
            If Current() Is Nothing Then
                Return
            End If

            If CurrentDBUtils Is Nothing Then
                Throw New ApplicationException("Application service connection is null!")
            End If

            CurrentDBUtils.commitTrans()

        End Sub

        Public Shared Sub rollbackTrans()
            If Current() Is Nothing Then
                Return
            End If

            If CurrentDBUtils Is Nothing Then
                Throw New ApplicationException("Application service connection is null!")
            End If

            CurrentDBUtils.rollbackTrans()

        End Sub

        ''' <summary>
        ''' Dictionary of global validators, per type.
        ''' The key should be a modelObject and the value should be a IModelObjectValidator type 
        ''' </summary>
        ''' <remarks></remarks>
        Private globalModelValidators As Dictionary(Of Type, Type) = New Dictionary(Of Type, Type)

        ''' <summary>
        ''' Adds a global validator for a specific model object.  This allows for separating the model 
        ''' from the logic of the application.  Without this, all validators would have to recide 
        ''' inside the Model project
        ''' </summary>
        ''' <param name="modelObjectType">The type of the model object.  Use GetType(x) where x is the class (not instance) of the model object</param>
        ''' <param name="validatorType">The type of the validator object.  Use GetType(val) where val is the class (not instance) of the validator</param>
        ''' <remarks></remarks>
        Public Sub addGlobalModelValidator(ByVal modelObjectType As Type, ByVal validatorType As Type)

            If Not GetType(IModelObject).IsAssignableFrom(modelObjectType) Then
                Throw New ApplicationException("modelObjectType param must implement IModelObject")
            End If

            If Not GetType(IModelObjectValidator).IsAssignableFrom(validatorType) Then
                Throw New ApplicationException("validatorType param must implement IModelObjectValidator")
            End If


            Me.globalModelValidators.Add(modelObjectType, validatorType)

        End Sub

        ''' <summary>
        ''' Returns a validator, if any, configured for the specified model object type
        ''' </summary>
        ''' <param name="modelObjectType">The type of the model object.  
        ''' Use GetType(x) where x is the class (not instance) of the model object
        ''' </param>
        ''' <returns>
        ''' Returns a validator, if any, configured for the specified model object type. 
        ''' If not validator is configured, it returns null(nothing)
        ''' </returns>
        ''' <remarks></remarks>
        Public Function getModelValidator(ByVal modelObjectType As Type) As IModelObjectValidator

            If Me.globalModelValidators.ContainsKey(modelObjectType) Then
                Return CType(Activator.CreateInstance(Me.globalModelValidators.Item(modelObjectType)),  _
                                IModelObjectValidator)
            End If
            Return Nothing

        End Function

        Public Shared Function GetModelDefaultMapper(ByVal modelType As Type) _
                       As DBMapper

            'get default dbMapper
            Dim sattr As DefaultMapperAttr = CType(Attribute.GetCustomAttribute(modelType, _
                                                        GetType(DefaultMapperAttr)), DefaultMapperAttr)

            If sattr Is Nothing Then
                Throw New ApplicationException( _
                    String.Format("Call to ModelContext.Save/Load Model Object must pass a model object with attribute DefaultMapperAttr set. ""{0}"" does not have the attribute set.", _
                    modelType.ToString))
            End If
            Dim mapper As DBMapper = CType(Activator.CreateInstance(sattr.defaultMapper),  _
                                            DBMapper)

            Return mapper

        End Function

        Public Shared Function GetModelDefaultMapper(ByVal modelObjectInstance As IModelObject) _
                        As DBMapper

            Return GetModelDefaultMapper(modelObjectInstance.GetType)

        End Function

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="modelObjectInstance">The model Object to save</param>
        ''' <remarks></remarks>
        Public Sub saveModelObject(modelObjectInstance As IModelObject)

            Dim mapper As DBMapper = GetModelDefaultMapper(modelObjectInstance)
            mapper.save(modelObjectInstance)

        End Sub

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="modelObjectInstance">The model Object to save</param>
        ''' <remarks></remarks>
        Public Sub deleteModelObject(modelObjectInstance As IModelObject)

            Dim mapper As DBMapper = GetModelDefaultMapper(modelObjectInstance)
            mapper.delete(modelObjectInstance)

        End Sub


        Public Function loadModelObject(Of T As ModelObject)(id As Object) As T
            ''Public Function Blah(Of T As {IImplementedByT})(Foo As T)
            Dim sattr As DefaultMapperAttr = CType(Attribute.GetCustomAttribute(GetType(T), _
                                                        GetType(DefaultMapperAttr)), DefaultMapperAttr)

            If sattr Is Nothing Then
                Throw New ApplicationException( _
                    String.Format("Call to ModelContext.Save must pass a model object with attribute DefaultMapperAttr set. ""{0}"" does not have the attribute set.", _
                    GetType(T).ToString))
            End If

            Dim tmp As DBMapper = CType(Activator.CreateInstance(sattr.defaultMapper),  _
                                            DBMapper)

            Return CType(CType(tmp.findByKey(id), ModelObject), T)

        End Function

    End Class

    
End Namespace