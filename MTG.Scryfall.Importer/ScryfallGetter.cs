using System.IO;
using System.Net.Http.Headers;
using MTG.Scryfall.Importer.Interfaces;

namespace MTG.Scryfall.Importer;

public class ScryfallGetter : IScryfallGetter
{
    public StreamReader GetScryfallCardStreamReader()
    {
        var path = @"D:\Cards Import\_MTG_\42_cards.json";

        if (string.IsNullOrEmpty(path) || !File.Exists(path))
            throw new FileNotFoundException(path);

        return new StreamReader(path);
    }

    public async Task<string> GetScryfallSetStreamReader()
    {
        var client = new HttpClient();
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        client.DefaultRequestHeaders.UserAgent.Clear();
        client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("MTG.Importer", ""));

        HttpResponseMessage response = await client.GetAsync("https://api.scryfall.com/sets");

        if (!response.IsSuccessStatusCode)
            throw new Exception(response.Content.ToString());

        return await response.Content.ReadAsStringAsync();
    }
}
