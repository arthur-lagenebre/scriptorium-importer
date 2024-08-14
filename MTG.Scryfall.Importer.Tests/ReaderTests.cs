namespace MTG.Scryfall.Importer.Tests;

public class ReaderTests
{
    [Fact]
    public void ShouldThrowExceptionWhenStreamIsEmpty()
    {
        //Arrange
        var reader = new Reader();

        // Act & Assert
        Assert.Throws<FileNotFoundException>(() => reader.Read(null));
    }
}
