using System.Text.RegularExpressions;
using MTG.Importer.Models.Card;
using MTG.Importer.Models.Catalog;
using MTG.Importer.Models.Ruling;
using MTG.Importer.Models.Set;
using MTG.Importer.Save.Interfaces;
using MTG.Scryfall.Importer.Helpers;
using MTG.Scryfall.Importer.Interfaces;
using MTG.Scryfall.Models.Card;
using MTG.Scryfall.Models.Ruling;
using MTG.Scryfall.Models.Set;

namespace MTG.Scryfall.Importer;

public class ScryfallMapper : IScryfallMapper
{
    private readonly IScryfallCardDirector _director;
    private readonly IDatabaseReader _databaseReader;

    public ScryfallMapper(IScryfallCardDirector director, IDatabaseReader databaseReader)
    {
        _director = director;
        _databaseReader = databaseReader;
    }

    public IList<Card> MapCards(IList<ScryfallCard> scryfallCards)
    {
        var cards = new List<Card>();
        var sets = _databaseReader.GetSets();
        var artists = _databaseReader.GetArtists();

        foreach (var scryfallCard in scryfallCards)
        {
            if (scryfallCard.Digital || scryfallCard.Layout.Equals("art_series") || DateHelper.GetDate(scryfallCard.ReleasedAt) > DateTime.Today)
                continue;

            try
            {
                var setId = sets?.FirstOrDefault(x => x.Code == scryfallCard.Set)?.Id;

                if (setId == null || !setId.HasValue)
                    continue;

                if (scryfallCard.ArtistIds != null && !string.IsNullOrEmpty(scryfallCard.Artist) && artists != null)
                    scryfallCard.ArtistIds = GetArtistsId(artists, scryfallCard.ArtistIds, scryfallCard.Artist);

                if (scryfallCard.CardFaces != null && artists != null)
                    foreach (var cardFace in scryfallCard.CardFaces)
                        if (cardFace.ArtistIds != null && !string.IsNullOrEmpty(cardFace.Artist))
                            cardFace.ArtistIds = GetArtistsId(artists, cardFace.ArtistIds, cardFace.Artist);
                        else if (scryfallCard.ArtistIds != null)
                            cardFace.ArtistIds = scryfallCard.ArtistIds;

                scryfallCard.SetId = setId.Value;
                cards.Add(_director.BuildCard(scryfallCard));
            }
            catch (NotSupportedException)
            {
                Console.WriteLine($"{scryfallCard.Name} / {scryfallCard.Layout} is not implemented");
            }
        }

        return cards;
    }

    private static List<Guid> GetArtistsId(IList<Artist> artists, IList<Guid> artistIds, string artistName)
    {
        var name = CleanArtistName(artistName);

        if (artistIds.Count == 1)
            return [artists.First(x => x.Name == name).Id];

        var ids = new List<Guid>();

        foreach (var artist in name.Split(" & "))
            ids.Add(artists.First(x => x.Name == artist).Id);

        return ids;
    }

    private static string CleanArtistName(string artistName)
    {
        var young = new List<string> { "Aliya, age 5½", "Eli, age 8", "Hyan Tran, age 6", "Kira, age 5½", "Mohamed, age 4", "Said, age 6" };

        if (young.Contains(artistName))
            return artistName;

        artistName = Regex.Replace(artistName, @"(“.+” )|(, (a|A)ge \d+(½|¾)?)", "");

        return artistName switch
        {
            "Chengo McFlingers" => "Robert Bliss",
            "Miho Irie" => "Tatamepi",
            "Erica Gassalasca-Jape" => "Heather Hudson",
            "Evkay Alkerway" => "Kev Walker",
            "Claymore J. Flapdoodle" => "Phil Foglio",
            "宋其金/Song Qijin" => "Song Qijin",
            "PuffyGator" => "Nana Qi",
            "Lars Grant-“Wild Wild”-West" => "Lars Grant-West",
            _ => artistName,
        };
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
            supertypes.Add(new Supertype(Guid.NewGuid(), supertypesName));

        return supertypes;
    }

    public IList<CardType> MapCardType(IList<string> cardtypesNames)
    {
        var types = new List<CardType>();

        foreach (var supertypesName in cardtypesNames)
            types.Add(new CardType(Guid.NewGuid(), supertypesName));

        return types;
    }

    public IList<Subtype> MapSubtype(IList<string> subtypesNames, string cardtype)
    {
        var subtypes = new List<Subtype>();

        foreach (var supertypesName in subtypesNames)
            subtypes.Add(new Subtype(Guid.NewGuid(), cardtype, supertypesName));

        return subtypes;
    }

    public IList<Set> MapSets(IList<ScryfallSet> scryfallSets)
    {
        var sets = new List<Set>();

        foreach (var scryfallSet in scryfallSets)
        {
            if (scryfallSet.Digital || DateHelper.GetDate(scryfallSet.ReleasedAt) > DateTime.Today)
                continue;

            sets.Add(MapSet(scryfallSet));
        }

        return sets;
    }

    private Set MapSet(ScryfallSet scryfallSet)
    {
        return new(Guid.NewGuid(), scryfallSet.Name, scryfallSet.Code, scryfallSet.Type, DateHelper.GetDate(scryfallSet.ReleasedAt), StringHelper.GetDefaultValue(scryfallSet.Block), StringHelper.GetDefaultValue(scryfallSet.BlockCode), StringHelper.GetDefaultValue(scryfallSet.ParentSetCode));
    }

    public IList<Ruling> MapRulings(IList<ScryfallRuling> scryfallRulings)
    {
        var rulings = new List<Ruling>();

        foreach (var scryfallRuling in scryfallRulings)
        {
            if (scryfallRuling.Source != "wotc")
                continue;

            rulings.Add(new Ruling(scryfallRuling.OracleId, "en", scryfallRuling.Comment, DateHelper.GetDate(scryfallRuling.PublishedAt)));
        }

        return rulings;
    }
}
