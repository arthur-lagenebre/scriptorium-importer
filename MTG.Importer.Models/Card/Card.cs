namespace MTG.Importer.Models.Card;

public record Card(Guid OracleId, Name Name, Typeline Typeline, Text Text, Cost Cost, Color Colors, Color ColorsIdentity, Color ColorsIndicator, string Layout, List<string> Keyword, List<string> ProducedMana, Set Set, List<Face> CardFaces, List<RelatedCard> RelatedCards, string? Power, string? Toughness, string? Loyalty, string? HandModifier, string? LifeModifier);
