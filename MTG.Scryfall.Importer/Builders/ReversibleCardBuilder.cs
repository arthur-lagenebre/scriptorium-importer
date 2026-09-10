using MTG.Scryfall.Importer.Interfaces;
using MTG.Scryfall.Models;

namespace MTG.Scryfall.Importer.Builders;

public class ReversibleCardBuilder : BaseScryfallBuilder
{
    public override Layout Layout => new("reversible_card");

    public ReversibleCardBuilder() : base() { }

    public override IScryfallBuilder AddVanguard(string? handModifier, string? lifeModifier)
    {
        throw new NotSupportedException();
    }
}
