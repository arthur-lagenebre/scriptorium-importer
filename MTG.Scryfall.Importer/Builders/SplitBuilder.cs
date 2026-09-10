using MTG.Scryfall.Importer.Interfaces;
using MTG.Scryfall.Models;

namespace MTG.Scryfall.Importer.Builders;

public class SplitBuilder : BaseScryfallBuilder
{
    public override Layout Layout => new("split");

    public SplitBuilder() : base() { }

    public override IScryfallBuilder AddVanguard(string? handModifier, string? lifeModifier)
    {
        throw new NotSupportedException();
    }
}
