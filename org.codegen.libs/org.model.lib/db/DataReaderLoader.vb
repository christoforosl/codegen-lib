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

Public Class LinQLoader(Of T As IModelObject)

    Public Function load() As IEnumerable(Of T)

        Dim sql = ModelContext.GetModelDefaultMapper(GetType(T)).getSQLStatement(DBMapperStatementsFile.StmtType.selectByPK)

        Using ctx As DataContext = DBUtils.Current().dbContext()

            Dim query = ctx.ExecuteQuery(Of T)(sql)
            Return query

        End Using

    End Function

End Class