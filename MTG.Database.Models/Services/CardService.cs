using Microsoft.EntityFrameworkCore;
using MTG.Database.Models.DatabaseContext;
using MTG.Database.Models.Entities;
using MTG.Database.Models.Interfaces;

namespace MTG.Database.Models.Services;

public class CardService : ICardService
{
    private readonly MTGDbContext _db;

    public CardService(MTGDbContext db)
    {
        _db = db;
    }

    public async Task<Card> GetCard(Guid oracleId)
    {
        return await _db.Cards.FirstOrDefaultAsync(x => x.OracleId == oracleId);
    }

    public async Task<Card> AddCard(Card card)
    {
        _db.Cards.Add(card);
        var result = await _db.SaveChangesAsync();

        return result >= 0 ? card : null;
    }

    public async Task<bool> DeleteCard(Guid oracleId)
    {
        var card = await _db.Cards.FirstOrDefaultAsync(x => x.OracleId == oracleId);

        if (card != null)
        {
            _db.Cards.Remove(card);

            var result = await _db.SaveChangesAsync();

            return result >= 0;
        }

        return false;
    }
}
