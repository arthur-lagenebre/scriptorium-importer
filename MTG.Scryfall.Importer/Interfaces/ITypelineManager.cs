using MTG.Importer.Models.Card;

namespace MTG.Scryfall.Importer.Interfaces
{
    public interface ITypelineManager
    {
        Typeline ExtractTypeline(string? typeline);
    }
}