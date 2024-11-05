using MTG.Importer.Models.Catalog;
using MTG.Importer.Models.Set;
using MTG.Importer.Save.Entities;

namespace MTG.Importer.Save.Interfaces;

public interface ICardDatabaseReader
{
    IList<Artist>? GetArtists();
    CardDto? GetCard(Guid oracleId);
    IList<CardNameDto>? GetCardNames(Guid oracleId);
    IList<CardTextDto>? GetCardTexts(Guid oracleId);
    IList<Set>? GetSets();
    IList<Subtype>? GetSubtypes();
    IList<Supertype>? GetSupertypes();
    IList<CardType>? GetTypes();
}