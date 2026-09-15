using Scriptorium.Mtg.Scryfall.Importer.Interfaces;
using Scriptorium.Mtg.Scryfall.Models.Card;
using Scriptorium.Mtg.Scryfall.Models.Catalog;
using Scriptorium.Mtg.Scryfall.Models.Ruling;
using Scriptorium.Mtg.Scryfall.Models.Set;
using Newtonsoft.Json;

namespace Scriptorium.Mtg.Scryfall.Importer;

public class ScryfallReader : IScryfallReader
{
    /// <summary>
    /// Les fichiers bulk de Scryfall sont au format JSON Lines : un objet
    /// complet par ligne, sans crochets englobants ni virgules. Ce format
    /// permet de lire le flux sans jamais charger l'ensemble en mémoire —
    /// ce qui compte sur un fichier de plusieurs gigaoctets.
    ///
    /// Les endpoints de l'API, eux, renvoient du JSON classique : d'où la
    /// coexistence des deux façons de lire dans cette classe.
    /// </summary>
    private static IEnumerable<T> ReadLines<T>(StreamReader streamReader)
    {
        var serializer = new JsonSerializer();

        while (streamReader.ReadLine() is { } line)
        {
            var trimmed = line.Trim().TrimEnd(',');

            // Tolère une éventuelle enveloppe de tableau, au cas où le format
            // changerait à nouveau.
            if (trimmed.Length == 0 || trimmed is "[" or "]")
                continue;

            using var reader = new JsonTextReader(new StringReader(trimmed));

            if (serializer.Deserialize<T>(reader) is { } item)
                yield return item;
        }
    }

    public IList<ScryfallCard> ReadCards(StreamReader? streamReader)
    {
        ArgumentNullException.ThrowIfNull(streamReader);

        return ReadLines<ScryfallCard>(streamReader).ToList();
    }

    public IList<ScryfallRuling> ReadRulings(StreamReader streamReader)
    {
        ArgumentNullException.ThrowIfNull(streamReader);

        return ReadLines<ScryfallRuling>(streamReader).ToList();
    }

    public IList<ScryfallSet> ReadSets(string? result)
    {
        ArgumentNullException.ThrowIfNull(result);

        var serializer = new JsonSerializer();

        using var reader = new JsonTextReader(new StringReader(result));

        var scryfallApiResult = serializer.Deserialize<ScryfallSetApiResult>(reader);

        if (scryfallApiResult?.Data == null)
            return [];

        return scryfallApiResult.Data;
    }

    public IList<string> ReadCatalog(string? result)
    {
        ArgumentNullException.ThrowIfNull(result);

        var serializer = new JsonSerializer();

        using var reader = new JsonTextReader(new StringReader(result));

        var scryfallApiResult = serializer.Deserialize<ScryfallCatalog>(reader);

        if (scryfallApiResult?.Data == null)
            return [];

        return scryfallApiResult.Data;
    }
}
