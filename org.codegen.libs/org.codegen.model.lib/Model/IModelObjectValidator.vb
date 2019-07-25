Imports org.codegen.model.lib.Model
''' <summary>
''' Interface for classes responsible for validating a model object.
''' Each model object instance has an internal array of these objects 
''' and calls then sequantially before saving to the database, or with a call 
''' to "validateObject()"
''' </summary>
''' <remarks></remarks>
Public Interface IModelObjectValidator
    Sub validate(ByVal mo As IModelObject)
End Interface
