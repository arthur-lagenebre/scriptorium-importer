using MTG.Importer.Models;
using MTG.Scryfall.Models;

namespace MTG.Scryfall.Interfaces;

public interface IMapper
{
    IList<Card> Map(IList<ScryfallCard> scryfallCards);
}