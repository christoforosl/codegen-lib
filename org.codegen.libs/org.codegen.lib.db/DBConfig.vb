Imports System.Configuration
''' <summary>
''' Configuration Reader Class for DBUtils.
''' This class loads the following configuration settings from app.config or web.config
''' file: dbconnstring, logFile, sqlDialect,sqlConnectionType,dbconnstringEncrypted
''' <example>
''' <ol>
''' <li>Define a config section in Config Sections as follows:<br/>
''' &lt;configSections&gt;<br/>
''' ...<br/>
''' &lt;section name="DBConfig" type="org.codegen.lib.db.DBConfig,org.codegen.lib.db"/&gt;<br/>
''' &lt;/configSections&gt;<br/>
''' </li>
''' <li>
''' &lt;DBConfig <br/>
'''          dbconnstring="User ID=payroll;password=apoel123;Initial Catalog=prl_dcom;Data Source=sparta" <br/>
'''          dbconnstringEncrypted="0" <br/>
'''          sqlDialect="0" <br/>
'''          sqlConnectionType="0"&gt;<br/></li>
'''</ol>
''' </example>
''' </summary>
''' <remarks></remarks>
Public Class DBConfig
    Inherits System.Configuration.ConfigurationSection
    ''' <summary>
    ''' Sets the ADO.NET connection string.  This can be encypted.
    ''' <see cref="DBConfig.dbConnStringEncrypted">DBConfig.dbConnStringEncrypted</see>
    ''' </summary>
    ''' <value></value>
    ''' <returns>Complete Connection String to connect to Database</returns>
    ''' <remarks>Required Setting</remarks>
    <ConfigurationProperty("dbconnstring", _
                           IsRequired:=True)> _
    Public Property dbconnstring() As String
        Get
            Return CStr(Me("dbconnstring"))
        End Get
        Set(ByVal value As String)
            Me("dbconnstring") = value
        End Set
    End Property
    ''' <summary>
    ''' DBUtils can log each sql statement executed along with the time taken to execute the statement.
    ''' </summary>
    ''' <value></value>
    ''' <returns>String of file used to log SQL Stetaments executed by DBUtils</returns>
    ''' <remarks>Not Required Setting</remarks>
    <ConfigurationProperty("logFile", _
                           IsRequired:=False)> _
        Public Property logFile() As String
        Get
            Return CStr(Me("logFile"))
        End Get
        Set(ByVal value As String)
            Me("logFile") = value
        End Set
    End Property
    ''' <summary>
    ''' Sets the sql flavor (syntax) used by DBUtils.
    ''' </summary>
    ''' <value></value>
    ''' <returns>
    '''    0 = MSSQL
    '''    1 = JET/MS Access
    '''    2 = ORACLE
    '''    3 = MYSQL
    ''' </returns>
    ''' <remarks>Required Setting</remarks>
    <ConfigurationProperty("sqlDialect", DefaultValue:=1, _
                           IsRequired:=True)> _
   Public Property sqlDialect() As Integer
        Get
            Return CInt(Me("sqlDialect"))
        End Get
        Set(ByVal value As Integer)
            Me("sqlDialect") = value
        End Set
    End Property

    ''' <summary>
    ''' Sets the SQL Connection type (NOT The syntax!!) used by DBUtils.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <ConfigurationProperty("sqlConnectionType", DefaultValue:=0, _
                           IsRequired:=False)> _
   Public Property sqlConnectionType() As Integer
        Get
            Return CInt(Me("sqlConnectionType"))
        End Get
        Set(ByVal value As Integer)
            Me("sqlConnectionType") = value
        End Set

    End Property
    ''' <summary>
    ''' Con
    ''' </summary>
    ''' <value></value>
    ''' <returns>
    ''' 1 = Connection String is encypted
    ''' 0 = Connection String is <b>NOT</b> encypted</returns>
    ''' <remarks></remarks>
    <ConfigurationProperty("dbconnstringEncrypted", _
                           DefaultValue:=0, _
                           IsRequired:=True)> _
    Public Property dbConnStringEncrypted() As Integer
        Get
            Return CInt(Me("dbconnstringEncrypted"))
        End Get
        Set(ByVal value As Integer)
            Me("dbconnstringEncrypted") = value
        End Set
    End Property



End Class


