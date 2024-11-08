using MTG.Importer.Models.Catalog;
using MTG.Importer.Models.Set;

namespace MTG.Importer.Save.Interfaces;

public interface IDatabaseReader
{
    IList<Artist>? GetArtists();
    IList<Set>? GetSets();
    IList<Subtype>? GetSubtypes();
    IList<Supertype>? GetSupertypes();
    IList<CardType>? GetTypes();
}