namespace MTG.Scryfall.Importer.Helpers;

public static class LanguageHelper
{
    private const string _defaultLanguage = "en";

    public static string GetLanguageValue(string language, string defaultValue, string? printedValue)
    {
        if (language.Equals(_defaultLanguage))
            return defaultValue;
        return StringHelper.GetDefaultValue(printedValue);
    }
}
