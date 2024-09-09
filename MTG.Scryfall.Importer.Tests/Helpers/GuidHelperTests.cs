using MTG.Scryfall.Importer.Helpers;

namespace MTG.Scryfall.Importer.Tests.Helpers;

public class GuidHelperTests
{
    [Fact]
    public void Should_Throw_ArgumentNullException_When_Guid_Is_Null()
    {
        Assert.Throws<ArgumentNullException>(() => GuidHelper.GetGuid(null));
    }

    [Fact]
    public void Should_Throw_ArgumentNullException_When_Guid_Is_Empty()
    {
        Assert.Throws<ArgumentNullException>(() => GuidHelper.GetGuid(Guid.Empty));
    }

    [Fact]
    public void Should_Have_Guid_When_Guid_Is_OK()
    {
        // Arrange
        Guid? guid = Guid.Parse("443bf311-2ebc-4972-b6a8-b9b993223b5c");
        var expected = Guid.Parse("443bf311-2ebc-4972-b6a8-b9b993223b5c");

        // Act
        var result = GuidHelper.GetGuid(guid);

        // Assert
        Assert.Equal(expected, result);
    }
}
