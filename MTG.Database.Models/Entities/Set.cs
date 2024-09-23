using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace MTG.Database.Models.Entities;

[PrimaryKey("Id")]
public class Set
{
    public int Id { get; set; }
    [Required]
    [MaxLength(6)]
    public string Code { get; set; }
    [Required]
    [MaxLength(150)]
    public string Name { get; set; }
    [Required]
    [MaxLength(50)]
    public string Type { get; set; }
    public DateTime ReleasedAt { get; set; }
    public string BlockCode { get; set; }
    public string Block { get; set; }
    [MaxLength(6)]
    public string ParentSetCode { get; set; }
}
