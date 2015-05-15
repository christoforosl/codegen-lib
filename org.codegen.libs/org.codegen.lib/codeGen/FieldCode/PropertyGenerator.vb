Imports System.Text
Imports org.codegen.lib.FileComponents
Imports org.codegen.lib.Tokens
Imports System.Collections.Generic

Public Class PropertyGenerator
    Implements IPropertyGenerator

    Public Function generateCode(ByVal field As IDBField) As String _
            Implements IPropertyGenerator.generateCode

        Dim sImplements As String = String.Empty
        Dim sLengthChecker As String = String.Empty
        Dim xmlIgnore As String = String.Empty
        Dim runtimeFieldName As String = field.RuntimeFieldName()
        Dim propertyFieldname As String = field.RuntimeFieldName

        If runtimeFieldName.ToLower = "readonly" Then runtimeFieldName = "[ReadOnly]"
        If runtimeFieldName.ToLower = "new" Then runtimeFieldName = "[new]"

        If field.RuntimeTypeStr = "System.String" Then
            sLengthChecker = vbTab & vbTab & "if value isNot Nothing andAlso value.Length > " & field.Size & " Then" & vbCrLf
            sLengthChecker &= vbTab & vbTab & vbTab & "Throw new ModelObjectFieldTooLongException(""" & field.FieldName & """)" & vbCrLf
            sLengthChecker &= vbTab & vbTab & "End If" & vbCrLf
        End If

        Dim iimplements As List(Of String) = New List(Of String)
        If field.isAuditField Then
            iimplements.Add(field.ParentTable.getAuditInterface & "." & field.PropertyName)
        End If

        Dim PropertyInterface As String = DirectCast( _
                    ModelGenerator.Current.CurrentObjectBeingGenerated.FileGroup(ModelObjectFileComponent.KEY),  _
                    DotNetClassFileComponent).ClassInterface

        If String.IsNullOrEmpty(PropertyInterface) = False Then
            iimplements.Add(PropertyInterface & "." & field.PropertyName)

        End If

        If iimplements.Count > 0 Then
            sImplements = " _ " & vbCrLf & vbTab & vbTab & _
                    "Implements " & _
                    String.Join(",", iimplements)
        End If

        If field.XMLSerializationIgnore Then
            xmlIgnore = "<XmlIgnore()> _" & vbCrLf
        End If

        Dim pfx As String = ModelGenerator.Current.FieldPropertyPrefix
        If (field.isAuditField) Then pfx = String.Empty

        Dim sproperty As StringBuilder = New StringBuilder(xmlIgnore).Append(vbTab). _
              Append(field.AccessLevel).Append(" Overridable Property ").Append(pfx).Append(runtimeFieldName). _
              Append(" as ").Append(field.getPropertyDataType).Append(sImplements).Append(vbCrLf). _
              Append(vbTab).Append("Get ").Append(vbCrLf)

        sproperty.Append(vbTab & vbTab & "return _" & runtimeFieldName & vbCrLf)

        sproperty.Append(vbTab & "End Get " & vbCrLf)
        sproperty.Append(
            vbTab).Append("Set(ByVal value As ").Append(field.getPropertyDataType).Append(")").Append(vbCrLf).Append( _
            vbTab).Append(vbTab).Append("if ModelObject.valueChanged(_").Append(runtimeFieldName).Append(", value) then").Append(vbCrLf). _
            Append(sLengthChecker).Append( _
            vbTab).Append(vbTab).Append(vbTab & "if me.IsObjectLoading = false then").Append(vbCrLf).Append( _
            vbTab).Append(vbTab).Append(vbTab & vbTab).Append("me.isDirty = true").Append(vbCrLf).Append( _
            vbTab).Append(vbTab).Append(vbTab & vbTab).Append("me.setFieldChanged(").Append(field.getConstantStr).Append(")").Append(vbCrLf & _
            vbTab).Append(vbTab).Append(vbTab & "End If").Append(vbCrLf)

        If field.isPrimaryKey Then
            sproperty.Append(vbCrLf & vbTab & vbTab & vbTab & "me.raiseBroadcastIdChange()" & vbCrLf)
        End If

        sproperty.Append(vbCrLf & vbTab & vbTab & "End if" & vbCrLf & _
           vbTab & "End Set " & vbCrLf & _
           vbTab & "End Property " & vbCrLf)


        'Me.generateStringSetters(sproperty, field)

        Return sproperty.ToString

    End Function

    Public Function generateInterfaceDeclaration(ByVal field As IDBField) As String _
                    Implements IPropertyGenerator.generateInterfaceDeclaration

        Dim sb As StringBuilder = New StringBuilder()
		Dim fname As String = field.PropertyName()
        If fname.ToLower = "readonly" OrElse fname.ToLower = "new" Then
            fname = "[" & fname & "]"
        End If
        If (Not field.FieldName.ToLower.Equals("id")) Then
            sb.Append(vbTab & "Property " & fname & " as " & field.getPropertyDataType)
            sb.Append(vbCrLf)
        End If

        Return sb.ToString

    End Function

    Private Sub generateStringSetters(ret As StringBuilder, ByVal field As IDBField)

        Dim runtimeFieldName As String = field.RuntimeFieldName()
        Dim propertyFieldname As String = field.RuntimeFieldName

        If field.isBoolean Then
            ret.Append("Public Sub set").Append(propertyFieldname).Append("(ByVal val As String)").Append(vbCrLf)
            ret.Append("	If String.IsNullOrEmpty(val) Then").Append(vbCrLf)
            ret.Append("		Me.").Append(field.PropertyName).Append(" = Nothing").Append(vbCrLf)
            ret.Append("	Else").Append(vbCrLf)
            ret.Append("	    Dim newval As Boolean").Append(vbCrLf)
            ret.Append("	    Dim success As Boolean = Boolean.TryParse(val, newval)").Append(vbCrLf)
            ret.Append("	    If (Not success) Then").Append(vbCrLf)
            ret.Append("		    Throw new ApplicationException(""Invalid Integer Number, field:").Append(runtimeFieldName). _
                                        Append(", value:"" & val)").Append(vbCrLf)
            ret.Append("	    End If").Append(vbCrLf)
            ret.Append("	    Me.").Append(field.PropertyName).Append(" = newval").Append(vbCrLf)
            ret.Append("	End If").Append(vbCrLf)
            ret.Append("End Sub").Append(vbCrLf)

        ElseIf field.isInteger Then
            ret.Append("Public Sub set").Append(propertyFieldname).Append("(ByVal val As String)").Append(vbCrLf)
            ret.Append("	If IsNumeric(val) Then").Append(vbCrLf)
            ret.Append("		Me.").Append(field.PropertyName).Append(" = CType(val, ").Append(field.FieldDataType).Append(")").Append(vbCrLf)

            ret.Append("	ElseIf String.IsNullOrEmpty(val) Then").Append(vbCrLf)
            ret.Append("		Me.").Append(field.PropertyName).Append(" = Nothing").Append(vbCrLf)
            ret.Append("	Else").Append(vbCrLf)
            'ret.Append("		Me.").Append(runtimeFieldName).Append(" = Nothing").Append(vbCrLf)
            ret.Append("		Throw new ApplicationException(""Invalid Integer Number, field:").Append(runtimeFieldName). _
                                        Append(", value:"" & val)").Append(vbCrLf)
            ret.Append("	End If").Append(vbCrLf)
            ret.Append("End Sub").Append(vbCrLf)

        ElseIf field.isDecimal Then
            ret.Append("Public Sub set").Append(propertyFieldname).Append("(ByVal val As String)").Append(vbCrLf)
            ret.Append("	If IsNumeric(val) Then").Append(vbCrLf)
            ret.Append("		Me.").Append(field.PropertyName).Append(" = CDec(val)").Append(vbCrLf)
            ret.Append("	ElseIf String.IsNullOrEmpty(val) Then").Append(vbCrLf)
            ret.Append("		Me.").Append(field.PropertyName).Append(" = Nothing").Append(vbCrLf)
            ret.Append("	Else").Append(vbCrLf)
            'ret.Append("		Me.").Append(runtimeFieldName).Append(" = Nothing").Append(vbCrLf)
            ret.Append("		Throw new ApplicationException(""Invalid Decimal Number, field:").Append(runtimeFieldName). _
                                        Append(", value:"" & val)").Append(vbCrLf)
            ret.Append("	End If").Append(vbCrLf)
            ret.Append("End Sub").Append(vbCrLf)
        ElseIf field.isDate Then
            ret.Append("Public Sub set").Append(propertyFieldname).Append("(ByVal val As String)").Append(vbCrLf)
            ret.Append("	If IsDate(val) Then").Append(vbCrLf)
            ret.Append("		Me.").Append(field.PropertyName).Append(" = CDate(val)").Append(vbCrLf)
            ret.Append("	ElseIf String.IsNullOrEmpty(val) Then").Append(vbCrLf)
            ret.Append("		Me.").Append(field.PropertyName).Append(" = Nothing").Append(vbCrLf)
            ret.Append("	Else").Append(vbCrLf)
            'ret.Append("		Me.").Append(runtimeFieldName).Append(" = Nothing").Append(vbCrLf)
            ret.Append("		Throw new ApplicationException(""Invalid Date, field:").Append(runtimeFieldName). _
                                        Append(", value:"" & val)").Append(vbCrLf)

            ret.Append("	End If").Append(vbCrLf)
            ret.Append("End Sub").Append(vbCrLf)
        ElseIf field.RuntimeTypeStr = "System.String" Then
            ret.Append("Public Sub set").Append(propertyFieldname).Append("(ByVal val As String)").Append(vbCrLf)
            ret.Append("	If not String.isNullOrEmpty(val) Then").Append(vbCrLf)
            ret.Append("		Me.").Append(field.PropertyName).Append(" = val").Append(vbCrLf)
            ret.Append("	Else").Append(vbCrLf)
            ret.Append("		Me.").Append(field.PropertyName).Append(" = Nothing").Append(vbCrLf)
            ret.Append("	End If").Append(vbCrLf)
            ret.Append("End Sub").Append(vbCrLf)
        End If



    End Sub
End Class
