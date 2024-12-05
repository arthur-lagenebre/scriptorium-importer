using MTG.Importer.Models.Card;
using MTG.Importer.Models.Ruling;
using MTG.Scryfall.Importer.Helpers;
using MTG.Scryfall.Importer.Interfaces;
using MTG.Scryfall.Models;
using MTG.Scryfall.Models.Card;

namespace MTG.Scryfall.Importer.Builders;

public class PlanarBuilder : IScryfallBuilder
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
    private CardSet _set;
    private List<Face> _cardFaces;
    private List<RelatedCard> _relatedCards;
    private string? _power;
    private string? _toughness;
    private string? _loyalty;
    private string? _handModifier;
    private string? _lifeModifier;
    private DateTime _releasedDate;

    public Layout Layout => new("planar");

    public PlanarBuilder()
    {
        Reset();
    }

    public void Reset()
    {
        _oracleId = Guid.Empty;
        _cost = new Cost(string.Empty, double.NaN);
        _language = string.Empty;
        _name = new Name(string.Empty, string.Empty);
        _typeline = new Typeline(string.Empty, string.Empty);
        _text = new Text(string.Empty, string.Empty);
        _color = Color.Unknown;
        _colorIdentity = Color.Unknown;
        _colorIndicator = Color.Unknown;
        _keywords = [];
        _producedMana = [];
        _cardFaces = [];
        _relatedCards = [];
        _power = null;
        _toughness = null;
        _loyalty = null;
        _handModifier = null;
        _lifeModifier = null;
        _releasedDate = DateTime.MinValue;
    }

    public Card Build(List<Ruling> rulings)
    {
        var card = new Card(_oracleId, _name, _typeline, _text, _cost, _releasedDate, _language, _color, _colorIdentity, _colorIndicator, Layout.Name, _keywords, _producedMana, _set, _cardFaces, _relatedCards, rulings, _power, _toughness, _loyalty, _handModifier, _lifeModifier);

        Reset();

        return card;
    }

    public IScryfallBuilder AddCardFaces(List<ScryfallCardFace>? scryfallCardFaces)
    {
        throw new NotSupportedException();
    }

    public IScryfallBuilder AddColors(List<string>? colors, List<string> colorIdentity, List<string>? colorIndicator)
    {
        _color = ColorHelper.GetCardColor(colors);
        _colorIdentity = ColorHelper.GetCardColor(colorIdentity);
        _colorIndicator = ColorHelper.GetCardColor(colorIndicator);
        return this;
    }

    public IScryfallBuilder AddCost(string? manacost, double manaValue)
    {
        _cost = new Cost(StringHelper.GetDefaultValue(manacost), DoubleHelper.GetDefaultValue(manaValue));
        return this;
    }

    public IScryfallBuilder AddCreature(string? power, string? toughness)
    {
        throw new NotSupportedException();
    }

    public IScryfallBuilder AddKeywords(List<string> keywords)
    {
        _keywords.AddRange(keywords);
        return this;
    }

    public IScryfallBuilder AddReleasedDate(string? releasedDate)
    {
        _releasedDate = DateHelper.GetDate(releasedDate);
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
        _oracleId = GuidHelper.GetGuid(oracleId);
        return this;
    }

    public IScryfallBuilder AddPlaneswalker(string? loyalty)
    {
        throw new NotSupportedException();
    }

    public IScryfallBuilder AddProducedMana(List<string>? producedMana)
    {
        if (producedMana != null)
            _producedMana.AddRange(producedMana);
        return this;
    }

    public IScryfallBuilder AddRelatedCards(List<ScryfallAllPart>? scryfallAllParts, string scryfallId)
    {
        if (scryfallAllParts != null)
            _relatedCards.AddRange(RelatedCardHelper.CreateRelatedCards(scryfallAllParts, scryfallId));
        return this;
    }

    public IScryfallBuilder AddSet(Guid setId, string? collectorNumber, string? rarity, List<Guid>? artistsId, List<string> normalImagesUrl, List<string> smallImagesUrl, string? flavorText, string? flavorName)
    {
        var flavors = new List<Flavor> { new(_language, StringHelper.GetDefaultValue(flavorText), StringHelper.GetDefaultValue(flavorName)) };
        var cardSetFaces = new List<CardSetFace> { new(0, artistsId, flavors) };

        _set = new CardSet(setId, StringHelper.GetDefaultValue(collectorNumber), StringHelper.GetDefaultValue(rarity), normalImagesUrl, smallImagesUrl, cardSetFaces);
        return this;
    }

    public IScryfallBuilder AddText(string? oracleText, string? printedText)
    {
        _text = new Text(_language, LanguageHelper.GetLanguageValue(_language, oracleText, printedText));
        return this;
    }

    public IScryfallBuilder AddTypeLine(string typeline, string? printedTypeline)
    {
        _typeline = new Typeline(_language, LanguageHelper.GetLanguageValue(_language, typeline, printedTypeline));
        return this;
    }

    public IScryfallBuilder AddVanguard(string? handModifier, string? lifeModifier)
    {
        throw new NotSupportedException();
    }
}
