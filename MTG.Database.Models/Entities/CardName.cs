using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MTG.Database.Models.Entities;

[Index(nameof(OracleId), nameof(Language))]
public class CardName
{
    [ForeignKey("Card")]
    public Guid OracleId { get; set; }
    public string Language { get; set; }
    public string Value { get; set; }
}
