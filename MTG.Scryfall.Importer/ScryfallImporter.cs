using MTG.Importer.Models.Card;
using MTG.Importer.Models.Catalog;
using MTG.Importer.Models.Set;
using MTG.Scryfall.Importer.Interfaces;

namespace MTG.Scryfall.Importer;

public class ScryfallImporter : IScryfallImporter
{
    private readonly IScryfallGetter _getter;
    private readonly IScryfallReader _reader;
    private readonly IScryfallMapper _mapper;

    public ScryfallImporter(IScryfallGetter getter, IScryfallReader reader, IScryfallMapper mapper)
    {
        _getter = getter;
        _reader = reader;
        _mapper = mapper;
    }

    public IList<Card>? CardsImport()
    {
        using var streamReader = _getter.GetScryfallCardStreamReader();

        var scryfallCards = _reader.ReadCards(streamReader);

        if (scryfallCards == null)
            return [];

        Console.WriteLine($"{scryfallCards.Count} scryfall cards");

        var cards = _mapper.MapCards(scryfallCards);

        Console.WriteLine($"{cards.Count} mapped");

        return cards;
    }

    public IList<Set>? SetsImport(IList<Set>? setsDb)
    {
        var result = _getter.GetScryfallUrl("sets");
        var scryfallSets = _reader.ReadSets(result);

        if (scryfallSets == null)
            return [];
        
        var sets = _mapper.MapSets(scryfallSets).ToList();

        if (setsDb != null && sets != null)
            sets = sets.Where(x => !setsDb.Select(y => y.Code).Contains(x.Code)).ToList();

        return sets;
    }

    public IList<Artist>? ArtistsImport(IList<Artist>? artistsDb)
    {
        var result = _getter.GetScryfallUrl("catalog/artist-names");
        var scryfallArtists = _reader.ReadCatalog(result);

        if (scryfallArtists == null)
            return [];

        if (artistsDb != null)
            scryfallArtists = scryfallArtists.Except(artistsDb.Select(x => x.Name)).ToList();

        var artists = _mapper.MapArtist(scryfallArtists);

        return artists;
    }

    public IList<Supertype>? SupertypesImport(IList<Supertype>? supertypesDb)
    {
        var result = _getter.GetScryfallUrl("catalog/supertypes");
        var scryfallSupertypes = _reader.ReadCatalog(result);

        if (scryfallSupertypes == null)
            return [];

        if (supertypesDb != null)
            scryfallSupertypes = scryfallSupertypes.Except(supertypesDb.Select(x => x.Name)).ToList();

        var supertypes = _mapper.MapSupertype(scryfallSupertypes);

        return supertypes;
    }

    public IList<CardType>? TypesImport(IList<CardType>? typesDb)
    {
        var result = _getter.GetScryfallUrl("catalog/card-types");
        var scryfallTypes = _reader.ReadCatalog(result);

        if (scryfallTypes == null)
            return [];

        if (typesDb != null)
            scryfallTypes = scryfallTypes.Except(typesDb.Select(x => x.Name)).ToList();

        var types = _mapper.MapCardType(scryfallTypes);

        return types;
    }

    public IList<Subtype>? SubtypesImport(IList<Subtype>? typesDb, string cardtype)
    {
        var result = _getter.GetScryfallUrl($"catalog/{cardtype.ToLower()}-types");
        var scryfallSubtypes = _reader.ReadCatalog(result);

        if (scryfallSubtypes == null)
            return [];

        if (typesDb != null)
            scryfallSubtypes = scryfallSubtypes.Except(typesDb.Where(x => x.TypeCard == cardtype).Select(x => x.Name)).ToList();

        var subtypes = _mapper.MapSubtype(scryfallSubtypes, cardtype);

        return subtypes;
    }
}
