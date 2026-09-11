using Scriptorium.Mtg.Importer.Models.Card;
using Scriptorium.Mtg.Importer.Models.Catalog;
using Scriptorium.Mtg.Importer.Models.Ruling;
using Scriptorium.Mtg.Importer.Models.Set;

namespace Scriptorium.Mtg.Scryfall.Importer.Interfaces;

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