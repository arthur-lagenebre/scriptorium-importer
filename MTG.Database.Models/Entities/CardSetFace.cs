using System.ComponentModel.DataAnnotations.Schema;

namespace MTG.Database.Models.Entities;

public class CardSetFace
{
    [ForeignKey("CardSet")]
    public int CardSetId { get; set; }
    public int FaceId { get; set; }
    public string Artist { get; set; }
    public string FlavorText { get; set; }
    public string FlavorName { get; set; }
}
