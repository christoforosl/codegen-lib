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

        If field.XMLSerializationIgnore Then
            xmlIgnore = "<XmlIgnore()> _" & vbCrLf
        End If
		Dim pfx As String = ModelGenerator.Current.FieldPropertyPrefix
		If (field.isAuditField) Then pfx = String.Empty

		Dim sproperty As String = xmlIgnore & _
		  vbTab & field.AccessLevel & " Overridable Property " & pfx & "<0> as {1}<IMPL>" & vbCrLf & _
		  vbTab & "Get " & vbCrLf

        If String.IsNullOrEmpty(field.UserSpecifiedDataType) = False Then

            If field.UserSpecifiedDataType = "System.Boolean" _
                       AndAlso (field.OriginalRuntimeType Is Type.GetType("System.Byte") _
                                OrElse field.OriginalRuntimeType Is Type.GetType("System.Int16") _
                                OrElse field.OriginalRuntimeType Is Type.GetType("System.Int32")) Then

                sproperty &= vbTab & vbTab & "if _{0}.hasValue then" & vbCrLf
                sproperty &= vbTab & vbTab & vbTab & "return  CType( _{0}," & field.FieldDataType & ")" & vbCrLf

                sproperty &= vbTab & vbTab & "Else" & vbCrLf
                sproperty &= vbTab & vbTab & vbTab & "return False" & vbCrLf
                sproperty &= vbTab & vbTab & "End if 'end customized check" & vbCrLf

            Else
                sproperty &= vbTab & vbTab & "return CType( _{0}," & field.UserSpecifiedDataType & " )" & _
                             vbCrLf


            End If

        Else
            sproperty &= vbTab & vbTab & "return _{0}" & _
                             vbCrLf
        End If

        Dim PropertyInterface As String = DirectCast( _
                ModelGenerator.Current.CurrentObjectBeingGenerated.FileGroup(ModelObjectFileComponent.KEY),  _
                DotNetClassFileComponent).ClassInterface

        sproperty &= vbTab & "End Get " & vbCrLf & _
            vbTab & "Set{2}" & vbCrLf & "{3}" & _
            vbTab & vbTab & "if ModelObject.valueChanged(_{0}, value) then" & vbCrLf & _
            vbTab & vbTab & vbTab & "if me.IsObjectLoading = false then" & vbCrLf & _
            vbTab & vbTab & vbTab & vbTab & "me.isDirty = true" & vbCrLf & _
            vbTab & vbTab & vbTab & vbTab & "me.setFieldChanged(" & field.getConstantStr & ")" & vbCrLf & _
            vbTab & vbTab & vbTab & "End If" & vbCrLf

        If field.isBoolean Then
            If field.OriginalRuntimeType Is Type.GetType("System.Boolean") Then
                sproperty &= vbTab & vbTab & vbTab & "me._{0} = value.HasValue AndAlso value.Value" & vbCrLf
            Else

                If field.UserSpecifiedDataType.ToLower = "system.boolean?" Then
                    sproperty &= vbTab & vbTab & vbTab & "me._{0} = CInt(IIf(value.HasValue AndAlso value.Value, 1, 0))" & vbCrLf
                Else
                    sproperty &= vbTab & vbTab & vbTab & "me._{0} = CInt(IIf(value, 1, 0))" & vbCrLf
                End If


            End If


        Else
            sproperty &= vbTab & vbTab & vbTab & "me._{0} = value" & vbCrLf
        End If

        If field.isPrimaryKey Then
            sproperty &= vbCrLf & vbTab & vbTab & vbTab & "me.raiseBroadcastIdChange()" & vbCrLf
        End If

        sproperty &= vbCrLf & vbTab & vbTab & "End if" & vbCrLf & _
            vbTab & "End Set " & vbCrLf & _
            vbTab & "End Property " & vbCrLf


        If field.RuntimeTypeStr = "System.String" Then
            sLengthChecker = vbTab & vbTab & "if value isNot Nothing andAlso value.Length > " & field.Size & " Then" & vbCrLf
            sLengthChecker &= vbTab & vbTab & vbTab & "Throw new ModelObjectFieldTooLongException(""" & field.FieldName & """)" & vbCrLf
            sLengthChecker &= vbTab & vbTab & "End If" & vbCrLf

        End If

        Dim iimplements As List(Of String) = New List(Of String)
        If field.isAuditField Then
			iimplements.Add(field.ParentTable.getAuditInterface & "." & field.PropertyName)
        End If

        If String.IsNullOrEmpty(PropertyInterface) = False Then
			iimplements.Add(PropertyInterface & "." & field.PropertyName)

        End If

        If iimplements.Count > 0 Then
            sImplements = " _ " & vbCrLf & vbTab & vbTab & _
                    "Implements " & _
                    String.Join(",", iimplements)
        End If


        Dim runtimeFieldName As String = field.RuntimeFieldName()
        Dim propertyFieldname As String = field.RuntimeFieldName

        Dim ret As StringBuilder = New StringBuilder

        If field.RuntimeTypeStr = "System.String" OrElse field.isPrimaryKey Then
            ret.Append(String.Format(sproperty, runtimeFieldName, _
                                field.getPropertyDataType, "", sLengthChecker))
        Else
            ret.Append(String.Format(sproperty, runtimeFieldName, _
                                field.getPropertyDataType, _
                                "(ByVal value As " & field.getPropertyDataType & ")", sLengthChecker))
        End If

        If runtimeFieldName.ToLower = "readonly" Then runtimeFieldName = "[ReadOnly]"
        If runtimeFieldName.ToLower = "new" Then runtimeFieldName = "[new]"

        ret = ret.Replace("<0>", runtimeFieldName)
        ret = ret.Replace("<IMPL>", sImplements)

        Me.generateStringSetters(ret, field)

        Return ret.ToString

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
