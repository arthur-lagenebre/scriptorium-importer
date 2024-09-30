using MTG.Importer.Save.Interfaces;

namespace MTG.Importer.Save;

public class CardMapper : ICardMapper
{
    public Database.Models.Entities.Card Convert(Models.Card.Card card)
    {
        var dbCard = new Database.Models.Entities.Card
        {
            Names = []
        };

        dbCard.Names.Add(new Database.Models.Entities.CardName { FaceId = 0, OracleId = card.OracleId, Language = card.Name.Language, Value = card.Name.Value });

        return dbCard;
    }
}
