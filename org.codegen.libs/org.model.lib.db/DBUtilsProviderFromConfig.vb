Imports org.model.lib.db.DBUtils
Imports org.model.lib.db.Encryption


''' <summary>
''' Returns a DBUtils instance from the connection string stored in the configuration file.
''' <seealso cref="DBConfig"></seealso>
''' </summary>
''' <remarks></remarks>
Public Class DBUtilsProviderFromConfig
    Implements IDBUtilsProvider

    ''' <summary>
    ''' Returns a DBUtils instance from the connection string stored in the configuration file.
    ''' <seealso cref="DBConfig"></seealso>
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getDBUtils() As DBUtils Implements IDBUtilsProvider.getDBUtils

        Dim dbConfigSect As DBConfig = _
                    TryCast(System.Configuration.ConfigurationManager.GetSection("DBConfig"),  _
                    DBConfig)

        Validate.isNotNull(dbConfigSect, "DBConfig is null, is there a section is app.config?")

        Dim connstr As String = dbConfigSect.dbconnstring

        If CBool(dbConfigSect.dbConnStringEncrypted) Then
            connstr = SimpleEncrypt.Decipher(connstr)
        End If


        Return DBUtils.getFromConnString(connstr, _
                                         CType(dbConfigSect.sqlConnectionType, enumConnType), _
                                         CType(dbConfigSect.sqlDialect, enumSqlDialect))

    End Function
End Class