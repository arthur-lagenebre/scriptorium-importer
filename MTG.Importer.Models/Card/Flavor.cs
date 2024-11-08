namespace MTG.Importer.Models.Card;

public record Flavor(int FaceId, List<Guid>? ArtistsId, string FlavorText, string FlavorName);
