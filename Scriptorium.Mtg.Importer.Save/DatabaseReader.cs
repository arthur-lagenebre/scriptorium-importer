using Scriptorium.Mtg.Importer.Models.Catalog;
using Scriptorium.Mtg.Importer.Models.Set;
using Scriptorium.Mtg.Importer.Save.Interfaces;
using Newtonsoft.Json;

namespace Scriptorium.Mtg.Importer.Save;

public class DatabaseReader(HttpClient httpClient) : IDatabaseReader
{
    public IList<Artist>? GetArtists()
    {
        var response = httpClient.GetAsync("Artists").Result;

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
        var response = httpClient.GetAsync("Supertypes/en").Result;

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
        var response = httpClient.GetAsync("Types/en").Result;

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
        var response = httpClient.GetAsync("Subtypes/en").Result;

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
        var response = httpClient.GetAsync("Sets").Result;

        if (!response.IsSuccessStatusCode)
            throw new Exception(response.Content.ToString());

        var result = response.Content.ReadAsStringAsync().Result;
        var serializer = new JsonSerializer();

        using var reader = new JsonTextReader(new StringReader(result));

        var sets = serializer.Deserialize<IList<Set>>(reader);

        return sets;
    }
}
