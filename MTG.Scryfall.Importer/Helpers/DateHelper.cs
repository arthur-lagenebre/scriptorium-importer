namespace MTG.Scryfall.Importer.Helpers;

public static class DateHelper
{
    public static DateTime GetDate(string? date)
    {
        if (string.IsNullOrWhiteSpace(date))
            return DateTime.MinValue;

        return DateTime.ParseExact(date, "yyyy-MM-dd", null);
    }
}
