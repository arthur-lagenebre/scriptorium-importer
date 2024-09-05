using MTG.Importer.Models.Card;
using MTG.Scryfall.Importer.Helpers;
using MTG.Scryfall.Importer.Interfaces;
using MTG.Scryfall.Models;
using MTG.Scryfall.Models.Card;

namespace MTG.Scryfall.Importer.Builders;

public class TransformBuilder : IScryfallBuilder
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

    public Layout Layout => new("transform");

    private IScryfallTypelineManager _typelineManager;

    public TransformBuilder(IScryfallTypelineManager typelineManager)
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
        if (scryfallCardFaces != null)
        {
            foreach (var face in scryfallCardFaces.Select((ScryfallCardFace, Index) => (ScryfallCardFace, Index)))
                _cardFaces.Add(CreateCardFace(face.Index, face.ScryfallCardFace));

            _name = new Name(_language, string.Join(" // ", _cardFaces.OrderBy(x => x.FaceId).Select(x => x.Name.Value)));
        }
        return this;
    }

    private Face CreateCardFace(int index, ScryfallCardFace face)
    {
        var cost = new Cost(StringHelper.GetDefaultValue(face.ManaCost), face.Cmc);
        var name = new Name(_language, LanguageHelper.GetLanguageValue(_language, face.Name, face.PrintedName));
        var typeline = _typelineManager.ExtractTypeline(face.TypeLine);
        var text = new Text(_language, LanguageHelper.GetLanguageValue(_language, face.OracleText, face.PrintedText));
        var creature = !string.IsNullOrWhiteSpace(face.Power) && !string.IsNullOrWhiteSpace(face.Toughness) ? new Creature(face.Power, face.Toughness) : null;
        var planeswalker = !string.IsNullOrWhiteSpace(face.Loyalty) ? new Planeswalker(face.Loyalty) : null;
        var battle = !string.IsNullOrWhiteSpace(face.Defense) ? new Battle(int.Parse(face.Defense)) : null;

        return new Face(index, cost, name, typeline, text, ColorHelper.GetCardColor(face.Colors), ColorHelper.GetCardColor(face.ColorIndicator), creature, planeswalker, battle);
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
        throw new NotSupportedException();
    }
}
