using Newtonsoft.Json;

namespace MTG.Scryfall.Models.Card;

public class ScryfallAllPart
{
    // An unique ID for this card in Scryfall’s database.
    [JsonProperty("id")]
    public required string Id { get; set; }

    // A field explaining what role this card plays in this relationship, one of token, meld_part, meld_result, or combo_piece.
    [JsonProperty("component")]
    public required string Component { get; set; }

    // The name of this particular related card.
    [JsonProperty("name")]
    public required string Name { get; set; }

    // The type line of this card.
    [JsonProperty("type_line")]
    public required string TypeLine { get; set; }
}
