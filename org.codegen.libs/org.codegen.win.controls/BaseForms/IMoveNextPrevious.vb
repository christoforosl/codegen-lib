Public Interface IMoveNextPrevious

    Sub MoveNext()
    Sub MovePrevious()
    Sub MoveCurrent()

End Interface

Public Class GridMoveNextPrevious
    Implements IMoveNextPrevious

    Private parentGrid As Grid.CGBaseGrid
    Private parentForm As frmBaseEdit

    Sub New(ByVal _parentForm As frmBaseEdit, ByVal _parentGrid As Grid.CGBaseGrid)
        Me.parentGrid = _parentGrid
        Me.parentForm = _parentForm
    End Sub

    Public Sub MoveNext() Implements IMoveNextPrevious.MoveNext

        If Me.parentGrid.SelectedRows.Count > 0 Then

            Dim selRowIdx As Integer = Me.parentGrid.SelectedRows(0).Index
            Dim newRowIdx As Integer = selRowIdx + 1

            If newRowIdx > (Me.parentGrid.Rows.Count - 1) Then
                newRowIdx = 0
            End If

            If parentForm.SaveBeforeMove Then
                Me.parentGrid.Rows(selRowIdx).Selected = False
                Me.parentGrid.Rows(newRowIdx).Selected = True

                parentForm.IdValue = Me.parentGrid.IdValue
                parentForm.LoadData()
            End If

        End If
    End Sub

    Public Sub MovePrevious() Implements IMoveNextPrevious.MovePrevious

        If Me.parentGrid.SelectedRows.Count > 0 Then

            Dim selRowIdx As Integer = Me.parentGrid.SelectedRows(0).Index
            Dim newRowIdx As Integer = selRowIdx - 1
            If (selRowIdx = 0) Then
                newRowIdx = Me.parentGrid.RowCount - 1
            End If

            If Me.parentForm.SaveBeforeMove Then
                'Me.SaveData()
                Me.parentGrid.Rows(selRowIdx).Selected = False
                Me.parentGrid.Rows(newRowIdx).Selected = True
                Me.parentForm.IdValue = Me.parentGrid.IdValue
                Me.parentForm.LoadData()

            End If

        End If

    End Sub

    Public Sub MoveCurrent() Implements IMoveNextPrevious.MoveCurrent

    End Sub
End Class
