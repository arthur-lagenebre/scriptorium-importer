using System.Runtime.Caching;
using MTG.Importer.Models.Catalog;
using MTG.Importer.Save.Interfaces;
using MTG.Scryfall.Importer.Interfaces;

namespace MTG.Scryfall.Importer;

public class TypelineMemoryCache : ITypelineMemoryCache
{
    private MemoryCache _cache = MemoryCache.Default;
    private readonly IDatabaseReader _databaseReader;

    public TypelineMemoryCache(IDatabaseReader databaseReader)
    {
        _databaseReader = databaseReader;
    }

    public IList<CardType> GetTypes()
    {
        if (_cache.Contains("CardType"))
        {
            return (IList<CardType>)_cache.Get("CardType");
        }
        else
        {
            var data = _databaseReader.GetTypes();
            _cache.Add("CardType", data, DateTimeOffset.Now.AddMinutes(30));

            return data ?? [];
        }
    }

    public IList<Supertype> GetSupertypes()
    {
        if (_cache.Contains("Supertype"))
        {
            return (IList<Supertype>)_cache.Get("Supertype");
        }
        else
        {
            var data = _databaseReader.GetSupertypes();
            _cache.Add("Supertype", data, DateTimeOffset.Now.AddMinutes(30));

            return data ?? [];
        }
    }

    public IList<Subtype> GetSubtypes()
    {
        if (_cache.Contains("Subtype"))
        {
            return (IList<Subtype>)_cache.Get("Subtype");
        }
        else
        {
            var data = _databaseReader.GetSubtypes();
            _cache.Add("Subtype", data, DateTimeOffset.Now.AddMinutes(30));

            return data ?? [];
        }
    }
}
