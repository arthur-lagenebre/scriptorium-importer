using Newtonsoft.Json;

namespace MTG.Scryfall.Models.Set;

public class ScryfallSetApiResult
{
    [JsonProperty("has_more")]
    public bool HasMore { get; set; }

    [JsonProperty("data")]
    public List<ScryfallSet>? Data { get; set; }
}
