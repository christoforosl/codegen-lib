Imports System.Text
Imports org.codegen.lib.FileComponents
Imports org.codegen.lib.Tokens
Imports System.Collections.Generic

Public Class CSharpPropertyGenerator
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
            sLengthChecker = vbTab & vbTab & "if (value != null && value.Length > " & field.Size & "){" & vbCrLf
            sLengthChecker &= vbTab & vbTab & vbTab & "throw new ModelObjectFieldTooLongException(""" & field.FieldName & """);" & vbCrLf
            sLengthChecker &= vbTab & vbTab & "}" & vbCrLf
        End If

        If field.XMLSerializationIgnore Then
            xmlIgnore = "[XmlIgnore()]" & vbCrLf
        End If

        Dim pfx As String = ModelGenerator.Current.FieldPropertyPrefix
        If (field.isAuditField) Then pfx = String.Empty

        Dim sproperty As StringBuilder = New StringBuilder(xmlIgnore).Append(vbTab). _
              Append(getLinqDataAttribute(field)).Append(vbTab).Append("[DataMember]public virtual ").Append(field.getPropertyDataType).Append(" "). _
        Append(pfx).Append(runtimeFieldName).Append("{").Append(vbCrLf). _
        Append(vbTab).Append("get{").Append(vbCrLf)

        sproperty.Append(vbTab & vbTab & "return " & Me.Converter(field) & ";" & vbCrLf)

        sproperty.Append(vbTab & "}" & vbCrLf)

        sproperty.Append(vbTab).Append("set {").Append(vbCrLf)
        sproperty.Append(vbTab).Append(vbTab).Append("if (ModelObject.valueChanged(_").Append(runtimeFieldName).Append(", value)){").Append(vbCrLf)
        sproperty.Append(sLengthChecker)
        sproperty.Append(vbTab).Append(vbTab).Append(vbTab & "if (!this.IsObjectLoading) {").Append(vbCrLf)
        sproperty.Append(vbTab).Append(vbTab).Append(vbTab & vbTab).Append("this.isDirty = true;").Append(vbCrLf)
        sproperty.Append(vbTab).Append(vbTab).Append(vbTab & vbTab).Append("this.setFieldChanged(").Append(field.getConstantStr).Append(");").Append(vbCrLf)
        sproperty.Append(vbTab).Append(vbTab).Append(vbTab & "}").Append(vbCrLf)
        sproperty.Append(vbTab).Append(vbTab).Append(Me.setConverter(field)).Append(vbCrLf)

        If field.isPrimaryKey Then
            sproperty.Append(vbCrLf & vbTab & vbTab & vbTab & "this.raiseBroadcastIdChange();" & vbCrLf)
        End If

        sproperty.Append(vbCrLf & vbTab & vbTab & "}" & vbCrLf & _
           vbTab & vbTab & "}" & vbCrLf & _
           vbTab & "}" & vbCrLf)

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
            sb.Append(vbTab & field.getPropertyDataType & " " & fname & " {get;set;} ")
            sb.Append(vbCrLf)
        End If

        Return sb.ToString

    End Function

    Private Sub generateStringSetters(ret As StringBuilder, ByVal field As IDBField)

        Dim runtimeFieldName As String = field.RuntimeFieldName()
        Dim propertyFieldname As String = field.RuntimeFieldName

        If field.isBoolean Then
            ret.Append("public void set").Append(propertyFieldname).Append("(String val ){").Append(vbCrLf)
            ret.Append("	if (String.IsNullOrEmpty(val)) {").Append(vbCrLf)
            ret.Append("		this.").Append(field.PropertyName).Append(" = false;").Append(vbCrLf)
            ret.Append("	} else {").Append(vbCrLf)
            ret.Append("	    bool newval = (""1""==val || ""true""==val.ToLower()) ;").Append(vbCrLf)
            ret.Append("	    this.").Append(field.PropertyName).Append(" = newval;").Append(vbCrLf)
            ret.Append("	}").Append(vbCrLf)
            ret.Append("}").Append(vbCrLf)

        ElseIf field.isInteger Then
            ret.Append("public void set").Append(propertyFieldname).Append("(String val){").Append(vbCrLf)
            ret.Append("	if (Information.IsNumeric(val)) {").Append(vbCrLf)
            ret.Append("		this.").Append(field.PropertyName).Append(" = Convert.ToInt64(val);").Append(vbCrLf)
            ret.Append("	} else if (String.IsNullOrEmpty(val)) {").Append(vbCrLf)
            If (field.isPrimaryKey) Then
                ret.Append("		throw new ApplicationException(""Cant update Primary Key to Null"");").Append(vbCrLf)
            Else
                ret.Append("		this.").Append(field.PropertyName).Append(" = null;").Append(vbCrLf)

            End If
            ret.Append("	} else {").Append(vbCrLf)

            ret.Append("		throw new ApplicationException(""Invalid Integer Number, field:").Append(runtimeFieldName). _
                                        Append(", value:"" + val);").Append(vbCrLf)
            ret.Append("	}").Append(vbCrLf)
            ret.Append("}").Append(vbCrLf)

        ElseIf field.isDecimal Then
            ret.Append("public void set").Append(propertyFieldname).Append("(String val ){").Append(vbCrLf)
            ret.Append("	if (Information.IsNumeric(val)) {").Append(vbCrLf)
            ret.Append("		this.").Append(field.PropertyName).Append(" =  Convert.ToDecimal(val);").Append(vbCrLf)
            ret.Append("	} else if ( string.IsNullOrEmpty(val) ) {").Append(vbCrLf)
            ret.Append("		this.").Append(field.PropertyName).Append(" = null;").Append(vbCrLf)
            ret.Append("	} else {").Append(vbCrLf)
            'ret.Append("		Me.").Append(runtimeFieldName).Append(" = Nothing").Append(vbCrLf)
            ret.Append("		throw new ApplicationException(""Invalid Decimal Number, field:").Append(runtimeFieldName). _
                                        Append(", value:"" + val);").Append(vbCrLf)
            ret.Append("	}").Append(vbCrLf)
            ret.Append("}").Append(vbCrLf)

        ElseIf field.isDate Then
            ret.Append("public void set").Append(propertyFieldname).Append("( String val ){").Append(vbCrLf)
            ret.Append("	if (Information.IsDate(val)) {").Append(vbCrLf)
            ret.Append("		this.").Append(field.PropertyName).Append(" = Convert.ToDateTime(val);").Append(vbCrLf)
            ret.Append("	} else if (String.IsNullOrEmpty(val) ) {").Append(vbCrLf)
            ret.Append("		this.").Append(field.PropertyName).Append(" = null;").Append(vbCrLf)
            ret.Append("	} else {").Append(vbCrLf)

            ret.Append("		throw new ApplicationException(""Invalid Date, field:").Append(runtimeFieldName). _
                                        Append(", value:"" + val);").Append(vbCrLf)

            ret.Append("	}").Append(vbCrLf)
            ret.Append("}").Append(vbCrLf)
        ElseIf field.RuntimeTypeStr = "System.String" Then
            ret.Append("public void set").Append(propertyFieldname).Append("( String val ) {").Append(vbCrLf)
            ret.Append("	if (! string.IsNullOrEmpty(val)) {").Append(vbCrLf)
            ret.Append("		this.").Append(field.PropertyName).Append(" = val;").Append(vbCrLf)
            ret.Append("	} else {").Append(vbCrLf)
            ret.Append("		this.").Append(field.PropertyName).Append(" = null;").Append(vbCrLf)
            ret.Append("	}").Append(vbCrLf)
            ret.Append("}").Append(vbCrLf)
        End If
    End Sub

    Private Function getLinqDataAttribute(field As IDBField) As String
        Dim nl As String = CStr(IIf(field.isDBFieldNullable, "", " NOT NULL"))

        Return "[Column(Name=""" & field.FieldName & """,Storage = ""_" & field.RuntimeFieldName & """, IsPrimaryKey=" & field.isPrimaryKey.ToString.ToLower & ",DbType = """ & field.DBType & nl & """,CanBeNull = " & field.isDBFieldNullable.ToString.ToLower & ")]" & vbCrLf
    End Function

    Private Function Converter(field As IDBField) As String

        If field.isBooleanFromInt Then
            Return String.Format("_{0}.GetValueOrDefault != 0 ? true: false", field.RuntimeFieldName)
        Else
            Return "_" & field.RuntimeFieldName()
        End If

    End Function


    Private Function setConverter(field As IDBField) As String

        If field.isBooleanFromInt Then
            Return String.Format("this._{0} = value ? 1 : 0", field.RuntimeFieldName)
            'String.Format("CBool(IIf(_{0}.GetValueOrDefault <> 0, True, False))", field.RuntimeFieldName)
        Else
            Return String.Format("this._{0} = value", field.RuntimeFieldName)
        End If

    End Function

End Class