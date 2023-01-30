
using System.Data;
using MySql.Data.MySqlClient;

public class oFunction
{
    public static List<string> GetTableList(MySqlConnection conn)
    {
        DataTable dt = conn.GetSchema("Tables");
        List<string> tableList = new List<string>();
        foreach (DataRow row in dt.Rows)
        {
            string tablename = (string)row[2];
            tableList.Add(tablename);
        }
        return tableList;
    }

    public static List<Dictionary<string, object>> GetTableColumn(MySqlConnection conn, string tableName, string lang)
    {
        using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM " + tableName, conn))
        {
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                DataTable schemaTable = reader.GetSchemaTable();
                List<Dictionary<string, object>> oList = new List<Dictionary<string, object>>();
                foreach (DataRow row in schemaTable.Rows)
                {
                    string columnName = row["ColumnName"].ToString()!;
                    Type dataType = (Type)row["DataType"];
                    string dataTypeName = dataType.ToString().Replace("System.", "");
                    if (dataType == typeof(string))
                    {
                        if (lang.Equals("C#"))
                        {
                            dataTypeName = "string";
                        }
                        else if (lang.Equals("Flutter"))
                        {
                            dataTypeName = "String";
                        }
                    }
                    else if (dataType == typeof(string[]))
                    {
                        if (lang.Equals("C#"))
                        {
                            dataTypeName = "string[]";
                        }
                        else if (lang.Equals("Flutter"))
                        {
                            dataTypeName = "List<String>";
                        }
                    }
                    else if (dataType == typeof(DateTime))
                    {
                        dataTypeName = "DateTime";
                    }
                    else if (dataType == typeof(int))
                    {
                        dataTypeName = "int";
                    }
                    else if (dataType == typeof(double))
                    {
                        dataTypeName = "double";
                    }
                    else if (dataType == typeof(bool))
                    {
                        dataTypeName = "bool";
                    }
                    else if (dataType == typeof(decimal))
                    {
                        if (lang.Equals("C#"))
                        {
                            dataTypeName = "decimal";
                        }
                        else if (lang.Equals("Flutter"))
                        {
                            dataTypeName = "double";
                        }
                    }
                    else if (dataType == typeof(Guid))
                    {
                        dataTypeName = "Guid";
                    }
                    else if (dataType == typeof(byte[]))
                    {
                        if (lang.Equals("C#"))
                        {
                            dataTypeName = "byte[]";
                        }
                        else if (lang.Equals("Flutter"))
                        {
                            dataTypeName = "List<int>";
                        }
                    }
                    else if (dataType == typeof(object))
                    {
                        dataTypeName = "object";
                    }
                    else if (dataType == typeof(long))
                    {
                        if (lang.Equals("C#"))
                        {
                            dataTypeName = "long";
                        }
                        else if (lang.Equals("Flutter"))
                        {
                            dataTypeName = "int";
                        }
                    }
                    else if (dataType == typeof(float))
                    {
                        dataTypeName = "float";
                    }
                    else if (dataType == typeof(short))
                    {
                        dataTypeName = "short";
                    }
                    else if (dataType == typeof(byte))
                    {
                        dataTypeName = "byte";
                    }
                    else if (dataType == typeof(char))
                    {
                        dataTypeName = "char";
                    }
                    else if (dataType == typeof(uint))
                    {
                        dataTypeName = "uint";
                    }
                    else if (dataType == typeof(ulong))
                    {
                        dataTypeName = "ulong";
                    }
                    else if (dataType == typeof(ushort))
                    {
                        dataTypeName = "ushort";
                    }
                    else if (dataType == typeof(sbyte))
                    {
                        dataTypeName = "sbyte";
                    }
                    else if (dataType == typeof(TimeSpan))
                    {
                        dataTypeName = "TimeSpan";
                    }
                    else if (dataType == typeof(DateTimeOffset))
                    {
                        dataTypeName = "DateTimeOffset";
                    }
                    else if (dataType == typeof(Uri))
                    {
                        dataTypeName = "Uri";
                    }
                    else if (dataType == typeof(DBNull))
                    {
                        dataTypeName = "DBNull";
                    }
                    else if (dataType == typeof(Version))
                    {
                        dataTypeName = "Version";
                    }
                    else if (dataType == typeof(Nullable))
                    {
                        dataTypeName = "Nullable";
                    }
                    else if (dataType == typeof(Enum))
                    {
                        dataTypeName = "Enum";
                    }
                    else if (dataType == typeof(Array))
                    {
                        dataTypeName = "Array";
                    }
                    else if (dataType == typeof(ValueType))
                    {
                        dataTypeName = "ValueType";
                    }
                    else if (dataType == typeof(MulticastDelegate))
                    {
                        dataTypeName = "MulticastDelegate";
                    }
                    else if (dataType == typeof(Delegate))
                    {
                        dataTypeName = "Delegate";
                    }
                    else if (dataType == typeof(object[]))
                    {
                        dataTypeName = "object[]";
                    }
                    else if (dataType == typeof(int[]))
                    {
                        dataTypeName = "int[]";
                    }
                    else if (dataType == typeof(double[]))
                    {
                        dataTypeName = "double[]";
                    }
                    else if (dataType == typeof(bool[]))
                    {
                        dataTypeName = "bool[]";
                    }
                    else if (dataType == typeof(decimal[]))
                    {
                        dataTypeName = "decimal[]";
                    }
                    else if (dataType == typeof(Guid[]))
                    {
                        dataTypeName = "Guid[]";
                    }
                    else if (dataType == typeof(byte[][]))
                    {
                        dataTypeName = "byte[][]";
                    }
                    else if (dataType == typeof(long[]))
                    {
                        dataTypeName = "long[]";
                    }
                    else if (dataType == typeof(float[]))
                    {
                        dataTypeName = "float[]";
                    }
                    else if (dataType == typeof(short[]))
                    {
                        dataTypeName = "short[]";
                    }
                    else if (dataType == typeof(byte[]))
                    {
                        dataTypeName = "byte[]";
                    }
                    else if (dataType == typeof(char[]))
                    {
                        dataTypeName = "char[]";
                    }
                    else if (dataType == typeof(uint[]))
                    {
                        dataTypeName = "uint[]";
                    }
                    else if (dataType == typeof(ulong[]))
                    {
                        dataTypeName = "ulong[]";
                    }
                    else if (dataType == typeof(ushort[]))
                    {
                        dataTypeName = "ushort[]";
                    }
                    else if (dataType == typeof(sbyte[]))
                    {
                        dataTypeName = "sbyte[]";
                    }
                    else if (dataType == typeof(TimeSpan[]))
                    {
                        dataTypeName = "TimeSpan[]";
                    }
                    else if (dataType == typeof(DateTimeOffset[]))
                    {
                        dataTypeName = "DateTimeOffset[]";
                    }
                    else if (dataType == typeof(Uri[]))
                    {
                        dataTypeName = "Uri[]";
                    }
                    else if (dataType == typeof(DBNull[]))
                    {
                        dataTypeName = "DBNull[]";
                    }
                    else if (dataType == typeof(Version[]))
                    {
                        dataTypeName = "Version[]";
                    }
                    else if (dataType == typeof(Enum[]))
                    {
                        dataTypeName = "Enum[]";
                    }
                    else if (dataType == typeof(Array[]))
                    {
                        dataTypeName = "Array[]";
                    }
                    else if (dataType == typeof(ValueType[]))
                    {
                        dataTypeName = "ValueType[]";
                    }
                    else if (dataType == typeof(MulticastDelegate[]))
                    {
                        dataTypeName = "MulticastDelegate[]";
                    }
                    else if (dataType == typeof(Delegate[]))
                    {
                        dataTypeName = "Delegate[]";
                    }
                    else
                    {
                        dataTypeName = "object";
                    }
                    oList.Add(new Dictionary<string, object> { { "ColumnName", columnName }, { "DataType", dataTypeName } });
                }
                return oList;
            }
        }
    }

    public static List<Dictionary<string, object>> GetPrimaryKey(MySqlConnection conn, string tableName)
    {
        string sql = "SELECT COLUMN_NAME, EXTRA FROM information_schema.COLUMNS WHERE TABLE_NAME = '" + tableName + "' AND COLUMN_KEY = 'PRI' ORDER BY ORDINAL_POSITION";
        MySqlCommand command = new MySqlCommand(sql, conn);
        List<Dictionary<string, object>> oList = new List<Dictionary<string, object>>();
        using (MySqlDataReader reader = command.ExecuteReader())
        {
            while (reader.Read())
            {
                string columnName = reader.GetString(0);
                string extra = reader.GetString(1);
                bool isAutoIncrement = extra == "auto_increment";
                oList.Add(new Dictionary<string, object> { { "ColumnName", columnName }, { "IsAutoIncrement", isAutoIncrement } });
            }
        }
        return oList;
    }


    public static string ToCamelCase(string str)
    {
        if (!string.IsNullOrEmpty(str) && str.Length > 1)
        {
            return char.ToLowerInvariant(str[0]) + str.Substring(1);
        }
        return str;
    }



}