using MTG.Importer.Models.Card;
using MTG.Scryfall.Importer.Helpers;
using MTG.Scryfall.Importer.Interfaces;

namespace MTG.Scryfall.Importer;

public class ScryfallTypelineManager : IScryfallTypelineManager
{
    private readonly ITypelineMemoryCache _typelineMemoryCache;

    public ScryfallTypelineManager(ITypelineMemoryCache typelineMemoryCache)
    {
        _typelineMemoryCache = typelineMemoryCache;
    }

    public Typeline ExtractTypeline(string typeline)
    {
        typeline = StringHelper.GetCleanedValue(typeline);

        var types = GetTypes(typeline);
        var supertypes = GetSupertypes(typeline);
        var subtypes = GetSubtypes(typeline);

        return new Typeline(types, supertypes, subtypes);
    }

    private List<Guid> GetTypes(string typeline)
    {
        if (!string.IsNullOrEmpty(typeline))
        {
            var cardTypes = new List<Guid>();
            var types = _typelineMemoryCache.GetTypes();

            if (types != null)
                foreach (var type in types)
                    if (typeline.Contains(type.Name))
                        cardTypes.Add(type.Id);

            return cardTypes;
        }

        return [];
    }

    private List<Guid> GetSupertypes(string typeline)
    {
        if (!string.IsNullOrEmpty(typeline))
        {
            var cardSupertypes = new List<Guid>();
            var supertypes = _typelineMemoryCache.GetSupertypes();

            if (supertypes != null)
                foreach (var supertype in supertypes)
                    if (typeline.Contains(supertype.Name))
                        cardSupertypes.Add(supertype.Id);

            return cardSupertypes;
        }

        return [];
    }

    private List<Guid> GetSubtypes(string typeline)
    {
        if (!string.IsNullOrEmpty(typeline))
        {
            var cardSubtypes = new List<Guid>();
            var subtypes = _typelineMemoryCache.GetSubtypes();

            if (subtypes != null)
                foreach (var subtype in subtypes)
                    if (typeline.Contains(subtype.Name))
                        cardSubtypes.Add(subtype.Id);

            return cardSubtypes;
        }

        return [];
    }
}
