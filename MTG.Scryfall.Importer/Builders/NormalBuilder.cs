using MTG.Importer.Models;
using MTG.Scryfall.Importer.Helpers;
using MTG.Scryfall.Importer.Interfaces;
using MTG.Scryfall.Models;

namespace MTG.Scryfall.Importer.Builders;

public class NormalBuilder : IBuilder
{
    private const string _layout = "normal";

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

    public NormalBuilder() => Reset();

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
        _color = ColorHelper.GetCardColor(colors);
        _colorIdentity = ColorHelper.GetCardColor(colorIdentity);
        _colorIndicator = ColorHelper.GetCardColor(colorIndicator);
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
        _set = new CardSet(StringHelper.GetDefaultValue(set), StringHelper.GetDefaultValue(artist), StringHelper.GetDefaultValue(collectorNumber), StringHelper.GetDefaultValue(rarity), StringHelper.GetDefaultValue(flavorText), StringHelper.GetDefaultValue(flavorName), DateHelper.GetDate(releasedAt));
        return this;
    }

    public IBuilder AddCardFaces(List<ScryfallCardFace>? scryfallCardFaces)
    {
        if (scryfallCardFaces != null)
            foreach (var face in scryfallCardFaces)
                _cardFaces.Add(CreateCardFace(face));
        return this;
    }

    private CardFace CreateCardFace(ScryfallCardFace face)
    {
        var cost = new CardCost(StringHelper.GetDefaultValue(face.ManaCost), face.Cmc);
        var name = new CardName(_language, LanguageHelper.GetLanguageValue(_language, face.Name, face.PrintedName));
        var typeline = new CardTypeline(_language, LanguageHelper.GetLanguageValue(_language, face.TypeLine, face.PrintedTypeLine));
        var text = new CardText(_language, LanguageHelper.GetLanguageValue(_language, face.OracleText, face.PrintedText));
        var creature = !string.IsNullOrWhiteSpace(face.Power) && !string.IsNullOrWhiteSpace(face.Toughness) ? new CardCreature(face.Power, face.Toughness) : null;
        var planeswalker = !string.IsNullOrWhiteSpace(face.Loyalty) ? new CardPlaneswalker(face.Loyalty) : null;
        var battle = !string.IsNullOrWhiteSpace(face.Defense) ? new CardBattle(int.Parse(face.Defense)) : null;

        return new CardFace(cost, name, typeline, text, ColorHelper.GetCardColor(face.Colors), ColorHelper.GetCardColor(face.ColorIndicator), creature, planeswalker, battle);
    }

    public IBuilder AddRelatedCards(List<ScryfallAllPart>? scryfallAllParts)
    {
        if (scryfallAllParts != null)
            foreach (var part in scryfallAllParts)
                _relatedCards.Add(new RelatedCard(RelatedCardComponentHelper.GetRelatedCardComponent(part.Component), StringHelper.GetDefaultValue(part.Name), StringHelper.GetDefaultValue(part.TypeLine)));
        return this;
    }

    public IBuilder AddCreature(string? power, string? toughness)
    {
        if (!string.IsNullOrWhiteSpace(power) && !string.IsNullOrWhiteSpace(toughness))
            _creature = new CardCreature(power, toughness);
        return this;
    }

    public IBuilder AddPlaneswalker(string? loyalty)
    {
        if (!string.IsNullOrWhiteSpace(loyalty))
            _planeswalker = new CardPlaneswalker(loyalty);
        return this;
    }

    public IBuilder AddVanguard(string? handModifier, string? lifeModifier)
    {
        return this;
    }
}
