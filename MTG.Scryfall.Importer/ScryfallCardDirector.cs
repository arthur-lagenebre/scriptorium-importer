using MTG.Importer.Models.Card;
using MTG.Importer.Models.Ruling;
using MTG.Scryfall.Importer.Interfaces;
using MTG.Scryfall.Models.Card;

namespace MTG.Scryfall.Importer;

public class ScryfallCardDirector : IScryfallCardDirector
{
    private readonly IEnumerable<IScryfallBuilder> _scryfallBuilders;

    public ScryfallCardDirector(IEnumerable<IScryfallBuilder> scryfallBuilders) => _scryfallBuilders = scryfallBuilders;

    public Card BuildCard(ScryfallCard scryfallCard, List<Ruling> rulings)
    {
        var layout = scryfallCard.Layout;
        var builder = _scryfallBuilders.SingleOrDefault(x => x.Layout.Name == layout) ?? throw new NotSupportedException();

        return layout switch
        {
            "adventure" => BuildAdventure(builder, scryfallCard, rulings),
            "augment" => BuildAugment(builder, scryfallCard, rulings),
            "case" => BuildCase(builder, scryfallCard, rulings),
            "class" => BuildClass(builder, scryfallCard, rulings),
            "double_faced_token" => BuildDoubleFacedToken(builder, scryfallCard, rulings),
            "emblem" => BuildEmblem(builder, scryfallCard, rulings),
            "flip" => BuildFlip(builder, scryfallCard, rulings),
            "host" => BuildHost(builder, scryfallCard, rulings),
            "leveler" => BuildLeveler(builder, scryfallCard, rulings),
            "meld" => BuildMeld(builder, scryfallCard, rulings),
            "modal_dfc" => BuildModalDfc(builder, scryfallCard, rulings),
            "mutate" => BuildMutate(builder, scryfallCard, rulings),
            "normal" => BuildNormal(builder, scryfallCard, rulings),
            "planar" => BuildPlanar(builder, scryfallCard, rulings),
            "prototype" => BuildPrototype(builder, scryfallCard, rulings),
            "reversible_card" => BuildReversibleCard(builder, scryfallCard, rulings),
            "saga" => BuildSaga(builder, scryfallCard, rulings),
            "scheme" => BuildScheme(builder, scryfallCard, rulings),
            "split" => BuildSplit(builder, scryfallCard, rulings),
            "token" => BuildToken(builder, scryfallCard, rulings),
            "transform" => BuildTransform(builder, scryfallCard, rulings),
            "vanguard" => BuildVanguard(builder, scryfallCard, rulings),
            _ => throw new NotSupportedException(),
        };
    }

    private static Card BuildAdventure(IScryfallBuilder builder, ScryfallCard scryfallCard, List<Ruling> rulings)
    {
        builder.AddOracleId(scryfallCard.OracleId)
               .AddLanguage(scryfallCard.Lang)
               .AddName(scryfallCard.Name, scryfallCard.PrintedName)
               .AddTypeLine(scryfallCard.TypeLine, scryfallCard.PrintedTypeLine)
               .AddText(scryfallCard.OracleText, scryfallCard.PrintedText)
               .AddColors(scryfallCard.Colors, scryfallCard.ColorIdentity, scryfallCard.ColorIndicator)
               .AddCost(scryfallCard.ManaCost, scryfallCard.ManaValue)
               .AddKeywords(scryfallCard.Keywords)
               .AddReleasedDate(scryfallCard.ReleasedAt)
               .AddProducedMana(scryfallCard.ProducedMana)
               .AddCardFaces(scryfallCard.CardFaces)
               .AddSet(scryfallCard.SetId, scryfallCard.CollectorNumber, scryfallCard.Rarity, scryfallCard.ArtistIds, scryfallCard.FlavorText, scryfallCard.FlavorName)
               .AddRelatedCards(scryfallCard.AllParts, scryfallCard.Id);

        return builder.Build(rulings);
    }

