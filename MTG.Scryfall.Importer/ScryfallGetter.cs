using System.Net.Http.Headers;
using MTG.Scryfall.Importer.Interfaces;

namespace MTG.Scryfall.Importer;

public class ScryfallGetter : IScryfallGetter
{
    public StreamReader GetScryfallCardStreamReader()
    {
        var path = @"D:\Cards Import\_MTG_\_cards.json";

        if (string.IsNullOrEmpty(path) || !File.Exists(path))
            throw new FileNotFoundException(path);

        return new StreamReader(path);
    }

    public StreamReader GetScryfallRulingStreamReader()
    {
        var path = @"D:\Cards Import\_MTG_\_rulings.json";

        if (string.IsNullOrEmpty(path) || !File.Exists(path))
            throw new FileNotFoundException(path);

        return new StreamReader(path);
    }

    public string GetScryfallUrl(string path)
    {
        var client = new HttpClient();
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        client.DefaultRequestHeaders.UserAgent.Clear();
        client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("MTG.Importer", ""));

        var response = client.GetAsync("https://api.scryfall.com/" + path).Result;

        if (!response.IsSuccessStatusCode)
            throw new Exception(response.Content.ToString());

        return response.Content.ReadAsStringAsync().Result;
    }
}
