using MTG.Importer.Models.Card;
using MTG.Scryfall.Importer.Interfaces;
using MTG.Scryfall.Models.Card;

namespace MTG.Scryfall.Importer.Helpers;

public static class CardFaceHelper
{
    public static List<Face> CreateCardFaces(List<ScryfallCardFace> cardFaces, string language, IScryfallTypelineManager typelineManager)
    {
        var faces = new List<Face>();

        foreach (var cardFace in cardFaces.Select((ScryfallCardFace, Index) => (ScryfallCardFace, Index)))
            faces.Add(CreateCardFace(cardFace.Index, cardFace.ScryfallCardFace, language, typelineManager));

        return faces;
    }

    private static Face CreateCardFace(int index, ScryfallCardFace face, string language, IScryfallTypelineManager typelineManager)
    {
        var cost = new Cost(StringHelper.GetDefaultValue(face.ManaCost), face.ManaValue);
        var name = new Name(language, LanguageHelper.GetLanguageValue(language, face.Name, face.PrintedName));
        var typeline = typelineManager.ExtractTypeline(face.TypeLine);
        var text = new Text(language, LanguageHelper.GetLanguageValue(language, face.OracleText, face.PrintedText));
        var creature = !string.IsNullOrWhiteSpace(face.Power) && !string.IsNullOrWhiteSpace(face.Toughness) ? new Creature(face.Power, face.Toughness) : null;
        var planeswalker = !string.IsNullOrWhiteSpace(face.Loyalty) ? new Planeswalker(face.Loyalty) : null;
        var battle = !string.IsNullOrWhiteSpace(face.Defense) ? new Battle(int.Parse(face.Defense)) : null;

        return new Face(index, cost, name, typeline, text, ColorHelper.GetCardColor(face.Colors), ColorHelper.GetCardColor(face.ColorIndicator), creature, planeswalker, battle);
    }
}
