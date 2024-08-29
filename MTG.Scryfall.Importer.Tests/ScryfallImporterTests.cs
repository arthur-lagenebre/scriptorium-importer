using MTG.Scryfall.Importer.Interfaces;
using NSubstitute;

namespace MTG.Scryfall.Importer.Tests;

public class ScryfallImporterTests
{
    private readonly IScryfallMapper _mapper = Substitute.For<IScryfallMapper>();
    private readonly IScryfallReader _reader = Substitute.For<IScryfallReader>();

    [Fact]
    public void Should_Throw_FileNotFoundException_When_Path_Is_Empty()
    {
        // Arrange
        var importer = new ScryfallImporter(_reader, _mapper);

        // Act & Assert
        Assert.Throws<FileNotFoundException>(() => importer.Import(string.Empty));
    }

    [Fact]
    public void Should_Throw_FileNotFoundException_When_Path_Is_Incorrect()
    {
        // Arrange
        var importer = new ScryfallImporter(_reader, _mapper);

        // Act & Assert
        Assert.Throws<FileNotFoundException>(() => importer.Import("Incorrect Path"));
    }
}