using MTG.Importer.Models.Card;
using MTG.Importer.Models.Catalog;
using MTG.Importer.Models.Ruling;
using MTG.Importer.Models.Set;

namespace MTG.Scryfall.Importer.Interfaces;

public interface IScryfallImporter
{
    IList<Artist>? ArtistsImport();
    IList<Card>? CardsImport(List<Ruling> rulings);
    IList<Ruling>? RulingsImport();
    IList<Set>? SetsImport();
    IList<Subtype>? SubtypesImport(string cardtype);
    IList<Supertype>? SupertypesImport();
    IList<CardType>? TypesImport();
}