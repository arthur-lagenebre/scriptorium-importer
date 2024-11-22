using MTG.Importer.Models.Card;
using MTG.Scryfall.Importer.Helpers;
using MTG.Scryfall.Models.Card;

namespace MTG.Scryfall.Importer.Tests.Helpers;

public class RelatedCardHelperTests
{
    private readonly List<ScryfallAllPart> _scryfallAllPart =
    [
        new() {
            Id = "0001e77a-7fff-49d2-a55c-42f6fdf6db08",
            Component = "combo_piece",
            Name = "Obyra's Attendants // Desperate Parry",
            TypeLine = "Creature — Faerie Wizard // Instant — Adventure",
        },
        new() {
            Id = "fcf4c7fb-7859-4c11-8552-6817f5119d2e",
            Component = "combo_piece",
            Name = "On an Adventure",
            TypeLine = "Card"
        }
    ];

    [Fact]
    public void Should_Have_One_Element_When_List_Have_Two_Elements_And_Same_Id()
    {
        // Arrange
        const string oracleId = "0001e77a-7fff-49d2-a55c-42f6fdf6db08";

        // Act
        var result = RelatedCardHelper.CreateRelatedCards(_scryfallAllPart, oracleId);

        // Assert
        Assert.Collection(result,
            e => {
                Assert.Equal("On an Adventure", e.Name);
                Assert.Equal("Card", e.TypeLine);
                Assert.Equal(RelatedCardComponent.ComboPiece, e.Component);
            });
    }

    [Fact]
    public void Should_Have_Two_Elements_When_List_Have_Two_Elements_And_Different_Id()
    {
        // Arrange
        const string oracleId = "0001e77a-7fff-49d2-a55c-42f6fdf6db09";

        // Act
        var result = RelatedCardHelper.CreateRelatedCards(_scryfallAllPart, oracleId);

        // Assert
        Assert.Collection(result,
            e => { Assert.Equal("Obyra's Attendants // Desperate Parry", e.Name); },
            e => { Assert.Equal("On an Adventure", e.Name); });
    }
}
