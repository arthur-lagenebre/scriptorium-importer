using MTG.Database.Models.Entities;

namespace MTG.Database.Models.Interfaces
{
    internal interface ICardNameService
    {
        Task<CardName> AddCardName(CardName cardName);
        Task<bool> DeleteCardName(Guid oracleId, string language);
        Task<CardName> GetCardName(Guid oracleId, string language);
    }
}