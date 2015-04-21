Imports System.Runtime.InteropServices

Namespace Model

    ''' <summary>
    ''' Represents a Model Object with a From-To effective date fields
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface IHistorical

        Property FromDate() As Date?
        Property ToDate() As Date?

    End Interface

    Public Class HistoricalAscendingComparer : Implements IComparer

        Public Function Compare1(ByVal x As Object, ByVal y As Object) As _
                    Integer Implements IComparer.Compare

            Return DirectCast(x, IHistorical).FromDate.GetValueOrDefault.CompareTo(DirectCast(y, IHistorical).FromDate.GetValueOrDefault)

        End Function

    End Class

    Public Class HistoricalDescendingComparer : Implements IComparer

        Public Function Compare1(ByVal x As Object, ByVal y As Object) As _
                    Integer Implements IComparer.Compare
            Return -1 * DirectCast(x, IHistorical).FromDate.GetValueOrDefault.CompareTo(DirectCast(y, IHistorical).FromDate.GetValueOrDefault)

        End Function
    End Class


End Namespace