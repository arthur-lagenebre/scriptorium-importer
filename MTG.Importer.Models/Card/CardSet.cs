namespace MTG.Importer.Models.Card;

public record CardSet(Guid SetId, string CollectorNumber, string Rarity, List<Flavor> Flavors);
