
Imports System.Collections.Generic
Imports System.Linq

Public MustInherit Class IPropertyGenerator

    Protected Const TWO_TABS = vbTab & vbTab
    Protected Const THREE_TABS = TWO_TABS & vbTab
    Protected Const FOUR_TABS = TWO_TABS & TWO_TABS
    Protected Const FIVE_TABS = THREE_TABS & TWO_TABS
    Protected Const SIX_TABS = THREE_TABS & THREE_TABS

    MustOverride Function generatePropertyCode(ByVal field As IDBField) As String
    MustOverride Function generateInterfaceDeclaration(ByVal field As IDBField) As String

    Public Function getParentAssociationOfField(ByVal field As IDBField) As Association

        Dim parents As IEnumerable(Of Association) = field.ParentTable.Associations.Where(Function(x)
                                                                                              Return x.isParent
                                                                                          End Function)

        For Each association As Association In parents

            Dim dtype As String = association.ChildFieldName()
            If (dtype.Equals(field.RuntimeFieldName)) Then
                Return association
            End If

        Next
        Return Nothing

    End Function

End Class

