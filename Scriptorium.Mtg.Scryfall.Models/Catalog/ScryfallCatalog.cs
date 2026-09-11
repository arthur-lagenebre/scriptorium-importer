using Newtonsoft.Json;

namespace Scriptorium.Mtg.Scryfall.Models.Catalog;

public class ScryfallCatalog
{
    [JsonProperty("data")]
    public List<string>? Data { get; set; }
}
