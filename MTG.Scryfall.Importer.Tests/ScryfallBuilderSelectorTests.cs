using MTG.Scryfall.Importer.Interfaces;
using NSubstitute;

namespace MTG.Scryfall.Importer.Tests;

public class ScryfallBuilderSelectorTests
{
    private readonly IEnumerable<IScryfallBuilder> _scryfallBuilders = Substitute.For<IEnumerable<IScryfallBuilder>>();

    [Fact]
    public void Should_Throw_ArgumentNullException_When_Layout_Is_Null()
    {
        // Arrange
        var selector = new ScryfallBuilderSelector(_scryfallBuilders);

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => selector.Create(null));
    }

    [Fact]
    public void Should_Throw_NotSupportedException_When_Layout_Is_Invalid()
    {
        // Arrange
        var selector = new ScryfallBuilderSelector(_scryfallBuilders);

        // Act & Assert
        Assert.Throws<NotSupportedException>(() => selector.Create("invalid"));
    }
}
