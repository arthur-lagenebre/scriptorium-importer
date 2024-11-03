using MTG.Importer.Models.Card;
using MTG.Importer.Models.Set;

namespace MTG.Importer.Save.Interfaces
{
    public interface ICardDatabaseSave
    {
        void SaveCards(IList<Card> cards);
        void SaveSets(IList<Set> sets);
    }
}