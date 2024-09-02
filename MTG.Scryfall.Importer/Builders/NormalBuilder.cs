using MTG.Importer.Models;
using MTG.Importer.Models.Card;
using MTG.Scryfall.Importer.Helpers;
using MTG.Scryfall.Importer.Interfaces;
using MTG.Scryfall.Models.Card;

namespace MTG.Scryfall.Importer.Builders;

public class NormalBuilder : IScryfallBuilder
{
    private Guid _oracleId;
    private Cost _cost;
    private string _language;
    private Name _name;
    private Typeline _typeline;
    private Text _text;
    private Color _color;
    private Color _colorIdentity;
    private Color _colorIndicator;
    private List<string> _keywords;
    private List<string> _producedMana;
    private Set _set;
    private List<Face> _cardFaces;
    private List<RelatedCard> _relatedCards;
    private Creature? _creature;
    private Planeswalker? _planeswalker;
    private Vanguard? _vanguard;

    public Layout Layout => new("normal");

    private ITypelineManager _typelineManager;

    public NormalBuilder(ITypelineManager typelineManager)
    {
        _typelineManager = typelineManager;
        Reset();
    }

    public void Reset()
    {
        _oracleId = Guid.Empty;
        _cost = new Cost(string.Empty, double.NaN);
        _language = string.Empty;
        _name = new Name(string.Empty, string.Empty);
        _typeline = new Typeline([], [], []);
        _text = new Text(string.Empty, string.Empty);
        _color = Color.Unknown;
        _colorIdentity = Color.Unknown;
        _colorIndicator = Color.Unknown;
        _keywords = [];
        _producedMana = [];
        _cardFaces = [];
        _relatedCards = [];
        _creature = null;
        _planeswalker = null;
        _vanguard = null;
    }

    public Card Build()
    {
        var card = new Card(_oracleId, _name, _typeline, _text, _cost, _color, _colorIdentity, _colorIndicator, Layout.Name, _keywords, _producedMana, _set, _cardFaces, _relatedCards, _creature, _planeswalker, _vanguard);

        Reset();

        return card;
    }

    public IScryfallBuilder AddCardFaces(List<ScryfallCardFace>? scryfallCardFaces)
    {
        return this;
    }

    public IScryfallBuilder AddColors(List<string>? colors, List<string>? colorIdentity, List<string>? colorIndicator)
    {
        _color = ColorHelper.GetCardColor(colors);
        _colorIdentity = ColorHelper.GetCardColor(colorIdentity);
        _colorIndicator = ColorHelper.GetCardColor(colorIndicator);
        return this;
    }

    public IScryfallBuilder AddCost(string? manacost, double manaValue)
    {
        _cost = new Cost(StringHelper.GetDefaultValue(manacost), manaValue);
        return this;
    }

    public IScryfallBuilder AddCreature(string? power, string? toughness)
    {
        if (!string.IsNullOrWhiteSpace(power) && !string.IsNullOrWhiteSpace(toughness))
            _creature = new Creature(power, toughness);
        return this;
    }

    public IScryfallBuilder AddKeywords(List<string>? keywords)
    {
        if (keywords != null)
            _keywords.AddRange(keywords);
        return this;
    }

    public IScryfallBuilder AddLanguage(ScryfallLanguage language)
    {
        _language = language.ToString();
        return this;
    }

    public IScryfallBuilder AddName(string? name, string? printedName)
    {
        _name = new Name(_language, LanguageHelper.GetLanguageValue(_language, name, printedName));
        return this;
    }

    public IScryfallBuilder AddOracleId(Guid? oracleId)
    {
        ArgumentNullException.ThrowIfNull(oracleId, nameof(oracleId));

        _oracleId = oracleId.Value;
        return this;
    }

    public IScryfallBuilder AddPlaneswalker(string? loyalty)
    {
        if (!string.IsNullOrWhiteSpace(loyalty))
            _planeswalker = new Planeswalker(loyalty);
        return this;
    }

    public IScryfallBuilder AddProducedMana(List<string>? producedMana)
    {
        if (producedMana != null)
            _producedMana.AddRange(producedMana);
        return this;
    }

    public IScryfallBuilder AddRelatedCards(List<ScryfallAllPart>? scryfallAllParts)
    {
        if (scryfallAllParts != null)
            foreach (var part in scryfallAllParts)
                _relatedCards.Add(new RelatedCard(EnumHelper.GetRelatedCardComponent(part.Component), StringHelper.GetDefaultValue(part.Name), StringHelper.GetDefaultValue(part.TypeLine)));
        return this;
    }

    public IScryfallBuilder AddSet(string? set, string? artist, string? collectorNumber, string? rarity, string? flavorText, string? flavorName, string? releasedAt)
    {
        var flavors = new List<Flavor> { new(0, StringHelper.GetDefaultValue(artist), StringHelper.GetDefaultValue(flavorText), StringHelper.GetDefaultValue(flavorName)) };

        _set = new Set(StringHelper.GetDefaultValue(set), StringHelper.GetDefaultValue(collectorNumber), StringHelper.GetDefaultValue(rarity), flavors, DateHelper.GetDate(releasedAt));
        return this;
    }

    public IScryfallBuilder AddText(string? oracleText, string? printedText)
    {
        _text = new Text(_language, LanguageHelper.GetLanguageValue(_language, oracleText, printedText));
        return this;
    }

    public IScryfallBuilder AddTypeLine(string? typeLine, string? printedTypeLine)
    {
        _typeline = _typelineManager.ExtractTypeline(typeLine);
        return this;
    }

    public IScryfallBuilder AddVanguard(string? handModifier, string? lifeModifier)
    {
        return this;
    }
}
