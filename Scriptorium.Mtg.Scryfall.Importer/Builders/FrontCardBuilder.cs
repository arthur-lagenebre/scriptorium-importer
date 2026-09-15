using Scriptorium.Mtg.Scryfall.Importer.Interfaces;
using Scriptorium.Mtg.Scryfall.Models;
using Scriptorium.Mtg.Scryfall.Models.Card;

namespace Scriptorium.Mtg.Scryfall.Importer.Builders;

/// <summary>
/// Carte de couverture de paquet Jumpstart — type « Card », sans coût de
/// mana ni illustrateur crédité. Une seule face, comme une carte normale.
/// </summary>
public class FrontCardBuilder : BaseScryfallBuilder
{
    public override Layout Layout => new("front_card");

    public FrontCardBuilder() : base() { }

    public override IScryfallBuilder AddCardFaces(List<ScryfallCardFace>? scryfallCardFaces)
    {
        throw new NotSupportedException();
    }

    public override IScryfallBuilder AddVanguard(string? handModifier, string? lifeModifier)
    {
        throw new NotSupportedException();
    }
}