''' <summary>
''' Configuration Reader Class for DBConfigRegistry.
''' This class loads the following configuration settings from app.config 
''' file: appname, section, key
''' These are then passed to a GetSetting() call to retrieve a connection string
''' <example>
''' <ol>
''' <li>Define a config section in Config Sections as follows:<br/>
''' &lt;configSections&gt;<br/>
''' ...<br/>
''' &lt;section name="DBConfigRegistry" type="org.codegen.lib.db.DBConfigRegistry,org.codegen.lib.db"/&gt;<br/>
''' &lt;/configSections&gt;<br/>
''' </li>
''' <li>
''' &lt;DBConfigRegistry <br/>
'''          dbRegAppname="AppName" <br/>
'''          dbRegSection="section" <br/>
'''          dbRegKey="somekey" <br/>
'''          dbconnstringEncrypted="0" <br/>
'''          sqlDialect="0" <br/>
'''          sqlConnectionType="0"&gt;<br/></li>
'''</ol>
''' </example>
''' </summary>
''' <remarks></remarks>
Public Class DBConfigRegistry
    Inherits System.Configuration.ConfigurationSection
    ''' <summary>
    ''' Sets the App name to be passed as a first argument in GetSetting()
    ''' </summary>
    ''' <value></value>
    ''' <remarks>Required Setting</remarks>
    <ConfigurationProperty("dbRegAppname", _
                           IsRequired:=True)> _
    Public Property dbRegAppname() As String
        Get
            Return CStr(Me("dbRegAppname"))
        End Get
        Set(ByVal value As String)
            Me("dbRegAppname") = value
        End Set
    End Property

    <ConfigurationProperty("dbConnectionStringRegKey", _
                           IsRequired:=True)> _
    Public Property dbConnectionStringRegKey() As String
        Get
            Return CStr(Me("dbConnectionStringRegKey"))
        End Get
        Set(ByVal value As String)
            Me("dbConnectionStringRegKey") = value
        End Set
    End Property

    <ConfigurationProperty("dbRegSection", _
                           IsRequired:=True)> _
    Public Property dbRegSection() As String
        Get
            Return CStr(Me("dbRegSection"))
        End Get
        Set(ByVal value As String)
            Me("dbRegSection") = value
        End Set
    End Property
    ''' <summary>
    ''' DBUtils can log each sql statement executed along with the time taken to execute the statement.
    ''' </summary>
    ''' <value></value>
    ''' <returns>String of file used to log SQL Stetaments executed by DBUtils</returns>
    ''' <remarks>Not Required Setting</remarks>
    <ConfigurationProperty("logFile", _
                           IsRequired:=False)> _
    Public Property logFile() As String
        Get
            Return CStr(Me("logFile"))
        End Get
        Set(ByVal value As String)
            Me("logFile") = value
        End Set
    End Property
    ''' <summary>
    ''' Sets the sql flavor (syntax) used by DBUtils.
    ''' </summary>
    ''' <value></value>
    ''' <returns>
    '''    0 = MSSQL
    '''    1 = JET/MS Access
    '''    2 = ORACLE
    '''    3 = MYSQL
    ''' </returns>
    ''' <remarks>Required Setting</remarks>
    <ConfigurationProperty("dbRegKeyDialect", DefaultValue:=1, _
                           IsRequired:=True)> _
    Public Property dbRegKeyDialect() As String
        Get
            Return CStr(Me("dbRegKeyDialect"))
        End Get
        Set(ByVal value As String)
            Me("dbRegKeyDialect") = value
        End Set
    End Property

    ''' <summary>
    ''' Sets the SQL Connection type (NOT The syntax!!) used by DBUtils.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <ConfigurationProperty("dbRegKeySqlConnectionType", DefaultValue:=0, _
                           IsRequired:=False)> _
    Public Property dbRegKeySqlConnectionType() As String
        Get
            Return CStr(Me("dbRegKeySqlConnectionType"))
        End Get
        Set(ByVal value As String)
            Me("dbRegKeySqlConnectionType") = value
        End Set

    End Property
    ''' <summary>
    ''' Con
    ''' </summary>
    ''' <value></value>
    ''' <returns>
    ''' 1 = Connection String is encypted
    ''' 0 = Connection String is <b>NOT</b> encypted</returns>
    ''' <remarks></remarks>
    <ConfigurationProperty("dbconnstringEncrypted", _
                           DefaultValue:=0, _
                           IsRequired:=True)> _
    Public Property dbConnStringEncrypted() As String
        Get
            Return CStr(Me("dbconnstringEncrypted"))
        End Get
        Set(ByVal value As String)
            Me("dbconnstringEncrypted") = value
        End Set
    End Property

    ''' <summary></summary>
    ''' <value></value>
    ''' <remarks></remarks>
    <ConfigurationProperty("dbUtilsImplemention",
                           DefaultValue:="",
                           IsRequired:=False)>
    Public Property dbUtilsImplemention() As String
        Get
            Return CStr(Me("dbUtilsImplemention"))
        End Get
        Set(ByVal value As String)
            Me("dbUtilsImplemention") = value
        End Set
    End Property


End Class