using MTG.Database.Models.Entities;

namespace MTG.Database.Models.Interfaces;

public interface ICardTextService
{
    Task<CardText> AddCardText(CardText cardText);
    Task<bool> DeleteCardText(Guid oracleId, string language);
    Task<CardText> GetCardText(Guid oracleId, string language);
}