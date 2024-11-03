using MTG.Importer.Models.Card;
using MTG.Importer.Models.Set;

namespace MTG.Scryfall.Importer.Interfaces;

public interface IScryfallImporter
{
    IList<Card> CardImport();
    IList<Set> SetImport();
}