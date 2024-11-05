namespace MTG.Scryfall.Importer.Interfaces;

public interface IScryfallGetter
{
    StreamReader GetScryfallCardStreamReader();
    string GetScryfallUrl(string path);
}