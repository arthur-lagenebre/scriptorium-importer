using MTG.Scryfall.Importer.Interfaces;
using MTG.Scryfall.Models;
using MTG.Scryfall.Models.Card;

namespace MTG.Scryfall.Importer.Builders;

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
