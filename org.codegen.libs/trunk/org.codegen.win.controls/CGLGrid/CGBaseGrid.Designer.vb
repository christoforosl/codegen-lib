Imports System.ComponentModel
Namespace Grid


    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class CGBaseGrid
        
        'UserControl overrides dispose to clean up the component list.
        <System.Diagnostics.DebuggerNonUserCode()> _
        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
            MyBase.Dispose(disposing)
        End Sub

        
        'Required by the Windows Form Designer
        Private components As System.ComponentModel.IContainer

        'NOTE: The following procedure is required by the Windows Form Designer
        'It can be modified using the Windows Form Designer.  
        'Do not modify it using the code editor.
        <System.Diagnostics.DebuggerStepThrough()> _
        Private Sub InitializeComponent()
            Me.components = New System.ComponentModel.Container()
            Me.SuspendLayout()
            '
            'grdData
            '
            Me.ReadOnly = True
            Me.BackgroundColor = System.Drawing.Color.White
            Me.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize

            Me.Location = New System.Drawing.Point(0, 0)
            Me.Name = "grdData"
            Me.Size = New System.Drawing.Size(661, 472)
            Me.ResumeLayout(False)

        End Sub

    End Class

End Namespace