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
        'sJcode = sJcode.Replace("<NAMESPACE>", Me.ClassNameSpace())
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