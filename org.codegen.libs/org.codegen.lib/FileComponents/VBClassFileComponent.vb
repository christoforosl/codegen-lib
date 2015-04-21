Imports System.Collections.Generic
Imports org.codegen.lib.codeGen
Imports System.Reflection
Imports System.IO
Imports System.Xml


Namespace FileComponents

    Public MustInherit Class DotNetClassFileComponent
        Inherits GeneratedFileComponent
        Implements IVBProjectIncludedFile

        ''' <summary>
        ''' The file as written in the vb.net project file.
        ''' </summary>
        ''' <remarks></remarks>
        Protected _FileNameForVBProject As String = Nothing

        ''' <summary>
        ''' The class name of the file.
        ''' </summary>
        ''' <remarks></remarks>
        Protected _ClassName As String = Nothing

        ''' <summary>
        ''' The class namespace
        ''' </summary>
        ''' <remarks></remarks>
        Protected _ClassNameSpace As String = Nothing

        Public Sub New(ByRef inObjGen As IObjectToGenerate)
            MyBase.New(inObjGen)
        End Sub

        Public MustOverride Function ClassName() As String
        Public MustOverride Function ClassNamespace() As String

        Public Overridable Function FileNameForProject() As String _
                        Implements IVBProjectIncludedFile.FileNameForProject


            Return Me.ClassNamespace.Replace(".", "\") & "\" & Me.generatedFilename


        End Function

        Public Overrides Function generatedFilenameWithPath() As String

            Dim tmp As String = ModelGenerator.Current().ProjectOutputDirModel & _
                               Me.ClassNamespace().Replace("."c, "\"c) & "\"

            If tmp.EndsWith("\") = False Then tmp &= "\"

            Dim f As New FileInfo(tmp)
            'just to make sure that this is a valid format path

            Return tmp & Me.generatedFilename

        End Function

        Public Overrides Function generatedFilename() As String _
                        Implements IVBProjectIncludedFile.FileName

            If ModelGenerator.Current.dotNetLanguage = ModelGenerator.enumLanguage.CSHARP Then
                Return Me.ClassName & ".cs"
            
            Else
                Return Me.ClassName & ".vb"
            End If



        End Function

        Public Overrides Function Name() As String
            Return Me.ClassNamespace & "." & Me.ClassName
        End Function

        Public Function ClassInterface() As String
            Return "I" & Me.ClassName
        End Function

    End Class

End Namespace
