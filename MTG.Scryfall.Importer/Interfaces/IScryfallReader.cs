using MTG.Scryfall.Models.Card;
using MTG.Scryfall.Models.Set;

namespace MTG.Scryfall.Importer.Interfaces;

public interface IScryfallReader
{
    IList<ScryfallCard> ReadCards(StreamReader? streamReader);
    IList<string> ReadCatalog(string? result);
    IList<ScryfallSet> ReadSets(string? result);
}