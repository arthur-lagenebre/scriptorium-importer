using MTG.Importer.Models;
using MTG.Scryfall.Models;

namespace MTG.Scryfall.Importer.Interfaces;

public interface IBuilder
{
    Card Build();
    IBuilder AddCardFaces(List<ScryfallCardFace>? scryfallCardFaces);
    IBuilder AddColors(List<string>? colors, List<string>? colorIdentity, List<string>? colorIndicator);
    IBuilder AddCost(string? manacost, double manaValue);
    IBuilder AddCreature(string? power, string? toughness);
    IBuilder AddKeywords(List<string>? keywords);
    IBuilder AddLanguage(string? language);
    IBuilder AddName(string? name, string? printedName);
    IBuilder AddOracleId(Guid? oracleId);
    IBuilder AddPlaneswalker(string? loyalty);
    IBuilder AddProducedMana(List<string>? producedMana);
    IBuilder AddRelatedCards(List<ScryfallAllPart>? scryfallAllParts);
    IBuilder AddSet(string? set, string? artist, string? collectorNumber, string? rarity, string? flavorText, string? flavorName, string? releasedAt);
    IBuilder AddText(string? oracleText, string? printedText);
    IBuilder AddTypeLine(string? typeLine, string? printedTypeLine);
    IBuilder AddVanguard(string? handModifier, string? lifeModifier);
}
