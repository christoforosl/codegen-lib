Imports System.IO
Imports System.Text.RegularExpressions
Imports System.Collections.Generic


''' <summary>
''' Decorator pattern: Another component interface
''' </summary>
''' <remarks></remarks>
Public Interface IGeneratedFileComponent

    Sub generateCode()
    Sub writeFile()

    Property WriteFileIf As enumWriteFileIf

    Function Name() As String
    Function generatedFilename() As String
    Function generatedFilenameWithPath() As String

End Interface
