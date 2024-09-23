using Microsoft.EntityFrameworkCore;
using MTG.Database.Models.DatabaseContext;
using MTG.Database.Models.Entities;
using MTG.Database.Models.Interfaces;

namespace MTG.Database.Models.Services;

public class CardTextService : ICardTextService
{
    private readonly MTGDbContext _db;

    public CardTextService(MTGDbContext db)
    {
        _db = db;
    }

    public async Task<CardText> GetCardText(Guid oracleId, string language)
    {
        return await _db.CardTexts.FirstOrDefaultAsync(x => x.OracleId == oracleId && x.Language == language);
    }

    public async Task<CardText> AddCardText(CardText cardText)
    {
        _db.CardTexts.Add(cardText);
        var result = await _db.SaveChangesAsync();

        return result >= 0 ? cardText : null;
    }

    public async Task<bool> DeleteCardText(Guid oracleId, string language)
    {
        var cardtext = await _db.CardTexts.FirstOrDefaultAsync(x => x.OracleId == oracleId && x.Language == language);

        if (cardtext != null)
        {
            _db.CardTexts.Remove(cardtext);

            var result = await _db.SaveChangesAsync();

            return result >= 0;
        }

        return false;
    }
}
