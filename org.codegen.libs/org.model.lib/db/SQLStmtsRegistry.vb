Imports org.model.lib.DBMapper
Imports org.model.lib.DBMapperStatementsFile

''' <summary>
''' Provides sql statements from xml files.
''' Keeps a registry of files and statements.
''' To get a statement, call SQLStmtsRegistry.getStatement(filekey,statemtKey)
''' </summary>
''' <remarks></remarks>
Public Class SQLStmtsRegistry

    Private Shared _dialect As DBUtils.enumSqlDialect?
    Private Shared _xmlStatementFiles As System.Collections.Generic.Dictionary(Of String, DBMapperStatementsFile)

    Shared Sub New()
        _xmlStatementFiles = New System.Collections.Generic.Dictionary(Of String, DBMapperStatementsFile)
    End Sub


    Public Shared Function getStatement(ByVal dbmapper As System.Type, ByVal statemtKey As StmtType, _
       ByVal sqlDialect As DBUtils.enumSqlDialect, _
       ByVal ParamArray pParams() As String) As String

        Dim cStmts As DBMapperStatementsFile
        Dim filekey As String = dbmapper.FullName()

        SyncLock _xmlStatementFiles

            If _xmlStatementFiles.ContainsKey(filekey) = False Then
                cStmts = DBMapperStatementsFile.getStmtsForMapper(dbmapper)
                Call _xmlStatementFiles.Add(filekey, cStmts)
            Else
                cStmts = _xmlStatementFiles.Item(filekey)
            End If


        End SyncLock

        Return cStmts.getStatement(statemtKey, sqlDialect, pParams)

    End Function


End Class
