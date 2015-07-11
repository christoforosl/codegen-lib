Imports Microsoft.VisualBasic
Imports System.Text
Imports org.codegen.lib.FileComponents

Public Class CSharpAssociation
    Inherits Association
   
    Public Overrides ReadOnly Property DataTypeVariable() As String
        Get
            If Me.isCardinalityMany Then
                Return "List< " & Me.DataType & ">"
            Else
                Return Me.DataType
            End If
        End Get

    End Property

    Public Overrides Function getTestCode() As String

        Dim ret As String = "Assert."
        If Me._cardinality.Equals("*") Then
            ret &= "IsTrue(p." & Me.PropertyName & " != null);"

        Else
            ret &= "IsTrue(p." & Me.PropertyName & " != null);"
        End If

        Return ret

    End Function


    Public Overrides Function getVariableDeclarationCode() As String

        Dim sb As System.Text.StringBuilder = New System.Text.StringBuilder() ' TODO type initialisation here
        sb.Append(vbTab & "private ")
        sb.Append(Me.DataTypeVariable())
        sb.Append(" ")
        sb.Append(Me.getVariableName)
        sb.Append(" = null;  //initialize to nothing, for lazy load logic below !!!")
        sb.Append(vbCrLf)
        Return sb.ToString()
    End Function

    Public Overrides Function getDeletedVariable() As String

        If Me.isCardinalityMany Then
            Dim sb As System.Text.StringBuilder = New System.Text.StringBuilder() ' TODO type initialisation here

            sb.Append(vbTab & " private List< " & Me.DataType & ">").Append(" _deleted").Append(Me.associationName)
            sb.Append(" = new ").Append("List< " & Me.DataType & ">();").Append("// initialize to empty list !!!")
            sb.Append(vbCrLf)
            Return sb.ToString()
        Else
            Return String.Empty
        End If

    End Function

    Public Overrides Function getSaveParentCode(ByVal parentMoObjType As String) As String
        Dim ret As String = ""
        If isParent() Then
            ' parent relationship!
            ' need to update self when parent is 
            ' updated. Example: Employer->Address. with link
            ' field address_id on the Employer table.
            '
            ' for example: address is saved first, employer second.

            If Me.IsReadOnly Then
                'in case of readonly association, just append a comment
                ret += vbTab + vbTab & "//***Readonly Parent Association:" & Me.associationName.ToLower() & " ***!" & vbCrLf
            Else
                Dim pfx As String = ModelGenerator.Current.FieldPropertyPrefix
                Dim mapperClassName As String = GetAssociatedMapperClassName()
                Dim mappervar As String = Me.associationName.ToLower() & "Mapper"
                ret += vbTab + vbTab & "//**** Parent Association:" & Me.associationName.ToLower() & vbCrLf
                If Me.isCardinalityMany Then
                    ret += vbTab + vbTab & "if (thisMo." & Me.LoadedFlagVariableName & " && thisMo." & Me.PropertyName() & ".NeedsSave) {" & vbCrLf
                Else
                    ret += vbTab + vbTab & "if ((thisMo." & Me.PropertyName() & "!=null) && (thisMo." & Me.PropertyName() & ".NeedsSave)) {" & vbCrLf
                End If

                ret += vbTab + vbTab + vbTab + mapperClassName & " mappervar = new " & mapperClassName & "(this.dbConn);" & vbCrLf
                ret += vbTab + vbTab + vbTab + "mappervar.save(thisMo." & Me.PropertyName() & ");" & vbCrLf
                ret += vbTab + vbTab & vbTab + "thisMo." & Me.ChildField.PropertyName & " = thisMo." & Me.PropertyName() & "." & Me.ParentField.PropertyName & ";" & vbCrLf
                ret += vbTab + vbTab & "}" & vbCrLf
                ret += vbTab + vbTab + vbCrLf

            End If
        End If
        Return ret

    End Function

    Public Overrides Function getSaveChildrenCode() As String

        Dim ret As String = ""
        If isParent() Then
            ' parent relationship!
            ' need to update self when parent is 
            ' updated. Example: Employer->Address
            ' address is saved first, employer second.

        Else
            If Me.IsReadOnly Then
                ret += vbTab + vbTab & "//* Readonly Child Association:" & Me.associationName.ToLower() & " ***!" & vbCrLf
            Else

                Dim mapperClassName As String = GetAssociatedMapperClassName()
                Dim mappervar As String = Me.associationName.ToLower() & "Mapper"
                ret = vbTab + vbTab & "//*Child Association:" & Me.associationName.ToLower() & vbCrLf
                ret &= vbTab & vbTab & "if (ret." & Me.LoadedFlagVariableName & ") { //scc " & vbCrLf
                ret &= vbTab & vbTab & vbTab & mapperClassName & " " & mappervar & _
                                " = new " & mapperClassName & "(this.dbConn);" & vbCrLf

                If Me.isCardinalityMany Then
                    ret &= vbTab & vbTab & vbTab & mappervar & ".saveList(ret." & Me.PropertyName() & ");" & vbCrLf
                    ret &= vbTab & vbTab & vbTab & mappervar & ".deleteList(ret." & Me.PropertyName() & "GetDeleted());" & vbCrLf
                Else
                    ret &= vbTab & vbTab & vbTab & mappervar & ".save(ret." & Me.PropertyName() & ");" & vbCrLf
                End If
                ret &= vbTab & vbTab & "}"
                ret &= vbCrLf

            End If

        End If

        Return ret

    End Function








End Class
