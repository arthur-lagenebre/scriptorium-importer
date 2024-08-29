using MTG.Importer.Models.Card;
using MTG.Scryfall.Importer.Interfaces;
using MTG.Scryfall.Models.Card;

namespace MTG.Scryfall.Importer;

public class ScryfallMapper : IScryfallMapper
{
    private IScryfallBuilderSelector _scryfallSelectorBuilder;

    public ScryfallMapper(IScryfallBuilderSelector scryfallSelectorBuilder)
    {
        _scryfallSelectorBuilder = scryfallSelectorBuilder;
    }

    public IList<Card> Map(IList<ScryfallCard> scryfallCards)
    {
        var cards = new List<Card>();

        foreach (var scryfallCard in scryfallCards)
        {
            if (scryfallCard.Digital)
                continue;

            try
            {
                IScryfallBuilder builder = _scryfallSelectorBuilder.Create(scryfallCard.Layout);

                builder.AddOracleId(scryfallCard.OracleId)
                       .AddLanguage(scryfallCard.Lang)
                       .AddName(scryfallCard.Name, scryfallCard.PrintedName)
                       .AddTypeLine(scryfallCard.TypeLine, scryfallCard.PrintedTypeLine)
                       .AddText(scryfallCard.OracleText, scryfallCard.PrintedText)
                       .AddColors(scryfallCard.Colors, scryfallCard.ColorIdentity, scryfallCard.ColorIndicator)
                       .AddCost(scryfallCard.ManaCost, scryfallCard.Cmc)
                       .AddKeywords(scryfallCard.Keywords)
                       .AddProducedMana(scryfallCard.ProducedMana)
                       .AddSet(scryfallCard.Set, scryfallCard.Artist, scryfallCard.CollectorNumber, scryfallCard.Rarity, scryfallCard.FlavorText, scryfallCard.FlavorName, scryfallCard.ReleasedAt)
                       .AddCardFaces(scryfallCard.CardFaces)
                       .AddRelatedCards(scryfallCard.AllParts)
                       .AddCreature(scryfallCard.Power, scryfallCard.Toughness)
                       .AddPlaneswalker(scryfallCard.Loyalty)
                       .AddVanguard(scryfallCard.HandModifier, scryfallCard.LifeModifier);

                cards.Add(builder.Build());
            }
            catch (NotSupportedException)
            {
                Console.WriteLine($"Card : {scryfallCard.Name} with layout {scryfallCard.Layout} is not implemented");
            }
        }

        return cards;
    }
}
