using MTG.Database.Models.Entities;

namespace MTG.Database.Models.Interfaces;

public interface ISetService
{
    Task<Set> AddCardName(Set set);
    Task<Set> GetSet(int id);
}