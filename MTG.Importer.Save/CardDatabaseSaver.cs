using System.Net.Http.Json;
using MTG.Importer.Models.Card;
using MTG.Importer.Models.Catalog;
using MTG.Importer.Models.Set;
using MTG.Importer.Save.Interfaces;

namespace MTG.Importer.Save;

public class CardDatabaseSaver : ICardDatabaseSaver
{
    private readonly IDatabaseMapper _cardConverter;
    private readonly ICardDatabaseReader _cardDatabaseReader;
    private readonly HttpClient _httpClient;

    public CardDatabaseSaver(IDatabaseMapper cardConverter, ICardDatabaseReader cardDatabaseReader)
    {
        _cardConverter = cardConverter;
        _cardDatabaseReader = cardDatabaseReader;
        _httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:7276/api/")
        };
    }

    public void SaveCards(IList<Card> cards)
    {
        var groups = cards.GroupBy(x => x.OracleId).ToList();

        foreach (var group in groups)
        {
            var cardDb = _cardDatabaseReader.GetCard(group.Key);
            var cardNamesDb = _cardDatabaseReader.GetCardNames(group.Key);
            var cardTextsDb = _cardDatabaseReader.GetCardTexts(group.Key);

            foreach (var card in group)
            {
                if (cardDb == null)
                {
                    var cardDto = _cardConverter.ConvertCard(card);
                    var response = _httpClient.PostAsJsonAsync("Cards", cardDto).Result;

                    response.EnsureSuccessStatusCode();
                }

                if (cardNamesDb != null && !cardNamesDb.Any(x => x.Language.Equals(card.Name.Language)))
                {
                    var cardNamesDto = _cardConverter.ConvertCardNames(card);

                    foreach (var cardNameDto in cardNamesDto)
                    {
                        var response = _httpClient.PostAsJsonAsync("CardNames", cardNameDto).Result;

                        response.EnsureSuccessStatusCode();
                    }
                }

                if (cardTextsDb != null && !cardTextsDb.Any(x => x.Language.Equals(card.Text.Language)))
                {
                    var cardTextsDto = _cardConverter.ConvertCardTexts(card);

                    foreach (var cardTextDto in cardTextsDto)
                    {
                        var response = _httpClient.PostAsJsonAsync("CardTexts", cardTextDto).Result;

                        response.EnsureSuccessStatusCode();
                    }
                }
            }
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
        var client = new HttpClient();

        foreach (var set in sets)
        {
            var response = client.PostAsJsonAsync("Sets", set).Result;

            response.EnsureSuccessStatusCode();
        }
    }
}
