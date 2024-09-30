using MTG.Importer.Models.Card;
using MTG.Scryfall.Importer.Builders;
using MTG.Scryfall.Importer.Interfaces;
using MTG.Scryfall.Models.Card;
using NSubstitute;

namespace MTG.Scryfall.Importer.Tests.Builders;

public class AdventureBuilderTests
{
    private readonly AdventureBuilder _builder = new(Substitute.For<IScryfallTypelineManager>());
    private readonly ScryfallCard _scryfallCard;

    public AdventureBuilderTests()
    {
        _scryfallCard = new ScryfallCard
        {
            Id = "0001e77a-7fff-49d2-a55c-42f6fdf6db08",
            OracleId = Guid.Parse("396b088d-f9af-4ee1-843f-dbe1633f9cc8"),
            Name = "Obyra's Attendants // Desperate Parry",
            PrintedName = null,
            OracleText = null,
            PrintedText = null,
            TypeLine = "Creature — Faerie Wizard // Instant — Adventure",
            Lang = ScryfallLanguage.en,
            ReleasedAt = "2023-09-08",
            Layout = "adventure",
            ManaCost = "{4}{U} // {1}{U}",
            ManaValue = 5.0,
            Power = "3",
            Toughness = "4",
            Loyalty = null,
            Colors = ["U"],
            ColorIdentity = ["U"],
            ColorIndicator = null,
            ProducedMana = null,
            Keywords = ["Flying"],
            AllParts = [
                new ScryfallAllPart {
                    Id = "0001e77a-7fff-49d2-a55c-42f6fdf6db08",
                    Component = "combo_piece",
                    Name = "Obyra's Attendants // Desperate Parry",
                    TypeLine = "Creature — Faerie Wizard // Instant — Adventure"
                },
                new ScryfallAllPart {
                    Id = "fcf4c7fb-7859-4c11-8552-6817f5119d2e",
                    Component = "combo_piece",
                    Name = "On an Adventure",
                    TypeLine = "Card"
                }
                ],
            CardFaces = [
                new ScryfallCardFace {
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
                new ScryfallCardFace {
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
                ],
            FlavorName = null,
            Set = "woe",
            CollectorNumber = "63",
            Digital = false,
            Rarity = "common",
            FlavorText = "Obyra's devoted servants shrieked as their sleeping mistress slashed at them, unseeing.",
            Artist = "Andreas Zafiratos",
            HandModifier = null,
            LifeModifier = null
        };
    }

    [Fact]
    public void Should_Throw_Exception_When_Oracle_Is_Null()
    {
        // Arrange & Act & Assert
        Assert.Throws<ArgumentNullException>(() => _builder.AddOracleId(null));
    }

    [Fact]
    public void Should_Throw_Exception_When_Oracle_Is_Empty()
    {
        // Arrange
        var oracleId = Guid.Empty;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => _builder.AddOracleId(oracleId));
    }

    [Fact]
    public void Should_Have_Same_OracleId_When_OracleId_Is_Set()
    {
        // Arrange
        var oracleId = Guid.Parse("396b088d-f9af-4ee1-843f-dbe1633f9cc8");

        // Act
        _builder.AddOracleId(oracleId);
        var card = _builder.Build();

        // Assert
        Assert.Equal(card.OracleId, oracleId);
    }

    [Fact]
    public void Should_Throw_Exception_When_Call_Planeswalker()
    {
        // Arrange
        var loyalty = "5";

        // Act & Assert
        Assert.Throws<NotSupportedException>(() => _builder.AddPlaneswalker(loyalty));
    }

    [Fact]
    public void Should_Throw_Exception_When_Call_Creature()
    {
        // Arrange
        var power = "1";
        var toughness = "1";

        // Act & Assert
        Assert.Throws<NotSupportedException>(() => _builder.AddCreature(power, toughness));
    }

    [Fact]
    public void Should_Throw_Exception_When_Call_Vanguard()
    {
        // Arrange
        var handModifier = "+1";
        var lifeModifier = "+1";

        // Act & Assert
        Assert.Throws<NotSupportedException>(() => _builder.AddVanguard(handModifier, lifeModifier));
    }

    [Fact]
    public void Should_Have_Same_Colors_When_Colors_Are_Set()
    {
        // Arrange
        var colors = new List<string> { "W" };
        var colorsExpected = Color.W;
        var colorIdentity = new List<string> { "U" };
        var colorIdentityExpected = Color.U;
        var colorIndicator = new List<string> { "W", "U" };
        var colorIndicatorExpected = Color.W | Color.U;

        // Act
        _builder.AddColors(colors, colorIdentity, colorIndicator);
        var card = _builder.Build();

        // Assert
        Assert.Equal(colorsExpected, card.Colors);
        Assert.Equal(colorIdentityExpected, card.ColorsIdentity);
        Assert.Equal(colorIndicatorExpected, card.ColorsIndicator);
    }

    [Fact]
    public void Should_Have_None_When_Colors_Are_Empty()
    {
        // Arrange
        var colors = new List<string>();
        var colorIdentity = new List<string>();
        var colorIndicator = new List<string>();
        var colorsExpected = Color.None;

        // Act
        _builder.AddColors(colors, colorIdentity, colorIndicator);
        var card = _builder.Build();

        // Assert
        Assert.Equal(colorsExpected, card.Colors);
        Assert.Equal(colorsExpected, card.ColorsIdentity);
        Assert.Equal(colorsExpected, card.ColorsIndicator);
    }

    [Theory]
    [InlineData(null, 0.0, "", 0.0)]
    [InlineData("{10}{W}", 11.0, "{10}{W}", 11.0)]
    public void Should_Have_Corresponding_Values_When_Manacost_Is_Set(string? manacost, double manaValue, string manacostExpected, double manaValueExpected)
    {
        // Act
        _builder.AddCost(manacost, manaValue);
        var card = _builder.Build();

        // Assert
        Assert.NotNull(card.Cost);
        Assert.Equal(manacostExpected, card.Cost.ManaCost);
        Assert.Equal(manaValueExpected, card.Cost.ManaValue);
    }

    [Fact]
    public void Should_Have_Keyword_Empty_If_List_Is_Empty()
    {
        // Act
        _builder.AddKeywords([]);
        var card = _builder.Build();

        // Assert
        Assert.Empty(card.Keyword);
    }

    [Fact]
    public void Should_Have_Keyword_With_One_Element_If_()
    {
        // Arrange
        var keywords = new List<string> { "Flying" };
        var expectedKeywords = "Flying";

        // Act
        _builder.AddKeywords(keywords);
        var card = _builder.Build();

        // Assert
        Assert.Collection(card.Keyword, e => { Assert.Equal(expectedKeywords, e); });
    }
}
