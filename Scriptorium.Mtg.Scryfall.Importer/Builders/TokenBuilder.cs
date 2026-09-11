using Scriptorium.Mtg.Scryfall.Importer.Interfaces;
using Scriptorium.Mtg.Scryfall.Models;
using Scriptorium.Mtg.Scryfall.Models.Card;

namespace Scriptorium.Mtg.Scryfall.Importer.Builders;

public class TokenBuilder : BaseScryfallBuilder
{
    public override Layout Layout => new("token");

    public TokenBuilder() : base() { }

    public override IScryfallBuilder AddCardFaces(List<ScryfallCardFace>? scryfallCardFaces)
    {
        throw new NotSupportedException();
    }

    public override IScryfallBuilder AddVanguard(string? handModifier, string? lifeModifier)
    {
        throw new NotSupportedException();
    }
}
