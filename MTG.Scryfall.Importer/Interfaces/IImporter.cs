using MTG.Importer.Models;

namespace MTG.Scryfall.Interfaces;

public interface IImporter
{
    IList<Card> Import(string path);
}