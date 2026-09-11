using Newtonsoft.Json;

namespace Scriptorium.Mtg.Scryfall.Models.Set;

public class ScryfallSetApiResult
{
    [JsonProperty("has_more")]
    public bool HasMore { get; set; }

    [JsonProperty("data")]
    public List<ScryfallSet>? Data { get; set; }
}
