
Imports org.codegen.win.controls
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.CompilerServices
Imports System
Imports System.Collections
Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Drawing
Imports System.Runtime.CompilerServices
Imports System.Windows.Forms

Namespace CGLGrid
    <DesignerGenerated()> _
    Public Class ucGridFilter
        Inherits UserControl

        ' Events
        Public Event filterSQLChanged()

        ' Fields
        Private _dbconn As DBUtils
        Private _filtersql As String

        Private components As IContainer

        ' Methods
        Public Sub New()

            AddHandler MyBase.ControlAdded, AddressOf Me.ucGridFilter_ControlAdded
            AddHandler MyBase.Load, AddressOf Me.ucGridFilter_Load
            Me.InitializeComponent()

        End Sub

        Public Overridable Sub addToSQL(ByRef sql As String, ByVal c As Control)

            If TypeOf c Is CGComboBox Then
                Dim box As CGComboBox = DirectCast(c, CGComboBox)
                If ((CStr(box.Value) <> String.Empty) AndAlso (CStr(box.Value) <> "0")) Then
                    sql = (sql & " and " & box.DBFieldName & "=")
                    'If (box.DBQuoteType = dbQuoteType.QUOTE_TYPE_STRING) Then
                    '    sql = (sql & Me._dbconn.quote((box.Value)))
                    'Else
                    '    sql = CStr(Operators.ConcatenateObject(sql, box.Value))
                    'End If
                End If
            ElseIf TypeOf c Is ucDateRange Then
                Dim range As ucDateRange = DirectCast(c, ucDateRange)
                If range.isValidDateRange Then
                    sql = String.Concat(New String() {sql, " and ", range.DBFieldName, " between ", Me._dbconn.quoteDate(range.dDatefrom), " AND ", Me._dbconn.quoteDate(range.dDateTo)})
                End If

            ElseIf TypeOf c Is CGIntTextBox Then
                If IsNumeric(DirectCast(c, CGTextBox).value) Then
                    sql = String.Concat(New String() {sql, " and ", _
                                                     DirectCast(c, CGIntTextBox).DBFieldName, "=", _
                                                    CStr(DirectCast(c, CGIntTextBox).value)})
                End If

            ElseIf TypeOf c Is CGTextBox Then
                If CStr(DirectCast(c, CGTextBox).value).Trim <> String.Empty Then
                    sql = String.Concat(New String() _
                        {sql, " and ", DirectCast(c, CGTextBox).DBFieldName, "=", Me._dbconn.quote(DirectCast(c, CGTextBox).value)})
                End If

            ElseIf (TypeOf c Is CGDateTextBox AndAlso IsDate(DirectCast(c, CGTextBox).value)) Then
                sql = String.Concat(New String() {sql, " and ", _
                                                  DirectCast(c, CGTextBox).DBFieldName, "=", _
                                                  CStr(DirectCast(c, CGTextBox).value)})

            End If
        End Sub

        Public Sub requery()
            Call builtSQL()
        End Sub

        Protected Overridable Sub builtSQL()

            Dim sql As String = " where 1=1 "
            Dim control As Control
            For Each control In Me.Controls
                Me.addToSQL(sql, control)
            Next

            Me._filtersql = sql
            Me.raiseFilterChanged()

        End Sub

        <DebuggerNonUserCode()> _
        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            Try
                If (disposing AndAlso (Not Me.components Is Nothing)) Then
                    Me.components.Dispose()
                End If
            Finally
                MyBase.Dispose(disposing)
            End Try
        End Sub

        Private Sub filter_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs)
            If (e.KeyCode = Keys.Return) Then
                Me.builtSQL()
            End If
        End Sub

        Public Sub filterfield_AfterUpdate(ByVal sender As Object, ByVal e As EventArgs)
            Me.builtSQL()
        End Sub

        <DebuggerStepThrough()> _
        Private Sub InitializeComponent()
            Me.SuspendLayout()
            Dim ef As New SizeF(6.0!, 13.0!)
            Me.AutoScaleDimensions = ef
            Me.AutoScaleMode = AutoScaleMode.Font
            Me.Name = "ucGridFilter"
            Dim size As New Size(150, &H5C)
            Me.Size = size
            Me.ResumeLayout(False)
        End Sub

        Public Sub raiseFilterChanged()

            RaiseEvent filterSQLChanged()

        End Sub

        Public Overridable Sub setGridSQLStatement(ByRef sql As String)
        End Sub

        Private Sub ucGridFilter_ControlAdded(ByVal sender As Object, ByVal e As ControlEventArgs)

            If TypeOf e.Control Is CGComboBox Then
                Dim control As CGComboBox = DirectCast(e.Control, CGComboBox)
                AddHandler control.SelectedValueChanged, AddressOf Me.filterfield_AfterUpdate

            ElseIf TypeOf e.Control Is ucDateRange Then
                Dim range As ucDateRange = DirectCast(e.Control, ucDateRange)
                AddHandler range.dtRangeChangedEvent, AddressOf Me.filterfield_AfterUpdate
                AddHandler range.dtKeyDownEvent, AddressOf Me.filter_KeyDown

            ElseIf TypeOf e.Control Is CheckBox Then
                AddHandler DirectCast(e.Control, CheckBox).CheckedChanged, New EventHandler(AddressOf Me.filterfield_AfterUpdate)

            ElseIf TypeOf e.Control Is CGTextBox Then
                Dim box2 As CGTextBox = DirectCast(e.Control, CGTextBox)
                AddHandler box2.LostFocus, AddressOf Me.filterfield_AfterUpdate
                AddHandler box2.KeyDown, AddressOf Me.filter_KeyDown
            End If

        End Sub

        Private Sub ucGridFilter_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

            Dim c As Control
            For Each c In Me.Controls
                If TypeOf (c) Is CGComboBox Then
                    If DirectCast(c, CGComboBox).datasourceSQL <> String.Empty Then

                        DirectCast(c, CGComboBox).loadListFromDB(Me._dbconn)
                    End If
                End If
            Next

        End Sub


        ' Properties
        Friend WriteOnly Property dbConn() As DBUtils
            Set(ByVal value As DBUtils)
                Me._dbconn = value
            End Set
        End Property

        Public ReadOnly Property filterWhereClause() As String
            Get
                Return Me._filtersql
            End Get
        End Property


       
    End Class
End Namespace

