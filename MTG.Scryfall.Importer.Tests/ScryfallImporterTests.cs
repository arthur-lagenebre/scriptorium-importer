using MTG.Scryfall.Importer.Interfaces;
using NSubstitute;

namespace MTG.Scryfall.Importer.Tests;

public class ScryfallImporterTests
{
    private IScryfallMapper _mapper = Substitute.For<IScryfallMapper>();
    private IScryfallReader _reader = Substitute.For<IScryfallReader>();

    [Fact]
    public void ShouldThrowFileNotFoundExceptionWhenPathIsEmpty()
    {
        //Arrange
        var importer = new ScryfallImporter(_reader, _mapper);

        // Act & Assert
        Assert.Throws<FileNotFoundException>(() => importer.Import(string.Empty));
    }

    [Fact]
    public void ShouldThrowFileNotFoundExceptionWhenPathIsIncorrect()
    {
        //Arrange
        var importer = new ScryfallImporter(_reader, _mapper);

        // Act & Assert
        Assert.Throws<FileNotFoundException>(() => importer.Import("Incorrect Path"));
    }
}