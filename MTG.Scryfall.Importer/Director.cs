using MTG.Scryfall.Importer.Interfaces;
using MTG.Scryfall.Models;

namespace MTG.Scryfall.Importer;

public class Director
{
    public IBuilder Builder { get; }

    public Director(IBuilder builder)
    { 
        Builder = builder;
    }

    public void MakeCard(ScryfallCard scryfallCard)
    {
        Builder.BuildCommonPart(scryfallCard);
    }
}
