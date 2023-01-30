using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;

public class ModelFlutterGenerator
{
    public static void GenerateModel(MySqlConnection conn, string folderFlutter)
    {
        if (Directory.Exists(folderFlutter))
        {
            Directory.Delete(folderFlutter, true);
        }
        Directory.CreateDirectory(folderFlutter);
        foreach (var table in oFunction.GetTableList(conn))
        {
            var columns = oFunction.GetTableColumn(conn, table, "Flutter");
            var primaryKeys = oFunction.GetPrimaryKey(conn, table);
            string q = "";
            q += "import \"dart:convert\";" + Environment.NewLine;
            q += "import '../helper/tools.dart';" + Environment.NewLine;
            q += "" + Environment.NewLine;
            q += "class " + table + "Mdl {" + Environment.NewLine;

            var required = Environment.NewLine + " " + table + "Mdl({" + Environment.NewLine;
            var fromJson = " " + table + "Mdl.fromJson(Map<String, dynamic> json) :" + Environment.NewLine;
            var initModel = " " + table + "Mdl.initModel() :" + Environment.NewLine;
            var toJson = " Map<String, dynamic> toJson() => {" + Environment.NewLine;
            foreach (var column in columns)
            {
                q += "  " + column["DataType"] + "? " + oFunction.ToCamelCase(column["ColumnName"].ToString()!) + ";" + Environment.NewLine;
                required += "    required this." + oFunction.ToCamelCase(column["ColumnName"].ToString()!) + "," + Environment.NewLine;

                if (column["DataType"].ToString()!.Equals("DateTime"))
                {
                    fromJson += "    " + oFunction.ToCamelCase(column["ColumnName"].ToString()!) + " = Tools.deserializeDatetime(json['" + column["ColumnName"] + "'])," + Environment.NewLine;

                }
                else
                {
                    fromJson += "    " + oFunction.ToCamelCase(column["ColumnName"].ToString()!) + " = json['" + column["ColumnName"] + "']," + Environment.NewLine;

                }

                initModel += "    " + oFunction.ToCamelCase(column["ColumnName"].ToString()!) + " = null," + Environment.NewLine;

                if (column["DataType"].ToString()!.Equals("DateTime"))
                {
                    toJson += "    '" + column["ColumnName"] + "': Tools.serializeDatetime(" + oFunction.ToCamelCase(column["ColumnName"].ToString()!) + "!)," + Environment.NewLine;

                }
                else
                {
                    toJson += "    '" + column["ColumnName"] + "': " + oFunction.ToCamelCase(column["ColumnName"].ToString()!) + "," + Environment.NewLine;

                }

            }

            required += " });";

            fromJson = fromJson.Substring(0, fromJson.Length - 2);
            fromJson += " ;";

            initModel = initModel.Substring(0, initModel.Length - 2);
            initModel += " ;";

            toJson = toJson.Substring(0, toJson.Length - 2);
            toJson += " };" + Environment.NewLine;

            q += required + Environment.NewLine + Environment.NewLine;
            q += fromJson + Environment.NewLine + Environment.NewLine;
            q += initModel + Environment.NewLine + Environment.NewLine;
            q += toJson + Environment.NewLine + Environment.NewLine;

            q += "  static void deleteModel() {" + Environment.NewLine;
            q += "      return Tools.saveString('" + table + "Mdl', '');" + Environment.NewLine;
            q += "  }" + Environment.NewLine + Environment.NewLine;

            var primParam = "";
            var primJson = "";
            foreach (var primaryKey in primaryKeys)
            {
                var colName = primaryKey["ColumnName"].ToString()!;
                var dataType = columns.Where(x => x["ColumnName"].ToString() == colName).FirstOrDefault()!["DataType"];
                primParam += dataType + " " + oFunction.ToCamelCase(colName) + ", ";
                primJson += "'" + colName + "': " + oFunction.ToCamelCase(colName) + ", ";
            }
            // q += "  static Future<" + table + "Mdl?> getData(" + primParam + ") async {" + Environment.NewLine;
            // q += "      try {" + Environment.NewLine;
            // q += "          var json = await Tools.postRequest('Model/Get" + table + "Mdl', {" + primJson + "});" + Environment.NewLine;
            // q += "          var o = " + table + "Mdl.fromJson(json);" + Environment.NewLine;
            // q += "          saveModel(o);" + Environment.NewLine;
            // q += "          return o;" + Environment.NewLine;
            // q += "      } catch (e) {" + Environment.NewLine;
            // q += "          return null;" + Environment.NewLine;
            // q += "      }" + Environment.NewLine;
            // q += "  }" + Environment.NewLine + Environment.NewLine;

            q += "  static " + table + "Mdl? getModel() {" + Environment.NewLine;
            q += "      try {" + Environment.NewLine;
            q += "          String json = Tools.getString('" + table + "Mdl');" + Environment.NewLine;
            q += "          Map<String, dynamic> oMap = jsonDecode(json);" + Environment.NewLine;
            q += "          return " + table + "Mdl.fromJson(oMap);" + Environment.NewLine;
            q += "      } catch (e) {" + Environment.NewLine;
            q += "          return null;" + Environment.NewLine;
            q += "      }" + Environment.NewLine;
            q += "  }" + Environment.NewLine + Environment.NewLine;

            q += "  static void saveModel(" + table + "Mdl oModel) {" + Environment.NewLine;
            q += "      return Tools.saveString('" + table + "Mdl', jsonEncode(oModel));" + Environment.NewLine;
            q += "  }" + Environment.NewLine + Environment.NewLine;

            q += "  static void saveModelJson(Map<String, dynamic> oMap) {" + Environment.NewLine;
            q += "      return Tools.saveString('" + table + "Mdl', jsonEncode(" + table + "Mdl.fromJson(oMap)));" + Environment.NewLine;
            q += "  }" + Environment.NewLine + Environment.NewLine;

            q += "}" + Environment.NewLine;

            string lowercasetable = Regex.Replace(table, "(?<=[a-z])([A-Z])", "_$1").ToLower();
            var fileName = folderFlutter + lowercasetable + "_mdl.dart";
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }
            File.WriteAllText(fileName, q);
        }

    }
}