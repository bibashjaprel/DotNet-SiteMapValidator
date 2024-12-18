using System.Text.Json;

public static class Util
{

    public static void Dump(object obj)
    {
        string json = JsonSerializer.Serialize(obj);
        Console.WriteLine(json);
    }

    public static void Dump(string str)
    {
        Console.WriteLine(str);
    }

}
