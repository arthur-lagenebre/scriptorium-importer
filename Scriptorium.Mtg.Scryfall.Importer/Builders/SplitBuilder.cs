using Scriptorium.Mtg.Scryfall.Importer.Interfaces;
using Scriptorium.Mtg.Scryfall.Models;

namespace Scriptorium.Mtg.Scryfall.Importer.Builders;

public class SplitBuilder : BaseScryfallBuilder
{
    public override Layout Layout => new("split");

    public SplitBuilder() : base() { }

    public override IScryfallBuilder AddVanguard(string? handModifier, string? lifeModifier)
    {
        throw new NotSupportedException();
    }
}
