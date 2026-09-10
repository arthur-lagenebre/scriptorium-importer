using MTG.Scryfall.Importer.Interfaces;
using MTG.Scryfall.Models;

namespace MTG.Scryfall.Importer.Builders;

public class MeldBuilder : BaseScryfallBuilder
{
    public override Layout Layout => new("meld");

    public MeldBuilder() : base() { }

    public override IScryfallBuilder AddVanguard(string? handModifier, string? lifeModifier)
    {
        throw new NotSupportedException();
    }
}
