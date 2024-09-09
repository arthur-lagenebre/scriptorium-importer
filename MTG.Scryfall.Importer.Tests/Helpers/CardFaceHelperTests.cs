using MTG.Scryfall.Models.Card;

namespace MTG.Scryfall.Importer.Tests.Helpers;

public class CardFaceHelperTests
{
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
}
