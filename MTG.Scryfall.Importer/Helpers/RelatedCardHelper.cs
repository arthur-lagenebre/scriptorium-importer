using MTG.Importer.Models.Card;
using MTG.Scryfall.Models.Card;

namespace MTG.Scryfall.Importer.Helpers;

public static class RelatedCardHelper
{
    public static List<RelatedCard> CreateRelatedCards(List<ScryfallAllPart> scryfallAllParts, string scryfallId)
    {
        var relatedCards = new List<RelatedCard>();

        foreach (var part in scryfallAllParts)
            if (part.Id != scryfallId)
                relatedCards.Add(new RelatedCard(EnumHelper.GetRelatedCardComponent(part.Component), part.Name, part.TypeLine));

        return relatedCards;
    }
}
