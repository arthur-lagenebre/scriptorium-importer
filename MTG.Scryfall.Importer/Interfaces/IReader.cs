using MTG.Scryfall.Models;

namespace MTG.Scryfall.Interfaces;

public interface IReader
{
    IList<ScryfallCard> Read(StreamReader streamReader);
}