Imports System.Collections.Generic

Namespace Tokens

    Public Class SaveParentCodeToken
        Inherits MultiLingualReplacementToken

        Sub New()
            Me.StringToReplace = "SAVE_PARENT_CODE"
        End Sub
        Public Overrides Function getReplacementCodeCSharp(t As IObjectToGenerate) As String
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

        Public Overrides Function getReplacementCodeCSharp(t As IObjectToGenerate) As String
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

        Sub New()
            Me.StringToReplace = "FILL_STATEMENT"
        End Sub

        Public Overrides Function getReplacementCodeCSharp(t As IObjectToGenerate) As String
            Dim sb As System.Text.StringBuilder = New System.Text.StringBuilder()
            Dim vec As Dictionary(Of String, IDBField) = t.DbTable.Fields()

            Dim setFromRs As String = vbTab & vbTab & vbTab & "if (! dr.IsNull(""{0}"")){{" & vbCrLf & _
                                      vbTab & vbTab & vbTab & vbTab & "obj.{1} = {2}(dr.item(""{0}""){3});" & vbCrLf & _
                                      vbTab & vbTab & vbTab & "}}"


            For Each field As DBField In vec.Values

                Dim lFormat As String = String.Format(setFromRs, field.FieldName(), _
                                                        field.RuntimeFieldName, _
                                                        String.Empty, _
                                                        String.Empty)
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


                Dim lFormat As String = String.Format(setFromRs, field.FieldName(), _
                                                        field.RuntimeFieldName, _
                                                        String.Empty, _
                                                        String.Empty)
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

        Public Overrides Function getReplacementCodeCSharp(t As IObjectToGenerate) As String
            If t.DbTable.isPrimaryKeyAutogenerated = False Then

                Return vbCrLf & "[AttrIsPrimaryKeyAutogenerated(false)]"
            Else
                Return ""
            End If

        End Function

        Public Overrides Function getReplacementCodeVB(t As IObjectToGenerate) As String
            If t.DbTable.isPrimaryKeyAutogenerated = False Then
                Return " _" & vbCrLf & "<AttrIsPrimaryKeyAutogenerated(False)>"
            Else
                Return ""
            End If

        End Function
    End Class

    Public Class FillStatementToken
        Inherits MultiLingualReplacementToken

        Sub New()
            Me.StringToReplace = "FILL_STATEMENT"
        End Sub

        Public Overrides Function getReplacementCodeCSharp(t As IObjectToGenerate) As String

            Dim ret As System.Text.StringBuilder = New System.Text.StringBuilder()
            Dim vec As Dictionary(Of String, IDBField) = t.DbTable.getTableFields() 'loads the field vector in out vector (vec).
            Dim pk As String = ""

            Dim parameterCounter As Integer = 1
            Dim keyparam As Integer = 0
            Dim pkParam As String = ""

            For Each field As DBField In vec.Values
                If Not field.isBinaryField() Then
                    If field.FieldName().Equals(t.DbTable.getPrimaryKeyName()) Then
                        ' do not process primary key here
                        pkParam = field.getSQLParameter()
                    Else

                        ret.Append(field.getSQLParameter())
                        parameterCounter += 1

                    End If
                End If

            Next

            ret.Append(vbCrLf)
            If (t.DbTable.isPrimaryKeyAutogenerated = False) Then
                ret.Append(pkParam).Append(vbCrLf)
            Else
                ret.Append(vbTab).Append(vbTab).Append(vbTab).Append("if (obj.isNew){").Append(vbCrLf)
               
                If ModelGenerator.Current.DbConnStringDialect = DBUtils.enumSqlDialect.ORACLE Then
                    ' for oracle register output parameter
                    ret.Append(vbTab).Append(vbTab).Append(vbTab).Append(vbTab)
                    ret.Append(String.Format("IDataParameter prm = this.dbConn.getParameterInOut({0}.{1});", CType(t, ObjectToGenerate).ClassName, t.DbTable.getPrimaryKeyField.getConstantStr)).Append(vbCrLf)
                    ret.Append(vbTab).Append(vbTab).Append(vbTab).Append(vbTab)
                    ret.Append("prm.DbType = DbType.Int64;").Append(vbCrLf)
                    ret.Append(vbTab).Append(vbTab).Append(vbTab).Append(vbTab)
                    ret.Append("prm.Direction = ParameterDirection.Output;").Append(vbCrLf)
                    ret.Append(vbTab).Append(vbTab).Append(vbTab).Append(vbTab)
                    ret.Append("stmt.Parameters.Add(prm);").Append(vbCrLf)


                    ret.Append(vbCrLf)
                End If

                ret.Append(vbTab).Append(vbTab).Append(vbTab).Append("} else {").Append(vbCrLf)
                ret.Append(vbTab).Append(vbTab).Append(vbTab & "//only add primary key if we are updating and as the last parameter").Append(vbCrLf)
                ret.Append(vbTab).Append(pkParam)
                ret.Append(vbTab).Append(vbTab).Append(vbTab).Append("}").Append(vbCrLf)
            End If

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
                If Not field.isBinaryField() Then
                    If field.FieldName().Equals(t.DbTable.getPrimaryKeyName()) Then
                        ' do not process primary key here
                        pkParam = field.getSQLParameter()
                    Else

                        ret.Append(field.getSQLParameter())
                        parameterCounter += 1

                    End If
                End If

            Next

            ret.Append(vbCrLf)
            If (t.DbTable.isPrimaryKeyAutogenerated = False) Then
                ret.Append(pkParam).Append(vbCrLf)
            Else
                ret.Append(vbTab).Append(vbTab).Append(vbTab).Append("If (obj.isNew) Then").Append(vbCrLf)
                If ModelGenerator.Current.DbConnStringDialect = DBUtils.enumSqlDialect.ORACLE Then
                    ' for oracle register output parameter
                    ret.Append(vbTab).Append(vbTab).Append(vbTab).Append(vbTab)
                    ret.Append(String.Format("Dim prm = this.dbConn.getParameterInOut({0}.{1});", CType(t, ObjectToGenerate).ClassName, t.DbTable.getPrimaryKeyField.getConstantStr)).Append(vbCrLf)
                    ret.Append(vbTab).Append(vbTab).Append(vbTab).Append(vbTab)
                    ret.Append("prm.DbType = DbType.Int64").Append(vbCrLf)
                    ret.Append(vbTab).Append(vbTab).Append(vbTab).Append(vbTab)
                    ret.Append("prm.Direction = ParameterDirection.Output").Append(vbCrLf)
                    ret.Append(vbTab).Append(vbTab).Append(vbTab).Append(vbTab)
                    ret.Append("stmt.Parameters.Add(prm)").Append(vbCrLf)


                    ret.Append(vbCrLf)
                End If

                ret.Append(vbTab).Append(vbTab).Append(vbTab).Append("Else").Append(vbCrLf)
                ret.Append(vbTab).Append(vbTab).Append(vbTab & "'only add primary key if we are updating and as the last parameter").Append(vbCrLf)
                ret.Append(vbTab).Append(pkParam)
                ret.Append(vbTab & vbTab & vbTab & "End if" & vbCrLf)
            End If

            'ret.Append(vbTab & vbTab & vbTab & "if obj.isNew Then" & vbCrLf)
            'If (t.DbTable.isPrimaryKeyAutogenerated = False) Then
            '    ret.Append(vbTab & vbTab & vbTab & "'pk is not autogenerated, add primary key if we are inserting..." & vbCrLf)
            '    ret.Append(pkParam)
            'End If
            'ret.Append(vbTab & vbTab & vbTab & "Else" & vbCrLf)
            'ret.Append(vbTab & vbTab & vbTab & "'only add primary key if we are updating and as the last parameter" & vbCrLf)
            'ret.Append(pkParam & vbCrLf)

            '

            Return ret.ToString().Replace("<MODEL_CLASS_NAME>", CType(t, ObjectToGenerate).ClassName)

        End Function
    End Class

    Public Class DBMapperBaseClassToken
        Inherits ReplacementToken
        Sub New()
            Me.StringToReplace = "MAPPER_BASE_CLASS"
        End Sub
        Public Overrides Function getReplacementCode(t As IObjectToGenerate) As String
            If ModelGenerator.Current.DbConnStringDialect = DBUtils.enumSqlDialect.ORACLE Then
                Return "OracleDBMapper"
            Else
                Return "DBMapper"
            End If
        End Function
    End Class

    Public Class LoadFromRSToken
        Inherits MultiLingualReplacementToken

        Sub New()
            Me.StringToReplace = "LOADFROMRS_CODE"
        End Sub
        Public Overrides Function getReplacementCodeCSharp(t As IObjectToGenerate) As String
            Dim sb As System.Text.StringBuilder = New System.Text.StringBuilder()
            Dim vec As Dictionary(Of String, IDBField) = t.DbTable.Fields()

            Dim setFromRs As String = vbTab & vbTab & vbTab & "if (!this.reader.IsDBNull(DATAREADER_{2}) ) {{" & vbCrLf & _
                    vbTab & vbTab & vbTab & vbTab & "obj.{0} = {1};" & vbCrLf & _
                    vbTab & vbTab & vbTab & "}}"

            'me.setFields(rs.getString(FIELD));

            For Each field As IDBField In vec.Values
                If Not field.isBinaryField() Then

                    Dim fldsetter As String = String.Format(setFromRs, _
                      field.PropertyName, _
                      Me.getDataReaderGetter(field), field.getConstant())
                    sb.Append(fldsetter)

                    sb.Append(vbCrLf)
                End If
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
                If Not field.isBinaryField() Then
                    Dim fldsetter As String = String.Format(setFromRs, _
                      field.PropertyName, _
                      Me.getDataReaderGetter(field), field.getConstant())
                    sb.Append(fldsetter)
                    sb.Append(vbCrLf)
                End If
            Next


            Return sb.ToString()

        End Function

        Private Function getDataReaderGetter(ByVal field As IDBField) As String

            Dim sString As String = ""
            Dim skey As String = field.getConstant()
            Dim meMarker As String = "me"
            Dim differentRuntimType As Boolean = field.RuntimeType IsNot field.OriginalRuntimeType

            If ModelGenerator.Current.dotNetLanguage = ModelGenerator.enumLanguage.CSHARP Then
                meMarker = "this"
            End If

            If field.OriginalRuntimeType Is System.Type.GetType("System.Date") OrElse _
               field.OriginalRuntimeType Is System.Type.GetType("System.DateTime") Then

                sString &= meMarker & ".reader.GetDateTime" & "(DATAREADER_" & skey & ")"

            ElseIf field.OriginalRuntimeType Is System.Type.GetType("System.Int16") Then
                sString &= meMarker & ".reader.GetInt16" & "(DATAREADER_" & skey & ")"

            ElseIf field.OriginalRuntimeType Is System.Type.GetType("System.Int32") Then
                sString &= meMarker & ".reader.GetInt32" & "(DATAREADER_" & skey & ")"

            ElseIf field.OriginalRuntimeType Is System.Type.GetType("System.Int64") Then
                sString &= meMarker & ".reader.GetInt64" & "(DATAREADER_" & skey & ")"
                
            ElseIf field.OriginalRuntimeType Is System.Type.GetType("System.Decimal") Then
                sString &= meMarker & ".reader.GetDecimal" & "(DATAREADER_" & skey & ")"

            ElseIf field.OriginalRuntimeType Is System.Type.GetType("System.Double") Then
                sString &= meMarker & ".reader.GetDouble" & "(DATAREADER_" & skey & ")"

            ElseIf field.OriginalRuntimeType Is System.Type.GetType("System.Single") Then
                sString &= meMarker & ".reader.GetFloat" & "(DATAREADER_" & skey & ")"

            ElseIf field.OriginalRuntimeType Is System.Type.GetType("System.Float") Then
                sString &= meMarker & ".reader.GetFloat" & "(DATAREADER_" & skey & ")"

            ElseIf field.OriginalRuntimeType Is System.Type.GetType("System.String") Then
                sString &= meMarker & ".reader.GetString" & "(DATAREADER_" & skey & ")"

            ElseIf field.OriginalRuntimeType Is System.Type.GetType("System.Boolean") Then
                sString &= meMarker & ".reader.GetBoolean" & "(DATAREADER_" & skey & ")"

            ElseIf field.OriginalRuntimeType Is System.Type.GetType("System.Byte") Then
                sString &= meMarker & ".reader.GetByte" & "(DATAREADER_" & skey & ")"

            ElseIf field.OriginalRuntimeType Is System.Type.GetType("System.Guid") Then
                sString &= meMarker & ".reader.GetGuid" & "(DATAREADER_" & skey & ")"

            ElseIf field.OriginalRuntimeType Is System.Type.GetType("System.Byte[]") Then
                If ModelGenerator.Current.dotNetLanguage = ModelGenerator.enumLanguage.CSHARP Then
                    sString &= "(Byte[])" & meMarker & ".reader.GetValue" & "(DATAREADER_" & skey & ")"
                Else
                    sString &= "Ctype(" & meMarker & ".reader.GetValue" & "(DATAREADER_" & skey & "), Byte())"
                End If

            Else
                Dim msg = field.ParentTable.TableName & "." & field.FieldName & _
                          ":Unhandled DataReader getter for type:" & _
                          field.RuntimeType.ToString
                Debug.WriteLine(msg)
                Throw New ApplicationException(msg)
            End If

            If field.isEnumFromInt Then
                Dim enumName = ModelGenerator.Current.EnumFieldsCollection.getEnumField(field).enumTypeName
                If ModelGenerator.Current.dotNetLanguage = ModelGenerator.enumLanguage.CSHARP Then
                    Return "(" & enumName & "?)" & sString
                Else
                    Return "CType(" & sString & "," & enumName & "?)"
                End If
            ElseIf field.isBooleanFromInt Then

                If ModelGenerator.Current.dotNetLanguage = ModelGenerator.enumLanguage.CSHARP Then
                    Return sString & "!=0"
                Else
                    Return sString & "<>0"
                End If

            ElseIf differentRuntimType AndAlso _
                    (Not String.IsNullOrEmpty(field.getDataReaderConverter())) Then
                sString = field.getDataReaderConverter() & "(" & sString & ")"

            End If

            Return sString

        End Function

    End Class

    Public Class DataReaderConstantsToken
        Inherits MultiLingualReplacementToken

        Sub New()
            Me.StringToReplace = "DATA_READER_CONSTANTS"
        End Sub

        Public Overrides Function getReplacementCodeCSharp(t As IObjectToGenerate) As String
            Dim sb As System.Text.StringBuilder = New System.Text.StringBuilder()
            Dim vec As Dictionary(Of String, IDBField) = t.DbTable.Fields()

            Dim i As Integer = 1
            For Each field As DBField In vec.Values
                If (Not field.isBinaryField) Then
                    sb.Append(vbTab & vbTab & vbTab & "const int DATAREADER_" & field.getConstant() & " = " & (i - 1))
                    i += 1
                    sb.Append(";")
                    sb.Append(vbCrLf)
                End If

            Next

            Return sb.ToString()
        End Function
        Public Overrides Function getReplacementCodeVb(ByVal t As IObjectToGenerate) As String

            Dim sb As System.Text.StringBuilder = New System.Text.StringBuilder()
            Dim vec As Dictionary(Of String, IDBField) = t.DbTable.Fields()

            Dim i As Integer = 1
            For Each field As DBField In vec.Values
                If (Not field.isBinaryField) Then
                    sb.Append(vbTab & vbTab & vbTab & "Const DATAREADER_" & field.getConstant() & " as Integer = " & (i - 1))
                    i += 1
                    sb.Append(vbCrLf)
                End If
            Next

            Return sb.ToString()
        End Function

    End Class

    Public Class PrimaryKeyFieldnameToken
        Inherits ReplacementToken


        Sub New()
            Me.StringToReplace = "PK_FIELD_NAME"
        End Sub

        Public Overrides Function getReplacementCode(t As IObjectToGenerate) As String
            Return t.DbTable.PrimaryKeyFieldName
        End Function

    End Class
End Namespace