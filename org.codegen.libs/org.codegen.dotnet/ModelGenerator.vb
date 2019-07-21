Imports System.IO
Imports org.codegen.model.lib.db

'''
''' <summary>
''' Generates ModelObject derived java classeses based 
''' on the ModelObject and Mapper objects 
''' </summary>
''' 
Public Class ModelGenerator

    Public Enum enumVERSION
        ONE = 1
        TWO = 2
    End Enum

    Private Const STR_CODE_GEN_ASSEMBLY As String = "org.codegen.lib.codeGen"

    Private _GeneratorVersion As Integer
    Private _vbNetProjectFile As String

    Private _template As String
    Private _outputDir As String

    Private _dbconn As DBUtils

    Private _IPropertyGenerator As IPropertyGenerator

    Private _updateAssociationEndsIds As New Dictionary(Of String, String())
    Private _xmlFile As DataSet

    Private Shared modelGentor As ModelGenerator ' implement singleton pattern


    Public Property ObjectsToGenerate() As Dictionary(Of String, IObjectToGenerate)
    Public Property XmlFileDataSet() As DataSet
    Public Property NumOfGeneratedFiles() As Integer

    Property NumOfNewGeneratedFiles As Integer

    Property CurrentObjectBeingGenerated As IObjectToGenerate

    Public Function getObjectOfDataType(ByVal stableName As String) As IObjectToGenerate

        'For i As Integer = 0 To Me.ObjectsToGenerate.Count - 1
        '    Dim t As ObjectToGenerate = ObjectsToGenerate.Values(i)
        '    If t.DbTable.TableName.ToLower = stableName.ToLower Then
        '        Return t
        '    End If
        'Next
        If Me.ObjectsToGenerate.ContainsKey(stableName) Then
            Return Me.ObjectsToGenerate.Item(stableName)
        End If

        Return Nothing

    End Function

    Public Shared Function Current() As ModelGenerator

        If (modelGentor Is Nothing) Then
            modelGentor = New ModelGenerator
            modelGentor._GeneratorVersion = 2
        End If

        Return modelGentor

    End Function

    Public Shared Function create(ByVal iversion As enumVERSION) As ModelGenerator

        modelGentor = New ModelGenerator
        modelGentor._GeneratorVersion = iversion

        Return modelGentor

    End Function

#Region "properties"

    Public Property dbConn() As DBUtils
        Get

            If _dbconn Is Nothing Then
                _dbconn = DBUtils.getFromConnString(Me.DbConnString, _
                        DirectCast([Enum].Parse(GetType(DBUtils.enumConnType), Me.DbConnStringType), DBUtils.enumConnType), _
                        DirectCast([Enum].Parse(GetType(DBUtils.enumSqlDialect), Me.DbConnStringDialect), DBUtils.enumSqlDialect))


            End If

            Return _dbconn
        End Get
        Set(ByVal value As DBUtils)
            _dbconn = value
        End Set
    End Property


    Public Property TestClassOutputDir() As String

    Public Property DbConnStringDialect() As String

    Public Property NumOfUnchangedFiles() As Integer

    Public Property DbConnString() As String

    Public Property DbConnStringType() As String



#End Region

    Public Property UpdateAssociationEndsIds() As Dictionary(Of String, String())
        Get
            Return _updateAssociationEndsIds
        End Get
        Set(ByVal value As Dictionary(Of String, String()))
            _updateAssociationEndsIds = value
        End Set
    End Property

    Private Sub New()
        'empty contructor
    End Sub

    Protected Overridable Sub genClassDirectory(ByVal ret As String)
        ' Create a directory; all non-existent ancestor directories are
        ' automatically created
        Dim f As New FileInfo(ret)
        If (Not f.Exists() AndAlso Not Directory.Exists(f.DirectoryName)) Then
            Call System.IO.Directory.CreateDirectory(f.DirectoryName)
        End If
    End Sub


    Public Property OutputDir() As String
        Set(ByVal v As String)

            If v.EndsWith("\") = False Then
                v &= "\"
            End If

            Dim f As New FileInfo(v) 'just to make sure that this is a valid path, not if it exists, but it is a valid windows path
            Me._outputDir = v

        End Set
        Get

            Return _outputDir
        End Get
    End Property

    Public ReadOnly Property IPropertyGenerator() As IPropertyGenerator
        Get

            Return _IPropertyGenerator
        End Get
    End Property

    Public Sub addObjectForGeneration(ByVal n As IObjectToGenerate)

        If (Me._ObjectsToGenerate Is Nothing) Then
            Me._ObjectsToGenerate = New Dictionary(Of String, IObjectToGenerate)
        End If

        Dim cphg As String = n.DbTable.TableName 'FullyQualifiedClassName
        If Me._ObjectsToGenerate.ContainsKey(cphg) Then
            Throw New ApplicationException(cphg & " already exists!  Class names must be unique in the XML file.")
        Else
            Me._ObjectsToGenerate.Add(cphg, n)
        End If


    End Sub

End Class
