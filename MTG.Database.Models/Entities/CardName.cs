using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MTG.Database.Models.Entities;

[PrimaryKey("Id")]
public class CardName
{
    public int Id { get; set; }
    [ForeignKey("Card")]
    public Guid OracleId { get; set; }
    public string Language { get; set; }
    public string Value { get; set; }
}
