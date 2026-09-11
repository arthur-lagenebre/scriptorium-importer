using Scriptorium.Mtg.Scryfall.Importer.Interfaces;

namespace Scriptorium.Mtg.Scryfall.Importer;

public class ScryfallGetter(HttpClient httpClient, ScryfallOptions options) : IScryfallGetter
{
    public StreamReader GetScryfallCardStreamReader() => OpenFile(options.CardsFilePath);

    public StreamReader GetScryfallRulingStreamReader() => OpenFile(options.RulingsFilePath);

    public string GetScryfallUrl(string path)
    {
        var response = httpClient.GetAsync(path).Result;
        response.EnsureSuccessStatusCode();

        return response.Content.ReadAsStringAsync().Result;
    }

    private static StreamReader OpenFile(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
            throw new InvalidOperationException("Aucun chemin configuré. Renseignez Scryfall:CardsFilePath et Scryfall:RulingsFilePath dans appsettings.json.");

        if (!File.Exists(path))
            throw new FileNotFoundException($"Fichier bulk introuvable : {path}", path);

        return new StreamReader(path);
    }
}