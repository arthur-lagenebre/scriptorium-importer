using Microsoft.EntityFrameworkCore;

namespace MTG.Database.Models.Entities;

[PrimaryKey("OracleId")]
public class Card
{
    public Guid OracleId { get; set; }
    public string ManaCost { get; set; }
    public double ManaValue { get; set; }
    public Color Colors { get; set; }
    public Color ColorsIdentity { get; set; }
    public Color ColorsIndicator { get; set; }
    public string Layout { get; set; }
    public List<string> Keyword { get; set; }
    public List<string> ProducedMana { get; set; }
    public string Power { get; set; }
    public string Toughness { get; set; }
    public string Loyalty { get; set; }
    public string HandModifier { get; set; }
    public string LifeModifier { get; set; }

    public virtual List<CardText> Texts { get; set; }
    public virtual List<CardName> Names { get; set; }
    public virtual List<CardSupertype> Supertypes { get; set; }
    public virtual List<CardType> Types { get; set; }
    public virtual List<CardSubtype> Subtypes { get; set; }
    public virtual List<CardFace> Faces { get; set; }
    public virtual List<RelatedCard> RelatedCards { get; set; }
    public virtual List<CardSet> Sets { get; set; }
}
