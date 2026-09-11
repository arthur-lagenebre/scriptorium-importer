using Scriptorium.Mtg.Scryfall.Importer.Interfaces;
using Scriptorium.Mtg.Scryfall.Models.Card;
using Scriptorium.Mtg.Scryfall.Models.Catalog;
using Scriptorium.Mtg.Scryfall.Models.Ruling;
using Scriptorium.Mtg.Scryfall.Models.Set;
using Newtonsoft.Json;

namespace Scriptorium.Mtg.Scryfall.Importer;

public class ScryfallReader : IScryfallReader
{
    public IList<ScryfallCard> ReadCards(StreamReader? streamReader)
    {
        ArgumentNullException.ThrowIfNull(streamReader);

        using var reader = new JsonTextReader(streamReader);

        var serializer = new JsonSerializer();

        var cards = serializer.Deserialize<List<ScryfallCard>>(reader);

        return cards ?? [];
    }

    public IList<ScryfallSet> ReadSets(string? result)
    {
        ArgumentNullException.ThrowIfNull(result);

        var serializer = new JsonSerializer();

        using var reader = new JsonTextReader(new StringReader(result));

        var scryfallApiResult = serializer.Deserialize<ScryfallSetApiResult>(reader);

        if (scryfallApiResult == null || scryfallApiResult.Data == null)
            return [];

        return scryfallApiResult.Data;
    }

    public IList<string> ReadCatalog(string? result)
    {
        ArgumentNullException.ThrowIfNull(result);

        var serializer = new JsonSerializer();

        using var reader = new JsonTextReader(new StringReader(result));

        var scryfallApiResult = serializer.Deserialize<ScryfallCatalog>(reader);

        if (scryfallApiResult == null || scryfallApiResult.Data == null)
            return [];

        return scryfallApiResult.Data;
    }

    public IList<ScryfallRuling> ReadRulings(StreamReader streamReader)
    {
        ArgumentNullException.ThrowIfNull(streamReader);

        using var reader = new JsonTextReader(streamReader);

        var serializer = new JsonSerializer();

        var rulings = serializer.Deserialize<List<ScryfallRuling>>(reader);

        return rulings ?? [];
    }
}