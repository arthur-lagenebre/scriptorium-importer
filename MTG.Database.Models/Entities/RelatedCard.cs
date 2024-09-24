using System.ComponentModel.DataAnnotations.Schema;

namespace MTG.Database.Models.Entities;

public class RelatedCard
{
    [ForeignKey("Card")]
    public Guid OracleId { get; set; }
    public string Component { get; set; }
    public string Name { get; set; }
    public string TypeLine { get; set; }
}
