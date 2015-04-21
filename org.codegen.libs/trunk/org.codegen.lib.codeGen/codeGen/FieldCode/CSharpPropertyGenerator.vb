Imports System.Text
Imports org.codegen.lib.codeGen.FileComponents
Imports org.codegen.lib.codeGen.Tokens
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
		Dim pfx As String = ModelGenerator.Current.FieldPropertyPrefix
		If (field.isAuditField) Then pfx = String.Empty

		Dim sproperty As String = xmlIgnore & _
		  vbTab & field.AccessLevel.ToLower & " virtual " & field.getPropertyDataType & " " & _
		  pfx & runtimeFieldName & "  {" & vbCrLf & _
		  vbTab & "get {" & vbCrLf

		If String.IsNullOrEmpty(field.UserSpecifiedDataType) = False Then

			If field.UserSpecifiedDataType = "System.Boolean" _
					   AndAlso (field.OriginalRuntimeType Is Type.GetType("System.Byte") _
								OrElse field.OriginalRuntimeType Is Type.GetType("System.Int16") _
								OrElse field.OriginalRuntimeType Is Type.GetType("System.Int32")) Then

				sproperty &= vbTab & vbTab & "if ( _" & runtimeFieldName & ".HasValue ) {" & vbCrLf
				sproperty &= vbTab & vbTab & vbTab & "return _" & runtimeFieldName & ".GetValueOrDefault()==1;" & vbCrLf

				sproperty &= vbTab & vbTab & "} else {" & vbCrLf
				sproperty &= vbTab & vbTab & vbTab & "return false;" & vbCrLf
				sproperty &= vbTab & vbTab & "} //end customized check" & vbCrLf

			Else
				sproperty &= vbTab & vbTab & "return (" & field.UserSpecifiedDataType & ") _" & runtimeFieldName & ";" & _
							 vbCrLf


			End If

		Else
			sproperty &= vbTab & vbTab & "return _" & runtimeFieldName & ";" & _
							 vbCrLf
		End If

		Dim PropertyInterface As String = DirectCast( _
				ModelGenerator.Current.CurrentObjectBeingGenerated.FileGroup(ModelObjectFileComponent.KEY),  _
				DotNetClassFileComponent).ClassInterface

		sproperty &= vbTab & "} " & vbCrLf & _
			vbTab & "set {" & vbCrLf & _
			vbTab & vbTab & "if (ModelObject.valueChanged(_" & runtimeFieldName & ", value)) {" & vbCrLf & _
			vbTab & vbTab & vbTab & "if (!this.IsObjectLoading ) {" & vbCrLf & _
			vbTab & vbTab & vbTab & vbTab & "this.isDirty = true;" & vbCrLf & _
			vbTab & vbTab & vbTab & vbTab & "this.setFieldChanged(" & field.getConstantStr & ");" & vbCrLf & _
			vbTab & vbTab & vbTab & "}" & vbCrLf

		If field.isBoolean Then
			If field.OriginalRuntimeType Is Type.GetType("System.Boolean") Then
				sproperty &= vbTab & vbTab & vbTab & "this._" & runtimeFieldName & " = value.HasValue && value.Value;" & vbCrLf
			Else
				sproperty &= vbTab & vbTab & vbTab & "this._" & runtimeFieldName & " = value.HasValue && value.Value? 1: 0;" & vbCrLf
			End If


		Else
			sproperty &= vbTab & vbTab & vbTab & "this._" & runtimeFieldName & " = value;" & vbCrLf
		End If

		If field.isPrimaryKey Then
			sproperty &= vbCrLf & vbTab & vbTab & vbTab & "this.raiseBroadcastIdChange();" & vbCrLf
		End If

		sproperty &= vbCrLf & vbTab & vbTab & "}" & vbCrLf & _
			vbTab & "}  " & vbCrLf & _
			vbTab & "}" & vbCrLf


		If field.RuntimeTypeStr = "System.String" Then
			sLengthChecker = vbTab & vbTab & "if ( value != null && value.length() > " & field.Size & " {" & vbCrLf
			sLengthChecker &= vbTab & vbTab & vbTab & "throw new ModelObjectFieldTooLongException(""" & field.FieldName & """)" & vbCrLf
			sLengthChecker &= vbTab & vbTab & "}" & vbCrLf

		End If

		Dim iimplements As List(Of String) = New List(Of String)
		If field.isAuditField Then
			iimplements.Add(field.ParentTable.getAuditInterface & "." & field.RuntimeFieldName)
		End If

		If String.IsNullOrEmpty(PropertyInterface) = False Then
			iimplements.Add(PropertyInterface & "." & field.RuntimeFieldName)

		End If


		Dim ret As StringBuilder = New StringBuilder

		ret.Append(sproperty)

		If runtimeFieldName.ToLower = "readonly" Then runtimeFieldName = "[ReadOnly]"
		If runtimeFieldName.ToLower = "new" Then runtimeFieldName = "[new]"

		If field.isBoolean Then
			ret.Append("public void set").Append(propertyFieldname).Append("(String val ){").Append(vbCrLf)
			ret.Append("	if (String.IsNullOrEmpty(val)) {").Append(vbCrLf)
			ret.Append("		this.").Append(field.PropertyName).Append(" = null;").Append(vbCrLf)
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
			sb.Append(vbTab & field.getPropertyDataType & " " & fname & " {get;set;} ")
			sb.Append(vbCrLf)
		End If

		Return sb.ToString

	End Function
End Class