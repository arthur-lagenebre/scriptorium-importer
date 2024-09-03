using MTG.Importer.Models.Card;

namespace MTG.Saver.Interfaces
{
    public interface IWriter
    {
        void WriteCards(IList<Card> cards, string path);
    }
}