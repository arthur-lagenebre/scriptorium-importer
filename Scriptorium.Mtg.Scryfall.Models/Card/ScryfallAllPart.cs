using Newtonsoft.Json;

namespace Scriptorium.Mtg.Scryfall.Models.Card;

public class ScryfallAllPart
{
    // An unique ID for this card in Scryfall’s database.
    [JsonProperty("id")]
    public required string Id { get; set; }

    // A unique ID for this card’s oracle identity.
    // This value is consistent across reprinted card editions, and unique among different cards with the same name (tokens, Unstable variants, etc).
    public Guid? OracleId { get; set; }

    // A field explaining what role this card plays in this relationship, one of token, meld_part, meld_result, or combo_piece.
    [JsonProperty("component")]
    public required string Component { get; set; }

    // The name of this particular related card.
    [JsonProperty("name")]
    public required string Name { get; set; }
}
