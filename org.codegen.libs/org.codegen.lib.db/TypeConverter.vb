Imports System
Imports System.Collections
Imports System.Data

''' <summary>
''' Convert a base data type to another base data type
''' </summary>
Public NotInheritable Class TypeConvertor

    Private Structure DbTypeMapEntry

        Public Type As Type
        Public DbType As DbType
        Public SqlDbType As SqlDbType
        Public Sub New(ByVal type As Type, ByVal dbType As DbType, ByVal sqlDbType As SqlDbType)

            Me.Type = type
            Me.DbType = dbType

            Me.SqlDbType = sqlDbType
        End Sub

    End Structure

    Private Shared _DbTypeList As New ArrayList()


#Region "Constructors"

    Shared Sub New()

        Dim dbTypeMapEntry As New DbTypeMapEntry(GetType(Boolean), DbType.[Boolean], SqlDbType.Bit)
        _DbTypeList.Add(dbTypeMapEntry)


        dbTypeMapEntry = New DbTypeMapEntry(GetType(Byte), DbType.Int16, SqlDbType.TinyInt)
        _DbTypeList.Add(dbTypeMapEntry)

        dbTypeMapEntry = New DbTypeMapEntry(GetType(Single), DbType.Single, SqlDbType.Decimal)
        _DbTypeList.Add(dbTypeMapEntry)


        dbTypeMapEntry = New DbTypeMapEntry(GetType(Byte()), DbType.Binary, SqlDbType.Image)
        _DbTypeList.Add(dbTypeMapEntry)


        dbTypeMapEntry = New DbTypeMapEntry(GetType(DateTime), DbType.DateTime, SqlDbType.DateTime)
        _DbTypeList.Add(dbTypeMapEntry)


        dbTypeMapEntry = New DbTypeMapEntry(GetType([Decimal]), DbType.[Decimal], SqlDbType.[Decimal])
        _DbTypeList.Add(dbTypeMapEntry)


        dbTypeMapEntry = New DbTypeMapEntry(GetType(Double), DbType.[Double], SqlDbType.Float)
        _DbTypeList.Add(dbTypeMapEntry)

        dbTypeMapEntry = New DbTypeMapEntry(GetType(Guid), DbType.Guid, SqlDbType.UniqueIdentifier)
        _DbTypeList.Add(dbTypeMapEntry)


        dbTypeMapEntry = New DbTypeMapEntry(GetType(Int16), DbType.Int16, SqlDbType.SmallInt)
        _DbTypeList.Add(dbTypeMapEntry)


        dbTypeMapEntry = New DbTypeMapEntry(GetType(Int32), DbType.Int32, SqlDbType.Int)
        _DbTypeList.Add(dbTypeMapEntry)


        dbTypeMapEntry = New DbTypeMapEntry(GetType(Int64), DbType.Int64, SqlDbType.BigInt)
        _DbTypeList.Add(dbTypeMapEntry)


        dbTypeMapEntry = New DbTypeMapEntry(GetType(Object), DbType.[Object], SqlDbType.[Variant])
        _DbTypeList.Add(dbTypeMapEntry)


        dbTypeMapEntry = New DbTypeMapEntry(GetType(String), DbType.[String], SqlDbType.NVarChar)


        _DbTypeList.Add(dbTypeMapEntry)
    End Sub


    Private Sub New()


    End Sub

#End Region


#Region "Methods"

    ''' <summary>
    ''' Convert db type to .Net data type
    ''' </summary>
    ''' <param name="dbType"></param>
    ''' <returns></returns>
    Public Shared Function ToNetType(ByVal dbType As DbType) As Type

        Dim entry As DbTypeMapEntry = Find(dbType)

        Return entry.Type
    End Function


    ''' <summary>
    ''' Convert TSQL type to .Net data type
    ''' </summary>
    ''' <param name="sqlDbType"></param>
    ''' <returns></returns>
    Public Shared Function ToNetType(ByVal sqlDbType As SqlDbType) As Type

        Dim entry As DbTypeMapEntry = Find(sqlDbType)

        Return entry.Type
    End Function

    ''' <summary>
    ''' Convert .Net type to Db type
    ''' </summary>
    ''' <param name="type"></param>
    ''' <returns></returns>
    Public Shared Function ToDbType(ByVal type As Type) As DbType
        Dim entry As DbTypeMapEntry = Find(type)
        Return entry.DbType
    End Function

    ''' <summary>
    ''' Convert TSQL data type to DbType
    ''' </summary>
    ''' <param name="sqlDbType"></param>
    ''' <returns></returns>
    Public Shared Function ToDbType(ByVal sqlDbType As SqlDbType) As DbType

        Dim entry As DbTypeMapEntry = Find(sqlDbType)
        Return entry.DbType

    End Function


    ''' <summary>
    ''' Convert .Net type to TSQL data type
    ''' </summary>
    ''' <param name="type"></param>
    ''' <returns></returns>
    Public Shared Function ToSqlDbType(ByVal type As Type) As SqlDbType

        Dim entry As DbTypeMapEntry = Find(type)

        Return entry.SqlDbType
    End Function


    ''' <summary>
    ''' Convert DbType type to TSQL data type
    ''' </summary>
    ''' <param name="dbType"></param>
    ''' <returns></returns>
    Public Shared Function ToSqlDbType(ByVal dbType As DbType) As SqlDbType

        Dim entry As DbTypeMapEntry = Find(dbType)

        Return entry.SqlDbType
    End Function


    Private Shared Function Find(ByVal type As Type) As DbTypeMapEntry

        Dim retObj As Object = Nothing
        For i As Integer = 0 To _DbTypeList.Count - 1

            Dim entry As DbTypeMapEntry = DirectCast(_DbTypeList(i), DbTypeMapEntry)
            If entry.Type Is type Then

                retObj = entry

                Exit For
            End If
        Next
        If retObj Is Nothing Then


            Throw New ApplicationException("Referenced an unsupported Type")
        End If


        Return DirectCast(retObj, DbTypeMapEntry)
    End Function

    Private Shared Function Find(ByVal dbType As DbType) As DbTypeMapEntry

        Dim retObj As Object = Nothing
        For i As Integer = 0 To _DbTypeList.Count - 1

            Dim entry As DbTypeMapEntry = DirectCast(_DbTypeList(i), DbTypeMapEntry)
            If entry.DbType = dbType Then

                retObj = entry

                Exit For
            End If
        Next
        If retObj Is Nothing Then


            Throw New ApplicationException("Referenced an unsupported DbType")
        End If


        Return DirectCast(retObj, DbTypeMapEntry)
    End Function
    Private Shared Function Find(ByVal sqlDbType As SqlDbType) As DbTypeMapEntry

        Dim retObj As Object = Nothing
        For i As Integer = 0 To _DbTypeList.Count - 1

            Dim entry As DbTypeMapEntry = DirectCast(_DbTypeList(i), DbTypeMapEntry)
            If entry.SqlDbType = sqlDbType Then

                retObj = entry

                Exit For
            End If
        Next
        If retObj Is Nothing Then


            Throw New ApplicationException("Referenced an unsupported SqlDbType")
        End If


        Return DirectCast(retObj, DbTypeMapEntry)
    End Function

#End Region
End Class
