using Scriptorium.Mtg.Importer.Models.Catalog;
using Scriptorium.Mtg.Importer.Models.Set;

namespace Scriptorium.Mtg.Importer.Save.Interfaces;

public interface IDatabaseReader
{
    IList<Artist>? GetArtists();
    IList<Set>? GetSets();
    IList<Subtype>? GetSubtypes();
    IList<Supertype>? GetSupertypes();
    IList<CardType>? GetTypes();
}