Imports System.Globalization
Imports System.Security.Permissions
Imports System.Threading
Imports System.Collections.Generic

Namespace TranslationServices
    ''' <summary>
    ''' Class to fascilitate translation services from an XML file.
    ''' The xml structure must be the same as the database table structure:
    ''' <example>
    ''' <code lang="xml">
    ''' <![CDATA[
    '''  <langString>
    '''    <langKey>0</langKey>
    '''    <langValueEN>Please enter a valid date in field '%1'.</langValueEN>
    '''    <langValueEL>Παρακαλώ Καταχωρείστε Ημερομηνία στο πεδίο: '%1'</langValueEL>
    '''  </langString>]]>
    ''' </code>
    ''' </example>
    ''' </summary>
    ''' <remarks></remarks>
    Public Class XMLanguageStrings
        Inherits TranslatedStringsProvider

        Private _dv As DataView

        Private ReadOnly Property dv() As DataView
            Get
                Return _dv
            End Get

        End Property


        Public Overrides Sub deleteString(ByVal key As String)

        End Sub

        Public Overrides Function retrieveStringFromStore(ByVal key As String, _
                                              Optional ByVal inLang As String = "") _
                                    As String

            Dim stm As String = String.Empty
            If inLang = String.Empty Then
                inLang = System.Threading.Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName.ToUpper
            End If
            inLang = inLang.ToUpper


            Dim i As Integer = Me.dv.Find(New Object() {key})
            If i > -1 Then
                'Debug.WriteLine("Found!")

                stm = CStr(Me.dv.Item(i).Item("langValue" & inLang))
                If stm Is Nothing OrElse stm = String.Empty Then
                    stm = key
                End If
            End If
            Return stm

        End Function

        Public Overrides Sub insertOrUpdate(ByVal key As String, ByVal valueEN As String, ByVal valueEL As String)

        End Sub


        Public Sub setResourceName(ByVal value As String, stype As Type)

            Me.setXMLDataView(CommonUtils.getResourceStream(value, stype))

        End Sub


        Public WriteOnly Property XMLStream() As System.IO.Stream
            Set(ByVal value As System.IO.Stream)
                Validate.isNotNull(value, "XMLStream PROP: XML STREAM IS NULL!!")
                Call setXMLDataView(value)
            End Set
        End Property

        Private Sub setXMLDataView(ByVal inxmlStream As System.IO.Stream)

            Dim _dsLang As New DataSet
            Try
                Validate.isNotNull(inxmlStream, "setXMLDataView: XML TREAM IS NULL!!")
                _dsLang.ReadXml(inxmlStream)

                Dim keys(0) As DataColumn
                keys(0) = _dsLang.Tables(0).Columns("langKey")

                _dv = _dsLang.Tables(0).DefaultView
                _dv.Sort = "langKey"


            Finally
                If inxmlStream Is Nothing = False Then inxmlStream.Close()
            End Try

        End Sub

    End Class
End Namespace
