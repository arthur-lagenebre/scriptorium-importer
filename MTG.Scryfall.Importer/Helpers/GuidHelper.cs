namespace MTG.Scryfall.Importer.Helpers;

public static class GuidHelper
{
    public static Guid GetGuid(Guid? guid)
    {
        ArgumentNullException.ThrowIfNull(guid, nameof(guid));
        if (guid == Guid.Empty)
            throw new ArgumentNullException(nameof(guid));

        return guid.Value;
    }
}
