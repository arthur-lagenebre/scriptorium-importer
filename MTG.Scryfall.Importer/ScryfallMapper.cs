using MTG.Importer.Models.Card;
using MTG.Importer.Models.Set;
using MTG.Scryfall.Importer.Helpers;
using MTG.Scryfall.Importer.Interfaces;
using MTG.Scryfall.Models.Card;
using MTG.Scryfall.Models.Set;

namespace MTG.Scryfall.Importer;

public class ScryfallMapper : IScryfallMapper
{
    private readonly IScryfallCardDirector _director;

    public ScryfallMapper(IScryfallCardDirector director) => _director = director;

    public IList<Card> MapCards(IList<ScryfallCard> scryfallCards)
    {
        var cards = new List<Card>();

        foreach (var scryfallCard in scryfallCards)
        {
            if (scryfallCard.Digital)
            {
                Console.WriteLine($"{scryfallCard.Name} is only digital");
                continue;
            }

            try
            {
                cards.Add(_director.BuildCard(scryfallCard));
            }
            catch (NotSupportedException)
            {
                Console.WriteLine($"{scryfallCard.Name} / {scryfallCard.Layout} is not implemented");
            }
        }

        return cards;
    }

    public IList<Set> MapSets(IList<ScryfallSet> scryfallSets)
    {
        var sets = new List<Set>();

        foreach (var scryfallSet in scryfallSets)
        {
            if (scryfallSet.Digital)
            {
                Console.WriteLine($"{scryfallSet.Name} is only digital");
                continue;
            }

            sets.Add(MapSet(scryfallSet));
        }

        return sets;
    }

    private Set MapSet(ScryfallSet scryfallSet) => new Set(Guid.NewGuid(), scryfallSet.Name, scryfallSet.Code, scryfallSet.Type, DateHelper.GetDate(scryfallSet.ReleasedAt), StringHelper.GetDefaultValue(scryfallSet.Block), StringHelper.GetDefaultValue(scryfallSet.BlockCode), StringHelper.GetDefaultValue(scryfallSet.ParentSetCode));
}
