using Scriptorium.Mtg.Importer.Models.Catalog;

namespace Scriptorium.Mtg.Scryfall.Importer.Interfaces
{
    public interface ITypelineMemoryCache
    {
        IList<Subtype> GetSubtypes();
        IList<Supertype> GetSupertypes();
        IList<CardType> GetTypes();
    }
}