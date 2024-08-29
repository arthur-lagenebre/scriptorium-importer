namespace MTG.Scryfall.Importer.Tests;

public class ScryfallReaderTests
{
    [Fact]
    public void Should_Throw_Exception_When_Stream_Is_Null()
    {
        //Arrange
        var reader = new ScryfallReader();

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => reader.Read(null));
    }
}