    private static Card BuildAugment(IScryfallBuilder builder, ScryfallCard scryfallCard, List<Ruling> rulings)
    {
        builder.AddOracleId(scryfallCard.OracleId)
               .AddLanguage(scryfallCard.Lang)
               .AddName(scryfallCard.Name, scryfallCard.PrintedName)
               .AddTypeLine(scryfallCard.TypeLine, scryfallCard.PrintedTypeLine)
               .AddText(scryfallCard.OracleText, scryfallCard.PrintedText)
               .AddColors(scryfallCard.Colors, scryfallCard.ColorIdentity, scryfallCard.ColorIndicator)
               .AddCost(scryfallCard.ManaCost, scryfallCard.ManaValue)
               .AddKeywords(scryfallCard.Keywords)
               .AddReleasedDate(scryfallCard.ReleasedAt)
               .AddSet(scryfallCard.SetId, scryfallCard.CollectorNumber, scryfallCard.Rarity, scryfallCard.ArtistIds, scryfallCard.FlavorText, scryfallCard.FlavorName);

        return builder.Build(rulings);
    }

    private static Card BuildCase(IScryfallBuilder builder, ScryfallCard scryfallCard, List<Ruling> rulings)
    {
        builder.AddOracleId(scryfallCard.OracleId)
               .AddLanguage(scryfallCard.Lang)
               .AddName(scryfallCard.Name, scryfallCard.PrintedName)
               .AddTypeLine(scryfallCard.TypeLine, scryfallCard.PrintedTypeLine)
               .AddText(scryfallCard.OracleText, scryfallCard.PrintedText)
               .AddColors(scryfallCard.Colors, scryfallCard.ColorIdentity, scryfallCard.ColorIndicator)
               .AddCost(scryfallCard.ManaCost, scryfallCard.ManaValue)
               .AddKeywords(scryfallCard.Keywords)
               .AddReleasedDate(scryfallCard.ReleasedAt)
               .AddSet(scryfallCard.SetId, scryfallCard.CollectorNumber, scryfallCard.Rarity, scryfallCard.ArtistIds, scryfallCard.FlavorText, scryfallCard.FlavorName)
               .AddRelatedCards(scryfallCard.AllParts, scryfallCard.Id);

        return builder.Build(rulings);
    }

    private static Card BuildClass(IScryfallBuilder builder, ScryfallCard scryfallCard, List<Ruling> rulings)
    {
        builder.AddOracleId(scryfallCard.OracleId)
               .AddLanguage(scryfallCard.Lang)
               .AddName(scryfallCard.Name, scryfallCard.PrintedName)
               .AddTypeLine(scryfallCard.TypeLine, scryfallCard.PrintedTypeLine)
               .AddText(scryfallCard.OracleText, scryfallCard.PrintedText)
               .AddColors(scryfallCard.Colors, scryfallCard.ColorIdentity, scryfallCard.ColorIndicator)
               .AddCost(scryfallCard.ManaCost, scryfallCard.ManaValue)
               .AddKeywords(scryfallCard.Keywords)
               .AddReleasedDate(scryfallCard.ReleasedAt)
               .AddSet(scryfallCard.SetId, scryfallCard.CollectorNumber, scryfallCard.Rarity, scryfallCard.ArtistIds, scryfallCard.FlavorText, scryfallCard.FlavorName)
               .AddRelatedCards(scryfallCard.AllParts, scryfallCard.Id);

        return builder.Build(rulings);
    }

