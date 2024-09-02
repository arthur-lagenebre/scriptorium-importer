using MTG.Importer.Models.Card;

namespace MTG.Saver
{
    public interface IWriter
    {
        void WriteCards(IList<Card> cards, string path);
    }
}