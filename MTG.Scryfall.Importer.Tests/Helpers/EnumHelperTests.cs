using MTG.Importer.Models.Card;
using MTG.Scryfall.Importer.Helpers;

namespace MTG.Scryfall.Importer.Tests.Helpers;

public class EnumHelperTests
{
    [Fact]
    public void ShouldHaveUnknownWhenInputIsEmpty()
    {
        //Arrange
        var relatedCardComponent = string.Empty;
        var expected = RelatedCardComponent.Unknown;

        // Act
        var result = EnumHelper.GetRelatedCardComponent(relatedCardComponent);

        //Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void ShouldHaveUnknownWhenInputIsNull()
    {
        //Arrange
        var expected = RelatedCardComponent.Unknown;

        // Act
        var result = EnumHelper.GetRelatedCardComponent(null);

        //Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void ShouldHaveUnknownWhenInputIsNotValidOption()
    {
        //Arrange
        var relatedCardComponent = "Random";
        var expected = RelatedCardComponent.Unknown;

        // Act
        var result = EnumHelper.GetRelatedCardComponent(relatedCardComponent);

        //Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void ShouldHaveTokenWhenInputIsToken()
    {
        //Arrange
        var relatedCardComponent = "token";
        var expected = RelatedCardComponent.Token;

        // Act
        var result = EnumHelper.GetRelatedCardComponent(relatedCardComponent);

        //Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void ShouldHaveMeldPartWhenInputIsMeldPart()
    {
        //Arrange
        var relatedCardComponent = "meld_part";
        var expected = RelatedCardComponent.MeldPart;

        // Act
        var result = EnumHelper.GetRelatedCardComponent(relatedCardComponent);

        //Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void ShouldHaveMeldResultWhenInputIsMeldResult()
    {
        //Arrange
        var relatedCardComponent = "meld_result";
        var expected = RelatedCardComponent.MeldResult;

        // Act
        var result = EnumHelper.GetRelatedCardComponent(relatedCardComponent);

        //Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void ShouldHaveComboPieceWhenInputIsComboPiece()
    {
        //Arrange
        var relatedCardComponent = "combo_piece";
        var expected = RelatedCardComponent.ComboPiece;

        // Act
        var result = EnumHelper.GetRelatedCardComponent(relatedCardComponent);

        //Assert
        Assert.Equal(expected, result);
    }
}
