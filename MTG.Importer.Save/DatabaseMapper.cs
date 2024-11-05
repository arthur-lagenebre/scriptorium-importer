using MTG.Importer.Models.Card;
using MTG.Importer.Save.Entities;
using MTG.Importer.Save.Interfaces;

namespace MTG.Importer.Save;

public class DatabaseMapper : IDatabaseMapper
{
    public CardDto ConvertCard(Card card)
    {
        return new CardDto(card.OracleId, card.Cost.ManaCost, card.Cost.ManaValue, (int)card.Colors, (int)card.ColorsIdentity, (int)card.ColorsIndicator, card.Layout ?? string.Empty, card.Keyword, card.ProducedMana, card.Power ?? string.Empty, card.Toughness ?? string.Empty, card.Loyalty ?? string.Empty, card.HandModifier ?? string.Empty, card.LifeModifier ?? string.Empty);
    }

    public IList<CardNameDto> ConvertCardNames(Card card)
    {
        var cardNamesDto = new List<CardNameDto>();

        if (card.CardFaces != null && card.CardFaces.Count > 0)
            foreach (var cardFace in card.CardFaces)
                cardNamesDto.Add(new CardNameDto(Guid.NewGuid(), card.OracleId, cardFace.FaceId, cardFace.Name.Language, cardFace.Name.Value));
        else
            cardNamesDto.Add(new CardNameDto(Guid.NewGuid(), card.OracleId, 0, card.Name.Language, card.Name.Value));

        return cardNamesDto;
    }

    public IList<CardTextDto> ConvertCardTexts(Card card)
    {
        var cardTextDto = new List<CardTextDto>();

        if (card.CardFaces != null && card.CardFaces.Count > 0)
            foreach (var cardFace in card.CardFaces)
                cardTextDto.Add(new CardTextDto(Guid.NewGuid(), card.OracleId, cardFace.FaceId, cardFace.Text.Language, cardFace.Text.Value));
        else
            cardTextDto.Add(new CardTextDto(Guid.NewGuid(), card.OracleId, 0, card.Text.Language, card.Text.Value));

        return cardTextDto;
    }
}
