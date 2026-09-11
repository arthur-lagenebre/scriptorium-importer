namespace Scriptorium.Mtg.Importer.Save.Entities;

public record CardTextDto(Guid Id, Guid CardId, int FaceId, string Language, string Value);
