namespace Scriptorium.Mtg.Importer.Models.Card;

public record CardSetFace(int FaceId, List<Guid>? ArtistsId, List<Flavor> Flavors);
