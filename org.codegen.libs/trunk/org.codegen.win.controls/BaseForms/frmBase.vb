Imports org.codegen.common.Plugins
Imports System.Configuration.ConfigurationManager

Public Class frmBase
    Inherits System.Windows.Forms.Form


#Region "Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()
        'If Me.DesignMode = False Then
        '	Call WinEventLogger.WriteInfo("Form loading:" & Me.GetType.Name)
        'End If

        'This call is required by the Windows Form Designer.
        InitializeComponent()
        Me.Font = FormsApplicationContext.current.ApplicationDefaultFont
        'Add any initialization after the InitializeComponent() call

    End Sub



    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.SuspendLayout()
        '
        'frmBase
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.Color.White
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.ClientSize = New System.Drawing.Size(595, 335)
        Me.ForeColor = System.Drawing.Color.Black
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmBase"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "frmBase"
        Me.ResumeLayout(False)

    End Sub

#End Region

#Region "Custom Properties"

    Private Shared _Translator As ILanguageStrings
    Private Shared _isLocalized As Boolean?

    Public Shared ReadOnly Property isLocalized As Boolean
        Get

            If _isLocalized Is Nothing Then
                If AppSettings.HasKeys Then
                    Dim sLocalizedForms As String = AppSettings.Item("LocalizedForms")
                    If sLocalizedForms IsNot Nothing Then
                        _isLocalized = (sLocalizedForms.ToLower = "true" _
                                        OrElse sLocalizedForms = "1")
                    End If
                End If
            End If
            Return _isLocalized.GetValueOrDefault

        End Get

    End Property

    Public Shared Property Translator() As ILanguageStrings

        Get
            If _Translator Is Nothing AndAlso AppSettings.Item("Translator") IsNot Nothing Then
                _Translator = ILanguageStrings.getFromConfig
            End If
            Return _Translator
        End Get

        Set(ByVal value As ILanguageStrings)
            _Translator = value
        End Set

    End Property

#End Region

#Region "Custom Methods"

	Private Sub frmBase_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles Me.KeyDown
		'if form is toplevel then close it.
		If e.KeyCode = Keys.Escape AndAlso Me.TopLevel Then
			Me.Close()
			Me.Dispose()
		End If

	End Sub

	Public Sub setLocalizedText()


		Const STR_FRM_BASE As String = "frmbase"
		Const STR_FRM_BASE_GRID As String = "frmbasegrid"
		Const STR_FRM_BASE_EDIT As String = "frmbaseedit"

		If String.IsNullOrEmpty(Me.Name) = False _
				AndAlso Me.Name.ToLower <> STR_FRM_BASE _
				AndAlso Me.Name.ToLower <> STR_FRM_BASE_GRID _
				AndAlso Me.Name.ToLower <> STR_FRM_BASE_EDIT _
				AndAlso frmBase.Translator IsNot Nothing Then

			Me.Text = frmBase.Translator.getString(Me.Name & ".Text")
		End If

	End Sub


#End Region

    Public Shared Function addStripItem( _
                                        ByRef parentStrip As ToolStrip, _
                                        ByRef stripItem As ToolStripItem, _
                                        ByVal itemText As String, _
                                        ByVal handler As System.EventHandler, _
                                        Optional ByVal witdh As Integer = 60, _
                                        Optional ByVal img As Drawing.Image = Nothing) _
                                                        As ToolStripItem

        stripItem.Text = itemText

        If (img IsNot Nothing) Then
            stripItem.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText
            stripItem.Image = img
            stripItem.ImageTransparentColor = System.Drawing.Color.Magenta
        Else

            stripItem.DisplayStyle = ToolStripItemDisplayStyle.Text
        End If

        stripItem.Size = New System.Drawing.Size(witdh, 22)
        parentStrip.Items.Add(stripItem)

        AddHandler stripItem.Click, handler

        Return stripItem

    End Function

    Private Sub frmBase_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If frmBase.isLocalized Then
            Me.setLocalizedText()
        End If

    End Sub
End Class
