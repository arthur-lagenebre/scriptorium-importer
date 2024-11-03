using System.Net.Http.Json;
using MTG.Importer.Models.Card;
using MTG.Importer.Models.Set;
using MTG.Importer.Save.Interfaces;

namespace MTG.Importer.Save;

public class CardDatabaseSave : ICardDatabaseSave
{
    private readonly ICardMapper _cardConverter;

    public CardDatabaseSave(ICardMapper cardConverter) => _cardConverter = cardConverter;

    public async void SaveCards(IList<Card> cards)
    {
        var client = new HttpClient();

        foreach (Card card in cards)
        {
            var cardDto = _cardConverter.Convert(card);
            HttpResponseMessage response = await client.PostAsJsonAsync("https://localhost:7276/api/Cards", cardDto);
            response.EnsureSuccessStatusCode();
        }
    }

    public async void SaveSets(IList<Set> sets)
    {
        var client = new HttpClient();

        foreach (var set in sets)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync("https://localhost:7276/api/Sets", set);
            response.EnsureSuccessStatusCode();
        }
    }
}
