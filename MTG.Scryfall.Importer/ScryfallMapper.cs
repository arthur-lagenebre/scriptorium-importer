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

public class ScryfallMapper(IScryfallCardDirector director, IDatabaseReader databaseReader)
    : IScryfallMapper
{
    public IList<Card> MapCards(IList<ScryfallCard> scryfallCards, List<Ruling> rulings)
    {
        var cards = new List<Card>();
        var sets = databaseReader.GetSets();
        var artists = databaseReader.GetArtists();

        foreach (var scryfallCard in scryfallCards)
        {
            if (scryfallCard.Digital || scryfallCard.Layout.Equals("art_series") || DateHelper.GetDate(scryfallCard.ReleasedAt) > DateTime.Today)
                continue;

            try
            {
                var setId = sets?.FirstOrDefault(x => x.Code == scryfallCard.Set)?.Id;

                if (setId is null)
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
                cards.Add(director.BuildCard(scryfallCard, [.. rulings.Where(x => x.OracleId == scryfallCard.OracleId)]));
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

        return artistIds.Count == 1 ? [artists.First(x => x.Name == name).Id] : name.Split(" & ").Select(artist => artists.First(x => x.Name == artist).Id).ToList();
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
        return artistsNames.Select(artistName => new Artist(Guid.NewGuid(), artistName)).ToList();
    }

    public IList<Supertype> MapSupertype(IList<string> supertypesNames)
    {
        return supertypesNames.Select(supertypesName => new Supertype(Guid.NewGuid(), supertypesName)).ToList();
    }

    public IList<CardType> MapCardType(IList<string> cardtypesNames)
    {
        return cardtypesNames.Select(supertypesName => new CardType(Guid.NewGuid(), supertypesName)).ToList();
    }

    public IList<Subtype> MapSubtype(IList<string> subtypesNames, string cardtype)
    {
        return subtypesNames.Select(supertypesName => new Subtype(Guid.NewGuid(), cardtype, supertypesName)).ToList();
    }

    public IList<Set> MapSets(IList<ScryfallSet> scryfallSets)
    {
        return (from scryfallSet in scryfallSets where !scryfallSet.Digital && DateHelper.GetDate(scryfallSet.ReleasedAt) <= DateTime.Today select MapSet(scryfallSet)).ToList();
    }

    private static Set MapSet(ScryfallSet scryfallSet)
    {
        return new Set(Guid.NewGuid(), scryfallSet.Name, scryfallSet.Code, scryfallSet.Type, DateHelper.GetDate(scryfallSet.ReleasedAt), StringHelper.GetDefaultValue(scryfallSet.Block), StringHelper.GetDefaultValue(scryfallSet.BlockCode), StringHelper.GetDefaultValue(scryfallSet.ParentSetCode));
    }

    public IList<Ruling> MapRulings(IList<ScryfallRuling> scryfallRulings)
    {
        return (from scryfallRuling in scryfallRulings where scryfallRuling.Source == "wotc" select new Ruling(scryfallRuling.OracleId, "en", scryfallRuling.Comment, DateHelper.GetDate(scryfallRuling.PublishedAt))).ToList();
    }
}
