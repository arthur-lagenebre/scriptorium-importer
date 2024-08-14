using MTG.Importer.Models;
using MTG.Scryfall.Interfaces;
using MTG.Scryfall.Models;

namespace MTG.Scryfall.Importer;

public class Mapper : IMapper
{
    public IList<Card> Map(IList<ScryfallCard> scryfallCards)
    {
        var builder = new NormalBuilder();
        var director = new Director(builder);
        var cards = new List<Card>();

        foreach (var scryfallCard in scryfallCards)
        {
            director.MakeCard(scryfallCard);
            cards.Add(builder.GetCard());
        }

        return cards;
    }
}
