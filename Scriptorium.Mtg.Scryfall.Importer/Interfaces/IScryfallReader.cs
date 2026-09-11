using Scriptorium.Mtg.Scryfall.Models.Card;
using Scriptorium.Mtg.Scryfall.Models.Ruling;
using Scriptorium.Mtg.Scryfall.Models.Set;

namespace Scriptorium.Mtg.Scryfall.Importer.Interfaces;

public interface IScryfallReader
{
    IList<ScryfallCard> ReadCards(StreamReader? streamReader);
    IList<string> ReadCatalog(string? result);
    IList<ScryfallRuling> ReadRulings(StreamReader streamReader);
    IList<ScryfallSet> ReadSets(string? result);
}