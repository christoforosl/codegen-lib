
Imports System.Linq
Imports System.Data.Linq

Namespace Model


    Public Class PagedModelObjectList(Of T As IModelObject)

        Private pwhereClause As String
        Private params As Object()
        Private skipRecords As Integer
        Private numRecordsPerPage As Integer
        Private pOrderby As String

        Public Function WhereClause(where As String) As PagedModelObjectList(Of T)

            If where.ToLower.IndexOf("order by") > -1 Then
                Throw New ApplicationException("[order by] in the where clause is not supported!")
            End If

            Me.pwhereClause = where
            Return Me
        End Function

        Public Function Parameters(ParamArray inparams As Object()) As PagedModelObjectList(Of T)
            Me.params = inparams
            Return Me
        End Function

        Public Function CurrentPageNo(incurrentPageNo As Integer) As PagedModelObjectList(Of T)
            Return Me.Skip(incurrentPageNo * Me.numRecordsPerPage)
        End Function

        Public Function NumberRecords(numRecs As Integer) As PagedModelObjectList(Of T)
            Return Me.Take(numRecs)
        End Function

        Public Function Take(numRecs As Integer) As PagedModelObjectList(Of T)
            Me.numRecordsPerPage = numRecs
            Return Me
        End Function

        Public Function Skip(recNumber As Integer) As PagedModelObjectList(Of T)
            Me.skipRecords = recNumber
            Return Me
        End Function

        Public Function OrderBy(sorderBy As String) As PagedModelObjectList(Of T)
            Me.pOrderby = sorderBy
            Return Me
        End Function

        Public Iterator Function getList() As IEnumerable(Of T)

            Using cts As DataContext = DBUtils.Current.dbContext

                return cts.

            End Using

            Dim ilen As Integer
            If (Me.params Is Nothing) Then
                ilen = 0
            Else
                ilen = Me.params.Length
            End If

            Dim newParams(ilen + 3) As Object
            If (Me.params IsNot Nothing) Then
                params.CopyTo(newParams, 0)
            End If

            newParams(ilen) = IIf(String.IsNullOrEmpty(Me.pOrderby), "1", Me.pOrderby)
            newParams(ilen + 1) = Me.skipRecords
            newParams(ilen + 2) = Me.numRecordsPerPage

            Dim idbM As DBMapper = ModelContext.GetModelDefaultMapper(GetType(T))

            Dim retLst As IEnumerable(Of IModelObject) = ModelContext.GetModelDefaultMapper(GetType(T)).findList( _
                Me.pwhereClause & "  order by ? OFFSET ? ROWS FETCH NEXT ? ROWS ONLY", _
                newParams)

            For Each mo As IModelObject In retLst
                Yield CType(mo, T)
            Next

        End Function


    End Class

End Namespace
