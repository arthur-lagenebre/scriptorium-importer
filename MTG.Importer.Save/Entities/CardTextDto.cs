namespace MTG.Importer.Save.Entities;

public record CardTextDto(Guid Id, Guid OracleId, int FaceId, string Language, string Value);
