using MTG.Importer.Models.Card;
using MTG.Importer.Save.Entities;

namespace MTG.Importer.Save.Interfaces;

public interface IDatabaseMapper
{
    CardDto ConvertCard(Card card);
    List<CardFaceDto> ConvertCardFaces(Card card);
    List<CardNameDto> ConvertCardNames(Card card);
    CardSetDto ConvertCardSet(Card card);
    List<CardSetFaceFlavorDto> ConvertCardSetFaceFlavors(Guid cardSetFaceId, CardSetFace cardSetFace);
    List<CardSetFaceDto> ConvertCardSetFaces(Card card, Guid cardSetId);
    List<CardFaceDto> ConvertCardSetFaces(Card card);
    List<CardTextDto> ConvertCardTexts(Card card);
    List<RelatedCardDto> ConvertRelatedCards(Card card);
    List<CardTypelineDto> ConvertTypelines(Card card);
}