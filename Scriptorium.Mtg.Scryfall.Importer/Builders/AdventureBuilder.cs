using Scriptorium.Mtg.Scryfall.Importer.Interfaces;
using Scriptorium.Mtg.Scryfall.Models;

namespace Scriptorium.Mtg.Scryfall.Importer.Builders;

public class AdventureBuilder : BaseScryfallBuilder
{
    public override Layout Layout => new("adventure");

    public AdventureBuilder() : base()
    {
    }

    public override IScryfallBuilder AddCreature(string? power, string? toughness)
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
