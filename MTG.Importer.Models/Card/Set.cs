namespace MTG.Importer.Models.Card;

public record Set(string Name, string CollectorNumber, string Rarity, List<Flavor> Flavors);
