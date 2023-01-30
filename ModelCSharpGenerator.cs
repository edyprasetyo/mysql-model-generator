
using MySql.Data.MySqlClient;

public class ModelCSharpGenerator
{
    public static void GenerateModel(MySqlConnection conn, string folderCsharp)
    {
        if (Directory.Exists(folderCsharp))
        {
            Directory.Delete(folderCsharp, true);
        }
        Directory.CreateDirectory(folderCsharp);
        foreach (var table in oFunction.GetTableList(conn))
        {
            var columns = oFunction.GetTableColumn(conn, table, "C#");
            var primaryKeys = oFunction.GetPrimaryKey(conn, table);
            string q = "";
            q += "using NgetemWeb.Helper;" + Environment.NewLine;
            q += "using NPoco;" + Environment.NewLine;
            q += "" + Environment.NewLine;
            q += "namespace NgetemWeb.Data" + Environment.NewLine;
            q += "{" + Environment.NewLine;
            q += "  [TableName(\"" + table + "\")]" + Environment.NewLine;
            var str = "";
            bool isAutoIncrement = false;
            foreach (var primaryKey in primaryKeys)
            {
                str += primaryKey["ColumnName"] + ",";
                isAutoIncrement = (bool)primaryKey["IsAutoIncrement"];
                var dataType = primaryKey["ColumnName"].GetType();
            }
            q += "  [PrimaryKey(\"" + str.Substring(0, str.Length - 1) + "\", AutoIncrement = " + isAutoIncrement.ToString().ToLower() + ")]" + Environment.NewLine;
            q += "  public partial class " + table + "Mdl" + Environment.NewLine;
            q += "  {" + Environment.NewLine;
            foreach (var column in columns)
            {

                if (column["DataType"].ToString()!.Equals("string"))
                {
                    q += "      public " + column["DataType"] + " " + column["ColumnName"] + " { get; set; } = \"\";" + Environment.NewLine;
                }
                else if (column["DataType"].ToString()!.Equals("byte[]"))
                {
                    q += "      public " + column["DataType"] + "? " + column["ColumnName"] + " { get; set; }" + Environment.NewLine;
                }
                else
                {
                    q += "      public " + column["DataType"] + " " + column["ColumnName"] + " { get; set; }" + Environment.NewLine;
                }
            }
            str = "";
            var str1 = "";
            var str2 = "";
            var i = 0;
            foreach (var primaryKey in primaryKeys)
            {
                var colName = primaryKey["ColumnName"].ToString()!;
                var dataType = columns.Where(x => x["ColumnName"].ToString() == colName).FirstOrDefault()!["DataType"];
                str += dataType + " " + oFunction.ToCamelCase(colName) + ", ";
                str1 += oFunction.ToCamelCase(colName) + ", ";
                str2 += colName + " = @" + i + " AND ";
                i++;
            }
            str = str.Substring(0, str.Length - 2);
            str1 = str1.Substring(0, str1.Length - 2);
            str2 = str2.Substring(0, str2.Length - 5);
            q += "      public static " + table + "Mdl GetData(" + str + ")" + Environment.NewLine;
            q += "      {" + Environment.NewLine;
            q += "          return MdlDtl.GetDataQuery<" + table + "Mdl>(\"SELECT * FROM " + table + " WHERE " + str2 + "\", " + str1 + ");" + Environment.NewLine;
            q += "      }" + Environment.NewLine;
            q += "      public static " + table + "Mdl GetDataTransaction(MdlDto oMdlDto, " + str + ")" + Environment.NewLine;
            q += "      {" + Environment.NewLine;
            q += "          return oMdlDto.GetDataQuery<" + table + "Mdl>(\"SELECT * FROM " + table + " WHERE " + str2 + "\", " + str1 + ");" + Environment.NewLine;
            q += "      }" + Environment.NewLine;
            q += "  }" + Environment.NewLine;
            q += "}" + Environment.NewLine;

            var fileName = folderCsharp + table + "Mdl.cs";
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }
            File.WriteAllText(fileName, q);
        }

    }
}