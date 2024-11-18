using System.Net.Http.Json;
using MTG.Importer.Models.Card;
using MTG.Importer.Models.Catalog;
using MTG.Importer.Models.Ruling;
using MTG.Importer.Models.Set;
using MTG.Importer.Save.Entities;
using MTG.Importer.Save.Interfaces;

namespace MTG.Importer.Save;

public class DatabaseSaver : IDatabaseSaver
{
    private readonly IDatabaseMapper _databaseMapper;
    private readonly HttpClient _httpClient;

    public DatabaseSaver(IDatabaseMapper cardConverter)
    {
        _databaseMapper = cardConverter;
        _httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:7276/api/")
        };
    }

    public void SaveCards(IList<Card> cards)
    {
        var cardsDto = new List<CardDto>();
        var groups = cards.GroupBy(x => x.OracleId);

        foreach (var group in groups)
        {
            Console.WriteLine($"Converting {group.Key}");
            var languageGroups = group.GroupBy(x => x.Language);

            foreach (var languageGroup in languageGroups)
            {
                if (languageGroup.Count() == 1)
                {
                    var card = languageGroup.First();
                    var cardDto = cardsDto.FirstOrDefault(x => x.Id == card.OracleId);

                    if (cardDto == null)
                        cardsDto.Add(_databaseMapper.ConvertCard(languageGroup.First()));
                    else
                    {
                        cardDto.CardNames.AddRange(_databaseMapper.ConvertCardNames(card));
                        cardDto.CardTexts.AddRange(_databaseMapper.ConvertCardTexts(card));
                        var cardSet = cardDto.CardSets.FirstOrDefault(x => x.SetId.Equals(card.Set.SetId));
                        if (cardSet == null)
                            cardDto.CardSets.Add(_databaseMapper.ConvertCardSet(card));
                    }
                }
                else
                {
                    foreach (var card in languageGroup)
                    {
                        var cardDto = cardsDto.FirstOrDefault(x => x.Id == card.OracleId);

                        if (cardDto == null)
                            cardsDto.Add(_databaseMapper.ConvertCard(languageGroup.First()));
                        else
                        {
                            var cardName = cardDto.CardNames.FirstOrDefault(x => x.Language.Equals(card.Language));
                            if (cardName == null)
                                cardDto.CardNames.AddRange(_databaseMapper.ConvertCardNames(card));
                            var cardText = cardDto.CardTexts.FirstOrDefault(x => x.Language.Equals(card.Language));
                            if (cardText == null)
                                cardDto.CardTexts.AddRange(_databaseMapper.ConvertCardTexts(card));
                            var cardSet = cardDto.CardSets.FirstOrDefault(x => x.SetId.Equals(card.Set.SetId));
                            if (cardSet == null)
                                cardDto.CardSets.Add(_databaseMapper.ConvertCardSet(card));
                        }
                    }
                }
            }
        }

        foreach (var cardDto in cardsDto)
        {
            Console.WriteLine($"Saving {cardDto.Id}");
            var response = _httpClient.PostAsJsonAsync("Cards", cardDto).Result;

            response.EnsureSuccessStatusCode();
        }
    }

    public void SaveArtists(IList<Artist> artists)
    {
        foreach (var artist in artists)
        {
            var response = _httpClient.PostAsJsonAsync("Artists", artist).Result;
            response.EnsureSuccessStatusCode();
        }
    }

    public void SaveSupertypes(IList<Supertype> supertypes)
    {
        foreach (var supertype in supertypes)
        {
            var response = _httpClient.PostAsJsonAsync("Supertypes", supertype).Result;

            response.EnsureSuccessStatusCode();
        }
    }

    public void SaveTypes(IList<CardType> types)
    {
        foreach (var type in types)
        {
            var response = _httpClient.PostAsJsonAsync("Types", type).Result;

            response.EnsureSuccessStatusCode();
        }
    }

    public void SaveSubtypes(IList<Subtype> subtypes)
    {
        foreach (var subtype in subtypes)
        {
            var response = _httpClient.PostAsJsonAsync("Subtypes", subtype).Result;

            response.EnsureSuccessStatusCode();
        }
    }

    public void SaveSets(IList<Set> sets)
    {
        foreach (var set in sets)
        {
            var response = _httpClient.PostAsJsonAsync("Sets", set).Result;

            response.EnsureSuccessStatusCode();
        }
    }

    public void SaveRulings(IList<Ruling> rulings)
    {
        foreach (var ruling in rulings)
        {
            var rulingDTO = new RulingDto(Guid.NewGuid(), ruling.OracleId, ruling.Language, ruling.Rule, ruling.PublishedAt);
            var response = _httpClient.PostAsJsonAsync("Rulings", ruling).Result;
            response.EnsureSuccessStatusCode();
        }
    }
}
