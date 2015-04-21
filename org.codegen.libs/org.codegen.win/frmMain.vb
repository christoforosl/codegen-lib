Imports System.Windows.Forms
Imports org.codegen.lib


Public Class frmMain

    Private Const STR_REG_SECTION As String = "codeGenWin"
    Private Const STR_LAST_EXECUTED As String = "LastExecuted"
    Private Const MSG_DIR_NOT_EXISTS As String = "Output Directory {0} does not exist!"
    Private WithEvents prg As ProgressIndicator


    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Call genFiles()
    End Sub

    Public Sub genFiles()

        Dim gen As XMLClassGenerator
        Dim cds As DataSet = Nothing

        Try
            If CheckSelectedFile() = False Then
                Return
            End If

            SaveSetting(STR_REG_SECTION, STR_REG_SECTION, "Path0", Me.cboXMLConfFile.Text)
            For i As Integer = 1 To 20
                SaveSetting(STR_REG_SECTION, STR_REG_SECTION, "Path" & i, String.Empty)
            Next

            For i As Integer = 0 To Me.cboXMLConfFile.Items.Count - 1
                Dim regXmlConfFile As String = CStr(Me.cboXMLConfFile.Items(i))
                If regXmlConfFile <> String.Empty Then
                    If regXmlConfFile <> Me.cboXMLConfFile.Text Then
                        If System.IO.File.Exists(regXmlConfFile) Then
                            'Me.cboXMLConfFile.Items.Add(regXmlConfFile)
                            SaveSetting(STR_REG_SECTION, STR_REG_SECTION, "Path" & (i + 1), regXmlConfFile)
                        End If
                    End If
                End If
            Next

            SaveSetting(STR_REG_SECTION, STR_REG_SECTION, STR_LAST_EXECUTED, Me.cboXMLConfFile.Text)


            Me.Show()


            Try

                Me.tsLabel.Text = "Generating Files..."
                Me.tsProgress.Maximum = 0 'set it to 0 to trigger it
                prg = New ProgressIndicator
                gen = New XMLClassGenerator(CStr(Me.cboXMLConfFile.Text))
                cds = New DataSet
                cds.ReadXmlSchema(common.CommonUtils.getResourceStream("org.codegen.lib.codeGen.classFenerator.xsd", GetType(XMLClassGenerator)))

                cds.ReadXml(CStr(Me.cboXMLConfFile.Text))
                gen.parseConfFile(cds)

                If System.IO.Directory.Exists(ModelGenerator.Current.ProjectOutputDirModel) = False Then
                    MsgBox(String.Format(MSG_DIR_NOT_EXISTS, ModelGenerator.Current.ProjectOutputDirModel))
                    Exit Sub
                End If

                gen.Progress = prg
                gen.genClasses()

                MsgBox("Completed: " & vbCrLf & _
                        "Objects:" & ModelGenerator.Current.ObjectsToGenerate.Count & vbCrLf & _
                        "New Gen Files:" & ModelGenerator.Current.NumOfNewGeneratedFiles & vbCrLf & _
                        "Updated Files:" & ModelGenerator.Current.NumOfGeneratedFiles & vbCrLf & _
                        "Skipped Files:" & ModelGenerator.Current.NumOfUnchangedFiles, MsgBoxStyle.Information)

            Finally

                Me.tsLabel.Text = "Ready"
            End Try


            Me.DialogResult = System.Windows.Forms.DialogResult.OK

        Catch ex As Exception


            Dim dsErr As String = String.Empty
            If cds Is Nothing = False Then
                For Each t As DataTable In cds.Tables
                    If t.HasErrors Then
                        For Each dr As DataRow In t.GetErrors
                            For Each c As DataColumn In dr.GetColumnsInError
                                dsErr &= dr.GetColumnError(c) & vbCrLf
                            Next
                        Next
                    End If
                Next
            End If
            Call MsgBox(String.Format("{0}{1}XML Conf File Error:{2}", ex.Message + vbCrLf + ex.StackTrace, vbCrLf, dsErr), MsgBoxStyle.Critical, "NETU Code Generator")
        End Try

    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub btnSelect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelect.Click


        ofSelectXMLConfFile.CheckFileExists = True
        ofSelectXMLConfFile.CheckPathExists = True
        'ofSelectXMLConfFile.FileNames = New String() {"XML Files"}
        ofSelectXMLConfFile.Filter = "XML Documents|*.xml|All Files|*.*"
        ofSelectXMLConfFile.FileName = String.Empty
        ofSelectXMLConfFile.Multiselect = False
        ofSelectXMLConfFile.Title = "Select XML Configuration File"
        ofSelectXMLConfFile.ShowDialog()

        If ofSelectXMLConfFile.FileName <> String.Empty Then
            Me.cboXMLConfFile.Items.Insert(0, ofSelectXMLConfFile.FileName)
            Me.cboXMLConfFile.SelectedIndex = 0

        End If

    End Sub

    Private writer As TextBoxWriter = Nothing


    Private Sub frmMain_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        writer = New TextBoxWriter(TextBox1)
        Console.SetOut(writer)
        Me.Text = String.Format("{0} V:{1}", Me.Text, System.Reflection.Assembly.GetExecutingAssembly.GetName.Version.ToString)

        For i As Integer = 0 To 20
            Dim regXmlConfFile As String = GetSetting(STR_REG_SECTION, STR_REG_SECTION, "Path" & i, String.Empty)
            If regXmlConfFile <> String.Empty Then
                If System.IO.File.Exists(regXmlConfFile) Then
                    Me.cboXMLConfFile.Items.Add(regXmlConfFile)
                End If

            End If
        Next

        Me.Show()
        Me.TopLevel = True
        Me.TopMost = True

        'Application.DoEvents()

        'if application started with command line argument
        If My.Application.CommandLineArgs.Count = 1 AndAlso My.Application.CommandLineArgs(0).EndsWith(".xml") Then
            Call MsgBox("Will generate Code from XML File:" & My.Application.CommandLineArgs(0), MsgBoxStyle.Information)
            Me.cboXMLConfFile.Text = My.Application.CommandLineArgs(0)
            Call OK_Button_Click(Nothing, Nothing)

        Else

            Dim lastExec As String = GetSetting(STR_REG_SECTION, STR_REG_SECTION, STR_LAST_EXECUTED, String.Empty)
            If lastExec <> String.Empty Then
                Me.cboXMLConfFile.SelectedItem = lastExec
            End If

        End If

    End Sub


    Private Function CheckSelectedFile() As Boolean

        Dim shouldReturn As Boolean = False

        If Me.cboXMLConfFile.Text Is Nothing Then
            Call MsgBox("Select an XML Conf File to execute", MsgBoxStyle.Critical)
            Return False
        End If

        If System.IO.File.Exists(Me.cboXMLConfFile.Text) = False Then
            Call MsgBox("Selected  XML Conf File " & vbCrLf & Me.cboXMLConfFile.Text & " does not exist!", MsgBoxStyle.Critical)
            Return False
        End If

        Return True

    End Function
    Private Sub btnView_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnView.Click

        If CheckSelectedFile() = False Then
            Return
        End If


        Call Shell("explorer " & CStr(Me.cboXMLConfFile.Text), AppWinStyle.NormalFocus, False)
    End Sub


    Private Sub prg_doProgress(ByVal currentStep As Integer) Handles prg.doProgress

        If Me.tsProgress.Maximum = 0 Then
            Me.tsProgress.Maximum = prg.MaxSteps
            Me.tsProgress.Minimum = 0
        End If

        Me.tsLabel.Text = currentStep & " of " & prg.MaxSteps
        If currentStep > Me.tsProgress.Maximum Then
        Else
            Me.tsProgress.Value = currentStep
            Application.DoEvents()

        End If

    End Sub
End Class
