using System.Globalization;
using CsvHelper;

public class Output
{
    private TextWriter _writer;
    private CsvWriter _csvWriter;
    private const string OutputFile = "output.csv";

    public Output(SitemapResponse res)
    {
        this._writer = new StreamWriter(OutputFile, false, System.Text.Encoding.UTF8);
        this._csvWriter = new CsvWriter(this._writer, CultureInfo.InvariantCulture);
        this._csvWriter.WriteHeader<SitemapResponse>();
    }

    public void AddRecord(SitemapResponse res)
    {
        this._csvWriter.NextRecord();
        this._csvWriter.WriteRecord(res);
    }

    public void Log(SitemapResponse res)
    {
        Util.Dump($"{res.Status}: URL: {res.URL}");
    }

}