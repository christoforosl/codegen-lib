''' <summary>
''' Provides sql statements from xml files.
''' Keeps a registry of files and statements.
''' To get a statement, call SQLStmtsRegistry.getStatement(filekey,statemtKey)
''' </summary>
''' <remarks></remarks>
Public Class SQLStmtsRegistry

    Private Shared _dialect As DBUtils.enumSqlDialect?
    Private Shared _xmlStatementFiles As System.Collections.Generic.Dictionary(Of String, SQLStmts)

    Shared Sub New()
        _xmlStatementFiles = New System.Collections.Generic.Dictionary(Of String, SQLStmts)
    End Sub


    Public Shared Function getStatement(ByVal filekey As String, _
                                        ByVal statemtKey As String, _
                                        ByVal sqlDialect As DBUtils.enumSqlDialect, _
                                        ByVal ParamArray pParams() As String) As String

        Dim cStmts As SQLStmts

        SyncLock _xmlStatementFiles

            If _xmlStatementFiles.ContainsKey(filekey) = False Then
                cStmts = SQLStmts.getStmtsFromResource(filekey)
                Call _xmlStatementFiles.Add(filekey, cStmts)
            Else
                cStmts = _xmlStatementFiles.Item(filekey)
            End If


        End SyncLock

        Return cStmts.getStatement(statemtKey, sqlDialect, pParams)

    End Function


End Class
