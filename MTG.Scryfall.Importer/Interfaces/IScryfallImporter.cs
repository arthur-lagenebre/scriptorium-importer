using MTG.Importer.Models.Card;

namespace MTG.Scryfall.Importer.Interfaces;

public interface IScryfallImporter
{
    IList<Card> Import();
}