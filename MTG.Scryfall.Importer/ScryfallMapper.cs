using MTG.Importer.Models.Card;
using MTG.Scryfall.Importer.Interfaces;
using MTG.Scryfall.Models.Card;

namespace MTG.Scryfall.Importer;

public class ScryfallMapper : IScryfallMapper
{
    private IScryfallDirector _director;

    public ScryfallMapper(IScryfallDirector director) => _director = director;

    public IList<Card> Map(IList<ScryfallCard> scryfallCards)
    {
        var cards = new List<Card>();

        foreach (var scryfallCard in scryfallCards)
        {
            if (scryfallCard.Digital)
                continue;

            try
            {
                cards.Add(_director.BuildCard(scryfallCard));
            }
            catch (NotSupportedException)
            {
                Console.WriteLine($"Card : {scryfallCard.Name} with layout {scryfallCard.Layout} is not implemented");
            }
        }

        return cards;
    }
}
