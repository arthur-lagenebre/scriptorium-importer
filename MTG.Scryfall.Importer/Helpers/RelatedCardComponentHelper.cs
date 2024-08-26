using MTG.Importer.Models;

namespace MTG.Scryfall.Importer.Helpers;

public static class RelatedCardComponentHelper
{
    public static RelatedCardComponent GetRelatedCardComponent(string? relatedCardComponent)
    {
        return relatedCardComponent switch
        {
            "token" => RelatedCardComponent.Token,
            "meld_part" => RelatedCardComponent.MeldPart,
            "meld_result" => RelatedCardComponent.MeldResult,
            "combo_piece" => RelatedCardComponent.ComboPiece,
            _ => RelatedCardComponent.Unknown,
        };
    }
}
