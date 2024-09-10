using Newtonsoft.Json;

namespace MTG.Scryfall.Models.Symbol;

public class ScryfallSymbol
{
    // The plaintext symbol. Often surrounded with curly braces {}. Note that not all symbols are ASCII text (for example, {∞}).
    [JsonProperty("symbol")]
    public required string Symbol { get; set; }

    // An alternate version of this symbol, if it is possible to write it without curly braces.
    [JsonProperty("loose_variant")]
    public string? LooseVariant { get; set; }

    // An English snippet that describes this symbol. Appropriate for use in alt text or other accessible communication formats.
    [JsonProperty("english")]
    public required string Description { get; set; }

    // True if it is possible to write this symbol “backwards”. For example, the official symbol {U/P} is sometimes written as {P/U} or {P\U} in informal settings. Note that the Scryfall API never writes symbols backwards in other responses. This field is provided for informational purposes.
    [JsonProperty("transposable")]
    public required bool Transposable { get; set; }

    // True if this is a mana symbol.
    [JsonProperty("represents_mana")]
    public required bool RepresentsMana { get; set; }

    // A decimal number representing this symbol’s mana value (also knowns as the converted mana cost). Note that mana symbols from funny sets can have fractional mana values.
    [JsonProperty("mana_value")]
    public decimal? ManaValue { get; set; }

    // True if this symbol appears in a mana cost on any Magic card. For example {20} has this field set to false because {20} only appears in Oracle text, not mana costs.
    [JsonProperty("appears_in_mana_costs")]
    public required bool AppearsInManaCosts { get; set; }

    // True if this symbol is only used on funny cards or Un-cards.
    [JsonProperty("funny")]
    public required bool Funny { get; set; }

    // An array of colors that this symbol represents.
    [JsonProperty("colors")]
    public required List<string> Colors { get; set; }

    // True if the symbol is a hybrid mana symbol. Note that monocolor Phyrexian symbols aren’t considered hybrid.
    [JsonProperty("hybrid")]
    public required bool Hybrid { get; set; }

    // True if the symbol is a Phyrexian mana symbol, i.e. it can be paid with 2 life.
    [JsonProperty("phyrexian")]
    public required bool Phyrexian { get; set; }

    // An array of plaintext versions of this symbol that Gatherer uses on old cards to describe original printed text. For example: {W} has ["oW", "ooW"] as alternates.
    [JsonProperty("gatherer_alternates")]
    public string? GathererAlternates { get; set; }

    // A URI to an SVG image of this symbol on Scryfall’s CDNs.
    [JsonProperty("svg_uri")]
    public string? SvgUri { get; set; }
}
