using MTG.Scryfall.Importer.Interfaces;
using MTG.Scryfall.Models.Card;
using NSubstitute;

namespace MTG.Scryfall.Importer.Tests;

public class ScryfallDirectorTests
{
    private readonly IEnumerable<IScryfallBuilder> _scryfallBuilders = Substitute.For<IEnumerable<IScryfallBuilder>>();

    [Fact]
    public void Should_Throw_NotSupportedException_When_Layout_Is_Invalid()
    {
        // Arrange
        var director = new ScryfallCardDirector(_scryfallBuilders);
        var scryfallCard = new ScryfallCard() { Layout = "invalid", Name = "Name", TypeLine = "TypeLine", Id = string.Empty, ColorIdentity = [], Keywords = [] };

        // Act & Assert
        Assert.Throws<NotSupportedException>(() => director.BuildCard(scryfallCard, []));
    }
}
