namespace MTG.Scryfall.Importer.Interfaces;

public interface IScryfallGetter
{
    StreamReader GetScryfallCardStreamReader();
    StreamReader GetScryfallRulingStreamReader();
    string GetScryfallUrl(string path);
}