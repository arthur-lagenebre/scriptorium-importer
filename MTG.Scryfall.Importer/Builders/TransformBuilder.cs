using MTG.Importer.Models.Card;
using MTG.Scryfall.Importer.Helpers;
using MTG.Scryfall.Importer.Interfaces;
using MTG.Scryfall.Models;

namespace MTG.Scryfall.Importer.Builders;

public class TransformBuilder : BaseScryfallBuilder
{
    public override Layout Layout => new("transform");

    public TransformBuilder() : base() { }

    public override IScryfallBuilder AddSet(Guid setId, string? collectorNumber, string? rarity, List<Guid>? artistsId, List<string> normalImagesUrl, List<string> smallImagesUrl, string? flavorText, string? flavorName)
    {
        var cardSetFaces = new List<CardSetFace>();

        foreach (KeyValuePair<int, Flavor> flavor in _flavors)
            cardSetFaces.Add(new(flavor.Key, artistsId, [flavor.Value]));

        _set = new CardSet(setId, StringHelper.GetDefaultValue(collectorNumber), StringHelper.GetDefaultValue(rarity), normalImagesUrl, smallImagesUrl, cardSetFaces);
        return this;
    }

    public override IScryfallBuilder AddVanguard(string? handModifier, string? lifeModifier)
    {
        throw new NotSupportedException();
    }
}
