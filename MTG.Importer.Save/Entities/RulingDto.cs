namespace MTG.Importer.Save.Entities;

public record RulingDto(Guid Id, Guid CardId, string Language, string Rule, DateTime PublishedAt);
