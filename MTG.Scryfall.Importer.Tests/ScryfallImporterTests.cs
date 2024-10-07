using MTG.Scryfall.Importer.Interfaces;
using NSubstitute;

namespace MTG.Scryfall.Importer.Tests;

public class ScryfallImporterTests
{
    private readonly IScryfallGetter _getter = Substitute.For<IScryfallGetter>();
    private readonly IScryfallMapper _mapper = Substitute.For<IScryfallMapper>();
    private readonly IScryfallReader _reader = Substitute.For<IScryfallReader>();
}