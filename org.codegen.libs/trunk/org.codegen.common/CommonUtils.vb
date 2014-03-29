
Imports System.Reflection
Imports System.io
Imports System.Text
Imports System.Threading
Imports System.Security.Principal
Imports System.Globalization
Imports System.Configuration


Public Class StringUtils

    Public Shared Function defaultString(x As String) As String
        Return defaultString(x, String.Empty)
    End Function

    Public Shared Function defaultString(x As String, def As String) As String
        If String.IsNullOrEmpty(x) Then
            Return def
        Else
            Return x
        End If
    End Function

End Class
Public Class CommonUtils

    Public Shared Function getAssemblyVersion(ByVal iAssemly As [Assembly]) As String
        If iAssemly Is Nothing Then Return String.Empty
        Dim ret As String = iAssemly.GetName.Version.Major & "." & iAssemly.GetName.Version.Minor
        If iAssemly.GetName.Version.Build > 1 Then
            ret &= "." & iAssemly.GetName.Version.Build
        End If
        Return ret

    End Function
    Public Shared Function getEntryAssemblyVersion() As String

        Dim iAssemly As [Assembly] = [Assembly].GetEntryAssembly
        Return getAssemblyVersion(iAssemly)

    End Function


    ''' <summary>
    ''' Retrieves the Assembly "Title" attribite as defined in the assembly.vb/assembly.cs file
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetAssemblyTitle(ByVal iAssemly As [Assembly]) As String
        If iAssemly Is Nothing Then Return String.Empty

        Dim customAttributes As Object() = iAssemly.GetCustomAttributes(False)
        For Each o As Object In customAttributes
            If TypeOf (o) Is System.Reflection.AssemblyTitleAttribute Then
                Return CType(o, System.Reflection.AssemblyTitleAttribute).Title
            End If
        Next
        Return String.Empty
    End Function

    Public Shared Function GetEntryAssemblyTitle() As String
        Return GetAssemblyTitle([Assembly].GetEntryAssembly)
    End Function


    ''' <summary>
    ''' Returns a resource in an assembly as a Stream
    ''' </summary>
    ''' <param name="resname">Resource File Name</param>
    ''' <param name="assemblyName">Assembly Name</param>
    ''' <returns></returns>
    ''' <remarks>The file must be marked as "Embedded Resource"</remarks>
    Public Shared Function getResourceStream(ByVal resname As String, ByVal assemblyName As String) As Stream

        Dim loadAsembly As [Assembly] = Nothing
        Dim i As Integer

        For i = 0 To Thread.GetDomain.GetAssemblies.Length - 1
            If Thread.GetDomain.GetAssemblies(i).FullName.ToLower.IndexOf(assemblyName.ToLower) = 0 Then
                loadAsembly = Thread.GetDomain.GetAssemblies(i)
                Exit For
            End If
        Next
        If loadAsembly Is Nothing Then
            Throw New ApplicationException("Could not load resource file " & assemblyName & " " & resname)
        End If

        Return loadAsembly.GetManifestResourceStream(assemblyName & "." & resname)


    End Function


    ''' <summary>
    ''' Returns a resource in an assembly as a Stream
    ''' </summary>
    ''' <param name="resname">Fully qualified name of Resource File (assembly.name)</param>
    ''' <param name="typeInAssembly">A type that exists in the assembly to load the resource from</param>
    ''' <returns></returns>
    ''' <remarks>The file must be marked as "Embedded Resource"</remarks>
    Public Shared Function getResourceStream(ByVal resname As String, ByVal typeInAssembly As Type) As Stream

        Return typeInAssembly.Assembly.GetManifestResourceStream(resname)

    End Function

    ''' <summary>
    ''' Returns a resource in an assembly as a Stream
    ''' </summary>
    ''' <param name="resname">Fully qualified file name in the assembly</param>
    ''' <returns></returns>
    ''' <remarks>The file must be marked as "Embedded Resource"</remarks>
    Public Shared Function getResourceStream(ByVal resname As String) As Stream
        'can be used for embedded resources that do not have a dot in their name
        Dim loadAsembly As [Assembly] = Nothing

        Dim i As Integer

        For i = 0 To Thread.GetDomain.GetAssemblies.Length - 1
            'Debug.WriteLine(Thread.GetDomain.GetAssemblies(i).FullName)
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
    ''' <summary>
    '''  Returns a resource in an assembly as a String
    ''' </summary>
    ''' <param name="resname"></param>
    ''' <param name="assemblyName"></param>
    ''' <returns></returns>
    ''' <remarks>The file must be marked as "Embedded Resource"</remarks>
    Public Shared Function getResourceFileText(ByVal resname As String, ByVal assemblyName As String) As String

        Dim templ As String = String.Empty
        Dim d As Stream = CommonUtils.getResourceStream(resname, assemblyName)
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
    '''  Returns a resource file in an assembly as a String
    ''' </summary>
    ''' <param name="resname">Fully qualified resource file name</param>
    ''' <param name="typeInAssembly">Any Type that exists in the assembly that has the resource file</param>
    ''' <returns></returns>
    ''' <remarks>The file must be marked as "Embedded Resource"</remarks>
    Public Shared Function getResourceFileText(ByVal resname As String, ByVal typeInAssembly As Type) As String

        Dim templ As String = String.Empty
        Dim d As Stream = CommonUtils.getResourceStream(resname, typeInAssembly)
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
    ''' This functions returns the roles the current windows identity user belongs to.
    ''' </summary>
    ''' <param name="identity"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetWindowsIdentityRoles(ByVal identity As WindowsIdentity) As String()

        Dim result As Object = GetType(WindowsIdentity).InvokeMember("_GetRoles", _
          BindingFlags.Static Or BindingFlags.InvokeMethod Or BindingFlags.NonPublic, _
          Nothing, identity, New Object() {identity.Token}, Nothing)

        Return CType(result, String())

    End Function

    ''' <summary>
    ''' Opens a file as UTF-8 and returs its contents as string
    ''' </summary>
    ''' <param name="sFilePath"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function FileToString(ByVal sFilePath As String) As String
        Return FileToString(sFilePath, Encoding.UTF8)
    End Function
    ''' <summary>
    ''' Opens a file and returns the contents as String
    ''' </summary>
    ''' <param name="sFilePath"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function FileToString(ByVal sFilePath As String, enc As Encoding) As String
        ' Create an instance of StreamReader to read from a file.
        Dim sr As StreamReader = New StreamReader(sFilePath, enc)
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
    ''' <summary>
    ''' Saves a string into a file
    ''' </summary>
    ''' <param name="sFilePath"></param>
    ''' <param name="sFileContents"></param>
    ''' <remarks></remarks>
    Public Shared Sub TextToFile(ByVal sFilePath As String, _
                                        ByVal sFileContents As String, enc As Encoding)

        ' Create an instance of StreamReader to read from a file.
        Dim sr As StreamWriter = New StreamWriter(sFilePath, False, enc)
        sr.Write(sFileContents)
        sr.Close()


    End Sub

    Public Shared Sub TextToFile(ByVal sFilePath As String, _
                                        ByVal sFileContents As String)

        ' Create an instance of StreamReader to read from a file.
        Dim sr As StreamWriter = New StreamWriter(sFilePath, False, Encoding.UTF8)
        sr.Write(sFileContents)
        sr.Close()


    End Sub


    Public Shared Function ReadSetting(ByVal skey As String, ByVal sConfigSection As String) As String

        Dim confTable As Hashtable
        confTable = CType(ConfigurationManager.GetSection(sConfigSection), Hashtable)
        Return CType(confTable.Item(skey), String)

    End Function
    ''' <summary>
    ''' Creates a DataTable from a string.
    ''' </summary>
    ''' <param name="sList">String of delimited values</param>
    ''' <returns>a 2 column Datatable.  First Column Name: ID, Second Column Name: value</returns>
    ''' <remarks>
    ''' The input string must have records separated by "|" and field values 
    ''' separated by ";". There can be up to 2 columns (fields)
    ''' Example input string: "1;Test|2;Test2"
    ''' Will create a datatable with 2 rows. 1st row , column ID will have 
    ''' value 1 and column "value" will have value "Test"
    ''' </remarks>
    Public Shared Function DataTableFromStringList(ByVal sList As String) As DataTable

        Dim d As New DataTable
        d.Columns.Add(New DataColumn("id", System.Type.GetType("System.String")))
        d.Columns.Add(New DataColumn("value", System.Type.GetType("System.String")))
        '"1;йупяиос еяцодотгс;2;аккодапг етаияиа ле дяастгяиотгтес сто енытеяийо;3;упеяайтиа етаияиа"

        Dim arrStrings() As String = Split(sList, "|")
        For i As Integer = 0 To arrStrings.Length - 1
            Dim itmsval() As String = Split(arrStrings(i), ";")
            Dim r As DataRow = d.NewRow
            r.Item(0) = itmsval(0)
            If itmsval.Length = 2 Then
                r.Item(1) = itmsval(1)
            Else
                r.Item(1) = itmsval(0)
            End If
            d.Rows.Add(r)
        Next
        Return d

    End Function

