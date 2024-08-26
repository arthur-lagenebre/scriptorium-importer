using MTG.Importer.Models;

namespace MTG.Scryfall.Importer.Helpers;

public static class ColorHelper
{
    public static CardColor GetCardColor(List<string>? colors)
    {
        if (colors == null || colors.Count == 0)
            return CardColor.None;

        var cardColor = CardColor.Unknown;

        foreach (var color in colors)
            cardColor |= Enum.Parse<CardColor>(color);

        return cardColor;
    }
}
