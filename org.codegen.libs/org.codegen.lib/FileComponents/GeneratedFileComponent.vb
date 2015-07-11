Imports System.IO
Imports System.Text.RegularExpressions
Imports System.Collections.Generic
Imports org.codegen.lib.Tokens


Namespace FileComponents

    ''' <summary>
    ''' Decorator pattern: This is the component interface
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface IReplacementComponent

        Sub replaceTokens()

    End Interface

    Public MustInherit Class GeneratedFileComponent
        Implements IGeneratedFileComponent, IReplacementComponent

        ''' <summary>
        ''' The template file, if any
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Property templateFileName As String

        ''' <summary>
        ''' the code generated after calling function generateCode()
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Property generatedCode As String

        ''' <summary>
        ''' Reference to IObjectToGenerate object, loaded from the xml
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Property objectToGenerate As IObjectToGenerate

        ''' <summary>
        ''' Switch that decides whether the file will be generated if it has chnages, always or if it does not exist
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property WriteFileIf As enumWriteFileIf = enumWriteFileIf.IF_CODE_CHANGED _
                        Implements IGeneratedFileComponent.WriteFileIf

        Public Overridable Sub generateCode() _
                        Implements IGeneratedFileComponent.generateCode

            Me.generatedCode = Me.loadTemplateCode()
            Me.replaceTokens()

        End Sub

        Public MustOverride Function generatedFilename() As String _
                    Implements IGeneratedFileComponent.generatedFilename

        Public MustOverride Function generatedFilenameWithPath() As String _
                    Implements IGeneratedFileComponent.generatedFilenameWithPath

        Sub New(ByVal inObjGen As IObjectToGenerate)
            Me.objectToGenerate = inObjGen
        End Sub

#Region "IGeneratedFileComponent implementations"

        ''' <summary>
        ''' get existing code of file
        ''' </summary>
        ''' <remarks></remarks>
        Public Function getExistingCode() As String

            If File.Exists(Me.generatedFilenameWithPath) Then
                Return Utilities.TextFromFileGet(Me.generatedFilenameWithPath)
            Else
                Return String.Empty
            End If

        End Function

        Protected Sub writeFile() Implements IGeneratedFileComponent.writeFile

            If Me.WriteFileIf = enumWriteFileIf.IF_CODE_CHANGED Then
                Me.saveFileIfChanged()

            ElseIf Me.WriteFileIf = enumWriteFileIf.ALWAYS Then
                Utilities.TextFromFileSave(Me.generatedFilenameWithPath, Me.generatedCode)

            ElseIf Me.WriteFileIf = enumWriteFileIf.NEVER Then
                'nothing to do!!!

            Else
                Me.saveFileIfNotExists()
            End If

        End Sub
#End Region

#Region "Class IO Operations"

        Public Function loadTemplateCode() As String

            If (String.IsNullOrEmpty(Me.templateFileName)) Then
                Throw New ApplicationException("No Code Template specified for: " & Me.GetType.Name & " :" & Me.Name)
            End If

            Dim ret As String = String.Empty
            Try

                ret = Utilities.getResourceFileText(GetType(GeneratedFileComponent), Me.templateFileName)
            Catch er As Exception
                Throw New ApplicationException("Error getting resource:" & Me.templateFileName)
            End Try

            Return ret
        End Function

        Protected Function saveFile() As Boolean

            If File.Exists(Me.generatedFilenameWithPath) Then
                ModelGenerator.Current.NumOfNewGeneratedFiles += 1

            Else
                ModelGenerator.Current.NumOfGeneratedFiles += 1
                Return True

            End If

            Utilities.TextFromFileSave(Me.generatedFilenameWithPath, Me.generatedCode)

        End Function

        Protected Function saveFileIfNotExists() As Boolean

            If File.Exists(Me.generatedFilenameWithPath) Then
                'Console.WriteLine("Skipped: " & f.Name & " : Already Exists")
                ModelGenerator.Current.NumOfUnchangedFiles += 1
                Return False

            Else
                Utilities.TextFromFileSave(Me.generatedFilenameWithPath, Me.generatedCode)
                ModelGenerator.Current.NumOfNewGeneratedFiles += 1
                Return True

            End If

        End Function

        Protected Function saveFileIfChanged() As Boolean
            'saves the file only if changed
            Const STR_COMMENTS As String = "</comments>"

            Dim compCode As String = Me.generatedCode
            Dim f As FileInfo = New FileInfo(Me.generatedFilenameWithPath)
            Dim ret As Boolean = False

            If f.Exists() Then

                Dim r As Regex = New Regex("\s*")

                Dim sExCode As String = Me.getExistingCode

                If sExCode.IndexOf(STR_COMMENTS) > 0 Then
                    sExCode = sExCode.Substring(sExCode.IndexOf(STR_COMMENTS))
                End If

                If compCode.IndexOf(STR_COMMENTS) > 0 Then
                    compCode = compCode.Substring(compCode.IndexOf(STR_COMMENTS))
                End If

                sExCode = r.Replace(sExCode, String.Empty)
                compCode = r.Replace(compCode, String.Empty)

                If ModelGenerator.Current.ignoreCase AndAlso sExCode.ToLower = compCode.ToLower Then
                    'Console.WriteLine("Skipped: " & f.Name & " : No Code Changes")
                    ModelGenerator.Current.NumOfUnchangedFiles += 1

                ElseIf (Not ModelGenerator.Current.ignoreCase) AndAlso sExCode = compCode Then
                    ModelGenerator.Current.NumOfUnchangedFiles += 1
                Else
                    Console.WriteLine("Write: " & f.Name & " : Code Changed")
                    Utilities.TextFromFileSave(Me.generatedFilenameWithPath, Me.generatedCode)
                    ModelGenerator.Current.NumOfGeneratedFiles += 1
                    ret = True
                End If

            Else
                Console.WriteLine("Write: " & f.Name & " : New File")

                If (f.Directory.Exists = False) Then
                    Directory.CreateDirectory(f.Directory.FullName)
                End If
                Utilities.TextFromFileSave(Me.generatedFilenameWithPath, Me.generatedCode)
                ModelGenerator.Current.NumOfNewGeneratedFiles += 1
                ret = True
            End If

            Return ret

        End Function
#End Region


        ''' <summary>
        ''' Loops thru the collection of Tokens.IReplacementToken and does the replacements
        ''' </summary>
        ''' <remarks></remarks>
        Public Overridable Sub replaceTokens() Implements IReplacementComponent.replaceTokens

            Dim tmp As List(Of IReplacementToken) = Me.getReplacementTokens
            For Each tk As IReplacementToken In tmp
                Me.generatedCode = tk.replaceToken(Me.generatedCode, Me.objectToGenerate)
            Next
        End Sub

        Public MustOverride Function Name() As String Implements IGeneratedFileComponent.Name

        Public Function getReplacementTokens() As List(Of Tokens.IReplacementToken)

            Dim tmp As New List(Of Tokens.IReplacementToken)
            Dim sattr As System.Attribute = Attribute.GetCustomAttribute(Me.GetType, GetType(ReplacementTokenAttribute))

            ' Show ReplacementTokenAttribute information for the testClass
            Dim attr As ReplacementTokenAttribute = CType(sattr, ReplacementTokenAttribute)

            If Not (attr Is Nothing) Then

                For Each tk As Type In attr.replacementTokens
                    'If tk Is GetType(Tokens.IReplacementToken) Then
                    tmp.Add(CType(Activator.CreateInstance(tk), Tokens.IReplacementToken))
                    'End If
                Next

            End If
            Return tmp

        End Function

    End Class

End Namespace