    private static Card BuildDoubleFacedToken(IScryfallBuilder builder, ScryfallCard scryfallCard, List<Ruling> rulings)
    {
        builder.AddOracleId(scryfallCard.OracleId)
               .AddLanguage(scryfallCard.Lang)
               .AddName(scryfallCard.Name, scryfallCard.PrintedName)
               .AddTypeLine(scryfallCard.TypeLine, scryfallCard.PrintedTypeLine)
               .AddText(scryfallCard.OracleText, scryfallCard.PrintedText)
               .AddColors(scryfallCard.Colors, scryfallCard.ColorIdentity, scryfallCard.ColorIndicator)
               .AddCost(scryfallCard.ManaCost, scryfallCard.ManaValue)
               .AddKeywords(scryfallCard.Keywords)
               .AddProducedMana(scryfallCard.ProducedMana)
               .AddCardFaces(scryfallCard.CardFaces)
               .AddReleasedDate(scryfallCard.ReleasedAt)
               .AddSet(scryfallCard.SetId, scryfallCard.CollectorNumber, scryfallCard.Rarity, scryfallCard.ArtistIds, scryfallCard.FlavorText, scryfallCard.FlavorName)
               .AddRelatedCards(scryfallCard.AllParts, scryfallCard.Id)
               .AddCreature(scryfallCard.Power, scryfallCard.Toughness)
               .AddPlaneswalker(scryfallCard.Loyalty);

        return builder.Build(rulings);
    }

    private static Card BuildEmblem(IScryfallBuilder builder, ScryfallCard scryfallCard, List<Ruling> rulings)
    {
        builder.AddOracleId(scryfallCard.OracleId)
               .AddLanguage(scryfallCard.Lang)
               .AddName(scryfallCard.Name, scryfallCard.PrintedName)
               .AddTypeLine(scryfallCard.TypeLine, scryfallCard.PrintedTypeLine)
               .AddText(scryfallCard.OracleText, scryfallCard.PrintedText)
               .AddColors(scryfallCard.Colors, scryfallCard.ColorIdentity, scryfallCard.ColorIndicator)
               .AddCost(scryfallCard.ManaCost, scryfallCard.ManaValue)
               .AddKeywords(scryfallCard.Keywords)
               .AddProducedMana(scryfallCard.ProducedMana)
               .AddReleasedDate(scryfallCard.ReleasedAt)
               .AddSet(scryfallCard.SetId, scryfallCard.CollectorNumber, scryfallCard.Rarity, scryfallCard.ArtistIds, scryfallCard.FlavorText, scryfallCard.FlavorName)
               .AddRelatedCards(scryfallCard.AllParts, scryfallCard.Id);

        return builder.Build(rulings);
    }

    private static Card BuildFlip(IScryfallBuilder builder, ScryfallCard scryfallCard, List<Ruling> rulings)
    {
        builder.AddOracleId(scryfallCard.OracleId)
               .AddLanguage(scryfallCard.Lang)
               .AddName(scryfallCard.Name, scryfallCard.PrintedName)
               .AddTypeLine(scryfallCard.TypeLine, scryfallCard.PrintedTypeLine)
               .AddText(scryfallCard.OracleText, scryfallCard.PrintedText)
               .AddColors(scryfallCard.Colors, scryfallCard.ColorIdentity, scryfallCard.ColorIndicator)
               .AddCost(scryfallCard.ManaCost, scryfallCard.ManaValue)
               .AddKeywords(scryfallCard.Keywords)
               .AddProducedMana(scryfallCard.ProducedMana)
               .AddReleasedDate(scryfallCard.ReleasedAt)
               .AddSet(scryfallCard.SetId, scryfallCard.CollectorNumber, scryfallCard.Rarity, scryfallCard.ArtistIds, scryfallCard.FlavorText, scryfallCard.FlavorName)
               .AddRelatedCards(scryfallCard.AllParts, scryfallCard.Id)
               .AddCreature(scryfallCard.Power, scryfallCard.Toughness);

        return builder.Build(rulings);
    }

