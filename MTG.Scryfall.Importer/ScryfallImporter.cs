using MTG.Importer.Models.Card;
using MTG.Scryfall.Importer.Interfaces;

namespace MTG.Scryfall.Importer;

public class ScryfallImporter : IScryfallImporter
{
    private readonly IScryfallGetter _getter;
    private readonly IScryfallReader _reader;
    private readonly IScryfallMapper _mapper;

    public ScryfallImporter(IScryfallGetter getter, IScryfallReader reader, IScryfallMapper mapper)
    {
        _getter = getter;
        _reader = reader;
        _mapper = mapper;
    }

    public IList<Card> Import()
    {
        using var streamReader = _getter.GetScryfallStreamReader();

        var scryfallCards = _reader.Read(streamReader);

        if (scryfallCards == null)
            return [];

        Console.WriteLine($"{scryfallCards.Count} scryfall cards");

        var cards = _mapper.Map(scryfallCards);

        Console.WriteLine($"{cards.Count} mapped");

        return cards;
    }
}
