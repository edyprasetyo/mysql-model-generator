using MySql.Data.MySqlClient;

var currentDir = Environment.CurrentDirectory;
string connStr = "Server=10.200.0.70;Port=6002;Database=Ngetem;Uid=app;Pwd=Indorent10!;";
MySqlConnection conn = new MySqlConnection(connStr);
conn.Open();

ViewsGenerator.GenerateViews(currentDir.Replace("ModelGeneratorMysql", "NgetemWeb/NgetemWeb/Views"),
                    currentDir.Replace("ModelGeneratorMysql", "NgetemWeb/NgetemWeb/DataModelExt/Views.cs"));

ModelCSharpGenerator.GenerateModel(conn, currentDir.Replace("ModelGeneratorMysql", "NgetemWeb/NgetemWeb/DataModel/"));

ModelFlutterGenerator.GenerateModel(conn, currentDir.Replace("ModelGeneratorMysql", "NgetemDepo/lib/model/"));

ModelFlutterGenerator.GenerateModel(conn, currentDir.Replace("ModelGeneratorMysql", "NgetemMobile/lib/model/"));

Console.WriteLine("All Models & Views Generated Successfully");

