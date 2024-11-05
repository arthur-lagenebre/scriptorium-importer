using MTG.Importer.Models.Card;
using MTG.Importer.Models.Catalog;
using MTG.Importer.Models.Set;
using MTG.Scryfall.Importer.Helpers;
using MTG.Scryfall.Importer.Interfaces;
using MTG.Scryfall.Models.Card;
using MTG.Scryfall.Models.Set;

namespace MTG.Scryfall.Importer;

public class ScryfallMapper : IScryfallMapper
{
    private readonly IScryfallCardDirector _director;
    private const string _language = "en";

    public ScryfallMapper(IScryfallCardDirector director) => _director = director;

    public IList<Card> MapCards(IList<ScryfallCard> scryfallCards)
    {
        var cards = new List<Card>();

        foreach (var scryfallCard in scryfallCards)
        {
            if (scryfallCard.Digital)
            {
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

    public IList<Artist> MapArtist(IList<string> artistsNames)
    {
        var artists = new List<Artist>();

        foreach (var artistName in artistsNames)
            artists.Add(new Artist(Guid.NewGuid(), artistName));

        return artists;
    }

    public IList<Supertype> MapSupertype(IList<string> supertypesNames)
    {
        var supertypes = new List<Supertype>();

        foreach (var supertypesName in supertypesNames)
            supertypes.Add(new Supertype(Guid.NewGuid(), _language, supertypesName));

        return supertypes;
    }

    public IList<CardType> MapCardType(IList<string> cardtypesNames)
    {
        var types = new List<CardType>();

        foreach (var supertypesName in cardtypesNames)
            types.Add(new CardType(Guid.NewGuid(), _language, supertypesName));

        return types;
    }

    public IList<Subtype> MapSubtype(IList<string> subtypesNames, string cardtype)
    {
        var subtypes = new List<Subtype>();

        foreach (var supertypesName in subtypesNames)
            subtypes.Add(new Subtype(Guid.NewGuid(), _language, cardtype, supertypesName));

        return subtypes;
    }

    public IList<Set> MapSets(IList<ScryfallSet> scryfallSets)
    {
        var sets = new List<Set>();

        foreach (var scryfallSet in scryfallSets)
        {
            if (scryfallSet.Digital)
            {
                continue;
            }

            sets.Add(MapSet(scryfallSet));
        }

        return sets;
    }

    private Set MapSet(ScryfallSet scryfallSet) => new(Guid.NewGuid(), scryfallSet.Name, scryfallSet.Code, scryfallSet.Type, DateHelper.GetDate(scryfallSet.ReleasedAt), StringHelper.GetDefaultValue(scryfallSet.Block), StringHelper.GetDefaultValue(scryfallSet.BlockCode), StringHelper.GetDefaultValue(scryfallSet.ParentSetCode));
}
