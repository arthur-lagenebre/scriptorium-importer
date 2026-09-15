using Newtonsoft.Json;

namespace Scriptorium.Mtg.Scryfall.Models.Card;

/// <summary>
/// Convertit le code de langue de Scryfall, sans faire tomber l'import
/// lorsqu'une valeur inconnue apparaît.
///
/// Scryfall ajoute des langues au fil des sorties — le quenya est arrivé
/// avec l'édition Le Seigneur des Anneaux. Sur un fichier de 2,7 Go traité
/// en plusieurs heures, une exception sur la millionième ligne coûte trop
/// cher : mieux vaut marquer la carte comme non reconnue et continuer.
/// </summary>
public class ScryfallLanguageConverter : JsonConverter<ScryfallLanguage>
{
    public override ScryfallLanguage ReadJson(
        JsonReader reader,
        Type objectType,
        ScryfallLanguage existingValue,
        bool hasExistingValue,
        JsonSerializer serializer)
    {
        var raw = reader.Value?.ToString();

        if (string.IsNullOrWhiteSpace(raw))
            return ScryfallLanguage.Unknown;

        if (Enum.TryParse<ScryfallLanguage>(raw, ignoreCase: true, out var language))
            return language;

        // Signalé une fois par code inconnu, pour que la sortie reste lisible.
        if (Reported.Add(raw))
            Console.WriteLine($"Langue inconnue ignorée : {raw}");

        return ScryfallLanguage.Unknown;
    }

    public override void WriteJson(JsonWriter writer, ScryfallLanguage value, JsonSerializer serializer)
    {
        writer.WriteValue(value.ToString().ToLowerInvariant());
    }

    private static readonly HashSet<string> Reported = [];
}
