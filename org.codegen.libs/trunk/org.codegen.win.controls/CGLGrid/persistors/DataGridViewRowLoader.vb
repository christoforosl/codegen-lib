

Namespace Grid
    ''' <summary>
    ''' Class that Loads a model object from a datarow
    ''' </summary>
    ''' <remarks></remarks>
    Public NotInheritable Class DataGridViewRowLoader
        Implements IModelObjectLoader

        Public Property DataSource As Object Implements model.lib.Model.IModelObjectLoader.DataSource

        ''' <summary>
        ''' Loads a model obejct from a DataGridViewRow
        ''' </summary>
        ''' <param name="mo">ModelObject to load</param>
        ''' <remarks>
        ''' Loading occurs by looping thru the Columns of the DataGridView
        ''' and for each column that has the DataPropertyName set, we call
        ''' method "setAttribute"
        ''' </remarks>
        Public Sub load(ByVal mo As IModelObject) _
                Implements IModelObjectLoader.load

            If Me.DataSource Is Nothing Then Return

            Dim dataRow As DataGridViewRow = DirectCast(Me.DataSource, DataGridViewRow)

            For Each dc As DataGridViewColumn In dataRow.DataGridView.Columns
                If String.IsNullOrEmpty(dc.DataPropertyName) = False Then
                    mo.setAttribute(dc.DataPropertyName, _
                                    dataRow.Cells(dc.Index).Value)
                End If

            Next

        End Sub
    End Class

    'System.Windows.Forms.DataGridViewRow'.

    ''' <summary>
    ''' Class that Loads a model object from a datarow
    ''' </summary>
    ''' <remarks></remarks>
    Public NotInheritable Class DataRowModelObjectLoader
        Implements IModelObjectLoader

        Public Property DataSource As Object Implements model.lib.Model.IModelObjectLoader.DataSource

        ''' <summary>
        ''' Loads a model obejct from a DataGridViewRow
        ''' </summary>
        ''' <param name="mo">ModelObject to load</param>
        ''' <remarks>
        ''' Loading occurs by looping thru the Columns of the DataGridView
        ''' and for each column that has the DataPropertyName set, we call
        ''' method "setAttribute"
        ''' </remarks>
        Public Sub load(ByVal mo As IModelObject) _
                Implements IModelObjectLoader.load

            If Me.DataSource Is Nothing Then Return

            Dim dataRow As DataRow = DirectCast(Me.DataSource, DataRow)

            For Each dc As DataColumn In dataRow.Table.Columns

                mo.setAttribute(dc.ColumnName, _
                                dataRow.Item(dc.ColumnName))


            Next

        End Sub
    End Class

End Namespace
