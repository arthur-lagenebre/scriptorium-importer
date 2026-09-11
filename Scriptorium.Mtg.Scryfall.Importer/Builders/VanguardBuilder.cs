using Scriptorium.Mtg.Scryfall.Importer.Interfaces;
using Scriptorium.Mtg.Scryfall.Models;
using Scriptorium.Mtg.Scryfall.Models.Card;

namespace Scriptorium.Mtg.Scryfall.Importer.Builders;

public class VanguardBuilder : BaseScryfallBuilder
{
    public override Layout Layout => new("vanguard");

    public VanguardBuilder() : base() { }

    public override IScryfallBuilder AddCardFaces(List<ScryfallCardFace>? scryfallCardFaces)
    {
        throw new NotSupportedException();
    }

    public override IScryfallBuilder AddRelatedCards(List<ScryfallAllPart>? scryfallAllParts, string? scryfallId)
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
}
