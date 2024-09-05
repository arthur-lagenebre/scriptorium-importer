using MTG.Importer.Models.Card;
using MTG.Scryfall.Importer.Interfaces;
using MTG.Scryfall.Models.Card;

namespace MTG.Scryfall.Importer;

public class ScryfallDirector : IScryfallDirector
{
    private readonly IEnumerable<IScryfallBuilder> _scryfallBuilders;

    public ScryfallDirector(IEnumerable<IScryfallBuilder> scryfallBuilders) => _scryfallBuilders = scryfallBuilders;

    public Card BuildCard(ScryfallCard scryfallCard)
    {
        var layout = scryfallCard.Layout;
        var builder = _scryfallBuilders.SingleOrDefault(x => x.Layout.Name == layout) ?? throw new NotSupportedException();

        return layout switch
        {
            "vanguard" => BuildVanguard(builder, scryfallCard),
            "saga" => BuildSaga(builder, scryfallCard),
            "transform" => BuildTransform(builder, scryfallCard),
            "normal" => BuildNormal(builder, scryfallCard),
            _ => throw new NotSupportedException(),
        };
    }

    private static Card BuildNormal(IScryfallBuilder builder, ScryfallCard scryfallCard)
    {
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
               .AddRelatedCards(scryfallCard.AllParts)
               .AddCreature(scryfallCard.Power, scryfallCard.Toughness)
               .AddPlaneswalker(scryfallCard.Loyalty);

        return builder.Build();
    }

    private static Card BuildTransform(IScryfallBuilder builder, ScryfallCard scryfallCard)
    {
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
               .AddPlaneswalker(scryfallCard.Loyalty);

        return builder.Build();
    }

    private static Card BuildSaga(IScryfallBuilder builder, ScryfallCard scryfallCard)
    {
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
               .AddRelatedCards(scryfallCard.AllParts);

        return builder.Build();
    }

    private static Card BuildVanguard(IScryfallBuilder builder, ScryfallCard scryfallCard)
    {
        builder.AddOracleId(scryfallCard.OracleId)
               .AddLanguage(scryfallCard.Lang)
               .AddName(scryfallCard.Name, scryfallCard.PrintedName)
               .AddTypeLine(scryfallCard.TypeLine, scryfallCard.PrintedTypeLine)
               .AddText(scryfallCard.OracleText, scryfallCard.PrintedText)
               .AddCost(scryfallCard.ManaCost, scryfallCard.Cmc)
               .AddKeywords(scryfallCard.Keywords)
               .AddProducedMana(scryfallCard.ProducedMana)
               .AddSet(scryfallCard.Set, scryfallCard.Artist, scryfallCard.CollectorNumber, scryfallCard.Rarity, scryfallCard.FlavorText, scryfallCard.FlavorName, scryfallCard.ReleasedAt)
               .AddVanguard(scryfallCard.HandModifier, scryfallCard.LifeModifier);

        return builder.Build();
    }
}
