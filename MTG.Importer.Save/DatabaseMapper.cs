using MTG.Importer.Models.Card;
using MTG.Importer.Save.Entities;
using MTG.Importer.Save.Interfaces;

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

        return new CardDto(card.OracleId, card.Cost.ManaCost, card.Cost.ManaValue, (int)card.Colors, (int)card.ColorsIdentity, (int)card.ColorsIndicator, card.Layout ?? string.Empty, card.Keyword, card.ProducedMana, card.Power ?? string.Empty, card.Toughness ?? string.Empty, card.Loyalty ?? string.Empty, card.HandModifier ?? string.Empty, card.LifeModifier ?? string.Empty, cardNames, cardTexts, cardTypelines, cardFaces, cardSets, relatedCards);
    }

    public List<CardFaceDto> ConvertCardSetFaces(Card card)
    {
        var cardFacesDto = new List<CardFaceDto>();

        foreach (var cardFace in card.CardFaces)
            cardFacesDto.Add(new CardFaceDto(Guid.NewGuid(), card.OracleId, cardFace.FaceId, cardFace.Cost.ManaCost, cardFace.Cost.ManaValue, (int)cardFace.Colors, (int)cardFace.ColorsIndicator, cardFace.Power ?? string.Empty, cardFace.Toughness ?? string.Empty, cardFace.Loyalty ?? string.Empty, cardFace.Defense));

        return cardFacesDto;
    }

    public List<CardNameDto> ConvertCardNames(Card card)
    {
        var cardNamesDto = new List<CardNameDto>();

        if (card.CardFaces != null && card.CardFaces.Count > 0)
            foreach (var cardFace in card.CardFaces)
                cardNamesDto.Add(new CardNameDto(Guid.NewGuid(), card.OracleId, cardFace.FaceId, cardFace.Name.Language, cardFace.Name.Value));
        else
            cardNamesDto.Add(new CardNameDto(Guid.NewGuid(), card.OracleId, 0, card.Name.Language, card.Name.Value));

        return cardNamesDto;
    }

    public List<CardTypelineDto> ConvertTypelines(Card card)
    {
        var cardTypelinesDto = new List<CardTypelineDto>();

        if (card.CardFaces != null && card.CardFaces.Count > 0)
            foreach (var cardFace in card.CardFaces)
                cardTypelinesDto.Add(new CardTypelineDto(Guid.NewGuid(), card.OracleId, cardFace.FaceId, cardFace.Typeline.Language, cardFace.Typeline.Value));
        else
            cardTypelinesDto.Add(new CardTypelineDto(Guid.NewGuid(), card.OracleId, 0, card.Typeline.Language, card.Typeline.Value));

        return cardTypelinesDto;
    }

    public List<CardTextDto> ConvertCardTexts(Card card)
    {
        var cardTextsDto = new List<CardTextDto>();

        if (card.CardFaces != null && card.CardFaces.Count > 0)
            foreach (var cardFace in card.CardFaces)
                cardTextsDto.Add(new CardTextDto(Guid.NewGuid(), card.OracleId, cardFace.FaceId, cardFace.Text.Language, cardFace.Text.Value));
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
        var cardSetFacesDto = new List<CardSetFaceDto>();

        foreach (var flavor in card.Set.Flavors)
            cardSetFacesDto.Add(new CardSetFaceDto(Guid.NewGuid(), cardSetId, flavor.FaceId, flavor.ArtistsId, flavor.FlavorText, flavor.FlavorName));

        return cardSetFacesDto;
    }

    public List<CardFaceDto> ConvertCardFaces(Card card)
    {
        var cardFacesDto = new List<CardFaceDto>();

        foreach (var cardFace in card.CardFaces)
            cardFacesDto.Add(new CardFaceDto(Guid.NewGuid(), card.OracleId, cardFace.FaceId, cardFace.Cost.ManaCost, cardFace.Cost.ManaValue, (int)cardFace.Colors, (int)cardFace.ColorsIndicator, cardFace.Power ?? string.Empty, cardFace.Toughness ?? string.Empty, cardFace.Loyalty ?? string.Empty, cardFace.Defense));

        return cardFacesDto;
    }

    public List<RelatedCardDto> ConvertRelatedCards(Card card)
    {
        var relatedsDto = new List<RelatedCardDto>();

        foreach (var RelatedCard in card.RelatedCards)
            if (RelatedCard.OracleId.HasValue)
                relatedsDto.Add(new RelatedCardDto(Guid.NewGuid(), card.OracleId, RelatedCard.Name, RelatedCard.Component.ToString()));

        return relatedsDto;
    }
}
