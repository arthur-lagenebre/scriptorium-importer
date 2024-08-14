using MTG.Importer.Models;
using MTG.Scryfall.Importer.Interfaces;
using MTG.Scryfall.Models;

namespace MTG.Scryfall.Importer;

public class NormalBuilder : IBuilder
{
    private Card _card;

    public NormalBuilder() => Reset();

    public void Reset()
    {
        this._card = new Card();
    }

    public void BuildCommonPart(ScryfallCard scryfallCard)
    {
        CheckSrcyfallCard(scryfallCard);

        _card.CardId = Guid.NewGuid();
        _card.OracleId = scryfallCard.OracleId.Value;
        _card.Name = scryfallCard.Name;
    }

    private void CheckSrcyfallCard(ScryfallCard srcyfallCard)
    {
        ArgumentNullException.ThrowIfNull(srcyfallCard.OracleId, nameof(srcyfallCard.OracleId));
        ArgumentNullException.ThrowIfNull(srcyfallCard.Name, nameof(srcyfallCard.Name));
    }

    public Card GetCard()
    {
        Card result = _card;

        this.Reset();

        return result;
    }
}
