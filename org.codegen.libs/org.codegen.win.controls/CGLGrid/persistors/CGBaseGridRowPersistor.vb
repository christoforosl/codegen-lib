
Imports System.Diagnostics

Namespace Grid

    Public Class CGGridRowPersistor
        Implements ICGGridRowPersistor

        Property DBMapper As DBMapper

        Public Sub SaveRowToStore(ByVal cgGrid As CGBaseGrid, _
                                  ByVal dataRow As DataGridViewRow) Implements ICGGridRowPersistor.SaveRowToStore

            Dim ts As TraceSource = New TraceSource("CGGridRowPersistor")

            If dataRow Is Nothing Then
                ts.TraceInformation("Data Row is nothing, return")
                Return
            End If

            'if we are during loading of data on the grid, just exit
            If cgGrid.DataLoading Then
                ts.TraceInformation("DataLoading = true, will return")
                Return
            End If

            If cgGrid.ReadOnly Then
                ts.TraceInformation("cgGrid.ReadOnly = true, will return")
                Return
            End If

            cgGrid.EndEdit()
            If cgGrid.BindingSource IsNot Nothing Then cgGrid.BindingSource.EndEdit()

            Dim mo As IModelObject
            Dim keyIdx As Integer = cgGrid.Columns(cgGrid.gpKeyColumnName).Index
            Dim pk As Integer? = CType(dataRow.Cells(keyIdx).Value, Integer?)

            If pk Is Nothing Then
                mo = Me.DBMapper.getModelInstance
                ts.TraceInformation("Instantiated a new Model Object")
            Else
                mo = Me.DBMapper.findByKey(pk.GetValueOrDefault)
                ts.TraceInformation("Instantiated and loaded an existing Model Object with pk:{0}", pk)
            End If
            Dim a As New DataGridViewRowLoader

            a.DataSource = dataRow
            a.load(mo)
            ts.TraceInformation("Loaded Model Object:{0}", mo.valuesToString)

            Me.DBMapper.save(mo)
            ts.TraceInformation("Saved Model Object:{0}", mo.valuesToString)

        End Sub

    End Class
End Namespace
