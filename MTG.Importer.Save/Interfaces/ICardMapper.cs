namespace MTG.Importer.Save.Interfaces
{
    public interface ICardMapper
    {
        Database.Models.Entities.Card Convert(Models.Card.Card card);
    }
}