namespace MTG.Importer.Models.Card;

public record Face(int FaceId, Cost Cost, Name Name, Typeline TypeLine, Text Text, Color Colors, Color ColorsIndicator, Creature? Creature, Planeswalker? Planeswalker, Battle? Battle);
