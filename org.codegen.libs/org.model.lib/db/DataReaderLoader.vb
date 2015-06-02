Option Infer On
Imports System.Data.Linq
Imports System.Linq

''' <summary>
''' Loads a model object from a System.Data.IDataReader
''' </summary>
''' <remarks></remarks>

Public MustInherit Class DataReaderLoader : Implements IModelObjectLoader

    Protected reader As IDataReader
    ''' <summary>
    ''' Gets/Sets the DataSource of the loader, an object if type IDataReader
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DataSource() As Object Implements Model.IModelObjectLoader.DataSource
        Get
            Return Me.reader
        End Get
        Set(ByVal value As Object)
            reader = DirectCast(value, IDataReader)
        End Set
    End Property

    ''' <summary>
    ''' Loads from IDataReader to ModelObject
    ''' </summary>
    ''' <remarks></remarks>
    Public MustOverride Sub load(ByVal mo As Model.IModelObject) Implements Model.IModelObjectLoader.load

End Class

