
Imports System.IO
Imports System.Collections.Generic
Imports org.codegen.lib.codeGen.FileComponents

Public Interface IFileGroupLoader
    Function loadFileGroups(ByVal obj As IObjectToGenerate) As Dictionary(Of String, IGeneratedFileComponent)
End Interface


''' <summary>
''' Abstracts the Loading of the group of files to be generated for each 
''' object to generate.  This allows for customization of file groups
''' </summary>
''' <remarks></remarks>
Public Class FileGroupLoader
    Implements IFileGroupLoader

    Public Function loadFileGroups(ByVal obj As IObjectToGenerate) As Dictionary(Of String, IGeneratedFileComponent) Implements IFileGroupLoader.loadFileGroups

        Dim fg As Dictionary(Of String, IGeneratedFileComponent) = New Dictionary(Of String, IGeneratedFileComponent)

        Dim moClassBase As ModelObjectBaseFileComponent = New ModelObjectBaseFileComponent(obj)
        If ModelGenerator.Current.dotNetLanguage = ModelGenerator.enumLanguage.VB Then
            moClassBase.templateFileName = "org.codegen.lib.codeGen.ModelBase2.visualBasic.txt"
        Else
            moClassBase.templateFileName = "org.codegen.lib.codeGen.ModelBase2.csharp.txt"

        End If


        moClassBase.WriteFileIf = enumWriteFileIf.IF_CODE_CHANGED
        fg.Add(ModelObjectBaseFileComponent.KEY, moClassBase)

        Dim moClass As ModelObjectFileComponent = New ModelObjectFileComponent(obj)
        If ModelGenerator.Current.dotNetLanguage = ModelGenerator.enumLanguage.VB Then
            moClass.templateFileName = "org.codegen.lib.codeGen.ModelBaseExtender2.visualBasic.txt"
        Else
            moClass.templateFileName = "org.codegen.lib.codeGen.ModelBaseExtender2.csharp.txt"
        End If

        moClass.WriteFileIf = enumWriteFileIf.IF_NOT_EXISTS
        fg.Add(ModelObjectFileComponent.KEY, moClass)

        If obj.GenerateMapper Then
            Dim mapperBaseClass As MapperBaseFileComponent = New MapperBaseFileComponent(obj)
            If ModelGenerator.Current.dotNetLanguage = ModelGenerator.enumLanguage.VB Then
                mapperBaseClass.templateFileName = "org.codegen.lib.codeGen.DBMapperBase.visualBasic.txt"
            Else
                mapperBaseClass.templateFileName = "org.codegen.lib.codeGen.DBMapperBase.csharp.txt"
            End If
            mapperBaseClass.WriteFileIf = enumWriteFileIf.IF_CODE_CHANGED
            fg.Add(MapperBaseFileComponent.KEY, mapperBaseClass)

            Dim sqlFile As SQLStatementsFileComponent = New SQLStatementsFileComponent(obj)
            sqlFile.templateFileName = "org.codegen.lib.codeGen.SqlTemplate.txt"
            sqlFile.WriteFileIf = enumWriteFileIf.IF_CODE_CHANGED
            fg.Add(SQLStatementsFileComponent.KEY, sqlFile)

        End If

        If String.IsNullOrEmpty(ModelGenerator.Current.ProjectOutputDirTest) = False Then
            Dim testClass As TestFileComponent = New TestFileComponent(obj)
            If ModelGenerator.Current.dotNetLanguage = ModelGenerator.enumLanguage.VB Then
                testClass.templateFileName = "org.codegen.lib.codeGen.TestTemplate.visualbasic.txt"
            Else
                testClass.templateFileName = "org.codegen.lib.codeGen.TestTemplate.csharp.txt"
            End If

            testClass.WriteFileIf = enumWriteFileIf.IF_CODE_CHANGED
            fg.Add(TestFileComponent.KEY, testClass)
        End If

            If String.IsNullOrEmpty(ModelGenerator.Current.ProjectOutputDirUI) = False AndAlso _
                                    obj.GenerateUI Then

                Dim lstForm As New ListWinFormFileComponent(obj)
                lstForm.WriteFileIf = enumWriteFileIf.IF_CODE_CHANGED
                fg.Add(ListWinFormFileComponent.KEY, lstForm)

                Dim editFrm As New EditWinFormFileComponent(obj)
                editFrm.WriteFileIf = enumWriteFileIf.IF_CODE_CHANGED
                fg.Add(EditWinFormFileComponent.KEY, editFrm)

                Dim lstControl As New ListWinCtlFileComponent(obj)
                lstControl.WriteFileIf = enumWriteFileIf.IF_CODE_CHANGED
                fg.Add(ListWinCtlFileComponent.KEY, lstControl)

                Dim editControl As New EditWinCtlFileComponent(obj)
                editControl.WriteFileIf = enumWriteFileIf.IF_CODE_CHANGED
                fg.Add(EditWinCtlFileComponent.KEY, editControl)

            End If

            Return fg

    End Function

End Class
