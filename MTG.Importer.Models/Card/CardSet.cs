namespace MTG.Importer.Models.Card;

public record CardSet(string Name, string CollectorNumber, string Rarity, List<Flavor> Flavors);
