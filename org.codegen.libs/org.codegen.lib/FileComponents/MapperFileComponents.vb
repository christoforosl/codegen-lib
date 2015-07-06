Imports System.Collections.Generic
Imports System.Reflection
Imports System.IO
Imports System.Xml

Imports org.codegen.lib.Tokens

Namespace FileComponents

    <ReplacementTokenAttribute(GetType(ClassAccessLevelToken), _
                    GetType(GeneratorToken), _
                    GetType(DBMapperClassNameSpaceToken), _
                    GetType(ModelObjectClassNameToken), _
                    GetType(TableNameToken), _
                    GetType(ClassNameSpaceToken), _
                    GetType(PKFieldRuntimeNameToken), _
                    GetType(DataReaderConstantsToken), _
                    GetType(LoadFromRSToken), _
                    GetType(PrimaryKeyFieldnameToken), GetType(SelectNameToken), _
                    GetType(DBMapperBaseClassToken), _
                    GetType(FillStatementToken), _
                    GetType(LoadFromDataRowToken), _
                    GetType(PrimaryKeyAutogenAttr), _
                    GetType(SaveChildrenCodeToken), _
                    GetType(SaveParentCodeToken), _
                    GetType(CurentDateToken))> _
    Public Class MapperBaseFileComponent
        Inherits ModelObjectFileComponent

        Public Const STR_DBMAPPER As String = "DBMapper"

        Public Sub New(ByVal inobjGen As IObjectToGenerate)
            MyBase.New(inobjGen)
        End Sub

        Public Overrides Function ClassName() As String
            Return MyBase.ClassName & MapperBaseFileComponent.STR_DBMAPPER
        End Function

        Public Overrides Function ClassNamespace() As String

            Dim thisRow As DataRow = Me.objectToGenerate.XMLDefinition
            Return XMLClassGenerator.getRowValue(thisRow, _
                   XMLClassGenerator.XML_TABLE_ATTR_DBMAPPER_NAMESPACE, _
                   ModelGenerator.Current.DefaultMapperNameSpace())

        End Function




        Public Overloads Shared Function KEY() As String
            Return "Mapper"
        End Function


    End Class

    Public Class SingletonMapperBaseToGenerate
        Inherits MapperBaseFileComponent

        Sub New(ByVal inobj As ObjectToGenerate)

            MyBase.New(inobj)
            Me.templateFileName = "DBMapperBaseSingleton.txt"

        End Sub

    End Class

End Namespace
