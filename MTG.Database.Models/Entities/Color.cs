using Microsoft.EntityFrameworkCore;

namespace MTG.Database.Models.Entities;

[PrimaryKey("Id")]
public class Color
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}
