namespace MTG.Importer.Models.Card;

public record RelatedCard(Guid? OracleId, RelatedCardComponent Component, string Name, string TypeLine);
