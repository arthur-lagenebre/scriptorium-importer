using MTG.Importer.Models.Card;
using MTG.Importer.Models.Catalog;
using MTG.Importer.Models.Set;
using MTG.Importer.Save.Entities;
using MTG.Importer.Save.Interfaces;
using Newtonsoft.Json;

namespace MTG.Importer.Save;

public class CardDatabaseReader : ICardDatabaseReader
{
    private readonly HttpClient _httpClient;

    public CardDatabaseReader()
    {
        _httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:7276/api/")
        };
    }

    public IList<Artist>? GetArtists()
    {
        var response = _httpClient.GetAsync("Artists").Result;

        if (!response.IsSuccessStatusCode)
            throw new Exception(response.Content.ToString());

        var result = response.Content.ReadAsStringAsync().Result;
        var serializer = new JsonSerializer();

        using var reader = new JsonTextReader(new StringReader(result));

        var artists = serializer.Deserialize<IList<Artist>>(reader);

        return artists;
    }

    public IList<Supertype>? GetSupertypes()
    {
        var response = _httpClient.GetAsync("Supertypes/en").Result;

        if (!response.IsSuccessStatusCode)
            throw new Exception(response.Content.ToString());

        var result = response.Content.ReadAsStringAsync().Result;
        var serializer = new JsonSerializer();

        using var reader = new JsonTextReader(new StringReader(result));

        var supertypes = serializer.Deserialize<IList<Supertype>>(reader);

        return supertypes;
    }

    public IList<CardType>? GetTypes()
    {
        var response = _httpClient.GetAsync("Types/en").Result;

        if (!response.IsSuccessStatusCode)
            throw new Exception(response.Content.ToString());

        var result = response.Content.ReadAsStringAsync().Result;
        var serializer = new JsonSerializer();

        using var reader = new JsonTextReader(new StringReader(result));

        var types = serializer.Deserialize<IList<CardType>>(reader);

        return types;
    }

    public IList<Subtype>? GetSubtypes()
    {
        var response = _httpClient.GetAsync("Subtypes/en").Result;

        if (!response.IsSuccessStatusCode)
            throw new Exception(response.Content.ToString());

        var result = response.Content.ReadAsStringAsync().Result;
        var serializer = new JsonSerializer();

        using var reader = new JsonTextReader(new StringReader(result));

        var subtypes = serializer.Deserialize<IList<Subtype>>(reader);

        return subtypes;
    }

    public IList<Set>? GetSets()
    {
        var response = _httpClient.GetAsync("Sets").Result;

        if (!response.IsSuccessStatusCode)
            throw new Exception(response.Content.ToString());

        var result = response.Content.ReadAsStringAsync().Result;
        var serializer = new JsonSerializer();

        using var reader = new JsonTextReader(new StringReader(result));

        var sets = serializer.Deserialize<IList<Set>>(reader);

        return sets;
    }

    public CardDto? GetCard(Guid oracleId)
    {
        var response = _httpClient.GetAsync($"Cards/{oracleId}").Result;

        if (!response.IsSuccessStatusCode)
            throw new Exception(response.Content.ToString());

        var result = response.Content.ReadAsStringAsync().Result;
        var serializer = new JsonSerializer();

        using var reader = new JsonTextReader(new StringReader(result));

        var cardDto = serializer.Deserialize<CardDto>(reader);

        return cardDto;
    }

    public IList<CardNameDto>? GetCardNames(Guid oracleId)
    {
        var response = _httpClient.GetAsync($"CardNames/{oracleId}").Result;

        if (!response.IsSuccessStatusCode)
            throw new Exception(response.Content.ToString());

        var result = response.Content.ReadAsStringAsync().Result;
        var serializer = new JsonSerializer();

        using var reader = new JsonTextReader(new StringReader(result));

        var cardNames = serializer.Deserialize<IList<CardNameDto>>(reader);

        return cardNames;
    }

    public IList<CardTextDto>? GetCardTexts(Guid oracleId)
    {
        var response = _httpClient.GetAsync($"CardTexts/{oracleId}").Result;

        if (!response.IsSuccessStatusCode)
            throw new Exception(response.Content.ToString());

        var result = response.Content.ReadAsStringAsync().Result;
        var serializer = new JsonSerializer();

        using var reader = new JsonTextReader(new StringReader(result));

        var cardTexts = serializer.Deserialize<IList<CardTextDto>>(reader);

        return cardTexts;
    }
}
