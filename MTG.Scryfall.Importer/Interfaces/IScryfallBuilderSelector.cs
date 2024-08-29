namespace MTG.Scryfall.Importer.Interfaces;

public interface IScryfallBuilderSelector
{
    IScryfallBuilder Create(string? layout);
}