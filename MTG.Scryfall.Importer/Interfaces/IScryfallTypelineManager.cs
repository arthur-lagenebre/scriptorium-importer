using MTG.Importer.Models.Card;

namespace MTG.Scryfall.Importer.Interfaces;

public interface IScryfallTypelineManager
{
    Typeline ExtractTypeline(string? typeline);
}