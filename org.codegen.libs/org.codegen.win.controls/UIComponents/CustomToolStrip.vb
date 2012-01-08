Namespace UIComponents

    Public Class CustomToolStrip

        Private _renderer As CustomToolStripRenderer


#Region "Custom ToolStrip Renderer"
        Public Class CustomToolStripRenderer : Inherits ToolStripProfessionalRenderer

            Public Sub New()
                Me.RoundedEdges = False
            End Sub


            Protected Overrides Sub OnRenderItemText(ByVal e As ToolStripItemTextRenderEventArgs)

                If (e.Item.Selected) Then
                    e.TextColor = Color.FromArgb(255, 223, 127)

                End If
                MyBase.OnRenderItemText(e)

            End Sub

            Protected Overrides Sub OnRenderButtonBackground(ByVal e As ToolStripItemRenderEventArgs)

                If (Not (e.Item.Selected)) Then
                    MyBase.OnRenderButtonBackground(e)
                End If

            End Sub

        End Class

#End Region

    End Class


End Namespace