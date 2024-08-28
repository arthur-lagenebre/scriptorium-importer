using MTG.Scryfall.Importer.Builders;
using MTG.Scryfall.Importer.Interfaces;

namespace MTG.Scryfall.Importer;

public static class ScryfallBuilder
{
    public static IBuilder Create(string? layout)
    {
        return layout switch
        {
            "normal" => new NormalBuilder(),
            "vanguard" => new VanguardBuilder(),
            "transform" => new TransformBuilder(),
            _ => throw new NotSupportedException(),
        };
    }
}
