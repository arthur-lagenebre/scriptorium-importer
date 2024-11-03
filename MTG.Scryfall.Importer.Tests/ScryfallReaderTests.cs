namespace MTG.Scryfall.Importer.Tests;

public class ScryfallReaderTests
{
    [Fact]
    public void Should_Throw_Exception_When_Stream_Is_Null()
    {
        // Arrange
        var reader = new ScryfallReader();

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => reader.ReadCards(null));
    }

    [Fact]
    public void Should_Empty_List_When_Stream_Is_Empty()
    {
        // Arrange
        var reader = new ScryfallReader();

        // Act
        var result = reader.ReadCards(StreamReader.Null);

        // Assert
        Assert.Empty(result);
    }
}
