namespace MTG.Importer.Save.Entities;

public record CardSetFaceDto(Guid Id, Guid CardSetId, int FaceId, List<Guid>? ArtistsId, string FlavorText, string FlavorName);
