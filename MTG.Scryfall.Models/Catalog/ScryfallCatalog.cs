using Newtonsoft.Json;

namespace MTG.Scryfall.Models.Catalog;

public class ScryfallCatalog
{
    public class ScryfallSetApiResult
    {
        [JsonProperty("data")]
        public List<string>? Data { get; set; }
    }
}
