using Newtonsoft.Json;

namespace MTG.Scryfall.Models.Catalog;

public class ScryfallCatalog
{
    [JsonProperty("data")]
    public List<string>? Data { get; set; }
}
