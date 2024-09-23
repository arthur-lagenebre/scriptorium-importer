using System.ComponentModel.DataAnnotations.Schema;

namespace MTG.Database.Models.Entities;

public class CardType
{
    [ForeignKey("Card")]
    public Guid OracleId { get; set; }
    public string Name { get; set; }
}
