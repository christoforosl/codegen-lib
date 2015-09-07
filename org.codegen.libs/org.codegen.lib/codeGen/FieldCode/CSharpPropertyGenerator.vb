Imports System.Text
Imports org.codegen.lib.FileComponents
Imports org.codegen.lib.Tokens
Imports System.Collections.Generic

Public Class CSharpPropertyGenerator
    Inherits IPropertyGenerator

    Public Overrides Function generatePropertyCode(ByVal field As IDBField) As String 

        Dim sImplements As String = String.Empty
        Dim sLengthChecker As String = String.Empty
        Dim xmlIgnore As String = String.Empty

        Dim runtimeFieldName As String = field.RuntimeFieldName()
        Dim fieldPropertyName As String = field.PropertyName()

        If field.RuntimeTypeStr = "System.String" Then
            sLengthChecker = FIVE_TABS & "if (value != null && value.Length > " & field.Size & "){" & vbCrLf
            sLengthChecker &= SIX_TABS & "throw new ModelObjectFieldTooLongException(""" & field.FieldName & """);" & vbCrLf
            sLengthChecker &= FIVE_TABS & "}" & vbCrLf
        End If

        If field.XMLSerializationIgnore Then
            xmlIgnore = "[XmlIgnore()]" & vbCrLf
        End If

        Dim sproperty As StringBuilder = New StringBuilder(xmlIgnore).Append(vbTab). _
              Append(getLinqDataAttribute(field)).Append(TWO_TABS).Append("[DataMember]").Append(vbCrLf)

        sproperty.Append(TWO_TABS).Append("public virtual ").Append(field.getPropertyDataType).Append(" ").Append(fieldPropertyName).Append("{").Append(vbCrLf)
        sproperty.Append(THREE_TABS).Append("get{").Append(THREE_TABS).Append(vbCrLf)
        sproperty.Append(THREE_TABS & vbTab & "return " & Me.Converter(field) & ";" & vbCrLf)
        sproperty.Append(THREE_TABS & "}" & vbCrLf)

        sproperty.Append(THREE_TABS).Append("set {").Append(vbCrLf)
        sproperty.Append(FOUR_TABS).Append("if (ModelObject.valueChanged(_").Append(runtimeFieldName).Append(", value)){").Append(vbCrLf)
        sproperty.Append(sLengthChecker)
        sproperty.Append(FIVE_TABS).Append("if (!this.IsObjectLoading) {").Append(vbCrLf)
        sproperty.Append(SIX_TABS).Append("this.isDirty = true; //").Append(vbCrLf)
        sproperty.Append(SIX_TABS).Append("this.setFieldChanged(").Append(field.getConstantStr).Append(");").Append(vbCrLf)

        Dim xass As Association = Me.getParentAssociationOfField(field)
        If xass IsNot Nothing Then
            sproperty.Append(SIX_TABS).Append("this." & xass.getVariableName()).Append("= null; // reset if id of parent object has changed").Append(vbCrLf)
            sproperty.Append(SIX_TABS).Append("this." & xass.LoadedFlagVariableName()).Append("= false;").Append(vbCrLf)
        End If

        sproperty.Append(FIVE_TABS).Append("}").Append(vbCrLf)
        sproperty.Append(FIVE_TABS).Append(Me.setConverter(field)).Append(";").Append(vbCrLf)

        If field.isPrimaryKey Then
            sproperty.Append(FIVE_TABS).Append("this.raiseBroadcastIdChange();").Append(vbCrLf)
        End If

        sproperty.Append(FOUR_TABS).Append("}").Append(vbCrLf)
        sproperty.Append(THREE_TABS).Append("}").Append(vbCrLf)
        sproperty.Append(TWO_TABS).Append("}").Append(vbCrLf)

        'Me.generateStringSetters(sproperty, field)

        Return sproperty.ToString

    End Function

    Public Overrides Function generateInterfaceDeclaration(ByVal field As IDBField) As String 

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
        Dim keyattr = CStr(IIf(field.isPrimaryKey, String.Empty, TWO_TABS & "[Key]" & vbCrLf))
        Dim reqAttr = CStr(IIf(field.isDBFieldNullable, String.Empty, TWO_TABS & "[Required]" & vbCrLf))
        Dim strAttr = CStr(IIf(field.isString, String.Format(TWO_TABS & "[StringLength({0}, ErrorMessage=""{1} must be {0} characters or less"")]" & vbCrLf, field.Size, field.FieldName), String.Empty))

        Return vbTab & "//Field " & field.FieldName & vbCrLf & keyattr & reqAttr & strAttr & TWO_TABS & "[Column(Name=""" & field.FieldName & """,Storage = ""_" & field.RuntimeFieldName & """, IsPrimaryKey=" & field.isPrimaryKey.ToString.ToLower & ",DbType = """ & field.DBType & nl & """,CanBeNull = " & field.isDBFieldNullable.ToString.ToLower & ")]" & vbCrLf

    End Function

    Private Function Converter(field As IDBField) As String

        If field.isBooleanFromInt Then
            Return String.Format("_{0}.GetValueOrDefault() != 0 ? true: false", field.RuntimeFieldName)

        ElseIf field.isEnumFromInt Then
            Return String.Format("({1}?)_{0}", field.RuntimeFieldName, _
                              ModelGenerator.Current.EnumFieldsCollection.getEnumField(field).enumTypeName)

        Else
            Return "_" & field.RuntimeFieldName()
        End If

    End Function


    Private Function setConverter(field As IDBField) As String

        If field.isBooleanFromInt Then
            Return String.Format("this._{0} = value ? 1 : 0", field.RuntimeFieldName)
            'String.Format("CBool(IIf(_{0}.GetValueOrDefault <> 0, True, False))", field.RuntimeFieldName)

        ElseIf field.isEnumFromInt Then
            Return String.Format("this._{0}=({1})value", field.RuntimeFieldName, _
                              field.getFieldDataType)

        Else
            Return String.Format("this._{0} = value", field.RuntimeFieldName)
        End If

    End Function

End Class