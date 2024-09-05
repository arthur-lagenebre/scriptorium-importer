using MTG.Scryfall.Importer.Interfaces;
using MTG.Scryfall.Models.Card;
using NSubstitute;

namespace MTG.Scryfall.Importer.Tests;

public class ScryfallDirectorTests
{
    private readonly IEnumerable<IScryfallBuilder> _scryfallBuilders = Substitute.For<IEnumerable<IScryfallBuilder>>();

    [Fact]
    public void Should_Throw_NotSupportedException_When_Layout_Is_Null()
    {
        // Arrange
        var director = new ScryfallDirector(_scryfallBuilders);
        var scryfallCard = new ScryfallCard();

        // Act & Assert
        Assert.Throws<NotSupportedException>(() => director.BuildCard(scryfallCard));
    }

    [Fact]
    public void Should_Throw_NotSupportedException_When_Layout_Is_Invalid()
    {
        // Arrange
        var director = new ScryfallDirector(_scryfallBuilders);
        var scryfallCard = new ScryfallCard() { Layout = "invalid" };

        // Act & Assert
        Assert.Throws<NotSupportedException>(() => director.BuildCard(scryfallCard));
    }
}
