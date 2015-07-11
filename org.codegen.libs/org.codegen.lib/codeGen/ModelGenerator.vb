Imports Microsoft.VisualBasic
Imports System.Collections
Imports System.Collections.Generic
Imports System.IO
Imports org.codegen.lib
Imports System.Text.RegularExpressions

Imports System.Reflection
Imports System.Text
Imports System.Threading

'''
''' <summary>
''' Generates ModelObject derived classes based 
''' on the ModelObject and Mapper objects 
''' </summary>
''' 
Public Class ModelGenerator

    Public Enum enumLanguage
        VB = 1
        CSHARP = 2
    End Enum


    Public Enum enumVERSION
        ONE = 1
        TWO = 2
    End Enum

    Private Const STR_CODE_GEN_ASSEMBLY As String = "org.codegen.lib.codeGen"

    Public Property dotNetLanguage As enumLanguage = enumLanguage.CSHARP
	Public Property FieldPropertyPrefix As String

	Public Property DefaultMapperNameSpace() As String

	Private _GeneratorVersion As Integer
	Private _vbNetProjectFile As String

	Private _template As String
	Private _ProjectOutputDirModel As String
	Private _projectOutputDirTest As String
	Private _ProjectOutputDirUI As String

	Private _dbconn As DBUtils

	Private _IPropertyGenerator As IPropertyGenerator
	Private _xmlFile As DataSet

	Private Shared modelGentor As ModelGenerator ' implement singleton pattern

	Public Property ObjectsToGenerate() As Dictionary(Of String, ObjectToGenerate)
	Public Property XmlFileDataSet() As DataSet
	Public Property NumOfGeneratedFiles() As Integer

	Property NumOfNewGeneratedFiles As Integer
	Property CurrentObjectBeingGenerated As ObjectToGenerate

    Private _systemAssociations As List(Of Association) = New List(Of Association)

	''' <summary>
	''' relative directory to decide paths when using . and .. in output directories
	''' </summary>
	Property relativeDirectory As String

    Property BooleanFieldsCollection As BooleanFieldsCollection

    Property EnumFieldsCollection As EnumFieldsCollection

    Public Sub addAssociation(ByVal ass As Association)
        Me._systemAssociations.Add(ass)
    End Sub

    Public Property SystemAssociations() As List(Of Association)
        Get
            Return _systemAssociations
        End Get
        Private Set(ByVal value As List(Of Association))
            _systemAssociations = value
        End Set
    End Property

	Public Function getObjectOfDataType(ByVal stableName As String) As ObjectToGenerate

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
                        Me.DbConnStringDialect)


			End If

			Return _dbconn
		End Get
		Set(ByVal value As DBUtils)
			_dbconn = value
		End Set
	End Property


    Public Property DbConnStringDialect() As DBUtils.enumSqlDialect
	Public Property NumOfUnchangedFiles() As Integer
	Public Property DbConnString() As String
	Public Property DbConnStringType() As String

#End Region



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

	Public Shared Function resolveRelativePathsAndCheck(ByVal inPath As String, ByVal relativeDirectory As String) As String

		Dim ret As String = inPath
		If String.IsNullOrEmpty(inPath) = False Then
			'**NOTE :must check for .. BEFORE .
			If inPath.StartsWith("..") Then
				Dim dr As New DirectoryInfo(relativeDirectory)
				Dim tmp As String = inPath.Substring(2)
				If tmp.StartsWith("\") = False Then tmp = "\" & tmp
				'dr.Parent.FullName does not return a "\" at the end
				'so make sure that tmp starts with a \
				ret = dr.Parent.FullName & inPath.Substring(2)

			ElseIf inPath.StartsWith(".") Then
				Dim dr As New DirectoryInfo(relativeDirectory)
				ret = dr.FullName & inPath.Substring(1)

			End If

			Dim f As New FileInfo(ret) 'just to make sure that this is a valid path, not if it exists, but it is a valid windows path
		End If

		Return ret

	End Function

	Public Property ProjectOutputDirTest() As String
		Set(ByVal val As String)

			If Not String.IsNullOrEmpty(val) Then
				val = resolveRelativePathsAndCheck(val, Me.relativeDirectory)
				If val.EndsWith("\") = False Then
					val &= "\"
				End If
			End If

			Me._projectOutputDirTest = val

		End Set
		Get
			Return _projectOutputDirTest
		End Get
	End Property

	Public Property ProjectOutputDirUI As String
		Set(ByVal val As String)
			If Not String.IsNullOrEmpty(val) Then
				val = resolveRelativePathsAndCheck(val, Me.relativeDirectory)
				If val.EndsWith("\") = False Then
					val &= "\"
				End If
			End If

			Me._ProjectOutputDirUI = val

		End Set
		Get
			Return _ProjectOutputDirUI
		End Get
	End Property

	Public Property ProjectOutputDirModel() As String
		Set(ByVal val As String)
			If Not String.IsNullOrEmpty(val) Then
				val = resolveRelativePathsAndCheck(val, Me.relativeDirectory)
				If val.EndsWith("\") = False Then
					val &= "\"
				End If
			End If

			Me._ProjectOutputDirModel = val

		End Set
		Get
			Return _ProjectOutputDirModel
		End Get
	End Property

	Public ReadOnly Property IPropertyGenerator() As IPropertyGenerator
		Get
			If _IPropertyGenerator Is Nothing Then
				If Me.dotNetLanguage = enumLanguage.CSHARP Then
					_IPropertyGenerator = New CSharpPropertyGenerator
				Else
					_IPropertyGenerator = New PropertyGenerator
				End If
			End If
			Return _IPropertyGenerator
		End Get
	End Property

	Public Sub addObjectForGeneration(ByVal n As ObjectToGenerate)

		If (Me._ObjectsToGenerate Is Nothing) Then
			Me._ObjectsToGenerate = New Dictionary(Of String, ObjectToGenerate)
		End If
		Dim cphg As String = n.FullyQualifiedClassName
		If Me._ObjectsToGenerate.ContainsKey(cphg) Then
			Throw New ApplicationException(cphg & " already exists!  Class names must be unique in the XML file.")
		Else
			Me._ObjectsToGenerate.Add(cphg, n)
		End If


	End Sub

    Function getAssociationInstance() As Association
        If Me.dotNetLanguage = enumLanguage.CSHARP Then
            Return New CSharpAssociation
        Else
            Return New Association
        End If

    End Function

End Class
