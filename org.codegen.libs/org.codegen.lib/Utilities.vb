Option Strict On

Imports System.IO
Imports System.Reflection
Imports System.Text
Imports System.Threading

''' <summary>
''' Shared utiliy functions
''' </summary>
''' <remarks></remarks>
Public Class Utilities
    <Serializable(), Xml.Serialization.XmlType("System.Int32")> _
    Public Enum EnumBoolean
        <Xml.Serialization.XmlEnum("NO")> NO = 0
        <Xml.Serialization.XmlEnum("YES")> YES = 1
    End Enum

    Public Shared Function TextFromFileGet(ByVal sFilePath As String) As String
        ' Create an instance of StreamReader to read from a file.
        Dim sr As StreamReader = New StreamReader(sFilePath)
        Dim line As String
        Dim tmpl As New StringBuilder
        ' Read and display the lines from the file until the end 
        ' of the file is reached.
        Do

            line = sr.ReadLine()
            If Not line Is Nothing Then
                tmpl.Append(line & vbCrLf)
            End If

        Loop Until line Is Nothing

        sr.Close()
        Return tmpl.ToString

    End Function

    Public Shared Sub TextFromFileSave(ByVal sFilePath As String, _
           ByVal sFileContents As String)

        ' Create an instance of StreamReader to read from a file.
        Dim sr As StreamWriter = New StreamWriter(sFilePath, False, System.Text.Encoding.UTF8)
        sr.Write(sFileContents)
        sr.Close()


    End Sub

    ''' <summary>
    ''' Loads a file stored in an assembly as "embedded resource"
    ''' </summary>
    ''' <param name="resname">Fully qualified resource name, for example, com.neu.lib.File.txt</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function getResourceFileText(stype As Type, ByVal resname As String) As String

        Dim templ As String = String.Empty
        Dim d As Stream = stype.Assembly.GetManifestResourceStream(resname)
        Dim ds As StreamReader = New StreamReader(d, System.Text.Encoding.GetEncoding("Windows-1253"))
        Dim tline As String
        Try

            Do
                tline = ds.ReadLine()
                templ &= tline & vbCrLf
            Loop Until tline Is Nothing
            Return templ
        Finally
            d.Close()
            ds.Close()

        End Try

    End Function

    ''' <summary>
    ''' Loads a file stored in an assembly as "embedded resource"
    ''' </summary>
    ''' <param name="resname">Fully qualified resource name, for example, com.neu.lib.File.txt</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function getResourceFileText(ByVal resname As String) As String

        Dim templ As String = String.Empty
        Dim d As Stream = getResourceStream(resname)
        Dim ds As StreamReader = New StreamReader(d, System.Text.Encoding.GetEncoding("Windows-1253"))
        Dim tline As String
        Try

            Do
                tline = ds.ReadLine()
                templ &= tline & vbCrLf
            Loop Until tline Is Nothing
            Return templ
        Finally
            d.Close()
            ds.Close()

        End Try

    End Function
    Public Shared Function getResourceStream(ByVal resname As String) As Stream

        'can be used for embedded resources that do not have a dot in their name

        Dim loadAsembly As [Assembly] = Nothing
        Dim i As Integer

        For i = 0 To Thread.GetDomain.GetAssemblies.Length - 1

            If resname.ToUpper.StartsWith(Thread.GetDomain.GetAssemblies(i).GetName.Name.ToUpper) Then
                loadAsembly = Thread.GetDomain.GetAssemblies(i)
                Exit For
            End If
        Next

        If loadAsembly Is Nothing Then
            Throw New ApplicationException("Could not load resource file " & resname)
        End If

        Return loadAsembly.GetManifestResourceStream(resname)

    End Function
    Public Shared Function getResourceStream(ByVal resname As String, ByVal assemblyName As String) As Stream

        Return getResourceStream(assemblyName & "." & resname)

    End Function
End Class