    private static Card BuildHost(IScryfallBuilder builder, ScryfallCard scryfallCard, List<Ruling> rulings)
    {
        builder.AddOracleId(scryfallCard.OracleId)
               .AddLanguage(scryfallCard.Lang)
               .AddName(scryfallCard.Name, scryfallCard.PrintedName)
               .AddTypeLine(scryfallCard.TypeLine, scryfallCard.PrintedTypeLine)
               .AddText(scryfallCard.OracleText, scryfallCard.PrintedText)
               .AddColors(scryfallCard.Colors, scryfallCard.ColorIdentity, scryfallCard.ColorIndicator)
               .AddCost(scryfallCard.ManaCost, scryfallCard.ManaValue)
               .AddKeywords(scryfallCard.Keywords)
               .AddReleasedDate(scryfallCard.ReleasedAt)
               .AddSet(scryfallCard.SetId, scryfallCard.CollectorNumber, scryfallCard.Rarity, scryfallCard.ArtistIds, scryfallCard.FlavorText, scryfallCard.FlavorName)
               .AddRelatedCards(scryfallCard.AllParts, scryfallCard.Id);

        return builder.Build(rulings);
    }

    private static Card BuildLeveler(IScryfallBuilder builder, ScryfallCard scryfallCard, List<Ruling> rulings)
    {
        builder.AddOracleId(scryfallCard.OracleId)
               .AddLanguage(scryfallCard.Lang)
               .AddName(scryfallCard.Name, scryfallCard.PrintedName)
               .AddTypeLine(scryfallCard.TypeLine, scryfallCard.PrintedTypeLine)
               .AddText(scryfallCard.OracleText, scryfallCard.PrintedText)
               .AddColors(scryfallCard.Colors, scryfallCard.ColorIdentity, scryfallCard.ColorIndicator)
               .AddCost(scryfallCard.ManaCost, scryfallCard.ManaValue)
               .AddKeywords(scryfallCard.Keywords)
               .AddProducedMana(scryfallCard.ProducedMana)
               .AddReleasedDate(scryfallCard.ReleasedAt)
               .AddSet(scryfallCard.SetId, scryfallCard.CollectorNumber, scryfallCard.Rarity, scryfallCard.ArtistIds, scryfallCard.FlavorText, scryfallCard.FlavorName)
               .AddRelatedCards(scryfallCard.AllParts, scryfallCard.Id)
               .AddCreature(scryfallCard.Power, scryfallCard.Toughness);

        return builder.Build(rulings);
    }

    private static Card BuildMeld(IScryfallBuilder builder, ScryfallCard scryfallCard, List<Ruling> rulings)
    {
        builder.AddOracleId(scryfallCard.OracleId)
               .AddLanguage(scryfallCard.Lang)
               .AddName(scryfallCard.Name, scryfallCard.PrintedName)
               .AddTypeLine(scryfallCard.TypeLine, scryfallCard.PrintedTypeLine)
               .AddText(scryfallCard.OracleText, scryfallCard.PrintedText)
               .AddColors(scryfallCard.Colors, scryfallCard.ColorIdentity, scryfallCard.ColorIndicator)
               .AddCost(scryfallCard.ManaCost, scryfallCard.ManaValue)
               .AddKeywords(scryfallCard.Keywords)
               .AddProducedMana(scryfallCard.ProducedMana)
               .AddCardFaces(scryfallCard.CardFaces)
               .AddReleasedDate(scryfallCard.ReleasedAt)
               .AddSet(scryfallCard.SetId, scryfallCard.CollectorNumber, scryfallCard.Rarity, scryfallCard.ArtistIds, scryfallCard.FlavorText, scryfallCard.FlavorName)
               .AddRelatedCards(scryfallCard.AllParts, scryfallCard.Id)
               .AddCreature(scryfallCard.Power, scryfallCard.Toughness)
               .AddPlaneswalker(scryfallCard.Loyalty);

        return builder.Build(rulings);
    }

