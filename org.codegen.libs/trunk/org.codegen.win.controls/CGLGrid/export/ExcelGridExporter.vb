Option Strict Off

Namespace Grid

    Class ExcelGridExporter

        Public Sub export(ByVal grid As CGBaseGrid)

            Dim currentCulture As System.Globalization.CultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture

            System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("en-US")
            Cursor.Current = Cursors.WaitCursor

            Dim ret As New System.Text.StringBuilder

            Dim oExcel As Object
            Dim oBook As Object
            Dim oSheet As Object
            Dim visibleColCount As Integer


            Try

                'Start a new workbook in Excel.
                oExcel = CreateObject("Excel.Application")

                oExcel.visible = True
                oExcel.ScreenUpdating = True
                oBook = oExcel.Workbooks.Add

                'step 1: get visible column count
                For i As Integer = 0 To grid.ColumnCount - 1
                    If grid.Columns(i).Visible Then
                        visibleColCount = visibleColCount + 1
                    End If
                Next

                'step 2: get header data, visible columns  only
                Dim DataArray(grid.Rows.Count + 1, visibleColCount - 1) As Object
                visibleColCount = 0
                For i As Integer = 0 To grid.ColumnCount - 1
                    If grid.Columns(i).Visible Then
                        DataArray(0, visibleColCount) = grid.Columns(i).HeaderText
                        visibleColCount = visibleColCount + 1
                    End If
                Next

                ''Output Data 
                For iRow As Integer = 0 To grid.Rows.Count - 1
                    visibleColCount = 0
                    For i As Integer = 0 To grid.ColumnCount - 1
                        If grid.Columns(i).Visible Then
                            If TypeOf (grid.Columns(i)) Is DataGridViewComboBoxColumn Then
                                Dim c As DataGridViewComboBoxColumn = CType(grid.Columns(i), DataGridViewComboBoxColumn)
                            Else
                            End If
                            DataArray(iRow + 1, visibleColCount) = grid.Rows(iRow).Cells(i).FormattedValue
                            visibleColCount = visibleColCount + 1
                        End If
                    Next

                Next

                'Transfer the array to the worksheet starting at cell A2.
                oExcel.worksheets.add.Range("A1").Resize(grid.Rows.Count, visibleColCount).Value = DataArray


            Finally
                oExcel.visible = True
                oExcel.StatusBar = True
                oExcel.ScreenUpdating = True
                oSheet = Nothing
                oBook = Nothing
                oExcel = Nothing
                GC.Collect()

                Cursor.Current = Cursors.Default
                System.Threading.Thread.CurrentThread.CurrentCulture = currentCulture

            End Try

            'Return flag

        End Sub

    End Class
End Namespace
