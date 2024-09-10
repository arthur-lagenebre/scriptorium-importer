namespace MTG.Scryfall.Importer.Tests;

public class ScryfallTypelineManagerTests
{
    [Fact]
    public void Should_Empty_Elements_When_typeline_Is_Null()
    {
        // Arrange
        var scryfallTypelineManager = new ScryfallTypelineManager();
        string? typeline = null;

        // Act
        var result = scryfallTypelineManager.ExtractTypeline(typeline);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result.Supertype);
        Assert.Empty(result.Types);
        Assert.Empty(result.Subtypes);
    }

    [Fact]
    public void Should_Empty_Elements_When_typeline_Is_Empty()
    {
        // Arrange
        var scryfallTypelineManager = new ScryfallTypelineManager();
        string? typeline = string.Empty;

        // Act
        var result = scryfallTypelineManager.ExtractTypeline(typeline);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result.Supertype);
        Assert.Empty(result.Types);
        Assert.Empty(result.Subtypes);
    }

    [Fact]
    public void Should_Only_Artifact_When_typeline_Is_Artifact()
    {
        // Arrange
        var scryfallTypelineManager = new ScryfallTypelineManager();
        string? typeline = "artifact";

        // Act
        var result = scryfallTypelineManager.ExtractTypeline(typeline);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result.Supertype);
        Assert.Collection(result.Types, e => { Assert.Equal("Artifact", e); });
        Assert.Empty(result.Subtypes);
    }
}
