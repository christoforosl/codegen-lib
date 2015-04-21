Option Strict On

Imports System.IO
Imports System.Text
Imports System.Xml
Imports System.Collections.Generic
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports org.codegen.common
Imports org.codegen.lib
Imports org.codegen.lib.db
Imports org.etek.lib.BusObjects
Imports System.Xml.Serialization

#Region "Test Class"



Public Class testMO1
    Inherits testMO

   
    Public Shadows Property ObjFlag() As Utilities.EnumBoolean
        Get
            Return CType(MyBase.ObjFlag, Utilities.EnumBoolean)
        End Get
        Set(ByVal value As Utilities.EnumBoolean)
            MyBase.ObjFlag = CInt(value)
        End Set
    End Property

End Class


Public Class testMO
    Inherits Object

    Private _objFlag As Integer?
    Private _objPrice As Decimal?
    Private _objId As Integer?
    Private _objDecr As String

    Public Property ObjPrice() As Decimal?
        Get
            Return _objPrice
        End Get
        Set(ByVal value As Decimal?)
            _objPrice = value
        End Set
    End Property

    <XmlIgnore()> _
    Public Property ObjFlag() As Integer?
        Get
            Return _objFlag
        End Get
        Set(ByVal value As Integer?)
            _objFlag = value
        End Set
    End Property
    Public Property ObjDecr() As String
        Get
            Return _objDecr
        End Get
        Set(ByVal value As String)
            _objDecr = value
        End Set
    End Property
    Public Property ObjId() As Integer?
        Get
            Return _objId
        End Get
        Set(ByVal value As Integer?)
            _objId = value
        End Set
    End Property

End Class
#End Region


