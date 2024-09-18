using Microsoft.EntityFrameworkCore;
using MTG.Database.Models.DatabaseContext;
using MTG.Database.Models.Entities;
using MTG.Database.Models.Interfaces;

namespace MTG.Database.Models.Services;

public class ColorService : IColorService
{
    private readonly MTGDbContext _db;

    public ColorService(MTGDbContext db)
    {
        _db = db;
    }

    public async Task<List<Color>> GetAllColors()
    {
        return await _db.Colors.ToListAsync();
    }

    public async Task<List<Color>> GetColorsByID(int id)
    {
        return await _db.Colors.Where(x => x.Id == id).ToListAsync();
    }
}
