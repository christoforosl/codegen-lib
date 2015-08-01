Imports org.model.lib.db.DBUtils

''' <summary>
''' Returns a DBUtils instance from the connection string stored in the registry.
''' <seealso cref="DBConfig"></seealso>
''' </summary>
''' <remarks></remarks>
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

        'Trace.TraceInformation("Get DBUtilsBase From registry with connstring:" & connString)

        Return DBUtils.getFromConnString(connString, sqlConnType, iDialect, dbConfigSect.logFile)

    End Function

End Class
