using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MTG.Database.Models.Entities;

[PrimaryKey("Id")]
public class CardSet
{
    public int Id { get; set; }
    [ForeignKey("Card")]
    public Guid OracleId { get; set; }
    public string CollectorNumber { get; set; }
    public string Rarity { get; set; }

    public virtual List<CardSetFace> CardSetFaces { get; set; }
}
