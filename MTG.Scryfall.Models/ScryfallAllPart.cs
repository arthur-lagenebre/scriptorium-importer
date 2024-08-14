using Newtonsoft.Json;

namespace MTG.Scryfall.Models;

public record ScryfallAllPart
{
    // A content type for this object, always related_card.
    [JsonProperty("object")]
    public string? Object { get; set; }
    // An unique ID for this card in Scryfall’s database.
    [JsonProperty("id")]
    public string? Id { get; set; }
    // A field explaining what role this card plays in this relationship, one of token, meld_part, meld_result, or combo_piece.
    [JsonProperty("component")]
    public string? Component { get; set; }
    // The name of this particular related card.
    [JsonProperty("name")]
    public string? Name { get; set; }
    // The type line of this card.
    [JsonProperty("type_line")]
    public string? TypeLine { get; set; }
    // A URI where you can retrieve a full object describing this card on Scryfall’s API.
    [JsonProperty("uri")]
    public string? Uri { get; set; }
}