    private static Card BuildModalDfc(IScryfallBuilder builder, ScryfallCard scryfallCard, List<Ruling> rulings)
    {
        builder.AddOracleId(scryfallCard.OracleId)
               .AddLanguage(scryfallCard.Lang)
               .AddName(scryfallCard.Name, scryfallCard.PrintedName)
               .AddTypeLine(scryfallCard.TypeLine, scryfallCard.PrintedTypeLine)
               .AddText(scryfallCard.OracleText, scryfallCard.PrintedText)
               .AddColors(scryfallCard.Colors, scryfallCard.ColorIdentity, scryfallCard.ColorIndicator)
               .AddCost(scryfallCard.ManaCost, scryfallCard.ManaValue)
               .AddKeywords(scryfallCard.Keywords)
               .AddProducedMana(scryfallCard.ProducedMana)
               .AddCardFaces(scryfallCard.CardFaces)
               .AddReleasedDate(scryfallCard.ReleasedAt)
               .AddSet(scryfallCard.SetId, scryfallCard.CollectorNumber, scryfallCard.Rarity, scryfallCard.ArtistIds, scryfallCard.FlavorText, scryfallCard.FlavorName)
               .AddRelatedCards(scryfallCard.AllParts, scryfallCard.Id);

        return builder.Build(rulings);
    }

    private static Card BuildMutate(IScryfallBuilder builder, ScryfallCard scryfallCard, List<Ruling> rulings)
    {
        builder.AddOracleId(scryfallCard.OracleId)
               .AddLanguage(scryfallCard.Lang)
               .AddName(scryfallCard.Name, scryfallCard.PrintedName)
               .AddTypeLine(scryfallCard.TypeLine, scryfallCard.PrintedTypeLine)
               .AddText(scryfallCard.OracleText, scryfallCard.PrintedText)
               .AddColors(scryfallCard.Colors, scryfallCard.ColorIdentity, scryfallCard.ColorIndicator)
               .AddCost(scryfallCard.ManaCost, scryfallCard.ManaValue)
               .AddKeywords(scryfallCard.Keywords)
               .AddReleasedDate(scryfallCard.ReleasedAt)
               .AddSet(scryfallCard.SetId, scryfallCard.CollectorNumber, scryfallCard.Rarity, scryfallCard.ArtistIds, scryfallCard.FlavorText, scryfallCard.FlavorName)
               .AddRelatedCards(scryfallCard.AllParts, scryfallCard.Id)
               .AddCreature(scryfallCard.Power, scryfallCard.Toughness);

        return builder.Build(rulings);
    }

    private static Card BuildNormal(IScryfallBuilder builder, ScryfallCard scryfallCard, List<Ruling> rulings)
    {
        builder.AddOracleId(scryfallCard.OracleId)
               .AddLanguage(scryfallCard.Lang)
               .AddName(scryfallCard.Name, scryfallCard.PrintedName)
               .AddTypeLine(scryfallCard.TypeLine, scryfallCard.PrintedTypeLine)
               .AddText(scryfallCard.OracleText, scryfallCard.PrintedText)
               .AddColors(scryfallCard.Colors, scryfallCard.ColorIdentity, scryfallCard.ColorIndicator)
               .AddCost(scryfallCard.ManaCost, scryfallCard.ManaValue)
               .AddKeywords(scryfallCard.Keywords)
               .AddProducedMana(scryfallCard.ProducedMana)
               .AddReleasedDate(scryfallCard.ReleasedAt)
               .AddSet(scryfallCard.SetId, scryfallCard.CollectorNumber, scryfallCard.Rarity, scryfallCard.ArtistIds, scryfallCard.FlavorText, scryfallCard.FlavorName)
               .AddRelatedCards(scryfallCard.AllParts, scryfallCard.Id)
               .AddCreature(scryfallCard.Power, scryfallCard.Toughness)
               .AddPlaneswalker(scryfallCard.Loyalty);

        return builder.Build(rulings);
    }

