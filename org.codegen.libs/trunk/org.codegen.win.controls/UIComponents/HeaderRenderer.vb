Imports System.Drawing.Drawing2D

Namespace UIComponents

    Public Class TitleStripRenderer : Inherits ToolStripProfessionalRenderer

        Private _startColor As Color = Color.White
        Private _endColor As Color = Color.FromArgb(168, 186, 212)
        Private _lines As Int16 = 6
        Private _drawEndLine As Boolean = True

        Public Sub New()

        End Sub



        Public Property EndColor() As Color
            Get
                Return _endColor
            End Get
            Set(ByVal value As Color)
                _endColor = value
            End Set
        End Property

        Public Property StartColor() As Color
            Get
                Return _startColor
            End Get
            Set(ByVal value As Color)
                _startColor = value
            End Set
        End Property

        Public Property Lines() As Int16
            Get
                Return _lines
            End Get
            Set(ByVal value As Int16)
                _lines = value
            End Set
        End Property


        Public Property DrawEndLine() As Boolean
            Get
                Return _drawEndLine
            End Get
            Set(ByVal value As Boolean)
                _drawEndLine = value
            End Set
        End Property


        Protected Overrides Sub OnRenderToolStripBackground(ByVal e As ToolStripRenderEventArgs)

            Dim start As Color = _startColor
            Dim cend As Color = _endColor

            Dim toolStrip As ToolStrip = e.ToolStrip
            Dim g As Graphics = e.Graphics

            Dim boundsHeight As Integer = e.AffectedBounds.Height
            Dim height As Integer = CInt((boundsHeight + _lines - 1) / _lines)
            Dim width As Integer = e.AffectedBounds.Width
            Dim stripeHeight As Integer = height - 1
            Dim stripeRect As Rectangle

            Dim b As Brush = New LinearGradientBrush(New Rectangle(0, 0, width, stripeHeight), start, cend, LinearGradientMode.Horizontal)

            For idx As Integer = 0 To _lines - 1
                stripeRect = New Rectangle(0, height * idx + 1, width, stripeHeight)
                g.FillRectangle(b, stripeRect)
                idx = idx + 1

            Next

            If Me.DrawEndLine Then

                Dim solidBrush As Brush = New SolidBrush(Color.FromArgb(177, 177, 177))
                g.FillRectangle(solidBrush, New Rectangle(0, boundsHeight - 1, width, 1))

            End If
        End Sub

        Protected Overrides Sub OnRenderToolStripBorder(ByVal e As ToolStripRenderEventArgs)

        End Sub

    End Class

End Namespace