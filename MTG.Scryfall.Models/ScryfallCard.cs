using Newtonsoft.Json;

namespace MTG.Scryfall.Models;

public record ScryfallCard
{
    // A content type for this object, always card.
    [JsonProperty("object")]
    public string? Object { get; set; }
    // This card’s Arena ID, if any. A large percentage of cards are not available on Arena and do not have this ID.
    [JsonProperty("arena_id")]
    public string? ArenaId { get; set; }
    // A unique ID for this card in Scryfall’s database.
    [JsonProperty("id")]
    public string? Id { get; set; }
    // A unique ID for this card’s oracle identity.
    // This value is consistent across reprinted card editions, and unique among different cards with the same name (tokens, Unstable variants, etc).
    [JsonProperty("oracle_id")]
    public Guid? OracleId { get; set; }
    // This card’s multiverse IDs on Gatherer, if any, as an array of integers.Note that Scryfall includes many promo cards, tokens, and other esoteric objects that do not have these identifiers.
    [JsonProperty("multiverse_ids")]
    public List<int>? MultiverseIds { get; set; }
    // This card’s Magic Online ID (also known as the Catalog ID), if any. A large percentage of cards are not available on Magic Online and do not have this ID.
    [JsonProperty("mtgo_id")]
    public int MtgoId { get; set; }
    // This card’s foil Magic Online ID (also known as the Catalog ID), if any. A large percentage of cards are not available on Magic Online and do not have this ID.
    [JsonProperty("mtgo_foil_id")]
    public int MtgoFoilId { get; set; }
    // This card’s ID on TCGplayer’s API, also known as the productId.
    [JsonProperty("tcgplayer_id")]
    public int TcgplayerId { get; set; }
    // This card’s ID on TCGplayer’s API, for its etched version if that version is a separate product.
    [JsonProperty("tcgplayer_etched_id")]
    public int TcgplayerEtchedId { get; set; }
    // This card’s ID on Cardmarket’s API, also known as the idProduct.
    [JsonProperty("cardmarket_id")]
    public int CardmarketId { get; set; }
    // The name of this card. If this card has multiple faces, this field will contain both names separated by ␣//␣.
    [JsonProperty("name")]
    public string? Name { get; set; }
    // The localized name printed on this card, if any.
    [JsonProperty("printed_name")]
    public string? PrintedName { get; set; }
    // The localized text printed on this card, if any.
    [JsonProperty("printed_text")]
    public string? PrintedText { get; set; }
    // The localized type line printed on this card, if any.
    [JsonProperty("printed_type_line")]
    public string? PrintedTypeLine { get; set; }
    // A language code for this printing.
    [JsonProperty("lang")]
    public string? Lang { get; set; }
    // The date this card was first released.
    [JsonProperty("released_at")]
    public string? ReleasedAt { get; set; }
    // A link to this card object on Scryfall’s API.
    [JsonProperty("uri")]
    public string? Uri { get; set; }
    // A link to this card’s permapage on Scryfall’s website.
    [JsonProperty("scryfall_uri")]
    public string? ScryfallUri { get; set; }
    // A code for this card’s layout.
    [JsonProperty("layout")]
    public string? Layout { get; set; }
    // True if this card’s imagery is high resolution.
    [JsonProperty("highres_image")]
    public bool HighresImage { get; set; }
    // A computer-readable indicator for the state of this card’s image, one of missing, placeholder, lowres, or highres_scan.
    [JsonProperty("image_status")]
    public string? ImageStatus { get; set; }
    // An object listing available imagery for this card. See the Card Imagery article for more information.
    [JsonProperty("image_uris")]
    public ScryfallImageUris? ImageUris { get; set; }
    // The mana cost for this card. This value will be any empty string "" if the cost is absent.
    // Remember that per the game rules, a missing mana cost and a mana cost of {0} are different values. Multi-faced cards will report this value in card faces.
    [JsonProperty("mana_cost")]
    public string? ManaCost { get; set; }
    // The card’s mana value. Note that some funny cards have fractional mana costs.
    [JsonProperty("cmc")]
    public double Cmc { get; set; }
    // The type line of this card.
    [JsonProperty("type_line")]
    public string? TypeLine { get; set; }
    // The Oracle text for this card, if any.
    [JsonProperty("oracle_text")]
    public string? OracleText { get; set; }
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
    // An object describing the legality of this card across play formats. Possible legalities are legal, not_legal, restricted, and banned.
    [JsonProperty("legalities")]
    public ScryfallLegalities? Legalities { get; set; }
    // A list of games that this card print is available in, paper, arena, and/or mtgo.
    [JsonProperty("games")]
    public List<string>? Games { get; set; }
    // True if this card is on the Reserved List.
    [JsonProperty("reserved")]
    public bool Reserved { get; set; }
    [JsonProperty("foil")]
    public bool Foil { get; set; }
    [JsonProperty("nonfoil")]
    public bool Nonfoil { get; set; }
    // The just-for-fun name printed on the card (such as for Godzilla series cards).
    [JsonProperty("flavor_name")]
    public string? FlavorName { get; set; }
    // An array of computer-readable flags that indicate if this card can come in foil, nonfoil, or etched finishes.
    [JsonProperty("finishes")]
    public List<string>? Finishes { get; set; }
    // True if this card is oversized.
    [JsonProperty("oversized")]
    public bool Oversized { get; set; }
    // True if this card is a promotional print.
    [JsonProperty("promo")]
    public bool Promo { get; set; }
    // True if this card is a reprint.
    [JsonProperty("reprint")]
    public bool Reprint { get; set; }
    // Whether this card is a variation of another printing.
    [JsonProperty("variation")]
    public bool Variation { get; set; }
    // The printing ID of the printing this card is a variation of.
    [JsonProperty("variation_of")]
    public string? VariationOf { get; set; }
    // This card’s Set object UUID.
    [JsonProperty("set_id")]
    public string? SetId { get; set; }
    // This card’s set code.
    [JsonProperty("set")]
    public string? Set { get; set; }
    // This card’s full set name.
    [JsonProperty("set_name")]
    public string? SetName { get; set; }
    // The type of set this printing is in.
    [JsonProperty("set_type")]
    public string? SetType { get; set; }
    // A link to this card’s set object on Scryfall’s API.
    [JsonProperty("set_uri")]
    public string? SetUri { get; set; }
    // A link to where you can begin paginating this card’s set on the Scryfall API.
    [JsonProperty("set_search_uri")]
    public string? SetSearchUri { get; set; }
    // The security stamp on this card, if any. One of oval, triangle, acorn, circle, arena, or heart.
    [JsonProperty("security_stamp")]
    public string? SecurityStamp { get; set; }
    // This card’s watermark, if any.
    [JsonProperty("watermark")]
    public string? Watermark { get; set; }
    // A link to this card’s set on Scryfall’s website.
    [JsonProperty("scryfall_set_uri")]
    public string? ScryfallSetUri { get; set; }
    // A link to this card’s rulings list on Scryfall’s API.
    [JsonProperty("rulings_uri")]
    public string? RulingsUri { get; set; }
    // A link to where you can begin paginating all re/prints for this card on Scryfall’s API.
    [JsonProperty("prints_search_uri")]
    public string? PrintsSearchUri { get; set; }
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
    // The Scryfall ID for the card back design present on this card.
    [JsonProperty("card_back_id")]
    public string? CardBackId { get; set; }
    // The name of the illustrator of this card.Newly spoiled cards may not have this field yet.
    [JsonProperty("artist")]
    public string? Artist { get; set; }
    [JsonProperty("artist_ids")]
    public List<string>? ArtistIds { get; set; }
    // A unique identifier for the card artwork that remains consistent across reprints. Newly spoiled cards may not have this field yet.
    [JsonProperty("illustration_id")]
    public string? IllustrationId { get; set; }
    // 	This card’s border color: black, white, borderless, silver, or gold.
    [JsonProperty("border_color")]
    public string? BorderColor { get; set; }
    // This card’s frame layout.
    [JsonProperty("frame")]
    public string? Frame { get; set; }
    // True if this card’s artwork is larger than normal.
    [JsonProperty("full_art")]
    public bool FullArt { get; set; }
    // True if the card is printed without text.
    [JsonProperty("textless")]
    public bool Textless { get; set; }
    // Whether this card is found in boosters.
    [JsonProperty("booster")]
    public bool Booster { get; set; }
    // True if this card is a Story Spotlight.
    [JsonProperty("story_spotlight")]
    public bool StorySpotlight { get; set; }
    // An array of strings describing what categories of promo cards this card falls into.
    [JsonProperty("promo_types")]
    public List<string>? PromoTypes { get; set; }
    // This card’s frame effects, if any.
    [JsonProperty("frame_effects")]
    public List<string>? FrameEffects { get; set; }
    // This card’s overall rank/popularity on EDHREC. Not all cards are ranked.
    [JsonProperty("edhrec_rank")]
    public int EdhrecRank { get; set; }
    // This card’s rank/popularity on Penny Dreadful. Not all cards are ranked.
    [JsonProperty("penny_rank")]
    public int PennyRank { get; set; }
    // This card’s hand modifier, if it is Vanguard card. This value will contain a delta, such as -1.
    [JsonProperty("hand_modifier")]
    public string? HandModifier { get; set; }
    // This card’s life modifier, if it is Vanguard card. This value will contain a delta, such as +2.
    [JsonProperty("life_modifier")]
    public string? LifeModifier { get; set; }
    // True if you should consider avoiding use of this print downstream.
    [JsonProperty("content_warning")]
    public bool ContentWarning { get; set; }
    // An object containing daily price information for this card, including usd, usd_foil, usd_etched, eur, and tix prices, as strings.
    [JsonProperty("prices")]
    public ScryfallPrices? Prices { get; set; }
    // An object providing URIs to this card’s listing on other Magic: The Gathering online resources.
    [JsonProperty("related_uris")]
    public ScryfallRelatedUris? RelatedUris { get; set; }
}
