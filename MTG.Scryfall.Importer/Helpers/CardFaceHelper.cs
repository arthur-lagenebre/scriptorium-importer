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
        var cost = new Cost(face.ManaCost, face.ManaValue);
        var name = new Name(language, LanguageHelper.GetLanguageValue(language, face.Name, face.PrintedName));
        var typeline = typelineManager.ExtractTypeline(face.TypeLine);
        var text = new Text(language, LanguageHelper.GetLanguageValue(language, StringHelper.GetDefaultValue(face.OracleText), face.PrintedText));
        var power = !string.IsNullOrWhiteSpace(face.Power) ? face.Power : null;
        var toughness = !string.IsNullOrWhiteSpace(face.Toughness) ? face.Toughness : null;
        var loyalty = !string.IsNullOrWhiteSpace(face.Loyalty) ? face.Loyalty : null;
        int? defense = !string.IsNullOrWhiteSpace(face.Defense) ? int.Parse(face.Defense) : null;

        return new Face(index, cost, name, typeline, text, ColorHelper.GetCardColor(face.Colors), ColorHelper.GetCardColor(face.ColorIndicator), power, toughness, loyalty, defense);
    }
}
