using MTG.Importer.Models.Card;
using MTG.Scryfall.Models;
using MTG.Scryfall.Models.Card;

namespace MTG.Scryfall.Importer.Interfaces;

public interface IScryfallBuilder
{
    Layout Layout { get; }
    Card Build();
    IScryfallBuilder AddCardFaces(List<ScryfallCardFace>? scryfallCardFaces);
    IScryfallBuilder AddColors(List<string>? colors, List<string> colorIdentity, List<string>? colorIndicator);
    IScryfallBuilder AddCost(string? manacost, double manaValue);
    IScryfallBuilder AddCreature(string? power, string? toughness);
    IScryfallBuilder AddKeywords(List<string> keywords);
    IScryfallBuilder AddLanguage(ScryfallLanguage language);
    IScryfallBuilder AddName(string name, string? printedName);
    IScryfallBuilder AddOracleId(Guid? oracleId);
    IScryfallBuilder AddPlaneswalker(string? loyalty);
    IScryfallBuilder AddProducedMana(List<string>? producedMana);
    IScryfallBuilder AddRelatedCards(List<ScryfallAllPart>? scryfallAllParts, string scryfallId);
    IScryfallBuilder AddSet(string? set, string? artist, string? collectorNumber, string? rarity, string? flavorText, string? flavorName);
    IScryfallBuilder AddText(string? oracleText, string? printedText);
    IScryfallBuilder AddTypeLine(string typeline);
    IScryfallBuilder AddVanguard(string? handModifier, string? lifeModifier);
}
