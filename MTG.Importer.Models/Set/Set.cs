namespace MTG.Importer.Models.Set;

public record Set(Guid Id, string Name, string Code, string Type, DateTime ReleasedAt, string Block, string BlockCode, string ParentSetCode);
