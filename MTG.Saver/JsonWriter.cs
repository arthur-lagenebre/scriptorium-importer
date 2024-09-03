using MTG.Importer.Models.Card;
using MTG.Saver.Interfaces;
using Newtonsoft.Json;

namespace MTG.Saver;

public class JsonWriter : IWriter
{
    public void WriteCards(IList<Card> cards, string path)
    {
        var json = JsonConvert.SerializeObject(cards, Formatting.Indented);

        File.WriteAllText(path, json);
    }
}