    private static Card BuildPlanar(IScryfallBuilder builder, ScryfallCard scryfallCard, List<Ruling> rulings)
    {
        builder.AddOracleId(scryfallCard.OracleId)
               .AddLanguage(scryfallCard.Lang)
               .AddName(scryfallCard.Name, scryfallCard.PrintedName)
               .AddTypeLine(scryfallCard.TypeLine, scryfallCard.PrintedTypeLine)
               .AddText(scryfallCard.OracleText, scryfallCard.PrintedText)
               .AddColors(scryfallCard.Colors, scryfallCard.ColorIdentity, scryfallCard.ColorIndicator)
               .AddCost(scryfallCard.ManaCost, scryfallCard.ManaValue)
               .AddKeywords(scryfallCard.Keywords)
               .AddProducedMana(scryfallCard.ProducedMana)
               .AddReleasedDate(scryfallCard.ReleasedAt)
               .AddSet(scryfallCard.SetId, scryfallCard.CollectorNumber, scryfallCard.Rarity, scryfallCard.ArtistIds, scryfallCard.FlavorText, scryfallCard.FlavorName)
               .AddRelatedCards(scryfallCard.AllParts, scryfallCard.Id);

        return builder.Build(rulings);
    }

    private static Card BuildPrototype(IScryfallBuilder builder, ScryfallCard scryfallCard, List<Ruling> rulings)
    {
        builder.AddOracleId(scryfallCard.OracleId)
               .AddLanguage(scryfallCard.Lang)
               .AddName(scryfallCard.Name, scryfallCard.PrintedName)
               .AddTypeLine(scryfallCard.TypeLine, scryfallCard.PrintedTypeLine)
               .AddText(scryfallCard.OracleText, scryfallCard.PrintedText)
               .AddColors(scryfallCard.Colors, scryfallCard.ColorIdentity, scryfallCard.ColorIndicator)
               .AddCost(scryfallCard.ManaCost, scryfallCard.ManaValue)
               .AddKeywords(scryfallCard.Keywords)
               .AddReleasedDate(scryfallCard.ReleasedAt)
               .AddSet(scryfallCard.SetId, scryfallCard.CollectorNumber, scryfallCard.Rarity, scryfallCard.ArtistIds, scryfallCard.FlavorText, scryfallCard.FlavorName)
               .AddRelatedCards(scryfallCard.AllParts, scryfallCard.Id)
               .AddCreature(scryfallCard.Power, scryfallCard.Toughness);

        return builder.Build(rulings);
    }

    private static Card BuildReversibleCard(IScryfallBuilder builder, ScryfallCard scryfallCard, List<Ruling> rulings)
    {
        builder.AddOracleId(scryfallCard.CardFaces[0].OracleId)
               .AddLanguage(scryfallCard.Lang)
               .AddName(scryfallCard.Name, scryfallCard.PrintedName)
               .AddTypeLine(scryfallCard.TypeLine, scryfallCard.PrintedTypeLine)
               .AddText(scryfallCard.OracleText, scryfallCard.PrintedText)
               .AddColors(scryfallCard.Colors, scryfallCard.ColorIdentity, scryfallCard.ColorIndicator)
               .AddCost(scryfallCard.ManaCost, scryfallCard.ManaValue)
               .AddKeywords(scryfallCard.Keywords)
               .AddProducedMana(scryfallCard.ProducedMana)
               .AddCardFaces(scryfallCard.CardFaces)
               .AddReleasedDate(scryfallCard.ReleasedAt)
               .AddSet(scryfallCard.SetId, scryfallCard.CollectorNumber, scryfallCard.Rarity, scryfallCard.ArtistIds, scryfallCard.FlavorText, scryfallCard.FlavorName)
               .AddRelatedCards(scryfallCard.AllParts, scryfallCard.Id)
               .AddCreature(scryfallCard.Power, scryfallCard.Toughness)
               .AddPlaneswalker(scryfallCard.Loyalty);

        return builder.Build(rulings);
    }

