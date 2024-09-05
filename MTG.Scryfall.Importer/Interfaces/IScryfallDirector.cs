using MTG.Importer.Models.Card;
using MTG.Scryfall.Models.Card;

namespace MTG.Scryfall.Importer.Interfaces;

public interface IScryfallDirector
{
    Card BuildCard(ScryfallCard scryfallCard);
}