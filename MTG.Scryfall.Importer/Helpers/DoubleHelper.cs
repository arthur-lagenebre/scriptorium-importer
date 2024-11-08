namespace MTG.Scryfall.Importer.Helpers;

public static class DoubleHelper
{
    public static double GetDefaultValue(double manaValue)
    {
        return double.IsNaN(manaValue) ? 0.0 : manaValue;
    }
}
