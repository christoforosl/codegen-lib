
Namespace Tokens

    Public MustInherit Class ReplacementToken
        Implements IReplacementToken

        Private Const STR_GT As String = ">"
        Private Const STR_LT As String = "<"

        Private _StringToReplace As String

        ''' <summary>
        ''' The code that will replace the token
        ''' </summary>
        ''' <param name="t"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public MustOverride Function getReplacementCode(ByVal t As IObjectToGenerate) As String _
                                Implements IReplacementToken.getReplacementCode

        Public Overridable Function replaceToken(ByVal TemplateCode As String, ByVal t As IObjectToGenerate) As String _
                            Implements IReplacementToken.replaceToken

            Return TemplateCode.Replace(Me.getStringTokenToReplace, Me.getReplacementCode(t))

        End Function

        Public Property StringToReplace As String
            Get
                Return _StringToReplace
            End Get
            Set(ByVal value As String)
                If value Is Nothing OrElse _
                        value.Contains(STR_LT) OrElse _
                        value.Contains(STR_GT) OrElse _
                        value.Contains(" ") Then
                    Throw New ApplicationException("Invalid character in token (Found "">"",""<"" or Space), or token is null")
                End If

                _StringToReplace = value

            End Set
        End Property

        Public Overridable Function getStringTokenToReplace() As String Implements IReplacementToken.StringTokenToReplace

            Return STR_LT & Me.StringToReplace & STR_GT

        End Function

    End Class
End Namespace