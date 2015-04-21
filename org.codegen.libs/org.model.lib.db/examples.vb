''' <summary>
''' <exclude />
''' </summary>
''' <remarks></remarks>
Public Module examples



#Region "SValueExample"

    Private Function SValueExample() As String

        Dim dbconn As DBUtils = DBUtils.Current
        Dim sValue As String = dbconn.getSValue("select test from testTable where testid=1")
        Return sValue

    End Function

#End Region

#Region "getSValueWithParams"

    Private Function getSValueWithParams() As String

        Dim dbconn As DBUtils = DBUtils.Current

        'In the example below, the parameters passed to the sql are 12 and string value "xwy"
        Dim sql As String = "select test from testTable where testid={0} and testName={1}"
        Dim sValue As String = dbconn.getSValueWithParams(sql, 12, "xwy")
        Return sValue

    End Function

#End Region

#Region "DtValueExample"

    Private Function DtValueExample() As Date

        Dim dbconn As DBUtils = DBUtils.Current
        Const sql As String = "select testDATE from testTable where testid=1"
        Dim dValue As Date = dbconn.getDtValue(sql)
        Return dValue

    End Function

#End Region

#Region "DataReader with parameters and NullChecker Examples"

    Private Sub DataReadeWithParamsExample()

        Dim dbconn As DBUtils = DBUtils.Current

        Dim ir As IDataReader = Nothing
        Dim dtVal As Date
        Dim strVal As String
        Dim intVal As Integer

        Try

            Dim sql As String = "select dateField, stringField, intField from TestTable where someValue={0} order by 1"
            ir = dbconn.getDataReaderWithParams(sql, 12)

            While ir.Read

                'get a date value into variable dt.  if ir.GetValue(0) is DBNull, 
                'NullChecker.dateNull wil return a NULL_DATE value 
                dtVal = NullChecker.dateNull(ir.GetValue(0))

                'get a string value into variable strVal.  if ir.GetValue(0) is DBNull, 
                'NullChecker.strNull wil return String.Empty
                strVal = NullChecker.strNull(ir.GetValue(1))

                'get an Integer value into variable intVal.  if ir.GetValue(0) is DBNull, 
                'NullChecker.intNull wil return 0
                intVal = NullChecker.intNull(ir.GetValue(1))


            End While

        Finally
            'IMPORTANT: always close data readers
            dbconn.closeDataReader(ir)
        End Try
    End Sub

#End Region

#Region "DataReader and NullChecker Examples"
    Private Sub DataReadeExample()

        Dim dbconn As DBUtils = DBUtils.Current

        Dim ir As IDataReader = Nothing
        Dim dtVal As Date
        Dim strVal As String
        Dim intVal As Integer

        Try

            Dim sql As String = "select dateField, stringField, intField from TestTable where someValue = 1 order by 1"
            ir = dbconn.getDataReader(sql)

            While ir.Read

                'get a date value into variable dt.  if ir.GetValue(0) is DBNull, 
                'NullChecker.dateNull wil return a NULL_DATE value 
                dtVal = NullChecker.dateNull(ir.GetValue(0))

                'get a string value into variable strVal.  if ir.GetValue(0) is DBNull, 
                'NullChecker.strNull wil return String.Empty
                strVal = NullChecker.strNull(ir.GetValue(1))

                'get an Integer value into variable intVal.  if ir.GetValue(0) is DBNull, 
                'NullChecker.intNull wil return 0
                intVal = NullChecker.intNull(ir.GetValue(1))


            End While

        Finally
            'IMPORTANT: always close data readers
            dbconn.closeDataReader(ir)
        End Try
    End Sub

#End Region

#Region "DataTableExample"

    Private Function DataTableExample() As DataTable

        Dim dbconn As DBUtils = DBUtils.Current
        Dim dtValue As DataTable = dbconn.getDataTable("select testDATE from testTable")
        Return dtValue

    End Function

#End Region

End Module
