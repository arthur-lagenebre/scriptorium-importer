namespace MTG.Importer.Save.Entities;

public record CardNameDto(Guid Id, Guid OracleId, int FaceId, string Language, string Value);
