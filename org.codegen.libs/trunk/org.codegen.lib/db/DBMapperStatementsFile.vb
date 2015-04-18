Option Strict On

Imports System.IO
Imports System.IO.File
Imports System.Xml
Imports System.Configuration
Imports System.Reflection
Imports System.Threading

Public Class DBMapperStatementsFile

	Private domDs As DataSet
	Private dv As DataView
	'Private _sqlStream As Stream
	'   Private _stmtsFile As String

	Private Const CONFIG_SQL_DIALECT As String = "sqlDialect"
	Private Const CONFIG_SQL_STMTS_FILE As String = "sqlStatementFile"

	Public Shared Function getStmtsFromResource(ByVal resourceName As String) As DBMapperStatementsFile

		Dim loadAsembly As Assembly = Assembly.GetExecutingAssembly
		Return getStmtsFromResource(loadAsembly, resourceName)

	End Function

	Public Shared Function getStmtsForMapper(ByVal dbmapper As System.Type) As DBMapperStatementsFile


		Dim loadAsembly As Assembly = Assembly.GetAssembly(dbmapper)
		Dim resourceName As String = dbmapper.FullName
		Return getStmtsFromResource(loadAsembly, resourceName)

	End Function

	Public Shared Function getStmtsFromResource(loadAsembly As Assembly, resourceName As String) As DBMapperStatementsFile
		Dim cSQLStmts As New DBMapperStatementsFile
		Using sqlStream As Stream = loadAsembly.GetManifestResourceStream(resourceName)

			If sqlStream Is Nothing = True Then
				Throw New ApplicationException("Could not load SQL Statements from " & resourceName & _
				 ". Make sure that the file is marked as Embedded Resource")
			End If

			cSQLStmts.loadStatements(sqlStream)

		End Using

		Return cSQLStmts

	End Function


	Private Sub New()

	End Sub

	Private Sub loadStatements(_sqlStream As Stream)

		If _sqlStream Is Nothing = False Then
			_sqlStream.Position = 0	'rewind the stream!!
		End If

		domDs = New DataSet
		domDs.ReadXml(_sqlStream)

		If domDs.Tables(0).Columns(1).ColumnName = "mssql" Then
			'old schema, must change to new!
			domDs.Tables(0).Columns(1).ColumnName = DBUtils.enumSqlDialect.MSSQL.ToString
			domDs.Tables(0).Columns(2).ColumnName = DBUtils.enumSqlDialect.ORACLE.ToString
			domDs.Tables(0).Columns(3).ColumnName = DBUtils.enumSqlDialect.JET.ToString
		End If
		'If domDs.Tables(0).Columns(2).ColumnName = DBUtilsBase.enumSqlDialect.ORACLE.ToString Then
		'    domDs.Tables(0).Columns.Remove(domDs.Tables(0).Columns(2))
		'End If
		Dim keys(0) As DataColumn
		keys(0) = domDs.Tables(0).Columns(0)
		dv = domDs.Tables(0).DefaultView
		dv.Sort = domDs.Tables(0).Columns(0).ColumnName

	End Sub

	Public Function getStatement(ByVal key As String, ByVal dialect As DBUtils.enumSqlDialect, ByVal ParamArray params() As String) As String

		Dim i As Integer
		Dim stm As String

		stm = String.Empty

		i = Me.dv.Find(key)
		If i > -1 Then
			stm = NullChecker.strNull(Me.dv.Item(i).Item(dialect.ToString))
			If stm Is Nothing OrElse stm = String.Empty Then
				stm = NullChecker.strNull(Me.dv.Item(i).Item(DBUtils.enumSqlDialect.COMMON))
			End If

			If stm = String.Empty Then
				Throw New ApplicationException("Statement Key:" & key & " not found")
			End If

			If params Is Nothing OrElse params.Length = 0 Then
				'just return the stmmt
			Else
				stm = String.Format(stm, params)
			End If

			If stm.IndexOf(" ") = -1 Then 'single word, assume table name
				stm = "select * from " & stm
			End If
		End If
		Return stm

	End Function

	Public Sub setStatement(ByVal skey As String, _
	   ByVal stnt As String, _
	   Optional ByVal isqlDialect As DBUtils.enumSqlDialect = DBUtils.enumSqlDialect.COMMON)

		Dim i As Integer
		Dim stm As String

		stm = String.Empty

		i = Me.dv.Find(skey)
		If i > -1 Then
			Me.dv.Item(i).Item(isqlDialect.ToString) = stnt
		Else
			Dim dr As DataRow = Me.domDs.Tables(0).NewRow
			dr("key") = skey
			dr(isqlDialect.ToString) = stnt
			Me.domDs.Tables(0).Rows.Add(dr)
		End If

		Me.domDs.AcceptChanges()


	End Sub

	Public Function getXML() As String

		Me.domDs.DataSetName = "SQLStatements"
		Me.domDs.AcceptChanges()
		Return Me.domDs.GetXml()

	End Function

	'Public Sub writeXML()

	'    Me.domDs.DataSetName = "SQLStatements"
	'    Me.domDs.AcceptChanges()
	'    _sqlStream.Close()
	'    Me.domDs.WriteXml(statementsFileFromPath, XmlWriteMode.IgnoreSchema)

	'End Sub

	Public Sub writeXML(ByVal sPath As String)

		Me.domDs.DataSetName = "SQLStatements"
		Me.domDs.AcceptChanges()
		Me.domDs.WriteXml(sPath, XmlWriteMode.IgnoreSchema)

	End Sub



End Class
