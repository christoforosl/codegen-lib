
Imports System.Diagnostics

Namespace Grid

    ''' <summary>
    ''' This class is used when a single cell is the editable part of the DBGrid
    ''' </summary>
    ''' <remarks></remarks>
    Public Class CGGridSingleCellPersistor
        Implements ICGGridRowPersistor

        Property DBMapper As DBMapper

        Public Shared Sub CGBaseGrid_CellValidated(ByVal sender As Object, _
                                             ByVal e As DataGridViewCellEventArgs)

            Dim grd As CGBaseGrid = CType(sender, CGBaseGrid)

            If grd.DataLoading Then Return
            If grd.ReadOnly Then Return

            If grd.IsCurrentRowDirty = False Then Return

            grd.GridRowPersistor.SaveRowToStore(grd)

        End Sub

        Protected Overridable Function canSaveRow(ByVal cgGrid As CGBaseGrid, _
                                  ByVal dataRow As DataGridViewRow) As Boolean

            If dataRow Is Nothing Then
                Return False
            End If

            'if we are during loading of data on the grid, just exit
            If cgGrid.DataLoading Then
                Return False
            End If

            If cgGrid.ReadOnly Then
                Return False
            End If

            Return True

        End Function


        Public Sub SaveRowToStore(ByVal cgGrid As CGBaseGrid ) _
                                        Implements ICGGridRowPersistor.SaveRowToStore

            Dim dataRow As DataGridViewRow = cgGrid.CurrentRow()

            If Not canSaveRow(cgGrid, dataRow) Then Return

            cgGrid.EndEdit()
            If cgGrid.BindingSource IsNot Nothing Then cgGrid.BindingSource.EndEdit()

            Dim mo As IModelObject
            Dim keyIdx As Integer = cgGrid.Columns(cgGrid.gpKeyColumnName).Index
            Dim pk As Integer? = Nothing

            If Not dataRow.Cells(keyIdx).Value Is Nothing AndAlso _
                        Not dataRow.Cells(keyIdx).Value Is DBNull.Value Then

                pk = CType(dataRow.Cells(keyIdx).Value, Integer?)
            End If

            If pk Is Nothing Then
                mo = Me.DBMapper.getModelInstance
                'ts.TraceInformation("Instantiated a new Model Object")
            Else
                mo = Me.DBMapper.findByKey(pk.GetValueOrDefault)
                'ts.TraceInformation("Instantiated and loaded an existing Model Object with pk:{0}", pk)
            End If
            Dim a As New DataGridViewRowLoader

            a.DataSource = dataRow
            a.load(mo)
            'ts.TraceInformation("Loaded Model Object:{0}", mo.valuesToString)

            Me.DBMapper.save(mo)
            'ts.TraceInformation("Saved Model Object:{0}", mo.valuesToString)

        End Sub

    End Class
End Namespace
