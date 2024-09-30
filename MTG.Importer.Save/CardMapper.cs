using MTG.Database.Models.Entities;
using MTG.Importer.Save.Interfaces;

namespace MTG.Importer.Save;

public class CardMapper : ICardMapper
{
    public Card Convert(Models.Card.Card card)
    {
        var dbCard = new Card
        {
            OracleId = card.OracleId,
            ManaCost = card.Cost.ManaCost,
            ManaValue = card.Cost.ManaValue,
            Colors = (int)card.Colors,
            ColorsIdentity = (int)card.ColorsIdentity,
            ColorsIndicator = (int)card.ColorsIndicator,
            Layout = card.Layout,
            Keyword = card.Keyword,
            ProducedMana = card.ProducedMana,
            Power = card.Power,
            Toughness = card.Toughness,
            Loyalty = card.Loyalty,
            HandModifier = card.HandModifier,
            LifeModifier = card.LifeModifier,
            Names = [],
            Texts = [],
            Supertypes = [],
            Types = [],
            Subtypes = [],
            Faces = [],
            RelatedCards = [],
            Sets = []
        };

        dbCard.Names.AddRange(GetCardNames(card.OracleId, card.Name, card.CardFaces));
        dbCard.Texts.AddRange(GetCardTexts(card.OracleId, card.Text, card.CardFaces));
        //dbCard.Supertypes;
        //dbCard.Types;
        //dbCard.Subtypes;
        dbCard.Faces.AddRange(GetCardFaces(card.OracleId, card.CardFaces));
        dbCard.RelatedCards.AddRange(GetRelatedCards(card.OracleId, card.RelatedCards));
        //dbCard.Sets;

        return dbCard;
    }

    private static List<CardName> GetCardNames(Guid oracleId, Models.Card.Name name, List<Models.Card.Face> cardFaces)
    {
        var cardNames = new List<CardName>();

        if (cardFaces.Count != 0)
        {
            foreach (var face in cardFaces)
            {
                cardNames.Add(new CardName { FaceId = face.FaceId, OracleId = oracleId, Language = face.Name.Language, Value = face.Name.Value });
            }
        }
        else
        {
            cardNames.Add(new CardName { FaceId = 0, OracleId = oracleId, Language = name.Language, Value = name.Value });
        }

        return cardNames;
    }

    private static List<CardText> GetCardTexts(Guid oracleId, Models.Card.Text text, List<Models.Card.Face> cardFaces)
    {
        var cardTexts = new List<CardText>();

        if (cardFaces.Count != 0)
        {
            foreach (var face in cardFaces)
            {
                cardTexts.Add(new CardText { FaceId = face.FaceId, OracleId = oracleId, Language = face.Text.Language, Value = face.Text.Value });
            }
        }
        else
        {
            cardTexts.Add(new CardText { FaceId = 0, OracleId = oracleId, Language = text.Language, Value = text.Value });
        }
        return cardTexts;

    }

    public static List<CardFace> GetCardFaces(Guid oracleId, List<Models.Card.Face> faces)
    {
        var cardFaces = new List<CardFace>();

        foreach (var face in faces)
        {
            cardFaces.Add(new CardFace { OracleId = oracleId, FaceId = face.FaceId });
        }

        return cardFaces;
    }

    private static List<RelatedCard> GetRelatedCards(Guid oracleId, List<Models.Card.RelatedCard> relatedCards)
    {
        var dbRelatedCards = new List<RelatedCard>();

        foreach (var relatedCard in relatedCards)
        {
            dbRelatedCards.Add(new RelatedCard { OracleId = oracleId, Component = relatedCard.Component.ToString(), Name = relatedCard.Name, TypeLine = relatedCard.TypeLine });
        }

        return dbRelatedCards;
    }
}
