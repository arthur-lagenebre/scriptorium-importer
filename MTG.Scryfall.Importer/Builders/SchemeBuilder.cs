using MTG.Scryfall.Importer.Interfaces;
using MTG.Scryfall.Models;

namespace MTG.Scryfall.Importer.Builders;

public class SchemeBuilder : BaseScryfallBuilder
{
    public override Layout Layout => new("scheme");

    public SchemeBuilder() : base() { }

    public override IScryfallBuilder AddCreature(string? power, string? toughness)
    {
        throw new NotSupportedException();
    }

    public override IScryfallBuilder AddKeywords(List<string> keywords)
    {
        throw new NotSupportedException();
    }

    public override IScryfallBuilder AddPlaneswalker(string? loyalty)
    {
        throw new NotSupportedException();
    }

    public override IScryfallBuilder AddVanguard(string? handModifier, string? lifeModifier)
    {
        throw new NotSupportedException();
    }
}
