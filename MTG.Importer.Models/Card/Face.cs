namespace MTG.Importer.Models.Card;

public record Face(int FaceId, Cost Cost, Name Name, Typeline TypeLine, Text Text, Color Colors, Color ColorsIndicator, string? Power, string? Toughness, string? Loyalty, int? Defense);
