using System.Net.Http.Json;
using Scriptorium.Mtg.Importer.Models.Card;
using Scriptorium.Mtg.Importer.Models.Catalog;
using Scriptorium.Mtg.Importer.Models.Set;
using Scriptorium.Mtg.Importer.Save.Entities;
using Scriptorium.Mtg.Importer.Save.Interfaces;

namespace Scriptorium.Mtg.Importer.Save;

public class DatabaseSaver : IDatabaseSaver
{
    private readonly IDatabaseMapper _cardConverter;
    private readonly HttpClient _httpClient;

    public DatabaseSaver(IDatabaseMapper cardConverter)
    {
        _cardConverter = cardConverter;
        _httpClient = new()
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
            Console.WriteLine($"Processing {group.Key}");

            var enCards = group.Where(x => x.Language == "en").ToList();
            var enCard = enCards.OrderByDescending(x => x.ReleasedDate).First();
            enCards.Remove(enCard);
            var cardDto = _cardConverter.ConvertCard(enCard);

            foreach (var card in enCards)
                cardDto.CardSets.Add(_cardConverter.ConvertCardSet(card));

            var languageGroups = group.GroupBy(x => x.Language);

            foreach (var languageGroup in languageGroups)
            {
                if (languageGroup.Key == "en")
                    continue;

                var localizedCards = languageGroup.OrderByDescending(x => x.ReleasedDate).ToList();
                var firstCard = localizedCards.First();
                localizedCards.Remove(firstCard);

                cardDto.CardNames.AddRange(_cardConverter.ConvertCardNames(firstCard));
                cardDto.CardTexts.AddRange(_cardConverter.ConvertCardTexts(firstCard));
                cardDto.CardTypelines.AddRange(_cardConverter.ConvertTypelines(firstCard));
                AddCardSet(cardDto, firstCard);

                foreach (var localizedCard in localizedCards)
                {
                    AddCardSet(cardDto, localizedCard);
                }
            }

            if (cardDto != null)
            {
                var response = _httpClient.PostAsJsonAsync("Cards", cardDto).Result;

                response.EnsureSuccessStatusCode();
            }
        }
    }

    private void AddCardSet(CardDto cardDto, Card card)
    {
        var cardSet = cardDto.CardSets.FirstOrDefault(x => x.SetId.Equals(card.Set.SetId));
        if (cardSet == null)
            cardDto.CardSets.Add(_cardConverter.ConvertCardSet(card));
        else if (card.Set.CardSetFaces.Count == cardSet.CardSetFaces.Count)
            foreach (var cardSetFace in cardSet.CardSetFaces)
                cardSetFace.CardSetFaceFlavors.AddRange(_cardConverter.ConvertCardSetFaceFlavors(cardSetFace.Id, card.Set.CardSetFaces.First(x => x.FaceId == cardSetFace.FaceId)));
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
