using MTG.Importer.Models.Card;
using MTG.Importer.Save.Interfaces;

namespace MTG.Importer.Save;

public class CardDatabaseSave : ICardDatabaseSave
{
    private readonly ICardMapper _cardConverter;

    public CardDatabaseSave(ICardMapper cardConverter) => _cardConverter = cardConverter;

    public void Save(List<Card> cards)
    {
        foreach (Card card in cards)
        {
            var dbCard = _cardConverter.Convert(card);
            Console.WriteLine(dbCard.Names.First().Value);
        }
    }
}
