using System.ComponentModel.DataAnnotations.Schema;

namespace MTG.Database.Models.Entities;

public class CardFace
{
    [ForeignKey("Card")]
    public Guid OracleId { get; set; }
    public int FaceId { get; set; }
    public string ManaCost { get; set; }
    public double ManaValue { get; set; }
    //public Name Name { get; set; }
    //public Typeline TypeLine { get; set; }
    //public Text Text { get; set; }
    public Color Colors { get; set; }
    public Color ColorsIndicator { get; set; }
    public string Power { get; set; }
    public string Toughness { get; set; }
    public string Loyalty { get; set; }
    public int Defense { get; set; }
}
