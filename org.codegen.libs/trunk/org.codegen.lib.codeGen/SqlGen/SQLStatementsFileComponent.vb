Imports System.Collections.Generic
Imports org.codegen.lib.codeGen
Imports System.Reflection
Imports System.IO
Imports System.Xml


Namespace FileComponents
    ''' <summary>
    ''' Generates SQL Statement file.  Inherits from DBMapper because 
    ''' the namespace is the same, and the class name is the same
    ''' </summary>
    ''' <remarks></remarks>
    Public Class SQLStatementsFileComponent
        Inherits MapperBaseFileComponent

        Public Sub New(ByVal inobjGen As IObjectToGenerate)
            MyBase.New(inobjGen)
        End Sub

        Public Overloads Shared Function KEY() As String
            Return "SQL"
        End Function

        Public Overrides Sub generateCode()

            Dim ogen As IObjectToGenerate = Me.objectToGenerate

            Dim sqlGen As SQLStmtsGenerator = New SQLStmtsGenerator(ogen.DbTable)
			Dim cSQLStmts As DBMapperStatementsFile = DBMapperStatementsFile.getStmtsFromResource("org.codegen.lib.codeGen.SqlTemplate.txt")

            Call cSQLStmts.setStatement("selectall", sqlGen.getSelectStatement(False, "@"), DBUtils.enumSqlDialect.MSSQL)
            Call cSQLStmts.setStatement("selectone", sqlGen.getSelectStatement(True, "@"), DBUtils.enumSqlDialect.MSSQL)

            Call cSQLStmts.setStatement("selectall", sqlGen.getSelectStatement(False, "@"), DBUtils.enumSqlDialect.JET)
            Call cSQLStmts.setStatement("selectone", sqlGen.getSelectStatement(True, "@"), DBUtils.enumSqlDialect.JET)

            'Call cSQLStmts.setStatement("selectall", me.getSelectStatement(False, ":"), DBUtils.enumSqlDialect.ORACLE)
            'Call cSQLStmts.setStatement("selectone", me.getSelectStatement(True, ":"), DBUtils.enumSqlDialect.ORACLE)

            If (Not ogen.DbTable().isReadOnly()) Then
                Call cSQLStmts.setStatement("delete", sqlGen.deleteStatement("@"), DBUtils.enumSqlDialect.MSSQL)
                Call cSQLStmts.setStatement("delete", sqlGen.deleteStatement("@"), DBUtils.enumSqlDialect.JET)
                'Call cSQLStmts.setStatement("delete", me.deleteStatement(":"), DBUtils.enumSqlDialect.ORACLE)

                Call cSQLStmts.setStatement("insert", sqlGen.insertStatementMSSQL(), DBUtils.enumSqlDialect.MSSQL)
                Call cSQLStmts.setStatement("insert", sqlGen.insertStatementMSSQL(), DBUtils.enumSqlDialect.JET)
                'Call cSQLStmts.setStatement("insert", me.insertStatementOracle(), DBUtils.enumSqlDialect.ORACLE)

                Call cSQLStmts.setStatement("update", sqlGen.updateStatement("@"), DBUtils.enumSqlDialect.MSSQL)
                Call cSQLStmts.setStatement("update", sqlGen.updateStatement("@"), DBUtils.enumSqlDialect.JET)
                'Call cSQLStmts.setStatement("update", me.updateStatement(":"), DBUtils.enumSqlDialect.ORACLE)
            End If
            Me.generatedCode = cSQLStmts.getXML

        End Sub

        Public Overrides Function generatedFilename() As String
            Return Me.ClassName & "Sql.xml"
        End Function

    End Class
End Namespace
