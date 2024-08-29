using Newtonsoft.Json;

namespace MTG.Scryfall.Models.Card;

public class ScryfallImageUris
{
    // A small full card image. Designed for use as thumbnail or list icon.
    [JsonProperty("small")]
    public string? Small { get; set; }

    // A medium-sized full card image
    [JsonProperty("normal")]
    public string? Normal { get; set; }

    // A large full card image
    [JsonProperty("large")]
    public string? Large { get; set; }

    // A transparent, rounded full card PNG. This is the best image to use for videos or other high-quality content.
    [JsonProperty("png")]
    public string? Png { get; set; }

    // A rectangular crop of the card’s art only. Not guaranteed to be perfect for cards with outlier designs or strange frame arrangements
    [JsonProperty("art_crop")]
    public string? ArtCrop { get; set; }

    // A full card image with the rounded corners and the majority of the border cropped off. Designed for dated contexts where rounded images can’t be used.
    [JsonProperty("border_crop")]
    public string? BorderCrop { get; set; }
}
