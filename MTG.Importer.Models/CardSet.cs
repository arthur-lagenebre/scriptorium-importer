namespace MTG.Importer.Models;

public record CardSet(string Set, string Artist, string CollectorNumber, string Rarity, string FlavorText, string FlavorName, DateTime Released);
