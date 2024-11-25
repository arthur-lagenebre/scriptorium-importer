using MTG.Importer.Models.Card;
using MTG.Importer.Save.Entities;
using MTG.Importer.Save.Interfaces;
using System.Linq;

namespace MTG.Importer.Save;

public class DatabaseMapper : IDatabaseMapper
{
    public CardDto ConvertCard(Card card)
    {
        var cardNames = ConvertCardNames(card);
        var cardTexts = ConvertCardTexts(card);
        var cardFaces = ConvertCardFaces(card);
        var cardSets = new List<CardSetDto> { ConvertCardSet(card) };
        var cardTypelines = ConvertTypelines(card);
        var relatedCards = ConvertRelatedCards(card);
        var rulings = ConvertRulings(card);

        return new CardDto(card.OracleId, card.Cost.ManaCost, card.Cost.ManaValue, (int)card.Colors, (int)card.ColorsIdentity, (int)card.ColorsIndicator, card.Layout ?? string.Empty, card.Keyword, card.ProducedMana, card.Power ?? string.Empty, card.Toughness ?? string.Empty, card.Loyalty ?? string.Empty, card.HandModifier ?? string.Empty, card.LifeModifier ?? string.Empty, cardNames, cardTexts, cardFaces, cardSets, cardTypelines, relatedCards, rulings);
    }

    private static List<RulingDto> ConvertRulings(Card card)
    {
        return card.Rulings.Select(ruling => new RulingDto(Guid.NewGuid(), card.OracleId, ruling.Language, ruling.Rule, ruling.PublishedAt)).ToList();
    }

    public List<CardFaceDto> ConvertCardSetFaces(Card card)
    {
        return card.CardFaces.Select(cardFace => new CardFaceDto(Guid.NewGuid(), card.OracleId, cardFace.FaceId, cardFace.Cost.ManaCost, cardFace.Cost.ManaValue, (int)cardFace.Colors, (int)cardFace.ColorsIndicator, cardFace.Power ?? string.Empty, cardFace.Toughness ?? string.Empty, cardFace.Loyalty ?? string.Empty, cardFace.Defense)).ToList();
    }

    public List<CardNameDto> ConvertCardNames(Card card)
    {
        var cardNamesDto = new List<CardNameDto>();

        if (card.CardFaces.Count > 0)
            cardNamesDto.AddRange(card.CardFaces.Select(cardFace => new CardNameDto(Guid.NewGuid(), card.OracleId, cardFace.FaceId, cardFace.Name.Language, cardFace.Name.Value)));
        else
            cardNamesDto.Add(new CardNameDto(Guid.NewGuid(), card.OracleId, 0, card.Name.Language, card.Name.Value));

        return cardNamesDto;
    }

    public List<CardTypelineDto> ConvertTypelines(Card card)
    {
        var cardTypelinesDto = new List<CardTypelineDto>();

        if (card.CardFaces.Count > 0)
            cardTypelinesDto.AddRange(card.CardFaces.Select(cardFace => new CardTypelineDto(Guid.NewGuid(), card.OracleId, cardFace.FaceId, cardFace.Typeline.Language, cardFace.Typeline.Value)));
        else
            cardTypelinesDto.Add(new CardTypelineDto(Guid.NewGuid(), card.OracleId, 0, card.Typeline.Language, card.Typeline.Value));

        return cardTypelinesDto;
    }

    public List<CardTextDto> ConvertCardTexts(Card card)
    {
        var cardTextsDto = new List<CardTextDto>();

        if (card.CardFaces.Count > 0)
            cardTextsDto.AddRange(card.CardFaces.Select(cardFace => new CardTextDto(Guid.NewGuid(), card.OracleId, cardFace.FaceId, cardFace.Text.Language, cardFace.Text.Value)));
        else
            cardTextsDto.Add(new CardTextDto(Guid.NewGuid(), card.OracleId, 0, card.Text.Language, card.Text.Value));

        return cardTextsDto;
    }

    public CardSetDto ConvertCardSet(Card card)
    {
        var cardSetId = Guid.NewGuid();
        var cardSetFaces = ConvertCardSetFaces(card, cardSetId);

        return new CardSetDto(cardSetId, card.OracleId, card.Set.SetId, card.Set.CollectorNumber, card.Set.Rarity, cardSetFaces);
    }

    public List<CardSetFaceDto> ConvertCardSetFaces(Card card, Guid cardSetId)
    {
        var cardSetFaces = new List<CardSetFaceDto>();

        foreach (var cardSetFace in card.Set.CardSetFaces)
        {
            var id = Guid.NewGuid();
            var flavors = ConvertCardSetFaceFlavors(id, cardSetFace);

            cardSetFaces.Add(new CardSetFaceDto(id, cardSetId, cardSetFace.FaceId, cardSetFace.ArtistsId, flavors));
        }

        return cardSetFaces;
    }

    public List<CardSetFaceFlavorDto> ConvertCardSetFaceFlavors(Guid cardSetFaceId, CardSetFace cardSetFace)
    {
        return cardSetFace.Flavors.Select(flavor => new CardSetFaceFlavorDto(Guid.NewGuid(), cardSetFaceId, flavor.Language, flavor.FlavorText, flavor.FlavorName)).ToList();
    }

    public List<CardFaceDto> ConvertCardFaces(Card card)
    {
        return card.CardFaces.Select(cardFace => new CardFaceDto(Guid.NewGuid(), card.OracleId, cardFace.FaceId, cardFace.Cost.ManaCost, cardFace.Cost.ManaValue, (int)cardFace.Colors, (int)cardFace.ColorsIndicator, cardFace.Power ?? string.Empty, cardFace.Toughness ?? string.Empty, cardFace.Loyalty ?? string.Empty, cardFace.Defense)).ToList();
    }

    public List<RelatedCardDto> ConvertRelatedCards(Card card)
    {
        var relatedsDto = new List<RelatedCardDto>();

        foreach (var relatedCard in card.RelatedCards)
            if (relatedCard.OracleId.HasValue)
                relatedsDto.Add(new RelatedCardDto(Guid.NewGuid(), card.OracleId, relatedCard.Name, relatedCard.Component.ToString()));

        return relatedsDto;
    }
}
