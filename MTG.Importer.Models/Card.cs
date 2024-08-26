namespace MTG.Importer.Models;

public record Card
{
    public Guid CardId { get; }
    public Guid OracleId { get; }
    public CardCost Cost { get; }
    public string Language { get; }
    public CardName Name { get; }
    public CardText Text { get; }
    public CardTypeline Typeline { get; }
    public CardColor Colors { get; }
    public CardColor ColorsIdentity { get; }
    public CardColor ColorsIndicator { get; }
    public string Layout { get; }
    public List<string> Keyword { get; }
    public List<string> ProducedMana { get; }
    public CardSet Set { get; }
    public List<CardFace> CardFaces { get; }
    public List<RelatedCard> RelatedCards { get; }
    public CardCreature? Creature { get; }
    public CardPlaneswalker? Planeswalker { get; }
    public CardVanguard? Vanguard { get; }

    public Card(Guid oracleId, CardCost cost, string language, CardName name, CardText text, CardTypeline typeline, CardColor colors, CardColor colorsIdentity, CardColor colorsIndicator, string layout, List<string> keyword, List<string> producedMana, CardSet set, List<CardFace> cardFaces, List<RelatedCard> relatedCards, CardCreature? creature, CardPlaneswalker? planeswalker, CardVanguard? vanguard)
    {
        OracleId = oracleId;
        Cost = cost;
        Language = language;
        Name = name;
        Typeline = typeline;
        Text = text;
        Colors = colors;
        ColorsIdentity = colorsIdentity;
        ColorsIndicator = colorsIndicator;
        Layout = layout;
        Keyword = keyword;
        ProducedMana = producedMana;
        Set = set;
        CardFaces = cardFaces;
        RelatedCards = relatedCards;
        Creature = creature;
        Planeswalker = planeswalker;
        Vanguard = vanguard;
    }
}
