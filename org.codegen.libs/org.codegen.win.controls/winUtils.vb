Imports System.Reflection
Imports System.Text
Imports System.Globalization

Public NotInheritable Class winUtils


    Private Shared _ApplicationDefaultCasing As CharacterCasing = _
        CharacterCasing.Normal

    Private Shared _ApplicationdefaultFont As Font = _
        New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, _
                                System.Drawing.GraphicsUnit.Point, CType(0, Byte))

    ''' <summary>
    ''' Gets/Sets the font used by all input controls
    ''' </summary>
    ''' <remarks>
    ''' All custom input controls are automatically assigned this font.
    ''' To change it, call
    ''' <code>winUtils.ApplicationDefaultFont = yourFont</code>
    ''' </remarks>
    Public Shared Property ApplicationDefaultFont() As Font
        Get
            Return _ApplicationdefaultFont
        End Get
        Set(ByVal value As Font)
            _ApplicationdefaultFont = value
        End Set
    End Property

    ''' <summary>
    ''' Gets/Sets the character casing used by all text controls in the system
    ''' </summary>
    ''' <remarks>
    ''' All CGTextBox input controls are automatically assigned this casing by default.
    ''' To change it, call
    ''' <code>winUtils.ApplicationDefaultCasing = xxx </code>
    ''' </remarks>
    Public Shared Property ApplicationDefaultCasing() As CharacterCasing
        Get
            Return _ApplicationDefaultCasing
        End Get
        Set(ByVal value As CharacterCasing)
            _ApplicationDefaultCasing = value
        End Set
    End Property

    Private Shared _windowTitle As String = Nothing
    ''' <summary>
    ''' Gets/Sets text that appears on Message Boxes
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>
    ''' At first retrieval of this property, and if the value of the property is null, 
    ''' it is retrieved from the Assembly Attribute "title"
    ''' So insert this in the assemblyinfo.vb file:
    ''' &lt;Assembly: AssemblyTitle("Your Title Here")&gt;
    ''' Alternatively, you can set this to a manual value in application startup:
    ''' <code>winUtils.windowTitle = "Your Title"</code>
    ''' </remarks>
    Public Shared Property ApplicationTitle As String
        Get
            If _windowTitle Is Nothing Then
                _windowTitle = CommonUtils.GetEntryAssemblyTitle() & " [v " & CommonUtils.getEntryAssemblyVersion() & "]"
            End If
            Return _windowTitle
        End Get
        Set(ByVal value As String)
            _windowTitle = value
        End Set
    End Property

    Public Shared Sub MsgboxInfo(ByVal msg As String)
        Call MsgBox(msg, MsgBoxStyle.Information, ApplicationTitle())
    End Sub


    Public Shared Sub MsgboxStop(ByVal msg As String)
        Call MsgBox(msg, MsgBoxStyle.Critical, ApplicationTitle())
    End Sub

    Public Shared Function MsgboxQuestion(ByVal msg As String) As MsgBoxResult

        Return MsgBox(msg, CType(MsgBoxStyle.Question + MsgBoxStyle.YesNo, MsgBoxStyle), ApplicationTitle())

    End Function

    Public Shared Function hasUnicode(ByVal str As String) As Boolean
        Dim flag As Boolean
        Dim num2 As Integer = Strings.Len(str)
        Dim i As Integer = 1
        Do While (i <= num2)
            If (Strings.Asc(Strings.Mid(str, i, 1)) > &H7F) Then
                Return True
            End If
            i += 1
        Loop
        Return flag
    End Function

    Public Shared Sub HourglassOff()
        Cursor.Current = Cursors.Default
    End Sub

    Public Shared Sub HourglassOn()
        Cursor.Current = Cursors.WaitCursor
    End Sub


    Public Shared Sub sizeMdiChild(ByVal f As Form)

        If f.IsMdiChild Then
            Const HEIGHT_OF_MENU_STATUS_BARS As Integer = 50
            ' Const WIDTH_OF_MENU_STATUS_BARS As Integer = 141

            f.Location = New Point(0, 0)
            f.Size = New Size(f.MdiParent.ClientRectangle.Width - 5, _
                f.MdiParent.ClientRectangle.Height - HEIGHT_OF_MENU_STATUS_BARS)
        End If

    End Sub




End Class