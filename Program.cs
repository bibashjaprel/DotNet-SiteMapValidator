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

u.Dump("-----------Starting the process -----------");


while (reader.Read())
{
    if (reader.Name.Equals("loc"))
    {
        reader.Read();
        if (reader.NodeType == XmlNodeType.Text)
        {
            await u.MakeRequest(reader.Value);
        }
    }
}


public struct Record
{
    public string URL { get; set; }

    public string Status { get; set; }
    public int StatusCode { get; set; }
    public string Message { get; set; }


    public Record(string url, string status, int statusCode, string message)
    {
        URL = url;
        Status = status;
        StatusCode = statusCode;
        Message = message;
    }
}


public class Util
{
    private HttpClient _httpClient;
    private TextWriter _writer;
    private CsvWriter _csvWriter;

    private const string OutputFile = "output.csv";
    private const string OK = "OK";
    private const string NOT_OK = "NOT_OK";

    public Util()
    {
        this._httpClient = new HttpClient();
        this._writer = new StreamWriter(OutputFile, false, System.Text.Encoding.UTF8);
        this._csvWriter = new CsvWriter(this._writer, CultureInfo.InvariantCulture);
        this._csvWriter.WriteHeader<Record>();
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
            Record rec = new Record(url, OK, (int)response.StatusCode, "");
            OutputResult(rec);
        }
        catch (HttpRequestException ex)
        {
            int statusCode = ex.StatusCode != null ? Convert.ToInt32(ex.StatusCode) : 400;
            Record rec = new Record(url, NOT_OK, statusCode, ex.Message);
            OutputResult(rec);
        }
    }

    public void OutputResult(Record r)
    {
        if (r.Status.Equals(NOT_OK))
        {
            this._csvWriter.NextRecord();
            this._csvWriter.WriteRecord(r);
        }
        Dump($"{r.Status}: URL: {r.URL}");
    }
}