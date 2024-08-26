namespace MTG.Importer.Models;

public record CardFace
{
    public CardCost CardCost { get; }
    public CardName Name { get; }
    public CardTypeline TypeLine { get; }
    public CardText Text { get; }
    public CardColor Colors { get; }
    public CardColor ColorsIndicator { get; }
    public CardCreature? Creature { get; }
    public CardPlaneswalker? Planeswalker { get; }
    public CardBattle? Battle { get; }

    //A voir comment on regroupe cela avec le set
    public string? Artist { get; }
    public string? FlavorName { get; }
    public string? FlavorText { get; }

    public CardFace(CardCost cardCost, CardName name, CardTypeline typeLine, CardText text, CardColor colors, CardColor colorsIndicator, CardCreature? creature, CardPlaneswalker? planeswalker, CardBattle? battle)
    {
        CardCost = cardCost;
        Name = name;
        TypeLine = typeLine;
        Text = text;
        Colors = colors;
        ColorsIndicator = colorsIndicator;
        Creature = creature;
        Planeswalker = planeswalker;
        Battle = battle;
    }
}