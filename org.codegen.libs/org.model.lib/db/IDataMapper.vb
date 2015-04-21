Imports System.Data
Imports System.Runtime.InteropServices

Namespace Model

   
    Public Interface IDataMapper

        '''	<summary>
        ''' Saves a Domain Object from the persistence medium 
        ''' </summary>
        ''' <param name="mobj"> The ModelObject to save </param>
        Sub save(ByVal mobj As IModelObject)

        '''	<summary>
        ''' Deletes a Domain Object from the persistence medium </summary>
        ''' <param name="mobj"> The ModelObject to delete </param>
        Sub delete(ByVal mobj As IModelObject)

        ''' <summary>Saves any **Parent** ModelObjects associated with the ModelObject to the persistence medium.
        ''' Clients should override this for any Parent objects that the Model Object mo carries
        ''' Example:
        ''' <ul>
        ''' <li>Person and Address, 1-to-1 relationship, with the AddressID on the Person object</li>
        ''' <li>Address must be saved first in order to save Person.</li>
        ''' <li>Address object must be a parent</li>
        ''' </ul> </summary>
        ''' <param name="mo"> ModelObject that parent belongs to </param>
        Sub saveParents(ByVal mo As IModelObject)

        ''' <summary>  Saves any child ModelObjects associated with the ModelObject to the persistence medium .
        ''' Clients should override this for any children objects that the Model Object mo carries </summary>
        ''' <param name="mo"> ModelObject that children belong to </param>
        Sub saveChildren(ByVal mo As IModelObject)


        ''' <summary>
        ''' Performs an <b>update</b> operation to the database 
        ''' </summary>
        ''' <param name="o"> ModelObject to save to database </param>
        Sub update(ByVal o As IModelObject)


        ''' <summary>  
        ''' Performs an <b>insert</b> operation to the database 
        ''' </summary>
        ''' <param name="mo"> ModelObject to save to database </param>
        Sub insert(ByVal mo As IModelObject)

        ''' <summary>
        ''' Gets/Sets the object responsible for loading ModelObject
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Property Loader() As IModelObjectLoader

        'Sub saveList(ByVal olst As IEnumerable(Of ModelObject))

    End Interface


    ''' <summary>
    ''' Loader of Model Objects Interface
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface IModelObjectLoader

        Property DataSource() As Object
        Sub load(ByVal mo As IModelObject)

    End Interface

End Namespace