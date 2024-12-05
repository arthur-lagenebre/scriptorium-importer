using System.Net.Http.Headers;
using MTG.Scryfall.Importer.Interfaces;

namespace MTG.Scryfall.Importer;

public class ScryfallGetter : IScryfallGetter
{
    public StreamReader GetScryfallCardStreamReader()
    {
        var path = @"C:\MTG\_scryfall\all-cards.json";

        return string.IsNullOrEmpty(path) || !File.Exists(path) ? throw new FileNotFoundException(path) : new StreamReader(path);
    }

    public StreamReader GetScryfallRulingStreamReader()
    {
        var path = @"C:\MTG\_scryfall\rulings.json";

        return string.IsNullOrEmpty(path) || !File.Exists(path) ? throw new FileNotFoundException(path) : new StreamReader(path);
    }

    public string GetScryfallUrl(string path)
    {
        var client = new HttpClient();
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        client.DefaultRequestHeaders.UserAgent.Clear();
        client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("MTG.Importer", "1.0"));

        var response = client.GetAsync("https://api.scryfall.com/" + path).Result;

        return !response.IsSuccessStatusCode
            ? throw new Exception(response.Content.ToString())
            : response.Content.ReadAsStringAsync().Result;
    }
}
