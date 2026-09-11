using Newtonsoft.Json;

namespace Scriptorium.Mtg.Scryfall.Models.Card;

public class ScryfallImageUris
{
    // A small full card image.Designed for use as thumbnail or list icon.
    [JsonProperty("small")]
    public string? Small { get; set; }

    // A medium-sized full card image
    [JsonProperty("normal")]
    public string? Normal { get; set; }
}
