''' <summary>
''' Interface implemented by all detail generated controls of the code generator
''' </summary>
''' <remarks></remarks>
Public Interface IUcEditControl
    Inherits IChangeTrackable

    Property ModelObject As IModelObject 

    ''' <summary>
    ''' Fills the controls on the screen from data in the object
    ''' </summary>
    ''' <remarks></remarks>
    Sub loadData()

    ''' <summary>
    ''' Loads the object from the database and then sets the proeperties 
    ''' of the object from values on the controls
    ''' </summary>
    ''' <remarks></remarks>
    Sub loadToObject()

End Interface

Public Interface IChangeTrackable
   

    ''' <summary>
    ''' Returns true if data on control has changed
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function hasChanges() As Boolean

    ''' <summary>
    ''' Returns a String wth all the controls' the text concatenated.
    ''' Used in determining if the form has changes or not
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function getConcatenatedControlValues() As String

    ''' <summary>
    ''' resets the _lastLoadedvalues variable to the concatenated values
    ''' </summary>
    Sub resetLastLoadedValues()

End Interface

