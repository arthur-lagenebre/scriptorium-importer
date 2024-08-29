using MTG.Scryfall.Importer.Helpers;

namespace MTG.Scryfall.Importer.Tests.Helpers;

public class StringHelperTests
{
    [Fact]
    public void ShouldHaveEmptyStringWhenInputIsEmpty()
    {
        //Arrange
        var input = string.Empty;
        var expected = string.Empty;

        // Act
        var result = StringHelper.GetDefaultValue(input);

        //Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void ShouldHaveEmptyStringWhenInputIsNull()
    {
        //Arrange
        var expected = string.Empty;

        // Act
        var result = StringHelper.GetDefaultValue(null);

        //Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void ShouldHaveStringWhenInputIsCorrect()
    {
        //Arrange
        var input = "2024-01-01";
        var expected = "2024-01-01";

        // Act
        var result = StringHelper.GetDefaultValue(input);

        //Assert
        Assert.Equal(expected, result);
    }
}
