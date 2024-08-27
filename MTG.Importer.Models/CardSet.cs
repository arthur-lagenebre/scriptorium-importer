namespace MTG.Importer.Models;

public record CardSet(string Set, string CollectorNumber, string Rarity, List<CardFlavor> Flavors, DateTime Released);
