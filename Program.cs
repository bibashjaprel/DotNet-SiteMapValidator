/**
     1. Add to the list
     2. Loop through list -> call if the URL is giving 200 Ok or not
     3. If errored -> create a CSV file for error with link & the error
     4. Once done, try to make it faster & efficient with multiple threads
     5. Unit tests
**/

using System.Xml;
using System.Text.Json;
using CsvHelper;
using System.Globalization;

String URLString = "http://localhost:8080/v1/common/sitemap-deals";
var reader = XmlReader.Create(URLString);

Util u = new Util();
u.WriteToCSV();
// while (reader.Read())
// {
//     if (reader.Name.Equals("loc"))
//     {
//         reader.Read();
//         if (reader.NodeType == XmlNodeType.Text)
//         {
//             await u.MakeRequest(reader.Value);
//         }
//     }
// }


public class Util
{
    private HttpClient _httpClient;

    public Util()
    {
        this._httpClient = new HttpClient();
    }

    public void Dump(object obj)
    {
        string json = JsonSerializer.Serialize(obj);
        Console.WriteLine(json);
    }

    public void Dump(string str)
    {
        Console.WriteLine(str);
    }

    public async Task MakeRequest(string url)
    {
        try
        {
            HttpResponseMessage response = await _httpClient.GetAsync(url);
            Dump(response.StatusCode);
        }
        catch (HttpRequestException ex)
        {
            Dump(ex.Message);
        }

    }

    public void WriteToCSV()
    {
        using (var writer = new StreamWriter("hello.csv", false, System.Text.Encoding.UTF8))
        using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
        {
            var randomObject = new
            {
                Id = 1,
                Name = "John Doe",
                Age = 25,
                Email = "johndoe@example.com"
            };

            csv.WriteRecord(randomObject);
        }
    }
}