
Imports System.IO
Imports System.Collections.Generic
Imports org.codegen.lib.FileComponents

Public Class ObjectToGenerate
    Implements IObjectToGenerate

    Public Property ConstructorCode() As String Implements IObjectToGenerate.ConstructorCode
    Public Property XMLDefinition As DataRow Implements IObjectToGenerate.XMLDefinition

    Public Property FileGroup As Dictionary(Of String, IGeneratedFileComponent) Implements IObjectToGenerate.FileGroup
    Public Property GenerateMapper As Boolean Implements IObjectToGenerate.GenerateMapper
    Public Property GenerateUI As Boolean Implements IObjectToGenerate.GenerateUI
    Public Property DbTable() As IDBTable Implements IObjectToGenerate.DbTable

#Region "Methods"

    Public Overridable Sub setTableName(ByVal sTableName As String, ByVal pkFieldName As String) Implements IObjectToGenerate.setTableName

        Try

            Me.DbTable = New DBTable(sTableName, pkFieldName)


        Catch ex As Exception
            Throw New ApplicationException("Error creating class DBTable:" & sTableName & ":" & ex.Message())
        End Try
    End Sub


#End Region

    Public Function ClassName() As String
        Return DirectCast(Me.FileGroup(ModelObjectFileComponent.KEY), DotNetClassFileComponent).ClassName
    End Function

    Public Function ClassNameSpace() As String
        Return DirectCast(Me.FileGroup(ModelObjectFileComponent.KEY), DotNetClassFileComponent).ClassNamespace
    End Function

    Public Function MapperClassNameSpace() As String
        Return DirectCast(Me.FileGroup(MapperBaseFileComponent.KEY), MapperBaseFileComponent).ClassNamespace
    End Function

    Public Function FullyQualifiedClassName() As String
        Return Me.ClassNameSpace & "." & Me.ClassName
    End Function

    Public Function FullyQualifiedMapperClassName() As String
        Return Me.MapperClassNameSpace & "." & Me.ClassName & MapperBaseFileComponent.STR_DBMAPPER
    End Function

    Public Sub loadFileGroups() Implements IObjectToGenerate.loadFileGroups

        If Me.FileGroup Is Nothing Then
            Me.FileGroup = New FileGroupLoader().loadFileGroups(Me)
        End If

    End Sub

    Public Sub generateCode() Implements IObjectToGenerate.generateCode

        Me.loadFileGroups()

        ModelGenerator.Current.CurrentObjectBeingGenerated = Me

        For i As Integer = 0 To Me.FileGroup.Values.Count - 1

            Me.FileGroup.Values(i).generateCode()
            Me.FileGroup.Values(i).writeFile()

        Next

        ModelGenerator.Current.CurrentObjectBeingGenerated = Nothing

    End Sub

End Class
