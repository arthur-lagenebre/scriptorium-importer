using MTG.Importer.Models.Card;
using MTG.Scryfall.Importer.Interfaces;

namespace MTG.Scryfall.Importer;

public class ScryfallImporter : IScryfallImporter
{
    private readonly IScryfallReader _reader;
    private readonly IScryfallMapper _mapper;

    public ScryfallImporter(IScryfallReader reader, IScryfallMapper mapper)
    {
        _reader = reader;
        _mapper = mapper;
    }

    public IList<Card> Import(string path)
    {
        if (string.IsNullOrEmpty(path) || !File.Exists(path))
            throw new FileNotFoundException(path);

        using var streamReader = new StreamReader(path);

        var srcyfallCards = _reader.Read(streamReader);

        if (srcyfallCards == null)
            return [];

        var cards = _mapper.Map(srcyfallCards);
        
        return cards;
    }
}
