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
            "adventure" => BuildAdventure(builder, scryfallCard),
            "case" => BuildCase(builder, scryfallCard),
            "class" => BuildClass(builder, scryfallCard),
            "leveler" => BuildLeveler(builder, scryfallCard),
            "mutate" => BuildMutate(builder, scryfallCard),
            "normal" => BuildNormal(builder, scryfallCard),
            "prototype" => BuildPrototype(builder, scryfallCard),
            "saga" => BuildSaga(builder, scryfallCard),
            "scheme" => BuildScheme(builder, scryfallCard),
            "transform" => BuildTransform(builder, scryfallCard),
            "vanguard" => BuildVanguard(builder, scryfallCard),
            _ => throw new NotSupportedException(),
        };
    }

    private static Card BuildAdventure(IScryfallBuilder builder, ScryfallCard scryfallCard)
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
               .AddRelatedCards(scryfallCard.AllParts, scryfallCard.Id);

        return builder.Build();
    }

    private static Card BuildCase(IScryfallBuilder builder, ScryfallCard scryfallCard)
    {
        builder.AddOracleId(scryfallCard.OracleId)
               .AddLanguage(scryfallCard.Lang)
               .AddName(scryfallCard.Name, scryfallCard.PrintedName)
               .AddTypeLine(scryfallCard.TypeLine, scryfallCard.PrintedTypeLine)
               .AddText(scryfallCard.OracleText, scryfallCard.PrintedText)
               .AddColors(scryfallCard.Colors, scryfallCard.ColorIdentity, scryfallCard.ColorIndicator)
               .AddCost(scryfallCard.ManaCost, scryfallCard.Cmc)
               .AddKeywords(scryfallCard.Keywords)
               .AddSet(scryfallCard.Set, scryfallCard.Artist, scryfallCard.CollectorNumber, scryfallCard.Rarity, scryfallCard.FlavorText, scryfallCard.FlavorName, scryfallCard.ReleasedAt)
               .AddRelatedCards(scryfallCard.AllParts, scryfallCard.Id);

        return builder.Build();
    }

    private static Card BuildClass(IScryfallBuilder builder, ScryfallCard scryfallCard)
    {
        builder.AddOracleId(scryfallCard.OracleId)
               .AddLanguage(scryfallCard.Lang)
               .AddName(scryfallCard.Name, scryfallCard.PrintedName)
               .AddTypeLine(scryfallCard.TypeLine, scryfallCard.PrintedTypeLine)
               .AddText(scryfallCard.OracleText, scryfallCard.PrintedText)
               .AddColors(scryfallCard.Colors, scryfallCard.ColorIdentity, scryfallCard.ColorIndicator)
               .AddCost(scryfallCard.ManaCost, scryfallCard.Cmc)
               .AddKeywords(scryfallCard.Keywords)
               .AddSet(scryfallCard.Set, scryfallCard.Artist, scryfallCard.CollectorNumber, scryfallCard.Rarity, scryfallCard.FlavorText, scryfallCard.FlavorName, scryfallCard.ReleasedAt)
               .AddRelatedCards(scryfallCard.AllParts, scryfallCard.Id);

        return builder.Build();
    }

    private static Card BuildLeveler(IScryfallBuilder builder, ScryfallCard scryfallCard)
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
               .AddRelatedCards(scryfallCard.AllParts, scryfallCard.Id)
               .AddCreature(scryfallCard.Power, scryfallCard.Toughness);

        return builder.Build();
    }

    private static Card BuildMutate(IScryfallBuilder builder, ScryfallCard scryfallCard)
    {
        builder.AddOracleId(scryfallCard.OracleId)
               .AddLanguage(scryfallCard.Lang)
               .AddName(scryfallCard.Name, scryfallCard.PrintedName)
               .AddTypeLine(scryfallCard.TypeLine, scryfallCard.PrintedTypeLine)
               .AddText(scryfallCard.OracleText, scryfallCard.PrintedText)
               .AddColors(scryfallCard.Colors, scryfallCard.ColorIdentity, scryfallCard.ColorIndicator)
               .AddCost(scryfallCard.ManaCost, scryfallCard.Cmc)
               .AddKeywords(scryfallCard.Keywords)
               .AddSet(scryfallCard.Set, scryfallCard.Artist, scryfallCard.CollectorNumber, scryfallCard.Rarity, scryfallCard.FlavorText, scryfallCard.FlavorName, scryfallCard.ReleasedAt)
               .AddRelatedCards(scryfallCard.AllParts, scryfallCard.Id)
               .AddCreature(scryfallCard.Power, scryfallCard.Toughness);

        return builder.Build();
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
               .AddRelatedCards(scryfallCard.AllParts, scryfallCard.Id)
               .AddCreature(scryfallCard.Power, scryfallCard.Toughness)
               .AddPlaneswalker(scryfallCard.Loyalty);

        return builder.Build();
    }

    private static Card BuildPrototype(IScryfallBuilder builder, ScryfallCard scryfallCard)
    {
        builder.AddOracleId(scryfallCard.OracleId)
               .AddLanguage(scryfallCard.Lang)
               .AddName(scryfallCard.Name, scryfallCard.PrintedName)
               .AddTypeLine(scryfallCard.TypeLine, scryfallCard.PrintedTypeLine)
               .AddText(scryfallCard.OracleText, scryfallCard.PrintedText)
               .AddColors(scryfallCard.Colors, scryfallCard.ColorIdentity, scryfallCard.ColorIndicator)
               .AddCost(scryfallCard.ManaCost, scryfallCard.Cmc)
               .AddKeywords(scryfallCard.Keywords)
               .AddSet(scryfallCard.Set, scryfallCard.Artist, scryfallCard.CollectorNumber, scryfallCard.Rarity, scryfallCard.FlavorText, scryfallCard.FlavorName, scryfallCard.ReleasedAt)
               .AddRelatedCards(scryfallCard.AllParts, scryfallCard.Id)
               .AddCreature(scryfallCard.Power, scryfallCard.Toughness);

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
               .AddRelatedCards(scryfallCard.AllParts, scryfallCard.Id)
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
               .AddRelatedCards(scryfallCard.AllParts, scryfallCard.Id);

        return builder.Build();
    }

    private static Card BuildScheme(IScryfallBuilder builder, ScryfallCard scryfallCard)
    {
        builder.AddOracleId(scryfallCard.OracleId)
               .AddLanguage(scryfallCard.Lang)
               .AddName(scryfallCard.Name, scryfallCard.PrintedName)
               .AddTypeLine(scryfallCard.TypeLine, scryfallCard.PrintedTypeLine)
               .AddText(scryfallCard.OracleText, scryfallCard.PrintedText)
               .AddColors(scryfallCard.Colors, scryfallCard.ColorIdentity, scryfallCard.ColorIndicator)
               .AddProducedMana(scryfallCard.ProducedMana)
               .AddSet(scryfallCard.Set, scryfallCard.Artist, scryfallCard.CollectorNumber, scryfallCard.Rarity, scryfallCard.FlavorText, scryfallCard.FlavorName, scryfallCard.ReleasedAt)
               .AddRelatedCards(scryfallCard.AllParts, scryfallCard.Id);

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
