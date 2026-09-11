using Scriptorium.Mtg.Importer.Models.Card;
using Scriptorium.Mtg.Importer.Models.Catalog;
using Scriptorium.Mtg.Importer.Models.Ruling;
using Scriptorium.Mtg.Importer.Models.Set;
using Scriptorium.Mtg.Scryfall.Importer.Interfaces;

namespace Scriptorium.Mtg.Scryfall.Importer;

public class ScryfallImporter(IScryfallGetter getter, IScryfallReader reader, IScryfallMapper mapper) : IScryfallImporter
{
    private readonly IScryfallGetter _getter = getter;
    private readonly IScryfallReader _reader = reader;
    private readonly IScryfallMapper _mapper = mapper;

    public IList<Card>? CardsImport(List<Ruling> rulings)
    {
        using var streamReader = _getter.GetScryfallCardStreamReader();

        var scryfallCards = _reader.ReadCards(streamReader);

        foreach (var card in scryfallCards)
            if (card.AllParts != null && card.AllParts.Count != 0)
                foreach (var part in card.AllParts)
                    part.OracleId = scryfallCards.FirstOrDefault(x => x.Name == part.Name)?.OracleId;

        return _mapper.MapCards(scryfallCards, rulings);
    }

    public IList<Set>? SetsImport()
    {
        var result = _getter.GetScryfallUrl("sets");
        var scryfallSets = _reader.ReadSets(result);

        return _mapper.MapSets(scryfallSets).ToList();
    }

    public IList<Artist>? ArtistsImport()
    {
        var result = _getter.GetScryfallUrl("catalog/artist-names");
        var scryfallArtists = _reader.ReadCatalog(result);

        return _mapper.MapArtist(scryfallArtists);
    }

    public IList<Supertype>? SupertypesImport()
    {
        var result = _getter.GetScryfallUrl("catalog/supertypes");
        var scryfallSupertypes = _reader.ReadCatalog(result);

        return _mapper.MapSupertype(scryfallSupertypes);
    }

    public IList<CardType>? TypesImport()
    {
        var result = _getter.GetScryfallUrl("catalog/card-types");
        var scryfallTypes = _reader.ReadCatalog(result);

        return _mapper.MapCardType(scryfallTypes);
    }

    public IList<Subtype>? SubtypesImport(string cardtype)
    {
        var result = _getter.GetScryfallUrl($"catalog/{cardtype.ToLower()}-types");
        var scryfallSubtypes = _reader.ReadCatalog(result);

        return _mapper.MapSubtype(scryfallSubtypes, cardtype);
    }

    public IList<Ruling>? RulingsImport()
    {
        using var streamReader = _getter.GetScryfallRulingStreamReader();

        var scryfallRulings = _reader.ReadRulings(streamReader);

        return _mapper.MapRulings(scryfallRulings);
    }
}
