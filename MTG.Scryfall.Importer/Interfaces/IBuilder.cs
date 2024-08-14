using MTG.Scryfall.Models;

namespace MTG.Scryfall.Importer.Interfaces;

public interface IBuilder
{
    void BuildCommonPart(ScryfallCard scryfallCard);
}