    private static Card BuildSplit(IScryfallBuilder builder, ScryfallCard scryfallCard, List<Ruling> rulings)
    {
        builder.AddOracleId(scryfallCard.OracleId)
               .AddLanguage(scryfallCard.Lang)
               .AddName(scryfallCard.Name, scryfallCard.PrintedName)
               .AddTypeLine(scryfallCard.TypeLine, scryfallCard.PrintedTypeLine)
               .AddText(scryfallCard.OracleText, scryfallCard.PrintedText)
               .AddColors(scryfallCard.Colors, scryfallCard.ColorIdentity, scryfallCard.ColorIndicator)
               .AddCost(scryfallCard.ManaCost, scryfallCard.ManaValue)
               .AddKeywords(scryfallCard.Keywords)
               .AddProducedMana(scryfallCard.ProducedMana)
               .AddCardFaces(scryfallCard.CardFaces)
               .AddReleasedDate(scryfallCard.ReleasedAt)
               .AddSet(scryfallCard.SetId, scryfallCard.CollectorNumber, scryfallCard.Rarity, scryfallCard.ArtistIds, scryfallCard.FlavorText, scryfallCard.FlavorName)
               .AddRelatedCards(scryfallCard.AllParts, scryfallCard.Id)
               .AddCreature(scryfallCard.Power, scryfallCard.Toughness)
               .AddPlaneswalker(scryfallCard.Loyalty);

        return builder.Build(rulings);
    }

    private static Card BuildSaga(IScryfallBuilder builder, ScryfallCard scryfallCard, List<Ruling> rulings)
    {
        builder.AddOracleId(scryfallCard.OracleId)
               .AddLanguage(scryfallCard.Lang)
               .AddName(scryfallCard.Name, scryfallCard.PrintedName)
               .AddTypeLine(scryfallCard.TypeLine, scryfallCard.PrintedTypeLine)
               .AddText(scryfallCard.OracleText, scryfallCard.PrintedText)
               .AddColors(scryfallCard.Colors, scryfallCard.ColorIdentity, scryfallCard.ColorIndicator)
               .AddCost(scryfallCard.ManaCost, scryfallCard.ManaValue)
               .AddKeywords(scryfallCard.Keywords)
               .AddProducedMana(scryfallCard.ProducedMana)
               .AddReleasedDate(scryfallCard.ReleasedAt)
               .AddSet(scryfallCard.SetId, scryfallCard.CollectorNumber, scryfallCard.Rarity, scryfallCard.ArtistIds, scryfallCard.FlavorText, scryfallCard.FlavorName)
               .AddRelatedCards(scryfallCard.AllParts, scryfallCard.Id);

        return builder.Build(rulings);
    }

    private static Card BuildScheme(IScryfallBuilder builder, ScryfallCard scryfallCard, List<Ruling> rulings)
    {
        builder.AddOracleId(scryfallCard.OracleId)
               .AddLanguage(scryfallCard.Lang)
               .AddName(scryfallCard.Name, scryfallCard.PrintedName)
               .AddTypeLine(scryfallCard.TypeLine, scryfallCard.PrintedTypeLine)
               .AddText(scryfallCard.OracleText, scryfallCard.PrintedText)
               .AddColors(scryfallCard.Colors, scryfallCard.ColorIdentity, scryfallCard.ColorIndicator)
               .AddCost(scryfallCard.ManaCost, scryfallCard.ManaValue)
               .AddProducedMana(scryfallCard.ProducedMana)
               .AddReleasedDate(scryfallCard.ReleasedAt)
               .AddSet(scryfallCard.SetId, scryfallCard.CollectorNumber, scryfallCard.Rarity, scryfallCard.ArtistIds, scryfallCard.FlavorText, scryfallCard.FlavorName)
               .AddRelatedCards(scryfallCard.AllParts, scryfallCard.Id);

        return builder.Build(rulings);
    }

