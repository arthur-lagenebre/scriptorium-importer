namespace MTG.Importer.Models;

public record Card
{
    public Guid CardId { get; set; }
    public Guid OracleId { get; set; }
    public string Name { get; set; } = string.Empty;
}
