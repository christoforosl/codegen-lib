
Namespace Tokens

    Public MustInherit Class MultiLingualReplacementToken
        Inherits ReplacementToken

        Public MustOverride Function getReplacementCodeVB(ByVal t As IObjectToGenerate) As String
        Public MustOverride Function getReplacementCodeCSharp(ByVal t As IObjectToGenerate) As String

        Public Overrides Function getReplacementCode(t As dotnet.IObjectToGenerate) As String
            If ModelGenerator.Current.dotNetLanguage = ModelGenerator.enumLanguage.VB Then
                Return Me.getReplacementCodeVB(t)
            Else
                Return Me.getReplacementCodeCSharp(t)
            End If
        End Function

    End Class

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