using MTG.Importer.Models;
using MTG.Scryfall.Importer.Interfaces;
using MTG.Scryfall.Interfaces;
using MTG.Scryfall.Models;

namespace MTG.Scryfall.Importer;

public class Mapper : IMapper
{
    public IList<Card> Map(IList<ScryfallCard> scryfallCards)
    {
        var cards = new List<Card>();

        foreach (var scryfallCard in scryfallCards)
        {
            if (scryfallCard.Digital)
                continue;

            try
            {
                IBuilder builder = ScryfallBuilder.Create(scryfallCard.Layout);

                builder.AddOracleId(scryfallCard.OracleId)
                       .AddLanguage(scryfallCard.Lang)
                       .AddCardName(scryfallCard.Name, scryfallCard.PrintedName)
                       .AddCardTypeLine(scryfallCard.TypeLine, scryfallCard.PrintedTypeLine)
                       .AddCardText(scryfallCard.OracleText, scryfallCard.PrintedText)
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

                cards.Add(builder.GetCard());
            }
            catch (NotSupportedException)
            {
                Console.WriteLine($"Card : {scryfallCard.Name} with layout {scryfallCard.Layout} is not implemented");
            }
        }

        return cards;
    }
}
