using MTG.Importer.Models.Card;
using MTG.Scryfall.Models.Card;

namespace MTG.Scryfall.Importer.Interfaces;

public interface IScryfallMapper
{
    IList<Card> Map(IList<ScryfallCard> scryfallCards);
}