using System.Net.Http.Headers;
using Scriptorium.Mtg.Scryfall.Importer.Interfaces;

namespace Scriptorium.Mtg.Scryfall.Importer;

public class ScryfallGetter : IScryfallGetter
{
    public StreamReader GetScryfallCardStreamReader()
    {
        var path = @"D:\Cards Import\_MTG_\_cards.json";

        return string.IsNullOrEmpty(path) || !File.Exists(path) ? throw new FileNotFoundException(path) : new StreamReader(path);
    }

    public StreamReader GetScryfallRulingStreamReader()
    {
        var path = @"D:\Cards Import\_MTG_\_rulings.json";

        return string.IsNullOrEmpty(path) || !File.Exists(path) ? throw new FileNotFoundException(path) : new StreamReader(path);
    }

    public string GetScryfallUrl(string path)
    {
        var client = new HttpClient();
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        client.DefaultRequestHeaders.UserAgent.Clear();
        client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("Scriptorium.Mtg.Importer", "1.0"));

        var response = client.GetAsync("https://api.scryfall.com/" + path).Result;

        return !response.IsSuccessStatusCode
            ? throw new Exception(response.Content.ToString())
            : response.Content.ReadAsStringAsync().Result;
    }
}
