using MTG.Scryfall.Importer.Helpers;

namespace MTG.Scryfall.Importer.Tests.Helpers;

public class DateHelperTests
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void Should_Have_Date_Min_When_Date_Is_Incorrect(string? date)
    {
        //Arrange
        var expected = DateTime.MinValue;

        // Act
        var result = DateHelper.GetDate(date);

        //Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Should_Have_Date_When_Date_Is_Correct()
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
