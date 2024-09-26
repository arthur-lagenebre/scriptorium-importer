using MTG.Importer.Models.Card;
using MTG.Scryfall.Importer.Helpers;
using MTG.Scryfall.Importer.Interfaces;
using MTG.Scryfall.Models.Card;
using NSubstitute;

namespace MTG.Scryfall.Importer.Tests.Helpers;

public class CardFaceHelperTests
{
    private readonly IScryfallTypelineManager _scryfallTypelineManager = Substitute.For<IScryfallTypelineManager>();
    private readonly List<ScryfallCardFace> _scryfallCardFaces;

    public CardFaceHelperTests() => _scryfallCardFaces =
        [
            new ScryfallCardFace() {
                    OracleId = null,
                    Name = "Obyra's Attendants",
                    PrintedName = null,
                    ManaCost = "{4}{U}",
                    ManaValue = 0.0,
                    TypeLine = "Creature — Faerie Wizard",
                    OracleText = "Flying",
                    PrintedText = null,
                    Artist = "Andreas Zafiratos",
                    FlavorName = null,
                    Layout = null,
                    Colors = null,
                    Defense = null,
                    Power = "3",
                    Toughness = "4",
                    Loyalty = null,
                    ColorIndicator = null,
                    FlavorText = "Obyra's devoted servants shrieked as their sleeping mistress slashed at them, unseeing."
            },
            new ScryfallCardFace() {
                    OracleId = null,
                    Name = "Desperate Parry",
                    PrintedName = null,
                    ManaCost = "{1}{U}",
                    ManaValue = 0.0,
                    TypeLine = "Instant — Adventure",
                    OracleText = "Target creature gets -4/-0 until end of turn. (Then exile this card. You may cast the creature later from exile.)",
                    PrintedText = null,
                    Artist = "Andreas Zafiratos",
                    FlavorName = null,
                    Layout = null,
                    Colors = null,
                    Defense = null,
                    Power = null,
                    Toughness = null,
                    Loyalty = null,
                    ColorIndicator = null,
                    FlavorText = null
            }
        ];

    [Fact]
    public void Should_Have_Two_Element_When_List_Have_Two_Elements()
    {
        // Act
        var result = CardFaceHelper.CreateCardFaces(_scryfallCardFaces, "en", _scryfallTypelineManager);

        // Assert
        Assert.Collection(result,
            e =>
            {
                Assert.Equal("en", e.Name.Language);
                Assert.Equal("Obyra's Attendants", e.Name.Value);
                Assert.Equal(e.Text.Language, e.Name.Language);
                Assert.Equal("Flying", e.Text.Value);
                Assert.Equal("3", e.Power);
                Assert.Equal("4", e.Toughness);
                Assert.Null(e.Loyalty);
            },
            e =>
            {
                Assert.Equal("Desperate Parry", e.Name.Value);
                Assert.Null(e.Power);
                Assert.Null(e.Toughness);
                Assert.Null(e.Loyalty);
            });
    }
}
