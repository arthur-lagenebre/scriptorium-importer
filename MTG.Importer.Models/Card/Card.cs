namespace MTG.Importer.Models.Card;

public record Card(Guid OracleId, Cost Cost, Name Name, Text Text, Typeline Typeline, Color Colors, Color ColorsIdentity, Color ColorsIndicator, string Layout, List<string> Keyword, List<string> ProducedMana, Set Set, List<Face> CardFaces, List<RelatedCard> RelatedCards, Creature? Creature, Planeswalker? Planeswalker, Vanguard? Vanguard);
