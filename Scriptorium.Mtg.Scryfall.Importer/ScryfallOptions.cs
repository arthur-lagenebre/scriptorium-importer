namespace Scriptorium.Mtg.Scryfall.Importer;

public class ScryfallOptions
{
    public string BaseUrl { get; set; } = "https://api.scryfall.com/";
    public string CardsFilePath { get; set; } = "";
    public string RulingsFilePath { get; set; } = "";
    public string UserAgentProduct { get; set; } = "Scriptorium.Mtg.Importer";
    public string UserAgentVersion { get; set; } = "1.0";
}