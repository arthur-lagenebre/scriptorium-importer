using MTG.Database.Models.Entities;

namespace MTG.Database.Models.Interfaces
{
    public interface IColorService
    {
        Task<List<Color>> GetAllColors();
        Task<List<Color>> GetColorsByID(int id);
    }
}