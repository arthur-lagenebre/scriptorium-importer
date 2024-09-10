using MTG.Importer.Models.Card;
using MTG.Scryfall.Importer.Helpers;
using MTG.Scryfall.Importer.Interfaces;
using MTG.Scryfall.Models;

namespace MTG.Scryfall.Importer;

public class ScryfallTypelineManager : IScryfallTypelineManager
{
    private readonly TypelineProperties _properties; 

    public ScryfallTypelineManager()
    {
        var subtypes = new List<string> { "New Phyrexia", "The Abyss", "Serra's Realm", "Bolas's Meditation Realm" };
        var supertypes = new List<string> { "Basic", "Host", "Elite", "Legendary", "Ongoing", "Snow", "Token", "World" };
        var types =  new List<string> { "Artifact", "Battle", "Conspiracy", "Creature", "Emblem", "Enchantment", "Hero", "Instant", "Land", "Phenomenon", "Plane", "Planeswalker", "Scheme", "Sorcery", "Tribal", "Vanguard"};
        _properties = new TypelineProperties("-", " ", [' '], subtypes, supertypes, types);
    }

    public Typeline ExtractTypeline(string typeline)
    {
        typeline = StringHelper.GetCleanedValue(typeline);

        var types = GetTypes(typeline);
        var supertypes = GetSupertypes(typeline);
        var subtypes = GetSubtypes(typeline);

        return new Typeline(types, supertypes, subtypes);
    }

    private List<string> GetTypes(string typeline)
    {
        if (!string.IsNullOrEmpty(typeline))
        {
            typeline = typeline.Split(_properties.TypelineSeparator)[0].Trim();

            return _properties.Types.Intersect(typeline.Split(_properties.TypeSeparators), StringComparer.OrdinalIgnoreCase).ToList();
        }

        return [];
    }

    private List<string> GetSupertypes(string typeline)
    {
        if (!string.IsNullOrEmpty(typeline))
        {
            typeline = typeline.Split(_properties.TypelineSeparator)[0].Trim();

            return _properties.Supertypes.Intersect(typeline.Split(_properties.TypeSeparators), StringComparer.OrdinalIgnoreCase).ToList();
        }

        return [];
    }

    private List<string> GetSubtypes(string typeline)
    {
        if (!string.IsNullOrEmpty(typeline) && typeline.Contains(_properties.TypelineSeparator))
        {
            var subtype = typeline.Split(_properties.TypelineSeparator)[1].Trim();

            if (_properties.Subtypes.Contains(subtype))
            {
                return [subtype];
            }

            return [.. subtype.Split(_properties.SubtypeSeparator)];
        }

        return [];
    }
}
