namespace MTG.Importer.Save.Entities;

public record CardSetFaceFlavorDto(Guid Id, Guid CardSetFaceId, string Language, string? FlavorText, string? FlavorName);
