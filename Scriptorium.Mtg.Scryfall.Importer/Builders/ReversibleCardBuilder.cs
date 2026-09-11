using Scriptorium.Mtg.Scryfall.Importer.Interfaces;
using Scriptorium.Mtg.Scryfall.Models;

namespace Scriptorium.Mtg.Scryfall.Importer.Builders;

public class ReversibleCardBuilder : BaseScryfallBuilder
{
    public override Layout Layout => new("reversible_card");

    public ReversibleCardBuilder() : base() { }

    public override IScryfallBuilder AddVanguard(string? handModifier, string? lifeModifier)
    {
        throw new NotSupportedException();
    }
}
