namespace MTG.Importer.Save.Entities;

public record CardSetDto(Guid Id, Guid CardId, Guid SetId, string CollectorNumber, string Rarity, List<string> NormalImagesUrl, List<string> SmallImagesUrl, List<CardSetFaceDto> CardSetFaces);
