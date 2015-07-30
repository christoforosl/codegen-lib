Imports System.ComponentModel
Imports Microsoft.VisualBasic.CompilerServices

Namespace Grid

    Public Class CGSQLGrid
        Inherits CGBaseGrid

        ' Methods
        Public Sub New()
            MyBase.New()

        End Sub

        Private lastLoadedSQL As String = String.Empty

        Private Function buildStatement() As String

            Dim retSQL As String = String.Empty
            Dim str As String = String.Empty

           If (Me._SelectSQLStatement <> String.Empty) Then
				retSQL = Me._SelectSQLStatement

			ElseIf (retSQL = String.Empty) Then

				str = Me.gpSelectFields

				If str.EndsWith(",") Then
					str = Strings.Mid(str, 1, (Strings.Len(str) - 1))
				End If
				If (String.IsNullOrEmpty(str)) Then
					str = "*"
				End If

				retSQL = String.Concat(New String() {"SELECT ", str, " FROM ", Me.gpSelectFrom, " ", Me.gpWhereclause})
			End If

			If ((retSQL.ToLower.IndexOf("where") = -1) And (Me.gpWhereclause.Trim <> String.Empty)) Then
				If (retSQL.ToLower.IndexOf("order by") > -1) Then
					retSQL = String.Concat(New String() {retSQL.Substring(0, retSQL.ToLower.IndexOf("order by")), " ", Me.gpWhereclause, " ", retSQL.Substring(retSQL.ToLower.IndexOf("order by"))})
				Else
					retSQL = String.Format("{0} {1}", retSQL, Me.gpWhereclause)
				End If
			End If

			Return retSQL

        End Function


        ''' <summary>
        ''' Binds the grid to the data
        ''' </summary>
        ''' <remarks></remarks>
        Protected Overrides Sub bindToData()
            If (Me.DesignMode = False) Then
                Me.DataSource = Nothing
                Me.BindingSource = New BindingSource
                Me.BindingSource.DataSource = DBUtils.Current.getDataTable(Me.buildStatement, "table1")
                Me.DataSource = Me.BindingSource
                Me.lastLoadedSQL = Me.buildStatement
            End If

        End Sub

      
        ' Properties
        <Browsable(False)> _
        Public ReadOnly Property gridBindingSource() As BindingSource
            Get
                Return BindingSource
            End Get
        End Property

        ' Properties
        <Browsable(False), Description("Returns the value of the cell specified in iRowIdx and iColIdx parameters.")> _
        Public ReadOnly Property cellValue(ByVal iRowIdx As Integer, ByVal iColIdx As Integer) As String
            Get
                If Information.IsDBNull((me.Item(iColIdx, iRowIdx).Value)) Then
                    Return String.Empty
                End If
                Return CStr(me.Item(iColIdx, iRowIdx).Value)
            End Get
        End Property

        <Description("Comma separated Fields to select. if rempty, *.")> _
        Public Property gpSelectFields() As String = "*"

        <Description("Data Table name to get data from.")> _
        Public Property gpSelectFrom() As String

        ''' <summary>
        ''' Backing field of property gpWhereclause
        ''' </summary>
        ''' <remarks></remarks>
        Private _SelectWhereClause As String = String.Empty

        <Description("Gets/Sets the where clause, without the ""WHERE"".")> _
        Public Property gpWhereclause() As String
            Get
                If ((Strings.Trim(Me._SelectWhereClause) <> String.Empty) AndAlso _
                     Not Strings.Trim(Me._SelectWhereClause).ToUpper.StartsWith("WHERE")) Then

                    Me._SelectWhereClause = "WHERE " & Me._SelectWhereClause
                End If

                If Strings.Trim(Me._SelectWhereClause).ToUpper.Equals("WHERE") Then
                    Return String.Empty
                Else
                    Return " " & Me._SelectWhereClause
                End If

            End Get
            Set(ByVal Value As String)
                Me._SelectWhereClause = Value
            End Set
        End Property



        <Description("Sets/Gets the SQL statement used to load data to the grid."), Browsable(True)> _
        Public Property SelectSQLStatement() As String

    End Class
End Namespace