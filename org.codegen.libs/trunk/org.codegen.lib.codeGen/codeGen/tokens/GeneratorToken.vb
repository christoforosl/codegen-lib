Imports System.Collections.Generic
Imports org.codegen.lib.codeGen.FileComponents

Namespace Tokens

    Public Class PKFieldTableNameToken
        Inherits ReplacementToken

        Sub New()
            Me.StringToReplace = "PK_TABKE_FIELD_NAME"
        End Sub

        Public Overrides Function getReplacementCode(ByVal og As IObjectToGenerate) As String
            Return og.DbTable.getPrimaryKeyName()
        End Function
    End Class

    Public Class PKConverter
        Inherits MultiLingualReplacementToken

        Sub New()
            Me.StringToReplace = "PK_CONVERTER"
        End Sub

        Public Overrides Function getReplacementCodeCSharp(t As dotnet.IObjectToGenerate) As String
            If t.DbTable.getPrimaryKeyField.isInteger Then
                Return "Convert.ToInt64(value)"
            ElseIf t.DbTable.getPrimaryKeyField.isString Then
                Return ""
            Else
                Throw New ApplicationException("Primary Key can be Integer or String")
            End If
        End Function

        Public Overrides Function getReplacementCodeVB(t As dotnet.IObjectToGenerate) As String
            If t.DbTable.getPrimaryKeyField.isInteger Then
                Return "Clng(value)"
            ElseIf t.DbTable.getPrimaryKeyField.isString Then
                Return "CStr(value)"
            Else
                Throw New ApplicationException("Primary Key can be Integer or String")
            End If
        End Function
    End Class



    Public Class PKFieldRuntimeNameToken
        Inherits ReplacementToken

        Sub New()
            Me.StringToReplace = "PK_MO_FIELD_NAME"
        End Sub

        Public Overrides Function getReplacementCode(ByVal og As IObjectToGenerate) As String
            Return DBTable.getRuntimeName(og.DbTable.getPrimaryKeyName())
        End Function
    End Class

    Public Class ClassNameSpaceToken
        Inherits ReplacementToken

        Sub New()
            Me.StringToReplace = "NAMESPACE"
        End Sub

        Public Overrides Function getReplacementCode(ByVal og As IObjectToGenerate) As String
            Return CType(og, ObjectToGenerate).ClassNameSpace
        End Function
    End Class

    Public Class DBMapperClassNameSpaceToken
        Inherits ReplacementToken

        Sub New()
            Me.StringToReplace = "DBMAPPER_NAMESPACE"
        End Sub

        Public Overrides Function getReplacementCode(ByVal og As IObjectToGenerate) As String

            Return CType(og, ObjectToGenerate).MapperClassNameSpace

        End Function
    End Class

    Public Class TableNameToken
        Inherits ReplacementToken

        Sub New()
            Me.StringToReplace = "TABLE_NAME"
        End Sub

        Public Overrides Function getReplacementCode(ByVal og As IObjectToGenerate) As String
            Return og.DbTable.TableName
        End Function
    End Class

    Public Class ModelObjectClassNameToken
        Inherits ReplacementToken

        Sub New()
            Me.StringToReplace = "MODEL_CLASS_NAME"
        End Sub

        Public Overrides Function getReplacementCode(ByVal og As IObjectToGenerate) As String
            Return CType(og, ObjectToGenerate).ClassName
        End Function
    End Class

    Public Class TableRuntimeNameToken
        Inherits ReplacementToken

        Sub New()
            Me.StringToReplace = "TABLE_RUNTIME_NAME"
        End Sub

        Public Overrides Function getReplacementCode(ByVal og As IObjectToGenerate) As String
            Return DBTable.getRuntimeName(og.DbTable.TableName)
        End Function
    End Class

    Public Class ModelExtraCodeToken
        Inherits ReplacementToken

        Sub New()
            Me.StringToReplace = "MODEL_EXTRA_CODE"
        End Sub

        Public Overrides Function getReplacementCode(ByVal og As IObjectToGenerate) As String

            Return XMLClassGenerator.getRowValue(og.XMLDefinition, _
                                            XMLClassGenerator.XML_ATTR_MODEL_EXTRA_CODE, _
                                            String.Empty)
        End Function
    End Class

    Public Class GeneratorToken
        Inherits ReplacementToken

        Sub New()
            Me.StringToReplace = "GENERATOR"
        End Sub

        Public Overrides Function getReplacementCode(ByVal t As IObjectToGenerate) As String
            Return ModelGenerator.Current.GetType().Name
        End Function

    End Class

    Public Class CurentDateToken
        Inherits ReplacementToken

        Sub New()
            Me.StringToReplace = "CURDATE"
        End Sub

        Public Overrides Function getReplacementCode(ByVal t As IObjectToGenerate) As String
            Return CStr(Now)
        End Function

    End Class

End Namespace