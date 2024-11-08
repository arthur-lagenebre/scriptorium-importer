namespace MTG.Importer.Save.Entities;

public record CardNameDto(Guid Id, Guid CardId, int FaceId, string Language, string Value);
