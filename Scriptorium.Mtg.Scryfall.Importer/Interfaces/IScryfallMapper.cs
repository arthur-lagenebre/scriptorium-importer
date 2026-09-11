using Scriptorium.Mtg.Importer.Models.Card;
using Scriptorium.Mtg.Importer.Models.Catalog;
using Scriptorium.Mtg.Importer.Models.Ruling;
using Scriptorium.Mtg.Importer.Models.Set;
using Scriptorium.Mtg.Scryfall.Models.Card;
using Scriptorium.Mtg.Scryfall.Models.Ruling;
using Scriptorium.Mtg.Scryfall.Models.Set;

namespace Scriptorium.Mtg.Scryfall.Importer.Interfaces;

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