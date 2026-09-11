namespace Scriptorium.Mtg.Importer.Models.Ruling;

public record Ruling(Guid OracleId, string Language, string Rule, DateTime PublishedAt);
