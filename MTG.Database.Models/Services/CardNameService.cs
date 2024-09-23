using Microsoft.EntityFrameworkCore;
using MTG.Database.Models.DatabaseContext;
using MTG.Database.Models.Entities;
using MTG.Database.Models.Interfaces;

namespace MTG.Database.Models.Services;

internal class CardNameService : ICardNameService
{
    private readonly MTGDbContext _db;

    public CardNameService(MTGDbContext db)
    {
        _db = db;
    }

    public async Task<CardName> GetCardName(Guid oracleId, string language)
    {
        return await _db.CardNames.FirstOrDefaultAsync(x => x.OracleId == oracleId && x.Language == language);
    }

    public async Task<CardName> AddCardName(CardName cardName)
    {
        _db.CardNames.Add(cardName);
        var result = await _db.SaveChangesAsync();

        return result >= 0 ? cardName : null;
    }

    public async Task<bool> DeleteCardName(Guid oracleId, string language)
    {
        var cardname = await _db.CardNames.FirstOrDefaultAsync(x => x.OracleId == oracleId && x.Language == language);

        if (cardname != null)
        {
            _db.CardNames.Remove(cardname);

            var result = await _db.SaveChangesAsync();

            return result >= 0;
        }

        return false;
    }
}
