using MTG.Scryfall.Importer.Helpers;

namespace MTG.Scryfall.Importer.Tests.Helpers;

public class LanguageHelperTests
{
    [Fact]
    public void ShouldHaveEmptyStringWhenLanguageIsENAndDefaultValueIsEmpty()
    {
        //Arrange
        var language = "en";
        var defaultValue = string.Empty;
        var printedValue = string.Empty;
        var expected = string.Empty;

        // Act
        var result = LanguageHelper.GetLanguageValue(language, defaultValue, printedValue);

        //Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void ShouldHaveEmptyStringWhenLanguageIsENAndDefaultValueIsNull()
    {
        //Arrange
        var language = "en";
        var printedValue = string.Empty;
        var expected = string.Empty;

        // Act
        var result = LanguageHelper.GetLanguageValue(language, null, printedValue);

        //Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void ShouldHaveENNameWhenLanguageIsENAndDefaultValueIsENNameAndPrintedNameIsFRName()
    {
        //Arrange
        var language = "en";
        var defaultValue = "ENName";
        var printedValue = "FRName";
        var expected = "ENName";

        // Act
        var result = LanguageHelper.GetLanguageValue(language, defaultValue, printedValue);

        //Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void ShouldHaveEmptyStringWhenLanguageIsFRAndPrintedValueIsEmpty()
    {
        //Arrange
        var language = "fr";
        var defaultValue = string.Empty;
        var printedValue = string.Empty;
        var expected = string.Empty;

        // Act
        var result = LanguageHelper.GetLanguageValue(language, defaultValue, printedValue);

        //Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void ShouldHaveEmptyStringWhenLanguageIsFRAndPrintedValueIsNull()
    {
        //Arrange
        var language = "fr";
        var defaultValue = string.Empty;
        var expected = string.Empty;

        // Act
        var result = LanguageHelper.GetLanguageValue(language, defaultValue, null);

        //Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void ShouldHaveFRNameWhenLanguageIsFRAndDefaultValueIsENNameAndPrintedNameIsFRName()
    {
        //Arrange
        var language = "fr";
        var defaultValue = "ENName";
        var printedValue = "FRName";
        var expected = "FRName";

        // Act
        var result = LanguageHelper.GetLanguageValue(language, defaultValue, printedValue);

        //Assert
        Assert.Equal(expected, result);
    }
}
