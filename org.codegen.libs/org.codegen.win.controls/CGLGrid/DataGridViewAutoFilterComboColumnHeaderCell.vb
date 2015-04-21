Imports Microsoft.VisualBasic.CallType

Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Diagnostics
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports System.Windows.Forms.VisualStyles
Imports System.Collections
Imports System.Reflection


''' <summary>
''' 
''' Provides a drop-down filter list in a DataGridViewColumnHeaderCell.
''' </summary>
Public Class DataGridViewAutoFilterComboColumnHeaderCell
    Inherits DataGridViewAutoFilterColumnHeaderCell

    Protected Overrides Sub PopulateFiltersData(ByVal data As BindingSource)

        Me.filters.Clear()
        Dim cbo As DataGridViewComboBoxColumn = TryCast(Me.DataGridView.Columns(Me.ColumnIndex), DataGridViewComboBoxColumn)
        If cbo Is Nothing Then
            Throw New ApplicationException("Column must be of type DataGridViewComboBoxColumn")
        End If

        If TypeOf cbo.DataSource Is IList Then

            Dim dsList As IList = CType(cbo.DataSource, IList)
            If dsList.Count > 0 Then
                For Each dr As Object In dsList
                    Dim displ As String = CStr(CallByName(dr, cbo.DisplayMember, [Get], Nothing))
                    Dim val As String = CStr(CallByName(dr, cbo.ValueMember, [Get], Nothing))
                    If Me.filters.Contains(displ) = False Then
                        Me.filters.Add(displ, val)
                    End If

                Next
            End If

        ElseIf TypeOf cbo.DataSource Is DataTable Then

            For Each dr As DataRow In CType(cbo.DataSource, DataTable).Rows
                Me.filters.Add(dr.Item(cbo.DisplayMember), CStr(dr.Item(cbo.ValueMember)))
            Next
        End If

    End Sub


End Class
