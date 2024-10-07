using MTG.Scryfall.Importer.Interfaces;

namespace MTG.Scryfall.Importer;

public class ScryfallGetter : IScryfallGetter
{
    public StreamReader GetScryfallStreamReader()
    {
        var path = @"D:\Cards Import\_MTG_\42_cards.json";

        if (string.IsNullOrEmpty(path) || !File.Exists(path))
            throw new FileNotFoundException(path);

        return new StreamReader(path);
    }
}
