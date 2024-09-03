using MTG.Importer.Models.Card;
using MTG.Scryfall.Importer.Helpers;
using MTG.Scryfall.Importer.Interfaces;

namespace MTG.Scryfall.Importer;

public class ScryfallTypelineManager : IScryfallTypelineManager
{
    public const string _typelineSeparator = "—";
    public const string _subtypeSeparator = " ";
    public static readonly char[] _typeSeparators = [' '];
    public static readonly List<string> _subtypes = ["New Phyrexia", "The Abyss", "Serra's Realm", "Bolas's Meditation Realm"];
    public static readonly List<string> _supertypes = ["Basic", "Elite", "Legendary", "Ongoing", "Snow", "Token", "World"];
    public static readonly List<string> _types = ["Artifact", "Battle", "Conspiracy", "Creature", "Emblem", "Enchantment", "Hero", "Instant", "Land", "Phenomenon", "Plane", "Planeswalker", "Scheme", "Sorcery", "Tribal", "Vanguard"];

    public Typeline ExtractTypeline(string? typeline)
    {
        typeline = StringHelper.GetCleanedValue(typeline);

        var types = GetTypes(typeline);
        var supertypes = GetSupertypes(typeline);
        var subtypes = GetSubtypes(typeline);

        return new Typeline(types, supertypes, subtypes);
    }

    private static List<string> GetTypes(string typeline)
    {
        if (!string.IsNullOrEmpty(typeline))
        {
            typeline = typeline.Split(_typelineSeparator)[0].Trim();

            return _types.Intersect(typeline.Split(_typeSeparators), StringComparer.OrdinalIgnoreCase).ToList();
        }

        return [];
    }

    private static List<string> GetSupertypes(string typeline)
    {
        if (!string.IsNullOrEmpty(typeline))
        {
            typeline = typeline.Split(_typelineSeparator)[0].Trim();

            return _supertypes.Intersect(typeline.Split(_typeSeparators), StringComparer.OrdinalIgnoreCase).ToList();
        }

        return [];
    }

    private static List<string> GetSubtypes(string typeline)
    {
        if (!string.IsNullOrEmpty(typeline) && typeline.Contains(_typelineSeparator))
        {
            var subtype = typeline.Split(_typelineSeparator)[1].Trim();

            if (_subtypes.Contains(subtype))
            {
                return [subtype];
            }

            return [.. subtype.Split(_subtypeSeparator)];
        }

        return [];
    }
}
