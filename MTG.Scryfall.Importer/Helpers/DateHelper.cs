namespace MTG.Scryfall.Importer.Helpers;

public static class DateHelper
{
    public static DateTime GetDate(string? date)
    {
        return string.IsNullOrWhiteSpace(date) ? DateTime.MinValue : DateTime.ParseExact(date, "yyyy-MM-dd", null);
    }
}
