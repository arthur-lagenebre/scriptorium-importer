using MTG.Scryfall.Importer.Interfaces;
using MTG.Scryfall.Models.Card;
using Newtonsoft.Json;

namespace MTG.Scryfall.Importer;

public class ScryfallReader : IScryfallReader
{
    public IList<ScryfallCard> Read(StreamReader streamReader)
    {
        ArgumentNullException.ThrowIfNull(streamReader);

        using var reader = new JsonTextReader(streamReader);

        var serializer = new JsonSerializer();

        var cards = serializer.Deserialize<List<ScryfallCard>>(reader);

        return cards ?? [];
    }
}