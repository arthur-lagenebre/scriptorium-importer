using Microsoft.EntityFrameworkCore;

namespace MTG.Database.Models.Entities;

[PrimaryKey("Id")]
public class SuperType
{
    public int Id { get; set; }
    public string Name { get; set; }
}
