Imports org.model.lib.db.DBUtils

Public Interface IDBUtilsProvider

    Function getDBUtils() As DBUtils

End Interface


Public Class DBUtilsProviderFromRegistry
    Implements IDBUtilsProvider

    Public Function getDBUtils() As DBUtils Implements IDBUtilsProvider.getDBUtils

        Dim dbConfigSect As DBConfigRegistry = _
                   TryCast(System.Configuration.ConfigurationManager.GetSection("DBConfigRegistry"),  _
                   DBConfigRegistry)

        Dim connString As String = GetSetting(dbConfigSect.dbRegAppname, dbConfigSect.dbRegSection, _
                                              dbConfigSect.dbConnectionStringRegKey, "")
        Dim sqlConnType As enumConnType = CType(GetSetting(dbConfigSect.dbRegAppname, dbConfigSect.dbRegSection, _
                                              dbConfigSect.dbRegKeySqlConnectionType, "1"), enumConnType)
        Dim iDialect As enumSqlDialect = CType(GetSetting(dbConfigSect.dbRegAppname, dbConfigSect.dbRegSection, _
                                              dbConfigSect.dbRegKeyDialect, "1"), enumSqlDialect)

        logStatement("Get DBUtilsBase From registry with connstring:" & connString)

        Return DBUtils.getFromConnString(connString, sqlConnType, iDialect, dbConfigSect.logFile)

    End Function
End Class

Public Class DBUtilsProviderFromConfig
    Implements IDBUtilsProvider

    ''' <summary>
    ''' Returns a DBUtils instance from the connection string stored in the configuration file.
    ''' <seealso cref="DBConfig"></seealso>
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getDBUtils() As DBUtils Implements IDBUtilsProvider.getDBUtils

        Dim ret As DBUtils
        Dim dbConfigSect As DBConfig = _
                    TryCast(System.Configuration.ConfigurationManager.GetSection("DBConfig"),  _
                    DBConfig)

        If dbConfigSect IsNot Nothing Then
            Select Case CType(dbConfigSect.sqlConnectionType, enumConnType)

                Case enumConnType.CONN_MSSQL
                    ret = New MSSQLUtils()
                    ret.sqldialect = enumSqlDialect.MSSQL

                Case enumConnType.CONN_OLEDB
                    ret = New OLEDBUtils()
                    ret.sqldialect = CType(dbConfigSect.sqlDialect, enumSqlDialect)

            End Select

            Dim connstr As String = dbConfigSect.dbconnstring

            If CBool(dbConfigSect.dbConnStringEncrypted) Then
                ret.ConnString = SimpleEncrypt.Decipher(connstr)

            Else
                ret.ConnString = connstr

            End If
        End If

        Return ret

    End Function
End Class