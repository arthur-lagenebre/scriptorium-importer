namespace Scriptorium.Mtg.Scryfall.Importer.Interfaces;

public interface IScryfallGetter
{
    StreamReader GetScryfallCardStreamReader();
    StreamReader GetScryfallRulingStreamReader();
    string GetScryfallUrl(string path);
}