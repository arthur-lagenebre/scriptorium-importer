using MTG.Importer.Models.Catalog;

namespace MTG.Scryfall.Importer.Interfaces
{
    public interface ITypelineMemoryCache
    {
        IList<Subtype> GetSubtypes();
        IList<Supertype> GetSupertypes();
        IList<CardType> GetTypes();
    }
}