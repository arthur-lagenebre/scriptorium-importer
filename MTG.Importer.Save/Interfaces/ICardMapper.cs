using MTG.Importer.Models.Card;
using MTG.Importer.Save.Entities;

namespace MTG.Importer.Save.Interfaces
{
    public interface ICardMapper
    {
        CardDto Convert(Card card);
    }
}