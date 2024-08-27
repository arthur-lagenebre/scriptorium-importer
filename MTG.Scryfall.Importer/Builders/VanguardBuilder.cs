using MTG.Importer.Models;
using MTG.Scryfall.Importer.Helpers;
using MTG.Scryfall.Importer.Interfaces;
using MTG.Scryfall.Models;

namespace MTG.Scryfall.Importer.Builders;

public class VanguardBuilder : IBuilder
{
    private const string _layout = "vanguard";

    private Guid _oracleId;
    private CardCost _cost;
    private string _language;
    private CardName _name;
    private CardTypeline _typeline;
    private CardText _text;
    private CardColor _color;
    private CardColor _colorIdentity;
    private CardColor _colorIndicator;
    private List<string> _keywords;
    private List<string> _producedMana;
    private CardSet _set;
    private List<CardFace> _cardFaces;
    private List<RelatedCard> _relatedCards;
    private CardCreature? _creature;
    private CardPlaneswalker? _planeswalker;
    private CardVanguard? _vanguard;

    public VanguardBuilder() => Reset();

    public void Reset()
    {
        _keywords = [];
        _producedMana = [];
        _cardFaces = [];
        _relatedCards = [];
        _creature = null;
        _planeswalker = null;
        _vanguard = null;
    }

    public Card GetCard()
    {
        var card = new Card(_oracleId, _cost, _language, _name, _text, _typeline, _color, _colorIdentity, _colorIndicator, _layout, _keywords, _producedMana, _set, _cardFaces, _relatedCards, _creature, _planeswalker, _vanguard);

        Reset();

        return card;
    }

    public IBuilder AddOracleId(Guid? oracleId)
    {
        ArgumentNullException.ThrowIfNull(oracleId, nameof(oracleId));

        _oracleId = oracleId.Value;
        return this;
    }

    public IBuilder AddLanguage(string? language)
    {
        ArgumentNullException.ThrowIfNull(language, nameof(language));

        _language = language;
        return this;
    }

    public IBuilder AddCardName(string? name, string? printedName)
    {
        _name = new CardName(_language, LanguageHelper.GetLanguageValue(_language, name, printedName));
        return this;
    }

    public IBuilder AddCardTypeLine(string? typeLine, string? printedTypeLine)
    {
        _typeline = new CardTypeline(_language, LanguageHelper.GetLanguageValue(_language, typeLine, printedTypeLine));
        return this;
    }

    public IBuilder AddCardText(string? oracleText, string? printedText)
    {
        _text = new CardText(_language, LanguageHelper.GetLanguageValue(_language, oracleText, printedText));
        return this;
    }

    public IBuilder AddColors(List<string>? colors, List<string>? colorIdentity, List<string>? colorIndicator)
    {
        return this;
    }

    public IBuilder AddCost(string? manacost, double manaValue)
    {
        _cost = new CardCost(StringHelper.GetDefaultValue(manacost), manaValue);
        return this;
    }

    public IBuilder AddKeywords(List<string>? keywords)
    {
        if (keywords != null)
            _keywords.AddRange(keywords);
        return this;
    }

    public IBuilder AddProducedMana(List<string>? producedMana)
    {
        if (producedMana != null)
            _producedMana.AddRange(producedMana);
        return this;
    }

    public IBuilder AddSet(string? set, string? artist, string? collectorNumber, string? rarity, string? flavorText, string? flavorName, string? releasedAt)
    {
        var flavors = new List<CardFlavor> { new(0, StringHelper.GetDefaultValue(artist), StringHelper.GetDefaultValue(flavorText), StringHelper.GetDefaultValue(flavorName)) };

        _set = new CardSet(StringHelper.GetDefaultValue(set), StringHelper.GetDefaultValue(collectorNumber), StringHelper.GetDefaultValue(rarity), flavors, DateHelper.GetDate(releasedAt));
        return this;
    }

    public IBuilder AddCardFaces(List<ScryfallCardFace>? scryfallCardFaces)
    {
        return this;
    }

    public IBuilder AddRelatedCards(List<ScryfallAllPart>? scryfallAllParts)
    {
        return this;
    }

    public IBuilder AddCreature(string? power, string? toughness)
    {
        return this;
    }

    public IBuilder AddPlaneswalker(string? loyalty)
    {
        return this;
    }

    public IBuilder AddVanguard(string? handModifier, string? lifeModifier)
    {
        _vanguard = new CardVanguard(StringHelper.GetDefaultValue(handModifier), StringHelper.GetDefaultValue(lifeModifier));
        return this;
    }
}
