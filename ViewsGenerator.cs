
public class ViewsGenerator
{
    public static void GenerateViews(string projectViewsPath, string projectOutputPath)
    {
        try
        {
            var path = projectViewsPath;
            string temp = "";
            if (!path.Substring(path.Length - 1, 1).Equals("/"))
            {
                path = path + "/";
            }

            string[] y = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories);
            List<string> allfiles = new List<string>();
            foreach (string x in y)
            {
                allfiles.Add(x.Replace(path, ""));
            }
            foreach (string file in allfiles)
            {
                if (file.Equals("Web.config") || file.Equals("_ViewStart.cshtml") || file.ToLower().Contains("_viewimports"))
                {
                    continue;
                }
                string variableName = file.Replace("/", "_").Replace(".cshtml", "").Replace(".", "_");
                string variableValue = "\"../" + file.Replace("/", "/").Replace(".cshtml", "") + "\";";
                temp += "   public const string " + variableName + " = " + variableValue + Environment.NewLine;
            }
            string result = "";
            result += "namespace NgetemWeb.Data" + Environment.NewLine;
            result += "{" + Environment.NewLine;

            result += " public partial class Views" + Environment.NewLine;
            result += " {" + Environment.NewLine;
            result += temp;
            result += " }" + Environment.NewLine;

            result += "}" + Environment.NewLine;
            string filePath = projectOutputPath;
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            using (StreamWriter sw = File.CreateText(filePath))
            {
                sw.WriteLine(result);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}