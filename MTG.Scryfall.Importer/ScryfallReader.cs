using MTG.Scryfall.Importer.Interfaces;
using MTG.Scryfall.Models.Card;
using MTG.Scryfall.Models.Catalog;
using MTG.Scryfall.Models.Set;
using Newtonsoft.Json;

namespace MTG.Scryfall.Importer;

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
}