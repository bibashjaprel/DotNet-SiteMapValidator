using System.Xml;
using Newtonsoft.Json;

String URLString = "http://localhost:8080/v1/common/sitemap-deals";
var reader = XmlReader.Create(URLString);

while (reader.Read())
{
    if (reader.Name.Equals("loc"))
    {
        reader.Read();
        if (reader.NodeType == XmlNodeType.Text)
        {
            Console.WriteLine(reader.Value);
        }
    }
}

public static class F
{
    public static string Dump(object obj)
    {
        return JsonConvert.SerializeObject(obj);
    }
}