    private static Card BuildToken(IScryfallBuilder builder, ScryfallCard scryfallCard, List<Ruling> rulings)
    {
        builder.AddOracleId(scryfallCard.OracleId)
               .AddLanguage(scryfallCard.Lang)
               .AddName(scryfallCard.Name, scryfallCard.PrintedName)
               .AddTypeLine(scryfallCard.TypeLine, scryfallCard.PrintedTypeLine)
               .AddText(scryfallCard.OracleText, scryfallCard.PrintedText)
               .AddColors(scryfallCard.Colors, scryfallCard.ColorIdentity, scryfallCard.ColorIndicator)
               .AddCost(scryfallCard.ManaCost, scryfallCard.ManaValue)
               .AddKeywords(scryfallCard.Keywords)
               .AddProducedMana(scryfallCard.ProducedMana)
               .AddReleasedDate(scryfallCard.ReleasedAt)
               .AddSet(scryfallCard.SetId, scryfallCard.CollectorNumber, scryfallCard.Rarity, scryfallCard.ArtistIds, scryfallCard.FlavorText, scryfallCard.FlavorName)
               .AddRelatedCards(scryfallCard.AllParts, scryfallCard.Id)
               .AddCreature(scryfallCard.Power, scryfallCard.Toughness);

        return builder.Build(rulings);
    }

    private static Card BuildTransform(IScryfallBuilder builder, ScryfallCard scryfallCard, List<Ruling> rulings)
    {
        builder.AddOracleId(scryfallCard.OracleId)
               .AddLanguage(scryfallCard.Lang)
               .AddName(scryfallCard.Name, scryfallCard.PrintedName)
               .AddTypeLine(scryfallCard.TypeLine, scryfallCard.PrintedTypeLine)
               .AddText(scryfallCard.OracleText, scryfallCard.PrintedText)
               .AddColors(scryfallCard.Colors, scryfallCard.ColorIdentity, scryfallCard.ColorIndicator)
               .AddCost(scryfallCard.ManaCost, scryfallCard.ManaValue)
               .AddKeywords(scryfallCard.Keywords)
               .AddProducedMana(scryfallCard.ProducedMana)
               .AddCardFaces(scryfallCard.CardFaces)
               .AddReleasedDate(scryfallCard.ReleasedAt)
               .AddSet(scryfallCard.SetId, scryfallCard.CollectorNumber, scryfallCard.Rarity, scryfallCard.ArtistIds, scryfallCard.FlavorText, scryfallCard.FlavorName)
               .AddRelatedCards(scryfallCard.AllParts, scryfallCard.Id)
               .AddCreature(scryfallCard.Power, scryfallCard.Toughness)
               .AddPlaneswalker(scryfallCard.Loyalty);

        return builder.Build(rulings);
    }

    private static Card BuildVanguard(IScryfallBuilder builder, ScryfallCard scryfallCard, List<Ruling> rulings)
    {
        builder.AddOracleId(scryfallCard.OracleId)
               .AddLanguage(scryfallCard.Lang)
               .AddName(scryfallCard.Name, scryfallCard.PrintedName)
               .AddTypeLine(scryfallCard.TypeLine, scryfallCard.PrintedTypeLine)
               .AddText(scryfallCard.OracleText, scryfallCard.PrintedText)
               .AddColors(scryfallCard.Colors, scryfallCard.ColorIdentity, scryfallCard.ColorIndicator)
               .AddCost(scryfallCard.ManaCost, scryfallCard.ManaValue)
               .AddKeywords(scryfallCard.Keywords)
               .AddProducedMana(scryfallCard.ProducedMana)
               .AddReleasedDate(scryfallCard.ReleasedAt)
               .AddSet(scryfallCard.SetId, scryfallCard.CollectorNumber, scryfallCard.Rarity, scryfallCard.ArtistIds, scryfallCard.FlavorText, scryfallCard.FlavorName)
               .AddVanguard(scryfallCard.HandModifier, scryfallCard.LifeModifier);

        return builder.Build(rulings);
    }
}
