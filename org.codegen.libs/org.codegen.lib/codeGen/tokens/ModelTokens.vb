Imports System.Collections.Generic
Imports System.Text
Imports org.codegen.lib.FileComponents

Namespace Tokens
    Public Class IfaceNameToken
        Inherits ReplacementToken
        Sub New()
            Me.StringToReplace = "MODEL_CLASS_IFACE_NAME"
        End Sub
        Public Overrides Function getReplacementCode(ByVal t As IObjectToGenerate) As String

            Dim ifaces As String = t.DbTable.ImplementsAsString
            Return CStr(IIf(String.IsNullOrEmpty(ifaces), String.Empty, "," & ifaces))

        End Function
    End Class

    Public Class ModelObjectParentArrayToken
        Inherits ReplacementToken
        Sub New()
            Me.StringToReplace = "SETUP_PARENTS_ARRAY"
        End Sub
        Public Overrides Function getReplacementCode(ByVal t As IObjectToGenerate) As String
            If ModelGenerator.Current.dotNetLanguage = ModelGenerator.enumLanguage.CSHARP Then
                Return getReplacementCodeCSharp(t)
            Else
                Return getReplacementCodeVB(t)
            End If
        End Function

        Public Function getReplacementCodeCSharp(ByVal t As IObjectToGenerate) As String

            Dim sb As System.Text.StringBuilder = New System.Text.StringBuilder()

            If t.DbTable.Associations().Count() > 0 Then

                Dim vec As List(Of Association) = t.DbTable.Associations()

                For Each association As Association In vec
                    If association.isParent Then
                        sb.Append("if  ( this." & association.getVariableName() & "!=null && this." & association.LoadedFlagVariableName & ") {" & vbCrLf)
                        sb.Append(vbTab).Append("ret.Add(this.").Append(association.PropertyName()).Append(");").Append(vbCrLf)
                        sb.Append("}" & vbCrLf)

                    End If

                Next


            End If

            Return sb.ToString()

        End Function

        Public Function getReplacementCodeVB(ByVal t As IObjectToGenerate) As String

            Dim sb As System.Text.StringBuilder = New System.Text.StringBuilder()

            If t.DbTable.Associations().Count() > 0 Then

                Dim vec As List(Of Association) = t.DbTable.Associations()

                For Each association As Association In vec
                    If association.isParent Then
                        sb.Append("if  Me." & association.getVariableName() & " isNot Nothing AndAlso Me." & association.LoadedFlagVariableName & " Then" & vbCrLf)
                        sb.Append(vbTab).Append("ret.Add(me." & association.PropertyName() & ")" & vbCrLf)
                        sb.Append("End If" & vbCrLf)

                    End If

                Next


            End If

            Return sb.ToString()

        End Function
    End Class

    Public Class OnDeserializedMethodToken
        Inherits MultiLingualReplacementToken
        Sub New()
            Me.StringToReplace = "ON_DESERIALIZED_METHOD"
        End Sub
        Public Overrides Function getReplacementCodeCSharp(t As IObjectToGenerate) As String

            Dim sb As System.Text.StringBuilder = New System.Text.StringBuilder()
            If t.DbTable.ChildrenAssociationCount() > 0 Then
                Dim vec As List(Of Association) = t.DbTable.Associations

                For Each association As Association In vec
                    If (Not association.isParent) Then
                        sb.Append(vbTab).Append(vbTab).Append(vbTab)
                        'this.").Append(association.LoadedFlagVariableName()).Append(" && 
                        sb.Append("if (this.")
                        sb.Append(association.getVariableName()).Append("!=null){").Append(vbCrLf)
                        sb.Append(vbTab).Append(vbTab).Append(vbTab).Append(vbTab)
                        If (association.isCardinalityMany) Then

                            sb.Append("foreach (").Append(association.DataType)
                            sb.Append(" ep in this.").Append(association.getVariableName())
                            sb.Append(") {").Append(vbCrLf)
                            sb.Append(vbTab).Append(vbTab).Append(vbTab).Append(vbTab).Append(vbTab)
                            sb.Append("this.IDChanged += ep.handleParentIdChanged;").Append(vbCrLf)
                            sb.Append(vbTab).Append(vbTab).Append(vbTab).Append(vbTab)
                            sb.Append("}").Append(vbCrLf)
                        Else
                            
                            sb.Append("this.IDChanged += this.").Append(association.getVariableName())
                            sb.Append(".handleParentIdChanged;").Append(vbCrLf)
                        End If
                        sb.Append(vbTab).Append(vbTab).Append(vbTab)
                        sb.Append("}").Append(vbCrLf)
                    End If

                Next
            End If

            Return sb.ToString()

        End Function

        Public Overrides Function getReplacementCodeVB(t As IObjectToGenerate) As String
            Dim sb As System.Text.StringBuilder = New System.Text.StringBuilder()
            If t.DbTable.ChildrenAssociationCount() > 0 Then
                Dim vec As List(Of Association) = t.DbTable.Associations

                For Each association As Association In vec
                    If (Not association.isParent) Then
                        sb.Append(vbTab).Append(vbTab).Append(vbTab)
                        sb.Append("if me.").Append(association.getVariableName()).Append(" isNot Nothing Then").Append(vbCrLf)
                        sb.Append(vbTab).Append(vbTab).Append(vbTab).Append(vbTab)
                        If (association.isCardinalityMany) Then

                            sb.Append("For Each ep as ").Append(association.DataType)
                            sb.Append("  in me.").Append(association.getVariableName())
                            sb.Append(vbCrLf)
                            sb.Append(vbTab).Append(vbTab).Append(vbTab).Append(vbTab).Append(vbTab)
                            sb.Append("AddHandler me.IDChanged, AddressOf ep.handleParentIdChanged").Append(vbCrLf)
                            sb.Append(vbTab).Append(vbTab).Append(vbTab).Append(vbTab)
                            sb.Append("Next '").Append(vbCrLf)
                        Else

                            sb.Append("AddHandler me.IDChanged, AddressOf me.").Append(association.getVariableName())
                            sb.Append(".handleParentIdChanged").Append(vbCrLf)
                        End If
                        sb.Append(vbTab).Append(vbTab).Append(vbTab)
                        sb.Append("End If").Append(vbCrLf)
                    End If

                Next
            End If

            Return sb.ToString()
        End Function
    End Class

    Public Class LoadObjectHierarchyToken
        Inherits ReplacementToken

        Sub New()
            Me.StringToReplace = "loadObjectHierarchy"
        End Sub

        Public Overrides Function getReplacementCode(t As IObjectToGenerate) As String
            Dim sb As System.Text.StringBuilder = New System.Text.StringBuilder()
            Dim endOfLine As String = "()"
            If ModelGenerator.Current.dotNetLanguage = ModelGenerator.enumLanguage.CSHARP Then
                endOfLine = "();"
            End If

            If t.DbTable.Associations().Count() > 0 Then
                Dim vec As List(Of Association) = t.DbTable.Associations()

                For Each association As Association In vec
                    sb.Append(vbTab).Append(vbTab).Append(association.getLoadAssociationMethodName()).Append(endOfLine).Append(vbCrLf)
                Next
            End If

            Return sb.ToString()

        End Function

    End Class
    Public Class ModelObjectChildArrayToken
        Inherits MultiLingualReplacementToken
        Sub New()
            Me.StringToReplace = "SETUP_CHILDREN_ARRAY"
        End Sub


        Public Overrides Function getReplacementCodeCSharp(ByVal t As IObjectToGenerate) As String
            Dim sb As System.Text.StringBuilder = New System.Text.StringBuilder()

            If t.DbTable.Associations().Count() > 0 Then
                Dim vec As List(Of Association) = t.DbTable.Associations()

                For Each association As Association In vec
                    If Not association.isParent Then
                        If association.isCardinalityMany Then
                            Dim dtype As String = association.ChildDatatype
                            'sb.Append(vbTab & "ret.add(me." & association.getVariableName() & ")" & vbCrLf)
                            sb.Append(vbTab & "if  (this." & association.LoadedFlagVariableName() & ") { // check if loaded first!" & vbCrLf)
                            sb.Append(vbTab & vbTab & "List< ModelObject > lp = this." & association.getVariableName() & ".ConvertAll(" & vbCrLf)
                            sb.Append(vbTab & vbTab & vbTab & vbTab & "new Converter< " & dtype & ", ModelObject>((" & vbCrLf)
                            sb.Append(vbTab & vbTab & vbTab & dtype & " pf )=> {")
                            sb.Append(vbTab & vbTab & vbTab & vbTab & "return (ModelObject)pf;}));" & vbCrLf)

                            sb.Append(vbTab & vbTab & "ret.AddRange(lp);" & vbCrLf)
                            sb.Append(vbTab & "}" & vbCrLf)
                        Else
                            sb.Append(vbTab & "if  (this." & association.getVariableName() & "!=null) {" & vbCrLf)
                            sb.Append(vbTab & vbTab & "ret.Add(this." & association.PropertyName() & ");" & vbCrLf)
                            sb.Append(vbTab & "}" & vbCrLf)
                        End If

                    End If

                Next
            End If

            Return sb.ToString()
        End Function
        Public Overrides Function getReplacementCodeVB(ByVal t As IObjectToGenerate) As String
            Dim sb As System.Text.StringBuilder = New System.Text.StringBuilder()

            If t.DbTable.Associations().Count() > 0 Then
                Dim vec As List(Of Association) = t.DbTable.Associations()

                For Each association As Association In vec
                    If Not association.isParent Then
                        If association.isCardinalityMany Then
                            Dim dtype As String = association.ChildDatatype
                            'sb.Append(vbTab & "ret.add(me." & association.getVariableName() & ")" & vbCrLf)
                            sb.Append(vbTab & "if  Me." & association.LoadedFlagVariableName() & " Then 'check if loaded first!" & vbCrLf)
                            sb.Append(vbTab & vbTab & "Dim lp As List(Of ModelObject) = Me." & association.getVariableName() & ".ConvertAll( _" & vbCrLf)
                            sb.Append(vbTab & vbTab & vbTab & vbTab & "New Converter(Of " & dtype & ", ModelObject)(" & vbCrLf)
                            sb.Append(vbTab & vbTab & vbTab & "Function(pf As " & dtype & ")" & vbCrLf)
                            sb.Append(vbTab & vbTab & vbTab & vbTab & "Return DirectCast(pf, ModelObject)" & vbCrLf)
                            sb.Append(vbTab & vbTab & vbTab & "End Function))" & vbCrLf)
                            sb.Append(vbTab & vbTab & "ret.AddRange(lp)" & vbCrLf)
                            sb.Append(vbTab & "End If" & vbCrLf)
                        Else
                            sb.Append(vbTab & "if  Me." & association.getVariableName() & " isNot Nothing then" & vbCrLf)
                            sb.Append(vbTab & vbTab & "ret.Add(me." & association.PropertyName() & ")" & vbCrLf)
                            sb.Append(vbTab & "End If" & vbCrLf)
                        End If

                    End If

                Next
            End If

            Return sb.ToString()
        End Function
    End Class

    Public Class ClassAccessLevelToken
        Inherits ReplacementToken
        Sub New()
            Me.StringToReplace = "CLASS_ACCESS_LEVEL"
        End Sub

        Public Overrides Function getReplacementCode(ByVal t As IObjectToGenerate) As String
            Return "public"
        End Function

    End Class

   
    Public Class RequiredFieldValidatorToken
        Inherits MultiLingualReplacementToken

        Sub New()
            Me.StringToReplace = "REQ_FIELDS_VALIDATOR"
        End Sub
        Public Overrides Function getReplacementCodeCSharp(t As IObjectToGenerate) As String

            Dim sb As System.Text.StringBuilder = New System.Text.StringBuilder()
            Dim vec As Dictionary(Of String, IDBField) = t.DbTable.Fields()
            Dim cnt As Integer = 0

            For i As Integer = 0 To vec.Keys.Count - 1
                'Dim field As DBField = CType(vec(i), DBField)
                Dim field As IDBField = vec.Item(vec.Keys(i))

                If Me.needToCheckField(field) Then

                    If field.isString Then

                        sb.Append("if (string.IsNullOrEmpty( mo.").Append(field.PropertyName).Append(")) {")
                    Else
                        sb.Append("if (mo.").Append(field.PropertyName).Append(" == null ) {")


                    End If
                    sb.Append(vbCrLf)
                    sb.Append(vbTab & vbTab)
                    sb.Append("throw new ModelObjectRequiredFieldException("""). _
                       Append(field.RuntimeFieldName).Append(""");"). _
                       Append(vbCrLf)
                    sb.Append("}").Append(vbCrLf)
                End If

            Next

            Return sb.ToString()
        End Function


        Public Overrides Function getReplacementCodeVb(ByVal t As IObjectToGenerate) As String

            Dim sb As System.Text.StringBuilder = New System.Text.StringBuilder()
            Dim vec As Dictionary(Of String, IDBField) = t.DbTable.Fields()
            Dim cnt As Integer = 0

            For i As Integer = 0 To vec.Keys.Count - 1

                Dim field As IDBField = vec.Item(vec.Keys(i))

                If Me.needToCheckField(field) Then

                    If field.isString Then
                        sb.Append("if String.isNullOrEmpty( mo.").Append(field.PropertyName).Append(") Then")
                    Else
                        sb.Append("if mo.").Append(field.PropertyName).Append(" is Nothing then")
                    End If
                    sb.Append(vbCrLf)
                    sb.Append(vbTab & vbTab)
                    sb.Append("Throw new ModelObjectRequiredFieldException("""). _
                       Append(field.RuntimeFieldName).Append(""")"). _
                       Append(vbCrLf)
                    sb.Append("End if ").Append(vbCrLf)
                End If

            Next

            Return sb.ToString()
        End Function

        Private Function needToCheckField(field As IDBField) As Boolean
            Return field.IsTableField() AndAlso (Not field.isBoolean) AndAlso (Not field.isBinaryField) _
                        AndAlso (Not field.isPrimaryKey) AndAlso field.isDBFieldNullable = False
        End Function


    End Class

    Public Class FieldListToken
        Inherits ReplacementToken

        Sub New()
            Me.StringToReplace = "STRING_FIELD_LIST"
        End Sub

        Public Overrides Function getReplacementCode(ByVal t As IObjectToGenerate) As String

            Dim sb As System.Text.StringBuilder = New System.Text.StringBuilder()
            Dim vec As Dictionary(Of String, IDBField) = t.DbTable.Fields()

            For i As Integer = 0 To vec.Keys.Count - 1
                Dim field As IDBField = CType(vec.Item(vec.Keys(i)), IDBField)
                If Not field.isBinaryField Then
                    If i > 0 Then sb.Append(",")
                    sb.Append(field.getConstantStr())
                End If

            Next

            Return sb.ToString()
        End Function


    End Class

    Public Class ClassFieldStringConstantsToken
        Inherits ReplacementToken

        Sub New()
            Me.StringToReplace = "STRCONSTANTS"
        End Sub

        Public Overrides Function getReplacementCode(ByVal t As IObjectToGenerate) As String

            If ModelGenerator.Current.dotNetLanguage = ModelGenerator.enumLanguage.CSHARP Then
                Return Me.getReplacementCodeCSharp(t)
            Else
                Return Me.getReplacementCodeVB(t)
            End If

        End Function

        Public Function getReplacementCodeCSharp(ByVal t As IObjectToGenerate) As String
            Dim sb As System.Text.StringBuilder = New System.Text.StringBuilder()
            Dim vec As Dictionary(Of String, IDBField) = t.DbTable.Fields()

            For Each field As IDBField In vec.Values
                If Not field.isBinaryField Then
                    sb.Append(vbTab & vbTab & vbTab & "public const String " & field.getConstantStr() & " = """ & DBTable.getRuntimeName(field.FieldName()) & """;")
                    sb.Append(vbCrLf)
                End If

            Next

            Return sb.ToString()
        End Function

        Public Function getReplacementCodeVB(ByVal t As IObjectToGenerate) As String
            Dim sb As System.Text.StringBuilder = New System.Text.StringBuilder()
            Dim vec As Dictionary(Of String, IDBField) = t.DbTable.Fields()

            For Each field As IDBField In vec.Values
                If Not field.isBinaryField Then
                    sb.Append(vbTab & vbTab & vbTab & "public Const " & field.getConstantStr() & " as String = """ & DBTable.getRuntimeName(field.FieldName()) & """")
                    sb.Append(vbCrLf)
                End If

            Next

            Return sb.ToString()
        End Function
    End Class

    Public Class ClassFieldIntConstantsToken
        Inherits ReplacementToken


        Sub New()
            Me.StringToReplace = "CONSTANTS"
        End Sub

        Public Overrides Function getReplacementCode(ByVal t As IObjectToGenerate) As String

            If ModelGenerator.Current.dotNetLanguage = ModelGenerator.enumLanguage.CSHARP Then
                Return Me.getReplacementCodeCSharp(t)
            Else
                Return Me.getReplacementCodeVB(t)
            End If

        End Function
        Public Function getReplacementCodeCSharp(ByVal t As IObjectToGenerate) As String
            Dim sb As System.Text.StringBuilder = New System.Text.StringBuilder()
            Dim vec As Dictionary(Of String, IDBField) = t.DbTable.Fields()

            Dim i As Integer = 0
            For Each field As DBField In vec.Values

                If Not field.isBinaryField Then
                    sb.Append(vbTab & vbTab & "public const int " & field.getConstant() & " = " & (i) & ";")
                    i += 1
                    sb.Append(vbCrLf)
                End If

            Next
            ''keep primary key to be the last field 
            'sb.Append("    public Const " & Me._dbTable.getPrimaryKeyField.getConstant() & " as Integer = " & (i))
            Return sb.ToString()
        End Function
        Public Function getReplacementCodeVB(ByVal t As IObjectToGenerate) As String
            Dim sb As System.Text.StringBuilder = New System.Text.StringBuilder()
            Dim vec As Dictionary(Of String, IDBField) = t.DbTable.Fields()

            Dim i As Integer = 0
            For Each field As DBField In vec.Values

                If Not field.isBinaryField Then
                    sb.Append(vbTab & vbTab & "public Const " & field.getConstant() & " as Integer = " & (i))
                    i += 1
                    sb.Append(vbCrLf)
                End If

            Next
            ''keep primary key to be the last field 
            'sb.Append("    public Const " & Me._dbTable.getPrimaryKeyField.getConstant() & " as Integer = " & (i))
            Return sb.ToString()
        End Function
    End Class

    Public Class ClassPropertiesToken
        Inherits ReplacementToken


        Sub New()
            Me.StringToReplace = "CLASS_PROPERTIES"
        End Sub

      
        Private Function getAssociationProperties(ByVal t As DBTable) As String
            Dim sb As System.Text.StringBuilder = New System.Text.StringBuilder()

            Dim commentSymbol As String = CStr(IIf(ModelGenerator.Current.dotNetLanguage = ModelGenerator.enumLanguage.VB, "'", "//"))
            Dim endregion As String = CStr(IIf(ModelGenerator.Current.dotNetLanguage = ModelGenerator.enumLanguage.VB, "#end region", "#endregion"))
            'Dim startregion As String = CStr(IIf(ModelGenerator.Current.dotNetLanguage = ModelGenerator.enumLanguage.VB, "'", "//"))

            If t.Associations().Count() > 0 Then

                sb.Append(vbTab).Append(vbTab).Append(endregion).Append(vbCrLf)
                sb.Append(vbTab).Append(vbTab).Append("#region ""Associations""").Append(vbCrLf)

                Dim vec As List(Of Association) = t.Associations()

                For Each association As Association In vec
                    sb.Append(association.getCodeFromTemplate())
                Next

            End If


            Return sb.ToString()

        End Function


        Public Overrides Function getReplacementCode(t As IObjectToGenerate) As String
            Dim sb As System.Text.StringBuilder = New System.Text.StringBuilder(vbCrLf)
            Dim vec As Dictionary(Of String, IDBField) = t.DbTable.Fields()

            Dim i As Integer = 0
            For Each field As IDBField In vec.Values
                'no need to generate anything for "id" field.  it is overriden
                If field.FieldName.ToLower.Equals("id") = False AndAlso Not field.isBinaryField Then
                    'field.PropertiesImplementedInterface = Me.GenerateInterface
                    sb.Append(field.getProperty())
                End If

            Next

            sb.Append(Me.getAssociationProperties(CType(t.DbTable, DBTable)))

            Return sb.ToString()
        End Function
    End Class

   
End Namespace
