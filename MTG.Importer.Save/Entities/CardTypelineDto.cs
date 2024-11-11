namespace MTG.Importer.Save.Entities;

public record CardTypelineDto(Guid Id, Guid CardId, int FaceId, string Language, string Value);
