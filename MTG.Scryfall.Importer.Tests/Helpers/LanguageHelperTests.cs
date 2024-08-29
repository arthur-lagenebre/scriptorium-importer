using MTG.Scryfall.Importer.Helpers;

namespace MTG.Scryfall.Importer.Tests.Helpers;

public class LanguageHelperTests
{
    [Theory]
    [InlineData("en", null, null)]
    [InlineData("en", "", null)]
    [InlineData("en", null, "")]
    [InlineData("en", null, "Value")]
    [InlineData("en", "", "Value")]
    [InlineData("fr", null, null)]
    [InlineData("fr", "", null)]
    [InlineData("fr", null, "")]
    [InlineData("fr", "value", null)]
    [InlineData("fr", "value", "")]
    public void Should_Have_Empty_String(string language, string? defaultValue, string? printedValue)
    {
        //Arrange
        var expected = string.Empty;

        // Act
        var result = LanguageHelper.GetLanguageValue(language, defaultValue, printedValue);

        //Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("en", "Value", null)]
    [InlineData("en", "Value", "Wrong Value 2")]
    [InlineData("fr", null, "Value")]
    [InlineData("fr", "Wrong Value", "Value")]
    public void Should_Have_Value(string language, string? defaultValue, string? printedValue)
    {
        //Arrange
        var expected = "Value";

        // Act
        var result = LanguageHelper.GetLanguageValue(language, defaultValue, printedValue);

        //Assert
        Assert.Equal(expected, result);
    }
}
