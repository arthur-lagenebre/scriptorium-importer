using MTG.Importer.Models.Card;
using MTG.Importer.Models.Set;
using MTG.Scryfall.Models.Card;
using MTG.Scryfall.Models.Set;

namespace MTG.Scryfall.Importer.Interfaces;

public interface IScryfallMapper
{
    IList<Card> MapCards(IList<ScryfallCard> scryfallCards);
    IList<Set> MapSets(IList<ScryfallSet> scryfallSets);
}