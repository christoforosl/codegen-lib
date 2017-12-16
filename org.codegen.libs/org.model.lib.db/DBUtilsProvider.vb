

Imports org.model.lib.db.DBUtils

Public Interface IDBUtilsProvider

    Function getDBUtils() As DBUtils

End Interface

''' <summary>
''' Sets the connection string from an arbitrary string
''' </summary>
Public Class ConnectionStringDBProvider
    Implements IDBUtilsProvider

    Public Sub New(connstr As String, sqlConnectionType As enumConnType, sqlDialect As enumSqlDialect)

        Me.connstr = connstr
        Me.sqlConnectionType = sqlConnectionType
        Me.sqlDialect = sqlDialect

    End Sub


    Public Property connstr As String
    Public Property sqlConnectionType As enumConnType
    Public Property sqlDialect As enumSqlDialect

    ''' <summary>
    ''' Returns a DBUtils instance from the connection string stored in the configuration file.
    ''' <seealso cref="DBConfig"></seealso>
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getDBUtils() As DBUtils Implements IDBUtilsProvider.getDBUtils

        Return DBUtils.getFromConnString(connstr, sqlConnectionType, sqlDialect)

    End Function
End Class

