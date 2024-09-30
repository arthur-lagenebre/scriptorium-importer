using Microsoft.EntityFrameworkCore;
using MTG.Database.Models.DatabaseContext;
using MTG.Database.Models.Entities;
using MTG.Database.Models.Interfaces;

namespace MTG.Database.Models.Services;

public class SetService : ISetService
{
    private readonly MTGDbContext _db;

    public SetService(MTGDbContext db)
    {
        _db = db;
    }

    public async Task<Set> GetSet(int id)
    {
        return await _db.Sets.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Set> AddCardName(Set set)
    {
        _db.Sets.Add(set);
        var result = await _db.SaveChangesAsync();

        return result >= 0 ? set : null;
    }
}
