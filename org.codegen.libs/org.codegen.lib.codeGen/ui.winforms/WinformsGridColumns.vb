Imports System.Collections.Generic
Imports org.codegen.lib.codeGen.Tokens

Namespace org.codegen.lib.codeGen.Tokens

    Public Class WinformsGridColumnsDataSources
        Inherits ReplacementToken

        Sub New()
            Me.StringToReplace = "WINFROMS_GRID_COLUMNS_DATASOURCES"
        End Sub

        Public Overrides Function getReplacementCode(ByVal t As dotnet.IObjectToGenerate) As String
            Dim sb As System.Text.StringBuilder = New System.Text.StringBuilder()
            Dim flds As Dictionary(Of String, IDBField) = t.DbTable.Fields()

            For Each field As DBField In flds.Values
                If field.isLookup Then
                    Dim lk As FieldLookupInfo = field.ParentTable.LookupInfo.Item(field.FieldName)
                    Dim ds As String = "new " & ControlsLoadCode.GetAssociatedMapperClassName(lk.DataSource) & "().findAll()"

                    sb.Append(vbTab & "Me.").Append(field.FieldName).Append(".HeaderCell =  New DataGridViewAutoFilterComboColumnHeaderCell()").Append(vbCrLf)

                    sb.Append(vbTab & "me.").Append(field.FieldName).Append(".DataSource = ").Append(ds).Append(vbCrLf)
                    sb.Append(vbTab & "me.").Append(field.FieldName).Append(".DisplayMember = """) _
                            .Append(lk.DisplayMember).Append("""").Append(vbCrLf)
                    ' EmployeeRankId.FlatStyle = FlatStyle.Flat

                    sb.Append(vbTab & "me.").Append(field.FieldName).Append(".ValueMember = """) _
                                .Append(lk.ValueMember).Append("""").Append(vbCrLf)

                    sb.Append(vbTab & "me.").Append(field.FieldName).Append(".FlatStyle = FlatStyle.Flat") _
                                .Append(vbCrLf)

                End If

            Next

            Return sb.ToString
        End Function
    End Class

    Public Class WinformsGridColumnsInstantiate
        Inherits ReplacementToken

        Sub New()
            Me.StringToReplace = "WINFROMS_GRID_COLUMNS_INSTANTIATE"
        End Sub

        Public Overrides Function getReplacementCode(ByVal t As dotnet.IObjectToGenerate) As String
            Dim sb As System.Text.StringBuilder = New System.Text.StringBuilder()
            Dim flds As Dictionary(Of String, IDBField) = t.DbTable.Fields()

            For Each field As DBField In flds.Values

                sb.Append(vbTab).Append("me.").Append(field.RuntimeFieldName). _
                            Append(" = new ")

                If field.isDate Then
                    sb.Append("DataGridViewCalendarColumn").Append(vbCrLf)
                ElseIf field.isBoolean Then
                    sb.Append("DataGridViewCheckBoxColumn").Append(vbCrLf)
                ElseIf field.isLookup Then
                    sb.Append("DataGridViewComboBoxColumn").Append(vbCrLf)
                Else
                    sb.Append("DataGridViewTextBoxColumn").Append(vbCrLf)
                End If
            Next

            Return sb.ToString
        End Function
    End Class

    Public Class WinformsGridColumnsAdd
        Inherits ReplacementToken

        Sub New()
            Me.StringToReplace = "WINFROMS_GRID_COLUMNS_ADD"
        End Sub

        Public Overrides Function getReplacementCode(ByVal t As dotnet.IObjectToGenerate) As String
            Dim sb As System.Text.StringBuilder = New System.Text.StringBuilder()
            Dim flds As Dictionary(Of String, IDBField) = t.DbTable.Fields()
            Dim modelname As String = DirectCast(t, ObjectToGenerate).ClassName

            For Each field As DBField In flds.Values

                sb.Append(vbTab).Append("me.grd").Append(modelname).Append(".Columns.Add(") _
                    .Append(field.RuntimeFieldName).Append(")") _
                    .Append(vbCrLf)
            Next

            Return sb.ToString

        End Function
    End Class

    Public Class WinformsGridColumnsDeclare
        Inherits ReplacementToken

        Sub New()
            Me.StringToReplace = "WINFROMS_GRID_COLUMNS_DECLARE"
        End Sub

        Public Overrides Function getReplacementCode(ByVal t As dotnet.IObjectToGenerate) As String
            Dim sb As System.Text.StringBuilder = New System.Text.StringBuilder()
            Dim flds As Dictionary(Of String, IDBField) = t.DbTable.Fields()

            For Each field As DBField In flds.Values

                sb.Append(vbTab).Append("Private WithEvents ").Append(field.RuntimeFieldName). _
                            Append(" As ")
                If field.isDate Then
                    sb.Append("DataGridViewCalendarColumn").Append(vbCrLf)
                ElseIf field.isBoolean Then
                    sb.Append("DataGridViewCheckBoxColumn").Append(vbCrLf)
                ElseIf field.isLookup Then
                    sb.Append("DataGridViewComboBoxColumn").Append(vbCrLf)
                Else
                    sb.Append("DataGridViewTextBoxColumn").Append(vbCrLf)

                End If

            Next

            Return sb.ToString
        End Function
    End Class

    Public Class WinformsGridColumns
        Inherits ReplacementToken

        Sub New()
            Me.StringToReplace = "WINFROMS_GRID_COLUMNS"
        End Sub

        Public Overrides Function getReplacementCode(ByVal t As dotnet.IObjectToGenerate) As String
            Dim sb As System.Text.StringBuilder = New System.Text.StringBuilder()
            Dim flds As Dictionary(Of String, IDBField) = t.DbTable.Fields()

            For Each field As DBField In flds.Values

                sb.Append(getFieldGridColumn(field))

            Next

            Return sb.ToString
        End Function


        Private Function getFieldGridColumn(ByVal field As DBField) As String

            Dim sb As System.Text.StringBuilder = New System.Text.StringBuilder()

            sb.Append(vbTab).Append("' column: ").Append(field.RuntimeFieldName).Append(vbCrLf)

            If field.isDate Then
                sb.Append(vbTab).Append("").Append(field.RuntimeFieldName) _
                     .Append(".CellTemplate = New CalendarCell").Append(vbCrLf)

            ElseIf field.isBoolean Then
                sb.Append(vbTab).Append("").Append(field.RuntimeFieldName) _
                     .Append(".CellTemplate = New DataGridViewCheckBoxCell").Append(vbCrLf)
                sb.Append(vbTab).Append("").Append(field.RuntimeFieldName) _
                    .Append(".HeaderCell = New DataGridViewAutoFilterBooleanColumnHeaderCell").Append(vbCrLf)

            ElseIf field.isLookup Then

                sb.Append(vbTab).Append("").Append(field.RuntimeFieldName) _
                     .Append(".CellTemplate = New DataGridViewComboBoxCell").Append(vbCrLf)
                sb.Append(vbTab).Append("").Append(field.RuntimeFieldName) _
                    .Append(".HeaderCell = New DataGridViewAutoFilterComboColumnHeaderCell").Append(vbCrLf)


                

            Else
                sb.Append(vbTab).Append("").Append(field.RuntimeFieldName) _
                     .Append(".CellTemplate = New DataGridViewTextBoxCell").Append(vbCrLf)
            End If

            sb.Append(vbTab).Append("").Append(field.RuntimeFieldName) _
                     .Append(".Name = """).Append(field.RuntimeFieldName).Append("""") _
                     .Append(vbCrLf)

            sb.Append(vbTab).Append("").Append(field.RuntimeFieldName) _
                     .Append(".DataPropertyName = """).Append(field.RuntimeFieldName).Append("""") _
                     .Append(vbCrLf)

            sb.Append(vbTab).Append("").Append(field.RuntimeFieldName) _
                     .Append(".ReadOnly = True").Append(vbCrLf)

            sb.Append(vbTab).Append("").Append(field.RuntimeFieldName) _
                     .Append(".HeaderText = """).Append(field.RuntimeFieldName).Append("""") _
                     .Append(vbCrLf)
            sb.Append(vbTab).Append("").Append(field.RuntimeFieldName) _
                     .Append(".HeaderCell.value = """).Append(field.RuntimeFieldName).Append("""") _
                     .Append(vbCrLf)

            sb.Append(vbTab).Append("").Append(field.RuntimeFieldName) _
                     .Append(".DefaultCellStyle.Alignment = "). _
                        Append(getFieldAlignment(field)). _
                        Append(vbCrLf)

            sb.Append(vbTab).Append("").Append(field.RuntimeFieldName) _
                     .Append(".HeaderCell.Style.Alignment = "). _
                        Append(getFieldAlignment(field)). _
                        Append(vbCrLf)


            sb.Append(vbTab).Append("").Append(field.RuntimeFieldName) _
                     .Append(".Width = "). _
                        Append(getFieldWidth(field)). _
                        Append(vbCrLf)

            sb.Append(vbTab).Append("").Append(field.RuntimeFieldName) _
                     .Append(".Visible = "). _
                        Append((field.isPrimaryKey = False)). _
                        Append(vbCrLf)

            If String.IsNullOrEmpty(getFieldFormat(field)) = False Then
                sb.Append(vbTab).Append("").Append(field.RuntimeFieldName) _
                         .Append(".CellTemplate.Style.Format = """).Append(getFieldFormat(field)) _
                         .Append("""") _
                          .Append(vbCrLf)
            End If

            'sb.Append(vbTab).Append("grid.Columns.Add(").Append(""). _
            '    Append(field.RuntimeFieldName).Append(")").Append(vbCrLf)

            sb.Append(vbTab).Append("'**** End Setup of column: ").Append(field.RuntimeFieldName). _
                Append(vbCrLf).Append(vbCrLf).Append(vbCrLf)

            Return sb.ToString

        End Function

        Private Function getFieldFormat(ByVal field As DBField) As String

            If field.isDate Then
                Return "d"
            ElseIf field.isDecimal Then
                Return "C"
            Else
                Return String.Empty
            End If

        End Function
        Private Function getFieldAlignment(ByVal field As DBField) As String

            If field.isBoolean Then
                Return "DataGridViewContentAlignment.MiddleCenter"

            ElseIf field.isLookup = False AndAlso (field.isDecimal OrElse field.isInteger) Then
                Return "DataGridViewContentAlignment.TopRight"

            Else
                Return "DataGridViewContentAlignment.TopLeft"
            End If

        End Function

        Private Function getFieldWidth(ByVal field As DBField) As String

            If field.isBoolean Then
                Return "100"

            ElseIf field.isLookup Then
                Return "100"

            ElseIf field.isDecimal OrElse field.isInteger Then
                Return "50"

            ElseIf field.isDate Then
                Return "100"
            Else
                Select Case field.Precision
                    Case 0 To 100
                        Return "50"
                    Case 100 To 255
                        Return "100"
                    Case Else
                        Return "200"
                End Select
            End If

        End Function

    End Class

End Namespace
