using MTG.Scryfall.Importer.Helpers;

namespace MTG.Scryfall.Importer.Tests.Helpers;

public class DateHelperTests
{
    [Fact]
    public void ShouldHaveDateMinWhenDateIsEmpty()
    {
        //Arrange
        var date = string.Empty;
        var expected = DateTime.MinValue;

        // Act
        var result = DateHelper.GetDate(date);

        //Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void ShouldHaveDateMinWhenDateIsNull()
    {
        //Arrange
        var expected = DateTime.MinValue;

        // Act
        var result = DateHelper.GetDate(null);

        //Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void ShouldHaveDateWhenDateIsCorrect()
    {
        //Arrange
        var date = "2024-01-01";
        var expected = new DateTime(2024, 1, 1);

        // Act
        var result = DateHelper.GetDate(date);

        //Assert
        Assert.Equal(expected, result);
    }
}
