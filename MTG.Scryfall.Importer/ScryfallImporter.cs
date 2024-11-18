using MTG.Importer.Models.Card;
using MTG.Importer.Models.Catalog;
using MTG.Importer.Models.Ruling;
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

        foreach (var card in scryfallCards)
            if (card.AllParts != null && card.AllParts.Count != 0)
                foreach (var part in card.AllParts)
                    part.OracleId = scryfallCards.FirstOrDefault(x => x.Name == part.Name)?.OracleId;

        var cards = _mapper.MapCards(scryfallCards);

        return cards;
    }

    public IList<Set>? SetsImport()
    {
        var result = _getter.GetScryfallUrl("sets");
        var scryfallSets = _reader.ReadSets(result);

        if (scryfallSets == null)
            return [];
        
        var sets = _mapper.MapSets(scryfallSets).ToList();

        return sets;
    }

    public IList<Artist>? ArtistsImport()
    {
        var result = _getter.GetScryfallUrl("catalog/artist-names");
        var scryfallArtists = _reader.ReadCatalog(result);

        if (scryfallArtists == null)
            return [];

        var artists = _mapper.MapArtist(scryfallArtists);

        return artists;
    }

    public IList<Supertype>? SupertypesImport()
    {
        var result = _getter.GetScryfallUrl("catalog/supertypes");
        var scryfallSupertypes = _reader.ReadCatalog(result);

        if (scryfallSupertypes == null)
            return [];

        var supertypes = _mapper.MapSupertype(scryfallSupertypes);

        return supertypes;
    }

    public IList<CardType>? TypesImport()
    {
        var result = _getter.GetScryfallUrl("catalog/card-types");
        var scryfallTypes = _reader.ReadCatalog(result);

        if (scryfallTypes == null)
            return [];

        var types = _mapper.MapCardType(scryfallTypes);

        return types;
    }

    public IList<Subtype>? SubtypesImport(string cardtype)
    {
        var result = _getter.GetScryfallUrl($"catalog/{cardtype.ToLower()}-types");
        var scryfallSubtypes = _reader.ReadCatalog(result);

        if (scryfallSubtypes == null)
            return [];

        var subtypes = _mapper.MapSubtype(scryfallSubtypes, cardtype);

        return subtypes;
    }

    public IList<Ruling>? RulingsImport()
    {
        using var streamReader = _getter.GetScryfallRulingStreamReader();

        var scryfallRulings = _reader.ReadRulings(streamReader);

        if (scryfallRulings == null)
            return [];

        var rulings = _mapper.MapRulings(scryfallRulings);

        return rulings;
    }
}
