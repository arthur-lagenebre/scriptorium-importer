namespace MTG.Scryfall.Importer.Interfaces;

public interface IScryfallGetter
{
    StreamReader GetScryfallCardStreamReader();
    Task<string> GetScryfallSetStreamReader();
}