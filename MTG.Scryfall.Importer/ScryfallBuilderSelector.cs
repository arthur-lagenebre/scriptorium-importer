using MTG.Scryfall.Importer.Interfaces;

namespace MTG.Scryfall.Importer;

public class ScryfallBuilderSelector : IScryfallBuilderSelector
{
    private readonly IEnumerable<IScryfallBuilder> _scryfallBuilders;

    public ScryfallBuilderSelector(IEnumerable<IScryfallBuilder> scryfallBuilders) => _scryfallBuilders = scryfallBuilders;

    public IScryfallBuilder Create(string? layout)
    {
        ArgumentNullException.ThrowIfNull(layout, nameof(layout));

        return _scryfallBuilders.SingleOrDefault(x => x.Layout.Name == layout) ?? throw new NotSupportedException();
    }
}
