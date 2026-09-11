using Scriptorium.Mtg.Importer.Models.Card;
using Scriptorium.Mtg.Importer.Models.Ruling;
using Scriptorium.Mtg.Scryfall.Models.Card;

namespace Scriptorium.Mtg.Scryfall.Importer.Interfaces;

public interface IScryfallCardDirector
{
    Card BuildCard(ScryfallCard scryfallCard, List<Ruling> rulings);
}