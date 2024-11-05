using MTG.Importer.Models.Card;
using MTG.Importer.Models.Catalog;
using MTG.Importer.Models.Set;

namespace MTG.Scryfall.Importer.Interfaces;

public interface IScryfallImporter
{
    IList<Artist>? ArtistsImport(IList<Artist>? artistsDb);
    IList<Card>? CardsImport();
    IList<Set>? SetsImport(IList<Set>? setsDb);
    IList<Subtype>? SubtypesImport(IList<Subtype>? typesDb, string cardtype);
    IList<Supertype>? SupertypesImport(IList<Supertype>? supertypesDb);
    IList<CardType>? TypesImport(IList<CardType>? typesDb);
}