
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

#Region "Custom Methods"

    Private Sub frmBase_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles Me.KeyDown
        'if form is toplevel then close it.
        If e.KeyCode = Keys.Escape Then
            Call handleEscapeKey()
            e.Handled = True
        End If

    End Sub

    Public Overridable Sub handleEscapeKey()

        If Me.TopLevel Then
            Me.Close()
            Me.Dispose()
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

        If handler IsNot Nothing Then
            AddHandler stripItem.Click, handler
        End If

        Return stripItem

    End Function

   
End Class
