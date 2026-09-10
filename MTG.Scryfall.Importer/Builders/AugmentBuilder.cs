using MTG.Scryfall.Importer.Interfaces;
using MTG.Scryfall.Models;
using MTG.Scryfall.Models.Card;

namespace MTG.Scryfall.Importer.Builders;

public class AugmentBuilder : BaseScryfallBuilder
{
    public override Layout Layout => new("augment");

    public AugmentBuilder() : base()
    {
    }

    public override IScryfallBuilder AddCardFaces(List<ScryfallCardFace>? scryfallCardFaces)
    {
        throw new NotSupportedException();
    }

    public override IScryfallBuilder AddPlaneswalker(string? loyalty)
    {
        throw new NotSupportedException();
    }

    public override IScryfallBuilder AddProducedMana(List<string>? producedMana)
    {
        throw new NotSupportedException();
    }

    public override IScryfallBuilder AddRelatedCards(List<ScryfallAllPart>? scryfallAllParts, string scryfallId)
    {
        throw new NotSupportedException();
    }

    public override IScryfallBuilder AddVanguard(string? handModifier, string? lifeModifier)
    {
        throw new NotSupportedException();
    }
}
