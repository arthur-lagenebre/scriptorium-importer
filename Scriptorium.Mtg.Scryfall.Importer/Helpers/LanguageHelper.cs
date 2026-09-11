namespace Scriptorium.Mtg.Scryfall.Importer.Helpers;

public static class LanguageHelper
{
    private const string _defaultLanguage = "en";

    public static string GetLanguageValue(string language, string? defaultValue, string? printedValue)
    {
        return language.Equals(_defaultLanguage) ? StringHelper.GetDefaultValue(defaultValue) : StringHelper.GetDefaultValue(printedValue);
    }
}
