using Microsoft.EntityFrameworkCore;

namespace MTG.Database.Models.Entities;

[PrimaryKey("OracleId")]
public class Card
{
    public Guid OracleId { get; set; }
    //public Name Name { get; set; }
    //public Typeline Typeline { get; set; }
    //public Text Text { get; set; }
    //public Cost Cost { get; set; }
    public Color Colors { get; set; }
    public Color ColorsIdentity { get; set; }
    public Color ColorsIndicator { get; set; }
    public string Layout { get; set; }
    public List<string> Keyword { get; set; }
    public List<string> ProducedMana { get; set; }
    //public Set Set { get; set; }
    //public List<Face> CardFaces { get; set; }
    //public List<RelatedCard> RelatedCards { get; set; }
    //public Creature? Creature { get; set; }
    //public Planeswalker? Planeswalker { get; set; }
    //public Vanguard? Vanguard { get; set; }
}
