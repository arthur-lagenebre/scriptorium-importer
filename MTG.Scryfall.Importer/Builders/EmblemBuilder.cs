using MTG.Scryfall.Importer.Interfaces;
using MTG.Scryfall.Models;
using MTG.Scryfall.Models.Card;

namespace MTG.Scryfall.Importer.Builders;

public class EmblemBuilder : BaseScryfallBuilder
{
    public override Layout Layout => new("emblem");

    public EmblemBuilder() : base() { }

    public override IScryfallBuilder AddCardFaces(List<ScryfallCardFace>? scryfallCardFaces)
    {
        throw new NotSupportedException();
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
