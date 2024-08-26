using MTG.Importer.Models;

namespace MTG.Scryfall.Importer.Interfaces;

public interface IImporter
{
    IList<Card> Import(string path);
}