End Class

Public Class Validate

    Public Shared Sub isTrue(val As Boolean, Optional msg As String = "Validate.isTrue assertion failed")

        If (Not val) Then
            Throw New ApplicationException(msg)
        End If

    End Sub

    Public Shared Sub isTrue(val As Boolean, msg As String, ParamArray args() As String)

        If (Not val) Then
            Throw New ApplicationException(String.Format(msg, args))
        End If

    End Sub

    Public Shared Sub isNotNull(val As Object, Optional msg As String = "Validate.isNotNull assertion failed")

        If (val Is Nothing) Then
            Throw New ApplicationException(msg)
        End If

    End Sub

    Public Shared Sub isNotNull(val As Object, msg As String, ParamArray args() As String)

        If (val Is Nothing) Then
            Throw New ApplicationException(String.Format(msg, args))
        End If

    End Sub

    Public Shared Sub isNotEmptyString(val As String, Optional msg As String = "Validate.isNotEmptyString assertion failed")

        If (String.IsNullOrEmpty(val)) Then
            Throw New ApplicationException(msg)
        End If

    End Sub

    Public Shared Sub isNotEmptyString(val As String, msg As String, ParamArray args() As String)

        If (String.IsNullOrEmpty(val)) Then
            Throw New ApplicationException(String.Format(msg, args))
        End If

    End Sub

End Class