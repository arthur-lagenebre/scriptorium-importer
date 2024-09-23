using MTG.Database.Models.Entities;

namespace MTG.Database.Models.Interfaces
{
    public interface ICardService
    {
        Task<Card> AddCard(Card card);
        Task<bool> DeleteCard(Guid oracleId);
        Task<Card> GetCard(Guid oracleId);
    }
}