
using System.Xml;

String URLString = "http://localhost:8080/v1/common/sitemap-deals";

var reader = XmlReader.Create(URLString);
var output = new Output(new SitemapResponse());

while (reader.Read())
{
    if (reader.Name.Equals("loc"))
    {
        reader.Read();
        if (reader.NodeType == XmlNodeType.Text)
        {
            SitemapReqClient client = new SitemapReqClient();
            SitemapResponse res = await client.Get(reader.Value);
            if (res.Status.Equals(client.OK))
            {
                output.AddRecord(res);
            }

            output.Log(res);
        }
    }
}