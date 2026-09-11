namespace Scriptorium.Mtg.Importer.Save.Entities;

public record CardFaceDto(Guid Id, Guid CardId, int FaceId, string ManaCost, double ManaValue, int Colors, int ColorsIndicator, string Power, string Toughness, string Loyalty, int? Defense);
