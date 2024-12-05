namespace MTG.Importer.Models.Card;

public record CardSet(Guid SetId, string CollectorNumber, string Rarity, List<string> NormalImagesUrl, List<string> SmallImageUrl, List<CardSetFace> CardSetFaces);
