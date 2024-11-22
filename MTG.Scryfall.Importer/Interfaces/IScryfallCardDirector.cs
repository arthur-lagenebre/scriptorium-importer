using MTG.Importer.Models.Card;
using MTG.Importer.Models.Ruling;
using MTG.Scryfall.Models.Card;

namespace MTG.Scryfall.Importer.Interfaces;

public interface IScryfallCardDirector
{
    Card BuildCard(ScryfallCard scryfallCard, List<Ruling> rulings);
}