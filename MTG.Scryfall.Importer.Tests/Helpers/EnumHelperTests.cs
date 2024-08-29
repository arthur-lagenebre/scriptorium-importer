using MTG.Importer.Models.Card;
using MTG.Scryfall.Importer.Helpers;

namespace MTG.Scryfall.Importer.Tests.Helpers;

public class EnumHelperTests
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("Invalid")]
    public void Should_Have_Unknown(string? relatedCardComponent)
    {
        // Arrange
        var expected = RelatedCardComponent.Unknown;

        // Act
        var result = EnumHelper.GetRelatedCardComponent(relatedCardComponent);

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("token", RelatedCardComponent.Token)]
    [InlineData("meld_part", RelatedCardComponent.MeldPart)]
    [InlineData("meld_result", RelatedCardComponent.MeldResult)]
    [InlineData("combo_piece", RelatedCardComponent.ComboPiece)]
    public void Should_Have_Good_Result_When_Input_Is_Correct(string? relatedCardComponent, RelatedCardComponent expected)
    {
        // Act
        var result = EnumHelper.GetRelatedCardComponent(relatedCardComponent);

        // Assert
        Assert.Equal(expected, result);
    }
}
