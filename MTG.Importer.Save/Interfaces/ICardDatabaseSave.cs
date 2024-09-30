using MTG.Importer.Models.Card;

namespace MTG.Importer.Save.Interfaces
{
    public interface ICardDatabaseSave
    {
        void Save(List<Card> cards);
    }
}