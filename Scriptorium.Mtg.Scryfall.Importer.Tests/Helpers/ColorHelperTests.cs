using Scriptorium.Mtg.Importer.Models.Card;
using Scriptorium.Mtg.Scryfall.Importer.Helpers;

namespace Scriptorium.Mtg.Scryfall.Importer.Tests.Helpers;

public class ColorHelperTests
{
    [Theory]
    [InlineData(null, Color.None)]
    public void Should_Have_None(List<string>? colors, Color expected)
    {
        // Act
        var result = ColorHelper.GetCardColor(colors);

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Should_Have_None_When_List_Is_Empty()
    {
        // Arrange
        var colors = new List<string>();
        var expected = Color.None;

        // Act
        var result = ColorHelper.GetCardColor(colors);

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Should_Have_B_When_List_Have_B()
    {
        // Arrange
        var colors = new List<string> { "B" };
        var expected = Color.B;

        // Act
        var result = ColorHelper.GetCardColor(colors);

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Should_Have_WB_When_List_Have_W_And_B()
    {
        // Arrange
        var colors = new List<string> { "W", "B" };
        var expected = Color.W | Color.B;

        // Act
        var result = ColorHelper.GetCardColor(colors);

        // Assert
        Assert.Equal(expected, result);
    }
}
