namespace MTG.Scryfall.Importer.Tests;

public class ScryfallReaderTests
{
    [Fact]
    public void ShouldThrowExceptionWhenStreamIsEmpty()
    {
        //Arrange
        var reader = new ScryfallReader();

        // Act & Assert
        Assert.Throws<FileNotFoundException>(() => reader.Read(null));
    }
}
