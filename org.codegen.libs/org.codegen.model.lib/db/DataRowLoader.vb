
Imports org.codegen.model.lib.Model
''' <summary>
''' Loads a model object from a System.Data.DataRow
''' </summary>
''' <remarks></remarks>
Public NotInheritable Class DataRowLoader
    Implements IModelObjectLoader

    ''' <summary>
    ''' Empty constructor
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()
    End Sub

    Public Property DataSource() As Object Implements Model.IModelObjectLoader.DataSource

    ''' <summary>
    ''' Loads ModelObject from data row.
    ''' Fields in columns of Datatable must match fields in Model Object
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub load(ByVal mo As IModelObject) Implements Model.IModelObjectLoader.load

        Dim dataRow As DataRow = DirectCast(Me.DataSource, DataRow)
        If dataRow Is Nothing Then Return

        mo.IsObjectLoading = True
        For Each dc As DataColumn In dataRow.Table.Columns
            mo.setAttribute(dc.ColumnName, dataRow.Item(dc.ColumnName))

        Next

        mo.IsObjectLoading = False
        mo.isNew = mo.Id <= 0

    End Sub


End Class