using MTG.Importer.Models.Card;
using MTG.Scryfall.Importer.Helpers;

namespace MTG.Scryfall.Importer.Tests.Helpers;

public class ColorHelperTests
{
    [Fact]
    public void ShouldHaveNoneWhenListIsEmpty()
    {
        //Arrange
        var colors = new List<string>();
        var expected = Color.None;

        // Act
        var result = ColorHelper.GetCardColor(colors);

        //Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void ShouldHaveNoneWhenListIsNull()
    {
        //Arrange
        var expected = Color.None;

        // Act
        var result = ColorHelper.GetCardColor(null);

        //Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void ShouldHaveBWhenListHaveB()
    {
        //Arrange
        var colors = new List<string> { "B" };
        var expected = Color.B;

        // Act
        var result = ColorHelper.GetCardColor(colors);

        //Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void ShouldHaveWBWhenListHaveWB()
    {
        //Arrange
        var colors = new List<string> { "W", "B" };
        var expected = Color.W | Color.B;

        // Act
        var result = ColorHelper.GetCardColor(colors);

        //Assert
        Assert.Equal(expected, result);
    }
}
