Imports System.Collections.Generic

Namespace Tokens

    Public Class SaveParentCodeToken
        Inherits MultiLingualReplacementToken

        Sub New()
            Me.StringToReplace = "SAVE_PARENT_CODE"
        End Sub
        Public Overrides Function getReplacementCodeCSharp(t As dotnet.IObjectToGenerate) As String
            If t.DbTable.ParentAssociationCount() = 0 Then
                Return String.Empty
            End If

            Dim ret As String = vbTab & "public override void saveParents(IModelObject mo ){" & vbCrLf + vbCrLf
            Dim cname As String = CType(t, ObjectToGenerate).ClassName
            ret &= vbTab & vbTab & cname & " thisMo " & " = ( " & cname & ")mo;" & vbCrLf

            Dim vec As List(Of IAssociation) = t.DbTable.Associations()

            For Each association As IAssociation In vec
                If association.isParent() Then
                    ret &= association.getSaveParentCode(CType(t, ObjectToGenerate).ClassName())
                End If
            Next

            ret &= vbTab & "}"

            Return ret
        End Function
        Public Overrides Function getReplacementCodevb(ByVal t As IObjectToGenerate) As String
            If t.DbTable.ParentAssociationCount() = 0 Then
                Return String.Empty
            End If

            Dim ret As String = vbTab & "public overrides sub saveParents(mo as IModelObject)" & vbCrLf + vbCrLf
            Dim cname As String = CType(t, ObjectToGenerate).ClassName
            ret &= vbTab & vbTab & "Dim thisMo as " & cname & " = directCast(mo, " & cname & ")" & vbCrLf

            Dim vec As List(Of IAssociation) = t.DbTable.Associations()

            For Each association As IAssociation In vec
                If association.isParent() Then
                    ret &= association.getSaveParentCode(CType(t, ObjectToGenerate).ClassName())
                End If
            Next

            ret &= vbTab & "end sub"

            Return ret
        End Function
    End Class

    Public Class SaveChildrenCodeToken
        Inherits MultiLingualReplacementToken

        'sJcode = sJcode.Replace("<SAVE_CHILDREN_CODE>", Me.getSaveChildrenCode())
        Sub New()
            Me.StringToReplace = "SAVE_CHILDREN_CODE"
        End Sub

        Public Overrides Function getReplacementCodeCSharp(t As dotnet.IObjectToGenerate) As String
            If t.DbTable.ChildrenAssociationCount = 0 Then
                Return ""
            End If

            Dim ret As String = ""

            ret &= "#region ""Save Children Code""" & vbCrLf
            ret &= vbTab & "public override void saveChildren(IModelObject mo) {" & vbCrLf & vbCrLf

            ret &= vbTab & vbTab & CType(t, ObjectToGenerate).ClassName & _
                        " ret = (" & CType(t, ObjectToGenerate).ClassName & ")mo;" & vbCrLf

            Dim vec As List(Of IAssociation) = t.DbTable.Associations()

            For Each association As IAssociation In vec

                If association.isParent() = False Then
                    ret += association.getSaveChildrenCode()
                    'Dim sKey As String = association.getDatatype() & "ModelBase"

                End If
            Next

            ret &= vbTab & "}" & vbCrLf
            ret &= "#endregion" & vbCrLf
            Return ret
        End Function
        Public Overrides Function getReplacementCodeVb(ByVal t As IObjectToGenerate) As String

            If t.DbTable.ChildrenAssociationCount = 0 Then
                Return ""
            End If

            Dim ret As String = ""

            ret &= "#Region ""Save Children Code""" & vbCrLf
            ret &= vbTab & "Public overrides Sub saveChildren(mo as IModelObject )" & vbCrLf & vbCrLf
            ret &= vbTab & vbTab & " dim ret as " & CType(t, ObjectToGenerate).ClassName & _
                        " = DirectCast(mo, " & CType(t, ObjectToGenerate).ClassName & ")" & vbCrLf

            Dim vec As List(Of IAssociation) = t.DbTable.Associations()

            For Each association As IAssociation In vec

                If association.isParent() = False Then
                    ret += association.getSaveChildrenCode()
                    'Dim sKey As String = association.getDatatype() & "ModelBase"

                End If
            Next

            ret &= vbTab & "End Sub" & vbCrLf
            ret &= "#End Region" & vbCrLf
            Return ret
        End Function
    End Class

    Public Class LoadFromDataRowToken
        Inherits MultiLingualReplacementToken
        Private Const STR_CType As String = "CType"
        'sJcode = sJcode.Replace("<LOADFROM_DATAROW_CODE>", getLoadFromDataRow())
        Sub New()
            Me.StringToReplace = "FILL_STATEMENT"
        End Sub

        Public Overrides Function getReplacementCodeCSharp(t As dotnet.IObjectToGenerate) As String
            Dim sb As System.Text.StringBuilder = New System.Text.StringBuilder()
            Dim vec As Dictionary(Of String, IDBField) = t.DbTable.Fields()

            Dim setFromRs As String = vbTab & vbTab & vbTab & "if (! dr.IsNull(""{0}"")){{" & vbCrLf & _
                                      vbTab & vbTab & vbTab & vbTab & "obj.{1} = {2}(dr.item(""{0}""){3});" & vbCrLf & _
                                      vbTab & vbTab & vbTab & "}}"


            For Each field As DBField In vec.Values
                Dim customType As String = String.Empty
                Dim converter As String = "CType"
                customType = "," & field.UserSpecifiedDataType

                Dim lFormat As String = String.Format(setFromRs, field.FieldName(), _
                                                        field.RuntimeFieldName, _
                                                        converter, _
                                                        customType)
                sb.Append(lFormat)
                sb.Append(";")
                sb.Append(vbCrLf)
            Next
            Return sb.ToString()
        End Function

        Public Overrides Function getReplacementCodeVb(ByVal t As IObjectToGenerate) As String

            Dim sb As System.Text.StringBuilder = New System.Text.StringBuilder()
            Dim vec As Dictionary(Of String, IDBField) = t.DbTable.Fields()

            Dim setFromRs As String = vbTab & vbTab & vbTab & "if dr.IsNull(""{0}"") = false Then" & vbCrLf & _
                                      vbTab & vbTab & vbTab & vbTab & "obj.{1} = {2}(dr.item(""{0}""){3})" & vbCrLf & _
                                      vbTab & vbTab & vbTab & "End if"

            'me.setFields(rs.getString(FIELD));

            For Each field As DBField In vec.Values
                Dim customType As String = String.Empty
                Dim converter As String = "CType"
                customType = "," & field.UserSpecifiedDataType

                Dim lFormat As String = String.Format(setFromRs, field.FieldName(), _
                                                        field.RuntimeFieldName, _
                                                        converter, _
                                                        customType)
                sb.Append(lFormat)
                sb.Append(vbCrLf)
            Next

            Return sb.ToString()
        End Function

       

    End Class

	Public Class PrimaryKeyAutogenAttr
		Inherits MultiLingualReplacementToken

		Sub New()
			Me.StringToReplace = "IS_PRIMARY_KEY_AUOGEN_FALSE"
		End Sub

		Public Overrides Function getReplacementCodeCSharp(t As dotnet.IObjectToGenerate) As String
			If t.DbTable.isPrimaryKeyAutogenerated = False Then

				Return vbCrLf & "[AttrIsPrimaryKeyAutogenerated(false)]"
			Else
				Return ""
			End If

		End Function

		Public Overrides Function getReplacementCodeVB(t As dotnet.IObjectToGenerate) As String
			If t.DbTable.isPrimaryKeyAutogenerated = False Then
				Return " _" & vbCrLf & "<AttrIsPrimaryKeyAutogenerated(False)>"
			Else
				Return ""
			End If

		End Function
	End Class

    Public Class GetUpdateDBCommandToken
        Inherits MultiLingualReplacementToken

        Sub New()
            Me.StringToReplace = "FILL_STATEMENT"
        End Sub

        Public Overrides Function getReplacementCodeCSharp(t As dotnet.IObjectToGenerate) As String

            Dim ret As System.Text.StringBuilder = New System.Text.StringBuilder()
            Dim vec As Dictionary(Of String, IDBField) = t.DbTable.getTableFields() 'loads the field vector in out vector (vec).
            Dim pk As String = ""

            Dim parameterCounter As Integer = 1
            Dim keyparam As Integer = 0
            Dim pkParam As String = ""

            For Each field As DBField In vec.Values

                If field.FieldName().Equals(t.DbTable.getPrimaryKeyName()) Then
                    ' do not process primary key here
                    pkParam = field.getSQLParameter()
                Else

                    ret.Append(field.getSQLParameter())
                    parameterCounter += 1

                End If
            Next

            ret.Append(vbCrLf)
            ret.Append(vbTab & vbTab & vbTab & "if ( obj.isNew ) {" & vbCrLf)
            If (t.DbTable.isPrimaryKeyAutogenerated = False) Then
                ret.Append(vbTab & vbTab & vbTab & "//pk is not autogenerated, add primary key if we are inserting..." & vbCrLf)
                ret.Append(vbTab & vbTab & vbTab & pkParam & ";" & vbCrLf)
            End If
            ret.Append(vbTab & vbTab & vbTab & "} else {" & vbCrLf)
            ret.Append(vbTab & vbTab & vbTab & "//only add primary key if we are updating and as the last parameter" & vbCrLf)
            ret.Append(vbTab & vbTab & vbTab & vbTab & pkParam)

            ret.Append(vbTab & vbTab & "}" & vbCrLf)

            Return ret.ToString().Replace("<MODEL_CLASS_NAME>", CType(t, ObjectToGenerate).ClassName)

        End Function

        Public Overrides Function getReplacementCodeVb(ByVal t As IObjectToGenerate) As String

            Dim ret As System.Text.StringBuilder = New System.Text.StringBuilder()
            Dim vec As Dictionary(Of String, IDBField) = t.DbTable.getTableFields() 'loads the field vector in out vector (vec).
            Dim pk As String = ""

            Dim parameterCounter As Integer = 1
            Dim keyparam As Integer = 0
            Dim pkParam As String = ""

            For Each field As DBField In vec.Values

                If field.FieldName().Equals(t.DbTable.getPrimaryKeyName()) Then
                    ' do not process primary key here
                    pkParam = field.getSQLParameter()
                Else

                    ret.Append(field.getSQLParameter())
                    parameterCounter += 1

                End If
            Next

            ret.Append(vbCrLf)
            ret.Append(vbTab & vbTab & vbTab & "if obj.isNew Then" & vbCrLf)
            If (t.DbTable.isPrimaryKeyAutogenerated = False) Then
                ret.Append(vbTab & vbTab & vbTab & "'pk is not autogenerated, add primary key if we are inserting..." & vbCrLf)
                ret.Append(pkParam)
            End If
            ret.Append(vbTab & vbTab & vbTab & "Else" & vbCrLf)
            ret.Append(vbTab & vbTab & vbTab & "'only add primary key if we are updating and as the last parameter" & vbCrLf)
            ret.Append(pkParam & vbCrLf)

            ret.Append(vbTab & vbTab & vbTab & "End if '" & vbCrLf)

            Return ret.ToString().Replace("<MODEL_CLASS_NAME>", CType(t, ObjectToGenerate).ClassName)

        End Function
    End Class

	Public Class LoadFromRSToken
		Inherits MultiLingualReplacementToken

		Sub New()
			Me.StringToReplace = "LOADFROMRS_CODE"
		End Sub
		Public Overrides Function getReplacementCodeCSharp(t As dotnet.IObjectToGenerate) As String
			Dim sb As System.Text.StringBuilder = New System.Text.StringBuilder()
			Dim vec As Dictionary(Of String, IDBField) = t.DbTable.Fields()

			Dim setFromRs As String = vbTab & vbTab & vbTab & "if (!this.reader.IsDBNull(DATAREADER_{2}) ) {{" & vbCrLf & _
					vbTab & vbTab & vbTab & vbTab & "obj.{0} = {1};" & vbCrLf & _
					vbTab & vbTab & vbTab & "}}"

			'me.setFields(rs.getString(FIELD));

			For Each field As DBField In vec.Values
				'field.OriginalRuntimeType
				Dim fldsetter As String = String.Format(setFromRs, _
				  field.PropertyName, _
				  Me.getDataReaderGetter(field), field.getConstant())
				sb.Append(fldsetter)

				sb.Append(vbCrLf)
			Next


			Return sb.ToString()
		End Function
		Public Overrides Function getReplacementCodeVb(ByVal t As IObjectToGenerate) As String

			Dim sb As System.Text.StringBuilder = New System.Text.StringBuilder()
			Dim vec As Dictionary(Of String, IDBField) = t.DbTable.Fields()

			Dim setFromRs As String = vbTab & vbTab & vbTab & "if me.reader.IsDBNull(DATAREADER_{2}) = false Then" & vbCrLf & _
			  vbTab & vbTab & vbTab & vbTab & "obj.{0} = {1}" & vbCrLf & _
			  vbTab & vbTab & vbTab & "End if"

			'me.setFields(rs.getString(FIELD));

			For Each field As DBField In vec.Values
				'field.OriginalRuntimeType
				Dim fldsetter As String = String.Format(setFromRs, _
				  field.PropertyName, _
				  Me.getDataReaderGetter(field), field.getConstant())
				sb.Append(fldsetter)
				sb.Append(vbCrLf)
			Next


			Return sb.ToString()
		End Function

		Private Function getDataReaderGetter(ByVal field As DBField) As String

			Dim sString As String = ""
			Dim skey As String = field.getConstant()
			Dim meMarker As String = "me"
			If ModelGenerator.Current.dotNetLanguage = ModelGenerator.enumLanguage.CSHARP Then
				meMarker = "this"
			End If
			If field.OriginalRuntimeType Is System.Type.GetType("System.Date") OrElse _
			   field.OriginalRuntimeType Is System.Type.GetType("System.DateTime") Then

				sString &= meMarker & ".reader.GetDateTime" & "(DATAREADER_" & skey & ")"

			ElseIf field.OriginalRuntimeType Is System.Type.GetType("System.Int16") Then
				sString &= "CInt(" & meMarker & ".reader.GetInt16" & "(DATAREADER_" & skey & "))"
				'Return "getInt32"

			ElseIf field.OriginalRuntimeType Is System.Type.GetType("System.Int32") Then
				sString &= meMarker & ".reader.GetInt32" & "(DATAREADER_" & skey & ")"
				'Return "getInt32"

			ElseIf field.OriginalRuntimeType Is System.Type.GetType("System.Int64") Then
				sString &= meMarker & ".reader.GetInt64" & "(DATAREADER_" & skey & ")"
				'Return "getInt64"

			ElseIf field.OriginalRuntimeType Is System.Type.GetType("System.Decimal") Then
				sString &= meMarker & ".reader.GetDecimal" & "(DATAREADER_" & skey & ")"
				'Return "getDecimal"

			ElseIf field.OriginalRuntimeType Is System.Type.GetType("System.Double") Then
				'Return "getDouble"
				sString &= "CDec( " & meMarker & ".reader.GetDouble" & "(DATAREADER_" & skey & "))"

			ElseIf field.OriginalRuntimeType Is System.Type.GetType("System.Single") Then
				'Return "GetFloat"
				sString &= "CDec(me.reader.GetFloat" & "(DATAREADER_" & skey & "))"

			ElseIf field.OriginalRuntimeType Is System.Type.GetType("System.String") Then
				sString &= meMarker & ".reader.GetString" & "(DATAREADER_" & skey & ")"
				'Return "getString"

			ElseIf field.OriginalRuntimeType Is System.Type.GetType("System.Boolean") Then
				sString &= meMarker & ".reader.GetBoolean" & "(DATAREADER_" & skey & ")"
				'Return "getBoolean"
			ElseIf field.OriginalRuntimeType Is System.Type.GetType("System.Byte") Then
				sString &= meMarker & ".reader.GetByte" & "(DATAREADER_" & skey & ")"
			Else
				Throw New ApplicationException(field.FieldName & _
						  ":Unhandled DataReader getter for type:" & _
						  field.RuntimeType.ToString)
			End If

			If String.IsNullOrEmpty(field.UserSpecifiedDataType) = False _
			  AndAlso field.UserSpecifiedDataType <> field.OriginalRuntimeType.ToString Then
				If ModelGenerator.Current.dotNetLanguage = ModelGenerator.enumLanguage.CSHARP Then
					If (field.UserSpecifiedDataType = "System.Boolean") Then
						sString = meMarker & ".reader.GetInt32" & "(DATAREADER_" & skey & ")==1;"
					End If
				Else
					sString = "CType(" & sString & "," & field.UserSpecifiedDataType & ")"
				End If
			End If
			Return sString

		End Function

	End Class

	Public Class DataReaderConstantsToken
		Inherits MultiLingualReplacementToken

		Sub New()
			Me.StringToReplace = "DATA_READER_CONSTANTS"
		End Sub

		Public Overrides Function getReplacementCodeCSharp(t As dotnet.IObjectToGenerate) As String
			Dim sb As System.Text.StringBuilder = New System.Text.StringBuilder()
			Dim vec As Dictionary(Of String, IDBField) = t.DbTable.Fields()

			Dim i As Integer = 1
			For Each field As DBField In vec.Values

				sb.Append(vbTab & vbTab & vbTab & "const int DATAREADER_" & field.getConstant() & " = " & (i - 1))
				i += 1
				sb.Append(";")
				sb.Append(vbCrLf)
			Next

			Return sb.ToString()
		End Function
		Public Overrides Function getReplacementCodeVb(ByVal t As IObjectToGenerate) As String

			Dim sb As System.Text.StringBuilder = New System.Text.StringBuilder()
			Dim vec As Dictionary(Of String, IDBField) = t.DbTable.Fields()

			Dim i As Integer = 1
			For Each field As DBField In vec.Values

				sb.Append(vbTab & vbTab & vbTab & "Const DATAREADER_" & field.getConstant() & " as Integer = " & (i - 1))
				i += 1
				sb.Append(vbCrLf)
			Next

			Return sb.ToString()
		End Function

	End Class

End Namespace