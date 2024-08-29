using MTG.Scryfall.Models.Card;

namespace MTG.Scryfall.Importer.Interfaces;

public interface IScryfallReader
{
    IList<ScryfallCard> Read(StreamReader? streamReader);
}