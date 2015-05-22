Namespace Tokens
    Public Interface IReplacementToken

        ''' <summary>
        ''' The StringToReplace enclosed in &lt; and &gt;
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function StringTokenToReplace() As String

        ''' <summary>
        ''' Returns the code that will replace the token
        ''' </summary>
        ''' <param name="t">ObjectToGenerate</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function getReplacementCode(ByVal t As IObjectToGenerate) As String

        ''' <summary>
        ''' Returns the TemplateCode parameter, with the token replaced with "getReplacementCode" string
        ''' </summary>
        ''' <param name="templateCode"></param>
        ''' <param name="t"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function replaceToken(ByVal templateCode As String, ByVal t As IObjectToGenerate) As String


    End Interface
End Namespace
