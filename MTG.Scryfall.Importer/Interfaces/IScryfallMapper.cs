using MTG.Importer.Models.Card;
using MTG.Importer.Models.Catalog;
using MTG.Importer.Models.Ruling;
using MTG.Importer.Models.Set;
using MTG.Scryfall.Models.Card;
using MTG.Scryfall.Models.Ruling;
using MTG.Scryfall.Models.Set;

namespace MTG.Scryfall.Importer.Interfaces;

public interface IScryfallMapper
{
    IList<Artist> MapArtist(IList<string> artistsNames);
    IList<Card> MapCards(IList<ScryfallCard> scryfallCards, List<Ruling> rulings);
    IList<CardType> MapCardType(IList<string> cardtypesNames);
    IList<Ruling> MapRulings(IList<ScryfallRuling> scryfallRulings);
    IList<Set> MapSets(IList<ScryfallSet> scryfallSets);
    IList<Subtype> MapSubtype(IList<string> subtypesNames, string cardtype);
    IList<Supertype> MapSupertype(IList<string> supertypesNames);
}