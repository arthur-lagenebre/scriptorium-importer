using Scriptorium.Mtg.Importer.Models.Card;
using Scriptorium.Mtg.Scryfall.Models.Card;

namespace Scriptorium.Mtg.Scryfall.Importer.Helpers;

public static class RelatedCardHelper
{
    public static List<RelatedCard> CreateRelatedCards(List<ScryfallAllPart> scryfallAllParts, string scryfallId)
    {
        var relatedCards = new List<RelatedCard>();

        foreach (var part in scryfallAllParts)
            if (part.Id != scryfallId)
                relatedCards.Add(new RelatedCard(part.OracleId, EnumHelper.GetRelatedCardComponent(part.Component), part.Name));

        return relatedCards;
    }
}
