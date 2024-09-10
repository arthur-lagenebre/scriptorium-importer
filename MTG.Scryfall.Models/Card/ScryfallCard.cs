using Newtonsoft.Json;

namespace MTG.Scryfall.Models.Card;

public class ScryfallCard
{
    // An unique ID for this card in Scryfall’s database.
    [JsonProperty("id")]
    public required string Id { get; set; }

    // A unique ID for this card’s oracle identity.
    // This value is consistent across reprinted card editions, and unique among different cards with the same name (tokens, Unstable variants, etc).
    [JsonProperty("oracle_id")]
    public Guid? OracleId { get; set; }

    // The name of this card. If this card has multiple faces, this field will contain both names separated by ␣//␣.
    [JsonProperty("name")]
    public string? Name { get; set; }

    // The localized name printed on this card, if any.
    [JsonProperty("printed_name")]
    public string? PrintedName { get; set; }

    // The Oracle text for this card, if any.
    [JsonProperty("oracle_text")]
    public string? OracleText { get; set; }

    // The localized text printed on this card, if any.
    [JsonProperty("printed_text")]
    public string? PrintedText { get; set; }

    // The type line of this card.
    [JsonProperty("type_line")]
    public string? TypeLine { get; set; }

    // The localized type line printed on this card, if any.
    [JsonProperty("printed_type_line")]
    public string? PrintedTypeLine { get; set; }

    // A language code for this printing.
    [JsonProperty("lang")]
    public ScryfallLanguage Lang { get; set; }

    // The date this card was first released.
    [JsonProperty("released_at")]
    public string? ReleasedAt { get; set; }

    // A code for this card’s layout.
    [JsonProperty("layout")]
    public string? Layout { get; set; }

    // The mana cost for this card. This value will be any empty string "" if the cost is absent.
    // Remember that per the game rules, a missing mana cost and a mana cost of {0} are different values. Multi-faced cards will report this value in card faces.
    [JsonProperty("mana_cost")]
    public string? ManaCost { get; set; }

    // The card’s mana value. Note that some funny cards have fractional mana costs.
    [JsonProperty("cmc")]
    public double ManaValue { get; set; }

    // This card’s power, if any. Note that some cards have powers that are not numeric, such as *.
    [JsonProperty("power")]
    public string? Power { get; set; }

    // This card’s toughness, if any. Note that some cards have toughnesses that are not numeric, such as *.
    [JsonProperty("toughness")]
    public string? Toughness { get; set; }

    // This loyalty if any. Note that some cards have loyalties that are not numeric, such as X.
    [JsonProperty("loyalty")]
    public string? Loyalty { get; set; }

    // This card’s colors, if the overall card has colors defined by the rules. Otherwise the colors will be on the card_faces objects.
    [JsonProperty("colors")]
    public List<string>? Colors { get; set; }

    // This card’s color identity.
    [JsonProperty("color_identity")]
    public List<string>? ColorIdentity { get; set; }

    // The colors in this card’s color indicator, if any. A null value for this field indicates the card does not have one.
    [JsonProperty("color_indicator")]
    public List<string>? ColorIndicator { get; set; }

    // Colors of mana that this card could produce.
    [JsonProperty("produced_mana")]
    public List<string>? ProducedMana { get; set; }

    // An array of keywords that this card uses, such as 'Flying' and 'Cumulative upkeep'.
    [JsonProperty("keywords")]
    public List<string>? Keywords { get; set; }

    // If this card is closely related to other cards, this property will be an array with Related Card Objects.
    [JsonProperty("all_parts")]
    public List<ScryfallAllPart>? AllParts { get; set; }

    // An array of Card Face objects, if this card is multifaced.
    [JsonProperty("card_faces")]
    public List<ScryfallCardFace>? CardFaces { get; set; }

    // The just-for-fun name printed on the card (such as for Godzilla series cards).
    [JsonProperty("flavor_name")]
    public string? FlavorName { get; set; }

    // This card’s set code.
    [JsonProperty("set")]
    public string? Set { get; set; }

    // This card’s collector number. Note that collector numbers can contain non-numeric characters, such as letters or ★.
    [JsonProperty("collector_number")]
    public string? CollectorNumber { get; set; }

    // True if this card was only released in a video game.
    [JsonProperty("digital")]
    public bool Digital { get; set; }

    // This card’s rarity. One of common, uncommon, rare, special, mythic, or bonus.
    [JsonProperty("rarity")]
    public string? Rarity { get; set; }

    // The flavor text, if any.
    [JsonProperty("flavor_text")]
    public string? FlavorText { get; set; }

    // The name of the illustrator(s) of this card. Newly spoiled cards may not have this field yet.
    [JsonProperty("artist")]
    public string? Artist { get; set; }

    // This card’s hand modifier, if it is Vanguard card. This value will contain a delta, such as -1.
    [JsonProperty("hand_modifier")]
    public string? HandModifier { get; set; }

    // This card’s life modifier, if it is Vanguard card. This value will contain a delta, such as +2.
    [JsonProperty("life_modifier")]
    public string? LifeModifier { get; set; }
}
