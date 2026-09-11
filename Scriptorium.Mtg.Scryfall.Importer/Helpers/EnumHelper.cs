using Scriptorium.Mtg.Importer.Models.Card;

namespace Scriptorium.Mtg.Scryfall.Importer.Helpers;

public static class EnumHelper
{
    public static RelatedCardComponent GetRelatedCardComponent(string relatedCardComponent)
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
