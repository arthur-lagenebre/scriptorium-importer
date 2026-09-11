using Scriptorium.Mtg.Scryfall.Importer.Helpers;

namespace Scriptorium.Mtg.Scryfall.Importer.Tests.Helpers;

public class StringHelperTests
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void Should_Have_Empty_String(string? input)
    {
        // Arrange
        var expected = string.Empty;

        // Act
        var result = StringHelper.GetDefaultValue(input);

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("value", "value")]
    public void Should_Have_Same_Input_And_Output(string? input, string? expected)
    {
        // Act
        var result = StringHelper.GetDefaultValue(input);

        // Assert
        Assert.Equal(expected, result);
    }
}
