using Newtonsoft.Json;

namespace MTG.Scryfall.Models.Ruling;

public class ScryfallRuling
{
    // The Oracle ID of the card this ruling is associated with.
    [JsonProperty("oracle_id")]
    public required string OracleId { get; set; }

    // The text of the ruling.
    [JsonProperty("comment")]
    public required string Comment { get; set; }

    // The date when the ruling or note was published.
    [JsonProperty("published_at")]
    public required string PublishedAt { get; set; }

    // A computer-readable string indicating which company produced this ruling, either wotc or scryfall.
    [JsonProperty("source")]
    public required string Source { get; set; }
}