<TestClass()> _
Public Class TestXMLSerialization

    Private _sPath As String = "C:\vs2008Projects\org.codegen.lib.vb\org.codegen.common.tests\"

    Public Shared Function getBasicApplication( _
                     ByVal isource As enumApplicationSource, _
                     Optional ByVal etekType As HEtekType.enumHEtekType = HEtekType.enumHEtekType.REGULAR_MEMBER) As Application

        Dim ret As Application = Application.Create(isource)

        Assert.IsTrue(ret.isEmpty = False, " Expected isEmpty flag to be false.")
        Assert.IsTrue(ret.Member.isEmpty = True, " Expected Member.isEmpty flag to be true.")
        Assert.IsTrue(ret.Member.HomeAddress.isEmpty = True, " Expected Member.HomeAddress.isEmpty flag to be true.")
        Assert.IsTrue(ret.Member.EmployerAddress.isEmpty = True, " Expected Member.EmployerAddress.isEmpty flag to be true.")

        ret.Comments = "TEST comments"
        ret.ElectionMainBranchId = Nothing
        ret.CriminalRecordDesc = "Unit Test criminal record"
        ret.EtekType = HEtekType.enumHEtekType.REGULAR_MEMBER
        ret.ApplicationDate = Date.Now

        ret.Member.LastName = "manual "
        ret.Member.FirstName = "First " & Format(Now, "hhmmss")
        ret.Member.FatherName = "Last " & Format(Now, "hhmmss")
        ret.Member.IdNo = Format(Date.Now.Month, "00") & Format(Date.Now.Day, "00") & _
                           Format(Date.Now.Hour, "00") & Format(Date.Now.Minute, "00") & _
                           Format(Date.Now.Second, "00")

        Dim rn As System.Random = New System.Random
        Dim yr As Integer = DateAdd(DateInterval.Year, -1 * rn.Next(20, 50), Now).Year
        ret.Member.DateOfBirth = DateSerial(yr, rn.Next(1, 12), rn.Next(1, 28))

        ret.Member.PassportNo = "000011"
        ret.Member.NationalityId = 60 'cyprus
        ret.Member.PlaceOfBirth = "PlaceOfBirth test"
        ret.Member.GenderId = 1

        ret.Member.HomeAddress.AddressLine1 = "HAddress 1"
        ret.Member.HomeAddress.CountryId = 60
        ret.Member.HomeAddress.AddressLine2 = "HAddress 2"
        ret.Member.HomeAddress.DistrictId = Nothing
        ret.Member.HomeAddress.AddressPostcode = "H PC"
        ret.Member.HomeAddress.AddressCity = "nicosia"

        'this part tests the case where adresses are assigned empty strings
        'such is the case from the web page.  The object should not be saved
        'and the isEmpty function is tested
        ret.Member.EmployerAddress.AddressLine1 = ""
        ret.Member.EmployerAddress.AddressLine2 = ""
        ret.Member.EmployerAddress.AddressPostcode = ""
        Assert.IsTrue(ret.Member.EmployerAddress.isEmpty = True, " Expected Member.EmployerAddress.isEmpty flag to be true.")
        '''''''''''''''''''''''''''''''''''''''''

        ret.Member.HomePhone = "00 000000"
        ret.Member.HomeFax = Nothing
        ret.Member.MobilePhone = "99 000000"
        ret.Member.EmploymentPhone = "22 000 000"
        ret.Member.EmploymentFax = "22 000 000"
        ret.Member.ProfessionId = 1
        ret.Member.Position = "position"
        ret.Member.EmployerId = Nothing
        ret.Member.Elections = Nothing
        ret.Member.MemberStatusId = Member.enumMemberStatus.APPLICANT
        ret.Member.AllowUseOfPersonalData = 1
        ret.Member.LicenseWaivedInd = Utilities.EnumBoolean.YES

        Dim cEduc As Education = Education.Create
        Assert.IsTrue(cEduc.isEmpty = True, " Expected Member.isEmpty flag to be true.")
        cEduc.Comments = "test comments"
        Assert.IsTrue(cEduc.isEmpty = False, " Expected Member.isEmpty flag to be false.")
        cEduc.DegreeTitleId = 1
        cEduc.EducationFrom = DateAdd(DateInterval.Year, 10, Date.Now)
        cEduc.EducationTo = DateAdd(DateInterval.Year, 6, Date.Now)
        cEduc.SchoolId = 5
        cEduc.Grade = "A+"
        ret.addApplicationEducation(cEduc)

        cEduc = Education.Create
        cEduc.Comments = "test comments 2"
        cEduc.DegreeTitleId = 2
        cEduc.SchoolId = 4
        cEduc.EducationFrom = DateAdd(DateInterval.Year, 3, Date.Now)
        cEduc.EducationTo = DateAdd(DateInterval.Year, 1, Date.Now)
        cEduc.Grade = "AB+"
        ret.addApplicationEducation(cEduc)

        Dim appBranh As ApplicationBranch = ApplicationBranch.Create
        appBranh.BranchId = 1
        ret.addApplicationBranch(appBranh)

        appBranh = ApplicationBranch.Create
        appBranh.BranchId = 2
        ret.addApplicationBranch(appBranh)

        Assert.IsTrue(ret.isNew, "Expected: isNew Flag to be true")
        Assert.IsTrue(ret.Member.isNew, "Expected: Member isNew flag to be true")
        Return ret

    End Function

    <TestMethod()> _
    Private Sub testXMLSerializationProfession()

        Dim cp As HProfession = HProfession.Create
        cp.ActivityId = 1
        cp.LicenseIssueInd = 1
        cp.LicensePaidInd = Utilities.EnumBoolean.YES
        cp.ProfessionCode = "11"
        cp.ProfessionDesc = "test"
        cp.ProfessionId = 90

        Dim sfile As String = _sPath & "hProf.xml"
        If System.IO.File.Exists(sfile) Then
            System.IO.File.Delete(sfile)
        End If
        Dim objStreamWriter As New System.IO.FileStream(sfile, FileMode.Create)


        Try

            Dim formatter As New XmlSerializer(cp.GetType)
            formatter.Serialize(objStreamWriter, cp)

        Finally
            objStreamWriter.Flush()
            objStreamWriter.Close()

        End Try
    End Sub
    <TestMethod()> _
Public Sub testXMLSerialization()

        Dim sfile As String = _sPath & "app.xml"
        Dim objStreamWriter As System.IO.FileStream
        testXMLSerializationProfession()

        Dim capp As Application = getBasicApplication(enumApplicationSource.WEB)
        capp.OnlineApplicationId = 999

        'we have an application!!
        'serialize and give the applicant the application id for a reference
        sfile = _sPath & "onlineApp" & capp.OnlineApplicationId & ".xml"

        If System.IO.File.Exists(sfile) Then
            System.IO.File.Delete(sfile)
        End If

        objStreamWriter = New System.IO.FileStream(sfile, FileMode.Create)

        Try

            Dim formatter As New XmlSerializer(capp.Member.GetType)
            formatter.Serialize(objStreamWriter, capp.Member)

        Finally
            objStreamWriter.Flush()
            objStreamWriter.Close()

        End Try

        capp = Nothing

        Dim objStreamReader As New System.IO.FileStream(sfile, FileMode.Open)
        capp = Application.Create(enumApplicationSource.WEB)

        'now decirialize!
        Try

            capp.readXML(objStreamReader)
        Catch e As Exception
            Diagnostics.Debug.WriteLine(e.Message)
        Finally
            objStreamReader.Close()
        End Try

        Assert.IsNotNull(capp)
        'Assert.AreEqual(_testApp.OnlineApplicationId, 999)
        'Assert.AreEqual(_testApp.Member.PassportNo, "000011")
        'Assert.AreEqual(_testApp.Member.HomeAddress.AddressLine1, "HAddress 1")
        'Assert.AreEqual(_testApp.applicationBranch.Count, 2)

        objStreamWriter = New System.IO.FileStream(sfile & "1", FileMode.Create)

        Try

            capp.writeXML(objStreamWriter)

        Finally
            objStreamWriter.Flush()
            objStreamWriter.Close()

        End Try
        If System.IO.File.Exists(sfile) Then
            'System.IO.File.Delete(sfile)
        End If

    End Sub

    <TestMethod()> _
   Public Sub testMODataContract()
        Dim mo As New testMO1
        mo.ObjFlag = Utilities.EnumBoolean.YES
        mo.ObjId = 1
        mo.ObjPrice = 12.34D
        Dim sfile As String = "C:\vs2008Projects\org.codegen.lib.vb\org.codegen.common.tests\moSerTest.xml"
        If System.IO.File.Exists(sfile) Then System.IO.File.Delete(sfile)

        Dim objStreamWriter As New System.IO.FileStream(sfile, FileMode.CreateNew)

        Try

            Dim formatter As New XmlSerializer(mo.GetType)
            formatter.Serialize(objStreamWriter, mo)

        Finally
            objStreamWriter.Close()
        End Try

    End Sub

    <TestMethod()> _
    Public Sub testMOXMLSerialization()
        Dim mo As New testMO1
        mo.ObjFlag = Utilities.EnumBoolean.NO
        mo.ObjId = 1
        mo.ObjPrice = 12.34D
        Dim sfile As String = "C:\vs2008Projects\org.codegen.lib.vb\org.codegen.common.tests\moSerTest.xml"
        If System.IO.File.Exists(sfile) Then System.IO.File.Delete(sfile)

        Dim objStreamWriter As New System.IO.FileStream(sfile, FileMode.CreateNew)

        Try

            Dim formatter As New XmlSerializer(mo.GetType)
            formatter.Serialize(objStreamWriter, mo)

        Finally
            objStreamWriter.Close()
        End Try

    End Sub

    <TestMethod()> _
    Public Sub TestItems()
        Dim reader As XmlTextReader = Nothing

        Try
            ' Load the reader with the data file and ignore all white space nodes.         
            reader = New XmlTextReader("C:\vs2008Projects\org.codegen.lib.vb\org.codegen.common.tests\app999.xml")
            reader.WhitespaceHandling = WhitespaceHandling.None

            ' Parse the file and display each of the nodes.
            While reader.Read()

                Select Case reader.NodeType
                    Case XmlNodeType.Attribute
                        Console.WriteLine("Attribute:", reader.Name)
                    Case XmlNodeType.Element
                        Console.WriteLine("Element: " & reader.Name & " ATTR:" & reader.AttributeCount)
                    Case XmlNodeType.Text
                        'Console.WriteLine(reader.Value)
                    Case XmlNodeType.CDATA
                        'Console.WriteLine("<![CDATA[{0}]]>", reader.Value)
                    Case XmlNodeType.ProcessingInstruction
                        'Console.WriteLine("<?{0} {1}?>", reader.Name, reader.Value)
                    Case XmlNodeType.Comment
                        'Console.WriteLine("<!--{0}-->", reader.Value)
                    Case XmlNodeType.XmlDeclaration
                        'Console.WriteLine("<?xml version='1.0'?>")
                    Case XmlNodeType.Document
                    Case XmlNodeType.DocumentType
                        'Console.WriteLine("<!DOCTYPE {0} [{1}]", reader.Name, reader.Value)
                    Case XmlNodeType.EntityReference
                        Console.WriteLine("EntityReference: " & reader.Name & " ENTITY ATTR:" & reader.AttributeCount)
                    Case XmlNodeType.EndElement
                        'Console.WriteLine("</{0}>", reader.Name)
                End Select
                'Console.WriteLine()
            End While

        Finally
            If Not (reader Is Nothing) Then
                reader.Close()
            End If
        End Try
    End Sub 'Main 


    <TestMethod()> _
    Sub TestXMLRead()

        Dim sfile As String = "C:\vs2008Projects\org.codegen.lib.vb\org.codegen.common.tests\app999.xml"
        Dim objStreamReader As New System.IO.FileStream(sfile, FileMode.Open)
        Dim a As Application = Application.Create(enumApplicationSource.WEB)

        'now decirialize!
        Try

            a.readXML(objStreamReader)

        Finally
            objStreamReader.Close()
        End Try

        a.writeXML(sfile & "1")

    End Sub

End Class
