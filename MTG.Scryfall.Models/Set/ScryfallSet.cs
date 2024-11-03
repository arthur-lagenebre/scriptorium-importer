using Newtonsoft.Json;

namespace MTG.Scryfall.Models.Set;

public class ScryfallSet
{
    // The unique three to six-letter code for this set.
    [JsonProperty("code")]
    public required string Code { get; set; }

    // The English name of the set.
    [JsonProperty("name")]
    public required string Name { get; set; }

    // The date the set was released or the first card was printed in the set (in GMT-8 Pacific time).
    [JsonProperty("released_at")]
    public string? ReleasedAt { get; set; }

    // The unique three to six-letter code for this set.
    [JsonProperty("set_type")]
    public required string Type { get; set; }

    // The unique three to six-letter code for this set.
    [JsonProperty("digital")]
    public bool Digital { get; set; }

    [JsonProperty("parent_set_code")]
    public string? ParentSetCode { get; set; }

    // The unique three to six-letter code for this set.
    [JsonProperty("block_code")]
    public string? BlockCode { get; set; }

    // The unique three to six-letter code for this set.
    [JsonProperty("block")]
    public string? Block { get; set; }
}
