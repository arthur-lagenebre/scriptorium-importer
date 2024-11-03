using MTG.Importer.Models.Card;
using MTG.Importer.Models.Set;
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

    public IList<Card> CardImport()
    {
        using var streamReader = _getter.GetScryfallCardStreamReader();

        var scryfallCards = _reader.ReadCards(streamReader);

        if (scryfallCards == null)
            return [];

        Console.WriteLine($"{scryfallCards.Count} scryfall cards");

        var cards = _mapper.MapCards(scryfallCards);

        Console.WriteLine($"{cards.Count} mapped");

        return cards;
    }

    public IList<Set> SetImport()
    {
        var result = _getter.GetScryfallSetStreamReader().Result;

        var scryfallSets = _reader.ReadSets(result);

        if (scryfallSets == null)
            return [];

        var sets = _mapper.MapSets(scryfallSets);

        return sets;
    }
}
