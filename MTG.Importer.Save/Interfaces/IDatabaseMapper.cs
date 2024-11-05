using MTG.Importer.Models.Card;
using MTG.Importer.Save.Entities;

namespace MTG.Importer.Save.Interfaces;

public interface IDatabaseMapper
{
    CardDto ConvertCard(Card card);
    IList<CardNameDto> ConvertCardNames(Card card);
    IList<CardTextDto> ConvertCardTexts(Card card);
}