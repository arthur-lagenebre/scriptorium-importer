using MTG.Importer.Models.Card;
using MTG.Importer.Models.Catalog;
using MTG.Importer.Models.Ruling;
using MTG.Importer.Models.Set;

namespace MTG.Importer.Save.Interfaces;

public interface IDatabaseSaver
{
    void SaveArtists(IList<Artist> artists);
    void SaveCards(IList<Card> cards);
    void SaveRulings(IList<Ruling> rulings);
    void SaveSets(IList<Set> sets);
    void SaveSubtypes(IList<Subtype> subtypes);
    void SaveSupertypes(IList<Supertype> supertypes);
    void SaveTypes(IList<CardType> supertypes);
}