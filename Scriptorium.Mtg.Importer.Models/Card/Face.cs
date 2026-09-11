namespace Scriptorium.Mtg.Importer.Models.Card;

public record Face(int FaceId, Cost Cost, Name Name, Typeline Typeline, Text Text, Color Colors, Color ColorsIndicator, string? Power, string? Toughness, string? Loyalty, int? Defense);
