using MTG.Scryfall.Interfaces;
using NSubstitute;

namespace MTG.Scryfall.Importer.Tests;

public class ImporterTests
{
    private IMapper _mapper = Substitute.For<IMapper>();
    private IReader _reader = Substitute.For<IReader>();

    [Fact]
    public void ShouldThrowFileNotFoundExceptionWhenPathIsEmpty()
    {
        //Arrange
        var importer = new Importer(_reader, _mapper);

        // Act & Assert
        Assert.Throws<FileNotFoundException>(() => importer.Import(string.Empty));
    }

    [Fact]
    public void ShouldThrowFileNotFoundExceptionWhenPathIsIncorrect()
    {
        //Arrange
        var importer = new Importer(_reader, _mapper);

        // Act & Assert
        Assert.Throws<FileNotFoundException>(() => importer.Import("Incorrect Path"));
    }
}