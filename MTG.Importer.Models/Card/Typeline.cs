namespace MTG.Importer.Models.Card;

public record Typeline(List<Guid> Types, List<Guid> Supertypes, List<Guid> Subtypes);
