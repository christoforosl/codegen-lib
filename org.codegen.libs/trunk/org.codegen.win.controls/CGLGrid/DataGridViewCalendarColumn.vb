Imports System
Imports System.Windows.Forms

Public Class DataGridViewCalendarColumn
    Inherits DataGridViewColumn

    Public Sub New()

        MyBase.New(New CalendarCell())
        Me.SortMode = DataGridViewColumnSortMode.Automatic
        Me.Width = 100

    End Sub

    Public Overrides Property CellTemplate() As DataGridViewCell
        Get
            Return MyBase.CellTemplate
        End Get
        Set(ByVal value As DataGridViewCell)

            ' Ensure that the cell used for the template is a CalendarCell.
            If (value IsNot Nothing) AndAlso _
                            Not value.GetType().IsAssignableFrom(GetType(CalendarCell)) Then
                Throw New InvalidCastException("Must be a CalendarCell")
            End If
            MyBase.CellTemplate = value

        End Set
    End Property

    Public Overrides Property DefaultCellStyle As System.Windows.Forms.DataGridViewCellStyle
        Get
            Return MyBase.DefaultCellStyle
        End Get
        Set(ByVal value As System.Windows.Forms.DataGridViewCellStyle)
            If Not String.IsNullOrEmpty(value.Format) Then
                Me.CellTemplate.Style.Format = value.Format
            End If
            MyBase.DefaultCellStyle = value
        End Set
    End Property
End Class

Public Class CalendarCell
    Inherits DataGridViewTextBoxCell

    Public Sub New()
        ' Use the short date format.
        Me.Style.Format = "d"
    End Sub

    Public Overrides Sub InitializeEditingControl(ByVal rowIndex As Integer, _
        ByVal initialFormattedValue As Object, _
        ByVal dataGridViewCellStyle As DataGridViewCellStyle)

        ' Set the value of the editing control to the current cell value.
        MyBase.InitializeEditingControl(rowIndex, initialFormattedValue, _
            dataGridViewCellStyle)

        Dim ctl As CalendarEditingControl = _
            CType(DataGridView.EditingControl, CalendarEditingControl)

    End Sub
    Public Overrides ReadOnly Property EditType() As Type
        Get
            ' Return the type of the editing contol that CalendarCell uses.
            Return GetType(CalendarEditingControl)
        End Get
    End Property

    Public Overrides ReadOnly Property ValueType() As Type
        Get
            ' Return the type of the value that CalendarCell contains.
            Return GetType(DateTime)
        End Get
    End Property

    Public Overrides ReadOnly Property DefaultNewRowValue() As Object
        Get
            ' Use the current date and time as the default value.
            Return DateTime.Now
        End Get
    End Property

End Class
