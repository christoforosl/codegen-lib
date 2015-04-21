Namespace FileComponents

    ''' <summary>
    ''' Intrerface representing a file included in vb.net project
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface IVBProjectIncludedFile
        ''' <summary>
        ''' This is the complete path + file name, relative to the root of the project
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function FileNameForProject() As String

        ''' <summary>
        ''' The file name without the path
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function FileName() As String
    End Interface

End Namespace
