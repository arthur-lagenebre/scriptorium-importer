namespace MTG.Scryfall.Importer.Helpers;

public static class StringHelper
{
    public static string GetDefaultValue(string? input)
    {
        return input ?? string.Empty;
    }

    public static string GetCleanedValue(string? input)
    {
        return !string.IsNullOrWhiteSpace(input) ? input.Replace("’", "'") : string.Empty;
    }
}
