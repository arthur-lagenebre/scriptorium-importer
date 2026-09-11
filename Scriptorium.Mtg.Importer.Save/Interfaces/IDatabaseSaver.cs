using Scriptorium.Mtg.Importer.Models.Card;
using Scriptorium.Mtg.Importer.Models.Catalog;
using Scriptorium.Mtg.Importer.Models.Set;

namespace Scriptorium.Mtg.Importer.Save.Interfaces;

public interface IDatabaseSaver
{
    void SaveArtists(IList<Artist> artists);
    void SaveCards(IList<Card> cards);
    void SaveSets(IList<Set> sets);
    void SaveSubtypes(IList<Subtype> subtypes);
    void SaveSupertypes(IList<Supertype> supertypes);
    void SaveTypes(IList<CardType> supertypes);
}