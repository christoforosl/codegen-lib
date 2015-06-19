Imports System.Collections.Generic

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
            Dim cSQLStmts As DBMapperStatementsFile = DBMapperStatementsFile.getStmtsFromResource("org.codegen.lib.SqlTemplate.txt")

            Call cSQLStmts.setStatement("selectall", sqlGen.getSelectStatement(), ModelGenerator.Current.DbConnStringDialect)
            
            If (Not ogen.DbTable().isReadOnly()) Then
                Call cSQLStmts.setStatement("delete", sqlGen.deleteStatement(), ModelGenerator.Current.DbConnStringDialect)
                Call cSQLStmts.setStatement("insert", sqlGen.insertStatement(), ModelGenerator.Current.DbConnStringDialect)
                Call cSQLStmts.setStatement("update", sqlGen.updateStatement(), ModelGenerator.Current.DbConnStringDialect)
            End If
            Me.generatedCode = cSQLStmts.getXML

        End Sub

        Public Overrides Function generatedFilename() As String
            Return Me.ClassName & "Sql.xml"
        End Function

    End Class
End Namespace
