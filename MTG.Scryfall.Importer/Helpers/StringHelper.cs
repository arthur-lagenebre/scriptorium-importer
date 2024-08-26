namespace MTG.Scryfall.Importer.Helpers;

public static class StringHelper
{
    public static string GetDefaultValue(string? input)
    {
        return input ?? string.Empty;
    }
}
