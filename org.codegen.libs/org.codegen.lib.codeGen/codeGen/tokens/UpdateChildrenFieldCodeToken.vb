Imports System.Collections.Generic

Namespace Tokens
    Public Class UpdateChildrenFieldCodeToken
        Inherits ReplacementToken

        Sub New()
            Me.StringToReplace = "UPDATE_CHILDREN_LINK_FIELD"
        End Sub

        Public Overrides Function getReplacementCode(ByVal t As IObjectToGenerate) As String

            'this code is executed if this class is acting as a child of another object
            'the updateAssociationEndsIds contain keys that are in the form of:
            ' <PARENT_OBJECT>.<ThisClassName>
            'so we loop thru the keys and if the key ends in .<ThisClassName>, 
            'we insert code for the parentIdChanged routine for each parent

            Const TWO_TABS As String = vbTab & vbTab
            Dim sAssociationsCode As System.Text.StringBuilder = New System.Text.StringBuilder()
            Dim sClassKey As String = CType(t, ObjectToGenerate).FullyQualifiedClassName

            For Each association As IAssociation In ModelGenerator.Current.SystemAssociations

                Dim dtype As String = association.ChildDatatype()
                If (dtype.Equals(sClassKey)) Then

                    Dim parentMoObjType As String = association.ParentDatatype()
                    Dim meField As String = DBTable.getRuntimeName(association.ChildFieldName)
                    Dim relField As String = DBTable.getRuntimeName(association.ParentFieldName)

                    sAssociationsCode.Append(TWO_TABS & "' Assocations from " & parentMoObjType & vbCrLf)
                    sAssociationsCode.Append(TWO_TABS & "if (typeof parentMo is " & parentMoObjType & ") Then" & vbCrLf)
                    sAssociationsCode.Append(TWO_TABS & vbTab & "me." & meField & _
                                             "= DirectCast(parentMo, " & parentMoObjType & ")." & relField & vbCrLf)
                    sAssociationsCode.Append(TWO_TABS & "End If" & vbCrLf)

                End If

            Next

            If sAssociationsCode.Length > 0 Then

                Dim s As System.Text.StringBuilder = New System.Text.StringBuilder()
                s.Append("#Region ""parentIdChanged""" & vbCrLf)
                s.Append(vbTab & "'below sub is called when parentIdChanged" & vbCrLf)
                s.Append(vbTab & "public Overrides Sub handleParentIdChanged(parentMo as IModelObject)" & vbCrLf)
                s.Append(sAssociationsCode.ToString)
                s.Append(vbTab & "End Sub" & vbCrLf)
                s.Append("#End Region" & vbCrLf)

                Return s.ToString()
            Else
                Return String.Empty
            End If
        End Function
    End Class
End Namespace
