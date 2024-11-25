using System.Net.Http.Json;
using MTG.Importer.Models.Card;
using MTG.Importer.Models.Catalog;
using MTG.Importer.Models.Set;
using MTG.Importer.Save.Entities;
using MTG.Importer.Save.Interfaces;
using Newtonsoft.Json;

namespace MTG.Importer.Save;

public class DatabaseSaver(IDatabaseMapper cardConverter) : IDatabaseSaver
{
    private readonly HttpClient _httpClient = new()
    {
        BaseAddress = new Uri("https://localhost:7276/api/")
    };

    public void SaveCards(IList<Card> cards)
    {
        var cardsDto = new List<CardDto>();
        var groups = cards.GroupBy(x => x.OracleId);

        foreach (var group in groups)
        {
            Console.WriteLine($"Converting {group.Key}");
            var languageGroups = group.GroupBy(x => x.Language);

            CardDto? cardDto = null;

            foreach (var languageGroup in languageGroups)
            {
                foreach (var card in languageGroup)
                {
                    if (cardDto == null)
                        cardDto = cardConverter.ConvertCard(card);
                    else
                    {
                        var cardName = cardDto.CardNames.FirstOrDefault(x => x.Language.Equals(card.Language));
                        if (cardName == null)
                            cardDto.CardNames.AddRange(cardConverter.ConvertCardNames(card));
                        var cardText = cardDto.CardTexts.FirstOrDefault(x => x.Language.Equals(card.Language));
                        if (cardText == null)
                            cardDto.CardTexts.AddRange(cardConverter.ConvertCardTexts(card));

                        var cardSet = cardDto.CardSets.FirstOrDefault(x => x.SetId.Equals(card.Set.SetId));
                        if (cardSet == null)
                            cardDto.CardSets.Add(cardConverter.ConvertCardSet(card));
                        else
                        {
                            foreach (var cardSetFace in cardSet.CardSetFaces)
                            {
                                cardSetFace.CardSetFaceFlavors.AddRange(cardConverter.ConvertCardSetFaceFlavors(cardSetFace.Id, card.Set.CardSetFaces.First(x => x.FaceId == cardSetFace.FaceId)));
                            }
                        }
                    }
                }
            }

            if (cardDto != null)
            {
                cardsDto.Add(cardDto);
            }
        }

        var text = JsonConvert.SerializeObject(cardsDto, Formatting.Indented);
        File.WriteAllText(@"C:\Users\Arthur\Desktop\Test\Test.json", text);

        //foreach (var cardDto in cardsDto)
        //{
        //    Console.WriteLine($"Saving {cardDto.Id}");
        //    var response = _httpClient.PostAsJsonAsync("Cards", cardDto).Result;

        //    response.EnsureSuccessStatusCode();
        //}
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
}
