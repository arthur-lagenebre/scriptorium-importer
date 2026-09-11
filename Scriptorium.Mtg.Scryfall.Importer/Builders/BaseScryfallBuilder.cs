using Scriptorium.Mtg.Importer.Models.Card;
using Scriptorium.Mtg.Importer.Models.Ruling;
using Scriptorium.Mtg.Scryfall.Importer.Helpers;
using Scriptorium.Mtg.Scryfall.Importer.Interfaces;
using Scriptorium.Mtg.Scryfall.Models;
using Scriptorium.Mtg.Scryfall.Models.Card;

namespace Scriptorium.Mtg.Scryfall.Importer.Builders;

public abstract class BaseScryfallBuilder : IScryfallBuilder
{
    protected Guid _oracleId = Guid.Empty;
    protected Cost _cost = new Cost(string.Empty, double.NaN);
    protected string _language = string.Empty;
    protected Name _name = new Name(string.Empty, string.Empty);
    protected Typeline _typeline = new Typeline(string.Empty, string.Empty);
    protected Text _text = new Text(string.Empty, string.Empty);
    protected Color _color = Color.Unknown;
    protected Color _colorIdentity = Color.Unknown;
    protected Color _colorIndicator = Color.Unknown;
    protected List<string> _keywords = [];
    protected List<string> _producedMana = [];
    protected CardSet _set = new CardSet(Guid.Empty, string.Empty, string.Empty, [], [], []);
    protected List<Face> _cardFaces = [];
    protected List<RelatedCard> _relatedCards = [];
    protected string? _power;
    protected string? _toughness;
    protected string? _loyalty;
    protected string? _handModifier;
    protected string? _lifeModifier;
    protected Dictionary<int, Flavor> _flavors = [];
    protected DateTime _releasedDate = DateTime.MinValue;

    protected BaseScryfallBuilder()
    {
        Reset();
    }

    public abstract Layout Layout { get; }

    public virtual void Reset()
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
        _flavors = [];
        _releasedDate = DateTime.MinValue;
    }

    public virtual Card Build(List<Ruling> rulings)
    {
        var card = new Card(_oracleId, _name, _typeline, _text, _cost, _releasedDate, _language, _color, _colorIdentity, _colorIndicator, Layout.Name, _keywords, _producedMana, _set, _cardFaces, _relatedCards, rulings, _power, _toughness, _loyalty, _handModifier, _lifeModifier);

        Reset();

        return card;
    }

    public virtual IScryfallBuilder AddCardFaces(List<ScryfallCardFace>? scryfallCardFaces)
    {
        if (scryfallCardFaces != null)
        {
            var (Faces, Flavors) = CardFaceHelper.CreateCardFacesInformations(scryfallCardFaces, _language);
            _cardFaces.AddRange(Faces);
            _flavors = Flavors;
            _name = new Name(_language, string.Join(" // ", _cardFaces.OrderBy(x => x.FaceId).Select(x => x.Name.Value)));
        }
        return this;
    }

    public virtual IScryfallBuilder AddColors(List<string>? colors, List<string> colorIdentity, List<string>? colorIndicator)
    {
        _color = ColorHelper.GetCardColor(colors);
        _colorIdentity = ColorHelper.GetCardColor(colorIdentity);
        _colorIndicator = ColorHelper.GetCardColor(colorIndicator);
        return this;
    }

    public virtual IScryfallBuilder AddCost(string? manacost, double manaValue)
    {
        _cost = new Cost(StringHelper.GetDefaultValue(manacost), DoubleHelper.GetDefaultValue(manaValue));
        return this;
    }

    public virtual IScryfallBuilder AddCreature(string? power, string? toughness)
    {
        _power = power;
        _toughness = toughness;
        return this;
    }

    public virtual IScryfallBuilder AddKeywords(List<string> keywords)
    {
        _keywords.AddRange(keywords);
        return this;
    }

    public virtual IScryfallBuilder AddReleasedDate(string? releasedDate)
    {
        _releasedDate = DateHelper.GetDate(releasedDate);
        return this;
    }

    public virtual IScryfallBuilder AddLanguage(ScryfallLanguage language)
    {
        _language = language.ToString();
        return this;
    }

    public virtual IScryfallBuilder AddName(string name, string? printedName)
    {
        _name = new Name(_language, LanguageHelper.GetLanguageValue(_language, name, printedName));
        return this;
    }

    public virtual IScryfallBuilder AddOracleId(Guid? oracleId)
    {
        _oracleId = GuidHelper.GetGuid(oracleId);
        return this;
    }

    public virtual IScryfallBuilder AddPlaneswalker(string? loyalty)
    {
        _loyalty = loyalty;
        return this;
    }

    public virtual IScryfallBuilder AddProducedMana(List<string>? producedMana)
    {
        if (producedMana != null)
            _producedMana.AddRange(producedMana);
        return this;
    }

    public virtual IScryfallBuilder AddRelatedCards(List<ScryfallAllPart>? scryfallAllParts, string scryfallId)
    {
        if (scryfallAllParts != null)
            _relatedCards.AddRange(RelatedCardHelper.CreateRelatedCards(scryfallAllParts, scryfallId));
        return this;
    }

    public virtual IScryfallBuilder AddSet(Guid setId, string? collectorNumber, string? rarity, List<Guid>? artistsId, List<string> normalImagesUrl, List<string> smallImagesUrl, string? flavorText, string? flavorName)
    {
        var flavors = new List<Flavor> { new(_language, StringHelper.GetDefaultValue(flavorText), StringHelper.GetDefaultValue(flavorName)) };
        var cardSetFaces = new List<CardSetFace> { new(0, artistsId, flavors) };

        _set = new CardSet(setId, StringHelper.GetDefaultValue(collectorNumber), StringHelper.GetDefaultValue(rarity), normalImagesUrl, smallImagesUrl, cardSetFaces);
        return this;
    }

    public virtual IScryfallBuilder AddText(string? oracleText, string? printedText)
    {
        _text = new Text(_language, LanguageHelper.GetLanguageValue(_language, oracleText, printedText));
        return this;
    }

    public virtual IScryfallBuilder AddTypeLine(string typeline, string? printedTypeline)
    {
        _typeline = new Typeline(_language, LanguageHelper.GetLanguageValue(_language, typeline, printedTypeline));
        return this;
    }

    public virtual IScryfallBuilder AddVanguard(string? handModifier, string? lifeModifier)
    {
        _handModifier = StringHelper.GetDefaultValue(handModifier);
        _lifeModifier = StringHelper.GetDefaultValue(lifeModifier);
        return this;
    }
}
