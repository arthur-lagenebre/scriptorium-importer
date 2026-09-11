namespace Scriptorium.Mtg.Importer.Models.Card;

public record Card(Guid OracleId, Name Name, Typeline Typeline, Text Text, Cost Cost, DateTime ReleasedDate, string Language, Color Colors, Color ColorsIdentity, Color ColorsIndicator, string Layout, List<string> Keyword, List<string> ProducedMana, CardSet Set, List<Face> CardFaces, List<RelatedCard> RelatedCards, List<Ruling.Ruling> Rulings, string? Power, string? Toughness, string? Loyalty, string? HandModifier, string? LifeModifier);
