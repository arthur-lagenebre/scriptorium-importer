using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MTG.Database.Models.Entities;

[Index(nameof(OracleId), nameof(FaceId))]
public class CardType
{
    [ForeignKey("Card")]
    public Guid OracleId { get; set; }
    public int FaceId { get; set; }
    public string Name { get; set; }
}
