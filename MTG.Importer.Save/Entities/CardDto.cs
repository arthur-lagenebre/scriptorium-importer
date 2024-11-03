namespace MTG.Importer.Save.Entities;

public record CardDto(Guid OracleId, string ManaCost, double ManaValue, int Colors, int ColorsIdentity, int ColorsIndicator, string Layout, List<string> Keyword, List<string> ProducedMana, string Power, string Toughness, string Loyalty, string HandModifier, string LifeModifier);
