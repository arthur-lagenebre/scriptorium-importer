namespace Scriptorium.Mtg.Importer.Save.Entities;

public record CardSetFaceDto(Guid Id, Guid CardSetId, int FaceId, List<Guid>? ArtistsId, List<CardSetFaceFlavorDto> CardSetFaceFlavors);
