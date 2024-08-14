using MTG.Importer.Models;
using MTG.Scryfall.Interfaces;

namespace MTG.Scryfall.Importer;

public class Importer : IImporter
{
    private IReader _reader;
    private IMapper _mapper;

    public Importer(IReader reader, IMapper mapper)
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
