using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MTG.Database.Models.Entities;

[Index(nameof(OracleId), nameof(Language), nameof(FaceId))]
public class CardText
{
    [ForeignKey("Card")]
    public Guid OracleId { get; set; }
    public int FaceId { get; set; }
    public string Language { get; set; }
    public string Value { get; set; }
}
