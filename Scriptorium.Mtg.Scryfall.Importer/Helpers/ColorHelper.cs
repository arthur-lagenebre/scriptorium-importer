using Scriptorium.Mtg.Importer.Models.Card;

namespace Scriptorium.Mtg.Scryfall.Importer.Helpers;

public static class ColorHelper
{
    public static Color GetCardColor(List<string>? colors)
    {
        if (colors == null || colors.Count == 0)
            return Color.None;

        var cardColor = Color.Unknown;

        foreach (var color in colors)
            cardColor |= Enum.Parse<Color>(color);

        return cardColor;
    }
}
