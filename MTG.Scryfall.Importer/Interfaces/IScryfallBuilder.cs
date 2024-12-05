using MTG.Importer.Models.Card;
using MTG.Importer.Models.Ruling;
using MTG.Scryfall.Models;
using MTG.Scryfall.Models.Card;

namespace MTG.Scryfall.Importer.Interfaces;

public interface IScryfallBuilder
{
    Layout Layout { get; }
    Card Build(List<Ruling> rulings);
    IScryfallBuilder AddCardFaces(List<ScryfallCardFace>? scryfallCardFaces);
    IScryfallBuilder AddColors(List<string>? colors, List<string> colorIdentity, List<string>? colorIndicator);
    IScryfallBuilder AddCost(string? manacost, double manaValue);
    IScryfallBuilder AddCreature(string? power, string? toughness);
    IScryfallBuilder AddKeywords(List<string> keywords);
    IScryfallBuilder AddReleasedDate(string? releasedDate);
    IScryfallBuilder AddLanguage(ScryfallLanguage language);
    IScryfallBuilder AddName(string name, string? printedName);
    IScryfallBuilder AddOracleId(Guid? oracleId);
    IScryfallBuilder AddPlaneswalker(string? loyalty);
    IScryfallBuilder AddProducedMana(List<string>? producedMana);
    IScryfallBuilder AddRelatedCards(List<ScryfallAllPart>? scryfallAllParts, string scryfallId);
    IScryfallBuilder AddSet(Guid setId, string? collectorNumber, string? rarity, List<Guid>? artistsId, List<string> normalImagesUrl, List<string> smallImagesUrl, string? flavorText, string? flavorName);
    IScryfallBuilder AddText(string? oracleText, string? printedText);
    IScryfallBuilder AddTypeLine(string typeline, string? printedTypeline);
    IScryfallBuilder AddVanguard(string? handModifier, string? lifeModifier);
}
