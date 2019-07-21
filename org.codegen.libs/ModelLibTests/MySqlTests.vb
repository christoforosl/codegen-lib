Imports org.codegen.model.lib.db.mysql


<TestClass()>
Public Class MySqlTests

    <TestMethod()>
    Public Sub testMySQLDBUtils()
        'connectionString="Server=skyhill.ipowermysql.com;Database=skyhill;Uid=skyhillweb;Pwd=yourpassword;
        Dim mysqlConnString = "Server=icopoghru9oezxh8.cbetxkdyhwsb.us-east-1.rds.amazonaws.com;Database=dqxjlwq6iyyfoiu9;Uid=qhhccud6o814xg95;Pwd=h6tinw7w7gcywek9"

        Dim mysqlUtils = DBUtils.getFromConnString(mysqlConnString, GetType(MySQLDbUtils))
        mysqlUtils.getSValue("select alert_descr from alert")
        mysqlUtils.executeSQL("update alert set alert_descr='x' where alert_descr='---'")


    End Sub

End Class
