using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace MTG.Database.Models.Entities;

[PrimaryKey("Id")]
public class Color
{
    public int Id { get; set; }
    [Required]
    [MaxLength(1)]
    public string Name { get; set; }
    [Required]
    [MaxLength(100)]
    public string Description { get; set; }
}
