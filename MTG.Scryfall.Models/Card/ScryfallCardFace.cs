using Newtonsoft.Json;

namespace MTG.Scryfall.Models.Card;

public class ScryfallCardFace
{
    // The Oracle ID of this particular face, if the card is reversible.
    [JsonProperty("oracle_id")]
    public Guid? OracleId { get; set; }

    // The name of this particular face.
    [JsonProperty("name")]
    public required string Name { get; set; }

    // The localized name printed on this face, if any.
    [JsonProperty("printed_name")]
    public string? PrintedName { get; set; }

    // The mana cost for this face. This value will be any empty string "" if the cost is absent.
    // Remember that per the game rules, a missing mana cost and a mana cost of {0} are different values.
    [JsonProperty("mana_cost")]
    public required string ManaCost { get; set; }

    // The mana value of this particular face, if the card is reversible.
    [JsonProperty("cmc")]
    public double ManaValue { get; set; }

    // The type line of this particular face, if the card is reversible.
    [JsonProperty("type_line")]
    public string? TypeLine { get; set; }

    // The localized type line printed on this face, if any.
    [JsonProperty("printed_type_line")]
    public string? PrintedTypeline { get; set; }

    // The Oracle text for this face, if any.
    [JsonProperty("oracle_text")]
    public string? OracleText { get; set; }

    // The localized text printed on this face, if any.
    [JsonProperty("printed_text")]
    public string? PrintedText { get; set; }

    // The name of the illustrator(s) of this card. Newly spoiled cards may not have this field yet.
    [JsonProperty("artist")]
    public string? Artist { get; set; }

    // The IDs of the artists that illustrated this card. Newly spoiled cards may not have this field yet.
    [JsonProperty("artist_ids")]
    public List<Guid>? ArtistIds { get; set; }

    // The just-for-fun name printed on this face, if any (such as for Godzilla series cards).
    [JsonProperty("flavor_name")]
    public string? FlavorName { get; set; }

    // The layout of this card face, if the card is reversible.
    [JsonProperty("layout")]
    public string? Layout { get; set; }

    // This face’s colors, if the game defines colors for the individual face of this card.
    [JsonProperty("colors")]
    public List<string>? Colors { get; set; }

    // This face’s defense, if the game defines colors for the individual face of this card.
    [JsonProperty("defense")]
    public string? Defense { get; set; }

    // This face’s power, if any. Note that some cards have powers that are not numeric, such as *.
    [JsonProperty("power")]
    public string? Power { get; set; }

    // This face’s toughness, if any.
    [JsonProperty("toughness")]
    public string? Toughness { get; set; }

    // This face’s loyalty, if any.
    [JsonProperty("loyalty")]
    public string? Loyalty { get; set; }

    // The colors in this face’s color indicator, if any.
    [JsonProperty("color_indicator")]
    public List<string>? ColorIndicator { get; set; }

    // The flavor text printed on this face, if any.
    [JsonProperty("flavor_text")]
    public string? FlavorText { get; set; }
}
